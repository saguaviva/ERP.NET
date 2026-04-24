using Erp.Application.Auditing;
using Erp.Application.Companies;
using Erp.Application.Contexts;
using Erp.Application.Tejidos;
using Erp.Domain.Common;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Support;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Tejidos;

public sealed class MySqlTejidoService : ITejidoQueries, ITejidoService
{
    private readonly MySqlConnectionFactory _connectionFactory;
    private readonly IAuditLogService _auditLogService;
    private readonly ICompanyAccessService _companyAccessService;
    private readonly ICurrentUserContext _currentUserContext;
    private readonly ITenantContext _tenantContext;
    private readonly IActiveCompanyContext _activeCompanyContext;

    public MySqlTejidoService(
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

    public async Task<TejidoSearchResultDto> SearchAsync(Guid tenantId, Guid companyId, TejidoFilter filter, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return new TejidoSearchResultDto();
        }

        if (!await CanReadCompanyAsync(tenantId, companyId, cancellationToken))
        {
            return new TejidoSearchResultDto();
        }

        var centerCode = await ResolveCompanyCenterCodeAsync(tenantId, companyId, cancellationToken);
        var pageSize = Math.Clamp(filter.PageSize, 10, 200);
        var page = Math.Max(filter.Page, 1);
        var offset = (page - 1) * pageSize;
        var search = filter.Search?.Trim() ?? string.Empty;
        var likeSearch = $"%{search}%";
        var tubularMode = filter.TubularMode?.Trim().ToLowerInvariant() ?? string.Empty;
        var onlyWithAvailableStock = filter.OnlyWithAvailableStock;
        const string widthExpression = "COALESCE(NULLIF(AMPLE2, 0), NULLIF(CAST(AMPLE AS DECIMAL(12,4)), 0), 0)";

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var countCommand = connection.CreateCommand();
        countCommand.CommandText =
            $"""
            SELECT COUNT(*)
            FROM teixits
            WHERE CENTRO = @centerCode
              AND is_deleted = 0
              AND (
                    @search = ''
                    OR CODI LIKE @likeSearch
                    OR DESCRI LIKE @likeSearch
                    OR NRO LIKE @likeSearch
                    OR AMPLE LIKE @likeSearch
                    OR OBSERV LIKE @likeSearch
                    OR CAST(MAQUI AS CHAR) LIKE @likeSearch
                    OR CAST(TEIXIDOR AS CHAR) LIKE @likeSearch
                    OR CAST(ACABADOR AS CHAR) LIKE @likeSearch
                  )
              AND (@weaverCode IS NULL OR TEIXIDOR = @weaverCode)
              AND (@finisherCode IS NULL OR ACABADOR = @finisherCode)
              AND (@gramWeightMin IS NULL OR GRAMA >= @gramWeightMin)
              AND (@gramWeightMax IS NULL OR GRAMA <= @gramWeightMax)
              AND (@widthMin IS NULL OR {widthExpression} >= @widthMin)
              AND (@widthMax IS NULL OR {widthExpression} <= @widthMax)
              AND (@pricePerMeterMin IS NULL OR PREUM >= @pricePerMeterMin)
              AND (@pricePerMeterMax IS NULL OR PREUM <= @pricePerMeterMax)
              AND (@pricePerKilogramMin IS NULL OR PREUK >= @pricePerKilogramMin)
              AND (@pricePerKilogramMax IS NULL OR PREUK <= @pricePerKilogramMax)
              AND (
                    @tubularMode = ''
                    OR (@tubularMode = 'yes' AND COALESCE(TUBULAR, 0) <> 0)
                    OR (@tubularMode = 'no' AND COALESCE(TUBULAR, 0) = 0)
                  )
              AND (@onlyWithAvailableStock = 0 OR COALESCE(STDISPM, 0) > 0);
            """;
        countCommand.Parameters.AddWithValue("@centerCode", centerCode);
        countCommand.Parameters.AddWithValue("@search", search);
        countCommand.Parameters.AddWithValue("@likeSearch", likeSearch);
        countCommand.Parameters.AddWithValue("@weaverCode", filter.WeaverCode.HasValue ? filter.WeaverCode.Value : DBNull.Value);
        countCommand.Parameters.AddWithValue("@finisherCode", filter.FinisherCode.HasValue ? filter.FinisherCode.Value : DBNull.Value);
        countCommand.Parameters.AddWithValue("@gramWeightMin", filter.GramWeightMin.HasValue ? filter.GramWeightMin.Value : DBNull.Value);
        countCommand.Parameters.AddWithValue("@gramWeightMax", filter.GramWeightMax.HasValue ? filter.GramWeightMax.Value : DBNull.Value);
        countCommand.Parameters.AddWithValue("@widthMin", filter.WidthMin.HasValue ? filter.WidthMin.Value : DBNull.Value);
        countCommand.Parameters.AddWithValue("@widthMax", filter.WidthMax.HasValue ? filter.WidthMax.Value : DBNull.Value);
        countCommand.Parameters.AddWithValue("@pricePerMeterMin", filter.PricePerMeterMin.HasValue ? filter.PricePerMeterMin.Value : DBNull.Value);
        countCommand.Parameters.AddWithValue("@pricePerMeterMax", filter.PricePerMeterMax.HasValue ? filter.PricePerMeterMax.Value : DBNull.Value);
        countCommand.Parameters.AddWithValue("@pricePerKilogramMin", filter.PricePerKilogramMin.HasValue ? filter.PricePerKilogramMin.Value : DBNull.Value);
        countCommand.Parameters.AddWithValue("@pricePerKilogramMax", filter.PricePerKilogramMax.HasValue ? filter.PricePerKilogramMax.Value : DBNull.Value);
        countCommand.Parameters.AddWithValue("@tubularMode", tubularMode);
        countCommand.Parameters.AddWithValue("@onlyWithAvailableStock", onlyWithAvailableStock ? 1 : 0);
        var totalCount = Convert.ToInt32(await countCommand.ExecuteScalarAsync(cancellationToken));
        if (totalCount == 0)
        {
            return new TejidoSearchResultDto { TotalCount = 0 };
        }

        var items = new List<TejidoListItemDto>();
        await using var command = connection.CreateCommand();
        command.CommandText =
            $"""
            SELECT CODI, CENTRO, DESCRI, MAQUI, TEIXIDOR, ACABADOR, AMPLE, GRAMA, PREUM, PREUK, STDISPM, TUBULAR, origin
            FROM teixits
            WHERE CENTRO = @centerCode
              AND is_deleted = 0
              AND (
                    @search = ''
                    OR CODI LIKE @likeSearch
                    OR DESCRI LIKE @likeSearch
                    OR NRO LIKE @likeSearch
                    OR AMPLE LIKE @likeSearch
                    OR OBSERV LIKE @likeSearch
                    OR CAST(MAQUI AS CHAR) LIKE @likeSearch
                    OR CAST(TEIXIDOR AS CHAR) LIKE @likeSearch
                    OR CAST(ACABADOR AS CHAR) LIKE @likeSearch
                  )
              AND (@weaverCode IS NULL OR TEIXIDOR = @weaverCode)
              AND (@finisherCode IS NULL OR ACABADOR = @finisherCode)
              AND (@gramWeightMin IS NULL OR GRAMA >= @gramWeightMin)
              AND (@gramWeightMax IS NULL OR GRAMA <= @gramWeightMax)
              AND (@widthMin IS NULL OR {widthExpression} >= @widthMin)
              AND (@widthMax IS NULL OR {widthExpression} <= @widthMax)
              AND (@pricePerMeterMin IS NULL OR PREUM >= @pricePerMeterMin)
              AND (@pricePerMeterMax IS NULL OR PREUM <= @pricePerMeterMax)
              AND (@pricePerKilogramMin IS NULL OR PREUK >= @pricePerKilogramMin)
              AND (@pricePerKilogramMax IS NULL OR PREUK <= @pricePerKilogramMax)
              AND (
                    @tubularMode = ''
                    OR (@tubularMode = 'yes' AND COALESCE(TUBULAR, 0) <> 0)
                    OR (@tubularMode = 'no' AND COALESCE(TUBULAR, 0) = 0)
                  )
              AND (@onlyWithAvailableStock = 0 OR COALESCE(STDISPM, 0) > 0)
            {BuildSearchOrderByClause(filter)}
            LIMIT @limit OFFSET @offset;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@search", search);
        command.Parameters.AddWithValue("@likeSearch", likeSearch);
        command.Parameters.AddWithValue("@weaverCode", filter.WeaverCode.HasValue ? filter.WeaverCode.Value : DBNull.Value);
        command.Parameters.AddWithValue("@finisherCode", filter.FinisherCode.HasValue ? filter.FinisherCode.Value : DBNull.Value);
        command.Parameters.AddWithValue("@gramWeightMin", filter.GramWeightMin.HasValue ? filter.GramWeightMin.Value : DBNull.Value);
        command.Parameters.AddWithValue("@gramWeightMax", filter.GramWeightMax.HasValue ? filter.GramWeightMax.Value : DBNull.Value);
        command.Parameters.AddWithValue("@widthMin", filter.WidthMin.HasValue ? filter.WidthMin.Value : DBNull.Value);
        command.Parameters.AddWithValue("@widthMax", filter.WidthMax.HasValue ? filter.WidthMax.Value : DBNull.Value);
        command.Parameters.AddWithValue("@pricePerMeterMin", filter.PricePerMeterMin.HasValue ? filter.PricePerMeterMin.Value : DBNull.Value);
        command.Parameters.AddWithValue("@pricePerMeterMax", filter.PricePerMeterMax.HasValue ? filter.PricePerMeterMax.Value : DBNull.Value);
        command.Parameters.AddWithValue("@pricePerKilogramMin", filter.PricePerKilogramMin.HasValue ? filter.PricePerKilogramMin.Value : DBNull.Value);
        command.Parameters.AddWithValue("@pricePerKilogramMax", filter.PricePerKilogramMax.HasValue ? filter.PricePerKilogramMax.Value : DBNull.Value);
        command.Parameters.AddWithValue("@tubularMode", tubularMode);
        command.Parameters.AddWithValue("@onlyWithAvailableStock", onlyWithAvailableStock ? 1 : 0);
        command.Parameters.AddWithValue("@limit", pageSize);
        command.Parameters.AddWithValue("@offset", offset);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new TejidoListItemDto
            {
                Code = reader.GetStringOrEmpty("CODI"),
                CompanyCenterCode = reader.GetStringOrEmpty("CENTRO"),
                Description = reader.GetStringOrEmpty("DESCRI"),
                MachineCode = reader.GetInt32OrDefault("MAQUI"),
                WeaverCode = reader.GetInt32OrDefault("TEIXIDOR"),
                FinisherCode = reader.GetInt32OrDefault("ACABADOR"),
                WidthText = reader.GetStringOrEmpty("AMPLE"),
                GramWeight = reader.GetDecimalOrDefault("GRAMA"),
                PricePerMeter = reader.GetDecimalOrDefault("PREUM"),
                PricePerKilogram = reader.GetDecimalOrDefault("PREUK"),
                AvailableStockMeters = reader.GetDecimalOrDefault("STDISPM"),
                IsTubular = reader.GetBooleanValue("TUBULAR"),
                Origin = reader.GetStringOrEmpty("origin")
            });
        }

        return new TejidoSearchResultDto
        {
            Items = items,
            TotalCount = totalCount
        };
    }

    public async Task<TejidoDetailDto?> GetByCodeAsync(Guid tenantId, Guid companyId, string code, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return null;
        }

        if (!await CanReadCompanyAsync(tenantId, companyId, cancellationToken))
        {
            return null;
        }

        var centerCode = await ResolveCompanyCenterCodeAsync(tenantId, companyId, cancellationToken);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT CODI, CENTRO, DESCRI, NRO, MAQUI, MATERIA, OBSERV, IVA, TEIXIDOR, PTEIXIR, ESTAMPADOR, PESTAM,
                   ACABADOR, ACABAT, PACA, CRU, AMPLE, RENDIMENT, MARGE, GRAMA, PREUM, PREUK, STCRUM, STDISPM,
                   STCRUK, STDISPK, PREUPERMODEL, TUBULAR, AMPLE2
            FROM teixits
            WHERE CODI = @code
              AND CENTRO = @centerCode
              AND is_deleted = 0
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@code", code);
        command.Parameters.AddWithValue("@centerCode", centerCode);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        var detail = new TejidoDetailDto
        {
            Code = reader.GetStringOrEmpty("CODI"),
            CompanyCenterCode = reader.GetStringOrEmpty("CENTRO"),
            Description = reader.GetStringOrEmpty("DESCRI"),
            CompositionText = reader.GetStringOrEmpty("NRO"),
            MachineCode = reader.GetInt32OrDefault("MAQUI"),
            MaterialCost = reader.GetDecimalOrDefault("MATERIA"),
            Notes = reader.GetStringOrEmpty("OBSERV"),
            VatCode = reader.GetStringOrEmpty("IVA"),
            WeaverCode = reader.GetInt32OrDefault("TEIXIDOR"),
            WeavingCost = reader.GetDecimalOrDefault("PTEIXIR"),
            PrinterCode = reader.GetInt32OrDefault("ESTAMPADOR"),
            PrintingCost = reader.GetDecimalOrDefault("PESTAM"),
            FinisherCode = reader.GetInt32OrDefault("ACABADOR"),
            FinishSummary = reader.GetStringOrEmpty("ACABAT"),
            FinishingCost = reader.GetDecimalOrDefault("PACA"),
            RawCost = reader.GetDecimalOrDefault("CRU"),
            WidthText = reader.GetStringOrEmpty("AMPLE"),
            Yield = reader.GetDecimalOrDefault("RENDIMENT"),
            Margin = reader.GetDecimalOrDefault("MARGE"),
            GramWeight = reader.GetDecimalOrDefault("GRAMA"),
            PricePerMeter = reader.GetDecimalOrDefault("PREUM"),
            PricePerKilogram = reader.GetDecimalOrDefault("PREUK"),
            RawStockMeters = reader.GetDecimalOrDefault("STCRUM"),
            AvailableStockMeters = reader.GetDecimalOrDefault("STDISPM"),
            RawStockKilograms = reader.GetDecimalOrDefault("STCRUK"),
            AvailableStockKilograms = reader.GetDecimalOrDefault("STDISPK"),
            SamplePrice = reader.GetDecimalOrDefault("PREUPERMODEL"),
            IsTubular = reader.GetBooleanValue("TUBULAR"),
            Width2 = reader.GetDecimalOrDefault("AMPLE2")
        };

        await reader.CloseAsync();
        detail.Colors = await LoadColorsAsync(connection, centerCode, detail.Code, cancellationToken);
        detail.Composition = await LoadCompositionAsync(connection, centerCode, detail.Code, cancellationToken);
        detail.Finishes = await LoadFinishesAsync(connection, centerCode, detail.Code, cancellationToken);
        return detail;
    }

    public async Task<string> SaveAsync(SaveTejidoCommand command, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return string.Empty;
        }

        await EnsureCompanyAccessAsync(command.TenantId, command.CompanyId, cancellationToken);
        EnsureTenantWriteAccess();
        Validate(command);
        var centerCode = await ResolveCompanyCenterCodeAsync(command.TenantId, command.CompanyId, cancellationToken);

        TejidoDetailDto? previous = null;
        if (command.IsNew)
        {
            var duplicate = await GetByCodeAsync(command.TenantId, command.CompanyId, command.Code!, cancellationToken);
            if (duplicate is not null)
            {
                throw new InvalidOperationException("Ya existe un tejido con este código.");
            }
        }
        else
        {
            previous = await GetByCodeAsync(command.TenantId, command.CompanyId, command.Code!, cancellationToken);
            if (previous is null)
            {
                throw new InvalidOperationException("No se ha encontrado el tejido que intentas modificar.");
            }
        }

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);
        var code = command.Code!.Trim().ToUpperInvariant();

        if (previous is not null)
        {
            await using var updateCommand = connection.CreateCommand();
            updateCommand.Transaction = transaction;
            updateCommand.CommandText =
                """
                UPDATE teixits
                SET DESCRI = @description,
                    NRO = @compositionText,
                    MAQUI = @machineCode,
                    MATERIA = @materialCost,
                    OBSERV = @notes,
                    IVA = @vatCode,
                    TEIXIDOR = @weaverCode,
                    PTEIXIR = @weavingCost,
                    ESTAMPADOR = @printerCode,
                    PESTAM = @printingCost,
                    ACABADOR = @finisherCode,
                    ACABAT = @finishSummary,
                    PACA = @finishingCost,
                    CRU = @rawCost,
                    AMPLE = @widthText,
                    RENDIMENT = @yield,
                    MARGE = @margin,
                    GRAMA = @gramWeight,
                    PREUM = @pricePerMeter,
                    PREUK = @pricePerKilogram,
                    STCRUM = @rawStockMeters,
                    STDISPM = @availableStockMeters,
                    STCRUK = @rawStockKilograms,
                    STDISPK = @availableStockKilograms,
                    PREUPERMODEL = @samplePrice,
                    TUBULAR = @isTubular,
                    AMPLE2 = @width2,
                    origin = 'local',
                    is_deleted = 0,
                    synced_utc = NULL
                WHERE CODI = @code
                  AND CENTRO = @centerCode;
                """;
            FillSaveParameters(updateCommand, centerCode, code, command);
            var affected = await updateCommand.ExecuteNonQueryAsync(cancellationToken);
            if (affected == 0)
            {
                throw new InvalidOperationException("No se ha podido actualizar el tejido.");
            }

            await ReplaceColorsAsync(connection, transaction, centerCode, code, command.Colors, cancellationToken);
            await ReplaceCompositionAsync(connection, transaction, centerCode, code, command.Composition, cancellationToken);
            await ReplaceFinishesAsync(connection, transaction, centerCode, code, command.Finishes, cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            await AuditUpdateAsync(command.TenantId, command.CompanyId, code, previous, command, cancellationToken);
            return code;
        }

        await using var insertCommand = connection.CreateCommand();
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
                @samplePrice, @isTubular, @width2, 'local', 0, NULL);
            """;
        FillSaveParameters(insertCommand, centerCode, code, command);
        await insertCommand.ExecuteNonQueryAsync(cancellationToken);

        await ReplaceColorsAsync(connection, transaction, centerCode, code, command.Colors, cancellationToken);
        await ReplaceCompositionAsync(connection, transaction, centerCode, code, command.Composition, cancellationToken);
        await ReplaceFinishesAsync(connection, transaction, centerCode, code, command.Finishes, cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = command.TenantId,
            CompanyId = command.CompanyId,
            UserId = _currentUserContext.UserId,
            Action = "TejidoCreated",
            EntityName = "Tejido",
            EntityId = code,
            Details = $"Tejido {code} creado: {command.Description}; colores={command.Colors.Count}; composición={command.Composition.Count}; acabados={command.Finishes.Count}"
        }, cancellationToken);

        return code;
    }

    public async Task DeleteAsync(Guid tenantId, Guid companyId, string code, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return;
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);
        EnsureTenantWriteAccess();
        var centerCode = await ResolveCompanyCenterCodeAsync(tenantId, companyId, cancellationToken);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        await DeleteChildrenAsync(connection, transaction, centerCode, code, cancellationToken);

        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            UPDATE teixits
            SET origin = 'local',
                is_deleted = 1,
                synced_utc = NULL
            WHERE CENTRO = @centerCode
              AND CODI = @code;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@code", code);
        var affected = await command.ExecuteNonQueryAsync(cancellationToken);
        if (affected == 0)
        {
            throw new InvalidOperationException("No se ha encontrado el tejido a eliminar.");
        }

        await transaction.CommitAsync(cancellationToken);
        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = tenantId,
            CompanyId = companyId,
            UserId = _currentUserContext.UserId,
            Action = "TejidoDeleted",
            EntityName = "Tejido",
            EntityId = code,
            Details = $"Tejido {code} eliminado en local."
        }, cancellationToken);
    }

    private static async Task<List<TejidoColorDetailDto>> LoadColorsAsync(MySqlConnection connection, string centerCode, string code, CancellationToken cancellationToken)
    {
        var items = new List<TejidoColorDetailDto>();
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT LINE_NUMBER, PROVE, COLOR, ACTUAL, MINIM, TINTAR, PREU, METRES, KG, OBSERV
            FROM teixits_color_detail
            WHERE CENTRO = @centerCode
              AND TEIXIT_CODI = @code
            ORDER BY LINE_NUMBER;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@code", code);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new TejidoColorDetailDto
            {
                LineNumber = reader.GetInt32OrDefault("LINE_NUMBER"),
                SupplierCode = reader.GetInt32OrDefault("PROVE"),
                Color = reader.GetStringOrEmpty("COLOR"),
                CurrentStock = reader.GetDecimalOrDefault("ACTUAL"),
                MinimumStock = reader.GetDecimalOrDefault("MINIM"),
                DyeingPrice = reader.GetDecimalOrDefault("TINTAR"),
                UnitCost = reader.GetDecimalOrDefault("PREU"),
                MetersPrice = reader.GetDecimalOrDefault("METRES"),
                KilogramsPrice = reader.GetDecimalOrDefault("KG"),
                Notes = reader.GetStringOrEmpty("OBSERV")
            });
        }

        return items;
    }

    private static async Task<List<TejidoCompositionDetailDto>> LoadCompositionAsync(MySqlConnection connection, string centerCode, string code, CancellationToken cancellationToken)
    {
        var items = new List<TejidoCompositionDetailDto>();
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT LINE_NUMBER, COMP, PER, PROVE, PREU, IMPORTE
            FROM teixits_composition_detail
            WHERE CENTRO = @centerCode
              AND TEIXIT_CODI = @code
            ORDER BY LINE_NUMBER;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@code", code);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new TejidoCompositionDetailDto
            {
                LineNumber = reader.GetInt32OrDefault("LINE_NUMBER"),
                ComponentCode = reader.GetStringOrEmpty("COMP"),
                Percentage = reader.GetInt32OrDefault("PER"),
                SupplierCode = reader.GetInt32OrDefault("PROVE"),
                UnitPrice = reader.GetDecimalOrDefault("PREU"),
                Amount = reader.GetDecimalOrDefault("IMPORTE")
            });
        }

        return items;
    }

    private static async Task<List<TejidoFinishDetailDto>> LoadFinishesAsync(MySqlConnection connection, string centerCode, string code, CancellationToken cancellationToken)
    {
        var items = new List<TejidoFinishDetailDto>();
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT LINE_NUMBER, ACABAT, PROVE, ORDEN, PREUM, PREUK
            FROM teixits_finish_detail
            WHERE CENTRO = @centerCode
              AND TEIXIT_CODI = @code
            ORDER BY LINE_NUMBER;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@code", code);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new TejidoFinishDetailDto
            {
                LineNumber = reader.GetInt32OrDefault("LINE_NUMBER"),
                FinishCode = reader.GetStringOrEmpty("ACABAT"),
                SupplierCode = reader.GetInt32OrDefault("PROVE"),
                Order = reader.GetInt32OrDefault("ORDEN"),
                PricePerMeter = reader.GetDecimalOrDefault("PREUM"),
                PricePerKilogram = reader.GetDecimalOrDefault("PREUK")
            });
        }

        return items;
    }

    private static async Task ReplaceColorsAsync(MySqlConnection connection, MySqlTransaction transaction, string centerCode, string code, IReadOnlyList<SaveTejidoColorInput> colors, CancellationToken cancellationToken)
    {
        await using (var deleteCommand = connection.CreateCommand())
        {
            deleteCommand.Transaction = transaction;
            deleteCommand.CommandText =
                """
                DELETE FROM teixits_color_detail
                WHERE CENTRO = @centerCode
                  AND TEIXIT_CODI = @code;
                """;
            deleteCommand.Parameters.AddWithValue("@centerCode", centerCode);
            deleteCommand.Parameters.AddWithValue("@code", code);
            await deleteCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        if (colors.Count == 0)
        {
            return;
        }

        await using var insertCommand = connection.CreateCommand();
        insertCommand.Transaction = transaction;
        insertCommand.CommandText =
            """
            INSERT INTO teixits_color_detail (CENTRO, TEIXIT_CODI, LINE_NUMBER, PROVE, COLOR, ACTUAL, MINIM, TINTAR, PREU, METRES, KG, OBSERV)
            VALUES (@centerCode, @code, @lineNumber, @supplierCode, @color, @currentStock, @minimumStock, @dyeingPrice, @unitCost, @metersPrice, @kilogramsPrice, @notes);
            """;
        insertCommand.Parameters.Add("@centerCode", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@code", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@lineNumber", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@supplierCode", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@color", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@currentStock", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@minimumStock", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@dyeingPrice", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@unitCost", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@metersPrice", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@kilogramsPrice", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@notes", MySqlDbType.VarChar);

        for (var index = 0; index < colors.Count; index++)
        {
            var item = colors[index];
            insertCommand.Parameters["@centerCode"].Value = centerCode;
            insertCommand.Parameters["@code"].Value = code;
            insertCommand.Parameters["@lineNumber"].Value = index + 1;
            insertCommand.Parameters["@supplierCode"].Value = item.SupplierCode;
            insertCommand.Parameters["@color"].Value = DbValue(item.Color);
            insertCommand.Parameters["@currentStock"].Value = item.CurrentStock;
            insertCommand.Parameters["@minimumStock"].Value = item.MinimumStock;
            insertCommand.Parameters["@dyeingPrice"].Value = item.DyeingPrice;
            insertCommand.Parameters["@unitCost"].Value = item.UnitCost;
            insertCommand.Parameters["@metersPrice"].Value = item.MetersPrice;
            insertCommand.Parameters["@kilogramsPrice"].Value = item.KilogramsPrice;
            insertCommand.Parameters["@notes"].Value = DbValue(item.Notes);
            await insertCommand.ExecuteNonQueryAsync(cancellationToken);
        }
    }

    private static async Task ReplaceCompositionAsync(MySqlConnection connection, MySqlTransaction transaction, string centerCode, string code, IReadOnlyList<SaveTejidoCompositionInput> composition, CancellationToken cancellationToken)
    {
        await using (var deleteCommand = connection.CreateCommand())
        {
            deleteCommand.Transaction = transaction;
            deleteCommand.CommandText =
                """
                DELETE FROM teixits_composition_detail
                WHERE CENTRO = @centerCode
                  AND TEIXIT_CODI = @code;
                """;
            deleteCommand.Parameters.AddWithValue("@centerCode", centerCode);
            deleteCommand.Parameters.AddWithValue("@code", code);
            await deleteCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        if (composition.Count == 0)
        {
            return;
        }

        await using var insertCommand = connection.CreateCommand();
        insertCommand.Transaction = transaction;
        insertCommand.CommandText =
            """
            INSERT INTO teixits_composition_detail (CENTRO, TEIXIT_CODI, LINE_NUMBER, COMP, PER, PROVE, PREU, IMPORTE)
            VALUES (@centerCode, @code, @lineNumber, @componentCode, @percentage, @supplierCode, @unitPrice, @amount);
            """;
        insertCommand.Parameters.Add("@centerCode", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@code", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@lineNumber", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@componentCode", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@percentage", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@supplierCode", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@unitPrice", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@amount", MySqlDbType.Decimal);

        for (var index = 0; index < composition.Count; index++)
        {
            var item = composition[index];
            insertCommand.Parameters["@centerCode"].Value = centerCode;
            insertCommand.Parameters["@code"].Value = code;
            insertCommand.Parameters["@lineNumber"].Value = index + 1;
            insertCommand.Parameters["@componentCode"].Value = DbValue(item.ComponentCode);
            insertCommand.Parameters["@percentage"].Value = item.Percentage;
            insertCommand.Parameters["@supplierCode"].Value = item.SupplierCode;
            insertCommand.Parameters["@unitPrice"].Value = item.UnitPrice;
            insertCommand.Parameters["@amount"].Value = item.Amount;
            await insertCommand.ExecuteNonQueryAsync(cancellationToken);
        }
    }

    private static async Task ReplaceFinishesAsync(MySqlConnection connection, MySqlTransaction transaction, string centerCode, string code, IReadOnlyList<SaveTejidoFinishInput> finishes, CancellationToken cancellationToken)
    {
        await using (var deleteCommand = connection.CreateCommand())
        {
            deleteCommand.Transaction = transaction;
            deleteCommand.CommandText =
                """
                DELETE FROM teixits_finish_detail
                WHERE CENTRO = @centerCode
                  AND TEIXIT_CODI = @code;
                """;
            deleteCommand.Parameters.AddWithValue("@centerCode", centerCode);
            deleteCommand.Parameters.AddWithValue("@code", code);
            await deleteCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        if (finishes.Count == 0)
        {
            return;
        }

        await using var insertCommand = connection.CreateCommand();
        insertCommand.Transaction = transaction;
        insertCommand.CommandText =
            """
            INSERT INTO teixits_finish_detail (CENTRO, TEIXIT_CODI, LINE_NUMBER, ACABAT, PROVE, ORDEN, PREUM, PREUK)
            VALUES (@centerCode, @code, @lineNumber, @finishCode, @supplierCode, @order, @pricePerMeter, @pricePerKilogram);
            """;
        insertCommand.Parameters.Add("@centerCode", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@code", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@lineNumber", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@finishCode", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@supplierCode", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@order", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@pricePerMeter", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@pricePerKilogram", MySqlDbType.Decimal);

        for (var index = 0; index < finishes.Count; index++)
        {
            var item = finishes[index];
            insertCommand.Parameters["@centerCode"].Value = centerCode;
            insertCommand.Parameters["@code"].Value = code;
            insertCommand.Parameters["@lineNumber"].Value = index + 1;
            insertCommand.Parameters["@finishCode"].Value = DbValue(item.FinishCode);
            insertCommand.Parameters["@supplierCode"].Value = item.SupplierCode;
            insertCommand.Parameters["@order"].Value = item.Order;
            insertCommand.Parameters["@pricePerMeter"].Value = item.PricePerMeter;
            insertCommand.Parameters["@pricePerKilogram"].Value = item.PricePerKilogram;
            await insertCommand.ExecuteNonQueryAsync(cancellationToken);
        }
    }

    private static async Task DeleteChildrenAsync(MySqlConnection connection, MySqlTransaction transaction, string centerCode, string code, CancellationToken cancellationToken)
    {
        var statements = new[]
        {
            "DELETE FROM teixits_color_detail WHERE CENTRO = @centerCode AND TEIXIT_CODI = @code;",
            "DELETE FROM teixits_composition_detail WHERE CENTRO = @centerCode AND TEIXIT_CODI = @code;",
            "DELETE FROM teixits_finish_detail WHERE CENTRO = @centerCode AND TEIXIT_CODI = @code;"
        };

        foreach (var statement in statements)
        {
            await using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = statement;
            command.Parameters.AddWithValue("@centerCode", centerCode);
            command.Parameters.AddWithValue("@code", code);
            await command.ExecuteNonQueryAsync(cancellationToken);
        }
    }

    private static void FillSaveParameters(MySqlCommand command, string centerCode, string code, SaveTejidoCommand model)
    {
        command.Parameters.AddWithValue("@code", code);
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@description", model.Description);
        command.Parameters.AddWithValue("@compositionText", DbValue(model.CompositionText));
        command.Parameters.AddWithValue("@machineCode", model.MachineCode == 0 ? DBNull.Value : model.MachineCode);
        command.Parameters.AddWithValue("@materialCost", model.MaterialCost);
        command.Parameters.AddWithValue("@notes", DbValue(model.Notes));
        command.Parameters.AddWithValue("@vatCode", DbValue(model.VatCode));
        command.Parameters.AddWithValue("@weaverCode", model.WeaverCode == 0 ? DBNull.Value : model.WeaverCode);
        command.Parameters.AddWithValue("@weavingCost", model.WeavingCost);
        command.Parameters.AddWithValue("@printerCode", model.PrinterCode == 0 ? DBNull.Value : model.PrinterCode);
        command.Parameters.AddWithValue("@printingCost", model.PrintingCost);
        command.Parameters.AddWithValue("@finisherCode", model.FinisherCode == 0 ? DBNull.Value : model.FinisherCode);
        command.Parameters.AddWithValue("@finishSummary", DbValue(model.FinishSummary));
        command.Parameters.AddWithValue("@finishingCost", model.FinishingCost);
        command.Parameters.AddWithValue("@rawCost", model.RawCost);
        command.Parameters.AddWithValue("@widthText", DbValue(model.WidthText));
        command.Parameters.AddWithValue("@yield", model.Yield);
        command.Parameters.AddWithValue("@margin", model.Margin);
        command.Parameters.AddWithValue("@gramWeight", model.GramWeight);
        command.Parameters.AddWithValue("@pricePerMeter", model.PricePerMeter);
        command.Parameters.AddWithValue("@pricePerKilogram", model.PricePerKilogram);
        command.Parameters.AddWithValue("@rawStockMeters", model.RawStockMeters);
        command.Parameters.AddWithValue("@availableStockMeters", model.AvailableStockMeters);
        command.Parameters.AddWithValue("@rawStockKilograms", model.RawStockKilograms);
        command.Parameters.AddWithValue("@availableStockKilograms", model.AvailableStockKilograms);
        command.Parameters.AddWithValue("@samplePrice", model.SamplePrice == 0 ? DBNull.Value : model.SamplePrice);
        command.Parameters.AddWithValue("@isTubular", model.IsTubular);
        command.Parameters.AddWithValue("@width2", model.Width2);
    }

    private async Task AuditUpdateAsync(Guid tenantId, Guid companyId, string code, TejidoDetailDto previous, SaveTejidoCommand current, CancellationToken cancellationToken)
    {
        var changes = new List<string>();
        Compare(changes, "Descripción", previous.Description, current.Description);
        Compare(changes, "Composición", previous.CompositionText, current.CompositionText);
        Compare(changes, "Tejedor", previous.WeaverCode.ToString(), current.WeaverCode.ToString());
        Compare(changes, "Acabador", previous.FinisherCode.ToString(), current.FinisherCode.ToString());
        Compare(changes, "Precio metro", previous.PricePerMeter.ToString("0.####"), current.PricePerMeter.ToString("0.####"));
        Compare(changes, "Gramaje", previous.GramWeight.ToString("0.####"), current.GramWeight.ToString("0.####"));

        if (previous.Colors.Count != current.Colors.Count)
        {
            changes.Add($"Carta de colores: {previous.Colors.Count} -> {current.Colors.Count}");
        }

        if (previous.Composition.Count != current.Composition.Count)
        {
            changes.Add($"Composición: {previous.Composition.Count} -> {current.Composition.Count}");
        }

        if (previous.Finishes.Count != current.Finishes.Count)
        {
            changes.Add($"Acabados: {previous.Finishes.Count} -> {current.Finishes.Count}");
        }

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = tenantId,
            CompanyId = companyId,
            UserId = _currentUserContext.UserId,
            Action = "TejidoUpdated",
            EntityName = "Tejido",
            EntityId = code,
            Details = changes.Count == 0
                ? $"Tejido {code} actualizado sin cambios detectados."
                : $"Tejido {code} actualizado: {string.Join("; ", changes)}"
        }, cancellationToken);
    }

    private async Task<string> ResolveCompanyCenterCodeAsync(Guid tenantId, Guid companyId, CancellationToken cancellationToken)
    {
        var allowedCompanies = await _companyAccessService.GetAllowedCompaniesAsync(_currentUserContext.UserId!.Value, tenantId, cancellationToken);
        var company = allowedCompanies.FirstOrDefault(item => item.CompanyId == companyId);
        if (company is null || string.IsNullOrWhiteSpace(company.LegacyCenterCode))
        {
            throw new InvalidOperationException("La empresa activa no tiene centro legacy configurado.");
        }

        return company.LegacyCenterCode.Trim().ToUpperInvariant();
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

        throw new InvalidOperationException("No tienes permisos para editar tejidos en este tenant.");
    }

    private async Task EnsureCompanyAccessAsync(Guid tenantId, Guid companyId, CancellationToken cancellationToken)
    {
        if (!_currentUserContext.IsAuthenticated || !_currentUserContext.UserId.HasValue)
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

        var allowedCompanies = await _companyAccessService.GetAllowedCompaniesAsync(_currentUserContext.UserId.Value, tenantId, cancellationToken);
        if (!allowedCompanies.Any(company => company.CompanyId == companyId))
        {
            throw new InvalidOperationException("No tienes acceso a la empresa activa.");
        }
    }

    private async Task<bool> CanReadCompanyAsync(Guid tenantId, Guid companyId, CancellationToken cancellationToken)
    {
        if (!_currentUserContext.IsAuthenticated || !_currentUserContext.UserId.HasValue)
        {
            return false;
        }

        if (!_tenantContext.TenantId.HasValue || _tenantContext.TenantId.Value != tenantId)
        {
            return false;
        }

        if (!_activeCompanyContext.CompanyId.HasValue || _activeCompanyContext.CompanyId.Value != companyId)
        {
            return false;
        }

        var allowedCompanies = await _companyAccessService.GetAllowedCompaniesAsync(_currentUserContext.UserId.Value, tenantId, cancellationToken);
        return allowedCompanies.Any(company => company.CompanyId == companyId);
    }

    private static void Validate(SaveTejidoCommand command)
    {
        command.Code = command.Code?.Trim().ToUpperInvariant();
        command.Description = command.Description.Trim();
        command.CompositionText = command.CompositionText.Trim();
        command.Notes = command.Notes.Trim();
        command.VatCode = command.VatCode.Trim().ToUpperInvariant();
        command.FinishSummary = command.FinishSummary.Trim();
        command.WidthText = command.WidthText.Trim();

        if (string.IsNullOrWhiteSpace(command.Code))
        {
            throw new InvalidOperationException("Debes indicar un código para el tejido.");
        }

        if (command.Code.Length > 10)
        {
            throw new InvalidOperationException("El código del tejido no puede superar 10 caracteres.");
        }

        if (string.IsNullOrWhiteSpace(command.Description) || command.Description.Length < 2)
        {
            throw new InvalidOperationException("La descripción del tejido es obligatoria y debe tener al menos 2 caracteres.");
        }

        if (command.MaterialCost < 0 || command.WeavingCost < 0 || command.PrintingCost < 0 || command.FinishingCost < 0 || command.RawCost < 0)
        {
            throw new InvalidOperationException("Los costes del tejido no pueden ser negativos.");
        }

        if (command.Margin < 0 || command.Yield < 0 || command.GramWeight < 0 || command.PricePerMeter < 0 || command.PricePerKilogram < 0)
        {
            throw new InvalidOperationException("Margen, rendimiento, gramaje y precios no pueden ser negativos.");
        }

        var colorKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        for (var index = 0; index < command.Colors.Count; index++)
        {
            var item = command.Colors[index];
            item.Color = item.Color.Trim();
            item.Notes = item.Notes.Trim();
            if (item.CurrentStock < 0 || item.MinimumStock < 0 || item.DyeingPrice < 0 || item.UnitCost < 0 || item.MetersPrice < 0 || item.KilogramsPrice < 0)
            {
                throw new InvalidOperationException($"La línea {index + 1} de colores no admite valores negativos.");
            }

            var duplicateKey = $"{item.SupplierCode}|{item.Color}";
            if (!colorKeys.Add(duplicateKey))
            {
                throw new InvalidOperationException($"La línea {index + 1} repite color/proveedor dentro del mismo tejido.");
            }
        }

        var totalPercentage = 0;
        for (var index = 0; index < command.Composition.Count; index++)
        {
            var item = command.Composition[index];
            item.ComponentCode = item.ComponentCode.Trim().ToUpperInvariant();
            if (item.Percentage < 0 || item.UnitPrice < 0 || item.Amount < 0)
            {
                throw new InvalidOperationException($"La línea {index + 1} de composición no admite valores negativos.");
            }

            totalPercentage += item.Percentage;
        }

        if (totalPercentage > 100)
        {
            throw new InvalidOperationException("La composición no puede superar el 100%.");
        }

        var finishKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        for (var index = 0; index < command.Finishes.Count; index++)
        {
            var item = command.Finishes[index];
            item.FinishCode = item.FinishCode.Trim().ToUpperInvariant();
            if (item.Order < 0 || item.PricePerMeter < 0 || item.PricePerKilogram < 0)
            {
                throw new InvalidOperationException($"La línea {index + 1} de acabados no admite valores negativos.");
            }

            var duplicateKey = $"{item.SupplierCode}|{item.FinishCode}";
            if (!finishKeys.Add(duplicateKey))
            {
                throw new InvalidOperationException($"La línea {index + 1} repite acabado/proveedor dentro del mismo tejido.");
            }
        }
    }

    private static void Compare(ICollection<string> changes, string label, string previous, string current)
    {
        var before = previous.Trim();
        var after = current.Trim();
        if (string.Equals(before, after, StringComparison.Ordinal))
        {
            return;
        }

        changes.Add($"{label}: '{before}' -> '{after}'");
    }

    private static object DbValue(string value) =>
        string.IsNullOrWhiteSpace(value) ? DBNull.Value : value;

    private static string BuildSearchOrderByClause(TejidoFilter filter)
    {
        var column = filter.SortColumn switch
        {
            nameof(TejidoListItemDto.Code) => "CODI",
            nameof(TejidoListItemDto.Description) => "DESCRI",
            nameof(TejidoListItemDto.MachineCode) => "MAQUI",
            nameof(TejidoListItemDto.WeaverCode) => "TEIXIDOR",
            nameof(TejidoListItemDto.FinisherCode) => "ACABADOR",
            nameof(TejidoListItemDto.WidthText) => "COALESCE(NULLIF(AMPLE2, 0), NULLIF(CAST(AMPLE AS DECIMAL(12,4)), 0), 0)",
            nameof(TejidoListItemDto.GramWeight) => "GRAMA",
            nameof(TejidoListItemDto.PricePerMeter) => "PREUM",
            nameof(TejidoListItemDto.PricePerKilogram) => "PREUK",
            nameof(TejidoListItemDto.AvailableStockMeters) => "STDISPM",
            _ => string.Empty
        };

        if (string.IsNullOrWhiteSpace(column))
        {
            return string.IsNullOrWhiteSpace(filter.Search)
                ? "ORDER BY CODI"
                : "ORDER BY DESCRI, CODI";
        }

        var direction = filter.SortDescending ? "DESC" : "ASC";
        return $"ORDER BY {column} {direction}, CODI";
    }
}
