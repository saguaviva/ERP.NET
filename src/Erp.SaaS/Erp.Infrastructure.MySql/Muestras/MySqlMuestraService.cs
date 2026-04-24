using Erp.Application.Auditing;
using Erp.Application.Companies;
using Erp.Application.Contexts;
using Erp.Application.Muestras;
using Erp.Domain.Common;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Support;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Muestras;

public sealed class MySqlMuestraService : IMuestraQueries, IMuestraService
{
    private readonly MySqlConnectionFactory _connectionFactory;
    private readonly IAuditLogService _auditLogService;
    private readonly ICompanyAccessService _companyAccessService;
    private readonly ICurrentUserContext _currentUserContext;
    private readonly ITenantContext _tenantContext;
    private readonly IActiveCompanyContext _activeCompanyContext;

    public MySqlMuestraService(
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

    public async Task<MuestraSearchResultDto> SearchAsync(Guid tenantId, Guid companyId, MuestraFilter filter, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return new MuestraSearchResultDto();
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);
        var centerCode = await ResolveCompanyCenterCodeAsync(tenantId, companyId, cancellationToken);
        var pageSize = Math.Clamp(filter.PageSize, 10, 200);
        var page = Math.Max(filter.Page, 1);
        var offset = (page - 1) * pageSize;
        var search = filter.Search?.Trim() ?? string.Empty;
        var likeSearch = $"%{search}%";
        var machineCode = filter.MachineCode.GetValueOrDefault();

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var countCommand = connection.CreateCommand();
        countCommand.CommandText =
            """
            SELECT COUNT(*)
            FROM mostres
            WHERE CENTRO = @centerCode
              AND is_deleted = 0
              AND (@machineCode = 0 OR MAQUINA = @machineCode)
              AND (
                    @search = ''
                    OR CODI LIKE @likeSearch
                    OR DESCRI LIKE @likeSearch
                    OR COALESCE(NOMCLIENT, '') LIKE @likeSearch
                    OR COALESCE(REFE, '') LIKE @likeSearch
                    OR COALESCE(TEMP, '') LIKE @likeSearch
                    OR CAST(MAQUINA AS CHAR) LIKE @likeSearch
                    OR COALESCE(NOMMAQUI, '') LIKE @likeSearch
                    OR COALESCE(OBSERV, '') LIKE @likeSearch
                  );
            """;
        countCommand.Parameters.AddWithValue("@centerCode", centerCode);
        countCommand.Parameters.AddWithValue("@machineCode", machineCode);
        countCommand.Parameters.AddWithValue("@search", search);
        countCommand.Parameters.AddWithValue("@likeSearch", likeSearch);
        var totalCount = Convert.ToInt32(await countCommand.ExecuteScalarAsync(cancellationToken));
        if (totalCount == 0)
        {
            return new MuestraSearchResultDto { TotalCount = 0 };
        }

        var items = new List<MuestraListItemDto>();
        await using var command = connection.CreateCommand();
        command.CommandText =
            $"""
            SELECT m.CODI,
                   m.CENTRO,
                   m.DESCRI,
                   m.CLIENT,
                   COALESCE(m.NOMCLIENT, '') AS NOMCLIENT,
                   COALESCE(m.REFE, '') AS REFE,
                   COALESCE(m.TEMP, '') AS TEMP,
                   m.MAQUINA,
                   COALESCE(m.NOMMAQUI, '') AS NOMMAQUI,
                   m.PREU,
                   m.origin,
                   (
                       SELECT COUNT(*)
                       FROM mostres_detail detail_rows
                       WHERE detail_rows.CENTRO = m.CENTRO
                         AND detail_rows.MOSTRA_CODI = m.CODI
                   ) AS DETAIL_LINES
            FROM mostres m
            WHERE m.CENTRO = @centerCode
              AND m.is_deleted = 0
              AND (@machineCode = 0 OR m.MAQUINA = @machineCode)
              AND (
                    @search = ''
                    OR m.CODI LIKE @likeSearch
                    OR m.DESCRI LIKE @likeSearch
                    OR COALESCE(m.NOMCLIENT, '') LIKE @likeSearch
                    OR COALESCE(m.REFE, '') LIKE @likeSearch
                    OR COALESCE(m.TEMP, '') LIKE @likeSearch
                    OR CAST(m.MAQUINA AS CHAR) LIKE @likeSearch
                    OR COALESCE(m.NOMMAQUI, '') LIKE @likeSearch
                    OR COALESCE(m.OBSERV, '') LIKE @likeSearch
                  )
            {BuildSearchOrderByClause(filter)}
            LIMIT @limit OFFSET @offset;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@machineCode", machineCode);
        command.Parameters.AddWithValue("@search", search);
        command.Parameters.AddWithValue("@likeSearch", likeSearch);
        command.Parameters.AddWithValue("@limit", pageSize);
        command.Parameters.AddWithValue("@offset", offset);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new MuestraListItemDto
            {
                Code = reader.GetStringOrEmpty("CODI"),
                CompanyCenterCode = reader.GetStringOrEmpty("CENTRO"),
                Description = reader.GetStringOrEmpty("DESCRI"),
                ClientCode = reader.GetInt32OrDefault("CLIENT"),
                ClientName = reader.GetStringOrEmpty("NOMCLIENT"),
                Reference = reader.GetStringOrEmpty("REFE"),
                Season = reader.GetStringOrEmpty("TEMP"),
                MachineCode = reader.GetInt32OrDefault("MAQUINA"),
                MachineName = reader.GetStringOrEmpty("NOMMAQUI"),
                UnitPrice = reader.GetDecimalOrDefault("PREU"),
                Origin = reader.GetStringOrEmpty("origin"),
                DetailLinesCount = reader.GetInt32OrDefault("DETAIL_LINES")
            });
        }

        return new MuestraSearchResultDto
        {
            Items = items,
            TotalCount = totalCount
        };
    }

    public async Task<MuestraDetailDto?> GetByCodeAsync(Guid tenantId, Guid companyId, string code, CancellationToken cancellationToken = default)
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
            SELECT CODI,
                   CENTRO,
                   DESCRI,
                   CLIENT,
                   COALESCE(NOMCLIENT, '') AS NOMCLIENT,
                   COALESCE(REFE, '') AS REFE,
                   COALESCE(TEMP, '') AS TEMP,
                   MAQUINA,
                   COALESCE(NOMMAQUI, '') AS NOMMAQUI,
                   COALESCE(MARGE, 0) AS MARGE,
                   COALESCE(IVA, '') AS IVA,
                   COALESCE(OBSERV, '') AS OBSERV,
                   COALESCE(COMPO, '') AS COMPO,
                   COALESCE(PREU, 0) AS PREU,
                   origin
            FROM mostres
            WHERE CENTRO = @centerCode
              AND CODI = @code
              AND is_deleted = 0
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@code", code);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        var detail = new MuestraDetailDto
        {
            Code = reader.GetStringOrEmpty("CODI"),
            CompanyCenterCode = reader.GetStringOrEmpty("CENTRO"),
            Description = reader.GetStringOrEmpty("DESCRI"),
            ClientCode = reader.GetInt32OrDefault("CLIENT"),
            ClientName = reader.GetStringOrEmpty("NOMCLIENT"),
            Reference = reader.GetStringOrEmpty("REFE"),
            Season = reader.GetStringOrEmpty("TEMP"),
            MachineCode = reader.GetInt32OrDefault("MAQUINA"),
            MachineName = reader.GetStringOrEmpty("NOMMAQUI"),
            MarginPercent = reader.GetDecimalOrDefault("MARGE"),
            VatCode = reader.GetStringOrEmpty("IVA"),
            Notes = reader.GetStringOrEmpty("OBSERV"),
            Composition = reader.GetStringOrEmpty("COMPO"),
            UnitPrice = reader.GetDecimalOrDefault("PREU"),
            Origin = reader.GetStringOrEmpty("origin")
        };

        await reader.CloseAsync();
        detail.Lines = await LoadLinesAsync(connection, centerCode, detail.Code, cancellationToken);
        detail.Breakdowns = await LoadBreakdownsAsync(connection, centerCode, detail.Code, cancellationToken);
        AttachBreakdownsToLines(detail);
        return detail;
    }

    public async Task<MuestraDetailDto?> GetByIdentityAsync(Guid tenantId, Guid companyId, string code, int clientCode, CancellationToken cancellationToken = default)
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
            SELECT CODI,
                   CENTRO,
                   DESCRI,
                   CLIENT,
                   COALESCE(NOMCLIENT, '') AS NOMCLIENT,
                   COALESCE(REFE, '') AS REFE,
                   COALESCE(TEMP, '') AS TEMP,
                   MAQUINA,
                   COALESCE(NOMMAQUI, '') AS NOMMAQUI,
                   COALESCE(MARGE, 0) AS MARGE,
                   COALESCE(IVA, '') AS IVA,
                   COALESCE(OBSERV, '') AS OBSERV,
                   COALESCE(COMPO, '') AS COMPO,
                   COALESCE(PREU, 0) AS PREU,
                   origin
            FROM mostres
            WHERE CENTRO = @centerCode
              AND CODI = @code
              AND CLIENT = @clientCode
              AND is_deleted = 0
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@code", code);
        command.Parameters.AddWithValue("@clientCode", clientCode);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        var detail = new MuestraDetailDto
        {
            Code = reader.GetStringOrEmpty("CODI"),
            CompanyCenterCode = reader.GetStringOrEmpty("CENTRO"),
            Description = reader.GetStringOrEmpty("DESCRI"),
            ClientCode = reader.GetInt32OrDefault("CLIENT"),
            ClientName = reader.GetStringOrEmpty("NOMCLIENT"),
            Reference = reader.GetStringOrEmpty("REFE"),
            Season = reader.GetStringOrEmpty("TEMP"),
            MachineCode = reader.GetInt32OrDefault("MAQUINA"),
            MachineName = reader.GetStringOrEmpty("NOMMAQUI"),
            MarginPercent = reader.GetDecimalOrDefault("MARGE"),
            VatCode = reader.GetStringOrEmpty("IVA"),
            Notes = reader.GetStringOrEmpty("OBSERV"),
            Composition = reader.GetStringOrEmpty("COMPO"),
            UnitPrice = reader.GetDecimalOrDefault("PREU"),
            Origin = reader.GetStringOrEmpty("origin")
        };

        await reader.CloseAsync();
        detail.Lines = await LoadLinesAsync(connection, centerCode, detail.Code, cancellationToken);
        detail.Breakdowns = await LoadBreakdownsAsync(connection, centerCode, detail.Code, cancellationToken);
        AttachBreakdownsToLines(detail);
        return detail;
    }

    public async Task<string> SaveAsync(SaveMuestraCommand command, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return string.Empty;
        }

        await EnsureCompanyAccessAsync(command.TenantId, command.CompanyId, cancellationToken);
        EnsureTenantWriteAccess();
        Validate(command);
        var centerCode = await ResolveCompanyCenterCodeAsync(command.TenantId, command.CompanyId, cancellationToken);
        var code = command.Code!.Trim().ToUpperInvariant();

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        MuestraDetailDto? previous = null;
        if (command.IsNew)
        {
            var duplicate = await GetByCodeAsync(command.TenantId, command.CompanyId, code, cancellationToken);
            if (duplicate is not null)
            {
                throw new InvalidOperationException("Ya existe una muestra con este código.");
            }
        }
        else
        {
            previous = await GetByCodeAsync(command.TenantId, command.CompanyId, code, cancellationToken);
            if (previous is null)
            {
                throw new InvalidOperationException("No se ha encontrado la muestra que intentas modificar.");
            }
        }

        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);
        if (previous is null)
        {
            await using var insertCommand = connection.CreateCommand();
            insertCommand.Transaction = transaction;
            insertCommand.CommandText =
                """
                INSERT INTO mostres (
                    CODI, CENTRO, DESCRI, CLIENT, NOMCLIENT, REFE, TEMP,
                    MAQUINA, NOMMAQUI, MARGE, IVA, OBSERV, COMPO, PREU,
                    origin, is_deleted, synced_utc)
                VALUES (
                    @code, @centerCode, @description, @clientCode, @clientName, @reference, @season,
                    @machineCode, @machineName, @marginPercent, @vatCode, @notes, @composition, @unitPrice,
                    'local', 0, NULL);
                """;
            FillHeaderParameters(insertCommand, centerCode, code, command);
            await insertCommand.ExecuteNonQueryAsync(cancellationToken);
        }
        else
        {
            await using var updateCommand = connection.CreateCommand();
            updateCommand.Transaction = transaction;
            updateCommand.CommandText =
                """
                UPDATE mostres
                SET DESCRI = @description,
                    CLIENT = @clientCode,
                    NOMCLIENT = @clientName,
                    REFE = @reference,
                    TEMP = @season,
                    MAQUINA = @machineCode,
                    NOMMAQUI = @machineName,
                    MARGE = @marginPercent,
                    IVA = @vatCode,
                    OBSERV = @notes,
                    COMPO = @composition,
                    PREU = @unitPrice,
                    origin = 'local',
                    is_deleted = 0,
                    synced_utc = NULL
                WHERE CENTRO = @centerCode
                  AND CODI = @code;
                """;
            FillHeaderParameters(updateCommand, centerCode, code, command);
            var affected = await updateCommand.ExecuteNonQueryAsync(cancellationToken);
            if (affected == 0)
            {
                throw new InvalidOperationException("No se ha podido actualizar la muestra.");
            }
        }

        await ReplaceLinesAsync(connection, transaction, centerCode, code, command.Lines, cancellationToken);
        await ReplaceBreakdownsAsync(connection, transaction, centerCode, code, command.Breakdowns, cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = command.TenantId,
            CompanyId = command.CompanyId,
            UserId = _currentUserContext.UserId,
            Action = previous is null ? "MuestraCreated" : "MuestraUpdated",
            EntityName = "Muestra",
            EntityId = code,
            Details = previous is null
                ? $"Muestra {code} creada: {command.Description}; detalle={command.Lines.Count}"
                : $"Muestra {code} actualizada: {command.Description}; detalle={command.Lines.Count}"
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
        await using (var deleteBreakdownLines = connection.CreateCommand())
        {
            deleteBreakdownLines.Transaction = transaction;
            deleteBreakdownLines.CommandText =
                """
                DELETE FROM mostres_breakdown_lines
                WHERE CENTRO = @centerCode
                  AND MOSTRA_CODI = @code;
                """;
            deleteBreakdownLines.Parameters.AddWithValue("@centerCode", centerCode);
            deleteBreakdownLines.Parameters.AddWithValue("@code", code);
            await deleteBreakdownLines.ExecuteNonQueryAsync(cancellationToken);
        }

        await using (var deleteBreakdownHeaders = connection.CreateCommand())
        {
            deleteBreakdownHeaders.Transaction = transaction;
            deleteBreakdownHeaders.CommandText =
                """
                DELETE FROM mostres_breakdown
                WHERE CENTRO = @centerCode
                  AND MOSTRA_CODI = @code;
                """;
            deleteBreakdownHeaders.Parameters.AddWithValue("@centerCode", centerCode);
            deleteBreakdownHeaders.Parameters.AddWithValue("@code", code);
            await deleteBreakdownHeaders.ExecuteNonQueryAsync(cancellationToken);
        }

        await using (var deleteDetail = connection.CreateCommand())
        {
            deleteDetail.Transaction = transaction;
            deleteDetail.CommandText =
                """
                DELETE FROM mostres_detail
                WHERE CENTRO = @centerCode
                  AND MOSTRA_CODI = @code;
                """;
            deleteDetail.Parameters.AddWithValue("@centerCode", centerCode);
            deleteDetail.Parameters.AddWithValue("@code", code);
            await deleteDetail.ExecuteNonQueryAsync(cancellationToken);
        }

        await using var deleteHeader = connection.CreateCommand();
        deleteHeader.Transaction = transaction;
        deleteHeader.CommandText =
            """
            UPDATE mostres
            SET origin = 'local',
                is_deleted = 1,
                synced_utc = NULL
            WHERE CENTRO = @centerCode
              AND CODI = @code;
            """;
        deleteHeader.Parameters.AddWithValue("@centerCode", centerCode);
        deleteHeader.Parameters.AddWithValue("@code", code);
        var affected = await deleteHeader.ExecuteNonQueryAsync(cancellationToken);
        if (affected == 0)
        {
            throw new InvalidOperationException("No se ha encontrado la muestra a eliminar.");
        }

        await transaction.CommitAsync(cancellationToken);
        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = tenantId,
            CompanyId = companyId,
            UserId = _currentUserContext.UserId,
            Action = "MuestraDeleted",
            EntityName = "Muestra",
            EntityId = code,
            Details = $"Muestra {code} eliminada en local."
        }, cancellationToken);
    }

    private static void FillHeaderParameters(MySqlCommand command, string centerCode, string code, SaveMuestraCommand model)
    {
        command.Parameters.AddWithValue("@code", code);
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@description", model.Description);
        command.Parameters.AddWithValue("@clientCode", model.ClientCode);
        command.Parameters.AddWithValue("@clientName", DbValue(model.ClientName));
        command.Parameters.AddWithValue("@reference", DbValue(model.Reference));
        command.Parameters.AddWithValue("@season", DbValue(model.Season));
        command.Parameters.AddWithValue("@machineCode", model.MachineCode);
        command.Parameters.AddWithValue("@machineName", DbValue(model.MachineName));
        command.Parameters.AddWithValue("@marginPercent", model.MarginPercent);
        command.Parameters.AddWithValue("@vatCode", DbValue(model.VatCode));
        command.Parameters.AddWithValue("@notes", DbValue(model.Notes));
        command.Parameters.AddWithValue("@composition", DbValue(model.Composition));
        command.Parameters.AddWithValue("@unitPrice", model.UnitPrice);
    }

    private static async Task<List<MuestraLineDto>> LoadLinesAsync(
        MySqlConnection connection,
        string centerCode,
        string code,
        CancellationToken cancellationToken)
    {
        var items = new List<MuestraLineDto>();
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT LINE_NUMBER,
                   COALESCE(TALLA, '') AS TALLA,
                   COALESCE(TALLAH, '') AS TALLAH,
                   COALESCE(TALLAL, '') AS TALLAL,
                   COALESCE(DESCRI, '') AS DESCRI,
                   COALESCE(COST, 0) AS COST,
                   COALESCE(VENDA, 0) AS VENDA,
                   COALESCE(COLOR, '') AS COLOR,
                   COALESCE(CLIENT, 0) AS CLIENT,
                   COALESCE(NOMCLIENT, '') AS NOMCLIENT,
                   COALESCE(NCCODE, '') AS NCCODE
            FROM mostres_detail
            WHERE CENTRO = @centerCode
              AND MOSTRA_CODI = @code
            ORDER BY LINE_NUMBER;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@code", code);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new MuestraLineDto
            {
                LineNumber = reader.GetInt32OrDefault("LINE_NUMBER"),
                SizeCode = reader.GetStringOrEmpty("TALLA"),
                SizeHigh = reader.GetStringOrEmpty("TALLAH"),
                SizeLow = reader.GetStringOrEmpty("TALLAL"),
                Description = reader.GetStringOrEmpty("DESCRI"),
                CostPrice = reader.GetDecimalOrDefault("COST"),
                SalePrice = reader.GetDecimalOrDefault("VENDA"),
                Color = reader.GetStringOrEmpty("COLOR"),
                ClientCode = reader.GetInt32OrDefault("CLIENT"),
                ClientName = reader.GetStringOrEmpty("NOMCLIENT"),
                NcCode = reader.GetStringOrEmpty("NCCODE")
            });
        }

        return items;
    }

    private static async Task<List<MuestraBreakdownDto>> LoadBreakdownsAsync(
        MySqlConnection connection,
        string centerCode,
        string code,
        CancellationToken cancellationToken)
    {
        var items = new List<MuestraBreakdownDto>();
        var lookup = new Dictionary<int, MuestraBreakdownDto>();

        await using (var command = connection.CreateCommand())
        {
            command.CommandText =
                """
                SELECT SAMPLE_LINE_NUMBER,
                       DATA,
                       COALESCE(CLIENT, 0) AS CLIENT,
                       COALESCE(NOMCLIENT, '') AS NOMCLIENT,
                       COALESCE(MAQUINA, 0) AS MAQUINA,
                       COALESCE(NOMMAQUI, '') AS NOMMAQUI,
                       COALESCE(OPERACIO, 0) AS OPERACIO,
                       COALESCE(NOMOPER, '') AS NOMOPER,
                       COALESCE(AGULLES, 0) AS AGULLES,
                       COALESCE(VELOSITAT, 0) AS VELOSITAT,
                       COALESCE(DISCO, '') AS DISCO,
                       COALESCE(TEMPS, 0) AS TEMPS,
                       COALESCE(MACHINE_RATE, 0) AS MACHINE_RATE,
                       COALESCE(MACHINE_IMPORT, 0) AS MACHINE_IMPORT,
                       COALESCE(CORTES, '') AS CORTES,
                       COALESCE(NOTES, '') AS NOTES
                FROM mostres_breakdown
                WHERE CENTRO = @centerCode
                  AND MOSTRA_CODI = @code
                  AND is_deleted = 0
                ORDER BY SAMPLE_LINE_NUMBER;
                """;
            command.Parameters.AddWithValue("@centerCode", centerCode);
            command.Parameters.AddWithValue("@code", code);

            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                var item = new MuestraBreakdownDto
                {
                    SampleLineNumber = reader.GetInt32OrDefault("SAMPLE_LINE_NUMBER"),
                    WorkDate = reader.IsDBNull(reader.GetOrdinal("DATA")) ? null : reader.GetDateTime("DATA"),
                    ClientCode = reader.GetInt32OrDefault("CLIENT"),
                    ClientName = reader.GetStringOrEmpty("NOMCLIENT"),
                    MachineCode = reader.GetInt32OrDefault("MAQUINA"),
                    MachineName = reader.GetStringOrEmpty("NOMMAQUI"),
                    OperationCode = reader.GetInt32OrDefault("OPERACIO"),
                    OperationName = reader.GetStringOrEmpty("NOMOPER"),
                    Needles = reader.GetDecimalOrDefault("AGULLES"),
                    Speed = reader.GetDecimalOrDefault("VELOSITAT"),
                    Disk = reader.GetStringOrEmpty("DISCO"),
                    TimeMinutes = reader.GetDecimalOrDefault("TEMPS"),
                    MachineRate = reader.GetDecimalOrDefault("MACHINE_RATE"),
                    MachineImport = reader.GetDecimalOrDefault("MACHINE_IMPORT"),
                    Cuts = reader.GetStringOrEmpty("CORTES"),
                    Notes = reader.GetStringOrEmpty("NOTES")
                };

                items.Add(item);
                lookup[item.SampleLineNumber] = item;
            }
        }

        if (items.Count == 0)
        {
            return items;
        }

        await using var detailCommand = connection.CreateCommand();
        detailCommand.CommandText =
            """
            SELECT SAMPLE_LINE_NUMBER,
                   LINE_NUMBER,
                   COALESCE(TEIXIT, '') AS TEIXIT,
                   COALESCE(PROVE, 0) AS PROVE,
                   COALESCE(NOMPROVE, '') AS NOMPROVE,
                   COALESCE(COLOR, '') AS COLOR,
                   COALESCE(FIL, 0) AS FIL,
                   COALESCE(CAPS, 0) AS CAPS,
                   COALESCE(PASSADES, 0) AS PASSADES,
                   COALESCE(GRADUACION, 0) AS GRADUACION,
                   COALESCE(CONSUM, 0) AS CONSUM,
                   COALESCE(PREU, 0) AS PREU,
                   COALESCE(IMPORT, 0) AS IMPORT
            FROM mostres_breakdown_lines
            WHERE CENTRO = @centerCode
              AND MOSTRA_CODI = @code
            ORDER BY SAMPLE_LINE_NUMBER, LINE_NUMBER;
            """;
        detailCommand.Parameters.AddWithValue("@centerCode", centerCode);
        detailCommand.Parameters.AddWithValue("@code", code);
        await using var detailReader = await detailCommand.ExecuteReaderAsync(cancellationToken);
        while (await detailReader.ReadAsync(cancellationToken))
        {
            var sampleLineNumber = detailReader.GetInt32OrDefault("SAMPLE_LINE_NUMBER");
            if (!lookup.TryGetValue(sampleLineNumber, out var breakdown))
            {
                continue;
            }

            breakdown.Lines.Add(new MuestraBreakdownLineDto
            {
                LineNumber = detailReader.GetInt32OrDefault("LINE_NUMBER"),
                YarnCode = detailReader.GetStringOrEmpty("TEIXIT"),
                ProviderCode = detailReader.GetInt32OrDefault("PROVE"),
                ProviderName = detailReader.GetStringOrEmpty("NOMPROVE"),
                MaterialColor = detailReader.GetStringOrEmpty("COLOR"),
                YarnMetric = detailReader.GetDecimalOrDefault("FIL"),
                Ends = detailReader.GetDecimalOrDefault("CAPS"),
                Passes = detailReader.GetDecimalOrDefault("PASSADES"),
                Graduation = detailReader.GetInt32OrDefault("GRADUACION"),
                Consumption = detailReader.GetDecimalOrDefault("CONSUM"),
                Price = detailReader.GetDecimalOrDefault("PREU"),
                ImportAmount = detailReader.GetDecimalOrDefault("IMPORT")
            });
        }

        return items;
    }

    private static void AttachBreakdownsToLines(MuestraDetailDto detail)
    {
        var lookup = detail.Breakdowns.ToDictionary(item => item.SampleLineNumber);
        foreach (var line in detail.Lines)
        {
            if (lookup.TryGetValue(line.LineNumber, out var breakdown))
            {
                line.Breakdown = breakdown;
            }
            else
            {
                line.Breakdown = CreateDefaultBreakdown(line, detail);
            }
        }
    }

    private static async Task ReplaceLinesAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        string centerCode,
        string code,
        IReadOnlyList<SaveMuestraLineInput> lines,
        CancellationToken cancellationToken)
    {
        await using (var deleteCommand = connection.CreateCommand())
        {
            deleteCommand.Transaction = transaction;
            deleteCommand.CommandText =
                """
                DELETE FROM mostres_detail
                WHERE CENTRO = @centerCode
                  AND MOSTRA_CODI = @code;
                """;
            deleteCommand.Parameters.AddWithValue("@centerCode", centerCode);
            deleteCommand.Parameters.AddWithValue("@code", code);
            await deleteCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        if (lines.Count == 0)
        {
            return;
        }

        await using var insertCommand = connection.CreateCommand();
        insertCommand.Transaction = transaction;
        insertCommand.CommandText =
            """
            INSERT INTO mostres_detail (
                CENTRO, MOSTRA_CODI, LINE_NUMBER, TALLA, TALLAH, TALLAL, DESCRI,
                COST, VENDA, COLOR, CLIENT, NOMCLIENT, NCCODE)
            VALUES (
                @centerCode, @code, @lineNumber, @sizeCode, @sizeHigh, @sizeLow, @description,
                @costPrice, @salePrice, @color, @clientCode, @clientName, @ncCode);
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
        insertCommand.Parameters.Add("@clientName", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@ncCode", MySqlDbType.VarChar);

        for (var index = 0; index < lines.Count; index++)
        {
            var line = lines[index];
            insertCommand.Parameters["@centerCode"].Value = centerCode;
            insertCommand.Parameters["@code"].Value = code;
            insertCommand.Parameters["@lineNumber"].Value = index + 1;
            insertCommand.Parameters["@sizeCode"].Value = DbValue(line.SizeCode);
            insertCommand.Parameters["@sizeHigh"].Value = DbValue(line.SizeHigh);
            insertCommand.Parameters["@sizeLow"].Value = DbValue(line.SizeLow);
            insertCommand.Parameters["@description"].Value = DbValue(line.Description);
            insertCommand.Parameters["@costPrice"].Value = line.CostPrice;
            insertCommand.Parameters["@salePrice"].Value = line.SalePrice;
            insertCommand.Parameters["@color"].Value = DbValue(line.Color);
            insertCommand.Parameters["@clientCode"].Value = line.ClientCode;
            insertCommand.Parameters["@clientName"].Value = DbValue(line.ClientName);
            insertCommand.Parameters["@ncCode"].Value = DbValue(line.NcCode);
            await insertCommand.ExecuteNonQueryAsync(cancellationToken);
        }
    }

    private static async Task ReplaceBreakdownsAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        string centerCode,
        string code,
        IReadOnlyList<SaveMuestraBreakdownInput> breakdowns,
        CancellationToken cancellationToken)
    {
        await using (var deleteDetailCommand = connection.CreateCommand())
        {
            deleteDetailCommand.Transaction = transaction;
            deleteDetailCommand.CommandText =
                """
                DELETE FROM mostres_breakdown_lines
                WHERE CENTRO = @centerCode
                  AND MOSTRA_CODI = @code;
                """;
            deleteDetailCommand.Parameters.AddWithValue("@centerCode", centerCode);
            deleteDetailCommand.Parameters.AddWithValue("@code", code);
            await deleteDetailCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        await using (var deleteHeaderCommand = connection.CreateCommand())
        {
            deleteHeaderCommand.Transaction = transaction;
            deleteHeaderCommand.CommandText =
                """
                DELETE FROM mostres_breakdown
                WHERE CENTRO = @centerCode
                  AND MOSTRA_CODI = @code;
                """;
            deleteHeaderCommand.Parameters.AddWithValue("@centerCode", centerCode);
            deleteHeaderCommand.Parameters.AddWithValue("@code", code);
            await deleteHeaderCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        var meaningfulBreakdowns = breakdowns
            .Where(HasBreakdownContent)
            .OrderBy(item => item.SampleLineNumber)
            .ToList();

        if (meaningfulBreakdowns.Count == 0)
        {
            return;
        }

        await using var insertHeaderCommand = connection.CreateCommand();
        insertHeaderCommand.Transaction = transaction;
        insertHeaderCommand.CommandText =
            """
            INSERT INTO mostres_breakdown (
                CENTRO, MOSTRA_CODI, SAMPLE_LINE_NUMBER, DATA, CLIENT, NOMCLIENT,
                MAQUINA, NOMMAQUI, OPERACIO, NOMOPER, AGULLES, VELOSITAT, DISCO, TEMPS,
                MACHINE_RATE, MACHINE_IMPORT, CORTES, NOTES,
                origin, is_deleted, synced_utc)
            VALUES (
                @centerCode, @code, @sampleLineNumber, @workDate, @clientCode, @clientName,
                @machineCode, @machineName, @operationCode, @operationName, @needles, @speed, @disk, @timeMinutes,
                @machineRate, @machineImport, @cuts, @notes,
                'local', 0, NULL);
            """;
        insertHeaderCommand.Parameters.Add("@centerCode", MySqlDbType.VarChar);
        insertHeaderCommand.Parameters.Add("@code", MySqlDbType.VarChar);
        insertHeaderCommand.Parameters.Add("@sampleLineNumber", MySqlDbType.Int32);
        insertHeaderCommand.Parameters.Add("@workDate", MySqlDbType.Date);
        insertHeaderCommand.Parameters.Add("@clientCode", MySqlDbType.Int32);
        insertHeaderCommand.Parameters.Add("@clientName", MySqlDbType.VarChar);
        insertHeaderCommand.Parameters.Add("@machineCode", MySqlDbType.Int32);
        insertHeaderCommand.Parameters.Add("@machineName", MySqlDbType.VarChar);
        insertHeaderCommand.Parameters.Add("@operationCode", MySqlDbType.Int32);
        insertHeaderCommand.Parameters.Add("@operationName", MySqlDbType.VarChar);
        insertHeaderCommand.Parameters.Add("@needles", MySqlDbType.Decimal);
        insertHeaderCommand.Parameters.Add("@speed", MySqlDbType.Decimal);
        insertHeaderCommand.Parameters.Add("@disk", MySqlDbType.VarChar);
        insertHeaderCommand.Parameters.Add("@timeMinutes", MySqlDbType.Decimal);
        insertHeaderCommand.Parameters.Add("@machineRate", MySqlDbType.Decimal);
        insertHeaderCommand.Parameters.Add("@machineImport", MySqlDbType.Decimal);
        insertHeaderCommand.Parameters.Add("@cuts", MySqlDbType.VarChar);
        insertHeaderCommand.Parameters.Add("@notes", MySqlDbType.VarChar);

        await using var insertLineCommand = connection.CreateCommand();
        insertLineCommand.Transaction = transaction;
        insertLineCommand.CommandText =
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
        insertLineCommand.Parameters.Add("@centerCode", MySqlDbType.VarChar);
        insertLineCommand.Parameters.Add("@code", MySqlDbType.VarChar);
        insertLineCommand.Parameters.Add("@sampleLineNumber", MySqlDbType.Int32);
        insertLineCommand.Parameters.Add("@lineNumber", MySqlDbType.Int32);
        insertLineCommand.Parameters.Add("@yarnCode", MySqlDbType.VarChar);
        insertLineCommand.Parameters.Add("@providerCode", MySqlDbType.Int32);
        insertLineCommand.Parameters.Add("@providerName", MySqlDbType.VarChar);
        insertLineCommand.Parameters.Add("@materialColor", MySqlDbType.VarChar);
        insertLineCommand.Parameters.Add("@yarnMetric", MySqlDbType.Decimal);
        insertLineCommand.Parameters.Add("@ends", MySqlDbType.Decimal);
        insertLineCommand.Parameters.Add("@passes", MySqlDbType.Decimal);
        insertLineCommand.Parameters.Add("@graduation", MySqlDbType.Int32);
        insertLineCommand.Parameters.Add("@consumption", MySqlDbType.Decimal);
        insertLineCommand.Parameters.Add("@price", MySqlDbType.Decimal);
        insertLineCommand.Parameters.Add("@importAmount", MySqlDbType.Decimal);

        foreach (var breakdown in meaningfulBreakdowns)
        {
            var machineImport = Math.Round(breakdown.TimeMinutes * breakdown.MachineRate, 4);
            insertHeaderCommand.Parameters["@centerCode"].Value = centerCode;
            insertHeaderCommand.Parameters["@code"].Value = code;
            insertHeaderCommand.Parameters["@sampleLineNumber"].Value = breakdown.SampleLineNumber;
            insertHeaderCommand.Parameters["@workDate"].Value = breakdown.WorkDate.HasValue ? breakdown.WorkDate.Value.Date : DBNull.Value;
            insertHeaderCommand.Parameters["@clientCode"].Value = breakdown.ClientCode;
            insertHeaderCommand.Parameters["@clientName"].Value = DbValue(breakdown.ClientName);
            insertHeaderCommand.Parameters["@machineCode"].Value = breakdown.MachineCode;
            insertHeaderCommand.Parameters["@machineName"].Value = DbValue(breakdown.MachineName);
            insertHeaderCommand.Parameters["@operationCode"].Value = breakdown.OperationCode;
            insertHeaderCommand.Parameters["@operationName"].Value = DbValue(breakdown.OperationName);
            insertHeaderCommand.Parameters["@needles"].Value = breakdown.Needles;
            insertHeaderCommand.Parameters["@speed"].Value = breakdown.Speed;
            insertHeaderCommand.Parameters["@disk"].Value = DbValue(breakdown.Disk);
            insertHeaderCommand.Parameters["@timeMinutes"].Value = breakdown.TimeMinutes;
            insertHeaderCommand.Parameters["@machineRate"].Value = breakdown.MachineRate;
            insertHeaderCommand.Parameters["@machineImport"].Value = machineImport;
            insertHeaderCommand.Parameters["@cuts"].Value = DbValue(breakdown.Cuts);
            insertHeaderCommand.Parameters["@notes"].Value = DbValue(breakdown.Notes);
            await insertHeaderCommand.ExecuteNonQueryAsync(cancellationToken);

            foreach (var (line, index) in breakdown.Lines
                         .Where(HasBreakdownLineContent)
                         .OrderBy(item => item.LineNumber)
                         .Select((item, index) => (item, index)))
            {
                insertLineCommand.Parameters["@centerCode"].Value = centerCode;
                insertLineCommand.Parameters["@code"].Value = code;
                insertLineCommand.Parameters["@sampleLineNumber"].Value = breakdown.SampleLineNumber;
                insertLineCommand.Parameters["@lineNumber"].Value = index + 1;
                insertLineCommand.Parameters["@yarnCode"].Value = DbValue(line.YarnCode);
                insertLineCommand.Parameters["@providerCode"].Value = line.ProviderCode;
                insertLineCommand.Parameters["@providerName"].Value = DbValue(line.ProviderName);
                insertLineCommand.Parameters["@materialColor"].Value = DbValue(line.MaterialColor);
                insertLineCommand.Parameters["@yarnMetric"].Value = line.YarnMetric;
                insertLineCommand.Parameters["@ends"].Value = line.Ends;
                insertLineCommand.Parameters["@passes"].Value = line.Passes;
                insertLineCommand.Parameters["@graduation"].Value = line.Graduation;
                insertLineCommand.Parameters["@consumption"].Value = line.Consumption;
                insertLineCommand.Parameters["@price"].Value = line.Price;
                insertLineCommand.Parameters["@importAmount"].Value = Math.Round(line.Consumption * line.Price, 4);
                await insertLineCommand.ExecuteNonQueryAsync(cancellationToken);
            }
        }
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

        throw new InvalidOperationException("No tienes permisos para editar muestras en este tenant.");
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

    private static void Validate(SaveMuestraCommand command)
    {
        command.Code = command.Code?.Trim().ToUpperInvariant();
        command.Description = command.Description.Trim();
        command.ClientName = command.ClientName.Trim();
        command.Reference = command.Reference.Trim();
        command.Season = command.Season.Trim();
        command.MachineName = command.MachineName.Trim();
        command.VatCode = command.VatCode.Trim().ToUpperInvariant();
        command.Notes = command.Notes.Trim();
        command.Composition = command.Composition.Trim();

        if (string.IsNullOrWhiteSpace(command.Code))
        {
            throw new InvalidOperationException("Debes indicar un código para la muestra.");
        }

        if (command.Code.Length > 40)
        {
            throw new InvalidOperationException("El código de la muestra no puede superar 40 caracteres.");
        }

        if (string.IsNullOrWhiteSpace(command.Description))
        {
            throw new InvalidOperationException("La descripción de la muestra es obligatoria.");
        }

        if (command.ClientCode < 0 || command.MachineCode < 0 || command.MarginPercent < 0 || command.UnitPrice < 0)
        {
            throw new InvalidOperationException("Cliente, máquina, margen y precio no pueden ser negativos.");
        }

        var duplicateKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        for (var index = 0; index < command.Lines.Count; index++)
        {
            var line = command.Lines[index];
            line.SizeCode = line.SizeCode.Trim();
            line.SizeHigh = line.SizeHigh.Trim();
            line.SizeLow = line.SizeLow.Trim();
            line.Description = line.Description.Trim();
            line.Color = line.Color.Trim();
            line.ClientName = line.ClientName.Trim();
            line.NcCode = line.NcCode.Trim();

            if (line.CostPrice < 0 || line.SalePrice < 0 || line.ClientCode < 0)
            {
                throw new InvalidOperationException($"La línea {index + 1} del detalle no admite valores negativos.");
            }

            var duplicateKey = $"{line.SizeCode}|{line.Color}|{line.ClientCode}";
            if (!duplicateKeys.Add(duplicateKey))
            {
                throw new InvalidOperationException($"La línea {index + 1} repite talla/color/cliente dentro de la misma muestra.");
            }
        }

        var breakdownLineNumbers = new HashSet<int>();
        foreach (var breakdown in command.Breakdowns)
        {
            breakdown.ClientName = breakdown.ClientName.Trim();
            breakdown.MachineName = breakdown.MachineName.Trim();
            breakdown.OperationName = breakdown.OperationName.Trim();
            breakdown.Disk = breakdown.Disk.Trim();
            breakdown.Cuts = breakdown.Cuts.Trim();
            breakdown.Notes = breakdown.Notes.Trim();

            if (breakdown.SampleLineNumber <= 0 || !command.Lines.Any(line => line.LineNumber == breakdown.SampleLineNumber))
            {
                throw new InvalidOperationException("El desglose de muestra apunta a una línea inexistente de la carta.");
            }

            if (!breakdownLineNumbers.Add(breakdown.SampleLineNumber))
            {
                throw new InvalidOperationException($"La línea {breakdown.SampleLineNumber} tiene más de un desglose técnico.");
            }

            if (breakdown.ClientCode < 0 ||
                breakdown.MachineCode < 0 ||
                breakdown.OperationCode < 0 ||
                breakdown.Needles < 0 ||
                breakdown.Speed < 0 ||
                breakdown.TimeMinutes < 0 ||
                breakdown.MachineRate < 0)
            {
                throw new InvalidOperationException($"El desglose técnico de la línea {breakdown.SampleLineNumber} contiene valores negativos no válidos.");
            }

            foreach (var (line, index) in breakdown.Lines.Select((item, index) => (item, index)))
            {
                line.YarnCode = line.YarnCode.Trim().ToUpperInvariant();
                line.ProviderName = line.ProviderName.Trim();
                line.MaterialColor = line.MaterialColor.Trim();

                if (line.ProviderCode < 0 ||
                    line.YarnMetric < 0 ||
                    line.Ends < 0 ||
                    line.Passes < 0 ||
                    line.Graduation < 0 ||
                    line.Consumption < 0 ||
                    line.Price < 0)
                {
                    throw new InvalidOperationException($"La línea {index + 1} del desglose técnico contiene valores negativos.");
                }
            }
        }
    }

    private static MuestraBreakdownDto CreateDefaultBreakdown(MuestraLineDto line, MuestraDetailDto detail) =>
        new()
        {
            SampleLineNumber = line.LineNumber,
            ClientCode = line.ClientCode > 0 ? line.ClientCode : detail.ClientCode,
            ClientName = !string.IsNullOrWhiteSpace(line.ClientName) ? line.ClientName : detail.ClientName,
            MachineCode = detail.MachineCode,
            MachineName = detail.MachineName
        };

    private static bool HasBreakdownContent(SaveMuestraBreakdownInput breakdown) =>
        breakdown.MachineCode > 0 ||
        !string.IsNullOrWhiteSpace(breakdown.MachineName) ||
        breakdown.OperationCode > 0 ||
        !string.IsNullOrWhiteSpace(breakdown.OperationName) ||
        breakdown.ClientCode > 0 ||
        !string.IsNullOrWhiteSpace(breakdown.ClientName) ||
        breakdown.WorkDate.HasValue ||
        breakdown.Needles > 0 ||
        breakdown.Speed > 0 ||
        !string.IsNullOrWhiteSpace(breakdown.Disk) ||
        breakdown.TimeMinutes > 0 ||
        breakdown.MachineRate > 0 ||
        !string.IsNullOrWhiteSpace(breakdown.Cuts) ||
        !string.IsNullOrWhiteSpace(breakdown.Notes) ||
        breakdown.Lines.Any(HasBreakdownLineContent);

    private static bool HasBreakdownLineContent(SaveMuestraBreakdownLineInput line) =>
        !string.IsNullOrWhiteSpace(line.YarnCode) ||
        line.ProviderCode > 0 ||
        !string.IsNullOrWhiteSpace(line.ProviderName) ||
        !string.IsNullOrWhiteSpace(line.MaterialColor) ||
        line.YarnMetric > 0 ||
        line.Ends > 0 ||
        line.Passes > 0 ||
        line.Graduation > 0 ||
        line.Consumption > 0 ||
        line.Price > 0;

    private static object DbValue(string value) =>
        string.IsNullOrWhiteSpace(value) ? DBNull.Value : value;

    private static string BuildSearchOrderByClause(MuestraFilter filter)
    {
        var column = filter.SortColumn switch
        {
            nameof(MuestraListItemDto.Code) => "m.CODI",
            nameof(MuestraListItemDto.Description) => "m.DESCRI",
            nameof(MuestraListItemDto.ClientCode) => "m.CLIENT",
            nameof(MuestraListItemDto.ClientName) => "m.NOMCLIENT",
            nameof(MuestraListItemDto.Reference) => "m.REFE",
            nameof(MuestraListItemDto.Season) => "m.TEMP",
            nameof(MuestraListItemDto.MachineCode) => "m.MAQUINA",
            nameof(MuestraListItemDto.UnitPrice) => "m.PREU",
            nameof(MuestraListItemDto.DetailLinesCount) => "DETAIL_LINES",
            _ => string.Empty
        };

        if (string.IsNullOrWhiteSpace(column))
        {
            return string.IsNullOrWhiteSpace(filter.Search)
                ? "ORDER BY m.CODI"
                : "ORDER BY m.DESCRI, m.CODI";
        }

        var direction = filter.SortDescending ? "DESC" : "ASC";
        return $"ORDER BY {column} {direction}, m.CODI";
    }
}
