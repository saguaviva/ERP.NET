using Erp.Application.LegacySync;
using Erp.Infrastructure.MySql.Database;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Disposiciones;

public sealed class MySqlDisposicionLegacySyncHandler : ILegacyModuleSyncHandler
{
    private readonly MySqlConnectionFactory _saasConnectionFactory;
    private readonly LegacyMySqlConnectionFactory _legacyConnectionFactory;

    public MySqlDisposicionLegacySyncHandler(
        MySqlConnectionFactory saasConnectionFactory,
        LegacyMySqlConnectionFactory legacyConnectionFactory)
    {
        _saasConnectionFactory = saasConnectionFactory;
        _legacyConnectionFactory = legacyConnectionFactory;
    }

    public string ModuleKey => LegacySyncModuleKeys.ArticleDispositions;
    public string DisplayName => "Artículos / Disposiciones";

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
        var importedCodes = new HashSet<int>();

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
                    importedCodes.Add(row.Code);
                    mappings.Add(new LegacySyncMappingRecord
                    {
                        LegacySourceSystem = "legacy",
                        LegacyCenterCode = context.CompanyLegacyCenterCode,
                        LegacyDocumentType = "DISPOS",
                        LegacyDocumentNumber = row.Code.ToString(),
                        TargetEntityName = "disposicion",
                        TargetEntityId = row.Code.ToString()
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
                Summary = $"Disposiciones replicadas={headerImport.ImportedRows}; líneas={detailImport.ImportedRows}; errores={errors.Count}",
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
        Action<ImportedHeaderContext> onRowImported,
        CancellationToken cancellationToken)
    {
        var result = new TableImportResult();
        var syncedUtc = DateTime.UtcNow;

        await using var readCommand = legacyConnection.CreateCommand();
        readCommand.CommandText =
            """
            SELECT CODI,
                   COALESCE(CODICLIENT, '') AS CODICLIENT,
                   CENTRO,
                   COALESCE(ANY, '') AS ANY,
                   COALESCE(IDDISPOS, 0) AS IDDISPOS,
                   CAST(FECHA AS CHAR) AS FECHA_TEXT,
                   CAST(DRECEPCION AS CHAR) AS DRECEPCION_TEXT,
                   COALESCE(ACABADOR, 0) AS ACABADOR,
                   COALESCE(ANULADA, 0) AS ANULADA,
                   COALESCE(CLIENT, 0) AS CLIENT,
                   COALESCE(OBSERV, '') AS OBSERV,
                   COALESCE(COLORCLIENTE, '') AS COLORCLIENTE,
                   COALESCE(TOTALPIEZAS, 0) AS TOTALPIEZAS,
                   COALESCE(TOTALKG, 0) AS TOTALKG,
                   COALESCE(COLOR, '') AS COLOR,
                   COALESCE(RECIBIDO, 0) AS RECIBIDO,
                   COALESCE(COMANDA, '') AS COMANDA
            FROM dispos
            WHERE CENTRO = @centerCode
            ORDER BY ANY, IDDISPOS, CODI;
            """;
        readCommand.Parameters.AddWithValue("@centerCode", centerCode);
        readCommand.CommandTimeout = 300;

        await using var insertCommand = saasConnection.CreateCommand();
        insertCommand.Transaction = transaction;
        insertCommand.CommandText =
            """
            INSERT INTO dispos (
                CODI,
                CODICLIENT,
                CENTRO,
                ANY,
                IDDISPOS,
                FECHA,
                DRECEPCION,
                ACABADOR,
                ANULADA,
                CLIENT,
                OBSERV,
                COLORCLIENTE,
                TOTALPIEZAS,
                TOTALKG,
                COLOR,
                RECIBIDO,
                COMANDA,
                origin,
                is_deleted,
                synced_utc)
            VALUES (
                @code,
                @clientReferenceCode,
                @centerCode,
                @year,
                @number,
                @date,
                @receptionDate,
                @finisherCode,
                @isCancelled,
                @clientCode,
                @notes,
                @clientColor,
                @totalPieces,
                @totalKilograms,
                @color,
                @isReceived,
                @orderReference,
                'legacy',
                0,
                @syncedUtc);
            """;
        insertCommand.Parameters.Add("@code", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@clientReferenceCode", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@centerCode", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@year", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@number", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@date", MySqlDbType.DateTime);
        insertCommand.Parameters.Add("@receptionDate", MySqlDbType.DateTime);
        insertCommand.Parameters.Add("@finisherCode", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@isCancelled", MySqlDbType.Bit);
        insertCommand.Parameters.Add("@clientCode", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@notes", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@clientColor", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@totalPieces", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@totalKilograms", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@color", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@isReceived", MySqlDbType.Bit);
        insertCommand.Parameters.Add("@orderReference", MySqlDbType.VarChar);
        insertCommand.Parameters.AddWithValue("@syncedUtc", syncedUtc);

        await using var reader = await readCommand.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var code = GetInt(reader, "CODI");
            try
            {
                insertCommand.Parameters["@code"].Value = code;
                insertCommand.Parameters["@clientReferenceCode"].Value = DbValue(GetString(reader, "CODICLIENT"));
                insertCommand.Parameters["@centerCode"].Value = centerCode;
                insertCommand.Parameters["@year"].Value = GetString(reader, "ANY");
                insertCommand.Parameters["@number"].Value = GetInt(reader, "IDDISPOS");
                insertCommand.Parameters["@date"].Value = GetDateTimeOrDefault(reader, "FECHA_TEXT");
                insertCommand.Parameters["@receptionDate"].Value = DbValue(GetDateTimeOrNull(reader, "DRECEPCION_TEXT"));
                insertCommand.Parameters["@finisherCode"].Value = GetInt(reader, "ACABADOR");
                insertCommand.Parameters["@isCancelled"].Value = GetBoolean(reader, "ANULADA");
                insertCommand.Parameters["@clientCode"].Value = GetInt(reader, "CLIENT");
                insertCommand.Parameters["@notes"].Value = DbValue(GetString(reader, "OBSERV"));
                insertCommand.Parameters["@clientColor"].Value = DbValue(GetString(reader, "COLORCLIENTE"));
                insertCommand.Parameters["@totalPieces"].Value = GetDecimal(reader, "TOTALPIEZAS");
                insertCommand.Parameters["@totalKilograms"].Value = GetDecimal(reader, "TOTALKG");
                insertCommand.Parameters["@color"].Value = DbValue(GetString(reader, "COLOR"));
                insertCommand.Parameters["@isReceived"].Value = GetBoolean(reader, "RECIBIDO");
                insertCommand.Parameters["@orderReference"].Value = DbValue(GetString(reader, "COMANDA"));
                await insertCommand.ExecuteNonQueryAsync(cancellationToken);
                result.ImportedRows++;
                onRowImported(new ImportedHeaderContext(code));
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
                    Stage = "dispos",
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
        ISet<int> importedCodes,
        List<LegacySyncErrorRecord> errors,
        CancellationToken cancellationToken)
    {
        var result = new TableImportResult();

        await using var readCommand = legacyConnection.CreateCommand();
        readCommand.CommandText =
            """
            SELECT COALESCE(DISPOS, 0) AS DISPOS,
                   COALESCE(LINEA, 0) AS LINEA,
                   COALESCE(DESCRIPCIO, '') AS DESCRIPCIO,
                   COALESCE(TEJEDOR, 0) AS TEJEDOR,
                   COALESCE(NALBARAN, '') AS NALBARAN,
                   COALESCE(TEJIDO, '') AS TEJIDO,
                   COALESCE(COMPOS, '') AS COMPOS,
                   COALESCE(NPIEZAS, '') AS NPIEZAS,
                   COALESCE(TOTALPIEZAS, 0) AS TOTALPIEZAS,
                   COALESCE(TOTALKG, 0) AS TOTALKG,
                   COALESCE(ACABADO, '') AS ACABADO,
                   COALESCE(ANCHO, '') AS ANCHO,
                   COALESCE(GRAMAJE, 0) AS GRAMAJE,
                   COALESCE(RENDIMIENTO, 0) AS RENDIMIENTO,
                   COALESCE(SERVIDO, 0) AS SERVIDO,
                   COALESCE(DISPUESTO, 0) AS DISPUESTO
            FROM ddispos
            WHERE CENTRO = @centerCode
            ORDER BY DISPOS, LINEA;
            """;
        readCommand.Parameters.AddWithValue("@centerCode", centerCode);
        readCommand.CommandTimeout = 300;

        await using var insertCommand = saasConnection.CreateCommand();
        insertCommand.Transaction = transaction;
        insertCommand.CommandText =
            """
            INSERT INTO ddispos (
                CENTRO,
                DESCRIPCIO,
                LINEA,
                DISPOS,
                TEJEDOR,
                NALBARAN,
                TEJIDO,
                COMPOS,
                NPIEZAS,
                TOTALPIEZAS,
                TOTALKG,
                ACABADO,
                ANCHO,
                GRAMAJE,
                RENDIMIENTO,
                SERVIDO,
                DISPUESTO)
            VALUES (
                @centerCode,
                @description,
                @lineNumber,
                @dispositionCode,
                @weaverCode,
                @deliveryNoteNumber,
                @fabricCode,
                @compositionText,
                @piecesText,
                @totalPieces,
                @totalKilograms,
                @finishText,
                @widthText,
                @gramWeight,
                @yield,
                @isServed,
                @isDisposed);
            """;
        insertCommand.Parameters.Add("@centerCode", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@description", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@lineNumber", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@dispositionCode", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@weaverCode", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@deliveryNoteNumber", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@fabricCode", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@compositionText", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@piecesText", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@totalPieces", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@totalKilograms", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@finishText", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@widthText", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@gramWeight", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@yield", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@isServed", MySqlDbType.Bit);
        insertCommand.Parameters.Add("@isDisposed", MySqlDbType.Bit);

        await using var reader = await readCommand.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var dispositionCode = GetInt(reader, "DISPOS");
            if (!importedCodes.Contains(dispositionCode))
            {
                result.SkippedRows++;
                continue;
            }

            try
            {
                insertCommand.Parameters["@centerCode"].Value = centerCode;
                insertCommand.Parameters["@description"].Value = DbValue(GetString(reader, "DESCRIPCIO"));
                insertCommand.Parameters["@lineNumber"].Value = GetInt(reader, "LINEA");
                insertCommand.Parameters["@dispositionCode"].Value = dispositionCode;
                insertCommand.Parameters["@weaverCode"].Value = GetInt(reader, "TEJEDOR");
                insertCommand.Parameters["@deliveryNoteNumber"].Value = GetString(reader, "NALBARAN");
                insertCommand.Parameters["@fabricCode"].Value = GetString(reader, "TEJIDO");
                insertCommand.Parameters["@compositionText"].Value = DbValue(GetString(reader, "COMPOS"));
                insertCommand.Parameters["@piecesText"].Value = DbValue(GetString(reader, "NPIEZAS"));
                insertCommand.Parameters["@totalPieces"].Value = GetDecimal(reader, "TOTALPIEZAS");
                insertCommand.Parameters["@totalKilograms"].Value = GetDecimal(reader, "TOTALKG");
                insertCommand.Parameters["@finishText"].Value = DbValue(GetString(reader, "ACABADO"));
                insertCommand.Parameters["@widthText"].Value = DbValue(GetString(reader, "ANCHO"));
                insertCommand.Parameters["@gramWeight"].Value = GetDecimal(reader, "GRAMAJE");
                insertCommand.Parameters["@yield"].Value = GetDecimal(reader, "RENDIMIENTO");
                insertCommand.Parameters["@isServed"].Value = GetBoolean(reader, "SERVIDO");
                insertCommand.Parameters["@isDisposed"].Value = GetBoolean(reader, "DISPUESTO");
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
                    Stage = "ddispos",
                    LegacyEntityKey = $"{centerCode}/{dispositionCode}/{GetInt(reader, "LINEA")}",
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
            DELETE FROM dispos
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
            FROM ddispos detail
            INNER JOIN dispos header
                ON header.CENTRO = detail.CENTRO
               AND header.CODI = detail.DISPOS
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

    private static bool GetBoolean(MySqlDataReader reader, string columnName)
    {
        var ordinal = reader.GetOrdinal(columnName);
        if (reader.IsDBNull(ordinal))
        {
            return false;
        }

        var value = reader.GetValue(ordinal);
        return value switch
        {
            bool booleanValue => booleanValue,
            byte byteValue => byteValue != 0,
            sbyte signedByte => signedByte != 0,
            short shortValue => shortValue != 0,
            int intValue => intValue != 0,
            long longValue => longValue != 0,
            string stringValue when string.Equals(stringValue, "1", StringComparison.OrdinalIgnoreCase) => true,
            string stringValue when string.Equals(stringValue, "true", StringComparison.OrdinalIgnoreCase) => true,
            _ => Convert.ToBoolean(value)
        };
    }

    private static DateTime GetDateTimeOrDefault(MySqlDataReader reader, string columnName)
    {
        return GetDateTimeOrNull(reader, columnName) ?? DateTime.Today;
    }

    private static DateTime? GetDateTimeOrNull(MySqlDataReader reader, string columnName)
    {
        var value = GetString(reader, columnName);
        if (string.IsNullOrWhiteSpace(value) ||
            string.Equals(value, "0000-00-00", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(value, "0000-00-00 00:00:00", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        return DateTime.TryParse(value, out var parsed) ? parsed : null;
    }

    private static object DbValue(string value) =>
        string.IsNullOrWhiteSpace(value) ? DBNull.Value : value;

    private static object DbValue(DateTime? value) =>
        value.HasValue ? value.Value : DBNull.Value;

    private sealed record TableImportResult(int ImportedRows = 0, int SkippedRows = 0)
    {
        public int ImportedRows { get; set; } = ImportedRows;
        public int SkippedRows { get; set; } = SkippedRows;
    }

    private sealed record ImportedHeaderContext(int Code);
}
