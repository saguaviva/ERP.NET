using Erp.Application.LegacySync;
using Erp.Infrastructure.MySql.Database;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Talleres;

public sealed class MySqlTallerLegacySyncHandler : ILegacyModuleSyncHandler
{
    private readonly MySqlConnectionFactory _saasConnectionFactory;
    private readonly LegacyMySqlConnectionFactory _legacyConnectionFactory;

    public MySqlTallerLegacySyncHandler(
        MySqlConnectionFactory saasConnectionFactory,
        LegacyMySqlConnectionFactory legacyConnectionFactory)
    {
        _saasConnectionFactory = saasConnectionFactory;
        _legacyConnectionFactory = legacyConnectionFactory;
    }

    public string ModuleKey => LegacySyncModuleKeys.CrmWorkshops;
    public string DisplayName => "CRM / Talleres";

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
        var sharedColumns = await GetSharedColumnsAsync(legacyConnection, saasConnection, "tallers", cancellationToken);
        if (!sharedColumns.Contains("CODI", StringComparer.OrdinalIgnoreCase) ||
            !sharedColumns.Contains("CENTRO", StringComparer.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("No se han encontrado las columnas mínimas necesarias para sincronizar la tabla tallers.");
        }

        await using var transaction = await saasConnection.BeginTransactionAsync(cancellationToken);
        var errors = new List<LegacySyncErrorRecord>();
        var mappings = new List<LegacySyncMappingRecord>();

        try
        {
            await DeleteExistingMappingsAsync(saasConnection, transaction, context, cancellationToken);
            await DeleteTargetRowsAsync(saasConnection, transaction, context.CompanyLegacyCenterCode, cancellationToken);

            var import = await CopyRowsAsync(
                legacyConnection,
                saasConnection,
                transaction,
                context.CompanyLegacyCenterCode,
                sharedColumns,
                errors,
                row =>
                {
                    mappings.Add(new LegacySyncMappingRecord
                    {
                        LegacySourceSystem = "legacy",
                        LegacyCenterCode = context.CompanyLegacyCenterCode,
                        LegacyDocumentType = "WORKSHOP",
                        LegacyDocumentNumber = row.EntityNumber,
                        TargetEntityName = "workshop",
                        TargetEntityId = row.EntityNumber
                    });
                },
                cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return new LegacySyncModuleRunResult
            {
                RecordsInserted = import.ImportedRows,
                RecordsUpdated = 0,
                RecordsSkipped = import.SkippedRows,
                NewCheckpointValue = DateTime.UtcNow.ToString("O"),
                Summary = $"Talleres replicados={import.ImportedRows}; errores={errors.Count}",
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

    private static async Task<TableImportResult> CopyRowsAsync(
        MySqlConnection legacyConnection,
        MySqlConnection saasConnection,
        MySqlTransaction transaction,
        string centerCode,
        IReadOnlyList<string> columns,
        List<LegacySyncErrorRecord> errors,
        Action<ImportedRowContext> onRowImported,
        CancellationToken cancellationToken)
    {
        var result = new TableImportResult();
        var columnList = string.Join(", ", columns.Select(column => $"`{column}`"));
        var insertColumnList = $"{columnList}, `origin`, `is_deleted`, `synced_utc`";
        var ordinals = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        var syncedUtc = DateTime.UtcNow;

        await using var readCommand = legacyConnection.CreateCommand();
        readCommand.CommandText =
            $"""
            SELECT {columnList}
            FROM `tallers`
            WHERE `CENTRO` = @centerCode
            ORDER BY `CODI`;
            """;
        readCommand.Parameters.AddWithValue("@centerCode", centerCode);

        await using var insertCommand = saasConnection.CreateCommand();
        insertCommand.Transaction = transaction;
        insertCommand.CommandText =
            $"""
            INSERT INTO `tallers` ({insertColumnList})
            VALUES ({string.Join(", ", columns.Select(column => $"@{column}"))}, 'legacy', 0, @syncedUtc);
            """;

        foreach (var column in columns)
        {
            insertCommand.Parameters.Add(new MySqlParameter($"@{column}", DBNull.Value));
        }
        insertCommand.Parameters.AddWithValue("@syncedUtc", syncedUtc);

        await using var reader = await readCommand.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            try
            {
                foreach (var column in columns)
                {
                    if (!ordinals.TryGetValue(column, out var ordinal))
                    {
                        ordinal = reader.GetOrdinal(column);
                        ordinals[column] = ordinal;
                    }

                    insertCommand.Parameters[$"@{column}"].Value = reader.IsDBNull(ordinal)
                        ? DBNull.Value
                        : reader.GetValue(ordinal);
                }

                await insertCommand.ExecuteNonQueryAsync(cancellationToken);
                result.ImportedRows++;
                onRowImported(new ImportedRowContext(GetEntityNumber(reader, ordinals)));
            }
            catch (MySqlException exception) when (exception.Number == 1062)
            {
                result.SkippedRows++;
            }
            catch (Exception exception)
            {
                result.SkippedRows++;
                errors.Add(new LegacySyncErrorRecord
                {
                    Stage = "tallers",
                    LegacyEntityKey = $"{centerCode}/{GetEntityNumber(reader, ordinals)}",
                    ErrorMessage = exception.Message,
                    Payload = string.Empty
                });
            }
        }

        return result;
    }

    private static async Task DeleteTargetRowsAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        string centerCode,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            DELETE FROM `tallers`
            WHERE `CENTRO` = @centerCode
              AND `origin` = 'legacy';
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task DeleteExistingMappingsAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        LegacySyncModuleContext context,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
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

    private static async Task<IReadOnlyList<string>> GetSharedColumnsAsync(
        MySqlConnection legacyConnection,
        MySqlConnection saasConnection,
        string tableName,
        CancellationToken cancellationToken)
    {
        var legacyColumns = await LoadOrderedColumnsAsync(legacyConnection, tableName, cancellationToken);
        var saasColumns = await LoadOrderedColumnsAsync(saasConnection, tableName, cancellationToken);
        var saasSet = saasColumns.ToHashSet(StringComparer.OrdinalIgnoreCase);
        return legacyColumns.Where(column => saasSet.Contains(column)).ToArray();
    }

    private static async Task<IReadOnlyList<string>> LoadOrderedColumnsAsync(
        MySqlConnection connection,
        string tableName,
        CancellationToken cancellationToken)
    {
        var columns = new List<string>();
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT COLUMN_NAME
            FROM INFORMATION_SCHEMA.COLUMNS
            WHERE TABLE_SCHEMA = DATABASE()
              AND TABLE_NAME = @tableName
            ORDER BY ORDINAL_POSITION;
            """;
        command.Parameters.AddWithValue("@tableName", tableName);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            columns.Add(reader.GetString(reader.GetOrdinal("COLUMN_NAME")));
        }

        return columns;
    }

    private static string GetEntityNumber(MySqlDataReader reader, IReadOnlyDictionary<string, int> ordinals)
    {
        if (ordinals.TryGetValue("CODI", out var ordinal) && !reader.IsDBNull(ordinal))
        {
            return Convert.ToString(reader.GetValue(ordinal)) ?? "0";
        }

        return "0";
    }

    private readonly record struct ImportedRowContext(string EntityNumber);

    private sealed class TableImportResult
    {
        public int ImportedRows { get; set; }
        public int SkippedRows { get; set; }
    }
}
