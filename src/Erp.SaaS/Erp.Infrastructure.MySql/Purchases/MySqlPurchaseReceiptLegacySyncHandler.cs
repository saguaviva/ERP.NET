using System.Globalization;
using Erp.Application.LegacySync;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Support;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Purchases;

public sealed class MySqlPurchaseReceiptLegacySyncHandler : ILegacyModuleSyncHandler
{
    private const int SyncCommandTimeoutSeconds = 300;
    private readonly MySqlConnectionFactory _saasConnectionFactory;
    private readonly LegacyMySqlConnectionFactory _legacyConnectionFactory;

    public MySqlPurchaseReceiptLegacySyncHandler(
        MySqlConnectionFactory saasConnectionFactory,
        LegacyMySqlConnectionFactory legacyConnectionFactory)
    {
        _saasConnectionFactory = saasConnectionFactory;
        _legacyConnectionFactory = legacyConnectionFactory;
    }

    public string ModuleKey => LegacySyncModuleKeys.PurchaseReceipts;
    public string DisplayName => "Compras / Recepciones";

    public async Task<LegacySyncModuleRunResult> RunAsync(
        LegacySyncModuleContext context,
        CancellationToken cancellationToken = default)
    {
        if (!_saasConnectionFactory.IsConfigured || !_legacyConnectionFactory.IsConfigured)
        {
            return new LegacySyncModuleRunResult
            {
                NewCheckpointValue = context.CheckpointValue
            };
        }

        await using var saasConnection = await _saasConnectionFactory.OpenConnectionAsync(cancellationToken);
        await using var legacyConnection = await _legacyConnectionFactory.OpenConnectionAsync(cancellationToken);

        var headers = await LoadLegacyHeadersAsync(legacyConnection, context.CompanyLegacyCenterCode, cancellationToken);
        var linesByReceipt = await LoadLegacyLinesAsync(legacyConnection, context.CompanyLegacyCenterCode, cancellationToken);
        var existingOrigins = await LoadExistingReceiptOriginsAsync(saasConnection, context.TenantId, context.CompanyId, cancellationToken);

        var inserted = 0;
        var updated = 0;
        var skipped = 0;
        var mappings = new List<LegacySyncMappingRecord>();
        var errors = new List<LegacySyncErrorRecord>();
        var seenReceiptNumbers = new HashSet<int>();

        await DeleteExistingMappingsAsync(saasConnection, context, cancellationToken);

        foreach (var header in headers)
        {
            if (!linesByReceipt.TryGetValue(header.ReceiptNumber, out var rawLines) || rawLines.Count == 0)
            {
                skipped++;
                continue;
            }

            var normalizedLines = NormalizeLegacyLines(rawLines);
            if (normalizedLines.Count == 0)
            {
                skipped++;
                continue;
            }

            if (existingOrigins.TryGetValue(header.ReceiptNumber, out var existingOrigin) &&
                !string.Equals(existingOrigin, "legacy", StringComparison.OrdinalIgnoreCase))
            {
                skipped++;
                errors.Add(new LegacySyncErrorRecord
                {
                    Stage = "UpsertPurchaseReceipt",
                    LegacyEntityKey = $"{context.CompanyLegacyCenterCode}/{header.LegacyDocumentType}/{header.LegacyDocumentNumber}",
                    ErrorMessage = "Existe una recepción web con el mismo número y no se puede sobreescribir desde la sincronización legacy.",
                    Payload = $"ReceiptNumber={header.ReceiptNumber}; Origin={existingOrigin}"
                });
                continue;
            }

            await using var transaction = await saasConnection.BeginTransactionAsync(cancellationToken);
            try
            {
                var nowUtc = DateTime.UtcNow;
                var exists = existingOrigins.ContainsKey(header.ReceiptNumber);
                var orderNumber = ResolveOrderNumber(normalizedLines);

                await UpsertImportedReceiptHeaderAsync(
                    saasConnection,
                    transaction,
                    context.TenantId,
                    context.CompanyId,
                    header,
                    orderNumber,
                    nowUtc,
                    cancellationToken);

                await ReplaceImportedReceiptLinesAsync(
                    saasConnection,
                    transaction,
                    context.TenantId,
                    context.CompanyId,
                    header,
                    orderNumber,
                    normalizedLines,
                    nowUtc,
                    cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                if (exists)
                {
                    updated++;
                }
                else
                {
                    inserted++;
                }

                existingOrigins[header.ReceiptNumber] = "legacy";
                seenReceiptNumbers.Add(header.ReceiptNumber);

                mappings.Add(new LegacySyncMappingRecord
                {
                    LegacyCenterCode = context.CompanyLegacyCenterCode,
                    LegacyDocumentType = header.LegacyDocumentType,
                    LegacyDocumentNumber = header.LegacyDocumentNumber.ToString(CultureInfo.InvariantCulture),
                    TargetEntityName = "PurchaseReceipt",
                    TargetEntityId = header.ReceiptNumber.ToString(CultureInfo.InvariantCulture)
                });

                foreach (var line in normalizedLines)
                {
                    mappings.Add(new LegacySyncMappingRecord
                    {
                        LegacyCenterCode = context.CompanyLegacyCenterCode,
                        LegacyDocumentType = header.LegacyDocumentType,
                        LegacyDocumentNumber = header.LegacyDocumentNumber.ToString(CultureInfo.InvariantCulture),
                        LegacyLineNumber = line.LegacyLineNumber,
                        TargetEntityName = "PurchaseReceiptLine",
                        TargetEntityId = $"{header.ReceiptNumber}:{line.LineNumber}"
                    });
                }
            }
            catch (Exception exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                skipped++;
                errors.Add(new LegacySyncErrorRecord
                {
                    Stage = "UpsertPurchaseReceipt",
                    LegacyEntityKey = $"{context.CompanyLegacyCenterCode}/{header.LegacyDocumentType}/{header.LegacyDocumentNumber}",
                    ErrorMessage = exception.Message,
                    Payload = $"ReceiptNumber={header.ReceiptNumber}; SupplierCode={header.SupplierCode}"
                });
            }
        }

        updated += await MarkMissingImportedReceiptsAsDeletedAsync(
            saasConnection,
            context.TenantId,
            context.CompanyId,
            seenReceiptNumbers,
            cancellationToken);

        return new LegacySyncModuleRunResult
        {
            RecordsInserted = inserted,
            RecordsUpdated = updated,
            RecordsSkipped = skipped,
            NewCheckpointValue = $"FULL@{DateTime.UtcNow:O}",
            Summary = $"Headers={headers.Count}; Insertados={inserted}; Actualizados={updated}; Omitidos={skipped}; Errores={errors.Count}",
            Mappings = mappings,
            Errors = errors
        };
    }

    private static async Task<List<LegacyPurchaseReceiptHeader>> LoadLegacyHeadersAsync(
        MySqlConnection connection,
        string centerCode,
        CancellationToken cancellationToken)
    {
        await using var command = CreateTimedCommand(connection);
        command.CommandText =
            """
            SELECT c.FRA,
                   c.TIPUS,
                   c.PROVE,
                   COALESCE(NULLIF(p.NOM, ''), CONCAT('Proveedor ', CAST(c.PROVE AS CHAR))) AS supplier_name,
                   COALESCE(p.NIF, '') AS supplier_tax_id,
                   CAST(c.DATA AS CHAR) AS raw_receipt_date,
                   COALESCE(c.OBSERV, '') AS notes
            FROM cactur c
            LEFT JOIN prove p
              ON p.CENTRO = c.CENTRO
             AND p.CODI = c.PROVE
            WHERE c.DOCUMENT = 'A'
              AND c.CENTRO = @centerCode
            ORDER BY c.TIPUS, c.FRA;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);

        var headersByReceipt = new Dictionary<int, LegacyPurchaseReceiptHeader>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var legacyType = NormalizeLegacyType(reader.GetStringOrEmpty("TIPUS"));
            var legacyNumber = reader.GetInt32(reader.GetOrdinal("FRA"));
            var receiptNumber = BuildReceiptNumber(legacyType, legacyNumber);
            var receiptDate = ParseLegacyDate(reader.GetStringOrEmpty("raw_receipt_date"));
            if (!receiptDate.HasValue)
            {
                continue;
            }

            if (!headersByReceipt.ContainsKey(receiptNumber))
            {
                headersByReceipt[receiptNumber] = new LegacyPurchaseReceiptHeader(
                    receiptNumber,
                    centerCode,
                    legacyType,
                    legacyNumber,
                    reader.GetInt32OrDefault("PROVE"),
                    reader.GetStringOrEmpty("supplier_name"),
                    reader.GetStringOrEmpty("supplier_tax_id"),
                    receiptDate.Value,
                    BuildReceiptSeries(centerCode, legacyType),
                    BuildReceiptNotes(legacyType, reader.GetStringOrEmpty("notes")));
            }
        }

        return headersByReceipt.Values
            .OrderBy(item => item.ReceiptNumber)
            .ToList();
    }

    private static async Task<Dictionary<int, List<LegacyPurchaseReceiptLine>>> LoadLegacyLinesAsync(
        MySqlConnection connection,
        string centerCode,
        CancellationToken cancellationToken)
    {
        await using var command = CreateTimedCommand(connection);
        command.CommandText =
            """
            SELECT TIPUS,
                   FRA,
                   NLINEA,
                   COALESCE(NULLIF(ARTICLE, ''), '') AS item_code,
                   COALESCE(DESCRI, '') AS description,
                   COALESCE(UNITATS, 0) AS quantity,
                   COALESCE(COMAN, 0) AS order_number
            FROM dcactu
            WHERE DOCUMENT = 'A'
              AND CENTRO = @centerCode
            ORDER BY TIPUS, FRA, NLINEA;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);

        var linesByReceipt = new Dictionary<int, List<LegacyPurchaseReceiptLine>>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var legacyType = NormalizeLegacyType(reader.GetStringOrEmpty("TIPUS"));
            var legacyNumber = reader.GetInt32(reader.GetOrdinal("FRA"));
            var receiptNumber = BuildReceiptNumber(legacyType, legacyNumber);

            if (!linesByReceipt.TryGetValue(receiptNumber, out var lines))
            {
                lines = [];
                linesByReceipt[receiptNumber] = lines;
            }

            lines.Add(new LegacyPurchaseReceiptLine(
                reader.GetInt32(reader.GetOrdinal("NLINEA")),
                legacyType,
                reader.GetStringOrEmpty("item_code"),
                reader.GetStringOrEmpty("description"),
                Math.Abs(reader.GetDecimalOrDefault("quantity")),
                reader.GetInt32OrDefault("order_number")));
        }

        return linesByReceipt;
    }

    private static List<ImportedPurchaseReceiptLine> NormalizeLegacyLines(IEnumerable<LegacyPurchaseReceiptLine> rawLines)
    {
        return rawLines
            .Where(line => line.Quantity > 0)
            .GroupBy(line => line.LegacyLineNumber)
            .Select(group =>
            {
                var first = group.First();
                var quantity = decimal.Round(group.Sum(item => item.Quantity), 3, MidpointRounding.AwayFromZero);
                var orderNumbers = group
                    .Select(item => item.LinkedLegacyOrderNumber)
                    .Where(value => value > 0)
                    .Distinct()
                    .ToArray();

                return new ImportedPurchaseReceiptLine(
                    first.LegacyLineNumber,
                    group.Min(item => item.LegacyLineNumber),
                    string.IsNullOrWhiteSpace(first.ItemCode) ? string.Empty : first.ItemCode.Trim(),
                    string.IsNullOrWhiteSpace(first.Description)
                        ? (!string.IsNullOrWhiteSpace(first.ItemCode) ? first.ItemCode.Trim() : $"Línea {first.LegacyLineNumber}")
                        : first.Description.Trim(),
                    quantity,
                    orderNumbers.Length == 0 ? 0 : BuildPurchaseOrderNumber(first.LegacyType, orderNumbers.Max()));
            })
            .OrderBy(line => line.LineNumber)
            .ToList();
    }

    private static int ResolveOrderNumber(IReadOnlyCollection<ImportedPurchaseReceiptLine> lines)
    {
        return lines
            .Select(line => line.LinkedOrderNumber)
            .Where(number => number > 0)
            .DefaultIfEmpty(0)
            .Max();
    }

    private static async Task<Dictionary<int, string>> LoadExistingReceiptOriginsAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        CancellationToken cancellationToken)
    {
        await using var command = CreateTimedCommand(connection);
        command.CommandText =
            """
            SELECT receipt_number, COALESCE(origin, 'saas') AS origin
            FROM purchase_order_receipts
            WHERE tenant_id = @tenantId
              AND company_id = @companyId;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());

        var items = new Dictionary<int, string>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            if (reader.IsDBNull(reader.GetOrdinal("receipt_number")))
            {
                continue;
            }

            items[reader.GetInt32(reader.GetOrdinal("receipt_number"))] = reader.GetStringOrEmpty("origin");
        }

        return items;
    }

    private static async Task UpsertImportedReceiptHeaderAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid tenantId,
        Guid companyId,
        LegacyPurchaseReceiptHeader header,
        int orderNumber,
        DateTime nowUtc,
        CancellationToken cancellationToken)
    {
        await using var command = CreateTimedCommand(connection, transaction);
        command.CommandText =
            """
            INSERT INTO purchase_order_receipts (
                receipt_id,
                receipt_series,
                receipt_number,
                tenant_id,
                company_id,
                order_number,
                receipt_date,
                warehouse,
                origin,
                is_deleted,
                legacy_source_system,
                legacy_center_code,
                legacy_document_type,
                legacy_document_number,
                synced_utc,
                carrier,
                supplier_reference,
                vehicle_plate,
                package_count,
                gross_weight_kg,
                notes,
                created_utc)
            VALUES (
                @receiptId,
                @receiptSeries,
                @receiptNumber,
                @tenantId,
                @companyId,
                @orderNumber,
                @receiptDate,
                NULL,
                'legacy',
                0,
                'legacy',
                @legacyCenterCode,
                @legacyDocumentType,
                @legacyDocumentNumber,
                @syncedUtc,
                NULL,
                NULL,
                NULL,
                NULL,
                NULL,
                @notes,
                @createdUtc)
            ON DUPLICATE KEY UPDATE
                receipt_series = VALUES(receipt_series),
                order_number = VALUES(order_number),
                receipt_date = VALUES(receipt_date),
                origin = VALUES(origin),
                is_deleted = VALUES(is_deleted),
                legacy_source_system = VALUES(legacy_source_system),
                legacy_center_code = VALUES(legacy_center_code),
                legacy_document_type = VALUES(legacy_document_type),
                legacy_document_number = VALUES(legacy_document_number),
                synced_utc = VALUES(synced_utc),
                notes = VALUES(notes);
            """;
        command.Parameters.AddWithValue("@receiptId", DeterministicGuid(tenantId, companyId, "purchase-receipt", header.ReceiptNumber).ToString());
        command.Parameters.AddWithValue("@receiptSeries", header.ReceiptSeries);
        command.Parameters.AddWithValue("@receiptNumber", header.ReceiptNumber);
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@orderNumber", orderNumber);
        command.Parameters.AddWithValue("@receiptDate", header.ReceiptDate);
        command.Parameters.AddWithValue("@legacyCenterCode", header.LegacyCenterCode);
        command.Parameters.AddWithValue("@legacyDocumentType", header.LegacyDocumentType);
        command.Parameters.AddWithValue("@legacyDocumentNumber", header.LegacyDocumentNumber.ToString(CultureInfo.InvariantCulture));
        command.Parameters.AddWithValue("@syncedUtc", nowUtc);
        command.Parameters.AddWithValue("@notes", DbValue(header.Notes));
        command.Parameters.AddWithValue("@createdUtc", nowUtc);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task ReplaceImportedReceiptLinesAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid tenantId,
        Guid companyId,
        LegacyPurchaseReceiptHeader header,
        int orderNumber,
        IReadOnlyCollection<ImportedPurchaseReceiptLine> lines,
        DateTime nowUtc,
        CancellationToken cancellationToken)
    {
        var receiptId = DeterministicGuid(tenantId, companyId, "purchase-receipt", header.ReceiptNumber).ToString();

        await using (var deleteCommand = CreateTimedCommand(connection, transaction))
        {
            deleteCommand.CommandText =
                """
                DELETE FROM purchase_order_receipt_lines
                WHERE receipt_id = @receiptId;
                """;
            deleteCommand.Parameters.AddWithValue("@receiptId", receiptId);
            await deleteCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        foreach (var line in lines)
        {
            await using var command = CreateTimedCommand(connection, transaction);
            command.CommandText =
                """
                INSERT INTO purchase_order_receipt_lines (
                    receipt_id,
                    tenant_id,
                    company_id,
                    order_number,
                    line_number,
                    description,
                    received_quantity,
                    legacy_source_system,
                    legacy_center_code,
                    legacy_document_type,
                    legacy_document_number,
                    legacy_line_number,
                    synced_utc)
                VALUES (
                    @receiptId,
                    @tenantId,
                    @companyId,
                    @orderNumber,
                    @lineOrderNumber,
                    @description,
                    @receivedQuantity,
                    'legacy',
                    @legacyCenterCode,
                    @legacyDocumentType,
                    @legacyDocumentNumber,
                    @legacyLineNumber,
                    @syncedUtc);
                """;
            command.Parameters.AddWithValue("@receiptId", receiptId);
            command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            command.Parameters.AddWithValue("@companyId", companyId.ToString());
            command.Parameters.AddWithValue("@orderNumber", orderNumber);
            command.Parameters.AddWithValue("@lineOrderNumber", line.LineNumber);
            command.Parameters.AddWithValue("@description", line.Description);
            command.Parameters.AddWithValue("@receivedQuantity", line.ReceivedQuantity);
            command.Parameters.AddWithValue("@legacyCenterCode", header.LegacyCenterCode);
            command.Parameters.AddWithValue("@legacyDocumentType", header.LegacyDocumentType);
            command.Parameters.AddWithValue("@legacyDocumentNumber", header.LegacyDocumentNumber.ToString(CultureInfo.InvariantCulture));
            command.Parameters.AddWithValue("@legacyLineNumber", line.LegacyLineNumber);
            command.Parameters.AddWithValue("@syncedUtc", nowUtc);
            await command.ExecuteNonQueryAsync(cancellationToken);
        }
    }

    private static async Task<int> MarkMissingImportedReceiptsAsDeletedAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        IReadOnlyCollection<int> seenReceiptNumbers,
        CancellationToken cancellationToken)
    {
        await using var command = CreateTimedCommand(connection);
        if (seenReceiptNumbers.Count == 0)
        {
            command.CommandText =
                """
                UPDATE purchase_order_receipts
                SET is_deleted = 1,
                    synced_utc = @syncedUtc
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND origin = 'legacy'
                  AND COALESCE(is_deleted, 0) = 0;
                """;
            command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            command.Parameters.AddWithValue("@companyId", companyId.ToString());
            command.Parameters.AddWithValue("@syncedUtc", DateTime.UtcNow);
            return await command.ExecuteNonQueryAsync(cancellationToken);
        }

        var parameterNames = new List<string>();
        for (var index = 0; index < seenReceiptNumbers.Count; index++)
        {
            parameterNames.Add($"@seen{index}");
        }

        command.CommandText =
            $"""
            UPDATE purchase_order_receipts
            SET is_deleted = 1,
                synced_utc = @syncedUtc
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND origin = 'legacy'
              AND COALESCE(is_deleted, 0) = 0
              AND receipt_number NOT IN ({string.Join(", ", parameterNames)});
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@syncedUtc", DateTime.UtcNow);
        var parameterIndex = 0;
        foreach (var receiptNumber in seenReceiptNumbers)
        {
            command.Parameters.AddWithValue(parameterNames[parameterIndex++], receiptNumber);
        }

        return await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task DeleteExistingMappingsAsync(
        MySqlConnection connection,
        LegacySyncModuleContext context,
        CancellationToken cancellationToken)
    {
        await using var command = CreateTimedCommand(connection);
        command.CommandText =
            """
            DELETE FROM legacy_sync_mappings
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND module_key = @moduleKey;
            """;
        command.Parameters.AddWithValue("@tenantId", context.TenantId.ToString());
        command.Parameters.AddWithValue("@companyId", context.CompanyId.ToString());
        command.Parameters.AddWithValue("@moduleKey", context.ModuleKey);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static string NormalizeLegacyType(string rawValue)
    {
        var value = rawValue?.Trim().ToUpperInvariant();
        return string.IsNullOrWhiteSpace(value) ? "GEN" : value;
    }

    private static int BuildReceiptNumber(string legacyType, int legacyNumber)
    {
        var prefix = legacyType switch
        {
            "M" => 1,
            "T" => 2,
            "F" => 3,
            "O" => 4,
            _ => 9
        };

        return (prefix * 1_000_000) + legacyNumber;
    }

    private static int BuildPurchaseOrderNumber(string legacyType, int legacyNumber)
    {
        var prefix = legacyType switch
        {
            "M" => 1,
            "T" => 2,
            "F" => 3,
            "O" => 4,
            _ => 9
        };

        return (prefix * 1_000_000) + legacyNumber;
    }

    private static DateTime? ParseLegacyDate(string rawValue)
    {
        var value = rawValue?.Trim();
        if (string.IsNullOrWhiteSpace(value) ||
            string.Equals(value, "0000-00-00", StringComparison.Ordinal) ||
            string.Equals(value, "0000-00-00 00:00:00", StringComparison.Ordinal))
        {
            return null;
        }

        if (DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out var parsed) ||
            DateTime.TryParse(value, CultureInfo.GetCultureInfo("es-ES"), DateTimeStyles.AssumeLocal, out parsed))
        {
            return parsed.Date;
        }

        return null;
    }

    private static string BuildReceiptSeries(string centerCode, string legacyType) =>
        $"AC-{centerCode.Trim().ToUpperInvariant()}-{legacyType}";

    private static string BuildReceiptNotes(string legacyType, string notes)
    {
        var suffix = string.IsNullOrWhiteSpace(notes) ? string.Empty : $" · {notes.Trim()}";
        return $"Legacy tipo {legacyType}{suffix}";
    }

    private static Guid DeterministicGuid(Guid tenantId, Guid companyId, string entityName, int number)
    {
        var seed = $"{tenantId:N}:{companyId:N}:{entityName}:{number}";
        var bytes = System.Security.Cryptography.MD5.HashData(System.Text.Encoding.UTF8.GetBytes(seed));
        return new Guid(bytes);
    }

    private static object DbValue(string value) => string.IsNullOrWhiteSpace(value) ? DBNull.Value : value;

    private static MySqlCommand CreateTimedCommand(MySqlConnection connection, MySqlTransaction? transaction = null)
    {
        var command = connection.CreateCommand();
        command.CommandTimeout = SyncCommandTimeoutSeconds;
        if (transaction is not null)
        {
            command.Transaction = transaction;
        }

        return command;
    }

    private sealed record LegacyPurchaseReceiptHeader(
        int ReceiptNumber,
        string LegacyCenterCode,
        string LegacyType,
        int LegacyDocumentNumber,
        int SupplierCode,
        string SupplierName,
        string SupplierTaxId,
        DateTime ReceiptDate,
        string ReceiptSeries,
        string Notes)
    {
        public string LegacyDocumentType => $"A-{LegacyType}";
    }

    private sealed record LegacyPurchaseReceiptLine(
        int LegacyLineNumber,
        string LegacyType,
        string ItemCode,
        string Description,
        decimal Quantity,
        int LinkedLegacyOrderNumber);

    private sealed record ImportedPurchaseReceiptLine(
        int LineNumber,
        int LegacyLineNumber,
        string ItemCode,
        string Description,
        decimal ReceivedQuantity,
        int LinkedOrderNumber);
}
