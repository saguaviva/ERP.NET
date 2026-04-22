using Erp.Application.LegacySync;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Support;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Models;

public sealed class MySqlModeloLegacySyncHandler : ILegacyModuleSyncHandler
{
    private readonly MySqlConnectionFactory _saasConnectionFactory;
    private readonly LegacyMySqlConnectionFactory _legacyConnectionFactory;

    public MySqlModeloLegacySyncHandler(
        MySqlConnectionFactory saasConnectionFactory,
        LegacyMySqlConnectionFactory legacyConnectionFactory)
    {
        _saasConnectionFactory = saasConnectionFactory;
        _legacyConnectionFactory = legacyConnectionFactory;
    }

    public string ModuleKey => LegacySyncModuleKeys.ArticleModels;
    public string DisplayName => "Artículos / Models";

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

        var mappings = new List<LegacySyncMappingRecord>();
        var errors = new List<LegacySyncErrorRecord>();

        try
        {
            await DeleteExistingMappingsAsync(saasConnection, transaction, context, cancellationToken);
            await DeleteExistingLegacyRowsAsync(saasConnection, transaction, context, cancellationToken);

            var modelColumns = await LoadLegacyColumnsAsync(legacyConnection, "MODELS", cancellationToken);
            EnsureRequiredColumns("MODELS", modelColumns, "CODI", "SERIE", "TEMPORADA", "CLIENT", "CENTRO");

            var modelIds = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            var headers = await CopyHeaderRowsAsync(
                legacyConnection,
                saasConnection,
                transaction,
                context,
                modelColumns,
                modelIds,
                mappings,
                errors,
                cancellationToken);

            var scandallo = await CopyScandalloRowsAsync(
                legacyConnection,
                saasConnection,
                transaction,
                context,
                modelIds,
                errors,
                cancellationToken);

            var colors = await CopyColorRowsAsync(
                legacyConnection,
                saasConnection,
                transaction,
                context,
                modelIds,
                errors,
                cancellationToken);

            var fornituras = await CopyFornituraRowsAsync(
                legacyConnection,
                saasConnection,
                transaction,
                context,
                modelIds,
                errors,
                cancellationToken);

            var stock = await CopyStockRowsAsync(
                legacyConnection,
                saasConnection,
                transaction,
                context,
                modelIds,
                errors,
                cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return new LegacySyncModuleRunResult
            {
                RecordsInserted = headers.ImportedRows,
                RecordsUpdated = 0,
                RecordsSkipped = headers.SkippedRows + scandallo.SkippedRows + colors.SkippedRows + fornituras.SkippedRows + stock.SkippedRows,
                NewCheckpointValue = DateTime.UtcNow.ToString("O"),
                Summary = $"Models={headers.ImportedRows}; escandallo={scandallo.ImportedRows}; colores={colors.ImportedRows}; fornituras={fornituras.ImportedRows}; stock={stock.ImportedRows}; errores={errors.Count}",
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

    private static async Task DeleteExistingLegacyRowsAsync(
        MySqlConnection saasConnection,
        MySqlTransaction transaction,
        LegacySyncModuleContext context,
        CancellationToken cancellationToken)
    {
        foreach (var childTable in new[]
                 {
                     "article_model_scandallo",
                     "article_model_colors",
                     "article_model_fornituras",
                     "article_model_stock"
                 })
        {
            await using var childDelete = saasConnection.CreateCommand();
            childDelete.Transaction = transaction;
            childDelete.CommandText =
                $"""
                DELETE FROM {childTable}
                WHERE model_id IN (
                    SELECT record_id
                    FROM article_models
                    WHERE tenant_id = @tenantId
                      AND company_id = @companyId
                      AND CENTRO = @centerCode
                      AND origin = 'legacy');
                """;
            childDelete.Parameters.AddWithValue("@tenantId", context.TenantId.ToString());
            childDelete.Parameters.AddWithValue("@companyId", context.CompanyId.ToString());
            childDelete.Parameters.AddWithValue("@centerCode", context.CompanyLegacyCenterCode);
            await childDelete.ExecuteNonQueryAsync(cancellationToken);
        }

        await using var headerDelete = saasConnection.CreateCommand();
        headerDelete.Transaction = transaction;
        headerDelete.CommandText =
            """
            DELETE FROM article_models
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND CENTRO = @centerCode
              AND origin = 'legacy';
            """;
        headerDelete.Parameters.AddWithValue("@tenantId", context.TenantId.ToString());
        headerDelete.Parameters.AddWithValue("@companyId", context.CompanyId.ToString());
        headerDelete.Parameters.AddWithValue("@centerCode", context.CompanyLegacyCenterCode);
        await headerDelete.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task<TableImportResult> CopyHeaderRowsAsync(
        MySqlConnection legacyConnection,
        MySqlConnection saasConnection,
        MySqlTransaction transaction,
        LegacySyncModuleContext context,
        ISet<string> columns,
        IDictionary<string, string> modelIds,
        List<LegacySyncMappingRecord> mappings,
        List<LegacySyncErrorRecord> errors,
        CancellationToken cancellationToken)
    {
        var result = new TableImportResult();
        var now = DateTime.UtcNow;

        await using var readCommand = legacyConnection.CreateCommand();
        readCommand.CommandText =
            $"""
            SELECT `CODI` AS CODI,
                   `SERIE` AS SERIE,
                   `TEMPORADA` AS TEMPORADA,
                   `CLIENT` AS CLIENT,
                   `CENTRO` AS CENTRO,
                   {LegacyColumn(columns, "DESCRI", "''")} AS DESCRI,
                   {LegacyColumn(columns, "CODIMODEL", "''")} AS CODIMODEL,
                   {LegacyColumn(columns, "TEIXIT", "''")} AS TEIXIT,
                   {LegacyColumn(columns, "DESCRITEIXIT", "''")} AS DESCRITEIXIT,
                   {LegacyColumn(columns, "PROVE", "0")} AS PROVE,
                   {LegacyColumn(columns, "AMPLE", "''")} AS AMPLE,
                   {LegacyColumn(columns, "TINT", "0")} AS TINT,
                   {LegacyColumn(columns, "ACA", "0")} AS ACA,
                   {LegacyColumn(columns, "ESTAM", "0")} AS ESTAM,
                   {LegacyColumn(columns, "CONFEC", "0")} AS CONFEC,
                   {LegacyColumn(columns, "RENDIM", "0")} AS RENDIM,
                   {LegacyColumn(columns, "FORNITURA", "''")} AS FORNITURA,
                   {LegacyColumn(columns, "CESTAM", "''")} AS CESTAM,
                   {LegacyColumn(columns, "CESTAM2", "''")} AS CESTAM2,
                   {LegacyColumn(columns, "NESTAM", "0")} AS NESTAM,
                   {LegacyColumn(columns, "NESTAM2", "0")} AS NESTAM2,
                   {LegacyColumn(columns, "NCONFEC", "0")} AS NCONFEC,
                   {LegacyColumn(columns, "NPLANXA", "0")} AS NPLANXA,
                   {LegacyColumn(columns, "NREPAS", "0")} AS NREPAS,
                   {LegacyColumn(columns, "QTRANS", "0")} AS QTRANS,
                   {LegacyColumn(columns, "NTRANS", "0")} AS NTRANS,
                   {LegacyColumn(columns, "QFLOCAT", "0")} AS QFLOCAT,
                   {LegacyColumn(columns, "NFLOCAT", "0")} AS NFLOCAT,
                   {LegacyColumn(columns, "QBRODAT", "0")} AS QBRODAT,
                   {LegacyColumn(columns, "NBRODAT", "0")} AS NBRODAT,
                   {LegacyColumn(columns, "NESTAMP", "0")} AS NESTAMP,
                   {LegacyColumn(columns, "NTINTP", "0")} AS NTINTP,
                   {LegacyColumn(columns, "NACAP", "0")} AS NACAP,
                   {LegacyColumn(columns, "NFORNITURA", "0")} AS NFORNITURA,
                   {LegacyColumn(columns, "MANIPULACION", "0")} AS MANIPULACION,
                   {LegacyColumn(columns, "COST", "0")} AS COST,
                   {LegacyColumn(columns, "MARGE", "0")} AS MARGE,
                   {LegacyColumn(columns, "VENDA", "0")} AS VENDA,
                   {LegacyColumn(columns, "VENDAFINAL", "0")} AS VENDAFINAL,
                   {LegacyColumn(columns, "OBSERV", "''")} AS OBSERV,
                   {LegacyColumn(columns, "IVA", "''")} AS IVA,
                   {LegacyColumn(columns, "TALLA01", "''")} AS TALLA01,
                   {LegacyColumn(columns, "TALLA02", "''")} AS TALLA02,
                   {LegacyColumn(columns, "TALLA03", "''")} AS TALLA03,
                   {LegacyColumn(columns, "TALLA04", "''")} AS TALLA04,
                   {LegacyColumn(columns, "TALLA05", "''")} AS TALLA05,
                   {LegacyColumn(columns, "TALLA06", "''")} AS TALLA06,
                   {LegacyColumn(columns, "TALLA07", "''")} AS TALLA07,
                   {LegacyColumn(columns, "TALLA08", "''")} AS TALLA08,
                   {LegacyColumn(columns, "TALLA09", "''")} AS TALLA09,
                   {LegacyColumn(columns, "TALLA10", "''")} AS TALLA10,
                   {LegacyColumn(columns, "NPACK", "0")} AS NPACK
            FROM `MODELS`
            WHERE `CENTRO` = @centerCode
            ORDER BY `TEMPORADA`, `SERIE`, `CLIENT`, `CODI`;
            """;
        readCommand.Parameters.AddWithValue("@centerCode", context.CompanyLegacyCenterCode);
        readCommand.CommandTimeout = 300;

        await using var insertCommand = saasConnection.CreateCommand();
        insertCommand.Transaction = transaction;
        insertCommand.CommandText =
            """
            INSERT INTO article_models (
                record_id, tenant_id, company_id, CENTRO, CODI, SERIE, CLIENT, NOMCLIENT, TEMPORADA, DESCRI, CODIMODEL,
                TEIXIT, DESCRITEIXIT, PROVE, NOMPROVE, AMPLE, TINT, NOMTINT, ACA, NOMACA, ESTAM, NOMESTAM, CONFEC, NOMCONFEC,
                RENDIM, FORNITURA, CESTAM, CESTAM2, NESTAM, NESTAM2, NCONFEC, NPLANXA, NREPAS, QTRANS, NTRANS, QFLOCAT, NFLOCAT,
                QBRODAT, NBRODAT, NESTAMP, NTINTP, NACAP, NFORNITURA, MANIPULACION, COST, MARGE, VENDA, VENDAFINAL, OBSERV, IVA,
                TALLA01, TALLA02, TALLA03, TALLA04, TALLA05, TALLA06, TALLA07, TALLA08, TALLA09, TALLA10, NPACK,
                origin, is_deleted, synced_utc, created_utc, updated_utc)
            VALUES (
                @recordId, @tenantId, @companyId, @centerCode, @code, @series, @clientCode, @clientName, @season, @description, @modelCode,
                @fabricCode, @fabricDescription, @supplierCode, @supplierName, @widthText, @dyeingWorkshopCode, @dyeingWorkshopName,
                @finishingWorkshopCode, @finishingWorkshopName, @printingWorkshopCode, @printingWorkshopName, @tailoringWorkshopCode, @tailoringWorkshopName,
                @yield, @fornituraSummary, @printingCode1, @printingCode2, @printingUnits1, @printingUnits2, @tailoringPrice, @platePrice, @reviewPrice,
                @transferQuantity, @transferPrice, @flockedQuantity, @flockedPrice, @embroideredQuantity, @embroideredPrice, @printingPrice,
                @dyeingPrice, @finishingPrice, @fornituraPrice, @manipulationPrice, @costPrice, @marginPercent, @salePrice, @finalSalePrice, @notes, @vatCode,
                @size01, @size02, @size03, @size04, @size05, @size06, @size07, @size08, @size09, @size10, @packagingPrice,
                'legacy', 0, @syncedUtc, @createdUtc, @updatedUtc);
            """;
        AddHeaderInsertParameters(insertCommand, context, now);

        await using var reader = await readCommand.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var code = reader.GetStringOrEmpty("CODI").Trim().ToUpperInvariant();
            var series = reader.GetStringOrEmpty("SERIE").Trim().ToUpperInvariant();
            var season = reader.GetStringOrEmpty("TEMPORADA").Trim().ToUpperInvariant();
            var centerCode = reader.GetStringOrEmpty("CENTRO").Trim().ToUpperInvariant();
            var clientCode = reader.GetInt32OrDefault("CLIENT");

            if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(series) || string.IsNullOrWhiteSpace(season) || clientCode <= 0)
            {
                result.SkippedRows++;
                continue;
            }

            var recordId = Guid.NewGuid().ToString();
            var identityKey = BuildIdentityKey(centerCode, code, series, season, clientCode);

            try
            {
                insertCommand.Parameters["@recordId"].Value = recordId;
                insertCommand.Parameters["@centerCode"].Value = centerCode;
                insertCommand.Parameters["@code"].Value = code;
                insertCommand.Parameters["@series"].Value = series;
                insertCommand.Parameters["@clientCode"].Value = clientCode;
                insertCommand.Parameters["@clientName"].Value = DBNull.Value;
                insertCommand.Parameters["@season"].Value = season;
                insertCommand.Parameters["@description"].Value = DbValue(reader.GetStringOrEmpty("DESCRI"), code);
                insertCommand.Parameters["@modelCode"].Value = DbValue(reader.GetStringOrEmpty("CODIMODEL"));
                insertCommand.Parameters["@fabricCode"].Value = DbValue(reader.GetStringOrEmpty("TEIXIT"));
                insertCommand.Parameters["@fabricDescription"].Value = DbValue(reader.GetStringOrEmpty("DESCRITEIXIT"));
                insertCommand.Parameters["@supplierCode"].Value = reader.GetInt32OrDefault("PROVE");
                insertCommand.Parameters["@supplierName"].Value = DBNull.Value;
                insertCommand.Parameters["@widthText"].Value = DbValue(reader.GetStringOrEmpty("AMPLE"));
                insertCommand.Parameters["@dyeingWorkshopCode"].Value = reader.GetInt32OrDefault("TINT");
                insertCommand.Parameters["@dyeingWorkshopName"].Value = DBNull.Value;
                insertCommand.Parameters["@finishingWorkshopCode"].Value = reader.GetInt32OrDefault("ACA");
                insertCommand.Parameters["@finishingWorkshopName"].Value = DBNull.Value;
                insertCommand.Parameters["@printingWorkshopCode"].Value = reader.GetInt32OrDefault("ESTAM");
                insertCommand.Parameters["@printingWorkshopName"].Value = DBNull.Value;
                insertCommand.Parameters["@tailoringWorkshopCode"].Value = reader.GetInt32OrDefault("CONFEC");
                insertCommand.Parameters["@tailoringWorkshopName"].Value = DBNull.Value;
                insertCommand.Parameters["@yield"].Value = reader.GetDecimalOrDefault("RENDIM");
                insertCommand.Parameters["@fornituraSummary"].Value = DbValue(reader.GetStringOrEmpty("FORNITURA"));
                insertCommand.Parameters["@printingCode1"].Value = DbValue(reader.GetStringOrEmpty("CESTAM"));
                insertCommand.Parameters["@printingCode2"].Value = DbValue(reader.GetStringOrEmpty("CESTAM2"));
                insertCommand.Parameters["@printingUnits1"].Value = reader.GetDecimalOrDefault("NESTAM");
                insertCommand.Parameters["@printingUnits2"].Value = reader.GetDecimalOrDefault("NESTAM2");
                insertCommand.Parameters["@tailoringPrice"].Value = reader.GetDecimalOrDefault("NCONFEC");
                insertCommand.Parameters["@platePrice"].Value = reader.GetDecimalOrDefault("NPLANXA");
                insertCommand.Parameters["@reviewPrice"].Value = reader.GetDecimalOrDefault("NREPAS");
                insertCommand.Parameters["@transferQuantity"].Value = reader.GetDecimalOrDefault("QTRANS");
                insertCommand.Parameters["@transferPrice"].Value = reader.GetDecimalOrDefault("NTRANS");
                insertCommand.Parameters["@flockedQuantity"].Value = reader.GetDecimalOrDefault("QFLOCAT");
                insertCommand.Parameters["@flockedPrice"].Value = reader.GetDecimalOrDefault("NFLOCAT");
                insertCommand.Parameters["@embroideredQuantity"].Value = reader.GetDecimalOrDefault("QBRODAT");
                insertCommand.Parameters["@embroideredPrice"].Value = reader.GetDecimalOrDefault("NBRODAT");
                insertCommand.Parameters["@printingPrice"].Value = reader.GetDecimalOrDefault("NESTAMP");
                insertCommand.Parameters["@dyeingPrice"].Value = reader.GetDecimalOrDefault("NTINTP");
                insertCommand.Parameters["@finishingPrice"].Value = reader.GetDecimalOrDefault("NACAP");
                insertCommand.Parameters["@fornituraPrice"].Value = reader.GetDecimalOrDefault("NFORNITURA");
                insertCommand.Parameters["@manipulationPrice"].Value = reader.GetDecimalOrDefault("MANIPULACION");
                insertCommand.Parameters["@costPrice"].Value = reader.GetDecimalOrDefault("COST");
                insertCommand.Parameters["@marginPercent"].Value = reader.GetDecimalOrDefault("MARGE");
                insertCommand.Parameters["@salePrice"].Value = reader.GetDecimalOrDefault("VENDA");
                insertCommand.Parameters["@finalSalePrice"].Value = reader.GetDecimalOrDefault("VENDAFINAL");
                insertCommand.Parameters["@notes"].Value = DbValue(reader.GetStringOrEmpty("OBSERV"));
                insertCommand.Parameters["@vatCode"].Value = DbValue(reader.GetStringOrEmpty("IVA"));
                insertCommand.Parameters["@size01"].Value = DbValue(reader.GetStringOrEmpty("TALLA01"));
                insertCommand.Parameters["@size02"].Value = DbValue(reader.GetStringOrEmpty("TALLA02"));
                insertCommand.Parameters["@size03"].Value = DbValue(reader.GetStringOrEmpty("TALLA03"));
                insertCommand.Parameters["@size04"].Value = DbValue(reader.GetStringOrEmpty("TALLA04"));
                insertCommand.Parameters["@size05"].Value = DbValue(reader.GetStringOrEmpty("TALLA05"));
                insertCommand.Parameters["@size06"].Value = DbValue(reader.GetStringOrEmpty("TALLA06"));
                insertCommand.Parameters["@size07"].Value = DbValue(reader.GetStringOrEmpty("TALLA07"));
                insertCommand.Parameters["@size08"].Value = DbValue(reader.GetStringOrEmpty("TALLA08"));
                insertCommand.Parameters["@size09"].Value = DbValue(reader.GetStringOrEmpty("TALLA09"));
                insertCommand.Parameters["@size10"].Value = DbValue(reader.GetStringOrEmpty("TALLA10"));
                insertCommand.Parameters["@packagingPrice"].Value = reader.GetDecimalOrDefault("NPACK");
                await insertCommand.ExecuteNonQueryAsync(cancellationToken);

                modelIds[identityKey] = recordId;
                mappings.Add(new LegacySyncMappingRecord
                {
                    LegacySourceSystem = "legacy",
                    LegacyCenterCode = centerCode,
                    LegacyDocumentType = "MODEL",
                    LegacyDocumentNumber = $"{code}|{series}|{season}|{clientCode}",
                    TargetEntityName = "article_model",
                    TargetEntityId = recordId
                });
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
                    Stage = "models",
                    LegacyEntityKey = identityKey,
                    ErrorMessage = exception.Message,
                    Payload = string.Empty
                });
            }
        }

        return result;
    }

    private static async Task<TableImportResult> CopyScandalloRowsAsync(
        MySqlConnection legacyConnection,
        MySqlConnection saasConnection,
        MySqlTransaction transaction,
        LegacySyncModuleContext context,
        IReadOnlyDictionary<string, string> modelIds,
        List<LegacySyncErrorRecord> errors,
        CancellationToken cancellationToken)
    {
        var columns = await LoadLegacyColumnsAsync(legacyConnection, "MODELSESCANDALLO", cancellationToken);
        if (columns.Count == 0)
        {
            return new TableImportResult();
        }

        EnsureRequiredColumns("MODELSESCANDALLO", columns, "MODEL", "SERIE", "TEMPORADA", "CLIENT", "CENTRO");
        var result = new TableImportResult();
        var lineNumbers = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        await using var readCommand = legacyConnection.CreateCommand();
        readCommand.CommandText =
            $"""
            SELECT `MODEL` AS MODEL,
                   `SERIE` AS SERIE,
                   `TEMPORADA` AS TEMPORADA,
                   `CLIENT` AS CLIENT,
                   `CENTRO` AS CENTRO,
                   {LegacyColumn(columns, "TITULO", "''")} AS TITULO,
                   {LegacyColumn(columns, "TEIXIT", "''")} AS TEIXIT,
                   {LegacyColumn(columns, "CONSUM", "0")} AS CONSUM,
                   {LegacyColumn(columns, "PREU", "0")} AS PREU,
                   {LegacyColumn(columns, "COST", "0")} AS COST
            FROM `MODELSESCANDALLO`
            WHERE `CENTRO` = @centerCode
            ORDER BY `MODEL`, `TEMPORADA`, `SERIE`, `CLIENT`, `TITULO`, `TEIXIT`;
            """;
        readCommand.Parameters.AddWithValue("@centerCode", context.CompanyLegacyCenterCode);
        readCommand.CommandTimeout = 300;

        await using var insertCommand = saasConnection.CreateCommand();
        insertCommand.Transaction = transaction;
        insertCommand.CommandText =
            """
            INSERT INTO article_model_scandallo (model_id, line_number, TITULO, TEIXIT, CONSUM, PREU, COST)
            VALUES (@modelId, @lineNumber, @title, @fabricCode, @consumption, @unitPrice, @costPrice);
            """;
        insertCommand.Parameters.Add("@modelId", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@lineNumber", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@title", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@fabricCode", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@consumption", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@unitPrice", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@costPrice", MySqlDbType.Decimal);

        await using var reader = await readCommand.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var identityKey = BuildIdentityKey(
                reader.GetStringOrEmpty("CENTRO"),
                reader.GetStringOrEmpty("MODEL"),
                reader.GetStringOrEmpty("SERIE"),
                reader.GetStringOrEmpty("TEMPORADA"),
                reader.GetInt32OrDefault("CLIENT"));

            if (!modelIds.TryGetValue(identityKey, out var modelId))
            {
                result.SkippedRows++;
                continue;
            }

            var title = reader.GetStringOrEmpty("TITULO").Trim();
            var fabricCode = reader.GetStringOrEmpty("TEIXIT").Trim().ToUpperInvariant();
            var consumption = reader.GetDecimalOrDefault("CONSUM");
            var unitPrice = reader.GetDecimalOrDefault("PREU");
            var costPrice = reader.GetDecimalOrDefault("COST");
            if (string.IsNullOrWhiteSpace(title) && string.IsNullOrWhiteSpace(fabricCode) && consumption == 0m && unitPrice == 0m && costPrice == 0m)
            {
                result.SkippedRows++;
                continue;
            }

            try
            {
                insertCommand.Parameters["@modelId"].Value = modelId;
                insertCommand.Parameters["@lineNumber"].Value = NextLineNumber(lineNumbers, identityKey);
                insertCommand.Parameters["@title"].Value = DbValue(title);
                insertCommand.Parameters["@fabricCode"].Value = DbValue(fabricCode);
                insertCommand.Parameters["@consumption"].Value = consumption;
                insertCommand.Parameters["@unitPrice"].Value = unitPrice;
                insertCommand.Parameters["@costPrice"].Value = costPrice;
                await insertCommand.ExecuteNonQueryAsync(cancellationToken);
                result.ImportedRows++;
            }
            catch (Exception exception)
            {
                result.SkippedRows++;
                errors.Add(new LegacySyncErrorRecord
                {
                    Stage = "modelsescandallo",
                    LegacyEntityKey = identityKey,
                    ErrorMessage = exception.Message,
                    Payload = title
                });
            }
        }

        return result;
    }

    private static async Task<TableImportResult> CopyColorRowsAsync(
        MySqlConnection legacyConnection,
        MySqlConnection saasConnection,
        MySqlTransaction transaction,
        LegacySyncModuleContext context,
        IReadOnlyDictionary<string, string> modelIds,
        List<LegacySyncErrorRecord> errors,
        CancellationToken cancellationToken)
    {
        var columns = await LoadLegacyColumnsAsync(legacyConnection, "MODCOL", cancellationToken);
        if (columns.Count == 0)
        {
            return new TableImportResult();
        }

        EnsureRequiredColumns("MODCOL", columns, "MODEL", "SERIE", "TEMPORADA", "CLIENT", "CENTRO");
        var result = new TableImportResult();
        var lineNumbers = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        await using var readCommand = legacyConnection.CreateCommand();
        readCommand.CommandText =
            $"""
            SELECT `MODEL` AS MODEL,
                   `SERIE` AS SERIE,
                   `TEMPORADA` AS TEMPORADA,
                   `CLIENT` AS CLIENT,
                   `CENTRO` AS CENTRO,
                   {LegacyColumn(columns, "MODCOL", "''")} AS MODCOL,
                   {LegacyColumn(columns, "TITULO", "''")} AS TITULO,
                   {LegacyColumn(columns, "COLTITULO", "''")} AS COLTITULO
            FROM `MODCOL`
            WHERE `CENTRO` = @centerCode
            ORDER BY `MODEL`, `TEMPORADA`, `SERIE`, `CLIENT`, `MODCOL`, `TITULO`;
            """;
        readCommand.Parameters.AddWithValue("@centerCode", context.CompanyLegacyCenterCode);
        readCommand.CommandTimeout = 300;

        await using var insertCommand = saasConnection.CreateCommand();
        insertCommand.Transaction = transaction;
        insertCommand.CommandText =
            """
            INSERT INTO article_model_colors (model_id, line_number, MODCOL, TITULO, COLTITULO)
            VALUES (@modelId, @lineNumber, @modelColorCode, @title, @colorTitle);
            """;
        insertCommand.Parameters.Add("@modelId", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@lineNumber", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@modelColorCode", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@title", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@colorTitle", MySqlDbType.VarChar);

        await using var reader = await readCommand.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var identityKey = BuildIdentityKey(
                reader.GetStringOrEmpty("CENTRO"),
                reader.GetStringOrEmpty("MODEL"),
                reader.GetStringOrEmpty("SERIE"),
                reader.GetStringOrEmpty("TEMPORADA"),
                reader.GetInt32OrDefault("CLIENT"));

            if (!modelIds.TryGetValue(identityKey, out var modelId))
            {
                result.SkippedRows++;
                continue;
            }

            var modelColorCode = reader.GetStringOrEmpty("MODCOL").Trim().ToUpperInvariant();
            var title = reader.GetStringOrEmpty("TITULO").Trim();
            var colorTitle = reader.GetStringOrEmpty("COLTITULO").Trim();
            if (string.IsNullOrWhiteSpace(modelColorCode) && string.IsNullOrWhiteSpace(title) && string.IsNullOrWhiteSpace(colorTitle))
            {
                result.SkippedRows++;
                continue;
            }

            try
            {
                insertCommand.Parameters["@modelId"].Value = modelId;
                insertCommand.Parameters["@lineNumber"].Value = NextLineNumber(lineNumbers, identityKey);
                insertCommand.Parameters["@modelColorCode"].Value = DbValue(modelColorCode);
                insertCommand.Parameters["@title"].Value = DbValue(title);
                insertCommand.Parameters["@colorTitle"].Value = DbValue(colorTitle);
                await insertCommand.ExecuteNonQueryAsync(cancellationToken);
                result.ImportedRows++;
            }
            catch (Exception exception)
            {
                result.SkippedRows++;
                errors.Add(new LegacySyncErrorRecord
                {
                    Stage = "modcol",
                    LegacyEntityKey = identityKey,
                    ErrorMessage = exception.Message,
                    Payload = modelColorCode
                });
            }
        }

        return result;
    }

    private static async Task<TableImportResult> CopyFornituraRowsAsync(
        MySqlConnection legacyConnection,
        MySqlConnection saasConnection,
        MySqlTransaction transaction,
        LegacySyncModuleContext context,
        IReadOnlyDictionary<string, string> modelIds,
        List<LegacySyncErrorRecord> errors,
        CancellationToken cancellationToken)
    {
        var columns = await LoadLegacyColumnsAsync(legacyConnection, "MFORNI", cancellationToken);
        if (columns.Count == 0)
        {
            return new TableImportResult();
        }

        EnsureRequiredColumns("MFORNI", columns, "MODEL", "SERIE", "TEMPORADA", "CLIENT", "CENTRO");
        var result = new TableImportResult();
        var lineNumbers = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        await using var readCommand = legacyConnection.CreateCommand();
        readCommand.CommandText =
            $"""
            SELECT `MODEL` AS MODEL,
                   `SERIE` AS SERIE,
                   `TEMPORADA` AS TEMPORADA,
                   `CLIENT` AS CLIENT,
                   `CENTRO` AS CENTRO,
                   {LegacyColumn(columns, "NLINEA", "0")} AS NLINEA,
                   {LegacyColumn(columns, "FORNI", "''")} AS FORNI,
                   {LegacyColumn(columns, "MEDIDA", "''")} AS MEDIDA,
                   {LegacyColumn(columns, "UNITATS", "0")} AS UNITATS,
                   {LegacyColumn(columns, "PREU", "0")} AS PREU,
                   {LegacyColumn(columns, "IMPORT", "0")} AS IMPORT
            FROM `MFORNI`
            WHERE `CENTRO` = @centerCode
            ORDER BY `MODEL`, `TEMPORADA`, `SERIE`, `CLIENT`, `NLINEA`, `FORNI`;
            """;
        readCommand.Parameters.AddWithValue("@centerCode", context.CompanyLegacyCenterCode);
        readCommand.CommandTimeout = 300;

        await using var insertCommand = saasConnection.CreateCommand();
        insertCommand.Transaction = transaction;
        insertCommand.CommandText =
            """
            INSERT INTO article_model_fornituras (model_id, line_number, FORNI, MEDIDA, UNITATS, PREU, IMPORT)
            VALUES (@modelId, @lineNumber, @fornituraCode, @measure, @units, @unitPrice, @importAmount);
            """;
        insertCommand.Parameters.Add("@modelId", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@lineNumber", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@fornituraCode", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@measure", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@units", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@unitPrice", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@importAmount", MySqlDbType.Decimal);

        await using var reader = await readCommand.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var identityKey = BuildIdentityKey(
                reader.GetStringOrEmpty("CENTRO"),
                reader.GetStringOrEmpty("MODEL"),
                reader.GetStringOrEmpty("SERIE"),
                reader.GetStringOrEmpty("TEMPORADA"),
                reader.GetInt32OrDefault("CLIENT"));

            if (!modelIds.TryGetValue(identityKey, out var modelId))
            {
                result.SkippedRows++;
                continue;
            }

            var fornituraCode = reader.GetStringOrEmpty("FORNI").Trim().ToUpperInvariant();
            var measure = reader.GetStringOrEmpty("MEDIDA").Trim();
            var units = reader.GetDecimalOrDefault("UNITATS");
            var unitPrice = reader.GetDecimalOrDefault("PREU");
            var importAmount = reader.GetDecimalOrDefault("IMPORT");
            if (string.IsNullOrWhiteSpace(fornituraCode) && string.IsNullOrWhiteSpace(measure) && units == 0m && unitPrice == 0m && importAmount == 0m)
            {
                result.SkippedRows++;
                continue;
            }

            try
            {
                var preferredLine = reader.GetInt32OrDefault("NLINEA");
                insertCommand.Parameters["@modelId"].Value = modelId;
                insertCommand.Parameters["@lineNumber"].Value = preferredLine > 0 ? preferredLine : NextLineNumber(lineNumbers, identityKey);
                insertCommand.Parameters["@fornituraCode"].Value = DbValue(fornituraCode);
                insertCommand.Parameters["@measure"].Value = DbValue(measure);
                insertCommand.Parameters["@units"].Value = units;
                insertCommand.Parameters["@unitPrice"].Value = unitPrice;
                insertCommand.Parameters["@importAmount"].Value = importAmount != 0m ? importAmount : Math.Round(units * unitPrice, 4, MidpointRounding.AwayFromZero);
                await insertCommand.ExecuteNonQueryAsync(cancellationToken);
                result.ImportedRows++;
            }
            catch (Exception exception)
            {
                result.SkippedRows++;
                errors.Add(new LegacySyncErrorRecord
                {
                    Stage = "mforni",
                    LegacyEntityKey = identityKey,
                    ErrorMessage = exception.Message,
                    Payload = fornituraCode
                });
            }
        }

        return result;
    }

    private static async Task<TableImportResult> CopyStockRowsAsync(
        MySqlConnection legacyConnection,
        MySqlConnection saasConnection,
        MySqlTransaction transaction,
        LegacySyncModuleContext context,
        IReadOnlyDictionary<string, string> modelIds,
        List<LegacySyncErrorRecord> errors,
        CancellationToken cancellationToken)
    {
        var columns = await LoadLegacyColumnsAsync(legacyConnection, "MODSTK", cancellationToken);
        if (columns.Count == 0)
        {
            return new TableImportResult();
        }

        EnsureRequiredColumns("MODSTK", columns, "MODEL", "SERIE", "TEMPORADA", "CLIENT", "CENTRO");
        var result = new TableImportResult();
        var lineNumbers = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        await using var readCommand = legacyConnection.CreateCommand();
        readCommand.CommandText =
            $"""
            SELECT `MODEL` AS MODEL,
                   `SERIE` AS SERIE,
                   `TEMPORADA` AS TEMPORADA,
                   `CLIENT` AS CLIENT,
                   `CENTRO` AS CENTRO,
                   {LegacyColumn(columns, "COLOR", "''")} AS COLOR,
                   {LegacyColumn(columns, "TALLA", "''")} AS TALLA,
                   {LegacyColumn(columns, "TALLA01", "0")} AS TALLA01,
                   {LegacyColumn(columns, "TALLA02", "0")} AS TALLA02,
                   {LegacyColumn(columns, "TALLA03", "0")} AS TALLA03,
                   {LegacyColumn(columns, "TALLA04", "0")} AS TALLA04,
                   {LegacyColumn(columns, "TALLA05", "0")} AS TALLA05,
                   {LegacyColumn(columns, "TALLA06", "0")} AS TALLA06,
                   {LegacyColumn(columns, "TALLA07", "0")} AS TALLA07,
                   {LegacyColumn(columns, "TALLA08", "0")} AS TALLA08,
                   {LegacyColumn(columns, "TALLA09", "0")} AS TALLA09,
                   {LegacyColumn(columns, "TALLA10", "0")} AS TALLA10
            FROM `MODSTK`
            WHERE `CENTRO` = @centerCode
            ORDER BY `MODEL`, `TEMPORADA`, `SERIE`, `CLIENT`, `COLOR`, `TALLA`;
            """;
        readCommand.Parameters.AddWithValue("@centerCode", context.CompanyLegacyCenterCode);
        readCommand.CommandTimeout = 300;

        await using var insertCommand = saasConnection.CreateCommand();
        insertCommand.Transaction = transaction;
        insertCommand.CommandText =
            """
            INSERT INTO article_model_stock (
                model_id, line_number, COLOR, TALLA, TALLA01, TALLA02, TALLA03, TALLA04, TALLA05,
                TALLA06, TALLA07, TALLA08, TALLA09, TALLA10)
            VALUES (
                @modelId, @lineNumber, @color, @sizeText, @sizeQuantity01, @sizeQuantity02, @sizeQuantity03, @sizeQuantity04, @sizeQuantity05,
                @sizeQuantity06, @sizeQuantity07, @sizeQuantity08, @sizeQuantity09, @sizeQuantity10);
            """;
        insertCommand.Parameters.Add("@modelId", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@lineNumber", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@color", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@sizeText", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@sizeQuantity01", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@sizeQuantity02", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@sizeQuantity03", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@sizeQuantity04", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@sizeQuantity05", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@sizeQuantity06", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@sizeQuantity07", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@sizeQuantity08", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@sizeQuantity09", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@sizeQuantity10", MySqlDbType.Decimal);

        await using var reader = await readCommand.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var identityKey = BuildIdentityKey(
                reader.GetStringOrEmpty("CENTRO"),
                reader.GetStringOrEmpty("MODEL"),
                reader.GetStringOrEmpty("SERIE"),
                reader.GetStringOrEmpty("TEMPORADA"),
                reader.GetInt32OrDefault("CLIENT"));

            if (!modelIds.TryGetValue(identityKey, out var modelId))
            {
                result.SkippedRows++;
                continue;
            }

            var color = reader.GetStringOrEmpty("COLOR").Trim();
            var sizeText = reader.GetStringOrEmpty("TALLA").Trim();
            var sizeQuantity01 = reader.GetDecimalOrDefault("TALLA01");
            var sizeQuantity02 = reader.GetDecimalOrDefault("TALLA02");
            var sizeQuantity03 = reader.GetDecimalOrDefault("TALLA03");
            var sizeQuantity04 = reader.GetDecimalOrDefault("TALLA04");
            var sizeQuantity05 = reader.GetDecimalOrDefault("TALLA05");
            var sizeQuantity06 = reader.GetDecimalOrDefault("TALLA06");
            var sizeQuantity07 = reader.GetDecimalOrDefault("TALLA07");
            var sizeQuantity08 = reader.GetDecimalOrDefault("TALLA08");
            var sizeQuantity09 = reader.GetDecimalOrDefault("TALLA09");
            var sizeQuantity10 = reader.GetDecimalOrDefault("TALLA10");
            if (string.IsNullOrWhiteSpace(color) &&
                string.IsNullOrWhiteSpace(sizeText) &&
                sizeQuantity01 == 0m &&
                sizeQuantity02 == 0m &&
                sizeQuantity03 == 0m &&
                sizeQuantity04 == 0m &&
                sizeQuantity05 == 0m &&
                sizeQuantity06 == 0m &&
                sizeQuantity07 == 0m &&
                sizeQuantity08 == 0m &&
                sizeQuantity09 == 0m &&
                sizeQuantity10 == 0m)
            {
                result.SkippedRows++;
                continue;
            }

            try
            {
                insertCommand.Parameters["@modelId"].Value = modelId;
                insertCommand.Parameters["@lineNumber"].Value = NextLineNumber(lineNumbers, identityKey);
                insertCommand.Parameters["@color"].Value = DbValue(color);
                insertCommand.Parameters["@sizeText"].Value = DbValue(sizeText);
                insertCommand.Parameters["@sizeQuantity01"].Value = sizeQuantity01;
                insertCommand.Parameters["@sizeQuantity02"].Value = sizeQuantity02;
                insertCommand.Parameters["@sizeQuantity03"].Value = sizeQuantity03;
                insertCommand.Parameters["@sizeQuantity04"].Value = sizeQuantity04;
                insertCommand.Parameters["@sizeQuantity05"].Value = sizeQuantity05;
                insertCommand.Parameters["@sizeQuantity06"].Value = sizeQuantity06;
                insertCommand.Parameters["@sizeQuantity07"].Value = sizeQuantity07;
                insertCommand.Parameters["@sizeQuantity08"].Value = sizeQuantity08;
                insertCommand.Parameters["@sizeQuantity09"].Value = sizeQuantity09;
                insertCommand.Parameters["@sizeQuantity10"].Value = sizeQuantity10;
                await insertCommand.ExecuteNonQueryAsync(cancellationToken);
                result.ImportedRows++;
            }
            catch (Exception exception)
            {
                result.SkippedRows++;
                errors.Add(new LegacySyncErrorRecord
                {
                    Stage = "modstk",
                    LegacyEntityKey = identityKey,
                    ErrorMessage = exception.Message,
                    Payload = color
                });
            }
        }

        return result;
    }

    private static void AddHeaderInsertParameters(MySqlCommand command, LegacySyncModuleContext context, DateTime now)
    {
        command.Parameters.Add("@recordId", MySqlDbType.VarChar);
        command.Parameters.AddWithValue("@tenantId", context.TenantId.ToString());
        command.Parameters.AddWithValue("@companyId", context.CompanyId.ToString());
        command.Parameters.Add("@centerCode", MySqlDbType.VarChar);
        command.Parameters.Add("@code", MySqlDbType.VarChar);
        command.Parameters.Add("@series", MySqlDbType.VarChar);
        command.Parameters.Add("@clientCode", MySqlDbType.Int32);
        command.Parameters.Add("@clientName", MySqlDbType.VarChar);
        command.Parameters.Add("@season", MySqlDbType.VarChar);
        command.Parameters.Add("@description", MySqlDbType.VarChar);
        command.Parameters.Add("@modelCode", MySqlDbType.VarChar);
        command.Parameters.Add("@fabricCode", MySqlDbType.VarChar);
        command.Parameters.Add("@fabricDescription", MySqlDbType.VarChar);
        command.Parameters.Add("@supplierCode", MySqlDbType.Int32);
        command.Parameters.Add("@supplierName", MySqlDbType.VarChar);
        command.Parameters.Add("@widthText", MySqlDbType.VarChar);
        command.Parameters.Add("@dyeingWorkshopCode", MySqlDbType.Int32);
        command.Parameters.Add("@dyeingWorkshopName", MySqlDbType.VarChar);
        command.Parameters.Add("@finishingWorkshopCode", MySqlDbType.Int32);
        command.Parameters.Add("@finishingWorkshopName", MySqlDbType.VarChar);
        command.Parameters.Add("@printingWorkshopCode", MySqlDbType.Int32);
        command.Parameters.Add("@printingWorkshopName", MySqlDbType.VarChar);
        command.Parameters.Add("@tailoringWorkshopCode", MySqlDbType.Int32);
        command.Parameters.Add("@tailoringWorkshopName", MySqlDbType.VarChar);
        command.Parameters.Add("@yield", MySqlDbType.Decimal);
        command.Parameters.Add("@fornituraSummary", MySqlDbType.VarChar);
        command.Parameters.Add("@printingCode1", MySqlDbType.VarChar);
        command.Parameters.Add("@printingCode2", MySqlDbType.VarChar);
        command.Parameters.Add("@printingUnits1", MySqlDbType.Decimal);
        command.Parameters.Add("@printingUnits2", MySqlDbType.Decimal);
        command.Parameters.Add("@tailoringPrice", MySqlDbType.Decimal);
        command.Parameters.Add("@platePrice", MySqlDbType.Decimal);
        command.Parameters.Add("@reviewPrice", MySqlDbType.Decimal);
        command.Parameters.Add("@transferQuantity", MySqlDbType.Decimal);
        command.Parameters.Add("@transferPrice", MySqlDbType.Decimal);
        command.Parameters.Add("@flockedQuantity", MySqlDbType.Decimal);
        command.Parameters.Add("@flockedPrice", MySqlDbType.Decimal);
        command.Parameters.Add("@embroideredQuantity", MySqlDbType.Decimal);
        command.Parameters.Add("@embroideredPrice", MySqlDbType.Decimal);
        command.Parameters.Add("@printingPrice", MySqlDbType.Decimal);
        command.Parameters.Add("@dyeingPrice", MySqlDbType.Decimal);
        command.Parameters.Add("@finishingPrice", MySqlDbType.Decimal);
        command.Parameters.Add("@fornituraPrice", MySqlDbType.Decimal);
        command.Parameters.Add("@manipulationPrice", MySqlDbType.Decimal);
        command.Parameters.Add("@costPrice", MySqlDbType.Decimal);
        command.Parameters.Add("@marginPercent", MySqlDbType.Decimal);
        command.Parameters.Add("@salePrice", MySqlDbType.Decimal);
        command.Parameters.Add("@finalSalePrice", MySqlDbType.Decimal);
        command.Parameters.Add("@notes", MySqlDbType.VarChar);
        command.Parameters.Add("@vatCode", MySqlDbType.VarChar);
        command.Parameters.Add("@size01", MySqlDbType.VarChar);
        command.Parameters.Add("@size02", MySqlDbType.VarChar);
        command.Parameters.Add("@size03", MySqlDbType.VarChar);
        command.Parameters.Add("@size04", MySqlDbType.VarChar);
        command.Parameters.Add("@size05", MySqlDbType.VarChar);
        command.Parameters.Add("@size06", MySqlDbType.VarChar);
        command.Parameters.Add("@size07", MySqlDbType.VarChar);
        command.Parameters.Add("@size08", MySqlDbType.VarChar);
        command.Parameters.Add("@size09", MySqlDbType.VarChar);
        command.Parameters.Add("@size10", MySqlDbType.VarChar);
        command.Parameters.Add("@packagingPrice", MySqlDbType.Decimal);
        command.Parameters.AddWithValue("@syncedUtc", now);
        command.Parameters.AddWithValue("@createdUtc", now);
        command.Parameters.AddWithValue("@updatedUtc", now);
    }

    private static int NextLineNumber(IDictionary<string, int> lineNumbers, string identityKey)
    {
        if (!lineNumbers.TryGetValue(identityKey, out var current))
        {
            current = 0;
        }

        current++;
        lineNumbers[identityKey] = current;
        return current;
    }

    private static string BuildIdentityKey(string centerCode, string code, string series, string season, int clientCode)
        => $"{centerCode.Trim().ToUpperInvariant()}|{code.Trim().ToUpperInvariant()}|{series.Trim().ToUpperInvariant()}|{season.Trim().ToUpperInvariant()}|{clientCode}";

    private static async Task<HashSet<string>> LoadLegacyColumnsAsync(
        MySqlConnection connection,
        string tableName,
        CancellationToken cancellationToken)
    {
        var columns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        try
        {
            await using var command = connection.CreateCommand();
            command.CommandText = $"SHOW COLUMNS FROM `{tableName}`;";
            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                columns.Add(reader.GetStringOrEmpty("Field"));
            }
        }
        catch (MySqlException exception) when (exception.Number == 1146)
        {
            return columns;
        }

        return columns;
    }

    private static void EnsureRequiredColumns(string tableName, ISet<string> columns, params string[] requiredColumns)
    {
        var missing = requiredColumns.Where(column => !columns.Contains(column)).ToArray();
        if (missing.Length == 0)
        {
            return;
        }

        throw new InvalidOperationException($"La tabla legacy {tableName} no contiene las columnas obligatorias: {string.Join(", ", missing)}.");
    }

    private static string LegacyColumn(ISet<string> columns, string columnName, string fallbackSql)
        => columns.Contains(columnName)
            ? $"COALESCE(`{columnName}`, {fallbackSql})"
            : fallbackSql;

    private static object DbValue(string? value, string? fallback = null)
    {
        var effectiveValue = string.IsNullOrWhiteSpace(value) ? fallback : value;
        return string.IsNullOrWhiteSpace(effectiveValue) ? DBNull.Value : effectiveValue.Trim();
    }

    private sealed class TableImportResult
    {
        public int ImportedRows { get; set; }
        public int SkippedRows { get; set; }
    }
}
