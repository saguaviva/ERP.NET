using System.Globalization;
using Erp.Application.LegacySync;
using Erp.Application.Purchases;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Support;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Purchases;

public sealed class MySqlPurchaseOrderLegacySyncHandler : ILegacyModuleSyncHandler
{
    private const int SyncCommandTimeoutSeconds = 300;
    private readonly MySqlConnectionFactory _saasConnectionFactory;
    private readonly LegacyMySqlConnectionFactory _legacyConnectionFactory;

    public MySqlPurchaseOrderLegacySyncHandler(
        MySqlConnectionFactory saasConnectionFactory,
        LegacyMySqlConnectionFactory legacyConnectionFactory)
    {
        _saasConnectionFactory = saasConnectionFactory;
        _legacyConnectionFactory = legacyConnectionFactory;
    }

    public string ModuleKey => LegacySyncModuleKeys.PurchaseOrders;
    public string DisplayName => "Compras / Pedidos";

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
        var linesByOrder = await LoadLegacyLinesAsync(legacyConnection, context.CompanyLegacyCenterCode, cancellationToken);
        var existingOrigins = await LoadExistingOrderOriginsAsync(saasConnection, context.TenantId, context.CompanyId, cancellationToken);

        var inserted = 0;
        var updated = 0;
        var skipped = 0;
        var mappings = new List<LegacySyncMappingRecord>();
        var errors = new List<LegacySyncErrorRecord>();
        var seenOrderNumbers = new HashSet<int>();

        await DeleteExistingMappingsAsync(saasConnection, context, cancellationToken);

        foreach (var header in headers)
        {
            if (!linesByOrder.TryGetValue(header.OrderNumber, out var rawLines) || rawLines.Count == 0)
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

            if (existingOrigins.TryGetValue(header.OrderNumber, out var existingOrigin) &&
                !string.Equals(existingOrigin, "legacy", StringComparison.OrdinalIgnoreCase))
            {
                skipped++;
                errors.Add(new LegacySyncErrorRecord
                {
                    Stage = "UpsertPurchaseOrder",
                    LegacyEntityKey = $"{context.CompanyLegacyCenterCode}/{header.LegacyDocumentType}/{header.LegacyDocumentNumber}",
                    ErrorMessage = "Existe un pedido web con el mismo número y no se puede sobreescribir desde la sincronización legacy.",
                    Payload = $"OrderNumber={header.OrderNumber}; Origin={existingOrigin}"
                });
                continue;
            }

            await using var transaction = await saasConnection.BeginTransactionAsync(cancellationToken);
            try
            {
                var nowUtc = DateTime.UtcNow;
                var exists = existingOrigins.ContainsKey(header.OrderNumber);

                await UpsertImportedOrderHeaderAsync(
                    saasConnection,
                    transaction,
                    context.TenantId,
                    context.CompanyId,
                    header,
                    normalizedLines,
                    nowUtc,
                    cancellationToken);

                await ReplaceImportedOrderLinesAsync(
                    saasConnection,
                    transaction,
                    context.TenantId,
                    context.CompanyId,
                    header,
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

                existingOrigins[header.OrderNumber] = "legacy";
                seenOrderNumbers.Add(header.OrderNumber);

                mappings.Add(new LegacySyncMappingRecord
                {
                    LegacyCenterCode = context.CompanyLegacyCenterCode,
                    LegacyDocumentType = header.LegacyDocumentType,
                    LegacyDocumentNumber = header.LegacyDocumentNumber.ToString(CultureInfo.InvariantCulture),
                    TargetEntityName = "PurchaseOrder",
                    TargetEntityId = header.OrderNumber.ToString(CultureInfo.InvariantCulture)
                });

                foreach (var line in normalizedLines)
                {
                    mappings.Add(new LegacySyncMappingRecord
                    {
                        LegacyCenterCode = context.CompanyLegacyCenterCode,
                        LegacyDocumentType = header.LegacyDocumentType,
                        LegacyDocumentNumber = header.LegacyDocumentNumber.ToString(CultureInfo.InvariantCulture),
                        LegacyLineNumber = line.LegacyLineNumber,
                        TargetEntityName = "PurchaseOrderLine",
                        TargetEntityId = $"{header.OrderNumber}:{line.LineNumber}"
                    });
                }
            }
            catch (Exception exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                skipped++;
                errors.Add(new LegacySyncErrorRecord
                {
                    Stage = "UpsertPurchaseOrder",
                    LegacyEntityKey = $"{context.CompanyLegacyCenterCode}/{header.LegacyDocumentType}/{header.LegacyDocumentNumber}",
                    ErrorMessage = exception.Message,
                    Payload = $"OrderNumber={header.OrderNumber}; SupplierCode={header.SupplierCode}"
                });
            }
        }

        updated += await MarkMissingImportedOrdersAsDeletedAsync(
            saasConnection,
            context.TenantId,
            context.CompanyId,
            seenOrderNumbers,
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

    private static async Task<List<LegacyPurchaseOrderHeader>> LoadLegacyHeadersAsync(
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
                   CAST(c.DATA AS CHAR) AS raw_document_date,
                   CAST(c.DATAENTREGA AS CHAR) AS raw_expected_date,
                   COALESCE(c.OBSERV, '') AS notes
            FROM cactur c
            LEFT JOIN prove p
              ON p.CENTRO = c.CENTRO
             AND p.CODI = c.PROVE
            WHERE c.DOCUMENT = 'C'
              AND c.CENTRO = @centerCode
            ORDER BY c.TIPUS, c.FRA;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);

        var headersByOrder = new Dictionary<int, LegacyPurchaseOrderHeader>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var legacyType = NormalizeLegacyType(reader.GetStringOrEmpty("TIPUS"));
            var legacyNumber = reader.GetInt32(reader.GetOrdinal("FRA"));
            var orderNumber = BuildOrderNumber(legacyType, legacyNumber);
            var documentDate = ParseLegacyDate(reader.GetStringOrEmpty("raw_document_date"));
            if (!documentDate.HasValue)
            {
                continue;
            }

            var header = new LegacyPurchaseOrderHeader(
                orderNumber,
                centerCode,
                legacyType,
                legacyNumber,
                reader.GetInt32OrDefault("PROVE"),
                reader.GetStringOrEmpty("supplier_name"),
                reader.GetStringOrEmpty("supplier_tax_id"),
                documentDate.Value,
                ParseLegacyDate(reader.GetStringOrEmpty("raw_expected_date")),
                BuildOrderNotes(legacyType, reader.GetStringOrEmpty("notes")));

            if (!headersByOrder.ContainsKey(orderNumber))
            {
                headersByOrder[orderNumber] = header;
            }
        }

        return headersByOrder.Values
            .OrderBy(item => item.OrderNumber)
            .ToList();
    }

    private static async Task<Dictionary<int, List<LegacyPurchaseOrderLine>>> LoadLegacyLinesAsync(
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
                   COALESCE(PERREBRE, 0) AS pending_quantity,
                   COALESCE(PREU, 0) AS unit_price,
                   COALESCE(IMPORT, 0) AS line_total
            FROM dcactu
            WHERE DOCUMENT = 'C'
              AND CENTRO = @centerCode
            ORDER BY TIPUS, FRA, NLINEA;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);

        var linesByOrder = new Dictionary<int, List<LegacyPurchaseOrderLine>>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var legacyType = NormalizeLegacyType(reader.GetStringOrEmpty("TIPUS"));
            var legacyNumber = reader.GetInt32(reader.GetOrdinal("FRA"));
            var orderNumber = BuildOrderNumber(legacyType, legacyNumber);

            if (!linesByOrder.TryGetValue(orderNumber, out var lines))
            {
                lines = [];
                linesByOrder[orderNumber] = lines;
            }

            lines.Add(new LegacyPurchaseOrderLine(
                reader.GetInt32(reader.GetOrdinal("NLINEA")),
                reader.GetStringOrEmpty("item_code"),
                reader.GetStringOrEmpty("description"),
                Math.Abs(reader.GetDecimalOrDefault("quantity")),
                reader.GetDecimalOrDefault("pending_quantity"),
                reader.GetDecimalOrDefault("unit_price"),
                reader.GetDecimalOrDefault("line_total")));
        }

        return linesByOrder;
    }

    private static List<ImportedPurchaseOrderLine> NormalizeLegacyLines(IEnumerable<LegacyPurchaseOrderLine> legacyLines)
    {
        return legacyLines
            .Where(line => Math.Abs(line.Quantity) > 0)
            .GroupBy(line => line.LegacyLineNumber)
            .Select(group =>
            {
                var first = group.First();
                var quantity = decimal.Round(group.Sum(item => Math.Abs(item.Quantity)), 3, MidpointRounding.AwayFromZero);
                var pending = decimal.Round(group.Sum(item => Math.Max(item.PendingQuantity, 0m)), 3, MidpointRounding.AwayFromZero);
                pending = Math.Min(quantity, pending);
                var received = decimal.Round(Math.Max(quantity - pending, 0m), 3, MidpointRounding.AwayFromZero);
                var weightedTotal = group.Sum(item => item.LineTotal);
                var weightedPrice = quantity > 0 && weightedTotal > 0
                    ? decimal.Round(weightedTotal / quantity, 4, MidpointRounding.AwayFromZero)
                    : decimal.Round(group.Max(item => item.UnitPrice), 4, MidpointRounding.AwayFromZero);

                return new ImportedPurchaseOrderLine(
                    first.LegacyLineNumber,
                    group.Min(item => item.LegacyLineNumber),
                    string.IsNullOrWhiteSpace(first.ItemCode) ? string.Empty : first.ItemCode.Trim(),
                    string.IsNullOrWhiteSpace(first.Description)
                        ? (!string.IsNullOrWhiteSpace(first.ItemCode) ? first.ItemCode.Trim() : $"Línea {first.LegacyLineNumber}")
                        : first.Description.Trim(),
                    quantity,
                    received,
                    weightedPrice);
            })
            .OrderBy(line => line.LineNumber)
            .ToList();
    }

    private static async Task<Dictionary<int, string>> LoadExistingOrderOriginsAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        CancellationToken cancellationToken)
    {
        await using var command = CreateTimedCommand(connection);
        command.CommandText =
            """
            SELECT order_number, COALESCE(origin, 'saas') AS origin
            FROM purchase_orders
            WHERE tenant_id = @tenantId
              AND company_id = @companyId;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());

        var items = new Dictionary<int, string>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items[reader.GetInt32(reader.GetOrdinal("order_number"))] = reader.GetStringOrEmpty("origin");
        }

        return items;
    }

    private static async Task UpsertImportedOrderHeaderAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid tenantId,
        Guid companyId,
        LegacyPurchaseOrderHeader header,
        IReadOnlyCollection<ImportedPurchaseOrderLine> lines,
        DateTime nowUtc,
        CancellationToken cancellationToken)
    {
        await using var command = CreateTimedCommand(connection, transaction);
        command.CommandText =
            """
            INSERT INTO purchase_orders (
                tenant_id,
                company_id,
                order_number,
                supplier_code,
                supplier_name,
                supplier_tax_id,
                document_date,
                expected_date,
                status,
                origin,
                is_deleted,
                legacy_source_system,
                legacy_center_code,
                legacy_document_type,
                legacy_document_number,
                synced_utc,
                notes,
                created_utc,
                updated_utc)
            VALUES (
                @tenantId,
                @companyId,
                @orderNumber,
                @supplierCode,
                @supplierName,
                @supplierTaxId,
                @documentDate,
                @expectedDate,
                @status,
                'legacy',
                0,
                'legacy',
                @legacyCenterCode,
                @legacyDocumentType,
                @legacyDocumentNumber,
                @syncedUtc,
                @notes,
                @createdUtc,
                @updatedUtc)
            ON DUPLICATE KEY UPDATE
                supplier_code = VALUES(supplier_code),
                supplier_name = VALUES(supplier_name),
                supplier_tax_id = VALUES(supplier_tax_id),
                document_date = VALUES(document_date),
                expected_date = VALUES(expected_date),
                status = VALUES(status),
                origin = VALUES(origin),
                is_deleted = VALUES(is_deleted),
                legacy_source_system = VALUES(legacy_source_system),
                legacy_center_code = VALUES(legacy_center_code),
                legacy_document_type = VALUES(legacy_document_type),
                legacy_document_number = VALUES(legacy_document_number),
                synced_utc = VALUES(synced_utc),
                notes = VALUES(notes),
                updated_utc = VALUES(updated_utc);
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@orderNumber", header.OrderNumber);
        command.Parameters.AddWithValue("@supplierCode", header.SupplierCode);
        command.Parameters.AddWithValue("@supplierName", header.SupplierName);
        command.Parameters.AddWithValue("@supplierTaxId", DbValue(header.SupplierTaxId));
        command.Parameters.AddWithValue("@documentDate", header.DocumentDate);
        command.Parameters.AddWithValue("@expectedDate", DbValue(header.ExpectedDate));
        command.Parameters.AddWithValue("@status", DetermineStatus(lines));
        command.Parameters.AddWithValue("@legacyCenterCode", header.LegacyCenterCode);
        command.Parameters.AddWithValue("@legacyDocumentType", header.LegacyDocumentType);
        command.Parameters.AddWithValue("@legacyDocumentNumber", header.LegacyDocumentNumber.ToString(CultureInfo.InvariantCulture));
        command.Parameters.AddWithValue("@syncedUtc", nowUtc);
        command.Parameters.AddWithValue("@notes", DbValue(header.Notes));
        command.Parameters.AddWithValue("@createdUtc", nowUtc);
        command.Parameters.AddWithValue("@updatedUtc", nowUtc);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task ReplaceImportedOrderLinesAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid tenantId,
        Guid companyId,
        LegacyPurchaseOrderHeader header,
        IReadOnlyCollection<ImportedPurchaseOrderLine> lines,
        DateTime nowUtc,
        CancellationToken cancellationToken)
    {
        await using (var deleteCommand = CreateTimedCommand(connection, transaction))
        {
            deleteCommand.CommandText =
                """
                DELETE FROM purchase_order_lines
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND order_number = @orderNumber;
                """;
            deleteCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            deleteCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
            deleteCommand.Parameters.AddWithValue("@orderNumber", header.OrderNumber);
            await deleteCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        foreach (var line in lines)
        {
            await using var insertCommand = CreateTimedCommand(connection, transaction);
            insertCommand.CommandText =
                """
                INSERT INTO purchase_order_lines (
                    tenant_id,
                    company_id,
                    order_number,
                    line_number,
                    item_code,
                    description,
                    quantity,
                    received_quantity,
                    unit_of_measure,
                    unit_price,
                    expected_date,
                    last_received_utc,
                    legacy_source_system,
                    legacy_center_code,
                    legacy_document_type,
                    legacy_document_number,
                    legacy_line_number,
                    synced_utc,
                    notes)
                VALUES (
                    @tenantId,
                    @companyId,
                    @orderNumber,
                    @lineNumber,
                    @itemCode,
                    @description,
                    @quantity,
                    @receivedQuantity,
                    NULL,
                    @unitPrice,
                    NULL,
                    NULL,
                    'legacy',
                    @legacyCenterCode,
                    @legacyDocumentType,
                    @legacyDocumentNumber,
                    @legacyLineNumber,
                    @syncedUtc,
                    NULL);
                """;
            insertCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            insertCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
            insertCommand.Parameters.AddWithValue("@orderNumber", header.OrderNumber);
            insertCommand.Parameters.AddWithValue("@lineNumber", line.LineNumber);
            insertCommand.Parameters.AddWithValue("@itemCode", DbValue(line.ItemCode));
            insertCommand.Parameters.AddWithValue("@description", line.Description);
            insertCommand.Parameters.AddWithValue("@quantity", line.Quantity);
            insertCommand.Parameters.AddWithValue("@receivedQuantity", line.ReceivedQuantity);
            insertCommand.Parameters.AddWithValue("@unitPrice", line.UnitPrice);
            insertCommand.Parameters.AddWithValue("@legacyCenterCode", header.LegacyCenterCode);
            insertCommand.Parameters.AddWithValue("@legacyDocumentType", header.LegacyDocumentType);
            insertCommand.Parameters.AddWithValue("@legacyDocumentNumber", header.LegacyDocumentNumber.ToString(CultureInfo.InvariantCulture));
            insertCommand.Parameters.AddWithValue("@legacyLineNumber", line.LegacyLineNumber);
            insertCommand.Parameters.AddWithValue("@syncedUtc", nowUtc);
            await insertCommand.ExecuteNonQueryAsync(cancellationToken);
        }
    }

    private static async Task<int> MarkMissingImportedOrdersAsDeletedAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        IReadOnlyCollection<int> seenOrderNumbers,
        CancellationToken cancellationToken)
    {
        await using var command = CreateTimedCommand(connection);
        if (seenOrderNumbers.Count == 0)
        {
            command.CommandText =
                """
                UPDATE purchase_orders
                SET is_deleted = 1,
                    status = @status,
                    synced_utc = @syncedUtc,
                    updated_utc = @updatedUtc
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND origin = 'legacy'
                  AND COALESCE(is_deleted, 0) = 0;
                """;
            command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            command.Parameters.AddWithValue("@companyId", companyId.ToString());
            command.Parameters.AddWithValue("@status", PurchaseOrderStatuses.Cancelled);
            command.Parameters.AddWithValue("@syncedUtc", DateTime.UtcNow);
            command.Parameters.AddWithValue("@updatedUtc", DateTime.UtcNow);
            return await command.ExecuteNonQueryAsync(cancellationToken);
        }

        var parameterNames = new List<string>();
        for (var index = 0; index < seenOrderNumbers.Count; index++)
        {
            parameterNames.Add($"@seen{index}");
        }

        command.CommandText =
            $"""
            UPDATE purchase_orders
            SET is_deleted = 1,
                status = @status,
                synced_utc = @syncedUtc,
                updated_utc = @updatedUtc
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND origin = 'legacy'
              AND COALESCE(is_deleted, 0) = 0
              AND order_number NOT IN ({string.Join(", ", parameterNames)});
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@status", PurchaseOrderStatuses.Cancelled);
        command.Parameters.AddWithValue("@syncedUtc", DateTime.UtcNow);
        command.Parameters.AddWithValue("@updatedUtc", DateTime.UtcNow);
        var parameterIndex = 0;
        foreach (var orderNumber in seenOrderNumbers)
        {
            command.Parameters.AddWithValue(parameterNames[parameterIndex++], orderNumber);
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

    private static int BuildOrderNumber(string legacyType, int legacyNumber)
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

    private static string DetermineStatus(IReadOnlyCollection<ImportedPurchaseOrderLine> lines)
    {
        var totalQuantity = lines.Sum(line => line.Quantity);
        var totalReceivedQuantity = lines.Sum(line => line.ReceivedQuantity);
        return totalQuantity <= 0 || totalReceivedQuantity <= 0
            ? PurchaseOrderStatuses.Sent
            : totalReceivedQuantity >= totalQuantity
            ? PurchaseOrderStatuses.Received
            : totalReceivedQuantity > 0
                ? PurchaseOrderStatuses.PartiallyReceived
                : PurchaseOrderStatuses.Sent;
    }

    private static string BuildOrderNotes(string legacyType, string notes)
    {
        var suffix = string.IsNullOrWhiteSpace(notes) ? string.Empty : $" · {notes.Trim()}";
        return $"Legacy tipo {legacyType}{suffix}";
    }

    private static object DbValue(string value) => string.IsNullOrWhiteSpace(value) ? DBNull.Value : value;
    private static object DbValue(DateTime? value) => value.HasValue ? value.Value : DBNull.Value;

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

    private sealed record LegacyPurchaseOrderHeader(
        int OrderNumber,
        string LegacyCenterCode,
        string LegacyType,
        int LegacyDocumentNumber,
        int SupplierCode,
        string SupplierName,
        string SupplierTaxId,
        DateTime DocumentDate,
        DateTime? ExpectedDate,
        string Notes)
    {
        public string LegacyDocumentType => $"C-{LegacyType}";
    }

    private sealed record LegacyPurchaseOrderLine(
        int LegacyLineNumber,
        string ItemCode,
        string Description,
        decimal Quantity,
        decimal PendingQuantity,
        decimal UnitPrice,
        decimal LineTotal);

    private sealed record ImportedPurchaseOrderLine(
        int LineNumber,
        int LegacyLineNumber,
        string ItemCode,
        string Description,
        decimal Quantity,
        decimal ReceivedQuantity,
        decimal UnitPrice);
}
