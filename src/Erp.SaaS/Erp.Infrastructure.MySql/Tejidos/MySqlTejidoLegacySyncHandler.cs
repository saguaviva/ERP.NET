using Erp.Application.LegacySync;
using Erp.Infrastructure.MySql.Database;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Tejidos;

public sealed class MySqlTejidoLegacySyncHandler : ILegacyModuleSyncHandler
{
    private readonly MySqlConnectionFactory _saasConnectionFactory;
    private readonly LegacyMySqlConnectionFactory _legacyConnectionFactory;

    public MySqlTejidoLegacySyncHandler(
        MySqlConnectionFactory saasConnectionFactory,
        LegacyMySqlConnectionFactory legacyConnectionFactory)
    {
        _saasConnectionFactory = saasConnectionFactory;
        _legacyConnectionFactory = legacyConnectionFactory;
    }

    public string ModuleKey => LegacySyncModuleKeys.ArticleTejidos;
    public string DisplayName => "Artículos / Tejidos";

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
        var hasWidth2Column = await LegacyColumnExistsAsync(legacyConnection, "teixits", "AMPLE2", cancellationToken);

        try
        {
            await DeleteExistingMappingsAsync(saasConnection, transaction, context, cancellationToken);
            await DeleteLegacyChildrenAsync(saasConnection, transaction, context.CompanyLegacyCenterCode, cancellationToken);
            await DeleteLegacyHeadersAsync(saasConnection, transaction, context.CompanyLegacyCenterCode, cancellationToken);

            var headerImport = await CopyHeaderRowsAsync(
                legacyConnection,
                saasConnection,
                transaction,
                context.CompanyLegacyCenterCode,
                hasWidth2Column,
                errors,
                row =>
                {
                    importedCodes.Add(row.EntityNumber);
                    mappings.Add(new LegacySyncMappingRecord
                    {
                        LegacySourceSystem = "legacy",
                        LegacyCenterCode = context.CompanyLegacyCenterCode,
                        LegacyDocumentType = "TEIXIT",
                        LegacyDocumentNumber = row.EntityNumber,
                        TargetEntityName = "tejido",
                        TargetEntityId = row.EntityNumber
                    });
                },
                cancellationToken);

            var colorImport = await CopyColorsAsync(legacyConnection, saasConnection, transaction, context.CompanyLegacyCenterCode, importedCodes, errors, cancellationToken);
            var compositionImport = await CopyCompositionAsync(legacyConnection, saasConnection, transaction, context.CompanyLegacyCenterCode, importedCodes, errors, cancellationToken);
            var finishImport = await CopyFinishesAsync(legacyConnection, saasConnection, transaction, context.CompanyLegacyCenterCode, importedCodes, errors, cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return new LegacySyncModuleRunResult
            {
                RecordsInserted = headerImport.ImportedRows,
                RecordsUpdated = 0,
                RecordsSkipped = headerImport.SkippedRows + colorImport.SkippedRows + compositionImport.SkippedRows + finishImport.SkippedRows,
                Errors = errors,
                Mappings = mappings,
                NewCheckpointValue = DateTime.UtcNow.ToString("O"),
                Summary = $"Tejidos={headerImport.ImportedRows}; colores={colorImport.ImportedRows}; composición={compositionImport.ImportedRows}; acabados={finishImport.ImportedRows}; errores={errors.Count}"
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
        bool hasWidth2Column,
        List<LegacySyncErrorRecord> errors,
        Action<ImportedRowContext> onRowImported,
        CancellationToken cancellationToken)
    {
        var result = new TableImportResult();
        var syncedUtc = DateTime.UtcNow;

        await using var readCommand = legacyConnection.CreateCommand();
        readCommand.CommandText =
            $"""
            SELECT CODI, DESCRI, NRO, COALESCE(MAQUI, 0) AS MAQUI, COALESCE(MATERIA, 0) AS MATERIA, OBSERV, IVA,
                   COALESCE(TEIXIDOR, 0) AS TEIXIDOR, COALESCE(PTEIXIR, 0) AS PTEIXIR,
                   COALESCE(ESTAMPADOR, 0) AS ESTAMPADOR, COALESCE(PESTAM, 0) AS PESTAM,
                   COALESCE(ACABADOR, 0) AS ACABADOR, ACABAT, COALESCE(PACA, 0) AS PACA,
                   COALESCE(CRU, 0) AS CRU, AMPLE, COALESCE(RENDIMENT, 0) AS RENDIMENT, COALESCE(MARGE, 0) AS MARGE,
                   COALESCE(GRAMA, 0) AS GRAMA, COALESCE(PREUM, 0) AS PREUM, COALESCE(PREUK, 0) AS PREUK,
                   COALESCE(STCRUM, 0) AS STCRUM, COALESCE(STDISPM, 0) AS STDISPM,
                   COALESCE(STCRUK, 0) AS STCRUK, COALESCE(STDISPK, 0) AS STDISPK,
                   COALESCE(PREUPERMODEL, 0) AS PREUPERMODEL, COALESCE(TUBULAR, 0) AS TUBULAR, {(hasWidth2Column ? "COALESCE(AMPLE2, 0)" : "0")} AS AMPLE2
            FROM teixits
            WHERE CENTRO = @centerCode
            ORDER BY CODI;
            """;
        readCommand.Parameters.AddWithValue("@centerCode", centerCode);
        readCommand.CommandTimeout = 300;

        await using var insertCommand = saasConnection.CreateCommand();
        insertCommand.Transaction = transaction;
        insertCommand.CommandText =
            """
            INSERT INTO teixits (
                CODI, CENTRO, DESCRI, NRO, MAQUI, MATERIA, OBSERV, IVA, TEIXIDOR, PTEIXIR,
                ESTAMPADOR, PESTAM, ACABADOR, ACABAT, PACA, CRU, AMPLE, RENDIMENT, MARGE, GRAMA,
                PREUM, PREUK, STCRUM, STDISPM, STCRUK, STDISPK, PREUPERMODEL, TUBULAR, AMPLE2,
                origin, is_deleted, synced_utc)
            VALUES (
                @code, @centerCode, @description, @compositionText, @machineCode, @materialCost, @notes, @vatCode, @weaverCode, @weavingCost,
                @printerCode, @printingCost, @finisherCode, @finishSummary, @finishingCost, @rawCost, @widthText, @yield, @margin, @gramWeight,
                @pricePerMeter, @pricePerKilogram, @rawStockMeters, @availableStockMeters, @rawStockKilograms, @availableStockKilograms,
                @samplePrice, @isTubular, @width2, 'legacy', 0, @syncedUtc);
            """;
        AddHeaderParameters(insertCommand, syncedUtc);

        await using var reader = await readCommand.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var code = GetString(reader, "CODI");
            try
            {
                FillHeaderParameterValues(insertCommand, centerCode, code, reader);
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
                errors.Add(CreateError("teixits", centerCode, code, exception));
            }
        }

        return result;
    }

    private static async Task<TableImportResult> CopyColorsAsync(
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
            SELECT FIL, COALESCE(PROVE, 0) AS PROVE, COLOR, COALESCE(ACTUAL, 0) AS ACTUAL, COALESCE(MINIM, 0) AS MINIM,
                   COALESCE(TINTAR, 0) AS TINTAR, COALESCE(PREU, 0) AS PREU, COALESCE(METRES, 0) AS METRES,
                   COALESCE(KG, 0) AS KG, OBSERV
            FROM filcol
            WHERE TIPUS = 'T'
              AND PROVE = 0
              AND CENTRO = @centerCode
            ORDER BY FIL, COLOR, OBSERV;
            """;
        readCommand.Parameters.AddWithValue("@centerCode", centerCode);
        readCommand.CommandTimeout = 300;

        await using var insertCommand = saasConnection.CreateCommand();
        insertCommand.Transaction = transaction;
        insertCommand.CommandText =
            """
            INSERT INTO teixits_color_detail (
                CENTRO, TEIXIT_CODI, LINE_NUMBER, PROVE, COLOR, ACTUAL, MINIM, TINTAR, PREU, METRES, KG, OBSERV)
            VALUES (
                @centerCode, @code, @lineNumber, @supplierCode, @color, @currentStock, @minimumStock, @dyeingPrice, @unitCost, @metersPrice, @kilogramsPrice, @notes);
            """;
        AddColorParameters(insertCommand);

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
                var nextLineNumber = lineNumbers.TryGetValue(code, out var currentLineNumber) ? currentLineNumber + 1 : 1;
                lineNumbers[code] = nextLineNumber;
                FillColorParameterValues(insertCommand, centerCode, code, nextLineNumber, reader);
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
                errors.Add(CreateError("teixits-colors", centerCode, code, exception));
            }
        }

        return result;
    }

    private static async Task<TableImportResult> CopyCompositionAsync(
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
            SELECT TEIXIT, COMP, COALESCE(PER, 0) AS PER, COALESCE(PROVE, 0) AS PROVE, COALESCE(PREU, 0) AS PREU, COALESCE(IMPORT, 0) AS IMPORTE
            FROM mattei
            WHERE CENTRO = @centerCode
            ORDER BY TEIXIT, COMP, PROVE;
            """;
        readCommand.Parameters.AddWithValue("@centerCode", centerCode);
        readCommand.CommandTimeout = 300;

        await using var insertCommand = saasConnection.CreateCommand();
        insertCommand.Transaction = transaction;
        insertCommand.CommandText =
            """
            INSERT INTO teixits_composition_detail (
                CENTRO, TEIXIT_CODI, LINE_NUMBER, COMP, PER, PROVE, PREU, IMPORTE)
            VALUES (
                @centerCode, @code, @lineNumber, @componentCode, @percentage, @supplierCode, @unitPrice, @amount);
            """;
        AddCompositionParameters(insertCommand);

        await using var reader = await readCommand.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var code = GetString(reader, "TEIXIT");
            if (string.IsNullOrWhiteSpace(code) || !importedCodes.Contains(code))
            {
                result.SkippedRows++;
                continue;
            }

            try
            {
                var nextLineNumber = lineNumbers.TryGetValue(code, out var currentLineNumber) ? currentLineNumber + 1 : 1;
                lineNumbers[code] = nextLineNumber;
                FillCompositionParameterValues(insertCommand, centerCode, code, nextLineNumber, reader);
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
                errors.Add(CreateError("teixits-composition", centerCode, code, exception));
            }
        }

        return result;
    }

    private static async Task<TableImportResult> CopyFinishesAsync(
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
            SELECT a.TEIXIT, a.ACABAT, COALESCE(a.PROVE, 0) AS PROVE, COALESCE(a.ORDEN, 0) AS ORDEN,
                   COALESCE(d.PREUM, 0) AS PREUM, COALESCE(d.PREUK, 0) AS PREUK
            FROM acabatsteixits a
            LEFT JOIN dacabats d
              ON d.CODI = a.ACABAT
             AND d.PROVE = a.PROVE
            WHERE a.CENTRO = @centerCode
            ORDER BY a.TEIXIT, a.ORDEN, a.ACABAT;
            """;
        readCommand.Parameters.AddWithValue("@centerCode", centerCode);
        readCommand.CommandTimeout = 300;

        await using var insertCommand = saasConnection.CreateCommand();
        insertCommand.Transaction = transaction;
        insertCommand.CommandText =
            """
            INSERT INTO teixits_finish_detail (
                CENTRO, TEIXIT_CODI, LINE_NUMBER, ACABAT, PROVE, ORDEN, PREUM, PREUK)
            VALUES (
                @centerCode, @code, @lineNumber, @finishCode, @supplierCode, @order, @pricePerMeter, @pricePerKilogram);
            """;
        AddFinishParameters(insertCommand);

        await using var reader = await readCommand.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var code = GetString(reader, "TEIXIT");
            if (string.IsNullOrWhiteSpace(code) || !importedCodes.Contains(code))
            {
                result.SkippedRows++;
                continue;
            }

            try
            {
                var nextLineNumber = lineNumbers.TryGetValue(code, out var currentLineNumber) ? currentLineNumber + 1 : 1;
                lineNumbers[code] = nextLineNumber;
                FillFinishParameterValues(insertCommand, centerCode, code, nextLineNumber, reader);
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
                errors.Add(CreateError("teixits-finishes", centerCode, code, exception));
            }
        }

        return result;
    }

    private static async Task DeleteLegacyHeadersAsync(MySqlConnection connection, MySqlTransaction transaction, string centerCode, CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            DELETE FROM teixits
            WHERE CENTRO = @centerCode
              AND origin = 'legacy';
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task DeleteLegacyChildrenAsync(MySqlConnection connection, MySqlTransaction transaction, string centerCode, CancellationToken cancellationToken)
    {
        var statements = new[]
        {
            """
            DELETE detail
            FROM teixits_color_detail detail
            INNER JOIN teixits header
                ON header.CENTRO = detail.CENTRO
               AND header.CODI = detail.TEIXIT_CODI
            WHERE detail.CENTRO = @centerCode
              AND header.origin = 'legacy';
            """,
            """
            DELETE detail
            FROM teixits_composition_detail detail
            INNER JOIN teixits header
                ON header.CENTRO = detail.CENTRO
               AND header.CODI = detail.TEIXIT_CODI
            WHERE detail.CENTRO = @centerCode
              AND header.origin = 'legacy';
            """,
            """
            DELETE detail
            FROM teixits_finish_detail detail
            INNER JOIN teixits header
                ON header.CENTRO = detail.CENTRO
               AND header.CODI = detail.TEIXIT_CODI
            WHERE detail.CENTRO = @centerCode
              AND header.origin = 'legacy';
            """
        };

        foreach (var statement in statements)
        {
            await using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = statement;
            command.Parameters.AddWithValue("@centerCode", centerCode);
            await command.ExecuteNonQueryAsync(cancellationToken);
        }
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

    private static void AddHeaderParameters(MySqlCommand command, DateTime syncedUtc)
    {
        command.Parameters.Add("@code", MySqlDbType.VarChar);
        command.Parameters.Add("@centerCode", MySqlDbType.VarChar);
        command.Parameters.Add("@description", MySqlDbType.VarChar);
        command.Parameters.Add("@compositionText", MySqlDbType.VarChar);
        command.Parameters.Add("@machineCode", MySqlDbType.Int32);
        command.Parameters.Add("@materialCost", MySqlDbType.Decimal);
        command.Parameters.Add("@notes", MySqlDbType.VarChar);
        command.Parameters.Add("@vatCode", MySqlDbType.VarChar);
        command.Parameters.Add("@weaverCode", MySqlDbType.Int32);
        command.Parameters.Add("@weavingCost", MySqlDbType.Decimal);
        command.Parameters.Add("@printerCode", MySqlDbType.Int32);
        command.Parameters.Add("@printingCost", MySqlDbType.Decimal);
        command.Parameters.Add("@finisherCode", MySqlDbType.Int32);
        command.Parameters.Add("@finishSummary", MySqlDbType.VarChar);
        command.Parameters.Add("@finishingCost", MySqlDbType.Decimal);
        command.Parameters.Add("@rawCost", MySqlDbType.Decimal);
        command.Parameters.Add("@widthText", MySqlDbType.VarChar);
        command.Parameters.Add("@yield", MySqlDbType.Decimal);
        command.Parameters.Add("@margin", MySqlDbType.Decimal);
        command.Parameters.Add("@gramWeight", MySqlDbType.Decimal);
        command.Parameters.Add("@pricePerMeter", MySqlDbType.Decimal);
        command.Parameters.Add("@pricePerKilogram", MySqlDbType.Decimal);
        command.Parameters.Add("@rawStockMeters", MySqlDbType.Decimal);
        command.Parameters.Add("@availableStockMeters", MySqlDbType.Decimal);
        command.Parameters.Add("@rawStockKilograms", MySqlDbType.Decimal);
        command.Parameters.Add("@availableStockKilograms", MySqlDbType.Decimal);
        command.Parameters.Add("@samplePrice", MySqlDbType.Decimal);
        command.Parameters.Add("@isTubular", MySqlDbType.Bit);
        command.Parameters.Add("@width2", MySqlDbType.Decimal);
        command.Parameters.AddWithValue("@syncedUtc", syncedUtc);
    }

    private static void FillHeaderParameterValues(MySqlCommand command, string centerCode, string code, MySqlDataReader reader)
    {
        command.Parameters["@code"].Value = code;
        command.Parameters["@centerCode"].Value = centerCode;
        command.Parameters["@description"].Value = GetString(reader, "DESCRI");
        command.Parameters["@compositionText"].Value = DbValue(GetString(reader, "NRO"));
        command.Parameters["@machineCode"].Value = GetInt(reader, "MAQUI");
        command.Parameters["@materialCost"].Value = GetDecimal(reader, "MATERIA");
        command.Parameters["@notes"].Value = DbValue(GetString(reader, "OBSERV"));
        command.Parameters["@vatCode"].Value = DbValue(GetString(reader, "IVA"));
        command.Parameters["@weaverCode"].Value = GetInt(reader, "TEIXIDOR");
        command.Parameters["@weavingCost"].Value = GetDecimal(reader, "PTEIXIR");
        command.Parameters["@printerCode"].Value = GetInt(reader, "ESTAMPADOR");
        command.Parameters["@printingCost"].Value = GetDecimal(reader, "PESTAM");
        command.Parameters["@finisherCode"].Value = GetInt(reader, "ACABADOR");
        command.Parameters["@finishSummary"].Value = DbValue(GetString(reader, "ACABAT"));
        command.Parameters["@finishingCost"].Value = GetDecimal(reader, "PACA");
        command.Parameters["@rawCost"].Value = GetDecimal(reader, "CRU");
        command.Parameters["@widthText"].Value = DbValue(GetString(reader, "AMPLE"));
        command.Parameters["@yield"].Value = GetDecimal(reader, "RENDIMENT");
        command.Parameters["@margin"].Value = GetDecimal(reader, "MARGE");
        command.Parameters["@gramWeight"].Value = GetDecimal(reader, "GRAMA");
        command.Parameters["@pricePerMeter"].Value = GetDecimal(reader, "PREUM");
        command.Parameters["@pricePerKilogram"].Value = GetDecimal(reader, "PREUK");
        command.Parameters["@rawStockMeters"].Value = GetDecimal(reader, "STCRUM");
        command.Parameters["@availableStockMeters"].Value = GetDecimal(reader, "STDISPM");
        command.Parameters["@rawStockKilograms"].Value = GetDecimal(reader, "STCRUK");
        command.Parameters["@availableStockKilograms"].Value = GetDecimal(reader, "STDISPK");
        command.Parameters["@samplePrice"].Value = GetDecimal(reader, "PREUPERMODEL");
        command.Parameters["@isTubular"].Value = GetBool(reader, "TUBULAR");
        command.Parameters["@width2"].Value = GetDecimal(reader, "AMPLE2");
    }

    private static void AddColorParameters(MySqlCommand command)
    {
        command.Parameters.Add("@centerCode", MySqlDbType.VarChar);
        command.Parameters.Add("@code", MySqlDbType.VarChar);
        command.Parameters.Add("@lineNumber", MySqlDbType.Int32);
        command.Parameters.Add("@supplierCode", MySqlDbType.Int32);
        command.Parameters.Add("@color", MySqlDbType.VarChar);
        command.Parameters.Add("@currentStock", MySqlDbType.Decimal);
        command.Parameters.Add("@minimumStock", MySqlDbType.Decimal);
        command.Parameters.Add("@dyeingPrice", MySqlDbType.Decimal);
        command.Parameters.Add("@unitCost", MySqlDbType.Decimal);
        command.Parameters.Add("@metersPrice", MySqlDbType.Decimal);
        command.Parameters.Add("@kilogramsPrice", MySqlDbType.Decimal);
        command.Parameters.Add("@notes", MySqlDbType.VarChar);
    }

    private static void FillColorParameterValues(MySqlCommand command, string centerCode, string code, int lineNumber, MySqlDataReader reader)
    {
        command.Parameters["@centerCode"].Value = centerCode;
        command.Parameters["@code"].Value = code;
        command.Parameters["@lineNumber"].Value = lineNumber;
        command.Parameters["@supplierCode"].Value = GetInt(reader, "PROVE");
        command.Parameters["@color"].Value = DbValue(GetString(reader, "COLOR"));
        command.Parameters["@currentStock"].Value = GetDecimal(reader, "ACTUAL");
        command.Parameters["@minimumStock"].Value = GetDecimal(reader, "MINIM");
        command.Parameters["@dyeingPrice"].Value = GetDecimal(reader, "TINTAR");
        command.Parameters["@unitCost"].Value = GetDecimal(reader, "PREU");
        command.Parameters["@metersPrice"].Value = GetDecimal(reader, "METRES");
        command.Parameters["@kilogramsPrice"].Value = GetDecimal(reader, "KG");
        command.Parameters["@notes"].Value = DbValue(GetString(reader, "OBSERV"));
    }

    private static void AddCompositionParameters(MySqlCommand command)
    {
        command.Parameters.Add("@centerCode", MySqlDbType.VarChar);
        command.Parameters.Add("@code", MySqlDbType.VarChar);
        command.Parameters.Add("@lineNumber", MySqlDbType.Int32);
        command.Parameters.Add("@componentCode", MySqlDbType.VarChar);
        command.Parameters.Add("@percentage", MySqlDbType.Int32);
        command.Parameters.Add("@supplierCode", MySqlDbType.Int32);
        command.Parameters.Add("@unitPrice", MySqlDbType.Decimal);
        command.Parameters.Add("@amount", MySqlDbType.Decimal);
    }

    private static void FillCompositionParameterValues(MySqlCommand command, string centerCode, string code, int lineNumber, MySqlDataReader reader)
    {
        command.Parameters["@centerCode"].Value = centerCode;
        command.Parameters["@code"].Value = code;
        command.Parameters["@lineNumber"].Value = lineNumber;
        command.Parameters["@componentCode"].Value = DbValue(GetString(reader, "COMP"));
        command.Parameters["@percentage"].Value = GetInt(reader, "PER");
        command.Parameters["@supplierCode"].Value = GetInt(reader, "PROVE");
        command.Parameters["@unitPrice"].Value = GetDecimal(reader, "PREU");
        command.Parameters["@amount"].Value = GetDecimal(reader, "IMPORTE");
    }

    private static void AddFinishParameters(MySqlCommand command)
    {
        command.Parameters.Add("@centerCode", MySqlDbType.VarChar);
        command.Parameters.Add("@code", MySqlDbType.VarChar);
        command.Parameters.Add("@lineNumber", MySqlDbType.Int32);
        command.Parameters.Add("@finishCode", MySqlDbType.VarChar);
        command.Parameters.Add("@supplierCode", MySqlDbType.Int32);
        command.Parameters.Add("@order", MySqlDbType.Int32);
        command.Parameters.Add("@pricePerMeter", MySqlDbType.Decimal);
        command.Parameters.Add("@pricePerKilogram", MySqlDbType.Decimal);
    }

    private static void FillFinishParameterValues(MySqlCommand command, string centerCode, string code, int lineNumber, MySqlDataReader reader)
    {
        command.Parameters["@centerCode"].Value = centerCode;
        command.Parameters["@code"].Value = code;
        command.Parameters["@lineNumber"].Value = lineNumber;
        command.Parameters["@finishCode"].Value = DbValue(GetString(reader, "ACABAT"));
        command.Parameters["@supplierCode"].Value = GetInt(reader, "PROVE");
        command.Parameters["@order"].Value = GetInt(reader, "ORDEN");
        command.Parameters["@pricePerMeter"].Value = GetDecimal(reader, "PREUM");
        command.Parameters["@pricePerKilogram"].Value = GetDecimal(reader, "PREUK");
    }

    private static LegacySyncErrorRecord CreateError(string stage, string centerCode, string code, Exception exception) =>
        new()
        {
            Stage = stage,
            LegacyEntityKey = $"{centerCode}/{code}",
            ErrorMessage = exception.Message,
            Payload = string.Empty
        };

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

    private static bool GetBool(MySqlDataReader reader, string columnName)
    {
        var ordinal = reader.GetOrdinal(columnName);
        if (reader.IsDBNull(ordinal))
        {
            return false;
        }

        var value = reader.GetValue(ordinal);
        return value switch
        {
            bool boolValue => boolValue,
            byte byteValue => byteValue != 0,
            sbyte sbyteValue => sbyteValue != 0,
            short shortValue => shortValue != 0,
            ushort ushortValue => ushortValue != 0,
            int intValue => intValue != 0,
            uint uintValue => uintValue != 0,
            long longValue => longValue != 0,
            ulong ulongValue => ulongValue != 0,
            byte[] bytes => bytes.Any(item => item != 0),
            _ => Convert.ToString(value)?.Equals("true", StringComparison.OrdinalIgnoreCase) == true ||
                 Convert.ToString(value) == "1"
        };
    }

    private static async Task<bool> LegacyColumnExistsAsync(
        MySqlConnection connection,
        string tableName,
        string columnName,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT COUNT(*)
            FROM information_schema.columns
            WHERE table_schema = DATABASE()
              AND table_name = @tableName
              AND column_name = @columnName;
            """;
        command.Parameters.AddWithValue("@tableName", tableName);
        command.Parameters.AddWithValue("@columnName", columnName);
        return Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken)) > 0;
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
