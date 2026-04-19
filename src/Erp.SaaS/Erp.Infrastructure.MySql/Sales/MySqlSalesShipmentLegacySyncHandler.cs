using Erp.Application.LegacySync;
using Erp.Application.Sales;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Support;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Sales;

public sealed class MySqlSalesShipmentLegacySyncHandler : ILegacyModuleSyncHandler
{
    private const int SyncCommandTimeoutSeconds = 300;
    private readonly MySqlConnectionFactory _saasConnectionFactory;
    private readonly LegacyMySqlConnectionFactory _legacyConnectionFactory;

    public MySqlSalesShipmentLegacySyncHandler(
        MySqlConnectionFactory saasConnectionFactory,
        LegacyMySqlConnectionFactory legacyConnectionFactory)
    {
        _saasConnectionFactory = saasConnectionFactory;
        _legacyConnectionFactory = legacyConnectionFactory;
    }

    public string ModuleKey => LegacySyncModuleKeys.SalesShipments;
    public string DisplayName => "Ventas / Albaranes";

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

        var linesByShipment = await LoadLegacyShipmentLinesAsync(legacyConnection, context.CompanyLegacyCenterCode, cancellationToken);
        var headers = await LoadLegacyShipmentHeadersAsync(legacyConnection, context.CompanyLegacyCenterCode, cancellationToken);
        var orderNumbersByShipment = BuildOrderNumbersByShipment(linesByShipment);
        var existingShipments = await LoadExistingShipmentOriginsAsync(saasConnection, context.TenantId, context.CompanyId, cancellationToken);

        var inserted = 0;
        var updated = 0;
        var skipped = 0;
        var mappings = new List<LegacySyncMappingRecord>();
        var errors = new List<LegacySyncErrorRecord>();
        var seenLegacyShipmentNumbers = new HashSet<int>();

        await DeleteExistingMappingsAsync(saasConnection, context, cancellationToken);

        foreach (var rawHeader in headers)
        {
            if (!linesByShipment.TryGetValue(rawHeader.ShipmentNumber, out var legacyLines) || legacyLines.Count == 0)
            {
                skipped++;
                continue;
            }

            var header = rawHeader with
            {
                OrderNumber = orderNumbersByShipment.TryGetValue(rawHeader.ShipmentNumber, out var orderNumber)
                    ? orderNumber
                    : 0
            };

            var normalizedLines = NormalizeLegacyShipmentLines(legacyLines);
            if (normalizedLines.Count == 0)
            {
                skipped++;
                continue;
            }

            if (existingShipments.TryGetValue(header.ShipmentNumber, out var existingOrigin) &&
                !string.Equals(existingOrigin, SalesOrderOrigins.Legacy, StringComparison.OrdinalIgnoreCase))
            {
                skipped++;
                errors.Add(new LegacySyncErrorRecord
                {
                    Stage = "UpsertShipment",
                    LegacyEntityKey = $"{context.CompanyLegacyCenterCode}/A/{header.ShipmentNumber}",
                    ErrorMessage = "Existe un albarán SaaS con el mismo número y no se puede sobreescribir desde la sincronización legacy.",
                    Payload = $"ShipmentNumber={header.ShipmentNumber}; Origin={existingOrigin}"
                });
                continue;
            }

            await using var transaction = await saasConnection.BeginTransactionAsync(cancellationToken);
            try
            {
                var nowUtc = DateTime.UtcNow;
                var exists = existingShipments.ContainsKey(header.ShipmentNumber);

                await UpsertImportedShipmentHeaderAsync(
                    saasConnection,
                    transaction,
                    context.TenantId,
                    context.CompanyId,
                    context.CompanyLegacyCenterCode,
                    header,
                    nowUtc,
                    cancellationToken);

                await ReplaceImportedShipmentLinesAsync(
                    saasConnection,
                    transaction,
                    context.TenantId,
                    context.CompanyId,
                    context.CompanyLegacyCenterCode,
                    header.ShipmentNumber,
                    header.OrderNumber,
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

                existingShipments[header.ShipmentNumber] = SalesOrderOrigins.Legacy;
                seenLegacyShipmentNumbers.Add(header.ShipmentNumber);

                mappings.Add(new LegacySyncMappingRecord
                {
                    LegacyCenterCode = context.CompanyLegacyCenterCode,
                    LegacyDocumentType = "A",
                    LegacyDocumentNumber = header.ShipmentNumber.ToString(),
                    TargetEntityName = "SalesShipment",
                    TargetEntityId = header.ShipmentNumber.ToString()
                });

                foreach (var line in normalizedLines)
                {
                    mappings.Add(new LegacySyncMappingRecord
                    {
                        LegacyCenterCode = context.CompanyLegacyCenterCode,
                        LegacyDocumentType = "A",
                        LegacyDocumentNumber = header.ShipmentNumber.ToString(),
                        LegacyLineNumber = line.LineNumber,
                        TargetEntityName = "SalesShipmentLine",
                        TargetEntityId = $"{header.ShipmentNumber}:{line.LineNumber}"
                    });
                }
            }
            catch (Exception exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                skipped++;
                errors.Add(new LegacySyncErrorRecord
                {
                    Stage = "UpsertShipment",
                    LegacyEntityKey = $"{context.CompanyLegacyCenterCode}/A/{header.ShipmentNumber}",
                    ErrorMessage = exception.Message,
                    Payload = $"ShipmentNumber={header.ShipmentNumber}; OrderNumber={header.OrderNumber}"
                });
            }
        }

        updated += await MarkMissingImportedShipmentsAsDeletedAsync(
            saasConnection,
            context.TenantId,
            context.CompanyId,
            seenLegacyShipmentNumbers,
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

    private static async Task<List<LegacySalesShipmentHeader>> LoadLegacyShipmentHeadersAsync(
        MySqlConnection connection,
        string centerCode,
        CancellationToken cancellationToken)
    {
        await using var command = CreateTimedCommand(connection);
        command.CommandText =
            """
            SELECT f.FRA,
                   f.CLIENT,
                   COALESCE(NULLIF(c.NOM, ''), CONCAT('Cliente ', CAST(f.CLIENT AS CHAR))) AS client_name,
                   COALESCE(c.NIF, '') AS client_tax_id,
                   f.DATA,
                   COALESCE(f.ALBCLI, '') AS customer_reference,
                   COALESCE(f.OBSERV, '') AS notes
            FROM factur f
            LEFT JOIN clients c
              ON c.CENTRO = f.CENTRO
             AND c.CODI = f.CLIENT
            WHERE f.DOCUMENT = 'A'
              AND f.CENTRO = @centerCode
            ORDER BY f.FRA;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);

        var headers = new List<LegacySalesShipmentHeader>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            headers.Add(new LegacySalesShipmentHeader(
                reader.GetInt32(reader.GetOrdinal("FRA")),
                0,
                reader.GetInt32OrDefault("CLIENT"),
                reader.GetStringOrEmpty("client_name"),
                reader.GetStringOrEmpty("client_tax_id"),
                reader.GetDateTime(reader.GetOrdinal("DATA")).Date,
                BuildShipmentNotes(reader.GetStringOrEmpty("customer_reference"), reader.GetStringOrEmpty("notes"))));
        }

        return headers;
    }

    private static async Task<Dictionary<int, List<LegacySalesShipmentLine>>> LoadLegacyShipmentLinesAsync(
        MySqlConnection connection,
        string centerCode,
        CancellationToken cancellationToken)
    {
        await using var command = CreateTimedCommand(connection);
        command.CommandText =
            """
            SELECT FRA,
                   NLINEA,
                   COALESCE(NULLIF(COMAN, 0), 0) AS order_number,
                   COALESCE(NULLIF(MOSTRA, ''), NULLIF(NCCODE, ''), '') AS item_code,
                   COALESCE(DESCRI, '') AS description,
                   COALESCE(UNITATS, 0) AS quantity
            FROM dfactu
            WHERE DOCUMENT = 'A'
              AND CENTRO = @centerCode
            ORDER BY FRA, NLINEA;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);

        var linesByShipment = new Dictionary<int, List<LegacySalesShipmentLine>>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var shipmentNumber = reader.GetInt32(reader.GetOrdinal("FRA"));
            if (!linesByShipment.TryGetValue(shipmentNumber, out var lines))
            {
                lines = [];
                linesByShipment[shipmentNumber] = lines;
            }

            lines.Add(new LegacySalesShipmentLine(
                reader.GetInt32(reader.GetOrdinal("NLINEA")),
                reader.GetInt32OrDefault("order_number"),
                reader.GetStringOrEmpty("item_code"),
                reader.GetStringOrEmpty("description"),
                Math.Abs(reader.GetDecimalOrDefault("quantity"))));
        }

        return linesByShipment;
    }

    private static List<ImportedSalesShipmentLine> NormalizeLegacyShipmentLines(IEnumerable<LegacySalesShipmentLine> legacyLines)
    {
        return legacyLines
            .Where(line => line.Quantity > 0)
            .GroupBy(line => line.LineNumber)
            .Select(group =>
            {
                var first = group.First();
                var description = string.IsNullOrWhiteSpace(first.Description)
                    ? (!string.IsNullOrWhiteSpace(first.ItemCode) ? first.ItemCode : $"Línea {first.LineNumber}")
                    : first.Description.Trim();

                return new ImportedSalesShipmentLine(
                    first.LineNumber,
                    description,
                    decimal.Round(group.Sum(item => item.Quantity), 3, MidpointRounding.AwayFromZero));
            })
            .OrderBy(line => line.LineNumber)
            .ToList();
    }

    private static Dictionary<int, int> BuildOrderNumbersByShipment(IReadOnlyDictionary<int, List<LegacySalesShipmentLine>> linesByShipment)
    {
        var items = new Dictionary<int, int>();
        foreach (var pair in linesByShipment)
        {
            var orderNumber = pair.Value
                .Select(line => line.OrderNumber)
                .Where(number => number > 0)
                .DefaultIfEmpty(0)
                .Max();

            items[pair.Key] = orderNumber;
        }

        return items;
    }

    private static async Task<Dictionary<int, string>> LoadExistingShipmentOriginsAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        CancellationToken cancellationToken)
    {
        await using var command = CreateTimedCommand(connection);
        command.CommandText =
            """
            SELECT shipment_number, COALESCE(origin, 'saas') AS origin
            FROM sales_order_shipments
            WHERE tenant_id = @tenantId
              AND company_id = @companyId;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());

        var items = new Dictionary<int, string>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var shipmentNumberOrdinal = reader.GetOrdinal("shipment_number");
            if (reader.IsDBNull(shipmentNumberOrdinal))
            {
                continue;
            }

            items[reader.GetInt32(shipmentNumberOrdinal)] = reader.GetStringOrEmpty("origin");
        }

        return items;
    }

    private static async Task UpsertImportedShipmentHeaderAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid tenantId,
        Guid companyId,
        string legacyCenterCode,
        LegacySalesShipmentHeader header,
        DateTime nowUtc,
        CancellationToken cancellationToken)
    {
        await using var command = CreateTimedCommand(connection, transaction);
        command.CommandText =
            """
            INSERT INTO sales_order_shipments (
                shipment_id,
                shipment_series,
                shipment_number,
                tenant_id,
                company_id,
                order_number,
                shipment_date,
                warehouse,
                origin,
                is_deleted,
                legacy_source_system,
                legacy_center_code,
                legacy_document_type,
                legacy_document_number,
                synced_utc,
                invoice_status,
                notes,
                created_utc)
            VALUES (
                @shipmentId,
                @shipmentSeries,
                @shipmentNumber,
                @tenantId,
                @companyId,
                @orderNumber,
                @shipmentDate,
                @warehouse,
                @origin,
                0,
                @legacySourceSystem,
                @legacyCenterCode,
                @legacyDocumentType,
                @legacyDocumentNumber,
                @syncedUtc,
                @invoiceStatus,
                @notes,
                @createdUtc)
            ON DUPLICATE KEY UPDATE
                order_number = VALUES(order_number),
                shipment_date = VALUES(shipment_date),
                warehouse = VALUES(warehouse),
                origin = VALUES(origin),
                is_deleted = VALUES(is_deleted),
                legacy_source_system = VALUES(legacy_source_system),
                legacy_center_code = VALUES(legacy_center_code),
                legacy_document_type = VALUES(legacy_document_type),
                legacy_document_number = VALUES(legacy_document_number),
                synced_utc = VALUES(synced_utc),
                notes = VALUES(notes);
            """;
        command.Parameters.AddWithValue("@shipmentId", DeterministicGuid(tenantId, companyId, "shipment", header.ShipmentNumber).ToString());
        command.Parameters.AddWithValue("@shipmentSeries", BuildShipmentSeries(legacyCenterCode));
        command.Parameters.AddWithValue("@shipmentNumber", header.ShipmentNumber);
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@orderNumber", header.OrderNumber);
        command.Parameters.AddWithValue("@shipmentDate", header.ShipmentDate);
        command.Parameters.AddWithValue("@warehouse", DBNull.Value);
        command.Parameters.AddWithValue("@origin", SalesOrderOrigins.Legacy);
        command.Parameters.AddWithValue("@legacySourceSystem", "legacy");
        command.Parameters.AddWithValue("@legacyCenterCode", legacyCenterCode);
        command.Parameters.AddWithValue("@legacyDocumentType", "A");
        command.Parameters.AddWithValue("@legacyDocumentNumber", header.ShipmentNumber.ToString());
        command.Parameters.AddWithValue("@syncedUtc", nowUtc);
        command.Parameters.AddWithValue("@invoiceStatus", "Pending");
        command.Parameters.AddWithValue("@notes", DbValue(header.Notes));
        command.Parameters.AddWithValue("@createdUtc", nowUtc);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task ReplaceImportedShipmentLinesAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid tenantId,
        Guid companyId,
        string legacyCenterCode,
        int shipmentNumber,
        int orderNumber,
        IReadOnlyCollection<ImportedSalesShipmentLine> lines,
        DateTime nowUtc,
        CancellationToken cancellationToken)
    {
        var shipmentId = DeterministicGuid(tenantId, companyId, "shipment", shipmentNumber);

        await using (var deleteCommand = CreateTimedCommand(connection, transaction))
        {
            deleteCommand.CommandText = "DELETE FROM sales_order_shipment_lines WHERE shipment_id = @shipmentId;";
            deleteCommand.Parameters.AddWithValue("@shipmentId", shipmentId.ToString());
            await deleteCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        foreach (var line in lines)
        {
            await using var command = CreateTimedCommand(connection, transaction);
            command.CommandText =
                """
                INSERT INTO sales_order_shipment_lines (
                    shipment_id,
                    tenant_id,
                    company_id,
                    order_number,
                    line_number,
                    description,
                    shipped_quantity)
                VALUES (
                    @shipmentId,
                    @tenantId,
                    @companyId,
                    @orderNumber,
                    @lineNumber,
                    @description,
                    @shippedQuantity);
                """;
            command.Parameters.AddWithValue("@shipmentId", shipmentId.ToString());
            command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            command.Parameters.AddWithValue("@companyId", companyId.ToString());
            command.Parameters.AddWithValue("@orderNumber", orderNumber);
            command.Parameters.AddWithValue("@lineNumber", line.LineNumber);
            command.Parameters.AddWithValue("@description", line.Description);
            command.Parameters.AddWithValue("@shippedQuantity", line.ShippedQuantity);
            await command.ExecuteNonQueryAsync(cancellationToken);
        }
    }

    private static async Task<int> MarkMissingImportedShipmentsAsDeletedAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        IReadOnlyCollection<int> visibleLegacyShipmentNumbers,
        CancellationToken cancellationToken)
    {
        await using var selectCommand = CreateTimedCommand(connection);
        selectCommand.CommandText =
            """
            SELECT shipment_number
            FROM sales_order_shipments
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND COALESCE(origin, 'saas') = @origin
              AND COALESCE(is_deleted, 0) = 0;
            """;
        selectCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        selectCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
        selectCommand.Parameters.AddWithValue("@origin", SalesOrderOrigins.Legacy);

        var missingShipments = new List<int>();
        await using (var reader = await selectCommand.ExecuteReaderAsync(cancellationToken))
        {
            while (await reader.ReadAsync(cancellationToken))
            {
                var shipmentNumberOrdinal = reader.GetOrdinal("shipment_number");
                if (reader.IsDBNull(shipmentNumberOrdinal))
                {
                    continue;
                }

                var shipmentNumber = reader.GetInt32(shipmentNumberOrdinal);
                if (!visibleLegacyShipmentNumbers.Contains(shipmentNumber))
                {
                    missingShipments.Add(shipmentNumber);
                }
            }
        }

        if (missingShipments.Count == 0)
        {
            return 0;
        }

        foreach (var shipmentNumber in missingShipments)
        {
            await using var updateCommand = CreateTimedCommand(connection);
            updateCommand.CommandText =
                """
                UPDATE sales_order_shipments
                SET is_deleted = 1,
                    synced_utc = @syncedUtc
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND shipment_number = @shipmentNumber;
                """;
            updateCommand.Parameters.AddWithValue("@syncedUtc", DateTime.UtcNow);
            updateCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            updateCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
            updateCommand.Parameters.AddWithValue("@shipmentNumber", shipmentNumber);
            await updateCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        return missingShipments.Count;
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

    private static string BuildShipmentSeries(string companyLegacyCenterCode) =>
        $"AV-{(string.IsNullOrWhiteSpace(companyLegacyCenterCode) ? "GEN" : companyLegacyCenterCode.Trim().ToUpperInvariant())}";

    private static string BuildShipmentNotes(string customerReference, string notes)
    {
        var parts = new List<string>();
        if (!string.IsNullOrWhiteSpace(customerReference))
        {
            parts.Add($"Ref. cliente: {customerReference.Trim()}");
        }

        if (!string.IsNullOrWhiteSpace(notes))
        {
            parts.Add(notes.Trim());
        }

        return string.Join(Environment.NewLine, parts);
    }

    private static Guid DeterministicGuid(Guid tenantId, Guid companyId, string entityName, int number)
    {
        var seed = $"{tenantId:N}:{companyId:N}:{entityName}:{number}";
        var bytes = System.Security.Cryptography.MD5.HashData(System.Text.Encoding.UTF8.GetBytes(seed));
        return new Guid(bytes);
    }

    private static object DbValue(string? value) =>
        string.IsNullOrWhiteSpace(value) ? DBNull.Value : value.Trim();

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

    private sealed record LegacySalesShipmentHeader(
        int ShipmentNumber,
        int OrderNumber,
        int ClientCode,
        string ClientName,
        string ClientTaxId,
        DateTime ShipmentDate,
        string Notes);

    private sealed record LegacySalesShipmentLine(
        int LineNumber,
        int OrderNumber,
        string ItemCode,
        string Description,
        decimal Quantity);

    private sealed record ImportedSalesShipmentLine(
        int LineNumber,
        string Description,
        decimal ShippedQuantity);
}
