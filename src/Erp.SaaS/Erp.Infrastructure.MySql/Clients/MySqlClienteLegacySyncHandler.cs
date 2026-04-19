using Erp.Application.LegacySync;
using Erp.Infrastructure.MySql.Database;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Clients;

public sealed class MySqlClienteLegacySyncHandler : ILegacyModuleSyncHandler
{
    private readonly MySqlConnectionFactory _saasConnectionFactory;
    private readonly LegacyMySqlConnectionFactory _legacyConnectionFactory;

    public MySqlClienteLegacySyncHandler(
        MySqlConnectionFactory saasConnectionFactory,
        LegacyMySqlConnectionFactory legacyConnectionFactory)
    {
        _saasConnectionFactory = saasConnectionFactory;
        _legacyConnectionFactory = legacyConnectionFactory;
    }

    public string ModuleKey => LegacySyncModuleKeys.CrmClients;
    public string DisplayName => "CRM / Clientes";

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
        var sharedClientColumns = await GetSharedColumnsAsync(legacyConnection, saasConnection, "clients", cancellationToken);
        if (!sharedClientColumns.Contains("CODI", StringComparer.OrdinalIgnoreCase) ||
            !sharedClientColumns.Contains("CENTRO", StringComparer.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("No se han encontrado las columnas minimas necesarias para sincronizar la tabla clients.");
        }

        var sharedAddressColumns = await GetSharedColumnsAsync(legacyConnection, saasConnection, "adres", cancellationToken);

        await using var transaction = await saasConnection.BeginTransactionAsync(cancellationToken);

        var errors = new List<LegacySyncErrorRecord>();
        var mappings = new List<LegacySyncMappingRecord>();

        try
        {
            await DeleteExistingMappingsAsync(saasConnection, transaction, context, cancellationToken);
            await DeleteTargetRowsAsync(saasConnection, transaction, "adres", context.CompanyLegacyCenterCode, cancellationToken);
            await DeleteTargetRowsAsync(saasConnection, transaction, "clients", context.CompanyLegacyCenterCode, cancellationToken);

            var clientImport = await CopyRowsAsync(
                legacyConnection,
                saasConnection,
                transaction,
                tableName: "clients",
                centerCode: context.CompanyLegacyCenterCode,
                columns: sharedClientColumns,
                orderColumns: ["CODI"],
                stage: "clients",
                onRowImported: row =>
                {
                    mappings.Add(new LegacySyncMappingRecord
                    {
                        LegacySourceSystem = "legacy",
                        LegacyCenterCode = context.CompanyLegacyCenterCode,
                        LegacyDocumentType = "CLIENT",
                        LegacyDocumentNumber = row.EntityNumber,
                        TargetEntityName = "client",
                        TargetEntityId = row.EntityNumber
                    });
                },
                errors,
                cancellationToken);

            var addressImport = sharedAddressColumns.Count == 0
                ? new TableImportResult()
                : await CopyRowsAsync(
                    legacyConnection,
                    saasConnection,
                    transaction,
                    tableName: "adres",
                    centerCode: context.CompanyLegacyCenterCode,
                    columns: sharedAddressColumns,
                    orderColumns: ["CODI", "DOM"],
                    stage: "adres",
                    onRowImported: null,
                    errors,
                    cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return new LegacySyncModuleRunResult
            {
                RecordsInserted = clientImport.ImportedRows,
                RecordsUpdated = addressImport.ImportedRows,
                RecordsSkipped = clientImport.SkippedRows + addressImport.SkippedRows,
                NewCheckpointValue = DateTime.UtcNow.ToString("O"),
                Summary = $"Clientes replicados={clientImport.ImportedRows}; direcciones replicadas={addressImport.ImportedRows}; errores={errors.Count}",
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
        string tableName,
        string centerCode,
        IReadOnlyList<string> columns,
        IReadOnlyList<string> orderColumns,
        string stage,
        Action<ImportedRowContext>? onRowImported,
        List<LegacySyncErrorRecord> errors,
        CancellationToken cancellationToken)
    {
        var result = new TableImportResult();
        var orderByClause = BuildOrderByClause(columns, orderColumns);
        var columnList = string.Join(", ", columns.Select(column => $"`{column}`"));

        await using var readCommand = legacyConnection.CreateCommand();
        readCommand.CommandText =
            $"""
            SELECT {columnList}
            FROM `{tableName}`
            WHERE `CENTRO` = @centerCode
            {orderByClause};
            """;
        readCommand.Parameters.AddWithValue("@centerCode", centerCode);

        await using var insertCommand = saasConnection.CreateCommand();
        insertCommand.Transaction = transaction;
        insertCommand.CommandText =
            $"""
            INSERT INTO `{tableName}` ({columnList})
            VALUES ({string.Join(", ", columns.Select(column => $"@{column}"))});
            """;

        var ordinals = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        foreach (var column in columns)
        {
            insertCommand.Parameters.Add(new MySqlParameter($"@{column}", DBNull.Value));
        }

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

                onRowImported?.Invoke(new ImportedRowContext(
                    EntityNumber: GetEntityNumber(reader, ordinals),
                    EntityLine: GetEntityLine(reader, ordinals)));
            }
            catch (Exception exception)
            {
                result.SkippedRows++;
                errors.Add(new LegacySyncErrorRecord
                {
                    Stage = stage,
                    LegacyEntityKey = BuildLegacyEntityKey(centerCode, reader, ordinals),
                    ErrorMessage = exception.Message,
                    Payload = string.Empty
                });
            }
        }

        return result;
    }

    private static async Task DeleteTargetRowsAsync(
        MySqlConnection saasConnection,
        MySqlTransaction transaction,
        string tableName,
        string centerCode,
        CancellationToken cancellationToken)
    {
        await using var command = saasConnection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = $"DELETE FROM `{tableName}` WHERE `CENTRO` = @centerCode;";
        command.Parameters.AddWithValue("@centerCode", centerCode);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task DeleteExistingMappingsAsync(
        MySqlConnection saasConnection,
        MySqlTransaction transaction,
        LegacySyncModuleContext context,
        CancellationToken cancellationToken)
    {
        await using var command = saasConnection.CreateCommand();
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

        return legacyColumns
            .Where(column => saasSet.Contains(column))
            .ToArray();
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

    private static string BuildOrderByClause(IReadOnlyList<string> columns, IReadOnlyList<string> preferredOrderColumns)
    {
        var existingOrderColumns = preferredOrderColumns
            .Where(column => columns.Contains(column, StringComparer.OrdinalIgnoreCase))
            .ToArray();

        return existingOrderColumns.Length == 0
            ? string.Empty
            : $"ORDER BY {string.Join(", ", existingOrderColumns.Select(column => $"`{column}`"))}";
    }

    private static string BuildLegacyEntityKey(
        string centerCode,
        MySqlDataReader reader,
        IReadOnlyDictionary<string, int> ordinals)
    {
        var code = GetEntityNumber(reader, ordinals);
        var line = GetEntityLine(reader, ordinals);
        return line is null
            ? $"{centerCode}/{code}"
            : $"{centerCode}/{code}/{line}";
    }

    private static string GetEntityNumber(MySqlDataReader reader, IReadOnlyDictionary<string, int> ordinals)
    {
        if (ordinals.TryGetValue("CODI", out var codeOrdinal) && !reader.IsDBNull(codeOrdinal))
        {
            return Convert.ToString(reader.GetValue(codeOrdinal)) ?? "0";
        }

        return "0";
    }

    private static int? GetEntityLine(MySqlDataReader reader, IReadOnlyDictionary<string, int> ordinals)
    {
        if (ordinals.TryGetValue("DOM", out var domOrdinal) && !reader.IsDBNull(domOrdinal))
        {
            var raw = Convert.ToString(reader.GetValue(domOrdinal));
            if (int.TryParse(raw, out var line))
            {
                return line;
            }
        }

        return null;
    }

    private readonly record struct ImportedRowContext(string EntityNumber, int? EntityLine);

    private sealed class TableImportResult
    {
        public int ImportedRows { get; set; }
        public int SkippedRows { get; set; }
    }
}
