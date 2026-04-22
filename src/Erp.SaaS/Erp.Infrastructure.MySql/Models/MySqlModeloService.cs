using Erp.Application.Auditing;
using Erp.Application.Companies;
using Erp.Application.Contexts;
using Erp.Application.Models;
using Erp.Domain.Common;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Support;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Models;

public sealed class MySqlModeloService : IModeloQueries, IModeloService
{
    private readonly MySqlConnectionFactory _connectionFactory;
    private readonly IAuditLogService _auditLogService;
    private readonly ICompanyAccessService _companyAccessService;
    private readonly ICurrentUserContext _currentUserContext;
    private readonly ITenantContext _tenantContext;
    private readonly IActiveCompanyContext _activeCompanyContext;

    public MySqlModeloService(
        MySqlConnectionFactory connectionFactory,
        IAuditLogService auditLogService,
        ICompanyAccessService companyAccessService,
        ICurrentUserContext currentUserContext,
        ITenantContext tenantContext,
        IActiveCompanyContext activeCompanyContext)
    {
        _connectionFactory = connectionFactory;
        _auditLogService = auditLogService;
        _companyAccessService = companyAccessService;
        _currentUserContext = currentUserContext;
        _tenantContext = tenantContext;
        _activeCompanyContext = activeCompanyContext;
    }

    public async Task<ModeloSearchResultDto> SearchAsync(Guid tenantId, Guid companyId, ModeloFilter filter, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return new ModeloSearchResultDto();
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);
        var centerCode = await ResolveCompanyCenterCodeAsync(tenantId, companyId, cancellationToken);
        var search = filter.Search?.Trim() ?? string.Empty;
        var likeSearch = $"%{search}%";
        var season = filter.Season?.Trim() ?? string.Empty;
        var series = filter.Series?.Trim() ?? string.Empty;
        var pageSize = Math.Clamp(filter.PageSize, 10, 200);
        var page = Math.Max(filter.Page, 1);
        var offset = (page - 1) * pageSize;

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);

        await using var countCommand = connection.CreateCommand();
        countCommand.CommandText =
            """
            SELECT COUNT(*)
            FROM article_models
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND CENTRO = @centerCode
              AND is_deleted = 0
              AND (@season = '' OR TEMPORADA = @season)
              AND (@series = '' OR SERIE = @series)
              AND (
                    @search = ''
                    OR CODI LIKE @likeSearch
                    OR DESCRI LIKE @likeSearch
                    OR COALESCE(NOMCLIENT, '') LIKE @likeSearch
                    OR COALESCE(DESCRITEIXIT, '') LIKE @likeSearch
                    OR COALESCE(TEIXIT, '') LIKE @likeSearch
                    OR COALESCE(CODIMODEL, '') LIKE @likeSearch
                  );
            """;
        countCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        countCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
        countCommand.Parameters.AddWithValue("@centerCode", centerCode);
        countCommand.Parameters.AddWithValue("@season", season);
        countCommand.Parameters.AddWithValue("@series", series);
        countCommand.Parameters.AddWithValue("@search", search);
        countCommand.Parameters.AddWithValue("@likeSearch", likeSearch);

        var totalCount = Convert.ToInt32(await countCommand.ExecuteScalarAsync(cancellationToken));
        if (totalCount == 0)
        {
            return new ModeloSearchResultDto { TotalCount = 0 };
        }

        var items = new List<ModeloListItemDto>();
        await using var command = connection.CreateCommand();
        command.CommandText =
            $"""
            SELECT record_id,
                   CODI,
                   SERIE,
                   TEMPORADA,
                   CLIENT,
                   COALESCE(NOMCLIENT, '') AS NOMCLIENT,
                   COALESCE(DESCRI, '') AS DESCRI,
                   COALESCE(TEIXIT, '') AS TEIXIT,
                   COALESCE(DESCRITEIXIT, '') AS DESCRITEIXIT,
                   COALESCE(COST, 0) AS COST,
                   COALESCE(VENDA, 0) AS VENDA,
                   COALESCE(VENDAFINAL, 0) AS VENDAFINAL,
                   origin,
                   (SELECT COUNT(*) FROM article_model_colors colors WHERE colors.model_id = article_models.record_id) AS COLORS_COUNT,
                   (SELECT COUNT(*) FROM article_model_scandallo scandallo WHERE scandallo.model_id = article_models.record_id) AS SCANDALLO_COUNT,
                   (SELECT COUNT(*) FROM article_model_fornituras forni WHERE forni.model_id = article_models.record_id) AS FORNITURAS_COUNT
            FROM article_models
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND CENTRO = @centerCode
              AND is_deleted = 0
              AND (@season = '' OR TEMPORADA = @season)
              AND (@series = '' OR SERIE = @series)
              AND (
                    @search = ''
                    OR CODI LIKE @likeSearch
                    OR DESCRI LIKE @likeSearch
                    OR COALESCE(NOMCLIENT, '') LIKE @likeSearch
                    OR COALESCE(DESCRITEIXIT, '') LIKE @likeSearch
                    OR COALESCE(TEIXIT, '') LIKE @likeSearch
                    OR COALESCE(CODIMODEL, '') LIKE @likeSearch
                  )
            {BuildSearchOrderByClause(filter)}
            LIMIT @limit OFFSET @offset;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@season", season);
        command.Parameters.AddWithValue("@series", series);
        command.Parameters.AddWithValue("@search", search);
        command.Parameters.AddWithValue("@likeSearch", likeSearch);
        command.Parameters.AddWithValue("@limit", pageSize);
        command.Parameters.AddWithValue("@offset", offset);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new ModeloListItemDto
            {
                Id = reader.GetGuid("record_id"),
                Code = reader.GetStringOrEmpty("CODI"),
                Series = reader.GetStringOrEmpty("SERIE"),
                Season = reader.GetStringOrEmpty("TEMPORADA"),
                ClientCode = reader.GetInt32OrDefault("CLIENT"),
                ClientName = reader.GetStringOrEmpty("NOMCLIENT"),
                Description = reader.GetStringOrEmpty("DESCRI"),
                FabricCode = reader.GetStringOrEmpty("TEIXIT"),
                FabricDescription = reader.GetStringOrEmpty("DESCRITEIXIT"),
                CostPrice = reader.GetDecimalOrDefault("COST"),
                SalePrice = reader.GetDecimalOrDefault("VENDA"),
                FinalSalePrice = reader.GetDecimalOrDefault("VENDAFINAL"),
                Origin = reader.GetStringOrEmpty("origin"),
                ColorsCount = reader.GetInt32OrDefault("COLORS_COUNT"),
                ScandalloLinesCount = reader.GetInt32OrDefault("SCANDALLO_COUNT"),
                ForniturasCount = reader.GetInt32OrDefault("FORNITURAS_COUNT")
            });
        }

        return new ModeloSearchResultDto
        {
            Items = items,
            TotalCount = totalCount
        };
    }

    public async Task<ModeloDetailDto?> GetByIdAsync(Guid tenantId, Guid companyId, Guid id, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return null;
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);
        var centerCode = await ResolveCompanyCenterCodeAsync(tenantId, companyId, cancellationToken);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT record_id,
                   CENTRO,
                   CODI,
                   SERIE,
                   TEMPORADA,
                   CLIENT,
                   COALESCE(NOMCLIENT, '') AS NOMCLIENT,
                   COALESCE(DESCRI, '') AS DESCRI,
                   COALESCE(CODIMODEL, '') AS CODIMODEL,
                   COALESCE(TEIXIT, '') AS TEIXIT,
                   COALESCE(DESCRITEIXIT, '') AS DESCRITEIXIT,
                   COALESCE(PROVE, 0) AS PROVE,
                   COALESCE(NOMPROVE, '') AS NOMPROVE,
                   COALESCE(AMPLE, '') AS AMPLE,
                   COALESCE(TINT, 0) AS TINT,
                   COALESCE(NOMTINT, '') AS NOMTINT,
                   COALESCE(ACA, 0) AS ACA,
                   COALESCE(NOMACA, '') AS NOMACA,
                   COALESCE(ESTAM, 0) AS ESTAM,
                   COALESCE(NOMESTAM, '') AS NOMESTAM,
                   COALESCE(CONFEC, 0) AS CONFEC,
                   COALESCE(NOMCONFEC, '') AS NOMCONFEC,
                   COALESCE(RENDIM, 0) AS RENDIM,
                   COALESCE(FORNITURA, '') AS FORNITURA,
                   COALESCE(CESTAM, '') AS CESTAM,
                   COALESCE(CESTAM2, '') AS CESTAM2,
                   COALESCE(NESTAM, 0) AS NESTAM,
                   COALESCE(NESTAM2, 0) AS NESTAM2,
                   COALESCE(NCONFEC, 0) AS NCONFEC,
                   COALESCE(NPLANXA, 0) AS NPLANXA,
                   COALESCE(NREPAS, 0) AS NREPAS,
                   COALESCE(QTRANS, 0) AS QTRANS,
                   COALESCE(NTRANS, 0) AS NTRANS,
                   COALESCE(QFLOCAT, 0) AS QFLOCAT,
                   COALESCE(NFLOCAT, 0) AS NFLOCAT,
                   COALESCE(QBRODAT, 0) AS QBRODAT,
                   COALESCE(NBRODAT, 0) AS NBRODAT,
                   COALESCE(NESTAMP, 0) AS NESTAMP,
                   COALESCE(NTINTP, 0) AS NTINTP,
                   COALESCE(NACAP, 0) AS NACAP,
                   COALESCE(NFORNITURA, 0) AS NFORNITURA,
                   COALESCE(MANIPULACION, 0) AS MANIPULACION,
                   COALESCE(COST, 0) AS COST,
                   COALESCE(MARGE, 0) AS MARGE,
                   COALESCE(VENDA, 0) AS VENDA,
                   COALESCE(VENDAFINAL, 0) AS VENDAFINAL,
                   COALESCE(OBSERV, '') AS OBSERV,
                   COALESCE(IVA, '') AS IVA,
                   COALESCE(TALLA01, '') AS TALLA01,
                   COALESCE(TALLA02, '') AS TALLA02,
                   COALESCE(TALLA03, '') AS TALLA03,
                   COALESCE(TALLA04, '') AS TALLA04,
                   COALESCE(TALLA05, '') AS TALLA05,
                   COALESCE(TALLA06, '') AS TALLA06,
                   COALESCE(TALLA07, '') AS TALLA07,
                   COALESCE(TALLA08, '') AS TALLA08,
                   COALESCE(TALLA09, '') AS TALLA09,
                   COALESCE(TALLA10, '') AS TALLA10,
                   COALESCE(NPACK, 0) AS NPACK,
                   COALESCE(origin, 'local') AS origin
            FROM article_models
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND CENTRO = @centerCode
              AND record_id = @id
              AND is_deleted = 0
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@id", id.ToString());

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        var detail = new ModeloDetailDto
        {
            Id = reader.GetGuid("record_id"),
            CompanyCenterCode = reader.GetStringOrEmpty("CENTRO"),
            Code = reader.GetStringOrEmpty("CODI"),
            Series = reader.GetStringOrEmpty("SERIE"),
            Season = reader.GetStringOrEmpty("TEMPORADA"),
            ClientCode = reader.GetInt32OrDefault("CLIENT"),
            ClientName = reader.GetStringOrEmpty("NOMCLIENT"),
            Description = reader.GetStringOrEmpty("DESCRI"),
            ModelCode = reader.GetStringOrEmpty("CODIMODEL"),
            FabricCode = reader.GetStringOrEmpty("TEIXIT"),
            FabricDescription = reader.GetStringOrEmpty("DESCRITEIXIT"),
            SupplierCode = reader.GetInt32OrDefault("PROVE"),
            SupplierName = reader.GetStringOrEmpty("NOMPROVE"),
            WidthText = reader.GetStringOrEmpty("AMPLE"),
            DyeingWorkshopCode = reader.GetInt32OrDefault("TINT"),
            DyeingWorkshopName = reader.GetStringOrEmpty("NOMTINT"),
            FinishingWorkshopCode = reader.GetInt32OrDefault("ACA"),
            FinishingWorkshopName = reader.GetStringOrEmpty("NOMACA"),
            PrintingWorkshopCode = reader.GetInt32OrDefault("ESTAM"),
            PrintingWorkshopName = reader.GetStringOrEmpty("NOMESTAM"),
            TailoringWorkshopCode = reader.GetInt32OrDefault("CONFEC"),
            TailoringWorkshopName = reader.GetStringOrEmpty("NOMCONFEC"),
            Yield = reader.GetDecimalOrDefault("RENDIM"),
            FornituraSummary = reader.GetStringOrEmpty("FORNITURA"),
            PrintingCode1 = reader.GetStringOrEmpty("CESTAM"),
            PrintingCode2 = reader.GetStringOrEmpty("CESTAM2"),
            PrintingUnits1 = reader.GetDecimalOrDefault("NESTAM"),
            PrintingUnits2 = reader.GetDecimalOrDefault("NESTAM2"),
            TailoringPrice = reader.GetDecimalOrDefault("NCONFEC"),
            PlatePrice = reader.GetDecimalOrDefault("NPLANXA"),
            ReviewPrice = reader.GetDecimalOrDefault("NREPAS"),
            TransferQuantity = reader.GetDecimalOrDefault("QTRANS"),
            TransferPrice = reader.GetDecimalOrDefault("NTRANS"),
            FlockedQuantity = reader.GetDecimalOrDefault("QFLOCAT"),
            FlockedPrice = reader.GetDecimalOrDefault("NFLOCAT"),
            EmbroideredQuantity = reader.GetDecimalOrDefault("QBRODAT"),
            EmbroideredPrice = reader.GetDecimalOrDefault("NBRODAT"),
            PrintingPrice = reader.GetDecimalOrDefault("NESTAMP"),
            DyeingPrice = reader.GetDecimalOrDefault("NTINTP"),
            FinishingPrice = reader.GetDecimalOrDefault("NACAP"),
            FornituraPrice = reader.GetDecimalOrDefault("NFORNITURA"),
            ManipulationPrice = reader.GetDecimalOrDefault("MANIPULACION"),
            CostPrice = reader.GetDecimalOrDefault("COST"),
            MarginPercent = reader.GetDecimalOrDefault("MARGE"),
            SalePrice = reader.GetDecimalOrDefault("VENDA"),
            FinalSalePrice = reader.GetDecimalOrDefault("VENDAFINAL"),
            Notes = reader.GetStringOrEmpty("OBSERV"),
            VatCode = reader.GetStringOrEmpty("IVA"),
            Size01 = reader.GetStringOrEmpty("TALLA01"),
            Size02 = reader.GetStringOrEmpty("TALLA02"),
            Size03 = reader.GetStringOrEmpty("TALLA03"),
            Size04 = reader.GetStringOrEmpty("TALLA04"),
            Size05 = reader.GetStringOrEmpty("TALLA05"),
            Size06 = reader.GetStringOrEmpty("TALLA06"),
            Size07 = reader.GetStringOrEmpty("TALLA07"),
            Size08 = reader.GetStringOrEmpty("TALLA08"),
            Size09 = reader.GetStringOrEmpty("TALLA09"),
            Size10 = reader.GetStringOrEmpty("TALLA10"),
            PackagingPrice = reader.GetDecimalOrDefault("NPACK"),
            Origin = reader.GetStringOrEmpty("origin")
        };

        await reader.CloseAsync();
        detail.ScandalloLines = await LoadScandalloLinesAsync(connection, detail.Id, cancellationToken);
        detail.ColorLines = await LoadColorLinesAsync(connection, detail.Id, cancellationToken);
        detail.FornituraLines = await LoadFornituraLinesAsync(connection, detail.Id, cancellationToken);
        detail.StockLines = await LoadStockLinesAsync(connection, detail.Id, cancellationToken);
        return detail;
    }

    public async Task<Guid> SaveAsync(SaveModeloCommand command, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return Guid.Empty;
        }

        await EnsureCompanyAccessAsync(command.TenantId, command.CompanyId, cancellationToken);
        EnsureTenantWriteAccess();
        NormalizeCommand(command);
        Validate(command);
        var centerCode = await ResolveCompanyCenterCodeAsync(command.TenantId, command.CompanyId, cancellationToken);
        var now = DateTime.UtcNow;
        var id = command.Id ?? Guid.NewGuid();

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        ModeloDetailDto? previous = null;
        if (command.Id.HasValue)
        {
            previous = await GetByIdAsync(command.TenantId, command.CompanyId, command.Id.Value, cancellationToken);
            if (previous is null)
            {
                throw new InvalidOperationException("No se ha encontrado el modelo que intentas modificar.");
            }
        }

        await EnsureUniqueIdentityAsync(connection, transaction, command.TenantId, command.CompanyId, id, centerCode, command, cancellationToken);

        command.FornituraPrice = command.FornituraLines.Sum(line => line.ImportAmount);
        var calculatedCost = CalculateDerivedCost(command);
        if (command.CostPrice == 0m)
        {
            command.CostPrice = calculatedCost;
        }

        if (command.FinalSalePrice == 0m && command.SalePrice != 0m && command.MarginPercent != 0m)
        {
            command.FinalSalePrice = Math.Round(command.SalePrice * (1m + (command.MarginPercent / 100m)), 4, MidpointRounding.AwayFromZero);
        }

        await using (var commandDb = connection.CreateCommand())
        {
            commandDb.Transaction = transaction;
            commandDb.CommandText =
                """
                INSERT INTO article_models (
                    record_id, tenant_id, company_id, CENTRO, CODI, SERIE, CLIENT, NOMCLIENT, TEMPORADA,
                    TEIXIT, DESCRITEIXIT, DESCRI, PROVE, NOMPROVE, AMPLE, TINT, NOMTINT, ACA, NOMACA, ESTAM, NOMESTAM,
                    CONFEC, NOMCONFEC, RENDIM, FORNITURA, CESTAM, CESTAM2, NESTAM, NESTAM2, NCONFEC, NPLANXA, NREPAS,
                    QTRANS, NTRANS, QFLOCAT, NFLOCAT, QBRODAT, NBRODAT, NESTAMP, NTINTP, NACAP, NFORNITURA, MANIPULACION,
                    COST, MARGE, VENDA, VENDAFINAL, OBSERV, IVA, TALLA01, TALLA02, TALLA03, TALLA04, TALLA05, TALLA06,
                    TALLA07, TALLA08, TALLA09, TALLA10, NPACK, CODIMODEL, origin, is_deleted, synced_utc, created_utc, updated_utc)
                VALUES (
                    @id, @tenantId, @companyId, @centerCode, @code, @series, @clientCode, @clientName, @season,
                    @fabricCode, @fabricDescription, @description, @supplierCode, @supplierName, @widthText, @dyeingWorkshopCode, @dyeingWorkshopName,
                    @finishingWorkshopCode, @finishingWorkshopName, @printingWorkshopCode, @printingWorkshopName, @tailoringWorkshopCode, @tailoringWorkshopName,
                    @yield, @fornituraSummary, @printingCode1, @printingCode2, @printingUnits1, @printingUnits2, @tailoringPrice, @platePrice, @reviewPrice,
                    @transferQuantity, @transferPrice, @flockedQuantity, @flockedPrice, @embroideredQuantity, @embroideredPrice, @printingPrice, @dyeingPrice,
                    @finishingPrice, @fornituraPrice, @manipulationPrice, @costPrice, @marginPercent, @salePrice, @finalSalePrice, @notes, @vatCode,
                    @size01, @size02, @size03, @size04, @size05, @size06, @size07, @size08, @size09, @size10,
                    @packagingPrice, @modelCode, @origin, 0, NULL, @createdUtc, @updatedUtc)
                ON DUPLICATE KEY UPDATE
                    NOMCLIENT = VALUES(NOMCLIENT),
                    DESCRI = VALUES(DESCRI),
                    TEIXIT = VALUES(TEIXIT),
                    DESCRITEIXIT = VALUES(DESCRITEIXIT),
                    PROVE = VALUES(PROVE),
                    NOMPROVE = VALUES(NOMPROVE),
                    AMPLE = VALUES(AMPLE),
                    TINT = VALUES(TINT),
                    NOMTINT = VALUES(NOMTINT),
                    ACA = VALUES(ACA),
                    NOMACA = VALUES(NOMACA),
                    ESTAM = VALUES(ESTAM),
                    NOMESTAM = VALUES(NOMESTAM),
                    CONFEC = VALUES(CONFEC),
                    NOMCONFEC = VALUES(NOMCONFEC),
                    RENDIM = VALUES(RENDIM),
                    FORNITURA = VALUES(FORNITURA),
                    CESTAM = VALUES(CESTAM),
                    CESTAM2 = VALUES(CESTAM2),
                    NESTAM = VALUES(NESTAM),
                    NESTAM2 = VALUES(NESTAM2),
                    NCONFEC = VALUES(NCONFEC),
                    NPLANXA = VALUES(NPLANXA),
                    NREPAS = VALUES(NREPAS),
                    QTRANS = VALUES(QTRANS),
                    NTRANS = VALUES(NTRANS),
                    QFLOCAT = VALUES(QFLOCAT),
                    NFLOCAT = VALUES(NFLOCAT),
                    QBRODAT = VALUES(QBRODAT),
                    NBRODAT = VALUES(NBRODAT),
                    NESTAMP = VALUES(NESTAMP),
                    NTINTP = VALUES(NTINTP),
                    NACAP = VALUES(NACAP),
                    NFORNITURA = VALUES(NFORNITURA),
                    MANIPULACION = VALUES(MANIPULACION),
                    COST = VALUES(COST),
                    MARGE = VALUES(MARGE),
                    VENDA = VALUES(VENDA),
                    VENDAFINAL = VALUES(VENDAFINAL),
                    OBSERV = VALUES(OBSERV),
                    IVA = VALUES(IVA),
                    TALLA01 = VALUES(TALLA01),
                    TALLA02 = VALUES(TALLA02),
                    TALLA03 = VALUES(TALLA03),
                    TALLA04 = VALUES(TALLA04),
                    TALLA05 = VALUES(TALLA05),
                    TALLA06 = VALUES(TALLA06),
                    TALLA07 = VALUES(TALLA07),
                    TALLA08 = VALUES(TALLA08),
                    TALLA09 = VALUES(TALLA09),
                    TALLA10 = VALUES(TALLA10),
                    NPACK = VALUES(NPACK),
                    CODIMODEL = VALUES(CODIMODEL),
                    origin = VALUES(origin),
                    is_deleted = 0,
                    synced_utc = NULL,
                    updated_utc = VALUES(updated_utc);
                """;
            AddHeaderParameters(commandDb, id, command, command.TenantId, command.CompanyId, centerCode, now);
            commandDb.Parameters.AddWithValue("@origin", previous is null ? "local" : ResolveUpdatedOrigin(previous.Origin));
            await commandDb.ExecuteNonQueryAsync(cancellationToken);
        }

        await DeleteChildrenAsync(connection, transaction, id, cancellationToken);
        await InsertScandalloLinesAsync(connection, transaction, id, command, cancellationToken);
        await InsertColorLinesAsync(connection, transaction, id, command, cancellationToken);
        await InsertFornituraLinesAsync(connection, transaction, id, command, cancellationToken);
        await InsertStockLinesAsync(connection, transaction, id, command, cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        var saved = await GetByIdAsync(command.TenantId, command.CompanyId, id, cancellationToken);
        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = command.TenantId,
            CompanyId = command.CompanyId,
            UserId = _currentUserContext.UserId,
            Action = previous is null ? "modelo.created" : "modelo.updated",
            EntityName = "article_model",
            EntityId = id.ToString(),
            Details = BuildAuditDetails(saved)
        }, cancellationToken);

        return id;
    }

    public async Task DeleteAsync(Guid tenantId, Guid companyId, Guid id, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return;
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);
        EnsureTenantWriteAccess();
        var centerCode = await ResolveCompanyCenterCodeAsync(tenantId, companyId, cancellationToken);
        var existing = await GetByIdAsync(tenantId, companyId, id, cancellationToken);
        if (existing is null)
        {
            return;
        }

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            UPDATE article_models
            SET is_deleted = 1,
                origin = 'local',
                synced_utc = NULL,
                updated_utc = @updatedUtc
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND CENTRO = @centerCode
              AND record_id = @id;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@id", id.ToString());
        command.Parameters.AddWithValue("@updatedUtc", DateTime.UtcNow);
        await command.ExecuteNonQueryAsync(cancellationToken);

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = tenantId,
            CompanyId = companyId,
            UserId = _currentUserContext.UserId,
            Action = "modelo.deleted",
            EntityName = "article_model",
            EntityId = id.ToString(),
            Details = $"{existing.Code} · {existing.Season} · {existing.Series} · Cliente {existing.ClientCode}"
        }, cancellationToken);
    }

    private static async Task<List<ModeloScandalloLineDto>> LoadScandalloLinesAsync(MySqlConnection connection, Guid modelId, CancellationToken cancellationToken)
    {
        var items = new List<ModeloScandalloLineDto>();
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT line_number, TITULO, TEIXIT, CONSUM, PREU, COST
            FROM article_model_scandallo
            WHERE model_id = @modelId
            ORDER BY line_number;
            """;
        command.Parameters.AddWithValue("@modelId", modelId.ToString());
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new ModeloScandalloLineDto
            {
                LineNumber = reader.GetInt32OrDefault("line_number"),
                Title = reader.GetStringOrEmpty("TITULO"),
                FabricCode = reader.GetStringOrEmpty("TEIXIT"),
                Consumption = reader.GetDecimalOrDefault("CONSUM"),
                UnitPrice = reader.GetDecimalOrDefault("PREU"),
                CostPrice = reader.GetDecimalOrDefault("COST")
            });
        }

        return items;
    }

    private static async Task<List<ModeloColorLineDto>> LoadColorLinesAsync(MySqlConnection connection, Guid modelId, CancellationToken cancellationToken)
    {
        var items = new List<ModeloColorLineDto>();
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT line_number, MODCOL, TITULO, COLTITULO
            FROM article_model_colors
            WHERE model_id = @modelId
            ORDER BY line_number;
            """;
        command.Parameters.AddWithValue("@modelId", modelId.ToString());
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new ModeloColorLineDto
            {
                LineNumber = reader.GetInt32OrDefault("line_number"),
                ModelColorCode = reader.GetStringOrEmpty("MODCOL"),
                Title = reader.GetStringOrEmpty("TITULO"),
                ColorTitle = reader.GetStringOrEmpty("COLTITULO")
            });
        }

        return items;
    }

    private static async Task<List<ModeloFornituraLineDto>> LoadFornituraLinesAsync(MySqlConnection connection, Guid modelId, CancellationToken cancellationToken)
    {
        var items = new List<ModeloFornituraLineDto>();
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT line_number, FORNI, MEDIDA, UNITATS, PREU, IMPORT
            FROM article_model_fornituras
            WHERE model_id = @modelId
            ORDER BY line_number;
            """;
        command.Parameters.AddWithValue("@modelId", modelId.ToString());
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new ModeloFornituraLineDto
            {
                LineNumber = reader.GetInt32OrDefault("line_number"),
                FornituraCode = reader.GetStringOrEmpty("FORNI"),
                Measure = reader.GetStringOrEmpty("MEDIDA"),
                Units = reader.GetDecimalOrDefault("UNITATS"),
                UnitPrice = reader.GetDecimalOrDefault("PREU"),
                ImportAmount = reader.GetDecimalOrDefault("IMPORT")
            });
        }

        return items;
    }

    private static async Task<List<ModeloStockLineDto>> LoadStockLinesAsync(MySqlConnection connection, Guid modelId, CancellationToken cancellationToken)
    {
        var items = new List<ModeloStockLineDto>();
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT line_number, COLOR, TALLA, TALLA01, TALLA02, TALLA03, TALLA04, TALLA05,
                   TALLA06, TALLA07, TALLA08, TALLA09, TALLA10
            FROM article_model_stock
            WHERE model_id = @modelId
            ORDER BY line_number;
            """;
        command.Parameters.AddWithValue("@modelId", modelId.ToString());
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new ModeloStockLineDto
            {
                LineNumber = reader.GetInt32OrDefault("line_number"),
                Color = reader.GetStringOrEmpty("COLOR"),
                SizeText = reader.GetStringOrEmpty("TALLA"),
                SizeQuantity01 = reader.GetDecimalOrDefault("TALLA01"),
                SizeQuantity02 = reader.GetDecimalOrDefault("TALLA02"),
                SizeQuantity03 = reader.GetDecimalOrDefault("TALLA03"),
                SizeQuantity04 = reader.GetDecimalOrDefault("TALLA04"),
                SizeQuantity05 = reader.GetDecimalOrDefault("TALLA05"),
                SizeQuantity06 = reader.GetDecimalOrDefault("TALLA06"),
                SizeQuantity07 = reader.GetDecimalOrDefault("TALLA07"),
                SizeQuantity08 = reader.GetDecimalOrDefault("TALLA08"),
                SizeQuantity09 = reader.GetDecimalOrDefault("TALLA09"),
                SizeQuantity10 = reader.GetDecimalOrDefault("TALLA10")
            });
        }

        return items;
    }

    private static async Task DeleteChildrenAsync(MySqlConnection connection, MySqlTransaction transaction, Guid modelId, CancellationToken cancellationToken)
    {
        foreach (var tableName in new[]
                 {
                     "article_model_scandallo",
                     "article_model_colors",
                     "article_model_fornituras",
                     "article_model_stock"
                 })
        {
            await using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = $"DELETE FROM {tableName} WHERE model_id = @modelId;";
            command.Parameters.AddWithValue("@modelId", modelId.ToString());
            await command.ExecuteNonQueryAsync(cancellationToken);
        }
    }

    private static async Task InsertScandalloLinesAsync(MySqlConnection connection, MySqlTransaction transaction, Guid modelId, SaveModeloCommand command, CancellationToken cancellationToken)
    {
        if (command.ScandalloLines.Count == 0)
        {
            return;
        }

        await using var insertCommand = connection.CreateCommand();
        insertCommand.Transaction = transaction;
        insertCommand.CommandText =
            """
            INSERT INTO article_model_scandallo (
                model_id, line_number, TITULO, TEIXIT, CONSUM, PREU, COST)
            VALUES (
                @modelId, @lineNumber, @title, @fabricCode, @consumption, @unitPrice, @costPrice);
            """;
        insertCommand.Parameters.Add("@modelId", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@lineNumber", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@title", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@fabricCode", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@consumption", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@unitPrice", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@costPrice", MySqlDbType.Decimal);

        foreach (var line in command.ScandalloLines.OrderBy(item => item.LineNumber))
        {
            insertCommand.Parameters["@modelId"].Value = modelId.ToString();
            insertCommand.Parameters["@lineNumber"].Value = line.LineNumber;
            insertCommand.Parameters["@title"].Value = DbValue(line.Title);
            insertCommand.Parameters["@fabricCode"].Value = DbValue(line.FabricCode);
            insertCommand.Parameters["@consumption"].Value = line.Consumption;
            insertCommand.Parameters["@unitPrice"].Value = line.UnitPrice;
            insertCommand.Parameters["@costPrice"].Value = line.CostPrice;
            await insertCommand.ExecuteNonQueryAsync(cancellationToken);
        }
    }

    private static async Task InsertColorLinesAsync(MySqlConnection connection, MySqlTransaction transaction, Guid modelId, SaveModeloCommand command, CancellationToken cancellationToken)
    {
        if (command.ColorLines.Count == 0)
        {
            return;
        }

        await using var insertCommand = connection.CreateCommand();
        insertCommand.Transaction = transaction;
        insertCommand.CommandText =
            """
            INSERT INTO article_model_colors (
                model_id, line_number, MODCOL, TITULO, COLTITULO)
            VALUES (
                @modelId, @lineNumber, @modelColorCode, @title, @colorTitle);
            """;
        insertCommand.Parameters.Add("@modelId", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@lineNumber", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@modelColorCode", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@title", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@colorTitle", MySqlDbType.VarChar);

        foreach (var line in command.ColorLines.OrderBy(item => item.LineNumber))
        {
            insertCommand.Parameters["@modelId"].Value = modelId.ToString();
            insertCommand.Parameters["@lineNumber"].Value = line.LineNumber;
            insertCommand.Parameters["@modelColorCode"].Value = DbValue(line.ModelColorCode);
            insertCommand.Parameters["@title"].Value = DbValue(line.Title);
            insertCommand.Parameters["@colorTitle"].Value = DbValue(line.ColorTitle);
            await insertCommand.ExecuteNonQueryAsync(cancellationToken);
        }
    }

    private static async Task InsertFornituraLinesAsync(MySqlConnection connection, MySqlTransaction transaction, Guid modelId, SaveModeloCommand command, CancellationToken cancellationToken)
    {
        if (command.FornituraLines.Count == 0)
        {
            return;
        }

        await using var insertCommand = connection.CreateCommand();
        insertCommand.Transaction = transaction;
        insertCommand.CommandText =
            """
            INSERT INTO article_model_fornituras (
                model_id, line_number, FORNI, MEDIDA, UNITATS, PREU, IMPORT)
            VALUES (
                @modelId, @lineNumber, @fornituraCode, @measure, @units, @unitPrice, @importAmount);
            """;
        insertCommand.Parameters.Add("@modelId", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@lineNumber", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@fornituraCode", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@measure", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@units", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@unitPrice", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@importAmount", MySqlDbType.Decimal);

        foreach (var line in command.FornituraLines.OrderBy(item => item.LineNumber))
        {
            insertCommand.Parameters["@modelId"].Value = modelId.ToString();
            insertCommand.Parameters["@lineNumber"].Value = line.LineNumber;
            insertCommand.Parameters["@fornituraCode"].Value = DbValue(line.FornituraCode);
            insertCommand.Parameters["@measure"].Value = DbValue(line.Measure);
            insertCommand.Parameters["@units"].Value = line.Units;
            insertCommand.Parameters["@unitPrice"].Value = line.UnitPrice;
            insertCommand.Parameters["@importAmount"].Value = line.ImportAmount;
            await insertCommand.ExecuteNonQueryAsync(cancellationToken);
        }
    }

    private static async Task InsertStockLinesAsync(MySqlConnection connection, MySqlTransaction transaction, Guid modelId, SaveModeloCommand command, CancellationToken cancellationToken)
    {
        if (command.StockLines.Count == 0)
        {
            return;
        }

        await using var insertCommand = connection.CreateCommand();
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

        foreach (var line in command.StockLines.OrderBy(item => item.LineNumber))
        {
            insertCommand.Parameters["@modelId"].Value = modelId.ToString();
            insertCommand.Parameters["@lineNumber"].Value = line.LineNumber;
            insertCommand.Parameters["@color"].Value = DbValue(line.Color);
            insertCommand.Parameters["@sizeText"].Value = DbValue(line.SizeText);
            insertCommand.Parameters["@sizeQuantity01"].Value = line.SizeQuantity01;
            insertCommand.Parameters["@sizeQuantity02"].Value = line.SizeQuantity02;
            insertCommand.Parameters["@sizeQuantity03"].Value = line.SizeQuantity03;
            insertCommand.Parameters["@sizeQuantity04"].Value = line.SizeQuantity04;
            insertCommand.Parameters["@sizeQuantity05"].Value = line.SizeQuantity05;
            insertCommand.Parameters["@sizeQuantity06"].Value = line.SizeQuantity06;
            insertCommand.Parameters["@sizeQuantity07"].Value = line.SizeQuantity07;
            insertCommand.Parameters["@sizeQuantity08"].Value = line.SizeQuantity08;
            insertCommand.Parameters["@sizeQuantity09"].Value = line.SizeQuantity09;
            insertCommand.Parameters["@sizeQuantity10"].Value = line.SizeQuantity10;
            await insertCommand.ExecuteNonQueryAsync(cancellationToken);
        }
    }

    private static void AddHeaderParameters(
        MySqlCommand command,
        Guid id,
        SaveModeloCommand input,
        Guid tenantId,
        Guid companyId,
        string centerCode,
        DateTime now)
    {
        command.Parameters.AddWithValue("@id", id.ToString());
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@code", input.Code);
        command.Parameters.AddWithValue("@series", input.Series);
        command.Parameters.AddWithValue("@clientCode", input.ClientCode);
        command.Parameters.AddWithValue("@clientName", DbValue(input.ClientName));
        command.Parameters.AddWithValue("@season", input.Season);
        command.Parameters.AddWithValue("@fabricCode", DbValue(input.FabricCode));
        command.Parameters.AddWithValue("@fabricDescription", DbValue(input.FabricDescription));
        command.Parameters.AddWithValue("@description", input.Description);
        command.Parameters.AddWithValue("@supplierCode", input.SupplierCode);
        command.Parameters.AddWithValue("@supplierName", DbValue(input.SupplierName));
        command.Parameters.AddWithValue("@widthText", DbValue(input.WidthText));
        command.Parameters.AddWithValue("@dyeingWorkshopCode", input.DyeingWorkshopCode);
        command.Parameters.AddWithValue("@dyeingWorkshopName", DbValue(input.DyeingWorkshopName));
        command.Parameters.AddWithValue("@finishingWorkshopCode", input.FinishingWorkshopCode);
        command.Parameters.AddWithValue("@finishingWorkshopName", DbValue(input.FinishingWorkshopName));
        command.Parameters.AddWithValue("@printingWorkshopCode", input.PrintingWorkshopCode);
        command.Parameters.AddWithValue("@printingWorkshopName", DbValue(input.PrintingWorkshopName));
        command.Parameters.AddWithValue("@tailoringWorkshopCode", input.TailoringWorkshopCode);
        command.Parameters.AddWithValue("@tailoringWorkshopName", DbValue(input.TailoringWorkshopName));
        command.Parameters.AddWithValue("@yield", input.Yield);
        command.Parameters.AddWithValue("@fornituraSummary", DbValue(input.FornituraSummary));
        command.Parameters.AddWithValue("@printingCode1", DbValue(input.PrintingCode1));
        command.Parameters.AddWithValue("@printingCode2", DbValue(input.PrintingCode2));
        command.Parameters.AddWithValue("@printingUnits1", input.PrintingUnits1);
        command.Parameters.AddWithValue("@printingUnits2", input.PrintingUnits2);
        command.Parameters.AddWithValue("@tailoringPrice", input.TailoringPrice);
        command.Parameters.AddWithValue("@platePrice", input.PlatePrice);
        command.Parameters.AddWithValue("@reviewPrice", input.ReviewPrice);
        command.Parameters.AddWithValue("@transferQuantity", input.TransferQuantity);
        command.Parameters.AddWithValue("@transferPrice", input.TransferPrice);
        command.Parameters.AddWithValue("@flockedQuantity", input.FlockedQuantity);
        command.Parameters.AddWithValue("@flockedPrice", input.FlockedPrice);
        command.Parameters.AddWithValue("@embroideredQuantity", input.EmbroideredQuantity);
        command.Parameters.AddWithValue("@embroideredPrice", input.EmbroideredPrice);
        command.Parameters.AddWithValue("@printingPrice", input.PrintingPrice);
        command.Parameters.AddWithValue("@dyeingPrice", input.DyeingPrice);
        command.Parameters.AddWithValue("@finishingPrice", input.FinishingPrice);
        command.Parameters.AddWithValue("@fornituraPrice", input.FornituraPrice);
        command.Parameters.AddWithValue("@manipulationPrice", input.ManipulationPrice);
        command.Parameters.AddWithValue("@costPrice", input.CostPrice);
        command.Parameters.AddWithValue("@marginPercent", input.MarginPercent);
        command.Parameters.AddWithValue("@salePrice", input.SalePrice);
        command.Parameters.AddWithValue("@finalSalePrice", input.FinalSalePrice);
        command.Parameters.AddWithValue("@notes", DbValue(input.Notes));
        command.Parameters.AddWithValue("@vatCode", DbValue(input.VatCode));
        command.Parameters.AddWithValue("@size01", DbValue(input.Size01));
        command.Parameters.AddWithValue("@size02", DbValue(input.Size02));
        command.Parameters.AddWithValue("@size03", DbValue(input.Size03));
        command.Parameters.AddWithValue("@size04", DbValue(input.Size04));
        command.Parameters.AddWithValue("@size05", DbValue(input.Size05));
        command.Parameters.AddWithValue("@size06", DbValue(input.Size06));
        command.Parameters.AddWithValue("@size07", DbValue(input.Size07));
        command.Parameters.AddWithValue("@size08", DbValue(input.Size08));
        command.Parameters.AddWithValue("@size09", DbValue(input.Size09));
        command.Parameters.AddWithValue("@size10", DbValue(input.Size10));
        command.Parameters.AddWithValue("@packagingPrice", input.PackagingPrice);
        command.Parameters.AddWithValue("@modelCode", DbValue(input.ModelCode));
        command.Parameters.AddWithValue("@createdUtc", now);
        command.Parameters.AddWithValue("@updatedUtc", now);
    }

    private static async Task EnsureUniqueIdentityAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid tenantId,
        Guid companyId,
        Guid currentId,
        string centerCode,
        SaveModeloCommand command,
        CancellationToken cancellationToken)
    {
        await using var duplicateCommand = connection.CreateCommand();
        duplicateCommand.Transaction = transaction;
        duplicateCommand.CommandText =
            """
            SELECT record_id
            FROM article_models
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND CENTRO = @centerCode
              AND CODI = @code
              AND SERIE = @series
              AND CLIENT = @clientCode
              AND TEMPORADA = @season
              AND record_id <> @id
            LIMIT 1;
            """;
        duplicateCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        duplicateCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
        duplicateCommand.Parameters.AddWithValue("@centerCode", centerCode);
        duplicateCommand.Parameters.AddWithValue("@code", command.Code);
        duplicateCommand.Parameters.AddWithValue("@series", command.Series);
        duplicateCommand.Parameters.AddWithValue("@clientCode", command.ClientCode);
        duplicateCommand.Parameters.AddWithValue("@season", command.Season);
        duplicateCommand.Parameters.AddWithValue("@id", currentId.ToString());

        var existingId = await duplicateCommand.ExecuteScalarAsync(cancellationToken);
        if (existingId is not null)
        {
            throw new InvalidOperationException("Ya existe un modelo con el mismo código, serie, temporada y cliente en esta empresa.");
        }
    }

    private static string ResolveUpdatedOrigin(string previousOrigin)
    {
        return string.Equals(previousOrigin, "legacy", StringComparison.OrdinalIgnoreCase)
            ? "local"
            : (string.IsNullOrWhiteSpace(previousOrigin) ? "local" : previousOrigin);
    }

    private static decimal CalculateDerivedCost(SaveModeloCommand command)
    {
        return Math.Round(
            command.TailoringPrice +
            command.PlatePrice +
            command.ReviewPrice +
            command.PackagingPrice +
            command.FornituraPrice +
            command.ManipulationPrice +
            command.ScandalloLines.Sum(line => line.CostPrice),
            4,
            MidpointRounding.AwayFromZero);
    }

    private static void NormalizeCommand(SaveModeloCommand command)
    {
        command.Code = command.Code.Trim().ToUpperInvariant();
        command.Series = command.Series.Trim().ToUpperInvariant();
        command.Season = command.Season.Trim().ToUpperInvariant();
        command.Description = command.Description.Trim();
        command.ClientName = command.ClientName.Trim();
        command.ModelCode = command.ModelCode.Trim();
        command.FabricCode = command.FabricCode.Trim().ToUpperInvariant();
        command.FabricDescription = command.FabricDescription.Trim();
        command.SupplierName = command.SupplierName.Trim();
        command.WidthText = command.WidthText.Trim();
        command.DyeingWorkshopName = command.DyeingWorkshopName.Trim();
        command.FinishingWorkshopName = command.FinishingWorkshopName.Trim();
        command.PrintingWorkshopName = command.PrintingWorkshopName.Trim();
        command.TailoringWorkshopName = command.TailoringWorkshopName.Trim();
        command.FornituraSummary = command.FornituraSummary.Trim();
        command.PrintingCode1 = command.PrintingCode1.Trim();
        command.PrintingCode2 = command.PrintingCode2.Trim();
        command.Notes = command.Notes.Trim();
        command.VatCode = command.VatCode.Trim().ToUpperInvariant();
        command.Size01 = command.Size01.Trim();
        command.Size02 = command.Size02.Trim();
        command.Size03 = command.Size03.Trim();
        command.Size04 = command.Size04.Trim();
        command.Size05 = command.Size05.Trim();
        command.Size06 = command.Size06.Trim();
        command.Size07 = command.Size07.Trim();
        command.Size08 = command.Size08.Trim();
        command.Size09 = command.Size09.Trim();
        command.Size10 = command.Size10.Trim();

        NormalizeScandalloLines(command);
        NormalizeColorLines(command);
        NormalizeFornituraLines(command);
        NormalizeStockLines(command);
    }

    private static void NormalizeScandalloLines(SaveModeloCommand command)
    {
        var normalized = new List<SaveModeloScandalloLineInput>();
        var lineNumber = 1;
        foreach (var line in command.ScandalloLines)
        {
            if (string.IsNullOrWhiteSpace(line.Title) &&
                string.IsNullOrWhiteSpace(line.FabricCode) &&
                line.Consumption == 0m &&
                line.UnitPrice == 0m &&
                line.CostPrice == 0m)
            {
                continue;
            }

            normalized.Add(new SaveModeloScandalloLineInput
            {
                LineNumber = lineNumber++,
                Title = line.Title.Trim(),
                FabricCode = line.FabricCode.Trim().ToUpperInvariant(),
                Consumption = line.Consumption,
                UnitPrice = line.UnitPrice,
                CostPrice = line.CostPrice
            });
        }

        command.ScandalloLines = normalized;
    }

    private static void NormalizeColorLines(SaveModeloCommand command)
    {
        var normalized = new List<SaveModeloColorLineInput>();
        var lineNumber = 1;
        foreach (var line in command.ColorLines)
        {
            if (string.IsNullOrWhiteSpace(line.ModelColorCode) &&
                string.IsNullOrWhiteSpace(line.Title) &&
                string.IsNullOrWhiteSpace(line.ColorTitle))
            {
                continue;
            }

            normalized.Add(new SaveModeloColorLineInput
            {
                LineNumber = lineNumber++,
                ModelColorCode = line.ModelColorCode.Trim().ToUpperInvariant(),
                Title = line.Title.Trim(),
                ColorTitle = line.ColorTitle.Trim()
            });
        }

        command.ColorLines = normalized;
    }

    private static void NormalizeFornituraLines(SaveModeloCommand command)
    {
        var normalized = new List<SaveModeloFornituraLineInput>();
        var lineNumber = 1;
        foreach (var line in command.FornituraLines)
        {
            if (string.IsNullOrWhiteSpace(line.FornituraCode) &&
                string.IsNullOrWhiteSpace(line.Measure) &&
                line.Units == 0m &&
                line.UnitPrice == 0m &&
                line.ImportAmount == 0m)
            {
                continue;
            }

            var importAmount = line.ImportAmount != 0m ? line.ImportAmount : Math.Round(line.Units * line.UnitPrice, 4, MidpointRounding.AwayFromZero);
            normalized.Add(new SaveModeloFornituraLineInput
            {
                LineNumber = lineNumber++,
                FornituraCode = line.FornituraCode.Trim().ToUpperInvariant(),
                Measure = line.Measure.Trim(),
                Units = line.Units,
                UnitPrice = line.UnitPrice,
                ImportAmount = importAmount
            });
        }

        command.FornituraLines = normalized;
    }

    private static void NormalizeStockLines(SaveModeloCommand command)
    {
        var normalized = new List<SaveModeloStockLineInput>();
        var lineNumber = 1;
        foreach (var line in command.StockLines)
        {
            if (string.IsNullOrWhiteSpace(line.Color) &&
                string.IsNullOrWhiteSpace(line.SizeText) &&
                line.SizeQuantity01 == 0m &&
                line.SizeQuantity02 == 0m &&
                line.SizeQuantity03 == 0m &&
                line.SizeQuantity04 == 0m &&
                line.SizeQuantity05 == 0m &&
                line.SizeQuantity06 == 0m &&
                line.SizeQuantity07 == 0m &&
                line.SizeQuantity08 == 0m &&
                line.SizeQuantity09 == 0m &&
                line.SizeQuantity10 == 0m)
            {
                continue;
            }

            line.LineNumber = lineNumber++;
            line.Color = line.Color.Trim();
            line.SizeText = line.SizeText.Trim();
            normalized.Add(line);
        }

        command.StockLines = normalized;
    }

    private static void Validate(SaveModeloCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.Code))
        {
            throw new InvalidOperationException("El código del modelo es obligatorio.");
        }

        if (string.IsNullOrWhiteSpace(command.Season))
        {
            throw new InvalidOperationException("La temporada del modelo es obligatoria.");
        }

        if (string.IsNullOrWhiteSpace(command.Series))
        {
            throw new InvalidOperationException("La serie del modelo es obligatoria.");
        }

        if (command.ClientCode <= 0)
        {
            throw new InvalidOperationException("El cliente del modelo es obligatorio.");
        }

        if (string.IsNullOrWhiteSpace(command.Description))
        {
            throw new InvalidOperationException("La descripción del modelo es obligatoria.");
        }
    }

    private string BuildAuditDetails(ModeloDetailDto? detail)
    {
        if (detail is null)
        {
            return string.Empty;
        }

        return $"{detail.Code} · {detail.Season} · {detail.Series} · Cliente {detail.ClientCode} · Coste {detail.CostPrice:0.00##} · Venta {detail.FinalSalePrice:0.00##}";
    }

    private static object DbValue(string? value)
        => string.IsNullOrWhiteSpace(value) ? DBNull.Value : value.Trim();

    private static string BuildSearchOrderByClause(ModeloFilter filter)
    {
        var column = filter.SortColumn switch
        {
            nameof(ModeloListItemDto.Code) => "CODI",
            nameof(ModeloListItemDto.Description) => "DESCRI",
            nameof(ModeloListItemDto.Series) => "SERIE",
            nameof(ModeloListItemDto.Season) => "TEMPORADA",
            nameof(ModeloListItemDto.ClientName) => "NOMCLIENT",
            nameof(ModeloListItemDto.FabricCode) => "TEIXIT",
            nameof(ModeloListItemDto.CostPrice) => "COST",
            nameof(ModeloListItemDto.SalePrice) => "VENDA",
            nameof(ModeloListItemDto.FinalSalePrice) => "VENDAFINAL",
            nameof(ModeloListItemDto.Origin) => "origin",
            _ => "TEMPORADA, SERIE, CLIENT, CODI"
        };

        var direction = filter.SortDescending ? "DESC" : "ASC";
        return column.Contains(',')
            ? $"ORDER BY {column}"
            : $"ORDER BY {column} {direction}, CLIENT ASC, CODI ASC";
    }

    private async Task EnsureCompanyAccessAsync(Guid tenantId, Guid companyId, CancellationToken cancellationToken)
    {
        if (!_currentUserContext.IsAuthenticated)
        {
            throw new InvalidOperationException("Debes iniciar sesión para acceder a esta empresa.");
        }

        if (!_tenantContext.TenantId.HasValue || _tenantContext.TenantId.Value != tenantId)
        {
            throw new InvalidOperationException("El tenant solicitado no coincide con tu sesión activa.");
        }

        if (!_activeCompanyContext.CompanyId.HasValue || _activeCompanyContext.CompanyId.Value != companyId)
        {
            throw new InvalidOperationException("La empresa activa no coincide con la empresa solicitada.");
        }

        if (!_currentUserContext.UserId.HasValue)
        {
            throw new InvalidOperationException("Debes iniciar sesión para acceder a esta empresa.");
        }

        var allowedCompanies = await _companyAccessService.GetAllowedCompaniesAsync(
            _currentUserContext.UserId.Value,
            tenantId,
            cancellationToken);

        if (!allowedCompanies.Any(company => company.CompanyId == companyId))
        {
            throw new InvalidOperationException("No tienes acceso a la empresa activa.");
        }
    }

    private void EnsureTenantWriteAccess()
    {
        if (_currentUserContext.IsPlatformAdmin)
        {
            return;
        }

        if (_currentUserContext.Roles.Contains(PlatformRoles.TenantAdmin, StringComparer.OrdinalIgnoreCase))
        {
            return;
        }

        throw new InvalidOperationException("No tienes permisos de edición para esta empresa.");
    }

    private async Task<string> ResolveCompanyCenterCodeAsync(Guid tenantId, Guid companyId, CancellationToken cancellationToken)
    {
        if (!_currentUserContext.UserId.HasValue)
        {
            throw new InvalidOperationException("Debes iniciar sesión para acceder a esta empresa.");
        }

        var companies = await _companyAccessService.GetAllowedCompaniesAsync(_currentUserContext.UserId.Value, tenantId, cancellationToken);
        var company = companies.FirstOrDefault(item => item.CompanyId == companyId);
        if (company is null)
        {
            throw new InvalidOperationException("No se ha podido resolver la empresa seleccionada.");
        }

        if (string.IsNullOrWhiteSpace(company.LegacyCenterCode))
        {
            throw new InvalidOperationException("La empresa activa no tiene centro legacy configurado.");
        }

        return company.LegacyCenterCode.Trim().ToUpperInvariant();
    }
}
