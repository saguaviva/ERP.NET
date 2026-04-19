using System.Globalization;
using Erp.Application.LegacySync;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Support;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Stock;

public sealed class MySqlStockLegacySyncHandler : ILegacyModuleSyncHandler
{
    private const int SyncCommandTimeoutSeconds = 300;
    private readonly MySqlConnectionFactory _saasConnectionFactory;
    private readonly LegacyMySqlConnectionFactory _legacyConnectionFactory;

    public MySqlStockLegacySyncHandler(
        MySqlConnectionFactory saasConnectionFactory,
        LegacyMySqlConnectionFactory legacyConnectionFactory)
    {
        _saasConnectionFactory = saasConnectionFactory;
        _legacyConnectionFactory = legacyConnectionFactory;
    }

    public string ModuleKey => LegacySyncModuleKeys.StockItems;
    public string DisplayName => "Almacén / Stock y artículos";

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
        await using var transaction = await saasConnection.BeginTransactionAsync(cancellationToken);

        var errors = new List<LegacySyncErrorRecord>();
        var mappings = new List<LegacySyncMappingRecord>();
        var insertedItemKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var insertedItems = 0;
        var insertedBalances = 0;
        var skipped = 0;
        var nowUtc = DateTime.UtcNow;

        try
        {
            await DeleteExistingMappingsAsync(saasConnection, transaction, context, cancellationToken);
            await DeleteExistingBalancesAsync(saasConnection, transaction, context, cancellationToken);
            await DeleteExistingItemsAsync(saasConnection, transaction, context, cancellationToken);

            insertedItems += await ImportMasterTableAsync(
                legacyConnection,
                saasConnection,
                transaction,
                context,
                tableName: "teixits",
                itemType: "Teixit",
                legacyDocumentType: "TEIXITS",
                createItemKey: code => $"TEIXIT:{code}",
                createDescription: (code, description) => FallbackDescription(description, $"Tejido {code}"),
                unitOfMeasure: string.Empty,
                insertedItemKeys,
                mappings,
                errors,
                nowUtc,
                cancellationToken);

            insertedItems += await ImportMasterTableAsync(
                legacyConnection,
                saasConnection,
                transaction,
                context,
                tableName: "fil",
                itemType: "Fil",
                legacyDocumentType: "FIL",
                createItemKey: code => $"FIL:{code}",
                createDescription: (code, description) => FallbackDescription(description, $"Hilo {code}"),
                unitOfMeasure: string.Empty,
                insertedItemKeys,
                mappings,
                errors,
                nowUtc,
                cancellationToken);

            insertedItems += await ImportMasterTableAsync(
                legacyConnection,
                saasConnection,
                transaction,
                context,
                tableName: "forni",
                itemType: "Forni",
                legacyDocumentType: "FORNI",
                createItemKey: code => $"FORNI:{code}",
                createDescription: (code, description) => FallbackDescription(description, $"Fornitura {code}"),
                unitOfMeasure: "u",
                insertedItemKeys,
                mappings,
                errors,
                nowUtc,
                cancellationToken);

            insertedItems += await ImportMasterTableAsync(
                legacyConnection,
                saasConnection,
                transaction,
                context,
                tableName: "mostres",
                itemType: "Mostra",
                legacyDocumentType: "MOSTRES",
                createItemKey: code => $"MOSTRA:{code}",
                createDescription: (code, description) => FallbackDescription(description, $"Muestra {code}"),
                unitOfMeasure: "u",
                insertedItemKeys,
                mappings,
                errors,
                nowUtc,
                cancellationToken);

            var teixitsResult = await ImportTeixitsBalancesAsync(
                legacyConnection,
                saasConnection,
                transaction,
                context,
                insertedItemKeys,
                mappings,
                errors,
                nowUtc,
                cancellationToken);
            insertedItems += teixitsResult.InsertedItems;
            insertedBalances += teixitsResult.InsertedBalances;
            skipped += teixitsResult.SkippedRows;

            var filcolResult = await ImportFilcolBalancesAsync(
                legacyConnection,
                saasConnection,
                transaction,
                context,
                insertedItemKeys,
                mappings,
                errors,
                nowUtc,
                cancellationToken);
            insertedItems += filcolResult.InsertedItems;
            insertedBalances += filcolResult.InsertedBalances;
            skipped += filcolResult.SkippedRows;

            await transaction.CommitAsync(cancellationToken);

            return new LegacySyncModuleRunResult
            {
                RecordsInserted = insertedBalances,
                RecordsUpdated = 0,
                RecordsSkipped = skipped,
                NewCheckpointValue = $"FULL@{DateTime.UtcNow:O}",
                Summary = $"Articulos={insertedItems}; Saldos={insertedBalances}; Omitidos={skipped}; Errores={errors.Count}",
                Mappings = mappings,
                Errors = errors
            };
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    private static async Task<int> ImportMasterTableAsync(
        MySqlConnection legacyConnection,
        MySqlConnection saasConnection,
        MySqlTransaction transaction,
        LegacySyncModuleContext context,
        string tableName,
        string itemType,
        string legacyDocumentType,
        Func<string, string> createItemKey,
        Func<string, string, string> createDescription,
        string unitOfMeasure,
        HashSet<string> insertedItemKeys,
        List<LegacySyncMappingRecord> mappings,
        List<LegacySyncErrorRecord> errors,
        DateTime nowUtc,
        CancellationToken cancellationToken)
    {
        var inserted = 0;
        await using var command = CreateTimedCommand(legacyConnection);
        command.CommandText =
            $"""
            SELECT CODI,
                   COALESCE(DESCRI, '') AS description
            FROM {tableName}
            WHERE CENTRO = @centerCode
            ORDER BY CODI;
            """;
        command.Parameters.AddWithValue("@centerCode", context.CompanyLegacyCenterCode);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var code = reader.GetStringOrEmpty("CODI").Trim();
            if (string.IsNullOrWhiteSpace(code))
            {
                continue;
            }

            try
            {
                var itemKey = createItemKey(code);
                if (!insertedItemKeys.Add(itemKey))
                {
                    continue;
                }

                var description = createDescription(code, reader.GetStringOrEmpty("description"));
                await InsertStockItemAsync(
                    saasConnection,
                    transaction,
                    context,
                    itemKey,
                    itemType,
                    code,
                    description,
                    unitOfMeasure,
                    legacyDocumentType,
                    code,
                    nowUtc,
                    cancellationToken);

                inserted++;
                mappings.Add(new LegacySyncMappingRecord
                {
                    LegacyCenterCode = context.CompanyLegacyCenterCode,
                    LegacyDocumentType = legacyDocumentType,
                    LegacyDocumentNumber = code,
                    TargetEntityName = "StockItem",
                    TargetEntityId = itemKey
                });
            }
            catch (Exception exception)
            {
                errors.Add(new LegacySyncErrorRecord
                {
                    Stage = $"Import{tableName}",
                    LegacyEntityKey = $"{context.CompanyLegacyCenterCode}/{legacyDocumentType}/{code}",
                    ErrorMessage = exception.Message,
                    Payload = string.Empty
                });
            }
        }

        return inserted;
    }

    private static async Task<BalanceImportResult> ImportTeixitsBalancesAsync(
        MySqlConnection legacyConnection,
        MySqlConnection saasConnection,
        MySqlTransaction transaction,
        LegacySyncModuleContext context,
        HashSet<string> insertedItemKeys,
        List<LegacySyncMappingRecord> mappings,
        List<LegacySyncErrorRecord> errors,
        DateTime nowUtc,
        CancellationToken cancellationToken)
    {
        var insertedBalances = 0;
        var insertedItems = 0;
        var skipped = 0;
        await using var command = CreateTimedCommand(legacyConnection);
        command.CommandText =
            """
            SELECT CODI,
                   COALESCE(DESCRI, '') AS description,
                   COALESCE(STDISPM, 0) AS available_meters,
                   COALESCE(STCRUM, 0) AS raw_meters,
                   COALESCE(STDISPK, 0) AS available_kilos,
                   COALESCE(STCRUK, 0) AS raw_kilos
            FROM teixits
            WHERE CENTRO = @centerCode
            ORDER BY CODI;
            """;
        command.Parameters.AddWithValue("@centerCode", context.CompanyLegacyCenterCode);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var code = reader.GetStringOrEmpty("CODI").Trim();
            if (string.IsNullOrWhiteSpace(code))
            {
                continue;
            }

            try
            {
                var description = FallbackDescription(reader.GetStringOrEmpty("description"), $"Tejido {code}");
                var balance = ChooseTeixitsBalance(
                    reader.GetDecimalOrDefault("available_meters"),
                    reader.GetDecimalOrDefault("raw_meters"),
                    reader.GetDecimalOrDefault("available_kilos"),
                    reader.GetDecimalOrDefault("raw_kilos"));

                if (!balance.HasValue)
                {
                    continue;
                }

                var itemKey = $"TEIXIT:{code}";
                if (insertedItemKeys.Add(itemKey))
                {
                    await InsertStockItemAsync(
                        saasConnection,
                        transaction,
                        context,
                        itemKey,
                        "Teixit",
                        code,
                        description,
                        balance.Value.UnitOfMeasure,
                        "TEIXITS",
                        code,
                        nowUtc,
                        cancellationToken);
                    insertedItems++;
                }

                await InsertLegacyBalanceAsync(
                    saasConnection,
                    transaction,
                    context,
                    warehouse: "Tejidos legacy",
                    itemKey,
                    code,
                    description,
                    balance.Value.UnitOfMeasure,
                    balance.Value.Quantity,
                    "TEIXITS",
                    code,
                    nowUtc,
                    cancellationToken);

                insertedBalances++;
                mappings.Add(new LegacySyncMappingRecord
                {
                    LegacyCenterCode = context.CompanyLegacyCenterCode,
                    LegacyDocumentType = "TEIXITS",
                    LegacyDocumentNumber = code,
                    TargetEntityName = "LegacyStockBalance",
                    TargetEntityId = $"Tejidos legacy:{itemKey}"
                });
            }
            catch (Exception exception)
            {
                skipped++;
                errors.Add(new LegacySyncErrorRecord
                {
                    Stage = "ImportTeixitsBalance",
                    LegacyEntityKey = $"{context.CompanyLegacyCenterCode}/TEIXITS/{code}",
                    ErrorMessage = exception.Message,
                    Payload = string.Empty
                });
            }
        }

        return new BalanceImportResult(insertedItems, insertedBalances, skipped);
    }

    private static async Task<BalanceImportResult> ImportFilcolBalancesAsync(
        MySqlConnection legacyConnection,
        MySqlConnection saasConnection,
        MySqlTransaction transaction,
        LegacySyncModuleContext context,
        HashSet<string> insertedItemKeys,
        List<LegacySyncMappingRecord> mappings,
        List<LegacySyncErrorRecord> errors,
        DateTime nowUtc,
        CancellationToken cancellationToken)
    {
        var insertedBalances = 0;
        var insertedItems = 0;
        var skipped = 0;
        var groupedBalances = new Dictionary<string, AggregatedFilcolBalance>(StringComparer.OrdinalIgnoreCase);
        await using var command = CreateTimedCommand(legacyConnection);
        command.CommandText =
            """
            SELECT fc.TIPUS,
                   COALESCE(fc.FIL, '') AS fil_code,
                   COALESCE(fc.COLOR, '') AS color_code,
                   COALESCE(fc.ACTUAL, 0) AS current_stock,
                   COALESCE(fc.MEDIDA, '') AS unit_of_measure,
                   COALESCE(NULLIF(f.DESCRI, ''), NULLIF(t.DESCRI, ''), '') AS base_description
            FROM filcol fc
            LEFT JOIN fil f
              ON f.CENTRO = fc.CENTRO
             AND f.CODI = fc.FIL
            LEFT JOIN teixits t
              ON t.CENTRO = fc.CENTRO
             AND t.CODI = fc.FIL
            WHERE fc.CENTRO = @centerCode
              AND COALESCE(fc.ACTUAL, 0) <> 0
            ORDER BY fc.TIPUS, fc.FIL, fc.COLOR;
            """;
        command.Parameters.AddWithValue("@centerCode", context.CompanyLegacyCenterCode);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var legacyType = NormalizeLegacyType(reader.GetStringOrEmpty("TIPUS"));
            var filCode = reader.GetStringOrEmpty("fil_code").Trim();
            var colorCode = reader.GetStringOrEmpty("color_code").Trim();
            var currentStock = decimal.Round(reader.GetDecimalOrDefault("current_stock"), 3, MidpointRounding.AwayFromZero);

            if (string.IsNullOrWhiteSpace(filCode) || currentStock == 0)
            {
                continue;
            }

            var itemKey = $"FILCOL:{legacyType}:{filCode}:{colorCode}";
            var itemCode = string.IsNullOrWhiteSpace(colorCode) ? filCode : $"{filCode}-{colorCode}";
            var description = BuildFilcolDescription(reader.GetStringOrEmpty("base_description"), filCode, colorCode);
            var unitOfMeasure = NormalizeUnit(reader.GetStringOrEmpty("unit_of_measure"));
            var itemType = MapFilcolItemType(legacyType);
            var warehouse = MapFilcolWarehouse(legacyType);
            var legacyDocumentType = $"FILCOL-{legacyType}";
            var legacyDocumentNumber = string.IsNullOrWhiteSpace(colorCode) ? filCode : $"{filCode}/{colorCode}";

            if (groupedBalances.TryGetValue(itemKey, out var existing))
            {
                groupedBalances[itemKey] = existing with
                {
                    CurrentStock = decimal.Round(existing.CurrentStock + currentStock, 3, MidpointRounding.AwayFromZero)
                };
            }
            else
            {
                groupedBalances[itemKey] = new AggregatedFilcolBalance(
                    itemKey,
                    itemCode,
                    description,
                    unitOfMeasure,
                    itemType,
                    warehouse,
                    legacyDocumentType,
                    legacyDocumentNumber,
                    currentStock);
            }
        }

        foreach (var balance in groupedBalances.Values)
        {
            try
            {
                if (insertedItemKeys.Add(balance.ItemKey))
                {
                    await InsertStockItemAsync(
                        saasConnection,
                        transaction,
                        context,
                        balance.ItemKey,
                        balance.ItemType,
                        balance.ItemCode,
                        balance.Description,
                        balance.UnitOfMeasure,
                        balance.LegacyDocumentType,
                        balance.LegacyDocumentNumber,
                        nowUtc,
                        cancellationToken);
                    insertedItems++;
                }

                await InsertLegacyBalanceAsync(
                    saasConnection,
                    transaction,
                    context,
                    balance.Warehouse,
                    balance.ItemKey,
                    balance.ItemCode,
                    balance.Description,
                    balance.UnitOfMeasure,
                    balance.CurrentStock,
                    balance.LegacyDocumentType,
                    balance.LegacyDocumentNumber,
                    nowUtc,
                    cancellationToken);

                insertedBalances++;
                mappings.Add(new LegacySyncMappingRecord
                {
                    LegacyCenterCode = context.CompanyLegacyCenterCode,
                    LegacyDocumentType = balance.LegacyDocumentType,
                    LegacyDocumentNumber = balance.LegacyDocumentNumber,
                    TargetEntityName = "LegacyStockBalance",
                    TargetEntityId = $"{balance.Warehouse}:{balance.ItemKey}"
                });
            }
            catch (Exception exception)
            {
                skipped++;
                errors.Add(new LegacySyncErrorRecord
                {
                    Stage = "ImportFilcolBalance",
                    LegacyEntityKey = $"{context.CompanyLegacyCenterCode}/{balance.LegacyDocumentType}/{balance.LegacyDocumentNumber}",
                    ErrorMessage = exception.Message,
                    Payload = string.Empty
                });
            }
        }

        return new BalanceImportResult(insertedItems, insertedBalances, skipped);
    }

    private static async Task InsertStockItemAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        LegacySyncModuleContext context,
        string itemKey,
        string itemType,
        string itemCode,
        string description,
        string unitOfMeasure,
        string legacyDocumentType,
        string legacyDocumentNumber,
        DateTime syncedUtc,
        CancellationToken cancellationToken)
    {
        await using var command = CreateTimedCommand(connection, transaction);
        command.CommandText =
            """
            INSERT INTO stock_items (
                tenant_id,
                company_id,
                item_key,
                item_type,
                item_code,
                description,
                unit_of_measure,
                origin,
                is_deleted,
                legacy_source_system,
                legacy_center_code,
                legacy_document_type,
                legacy_document_number,
                synced_utc,
                created_utc,
                updated_utc)
            VALUES (
                @tenantId,
                @companyId,
                @itemKey,
                @itemType,
                @itemCode,
                @description,
                @unitOfMeasure,
                'legacy',
                0,
                'legacy',
                @legacyCenterCode,
                @legacyDocumentType,
                @legacyDocumentNumber,
                @syncedUtc,
                @createdUtc,
                @updatedUtc);
            """;
        command.Parameters.AddWithValue("@tenantId", context.TenantId.ToString());
        command.Parameters.AddWithValue("@companyId", context.CompanyId.ToString());
        command.Parameters.AddWithValue("@itemKey", itemKey);
        command.Parameters.AddWithValue("@itemType", itemType);
        command.Parameters.AddWithValue("@itemCode", itemCode);
        command.Parameters.AddWithValue("@description", description);
        command.Parameters.AddWithValue("@unitOfMeasure", DbValue(unitOfMeasure));
        command.Parameters.AddWithValue("@legacyCenterCode", context.CompanyLegacyCenterCode);
        command.Parameters.AddWithValue("@legacyDocumentType", legacyDocumentType);
        command.Parameters.AddWithValue("@legacyDocumentNumber", legacyDocumentNumber);
        command.Parameters.AddWithValue("@syncedUtc", syncedUtc);
        command.Parameters.AddWithValue("@createdUtc", syncedUtc);
        command.Parameters.AddWithValue("@updatedUtc", syncedUtc);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task InsertLegacyBalanceAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        LegacySyncModuleContext context,
        string warehouse,
        string itemKey,
        string itemCode,
        string description,
        string unitOfMeasure,
        decimal currentStock,
        string legacyDocumentType,
        string legacyDocumentNumber,
        DateTime syncedUtc,
        CancellationToken cancellationToken)
    {
        await using var command = CreateTimedCommand(connection, transaction);
        command.CommandText =
            """
            INSERT INTO legacy_stock_balances (
                tenant_id,
                company_id,
                warehouse,
                item_key,
                item_code,
                item_description,
                unit_of_measure,
                current_stock,
                movement_count,
                last_movement_date,
                legacy_source_system,
                legacy_center_code,
                legacy_document_type,
                legacy_document_number,
                synced_utc)
            VALUES (
                @tenantId,
                @companyId,
                @warehouse,
                @itemKey,
                @itemCode,
                @description,
                @unitOfMeasure,
                @currentStock,
                0,
                NULL,
                'legacy',
                @legacyCenterCode,
                @legacyDocumentType,
                @legacyDocumentNumber,
                @syncedUtc);
            """;
        command.Parameters.AddWithValue("@tenantId", context.TenantId.ToString());
        command.Parameters.AddWithValue("@companyId", context.CompanyId.ToString());
        command.Parameters.AddWithValue("@warehouse", warehouse);
        command.Parameters.AddWithValue("@itemKey", itemKey);
        command.Parameters.AddWithValue("@itemCode", itemCode);
        command.Parameters.AddWithValue("@description", description);
        command.Parameters.AddWithValue("@unitOfMeasure", DbValue(unitOfMeasure));
        command.Parameters.AddWithValue("@currentStock", currentStock);
        command.Parameters.AddWithValue("@legacyCenterCode", context.CompanyLegacyCenterCode);
        command.Parameters.AddWithValue("@legacyDocumentType", legacyDocumentType);
        command.Parameters.AddWithValue("@legacyDocumentNumber", legacyDocumentNumber);
        command.Parameters.AddWithValue("@syncedUtc", syncedUtc);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task DeleteExistingMappingsAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        LegacySyncModuleContext context,
        CancellationToken cancellationToken)
    {
        await using var command = CreateTimedCommand(connection, transaction);
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

    private static async Task DeleteExistingBalancesAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        LegacySyncModuleContext context,
        CancellationToken cancellationToken)
    {
        await using var command = CreateTimedCommand(connection, transaction);
        command.CommandText =
            """
            DELETE FROM legacy_stock_balances
            WHERE tenant_id = @tenantId
              AND company_id = @companyId;
            """;
        command.Parameters.AddWithValue("@tenantId", context.TenantId.ToString());
        command.Parameters.AddWithValue("@companyId", context.CompanyId.ToString());
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task DeleteExistingItemsAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        LegacySyncModuleContext context,
        CancellationToken cancellationToken)
    {
        await using var command = CreateTimedCommand(connection, transaction);
        command.CommandText =
            """
            DELETE FROM stock_items
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND origin = 'legacy';
            """;
        command.Parameters.AddWithValue("@tenantId", context.TenantId.ToString());
        command.Parameters.AddWithValue("@companyId", context.CompanyId.ToString());
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static (decimal Quantity, string UnitOfMeasure)? ChooseTeixitsBalance(
        decimal availableMeters,
        decimal rawMeters,
        decimal availableKilos,
        decimal rawKilos)
    {
        if (availableMeters != 0)
        {
            return (decimal.Round(availableMeters, 3, MidpointRounding.AwayFromZero), "m");
        }

        if (rawMeters != 0)
        {
            return (decimal.Round(rawMeters, 3, MidpointRounding.AwayFromZero), "m");
        }

        if (availableKilos != 0)
        {
            return (decimal.Round(availableKilos, 3, MidpointRounding.AwayFromZero), "kg");
        }

        if (rawKilos != 0)
        {
            return (decimal.Round(rawKilos, 3, MidpointRounding.AwayFromZero), "kg");
        }

        return null;
    }

    private static string NormalizeLegacyType(string value)
    {
        value = value?.Trim().ToUpperInvariant() ?? string.Empty;
        return string.IsNullOrWhiteSpace(value) ? "GEN" : value;
    }

    private static string MapFilcolItemType(string legacyType) => legacyType switch
    {
        "F" => "FilColor",
        "T" => "TeixitColor",
        "U" => "StockAuxiliar",
        _ => "StockLegacy"
    };

    private static string MapFilcolWarehouse(string legacyType) => legacyType switch
    {
        "F" => "Hilo color legacy",
        "T" => "Tejido color legacy",
        "U" => "Stock auxiliar legacy",
        _ => "Stock legacy"
    };

    private static string NormalizeUnit(string value)
    {
        value = value?.Trim() ?? string.Empty;
        return string.IsNullOrWhiteSpace(value) ? "u" : value;
    }

    private static string BuildFilcolDescription(string baseDescription, string filCode, string colorCode)
    {
        var description = FallbackDescription(baseDescription, $"Artículo {filCode}");
        return string.IsNullOrWhiteSpace(colorCode) ? description : $"{description} / {colorCode}";
    }

    private static string FallbackDescription(string value, string fallback)
    {
        value = value?.Trim() ?? string.Empty;
        return string.IsNullOrWhiteSpace(value) ? fallback : value;
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

    private readonly record struct BalanceImportResult(int InsertedItems, int InsertedBalances, int SkippedRows);
    private sealed record AggregatedFilcolBalance(
        string ItemKey,
        string ItemCode,
        string Description,
        string UnitOfMeasure,
        string ItemType,
        string Warehouse,
        string LegacyDocumentType,
        string LegacyDocumentNumber,
        decimal CurrentStock);
}
