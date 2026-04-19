using Erp.Application.LegacySync;
using Erp.Infrastructure.MySql.Database;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Fornituras;

public sealed class MySqlFornituraLegacySyncHandler : ILegacyModuleSyncHandler
{
    private readonly MySqlConnectionFactory _saasConnectionFactory;
    private readonly LegacyMySqlConnectionFactory _legacyConnectionFactory;

    public MySqlFornituraLegacySyncHandler(
        MySqlConnectionFactory saasConnectionFactory,
        LegacyMySqlConnectionFactory legacyConnectionFactory)
    {
        _saasConnectionFactory = saasConnectionFactory;
        _legacyConnectionFactory = legacyConnectionFactory;
    }

    public string ModuleKey => LegacySyncModuleKeys.ArticleFornituras;
    public string DisplayName => "Artículos / Fornituras";

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
        var sharedColumns = await GetSharedColumnsAsync(legacyConnection, saasConnection, "forni", cancellationToken);
        if (!sharedColumns.Contains("CODI", StringComparer.OrdinalIgnoreCase) ||
            !sharedColumns.Contains("CENTRO", StringComparer.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("No se han encontrado las columnas mínimas necesarias para sincronizar la tabla forni.");
        }

        await using var transaction = await saasConnection.BeginTransactionAsync(cancellationToken);
        var errors = new List<LegacySyncErrorRecord>();
        var mappings = new List<LegacySyncMappingRecord>();
        var importedCodes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        try
        {
            await DeleteExistingMappingsAsync(saasConnection, transaction, context, cancellationToken);
            await DeleteDetailRowsAsync(saasConnection, transaction, context.CompanyLegacyCenterCode, cancellationToken);
            await DeleteTargetRowsAsync(saasConnection, transaction, context.CompanyLegacyCenterCode, cancellationToken);

            var import = await CopyHeaderRowsAsync(
                legacyConnection,
                saasConnection,
                transaction,
                context.CompanyLegacyCenterCode,
                sharedColumns,
                errors,
                row =>
                {
                    importedCodes.Add(row.EntityNumber);
                    mappings.Add(new LegacySyncMappingRecord
                    {
                        LegacySourceSystem = "legacy",
                        LegacyCenterCode = context.CompanyLegacyCenterCode,
                        LegacyDocumentType = "TRIM",
                        LegacyDocumentNumber = row.EntityNumber,
                        TargetEntityName = "trim",
                        TargetEntityId = row.EntityNumber
                    });
                },
                cancellationToken);

            var detailImport = await CopyDetailRowsAsync(
                legacyConnection,
                saasConnection,
                transaction,
                context.CompanyLegacyCenterCode,
                importedCodes,
                errors,
                cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return new LegacySyncModuleRunResult
            {
                RecordsInserted = import.ImportedRows,
                RecordsUpdated = 0,
                RecordsSkipped = import.SkippedRows + detailImport.SkippedRows,
                NewCheckpointValue = DateTime.UtcNow.ToString("O"),
                Summary = $"Fornituras replicadas={import.ImportedRows}; detalles={detailImport.ImportedRows}; errores={errors.Count}",
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

    private static async Task<TableImportResult> CopyHeaderRowsAsync(
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
            FROM `forni`
            WHERE `CENTRO` = @centerCode
            ORDER BY `CODI`;
            """;
        readCommand.Parameters.AddWithValue("@centerCode", centerCode);

        await using var insertCommand = saasConnection.CreateCommand();
        insertCommand.Transaction = transaction;
        insertCommand.CommandText =
            $"""
            INSERT INTO `forni` ({insertColumnList})
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
                    Stage = "forni",
                    LegacyEntityKey = $"{centerCode}/{GetEntityNumber(reader, ordinals)}",
                    ErrorMessage = exception.Message,
                    Payload = string.Empty
                });
            }
        }

        return result;
    }

    private static async Task<TableImportResult> CopyDetailRowsAsync(
        MySqlConnection legacyConnection,
        MySqlConnection saasConnection,
        MySqlTransaction transaction,
        string centerCode,
        ISet<string> importedCodes,
        List<LegacySyncErrorRecord> errors,
        CancellationToken cancellationToken)
    {
        var result = new TableImportResult();
        var lineNumbers = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        await using var readCommand = legacyConnection.CreateCommand();
        readCommand.CommandText =
            """
            SELECT FIL, COALESCE(prove, 0) AS prove, OBSERV, COLOR, MEDIDA, COALESCE(PREU, 0) AS PREU,
                   COALESCE(ACTUAL, 0) AS ACTUAL, COALESCE(MINIM, 0) AS MINIM, COALESCE(PREUCOST, 0) AS PREUCOST
            FROM filcol
            WHERE TIPUS = 'O'
              AND CENTRO = @centerCode
            ORDER BY FIL, COLOR, MEDIDA, OBSERV, prove;
            """;
        readCommand.Parameters.AddWithValue("@centerCode", centerCode);

        await using var insertCommand = saasConnection.CreateCommand();
        insertCommand.Transaction = transaction;
        insertCommand.CommandText =
            """
            INSERT INTO forni_detail (
                CENTRO,
                FORNI_CODI,
                LINE_NUMBER,
                PROVE,
                OBSERV,
                COLOR,
                MEDIDA,
                PREU,
                ACTUAL,
                MINIM,
                PREUCOST)
            VALUES (
                @centerCode,
                @forniCode,
                @lineNumber,
                @prove,
                @observ,
                @color,
                @medida,
                @preu,
                @actual,
                @minim,
                @preucost);
            """;
        insertCommand.Parameters.Add("@centerCode", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@forniCode", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@lineNumber", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@prove", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@observ", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@color", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@medida", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@preu", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@actual", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@minim", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@preucost", MySqlDbType.Decimal);

        await using var reader = await readCommand.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var forniCode = GetString(reader, "FIL");
            if (string.IsNullOrWhiteSpace(forniCode) || !importedCodes.Contains(forniCode))
            {
                result.SkippedRows++;
                continue;
            }

            try
            {
                var nextLineNumber = lineNumbers.TryGetValue(forniCode, out var currentLineNumber)
                    ? currentLineNumber + 1
                    : 1;
                lineNumbers[forniCode] = nextLineNumber;

                insertCommand.Parameters["@centerCode"].Value = centerCode;
                insertCommand.Parameters["@forniCode"].Value = forniCode;
                insertCommand.Parameters["@lineNumber"].Value = nextLineNumber;
                insertCommand.Parameters["@prove"].Value = GetInt(reader, "prove");
                insertCommand.Parameters["@observ"].Value = DbValue(GetString(reader, "OBSERV"));
                insertCommand.Parameters["@color"].Value = DbValue(GetString(reader, "COLOR"));
                insertCommand.Parameters["@medida"].Value = DbValue(GetString(reader, "MEDIDA"));
                insertCommand.Parameters["@preu"].Value = GetDecimal(reader, "PREU");
                insertCommand.Parameters["@actual"].Value = GetDecimal(reader, "ACTUAL");
                insertCommand.Parameters["@minim"].Value = GetDecimal(reader, "MINIM");
                insertCommand.Parameters["@preucost"].Value = GetDecimal(reader, "PREUCOST");

                await insertCommand.ExecuteNonQueryAsync(cancellationToken);
                result.ImportedRows++;
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
                    Stage = "forni-detail",
                    LegacyEntityKey = $"{centerCode}/{forniCode}",
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
            DELETE FROM `forni`
            WHERE `CENTRO` = @centerCode
              AND `origin` = 'legacy';
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task DeleteDetailRowsAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        string centerCode,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            DELETE detail
            FROM `forni_detail` detail
            INNER JOIN `forni` header
                ON header.CENTRO = detail.CENTRO
               AND header.CODI = detail.FORNI_CODI
            WHERE detail.CENTRO = @centerCode
              AND header.origin = 'legacy';
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
            return Convert.ToString(reader.GetValue(ordinal)) ?? string.Empty;
        }

        return string.Empty;
    }

    private static string GetString(MySqlDataReader reader, string columnName)
    {
        var ordinal = reader.GetOrdinal(columnName);
        return reader.IsDBNull(ordinal)
            ? string.Empty
            : Convert.ToString(reader.GetValue(ordinal))?.Trim() ?? string.Empty;
    }

    private static int GetInt(MySqlDataReader reader, string columnName)
    {
        var ordinal = reader.GetOrdinal(columnName);
        if (reader.IsDBNull(ordinal))
        {
            return 0;
        }

        return Convert.ToInt32(reader.GetValue(ordinal));
    }

    private static decimal GetDecimal(MySqlDataReader reader, string columnName)
    {
        var ordinal = reader.GetOrdinal(columnName);
        if (reader.IsDBNull(ordinal))
        {
            return 0m;
        }

        return Convert.ToDecimal(reader.GetValue(ordinal));
    }

    private static object DbValue(string value) =>
        string.IsNullOrWhiteSpace(value) ? DBNull.Value : value;

    private readonly record struct ImportedRowContext(string EntityNumber);

    private sealed class TableImportResult
    {
        public int ImportedRows { get; set; }
        public int SkippedRows { get; set; }
    }
}
