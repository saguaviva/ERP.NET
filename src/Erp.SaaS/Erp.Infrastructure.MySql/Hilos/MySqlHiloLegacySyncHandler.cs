using Erp.Application.LegacySync;
using Erp.Infrastructure.MySql.Database;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Hilos;

public sealed class MySqlHiloLegacySyncHandler : ILegacyModuleSyncHandler
{
    private readonly MySqlConnectionFactory _saasConnectionFactory;
    private readonly LegacyMySqlConnectionFactory _legacyConnectionFactory;

    public MySqlHiloLegacySyncHandler(
        MySqlConnectionFactory saasConnectionFactory,
        LegacyMySqlConnectionFactory legacyConnectionFactory)
    {
        _saasConnectionFactory = saasConnectionFactory;
        _legacyConnectionFactory = legacyConnectionFactory;
    }

    public string ModuleKey => LegacySyncModuleKeys.ArticleHilos;
    public string DisplayName => "Artículos / Hilos";

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
        var importedCodes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        try
        {
            await DeleteExistingMappingsAsync(saasConnection, transaction, context, cancellationToken);
            await DeleteDetailRowsAsync(saasConnection, transaction, context.CompanyLegacyCenterCode, cancellationToken);
            await DeleteTargetRowsAsync(saasConnection, transaction, context.CompanyLegacyCenterCode, cancellationToken);

            var headerImport = await CopyHeaderRowsAsync(
                legacyConnection,
                saasConnection,
                transaction,
                context.CompanyLegacyCenterCode,
                errors,
                row =>
                {
                    importedCodes.Add(row.EntityNumber);
                    mappings.Add(new LegacySyncMappingRecord
                    {
                        LegacySourceSystem = "legacy",
                        LegacyCenterCode = context.CompanyLegacyCenterCode,
                        LegacyDocumentType = "FIL",
                        LegacyDocumentNumber = row.EntityNumber,
                        TargetEntityName = "hilo",
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
                RecordsInserted = headerImport.ImportedRows,
                RecordsUpdated = 0,
                RecordsSkipped = headerImport.SkippedRows + detailImport.SkippedRows,
                NewCheckpointValue = DateTime.UtcNow.ToString("O"),
                Summary = $"Hilos replicados={headerImport.ImportedRows}; detalle={detailImport.ImportedRows}; errores={errors.Count}",
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
        List<LegacySyncErrorRecord> errors,
        Action<ImportedRowContext> onRowImported,
        CancellationToken cancellationToken)
    {
        var result = new TableImportResult();
        var syncedUtc = DateTime.UtcNow;

        await using var readCommand = legacyConnection.CreateCommand();
        readCommand.CommandText =
            """
            SELECT CODI,
                   CENTRO,
                   DESCRI,
                   COALESCE(PROVE, 0) AS PROVE,
                   COALESCE(COST, 0) AS COST,
                   COALESCE(PREU, 0) AS PREU,
                   IVA,
                   OBSERV
            FROM fil
            WHERE CENTRO = @centerCode
            ORDER BY CODI;
            """;
        readCommand.Parameters.AddWithValue("@centerCode", centerCode);
        readCommand.CommandTimeout = 300;

        await using var insertCommand = saasConnection.CreateCommand();
        insertCommand.Transaction = transaction;
        insertCommand.CommandText =
            """
            INSERT INTO fil (CODI, CENTRO, DESCRI, PROVE, COST, PREU, IVA, OBSERV, origin, is_deleted, synced_utc)
            VALUES (@code, @centerCode, @description, @supplierCode, @costPrice, @unitPrice, @vatCode, @notes, 'legacy', 0, @syncedUtc);
            """;
        insertCommand.Parameters.Add("@code", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@centerCode", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@description", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@supplierCode", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@costPrice", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@unitPrice", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@vatCode", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@notes", MySqlDbType.VarChar);
        insertCommand.Parameters.AddWithValue("@syncedUtc", syncedUtc);

        await using var reader = await readCommand.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var code = GetString(reader, "CODI");
            try
            {
                insertCommand.Parameters["@code"].Value = code;
                insertCommand.Parameters["@centerCode"].Value = centerCode;
                insertCommand.Parameters["@description"].Value = GetString(reader, "DESCRI");
                insertCommand.Parameters["@supplierCode"].Value = GetInt(reader, "PROVE");
                insertCommand.Parameters["@costPrice"].Value = GetDecimal(reader, "COST");
                insertCommand.Parameters["@unitPrice"].Value = GetDecimal(reader, "PREU");
                insertCommand.Parameters["@vatCode"].Value = DbValue(GetString(reader, "IVA"));
                insertCommand.Parameters["@notes"].Value = DbValue(GetString(reader, "OBSERV"));
                await insertCommand.ExecuteNonQueryAsync(cancellationToken);
                result.ImportedRows++;
                onRowImported(new ImportedRowContext(code));
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
                    Stage = "fil",
                    LegacyEntityKey = $"{centerCode}/{code}",
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
            SELECT FIL,
                   COALESCE(PROVE, 0) AS PROVE,
                   COLOR,
                   COALESCE(ACTUAL, 0) AS ACTUAL,
                   COALESCE(MINIM, 0) AS MINIM,
                   COALESCE(PREU, 0) AS PREU,
                   COALESCE(PREUCOST, 0) AS PREUCOST,
                   COALESCE(TINTAR, 0) AS TINTAR,
                   COALESCE(METRES, 0) AS METRES,
                   COALESCE(KG, 0) AS KG,
                   OBSERV
            FROM filcol
            WHERE TIPUS = 'F'
              AND CENTRO = @centerCode
            ORDER BY FIL, PROVE, COLOR, OBSERV;
            """;
        readCommand.Parameters.AddWithValue("@centerCode", centerCode);
        readCommand.CommandTimeout = 300;

        await using var insertCommand = saasConnection.CreateCommand();
        insertCommand.Transaction = transaction;
        insertCommand.CommandText =
            """
            INSERT INTO fil_detail (
                CENTRO,
                FIL_CODI,
                LINE_NUMBER,
                PROVE,
                COLOR,
                ACTUAL,
                MINIM,
                PREU,
                PREUCOST,
                TINTAR,
                METRES,
                KG,
                OBSERV)
            VALUES (
                @centerCode,
                @code,
                @lineNumber,
                @supplierCode,
                @color,
                @currentStock,
                @minimumStock,
                @unitPrice,
                @costPrice,
                @dyeingPrice,
                @meters,
                @kilograms,
                @notes);
            """;
        insertCommand.Parameters.Add("@centerCode", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@code", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@lineNumber", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@supplierCode", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@color", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@currentStock", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@minimumStock", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@unitPrice", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@costPrice", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@dyeingPrice", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@meters", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@kilograms", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@notes", MySqlDbType.VarChar);

        await using var reader = await readCommand.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var code = GetString(reader, "FIL");
            if (string.IsNullOrWhiteSpace(code) || !importedCodes.Contains(code))
            {
                result.SkippedRows++;
                continue;
            }

            try
            {
                var nextLineNumber = lineNumbers.TryGetValue(code, out var currentLineNumber)
                    ? currentLineNumber + 1
                    : 1;
                lineNumbers[code] = nextLineNumber;

                insertCommand.Parameters["@centerCode"].Value = centerCode;
                insertCommand.Parameters["@code"].Value = code;
                insertCommand.Parameters["@lineNumber"].Value = nextLineNumber;
                insertCommand.Parameters["@supplierCode"].Value = GetInt(reader, "PROVE");
                insertCommand.Parameters["@color"].Value = DbValue(GetString(reader, "COLOR"));
                insertCommand.Parameters["@currentStock"].Value = GetDecimal(reader, "ACTUAL");
                insertCommand.Parameters["@minimumStock"].Value = GetDecimal(reader, "MINIM");
                insertCommand.Parameters["@unitPrice"].Value = GetDecimal(reader, "PREU");
                insertCommand.Parameters["@costPrice"].Value = GetDecimal(reader, "PREUCOST");
                insertCommand.Parameters["@dyeingPrice"].Value = GetDecimal(reader, "TINTAR");
                insertCommand.Parameters["@meters"].Value = GetDecimal(reader, "METRES");
                insertCommand.Parameters["@kilograms"].Value = GetDecimal(reader, "KG");
                insertCommand.Parameters["@notes"].Value = DbValue(GetString(reader, "OBSERV"));
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
                    Stage = "fil-detail",
                    LegacyEntityKey = $"{centerCode}/{code}",
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
            DELETE FROM fil
            WHERE CENTRO = @centerCode
              AND origin = 'legacy';
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
            FROM fil_detail detail
            INNER JOIN fil header
                ON header.CENTRO = detail.CENTRO
               AND header.CODI = detail.FIL_CODI
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
        return reader.IsDBNull(ordinal) ? 0 : Convert.ToInt32(reader.GetValue(ordinal));
    }

    private static decimal GetDecimal(MySqlDataReader reader, string columnName)
    {
        var ordinal = reader.GetOrdinal(columnName);
        return reader.IsDBNull(ordinal) ? 0m : Convert.ToDecimal(reader.GetValue(ordinal));
    }

    private static object DbValue(string value) =>
        string.IsNullOrWhiteSpace(value) ? DBNull.Value : value;

    private sealed record TableImportResult(int ImportedRows = 0, int SkippedRows = 0)
    {
        public int ImportedRows { get; set; } = ImportedRows;
        public int SkippedRows { get; set; } = SkippedRows;
    }

    private sealed record ImportedRowContext(string EntityNumber);
}
