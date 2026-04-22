using Erp.Application.LegacySync;
using Erp.Infrastructure.MySql.Database;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Muestras;

public sealed class MySqlMuestraLegacySyncHandler : ILegacyModuleSyncHandler
{
    private readonly MySqlConnectionFactory _saasConnectionFactory;
    private readonly LegacyMySqlConnectionFactory _legacyConnectionFactory;

    public MySqlMuestraLegacySyncHandler(
        MySqlConnectionFactory saasConnectionFactory,
        LegacyMySqlConnectionFactory legacyConnectionFactory)
    {
        _saasConnectionFactory = saasConnectionFactory;
        _legacyConnectionFactory = legacyConnectionFactory;
    }

    public string ModuleKey => LegacySyncModuleKeys.ArticleMuestras;
    public string DisplayName => "Artículos / Muestras";

    public async Task<LegacySyncModuleRunResult> RunAsync(LegacySyncModuleContext context, CancellationToken cancellationToken = default)
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
            await DeleteBreakdownDetailRowsAsync(saasConnection, transaction, context.CompanyLegacyCenterCode, cancellationToken);
            await DeleteBreakdownRowsAsync(saasConnection, transaction, context.CompanyLegacyCenterCode, cancellationToken);
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
                        LegacyDocumentType = "MOSTRA",
                        LegacyDocumentNumber = row.EntityNumber,
                        TargetEntityName = "muestra",
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

            var lineMap = await LoadDetailLineMapAsync(
                saasConnection,
                transaction,
                context.CompanyLegacyCenterCode,
                cancellationToken);

            var machineImport = await CopyBreakdownHeadersAsync(
                legacyConnection,
                saasConnection,
                transaction,
                context.CompanyLegacyCenterCode,
                lineMap,
                errors,
                cancellationToken);

            var materialImport = await CopyBreakdownLinesAsync(
                legacyConnection,
                saasConnection,
                transaction,
                context.CompanyLegacyCenterCode,
                lineMap,
                errors,
                cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return new LegacySyncModuleRunResult
            {
                RecordsInserted = headerImport.ImportedRows,
                RecordsUpdated = 0,
                RecordsSkipped = headerImport.SkippedRows + detailImport.SkippedRows + machineImport.SkippedRows + materialImport.SkippedRows,
                NewCheckpointValue = DateTime.UtcNow.ToString("O"),
                Summary = $"Muestras replicadas={headerImport.ImportedRows}; detalle={detailImport.ImportedRows}; desglose={machineImport.ImportedRows}/{materialImport.ImportedRows}; errores={errors.Count}",
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
                   COALESCE(DESCRI, '') AS DESCRI,
                   COALESCE(CLIENT, 0) AS CLIENT,
                   COALESCE(REFE, '') AS REFE,
                   COALESCE(TEMP, '') AS TEMP,
                   COALESCE(MAQUINA, 0) AS MAQUINA,
                   COALESCE(MARGE, 0) AS MARGE,
                   COALESCE(IVA, '') AS IVA,
                   COALESCE(OBSERV, '') AS OBSERV,
                   COALESCE(COMPO, '') AS COMPO,
                   COALESCE(PREU, 0) AS PREU
            FROM MOSTRES
            WHERE CENTRO = @centerCode
            ORDER BY CODI, CLIENT;
            """;
        readCommand.Parameters.AddWithValue("@centerCode", centerCode);
        readCommand.CommandTimeout = 300;

        await using var insertCommand = saasConnection.CreateCommand();
        insertCommand.Transaction = transaction;
        insertCommand.CommandText =
            """
            INSERT INTO mostres (
                CODI, CENTRO, DESCRI, CLIENT, NOMCLIENT, REFE, TEMP,
                MAQUINA, NOMMAQUI, MARGE, IVA, OBSERV, COMPO, PREU,
                origin, is_deleted, synced_utc)
            VALUES (
                @code, @centerCode, @description, @clientCode, '', @reference, @season,
                @machineCode, '', @marginPercent, @vatCode, @notes, @composition, @unitPrice,
                'legacy', 0, @syncedUtc);
            """;
        insertCommand.Parameters.Add("@code", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@centerCode", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@description", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@clientCode", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@reference", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@season", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@machineCode", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@marginPercent", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@vatCode", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@notes", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@composition", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@unitPrice", MySqlDbType.Decimal);
        insertCommand.Parameters.AddWithValue("@syncedUtc", syncedUtc);

        await using var reader = await readCommand.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var code = GetString(reader, "CODI");
            if (string.IsNullOrWhiteSpace(code))
            {
                result.SkippedRows++;
                continue;
            }

            try
            {
                insertCommand.Parameters["@code"].Value = code;
                insertCommand.Parameters["@centerCode"].Value = centerCode;
                insertCommand.Parameters["@description"].Value = GetString(reader, "DESCRI");
                insertCommand.Parameters["@clientCode"].Value = GetInt(reader, "CLIENT");
                insertCommand.Parameters["@reference"].Value = DbValue(GetString(reader, "REFE"));
                insertCommand.Parameters["@season"].Value = DbValue(GetString(reader, "TEMP"));
                insertCommand.Parameters["@machineCode"].Value = GetInt(reader, "MAQUINA");
                insertCommand.Parameters["@marginPercent"].Value = GetDecimal(reader, "MARGE");
                insertCommand.Parameters["@vatCode"].Value = DbValue(GetString(reader, "IVA"));
                insertCommand.Parameters["@notes"].Value = DbValue(GetString(reader, "OBSERV"));
                insertCommand.Parameters["@composition"].Value = DbValue(GetString(reader, "COMPO"));
                insertCommand.Parameters["@unitPrice"].Value = GetDecimal(reader, "PREU");
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
                    Stage = "mostres",
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
            SELECT COALESCE(MOSTRA, '') AS MOSTRA,
                   COALESCE(TALLA, '') AS TALLA,
                   COALESCE(TALLAH, '') AS TALLAH,
                   COALESCE(TALLAL, '') AS TALLAL,
                   COALESCE(DESCRI, '') AS DESCRI,
                   COALESCE(COST, 0) AS COST,
                   COALESCE(VENDA, 0) AS VENDA,
                   COALESCE(COLOR, '') AS COLOR,
                   COALESCE(CLIENT, 0) AS CLIENT,
                   COALESCE(NCCODE, '') AS NCCODE
            FROM talla
            WHERE CENTRO = @centerCode
            ORDER BY MOSTRA, CLIENT, COLOR, TALLA;
            """;
        readCommand.Parameters.AddWithValue("@centerCode", centerCode);
        readCommand.CommandTimeout = 300;

        await using var insertCommand = saasConnection.CreateCommand();
        insertCommand.Transaction = transaction;
        insertCommand.CommandText =
            """
            INSERT INTO mostres_detail (
                CENTRO, MOSTRA_CODI, LINE_NUMBER, TALLA, TALLAH, TALLAL, DESCRI,
                COST, VENDA, COLOR, CLIENT, NOMCLIENT, NCCODE)
            VALUES (
                @centerCode, @code, @lineNumber, @sizeCode, @sizeHigh, @sizeLow, @description,
                @costPrice, @salePrice, @color, @clientCode, '', @ncCode);
            """;
        insertCommand.Parameters.Add("@centerCode", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@code", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@lineNumber", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@sizeCode", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@sizeHigh", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@sizeLow", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@description", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@costPrice", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@salePrice", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@color", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@clientCode", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@ncCode", MySqlDbType.VarChar);

        await using var reader = await readCommand.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var code = GetString(reader, "MOSTRA");
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
                insertCommand.Parameters["@sizeCode"].Value = DbValue(GetString(reader, "TALLA"));
                insertCommand.Parameters["@sizeHigh"].Value = DbValue(GetString(reader, "TALLAH"));
                insertCommand.Parameters["@sizeLow"].Value = DbValue(GetString(reader, "TALLAL"));
                insertCommand.Parameters["@description"].Value = DbValue(GetString(reader, "DESCRI"));
                insertCommand.Parameters["@costPrice"].Value = GetDecimal(reader, "COST");
                insertCommand.Parameters["@salePrice"].Value = GetDecimal(reader, "VENDA");
                insertCommand.Parameters["@color"].Value = DbValue(GetString(reader, "COLOR"));
                insertCommand.Parameters["@clientCode"].Value = GetInt(reader, "CLIENT");
                insertCommand.Parameters["@ncCode"].Value = DbValue(GetString(reader, "NCCODE"));
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
                    Stage = "mostres-detail",
                    LegacyEntityKey = $"{centerCode}/{code}",
                    ErrorMessage = exception.Message,
                    Payload = string.Empty
                });
            }
        }

        return result;
    }

    private static async Task<Dictionary<string, int>> LoadDetailLineMapAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        string centerCode,
        CancellationToken cancellationToken)
    {
        var map = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            SELECT MOSTRA_CODI,
                   LINE_NUMBER,
                   COALESCE(CLIENT, 0) AS CLIENT,
                   COALESCE(COLOR, '') AS COLOR,
                   COALESCE(TALLA, '') AS TALLA
            FROM mostres_detail
            WHERE CENTRO = @centerCode;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            map[BuildBreakdownKey(
                GetString(reader, "MOSTRA_CODI"),
                GetInt(reader, "CLIENT"),
                GetString(reader, "COLOR"),
                GetString(reader, "TALLA"))] = GetInt(reader, "LINE_NUMBER");
        }

        return map;
    }

    private static async Task<TableImportResult> CopyBreakdownHeadersAsync(
        MySqlConnection legacyConnection,
        MySqlConnection saasConnection,
        MySqlTransaction transaction,
        string centerCode,
        IReadOnlyDictionary<string, int> lineMap,
        List<LegacySyncErrorRecord> errors,
        CancellationToken cancellationToken)
    {
        var result = new TableImportResult();
        var syncedUtc = DateTime.UtcNow;

        await using var readCommand = legacyConnection.CreateCommand();
        readCommand.CommandText =
            """
            SELECT COALESCE(maq.MOSTRA, '') AS MOSTRA,
                   COALESCE(maq.CLIENT, 0) AS CLIENT,
                   COALESCE(maq.COLOR, '') AS COLOR,
                   COALESCE(maq.TALLA, '') AS TALLA,
                   maq.DATA,
                   COALESCE(mostra.MAQUINA, 0) AS MAQUINA,
                   COALESCE(maqui.DESCRI, '') AS NOMMAQUI,
                   COALESCE(maq.AGULLES, 0) AS AGULLES,
                   COALESCE(maq.VELOSITAT, 0) AS VELOSITAT,
                   COALESCE(maq.DISCO, '') AS DISCO,
                   COALESCE(maq.TEMPS, 0) AS TEMPS,
                   COALESCE(maqui.PREU, 0) AS MACHINE_RATE,
                   COALESCE(maq.IMPORT, 0) AS MACHINE_IMPORT,
                   COALESCE(maq.CORTES, '') AS CORTES,
                   COALESCE(maq.NOTAS, '') AS NOTES
            FROM MAQ maq
            LEFT JOIN MOSTRES mostra
              ON mostra.CENTRO = maq.CENTRO
             AND mostra.CODI = maq.MOSTRA
             AND mostra.CLIENT = maq.CLIENT
            LEFT JOIN MAQUI maqui
              ON maqui.CODI = mostra.MAQUINA
            WHERE maq.CENTRO = @centerCode
            ORDER BY maq.MOSTRA, maq.CLIENT, maq.COLOR, maq.TALLA;
            """;
        readCommand.Parameters.AddWithValue("@centerCode", centerCode);
        readCommand.CommandTimeout = 300;

        await using var insertCommand = saasConnection.CreateCommand();
        insertCommand.Transaction = transaction;
        insertCommand.CommandText =
            """
            INSERT INTO mostres_breakdown (
                CENTRO, MOSTRA_CODI, SAMPLE_LINE_NUMBER, DATA, CLIENT, NOMCLIENT,
                MAQUINA, NOMMAQUI, AGULLES, VELOSITAT, DISCO, TEMPS,
                MACHINE_RATE, MACHINE_IMPORT, CORTES, NOTES,
                origin, is_deleted, synced_utc)
            VALUES (
                @centerCode, @code, @sampleLineNumber, @workDate, @clientCode, '',
                @machineCode, @machineName, @needles, @speed, @disk, @timeMinutes,
                @machineRate, @machineImport, @cuts, @notes,
                'legacy', 0, @syncedUtc);
            """;
        insertCommand.Parameters.Add("@centerCode", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@code", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@sampleLineNumber", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@workDate", MySqlDbType.Date);
        insertCommand.Parameters.Add("@clientCode", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@machineCode", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@machineName", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@needles", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@speed", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@disk", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@timeMinutes", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@machineRate", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@machineImport", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@cuts", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@notes", MySqlDbType.VarChar);
        insertCommand.Parameters.AddWithValue("@syncedUtc", syncedUtc);

        await using var reader = await readCommand.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var code = GetString(reader, "MOSTRA");
            var clientCode = GetInt(reader, "CLIENT");
            var key = BuildBreakdownKey(code, clientCode, GetString(reader, "COLOR"), GetString(reader, "TALLA"));
            if (string.IsNullOrWhiteSpace(code) || !lineMap.TryGetValue(key, out var sampleLineNumber))
            {
                result.SkippedRows++;
                continue;
            }

            try
            {
                insertCommand.Parameters["@centerCode"].Value = centerCode;
                insertCommand.Parameters["@code"].Value = code;
                insertCommand.Parameters["@sampleLineNumber"].Value = sampleLineNumber;
                insertCommand.Parameters["@workDate"].Value = GetNullableDate(reader, "DATA");
                insertCommand.Parameters["@clientCode"].Value = clientCode;
                insertCommand.Parameters["@machineCode"].Value = GetInt(reader, "MAQUINA");
                insertCommand.Parameters["@machineName"].Value = DbValue(GetString(reader, "NOMMAQUI"));
                insertCommand.Parameters["@needles"].Value = GetDecimal(reader, "AGULLES");
                insertCommand.Parameters["@speed"].Value = GetDecimal(reader, "VELOSITAT");
                insertCommand.Parameters["@disk"].Value = DbValue(GetString(reader, "DISCO"));
                insertCommand.Parameters["@timeMinutes"].Value = GetDecimal(reader, "TEMPS");
                insertCommand.Parameters["@machineRate"].Value = GetDecimal(reader, "MACHINE_RATE");
                insertCommand.Parameters["@machineImport"].Value = GetDecimal(reader, "MACHINE_IMPORT");
                insertCommand.Parameters["@cuts"].Value = DbValue(GetString(reader, "CORTES"));
                insertCommand.Parameters["@notes"].Value = DbValue(GetString(reader, "NOTES"));
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
                    Stage = "mostres-breakdown",
                    LegacyEntityKey = $"{centerCode}/{code}/{key}",
                    ErrorMessage = exception.Message,
                    Payload = string.Empty
                });
            }
        }

        return result;
    }

    private static async Task<TableImportResult> CopyBreakdownLinesAsync(
        MySqlConnection legacyConnection,
        MySqlConnection saasConnection,
        MySqlTransaction transaction,
        string centerCode,
        IReadOnlyDictionary<string, int> lineMap,
        List<LegacySyncErrorRecord> errors,
        CancellationToken cancellationToken)
    {
        var result = new TableImportResult();
        var lineNumbers = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        await using var readCommand = legacyConnection.CreateCommand();
        readCommand.CommandText =
            """
            SELECT COALESCE(color.MOSTRA, '') AS MOSTRA,
                   COALESCE(color.CLIENT, 0) AS CLIENT,
                   COALESCE(color.COLORM, '') AS COLORM,
                   COALESCE(color.TALLA, '') AS TALLA,
                   COALESCE(color.TEIXIT, '') AS TEIXIT,
                   COALESCE(color.PROVE, 0) AS PROVE,
                   COALESCE(prove.NOM, '') AS NOMPROVE,
                   COALESCE(color.COLOR, '') AS COLOR,
                   COALESCE(color.FIL, 0) AS FIL,
                   COALESCE(color.CAPS, 0) AS CAPS,
                   COALESCE(color.PASSADES, 0) AS PASSADES,
                   COALESCE(color.GRADUACION, 0) AS GRADUACION,
                   COALESCE(color.CONSUM, 0) AS CONSUM,
                   COALESCE(color.PREU, 0) AS PREU,
                   COALESCE(color.IMPORT, 0) AS IMPORT
            FROM COLOR color
            LEFT JOIN PROVE prove
              ON prove.CODI = color.PROVE
            WHERE color.CENTRO = @centerCode
            ORDER BY color.MOSTRA, color.CLIENT, color.COLORM, color.TALLA, color.LINEA;
            """;
        readCommand.Parameters.AddWithValue("@centerCode", centerCode);
        readCommand.CommandTimeout = 300;

        await using var insertCommand = saasConnection.CreateCommand();
        insertCommand.Transaction = transaction;
        insertCommand.CommandText =
            """
            INSERT INTO mostres_breakdown_lines (
                CENTRO, MOSTRA_CODI, SAMPLE_LINE_NUMBER, LINE_NUMBER, TEIXIT,
                PROVE, NOMPROVE, COLOR, FIL, CAPS, PASSADES, GRADUACION,
                CONSUM, PREU, IMPORT)
            VALUES (
                @centerCode, @code, @sampleLineNumber, @lineNumber, @yarnCode,
                @providerCode, @providerName, @materialColor, @yarnMetric, @ends, @passes, @graduation,
                @consumption, @price, @importAmount);
            """;
        insertCommand.Parameters.Add("@centerCode", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@code", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@sampleLineNumber", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@lineNumber", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@yarnCode", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@providerCode", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@providerName", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@materialColor", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@yarnMetric", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@ends", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@passes", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@graduation", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@consumption", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@price", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@importAmount", MySqlDbType.Decimal);

        await using var reader = await readCommand.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var code = GetString(reader, "MOSTRA");
            var key = BuildBreakdownKey(code, GetInt(reader, "CLIENT"), GetString(reader, "COLORM"), GetString(reader, "TALLA"));
            if (string.IsNullOrWhiteSpace(code) || !lineMap.TryGetValue(key, out var sampleLineNumber))
            {
                result.SkippedRows++;
                continue;
            }

            try
            {
                var lineNumberKey = $"{code}|{sampleLineNumber}";
                var nextLineNumber = lineNumbers.TryGetValue(lineNumberKey, out var currentLineNumber)
                    ? currentLineNumber + 1
                    : 1;
                lineNumbers[lineNumberKey] = nextLineNumber;

                insertCommand.Parameters["@centerCode"].Value = centerCode;
                insertCommand.Parameters["@code"].Value = code;
                insertCommand.Parameters["@sampleLineNumber"].Value = sampleLineNumber;
                insertCommand.Parameters["@lineNumber"].Value = nextLineNumber;
                insertCommand.Parameters["@yarnCode"].Value = DbValue(GetString(reader, "TEIXIT"));
                insertCommand.Parameters["@providerCode"].Value = GetInt(reader, "PROVE");
                insertCommand.Parameters["@providerName"].Value = DbValue(GetString(reader, "NOMPROVE"));
                insertCommand.Parameters["@materialColor"].Value = DbValue(GetString(reader, "COLOR"));
                insertCommand.Parameters["@yarnMetric"].Value = GetDecimal(reader, "FIL");
                insertCommand.Parameters["@ends"].Value = GetDecimal(reader, "CAPS");
                insertCommand.Parameters["@passes"].Value = GetDecimal(reader, "PASSADES");
                insertCommand.Parameters["@graduation"].Value = GetInt(reader, "GRADUACION");
                insertCommand.Parameters["@consumption"].Value = GetDecimal(reader, "CONSUM");
                insertCommand.Parameters["@price"].Value = GetDecimal(reader, "PREU");
                insertCommand.Parameters["@importAmount"].Value = GetDecimal(reader, "IMPORT");
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
                    Stage = "mostres-breakdown-lines",
                    LegacyEntityKey = $"{centerCode}/{code}/{key}",
                    ErrorMessage = exception.Message,
                    Payload = string.Empty
                });
            }
        }

        return result;
    }

    private static async Task DeleteTargetRowsAsync(MySqlConnection connection, MySqlTransaction transaction, string centerCode, CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            DELETE FROM mostres
            WHERE CENTRO = @centerCode
              AND origin = 'legacy';
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task DeleteBreakdownRowsAsync(MySqlConnection connection, MySqlTransaction transaction, string centerCode, CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            DELETE FROM mostres_breakdown
            WHERE CENTRO = @centerCode
              AND origin = 'legacy';
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task DeleteBreakdownDetailRowsAsync(MySqlConnection connection, MySqlTransaction transaction, string centerCode, CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            DELETE detail
            FROM mostres_breakdown_lines detail
            INNER JOIN mostres_breakdown header
                ON header.CENTRO = detail.CENTRO
               AND header.MOSTRA_CODI = detail.MOSTRA_CODI
               AND header.SAMPLE_LINE_NUMBER = detail.SAMPLE_LINE_NUMBER
            WHERE detail.CENTRO = @centerCode
              AND header.origin = 'legacy';
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task DeleteDetailRowsAsync(MySqlConnection connection, MySqlTransaction transaction, string centerCode, CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            DELETE detail
            FROM mostres_detail detail
            INNER JOIN mostres header
                ON header.CENTRO = detail.CENTRO
               AND header.CODI = detail.MOSTRA_CODI
            WHERE detail.CENTRO = @centerCode
              AND header.origin = 'legacy';
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task DeleteExistingMappingsAsync(MySqlConnection connection, MySqlTransaction transaction, LegacySyncModuleContext context, CancellationToken cancellationToken)
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

    private static object GetNullableDate(MySqlDataReader reader, string columnName)
    {
        var ordinal = reader.GetOrdinal(columnName);
        if (reader.IsDBNull(ordinal))
        {
            return DBNull.Value;
        }

        try
        {
            return Convert.ToDateTime(reader.GetValue(ordinal)).Date;
        }
        catch
        {
            return DBNull.Value;
        }
    }

    private static string BuildBreakdownKey(string code, int clientCode, string color, string sizeCode) =>
        $"{code.Trim().ToUpperInvariant()}|{clientCode}|{color.Trim().ToUpperInvariant()}|{sizeCode.Trim().ToUpperInvariant()}";

    private static object DbValue(string value) =>
        string.IsNullOrWhiteSpace(value) ? DBNull.Value : value;

    private sealed record TableImportResult(int ImportedRows = 0, int SkippedRows = 0)
    {
        public int ImportedRows { get; set; } = ImportedRows;
        public int SkippedRows { get; set; } = SkippedRows;
    }

    private sealed record ImportedRowContext(string EntityNumber);
}
