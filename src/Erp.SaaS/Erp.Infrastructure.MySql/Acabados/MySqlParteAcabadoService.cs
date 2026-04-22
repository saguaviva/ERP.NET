using Erp.Application.Acabados;
using Erp.Application.Auditing;
using Erp.Application.BaseData;
using Erp.Application.Companies;
using Erp.Application.Contexts;
using Erp.Application.Stock;
using Erp.Domain.Common;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Support;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Acabados;

public sealed class MySqlParteAcabadoService : IParteAcabadoQueries, IParteAcabadoService
{
    private readonly MySqlConnectionFactory _connectionFactory;
    private readonly IAuditLogService _auditLogService;
    private readonly ICompanyAccessService _companyAccessService;
    private readonly ICurrentUserContext _currentUserContext;
    private readonly ITenantContext _tenantContext;
    private readonly IActiveCompanyContext _activeCompanyContext;

    public MySqlParteAcabadoService(
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

    public async Task<ParteAcabadoSearchResultDto> SearchAsync(Guid tenantId, Guid companyId, ParteAcabadoFilter filter, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return new ParteAcabadoSearchResultDto();
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);
        var centerCode = await ResolveCompanyCenterCodeAsync(tenantId, companyId, cancellationToken);
        var pageSize = Math.Clamp(filter.PageSize, 10, 200);
        var page = Math.Max(filter.Page, 1);
        var offset = (page - 1) * pageSize;
        var search = filter.Search?.Trim() ?? string.Empty;
        var likeSearch = $"%{search}%";
        var status = NormalizeStatus(filter.Status, allowEmpty: true);
        var finisherCode = filter.FinisherCode.GetValueOrDefault();
        var machineCode = filter.MachineCode.GetValueOrDefault();
        var operationCode = filter.OperationCode.GetValueOrDefault();
        var sourceSampleKind = ParteAcabadoSourceKinds.Normalize(filter.SourceSampleKind);
        var sourceSampleCode = filter.SourceSampleCode?.Trim() ?? string.Empty;
        var liveOnly = filter.LiveOnly;

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);

        await using var countCommand = connection.CreateCommand();
        countCommand.CommandText =
            """
            SELECT COUNT(*)
            FROM finish_work_orders header
            WHERE header.tenant_id = @tenantId
              AND header.company_id = @companyId
              AND header.center_code = @centerCode
              AND header.is_deleted = 0
              AND (@status = '' OR header.status = @status)
              AND (@finisherCode = 0 OR header.finisher_code = @finisherCode)
              AND (@machineCode = 0 OR header.machine_code = @machineCode)
              AND (@operationCode = 0 OR header.operation_code = @operationCode)
              AND (@sourceSampleKind = '' OR header.source_sample_kind = @sourceSampleKind)
              AND (@sourceSampleCode = '' OR header.source_sample_code = @sourceSampleCode)
              AND (@liveOnly = 0 OR header.status IN ('Pending', 'InProgress'))
              AND (
                    @search = ''
                    OR CAST(header.order_number AS CHAR) LIKE @likeSearch
                    OR CAST(header.client_code AS CHAR) LIKE @likeSearch
                    OR COALESCE(header.client_name, '') LIKE @likeSearch
                    OR CAST(header.finisher_code AS CHAR) LIKE @likeSearch
                    OR COALESCE(header.finisher_name, '') LIKE @likeSearch
                    OR CAST(header.machine_code AS CHAR) LIKE @likeSearch
                    OR COALESCE(header.machine_name, '') LIKE @likeSearch
                    OR CAST(header.operation_code AS CHAR) LIKE @likeSearch
                    OR COALESCE(header.operation_name, '') LIKE @likeSearch
                    OR COALESCE(header.source_sample_kind, '') LIKE @likeSearch
                    OR COALESCE(header.source_sample_code, '') LIKE @likeSearch
                    OR COALESCE(header.primary_fabric_code, '') LIKE @likeSearch
                    OR COALESCE(header.primary_fabric_description, '') LIKE @likeSearch
                    OR COALESCE(header.primary_color, '') LIKE @likeSearch
                    OR COALESCE(header.notes, '') LIKE @likeSearch
                  );
            """;
        FillSearchParameters(countCommand, tenantId, companyId, centerCode, search, likeSearch, status, finisherCode, machineCode, operationCode, sourceSampleKind, sourceSampleCode, liveOnly);
        var totalCount = Convert.ToInt32(await countCommand.ExecuteScalarAsync(cancellationToken));
        if (totalCount == 0)
        {
            return new ParteAcabadoSearchResultDto();
        }

        var items = new List<ParteAcabadoListItemDto>();
        await using var command = connection.CreateCommand();
        command.CommandText =
            $"""
            SELECT header.order_number,
                   header.work_date,
                   header.status,
                   header.client_code,
                   header.client_name,
                   header.finisher_code,
                   header.finisher_name,
                   header.machine_code,
                   header.machine_name,
                   header.operation_code,
                   header.operation_name,
                   header.source_sample_kind,
                   header.source_sample_code,
                   header.source_sample_line_number,
                   header.primary_fabric_code,
                   header.primary_fabric_description,
                   header.primary_color,
                   header.total_kilograms,
                   header.total_pieces,
                   header.origin,
                   (
                       SELECT COUNT(*)
                       FROM finish_work_order_lines detail_rows
                       WHERE detail_rows.order_id = header.order_id
                   ) AS lines_count
            FROM finish_work_orders header
            WHERE header.tenant_id = @tenantId
              AND header.company_id = @companyId
              AND header.center_code = @centerCode
              AND header.is_deleted = 0
              AND (@status = '' OR header.status = @status)
              AND (@finisherCode = 0 OR header.finisher_code = @finisherCode)
              AND (@machineCode = 0 OR header.machine_code = @machineCode)
              AND (@operationCode = 0 OR header.operation_code = @operationCode)
              AND (@sourceSampleKind = '' OR header.source_sample_kind = @sourceSampleKind)
              AND (@sourceSampleCode = '' OR header.source_sample_code = @sourceSampleCode)
              AND (@liveOnly = 0 OR header.status IN ('Pending', 'InProgress'))
              AND (
                    @search = ''
                    OR CAST(header.order_number AS CHAR) LIKE @likeSearch
                    OR CAST(header.client_code AS CHAR) LIKE @likeSearch
                    OR COALESCE(header.client_name, '') LIKE @likeSearch
                    OR CAST(header.finisher_code AS CHAR) LIKE @likeSearch
                    OR COALESCE(header.finisher_name, '') LIKE @likeSearch
                    OR CAST(header.machine_code AS CHAR) LIKE @likeSearch
                    OR COALESCE(header.machine_name, '') LIKE @likeSearch
                    OR CAST(header.operation_code AS CHAR) LIKE @likeSearch
                    OR COALESCE(header.operation_name, '') LIKE @likeSearch
                    OR COALESCE(header.source_sample_kind, '') LIKE @likeSearch
                    OR COALESCE(header.source_sample_code, '') LIKE @likeSearch
                    OR COALESCE(header.primary_fabric_code, '') LIKE @likeSearch
                    OR COALESCE(header.primary_fabric_description, '') LIKE @likeSearch
                    OR COALESCE(header.primary_color, '') LIKE @likeSearch
                    OR COALESCE(header.notes, '') LIKE @likeSearch
                  )
            {BuildSearchOrderByClause(filter)}
            LIMIT @limit OFFSET @offset;
            """;
        FillSearchParameters(command, tenantId, companyId, centerCode, search, likeSearch, status, finisherCode, machineCode, operationCode, sourceSampleKind, sourceSampleCode, liveOnly);
        command.Parameters.AddWithValue("@limit", pageSize);
        command.Parameters.AddWithValue("@offset", offset);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new ParteAcabadoListItemDto
            {
                OrderNumber = reader.GetInt32OrDefault("order_number"),
                Date = GetNullableDateTime(reader, "work_date"),
                Status = reader.GetStringOrEmpty("status"),
                ClientCode = reader.GetInt32OrDefault("client_code"),
                ClientName = reader.GetStringOrEmpty("client_name"),
                FinisherCode = reader.GetInt32OrDefault("finisher_code"),
                FinisherName = reader.GetStringOrEmpty("finisher_name"),
                MachineCode = reader.GetInt32OrDefault("machine_code"),
                MachineName = reader.GetStringOrEmpty("machine_name"),
                OperationCode = reader.GetInt32OrDefault("operation_code"),
                OperationName = reader.GetStringOrEmpty("operation_name"),
                SourceSampleKind = reader.GetStringOrEmpty("source_sample_kind"),
                SourceSampleCode = reader.GetStringOrEmpty("source_sample_code"),
                SourceSampleLineNumber = reader.IsDBNull(reader.GetOrdinal("source_sample_line_number")) ? null : reader.GetInt32("source_sample_line_number"),
                PrimaryFabricCode = reader.GetStringOrEmpty("primary_fabric_code"),
                PrimaryFabricDescription = reader.GetStringOrEmpty("primary_fabric_description"),
                PrimaryColor = reader.GetStringOrEmpty("primary_color"),
                TotalKilograms = reader.GetDecimalOrDefault("total_kilograms"),
                TotalPieces = reader.GetDecimalOrDefault("total_pieces"),
                LinesCount = reader.GetInt32OrDefault("lines_count"),
                Origin = reader.GetStringOrEmpty("origin")
            });
        }

        return new ParteAcabadoSearchResultDto
        {
            Items = items,
            TotalCount = totalCount
        };
    }

    public async Task<ParteAcabadoDetailDto?> GetByNumberAsync(Guid tenantId, Guid companyId, int orderNumber, CancellationToken cancellationToken = default)
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
            SELECT order_id,
                   order_number,
                   center_code,
                   work_date,
                   status,
                   client_code,
                   client_name,
                   finisher_code,
                   finisher_name,
                   machine_code,
                   machine_name,
                   operation_code,
                   operation_name,
                   disposition_code,
                   disposition_label,
                   source_sample_kind,
                   source_sample_code,
                   source_sample_line_number,
                   notes,
                   total_kilograms,
                   total_pieces,
                   origin
            FROM finish_work_orders
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND center_code = @centerCode
              AND order_number = @orderNumber
              AND is_deleted = 0
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@orderNumber", orderNumber);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        var orderId = reader.GetStringOrEmpty("order_id");
        var detail = new ParteAcabadoDetailDto
        {
            OrderNumber = reader.GetInt32OrDefault("order_number"),
            CompanyCenterCode = reader.GetStringOrEmpty("center_code"),
            Date = GetNullableDateTime(reader, "work_date"),
            Status = reader.GetStringOrEmpty("status"),
            ClientCode = reader.GetInt32OrDefault("client_code"),
            ClientName = reader.GetStringOrEmpty("client_name"),
            FinisherCode = reader.GetInt32OrDefault("finisher_code"),
            FinisherName = reader.GetStringOrEmpty("finisher_name"),
            MachineCode = reader.GetInt32OrDefault("machine_code"),
            MachineName = reader.GetStringOrEmpty("machine_name"),
            OperationCode = reader.GetInt32OrDefault("operation_code"),
            OperationName = reader.GetStringOrEmpty("operation_name"),
            DispositionCode = reader.IsDBNull(reader.GetOrdinal("disposition_code")) ? null : reader.GetInt32("disposition_code"),
            DispositionLabel = reader.GetStringOrEmpty("disposition_label"),
            SourceSampleKind = reader.GetStringOrEmpty("source_sample_kind"),
            SourceSampleCode = reader.GetStringOrEmpty("source_sample_code"),
            SourceSampleLineNumber = reader.IsDBNull(reader.GetOrdinal("source_sample_line_number")) ? null : reader.GetInt32("source_sample_line_number"),
            Notes = reader.GetStringOrEmpty("notes"),
            TotalKilograms = reader.GetDecimalOrDefault("total_kilograms"),
            TotalPieces = reader.GetDecimalOrDefault("total_pieces"),
            Origin = reader.GetStringOrEmpty("origin")
        };

        await reader.CloseAsync();
        detail.Lines = await LoadLinesAsync(connection, orderId, cancellationToken);
        return detail;
    }

    public async Task<int> SaveAsync(SaveParteAcabadoCommand command, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return 0;
        }

        await EnsureCompanyAccessAsync(command.TenantId, command.CompanyId, cancellationToken);
        EnsureTenantWriteAccess();
        NormalizeAndValidate(command);
        var centerCode = await ResolveCompanyCenterCodeAsync(command.TenantId, command.CompanyId, cancellationToken);

        ParteAcabadoDetailDto? previous = null;
        var existingOrderNumber = command.OrderNumber.GetValueOrDefault();
        if (existingOrderNumber > 0)
        {
            previous = await GetByNumberAsync(command.TenantId, command.CompanyId, existingOrderNumber, cancellationToken);
            if (previous is null)
            {
                throw new InvalidOperationException("No se ha encontrado la orden / parte de acabado que intentas modificar.");
            }
        }

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        var orderNumber = command.OrderNumber.GetValueOrDefault();
        var orderId = previous is null ? Guid.NewGuid().ToString() : await ResolveOrderIdAsync(connection, transaction, command.TenantId, command.CompanyId, centerCode, orderNumber, cancellationToken);
        if (orderNumber <= 0)
        {
            orderNumber = await GenerateNextOrderNumberAsync(connection, transaction, command.TenantId, command.CompanyId, centerCode, cancellationToken);
        }

        await HydrateLookupNamesAsync(connection, transaction, centerCode, command, cancellationToken);
        var summary = BuildHeaderSummary(command.Lines);

        if (previous is null)
        {
            await using var insertCommand = connection.CreateCommand();
            insertCommand.Transaction = transaction;
            insertCommand.CommandText =
                """
                INSERT INTO finish_work_orders (
                    order_id,
                    tenant_id,
                    company_id,
                    center_code,
                    order_number,
                    work_date,
                    status,
                    client_code,
                    client_name,
                    finisher_code,
                    finisher_name,
                    machine_code,
                    machine_name,
                    operation_code,
                    operation_name,
                    disposition_code,
                    disposition_label,
                    source_sample_kind,
                    source_sample_code,
                    source_sample_line_number,
                    primary_fabric_code,
                    primary_fabric_description,
                    primary_color,
                    total_kilograms,
                    total_pieces,
                    notes,
                    origin,
                    is_deleted,
                    synced_utc,
                    created_utc,
                    updated_utc)
                VALUES (
                    @orderId,
                    @tenantId,
                    @companyId,
                    @centerCode,
                    @orderNumber,
                    @workDate,
                    @status,
                    @clientCode,
                    @clientName,
                    @finisherCode,
                    @finisherName,
                    @machineCode,
                    @machineName,
                    @operationCode,
                    @operationName,
                    @dispositionCode,
                    @dispositionLabel,
                    @sourceSampleKind,
                    @sourceSampleCode,
                    @sourceSampleLineNumber,
                    @primaryFabricCode,
                    @primaryFabricDescription,
                    @primaryColor,
                    @totalKilograms,
                    @totalPieces,
                    @notes,
                    'local',
                    0,
                    NULL,
                    @createdUtc,
                    @updatedUtc);
                """;
            FillHeaderParameters(insertCommand, orderId, command.TenantId, command.CompanyId, centerCode, orderNumber, command, summary, DateTime.UtcNow);
            await insertCommand.ExecuteNonQueryAsync(cancellationToken);
        }
        else
        {
            await using var updateCommand = connection.CreateCommand();
            updateCommand.Transaction = transaction;
            updateCommand.CommandText =
                """
                UPDATE finish_work_orders
                SET work_date = @workDate,
                    status = @status,
                    client_code = @clientCode,
                    client_name = @clientName,
                    finisher_code = @finisherCode,
                    finisher_name = @finisherName,
                    machine_code = @machineCode,
                    machine_name = @machineName,
                    operation_code = @operationCode,
                    operation_name = @operationName,
                    disposition_code = @dispositionCode,
                    disposition_label = @dispositionLabel,
                    source_sample_kind = @sourceSampleKind,
                    source_sample_code = @sourceSampleCode,
                    source_sample_line_number = @sourceSampleLineNumber,
                    primary_fabric_code = @primaryFabricCode,
                    primary_fabric_description = @primaryFabricDescription,
                    primary_color = @primaryColor,
                    total_kilograms = @totalKilograms,
                    total_pieces = @totalPieces,
                    notes = @notes,
                    origin = 'local',
                    is_deleted = 0,
                    synced_utc = NULL,
                    updated_utc = @updatedUtc
                WHERE order_id = @orderId;
                """;
            FillHeaderParameters(updateCommand, orderId, command.TenantId, command.CompanyId, centerCode, orderNumber, command, summary, DateTime.UtcNow);
            await updateCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        await ReplaceLinesAsync(connection, transaction, orderId, command.Lines, cancellationToken);
        await SyncDispositionAsync(connection, transaction, centerCode, command, cancellationToken);
        await ReplaceProgressMovementsAsync(connection, transaction, orderId, command.TenantId, command.CompanyId, orderNumber, command, cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = command.TenantId,
            CompanyId = command.CompanyId,
            Action = previous is null ? "ParteAcabadoCreated" : "ParteAcabadoUpdated",
            EntityName = "ParteAcabado",
            EntityId = orderNumber.ToString(),
            Details = previous is null
                ? $"Parte de acabado {orderNumber} creada; líneas={command.Lines.Count}; estado={command.Status}."
                : $"Parte de acabado {orderNumber} actualizada; líneas={command.Lines.Count}; estado={command.Status}."
        }, cancellationToken);

        return orderNumber;
    }

    public async Task DeleteAsync(Guid tenantId, Guid companyId, int orderNumber, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return;
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);
        EnsureTenantWriteAccess();
        var centerCode = await ResolveCompanyCenterCodeAsync(tenantId, companyId, cancellationToken);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            UPDATE finish_work_orders
            SET is_deleted = 1,
                origin = 'local',
                synced_utc = NULL,
                updated_utc = @updatedUtc
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND center_code = @centerCode
              AND order_number = @orderNumber
              AND is_deleted = 0;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@orderNumber", orderNumber);
        command.Parameters.AddWithValue("@updatedUtc", DateTime.UtcNow);

        var affected = await command.ExecuteNonQueryAsync(cancellationToken);
        if (affected == 0)
        {
            throw new InvalidOperationException("No se ha encontrado la orden / parte de acabado que intentas eliminar.");
        }

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = tenantId,
            CompanyId = companyId,
            Action = "ParteAcabadoDeleted",
            EntityName = "ParteAcabado",
            EntityId = orderNumber.ToString(),
            Details = $"Parte de acabado {orderNumber} marcada como eliminada."
        }, cancellationToken);
    }

    private static void NormalizeAndValidate(SaveParteAcabadoCommand command)
    {
        command.Status = NormalizeStatus(command.Status, allowEmpty: false);
        command.ClientName = command.ClientName?.Trim() ?? string.Empty;
        command.FinisherName = command.FinisherName?.Trim() ?? string.Empty;
        command.OperationName = command.OperationName?.Trim() ?? string.Empty;
        command.DispositionLabel = command.DispositionLabel?.Trim() ?? string.Empty;
        command.SourceSampleCode = command.SourceSampleCode?.Trim() ?? string.Empty;
        command.SourceSampleKind = ParteAcabadoSourceKinds.Normalize(command.SourceSampleKind, hasLinkedSource: !string.IsNullOrWhiteSpace(command.SourceSampleCode));
        command.Notes = command.Notes?.Trim() ?? string.Empty;
        command.Date ??= DateTime.Today;
        command.Lines = command.Lines
            .OrderBy(item => item.LineNumber)
            .ToList();

        if (command.Lines.Count == 0)
        {
            throw new InvalidOperationException("La orden / parte de acabado necesita al menos una línea.");
        }

        if (command.OperationCode < 0)
        {
            throw new InvalidOperationException("La operación del parte no puede ser negativa.");
        }

        var seen = new HashSet<int>();
        foreach (var line in command.Lines)
        {
            if (line.LineNumber <= 0)
            {
                throw new InvalidOperationException("Cada línea debe tener un número válido.");
            }

            if (!seen.Add(line.LineNumber))
            {
                throw new InvalidOperationException($"La línea {line.LineNumber} está repetida.");
            }

            line.FabricCode = (line.FabricCode ?? string.Empty).Trim();
            line.FabricDescription = (line.FabricDescription ?? string.Empty).Trim();
            line.Color = (line.Color ?? string.Empty).Trim();
            line.Notes = (line.Notes ?? string.Empty).Trim();
            line.Status = NormalizeStatus(line.Status, allowEmpty: false);

            if (string.IsNullOrWhiteSpace(line.FabricCode) && string.IsNullOrWhiteSpace(line.FabricDescription))
            {
                throw new InvalidOperationException($"La línea {line.LineNumber} debe informar tejido o descripción.");
            }

            if (line.TotalKilograms < 0 || line.TotalPieces < 0)
            {
                throw new InvalidOperationException($"La línea {line.LineNumber} no puede tener kg o piezas negativos.");
            }
        }
    }

    private static async Task SyncDispositionAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        string centerCode,
        SaveParteAcabadoCommand command,
        CancellationToken cancellationToken)
    {
        if (!command.DispositionCode.HasValue || command.DispositionCode.Value <= 0)
        {
            return;
        }

        foreach (var line in command.Lines.OrderBy(item => item.LineNumber))
        {
            var (isDisposed, isServed) = MapDispositionFlags(line.Status);
            await using var updateLineCommand = connection.CreateCommand();
            updateLineCommand.Transaction = transaction;
            updateLineCommand.CommandText =
                """
                UPDATE ddispos
                SET DISPUESTO = @isDisposed,
                    SERVIDO = @isServed
                WHERE CENTRO = @centerCode
                  AND DISPOS = @dispositionCode
                  AND LINEA = @lineNumber;
                """;
            updateLineCommand.Parameters.AddWithValue("@isDisposed", isDisposed);
            updateLineCommand.Parameters.AddWithValue("@isServed", isServed);
            updateLineCommand.Parameters.AddWithValue("@centerCode", centerCode);
            updateLineCommand.Parameters.AddWithValue("@dispositionCode", command.DispositionCode.Value);
            updateLineCommand.Parameters.AddWithValue("@lineNumber", line.LineNumber);
            await updateLineCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        var allFinished = command.Lines.Count > 0 && command.Lines.All(item => string.Equals(item.Status, ParteAcabadoStatuses.Finished, StringComparison.OrdinalIgnoreCase));
        var anyCancelled = command.Lines.Count > 0 && command.Lines.All(item => string.Equals(item.Status, ParteAcabadoStatuses.Cancelled, StringComparison.OrdinalIgnoreCase));

        await using var updateHeaderCommand = connection.CreateCommand();
        updateHeaderCommand.Transaction = transaction;
        updateHeaderCommand.CommandText =
            """
            UPDATE dispos
            SET RECIBIDO = @isReceived,
                DRECEPCION = @receptionDate,
                ANULADA = @isCancelled,
                origin = 'local',
                synced_utc = NULL
            WHERE CENTRO = @centerCode
              AND CODI = @dispositionCode
              AND is_deleted = 0;
            """;
        updateHeaderCommand.Parameters.AddWithValue("@isReceived", allFinished);
        updateHeaderCommand.Parameters.AddWithValue("@receptionDate", allFinished ? (command.Date ?? DateTime.Today).Date : DBNull.Value);
        updateHeaderCommand.Parameters.AddWithValue("@isCancelled", anyCancelled);
        updateHeaderCommand.Parameters.AddWithValue("@centerCode", centerCode);
        updateHeaderCommand.Parameters.AddWithValue("@dispositionCode", command.DispositionCode.Value);
        await updateHeaderCommand.ExecuteNonQueryAsync(cancellationToken);
    }

    private static (bool IsDisposed, bool IsServed) MapDispositionFlags(string status) => NormalizeStatus(status, allowEmpty: false) switch
    {
        ParteAcabadoStatuses.Pending => (false, false),
        ParteAcabadoStatuses.InProgress => (true, false),
        ParteAcabadoStatuses.Finished => (true, true),
        ParteAcabadoStatuses.Cancelled => (false, false),
        _ => (false, false)
    };

    private static async Task ReplaceProgressMovementsAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        string orderId,
        Guid tenantId,
        Guid companyId,
        int orderNumber,
        SaveParteAcabadoCommand command,
        CancellationToken cancellationToken)
    {
        await using (var deleteCommand = connection.CreateCommand())
        {
            deleteCommand.Transaction = transaction;
            deleteCommand.CommandText =
                """
                DELETE FROM inventory_movements
                WHERE source_document_type IN ('FinishWorkOrderProgress', 'FinishWorkOrderCompleted')
                  AND source_document_id = @orderId;
                """;
            deleteCommand.Parameters.AddWithValue("@orderId", orderId);
            await deleteCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        foreach (var line in command.Lines.OrderBy(item => item.LineNumber))
        {
            var normalizedStatus = NormalizeStatus(line.Status, allowEmpty: false);
            if (normalizedStatus == ParteAcabadoStatuses.Pending || normalizedStatus == ParteAcabadoStatuses.Cancelled)
            {
                continue;
            }

            var movementType = normalizedStatus == ParteAcabadoStatuses.Finished
                ? StockMovementTypes.FinishWorkOrderFinished
                : StockMovementTypes.FinishWorkOrderInProgress;
            var sourceDocumentType = normalizedStatus == ParteAcabadoStatuses.Finished
                ? "FinishWorkOrderCompleted"
                : "FinishWorkOrderProgress";
            var warehouse = normalizedStatus == ParteAcabadoStatuses.Finished
                ? "ACABADO-TERMINADO"
                : "ACABADO-EN-PROCESO";
            var quantity = line.TotalKilograms > 0 ? line.TotalKilograms : line.TotalPieces;
            var unit = line.TotalKilograms > 0 ? "kg" : "pz";

            if (quantity <= 0)
            {
                continue;
            }

            await using var insertCommand = connection.CreateCommand();
            insertCommand.Transaction = transaction;
            insertCommand.CommandText =
                """
                INSERT INTO inventory_movements (
                    movement_id,
                    tenant_id,
                    company_id,
                    movement_type,
                    movement_date,
                    warehouse,
                    item_code,
                    item_description,
                    color,
                    quantity,
                    unit_of_measure,
                    source_document_type,
                    source_document_id,
                    source_document_number,
                    source_line_number,
                    notes,
                    created_utc)
                VALUES (
                    @movementId,
                    @tenantId,
                    @companyId,
                    @movementType,
                    @movementDate,
                    @warehouse,
                    @itemCode,
                    @itemDescription,
                    @color,
                    @quantity,
                    @unitOfMeasure,
                    @sourceDocumentType,
                    @sourceDocumentId,
                    @sourceDocumentNumber,
                    @sourceLineNumber,
                    @notes,
                    @createdUtc);
                """;
            insertCommand.Parameters.AddWithValue("@movementId", Guid.NewGuid().ToString());
            insertCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            insertCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
            insertCommand.Parameters.AddWithValue("@movementType", movementType);
            insertCommand.Parameters.AddWithValue("@movementDate", (command.Date ?? DateTime.Today).Date);
            insertCommand.Parameters.AddWithValue("@warehouse", warehouse);
            insertCommand.Parameters.AddWithValue("@itemCode", DbValue(line.FabricCode));
            insertCommand.Parameters.AddWithValue("@itemDescription", string.IsNullOrWhiteSpace(line.FabricDescription) ? line.FabricCode : line.FabricDescription);
            insertCommand.Parameters.AddWithValue("@color", DbValue(line.Color));
            insertCommand.Parameters.AddWithValue("@quantity", quantity);
            insertCommand.Parameters.AddWithValue("@unitOfMeasure", unit);
            insertCommand.Parameters.AddWithValue("@sourceDocumentType", sourceDocumentType);
            insertCommand.Parameters.AddWithValue("@sourceDocumentId", orderId);
            insertCommand.Parameters.AddWithValue("@sourceDocumentNumber", orderNumber);
            insertCommand.Parameters.AddWithValue("@sourceLineNumber", line.LineNumber);
            insertCommand.Parameters.AddWithValue("@notes", DbValue($"Parte acabado {orderNumber}: {GetStockMovementNote(normalizedStatus, line)}"));
            insertCommand.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
            await insertCommand.ExecuteNonQueryAsync(cancellationToken);
        }
    }

    private static string GetStockMovementNote(string status, SaveParteAcabadoLineInput line) => status switch
    {
        ParteAcabadoStatuses.InProgress => $"línea {line.LineNumber} en proceso para tejido {line.FabricCode}.",
        ParteAcabadoStatuses.Finished => $"línea {line.LineNumber} terminada para tejido {line.FabricCode}.",
        _ => $"línea {line.LineNumber}."
    };

    private static string NormalizeStatus(string? status, bool allowEmpty)
    {
        var normalized = (status ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(normalized))
        {
            return allowEmpty ? string.Empty : ParteAcabadoStatuses.Pending;
        }

        var match = ParteAcabadoStatuses.All.FirstOrDefault(item => string.Equals(item, normalized, StringComparison.OrdinalIgnoreCase));
        if (match is null)
        {
            throw new InvalidOperationException($"Estado de acabado no válido: {status}.");
        }

        return match;
    }

    private static (string PrimaryFabricCode, string PrimaryFabricDescription, string PrimaryColor, decimal TotalKilograms, decimal TotalPieces) BuildHeaderSummary(IEnumerable<SaveParteAcabadoLineInput> lines)
    {
        var ordered = lines.OrderBy(item => item.LineNumber).ToList();
        var first = ordered.First();
        return (
            first.FabricCode,
            first.FabricDescription,
            first.Color,
            ordered.Sum(item => item.TotalKilograms),
            ordered.Sum(item => item.TotalPieces));
    }

    private async Task HydrateLookupNamesAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        string centerCode,
        SaveParteAcabadoCommand command,
        CancellationToken cancellationToken)
    {
        if (command.ClientCode > 0)
        {
            command.ClientName = await ResolveLookupNameAsync(connection, transaction, "clients", centerCode, command.ClientCode, command.ClientName, cancellationToken);
        }

        if (command.FinisherCode > 0)
        {
            command.FinisherName = await ResolveLookupNameAsync(connection, transaction, "tallers", centerCode, command.FinisherCode, command.FinisherName, cancellationToken);
        }

        if (command.MachineCode <= 0)
        {
            command.MachineCode = await ResolveMachineCodeFromLinesAsync(connection, transaction, centerCode, command.Lines, cancellationToken);
        }

        if (command.MachineCode > 0)
        {
            command.MachineName = await ResolveMachineNameAsync(connection, transaction, command.TenantId, command.CompanyId, command.MachineCode, command.MachineName, cancellationToken);
        }

        if (command.OperationCode > 0)
        {
            command.OperationName = await ResolveOperationNameAsync(connection, transaction, command.TenantId, command.CompanyId, command.OperationCode, command.OperationName, cancellationToken);
        }

        foreach (var line in command.Lines)
        {
            if (string.IsNullOrWhiteSpace(line.FabricCode))
            {
                continue;
            }

            await using var commandLookup = connection.CreateCommand();
            commandLookup.Transaction = transaction;
            commandLookup.CommandText =
                """
                SELECT COALESCE(DESCRI, '')
                FROM teixits
                WHERE CENTRO = @centerCode
                  AND CODI = @code
                  AND is_deleted = 0
                LIMIT 1;
                """;
            commandLookup.Parameters.AddWithValue("@centerCode", centerCode);
            commandLookup.Parameters.AddWithValue("@code", line.FabricCode);
            var value = (await commandLookup.ExecuteScalarAsync(cancellationToken))?.ToString() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(line.FabricDescription) && !string.IsNullOrWhiteSpace(value))
            {
                line.FabricDescription = value.Trim();
            }
        }
    }

    private static async Task<string> ResolveLookupNameAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        string tableName,
        string centerCode,
        int code,
        string fallback,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            $"""
            SELECT COALESCE(NOM, '')
            FROM {tableName}
            WHERE CENTRO = @centerCode
              AND CODI = @code
              AND is_deleted = 0
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@code", code);
        var value = (await command.ExecuteScalarAsync(cancellationToken))?.ToString() ?? string.Empty;
        return string.IsNullOrWhiteSpace(value) ? fallback : value.Trim();
    }

    private static async Task<int> ResolveMachineCodeFromLinesAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        string centerCode,
        IEnumerable<SaveParteAcabadoLineInput> lines,
        CancellationToken cancellationToken)
    {
        foreach (var line in lines.OrderBy(item => item.LineNumber))
        {
            if (string.IsNullOrWhiteSpace(line.FabricCode))
            {
                continue;
            }

            await using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText =
                """
                SELECT COALESCE(MAQUI, 0)
                FROM teixits
                WHERE CENTRO = @centerCode
                  AND CODI = @code
                  AND is_deleted = 0
                LIMIT 1;
                """;
            command.Parameters.AddWithValue("@centerCode", centerCode);
            command.Parameters.AddWithValue("@code", line.FabricCode);
            var value = Convert.ToInt32((await command.ExecuteScalarAsync(cancellationToken)) ?? 0);
            if (value > 0)
            {
                return value;
            }
        }

        return 0;
    }

    private static async Task<string> ResolveMachineNameAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid tenantId,
        Guid companyId,
        int machineCode,
        string fallback,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            SELECT COALESCE(name, '')
            FROM base_catalog_items
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND catalog_key = 'maquinas'
              AND code = @code
              AND is_deleted = 0
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@code", machineCode.ToString());
        var value = (await command.ExecuteScalarAsync(cancellationToken))?.ToString() ?? string.Empty;
        return string.IsNullOrWhiteSpace(value) ? fallback : value.Trim();
    }

    private static async Task<string> ResolveOperationNameAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid tenantId,
        Guid companyId,
        int operationCode,
        string fallback,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            SELECT COALESCE(name, '')
            FROM base_catalog_items
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND catalog_key = @catalogKey
              AND code = @code
              AND is_deleted = 0
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@catalogKey", BaseCatalogKeys.Operations);
        command.Parameters.AddWithValue("@code", operationCode.ToString());
        var value = (await command.ExecuteScalarAsync(cancellationToken))?.ToString() ?? string.Empty;
        return string.IsNullOrWhiteSpace(value) ? fallback : value.Trim();
    }

    private static async Task<List<ParteAcabadoLineDto>> LoadLinesAsync(MySqlConnection connection, string orderId, CancellationToken cancellationToken)
    {
        var lines = new List<ParteAcabadoLineDto>();
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT line_number,
                   fabric_code,
                   fabric_description,
                   color,
                   total_kilograms,
                   total_pieces,
                   status,
                   notes
            FROM finish_work_order_lines
            WHERE order_id = @orderId
            ORDER BY line_number;
            """;
        command.Parameters.AddWithValue("@orderId", orderId);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            lines.Add(new ParteAcabadoLineDto
            {
                LineNumber = reader.GetInt32OrDefault("line_number"),
                FabricCode = reader.GetStringOrEmpty("fabric_code"),
                FabricDescription = reader.GetStringOrEmpty("fabric_description"),
                Color = reader.GetStringOrEmpty("color"),
                TotalKilograms = reader.GetDecimalOrDefault("total_kilograms"),
                TotalPieces = reader.GetDecimalOrDefault("total_pieces"),
                Status = reader.GetStringOrEmpty("status"),
                Notes = reader.GetStringOrEmpty("notes")
            });
        }

        return lines;
    }

    private static async Task ReplaceLinesAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        string orderId,
        IReadOnlyCollection<SaveParteAcabadoLineInput> lines,
        CancellationToken cancellationToken)
    {
        await using (var deleteCommand = connection.CreateCommand())
        {
            deleteCommand.Transaction = transaction;
            deleteCommand.CommandText = "DELETE FROM finish_work_order_lines WHERE order_id = @orderId;";
            deleteCommand.Parameters.AddWithValue("@orderId", orderId);
            await deleteCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        foreach (var line in lines.OrderBy(item => item.LineNumber))
        {
            await using var insertCommand = connection.CreateCommand();
            insertCommand.Transaction = transaction;
            insertCommand.CommandText =
                """
                INSERT INTO finish_work_order_lines (
                    order_id,
                    line_number,
                    fabric_code,
                    fabric_description,
                    color,
                    total_kilograms,
                    total_pieces,
                    status,
                    notes)
                VALUES (
                    @orderId,
                    @lineNumber,
                    @fabricCode,
                    @fabricDescription,
                    @color,
                    @totalKilograms,
                    @totalPieces,
                    @status,
                    @notes);
                """;
            insertCommand.Parameters.AddWithValue("@orderId", orderId);
            insertCommand.Parameters.AddWithValue("@lineNumber", line.LineNumber);
            insertCommand.Parameters.AddWithValue("@fabricCode", DbValue(line.FabricCode));
            insertCommand.Parameters.AddWithValue("@fabricDescription", DbValue(line.FabricDescription));
            insertCommand.Parameters.AddWithValue("@color", DbValue(line.Color));
            insertCommand.Parameters.AddWithValue("@totalKilograms", line.TotalKilograms);
            insertCommand.Parameters.AddWithValue("@totalPieces", line.TotalPieces);
            insertCommand.Parameters.AddWithValue("@status", line.Status);
            insertCommand.Parameters.AddWithValue("@notes", DbValue(line.Notes));
            await insertCommand.ExecuteNonQueryAsync(cancellationToken);
        }
    }

    private static void FillHeaderParameters(
        MySqlCommand command,
        string orderId,
        Guid tenantId,
        Guid companyId,
        string centerCode,
        int orderNumber,
        SaveParteAcabadoCommand source,
        (string PrimaryFabricCode, string PrimaryFabricDescription, string PrimaryColor, decimal TotalKilograms, decimal TotalPieces) summary,
        DateTime utcNow)
    {
        command.Parameters.AddWithValue("@orderId", orderId);
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@orderNumber", orderNumber);
        command.Parameters.AddWithValue("@workDate", (source.Date ?? DateTime.Today).Date);
        command.Parameters.AddWithValue("@status", source.Status);
        command.Parameters.AddWithValue("@clientCode", source.ClientCode);
        command.Parameters.AddWithValue("@clientName", DbValue(source.ClientName));
        command.Parameters.AddWithValue("@finisherCode", source.FinisherCode);
        command.Parameters.AddWithValue("@finisherName", DbValue(source.FinisherName));
        command.Parameters.AddWithValue("@machineCode", source.MachineCode);
        command.Parameters.AddWithValue("@machineName", DbValue(source.MachineName));
        command.Parameters.AddWithValue("@operationCode", source.OperationCode);
        command.Parameters.AddWithValue("@operationName", DbValue(source.OperationName));
        command.Parameters.AddWithValue("@dispositionCode", source.DispositionCode.HasValue ? source.DispositionCode.Value : DBNull.Value);
        command.Parameters.AddWithValue("@dispositionLabel", DbValue(source.DispositionLabel));
        command.Parameters.AddWithValue("@sourceSampleKind", DbValue(source.SourceSampleKind));
        command.Parameters.AddWithValue("@sourceSampleCode", DbValue(source.SourceSampleCode));
        command.Parameters.AddWithValue("@sourceSampleLineNumber", source.SourceSampleLineNumber.HasValue ? source.SourceSampleLineNumber.Value : DBNull.Value);
        command.Parameters.AddWithValue("@primaryFabricCode", DbValue(summary.PrimaryFabricCode));
        command.Parameters.AddWithValue("@primaryFabricDescription", DbValue(summary.PrimaryFabricDescription));
        command.Parameters.AddWithValue("@primaryColor", DbValue(summary.PrimaryColor));
        command.Parameters.AddWithValue("@totalKilograms", summary.TotalKilograms);
        command.Parameters.AddWithValue("@totalPieces", summary.TotalPieces);
        command.Parameters.AddWithValue("@notes", DbValue(source.Notes));
        command.Parameters.AddWithValue("@createdUtc", utcNow);
        command.Parameters.AddWithValue("@updatedUtc", utcNow);
    }

    private static async Task<int> GenerateNextOrderNumberAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid tenantId,
        Guid companyId,
        string centerCode,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            SELECT COALESCE(MAX(order_number), 0) + 1
            FROM finish_work_orders
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND center_code = @centerCode;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@centerCode", centerCode);
        return Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));
    }

    private static async Task<string> ResolveOrderIdAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid tenantId,
        Guid companyId,
        string centerCode,
        int orderNumber,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            SELECT order_id
            FROM finish_work_orders
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND center_code = @centerCode
              AND order_number = @orderNumber
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@orderNumber", orderNumber);
        return (await command.ExecuteScalarAsync(cancellationToken))?.ToString() ?? Guid.NewGuid().ToString();
    }

    private static void FillSearchParameters(
        MySqlCommand command,
        Guid tenantId,
        Guid companyId,
        string centerCode,
        string search,
        string likeSearch,
        string status,
        int finisherCode,
        int machineCode,
        int operationCode,
        string sourceSampleKind,
        string sourceSampleCode,
        bool liveOnly)
    {
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@search", search);
        command.Parameters.AddWithValue("@likeSearch", likeSearch);
        command.Parameters.AddWithValue("@status", status);
        command.Parameters.AddWithValue("@finisherCode", finisherCode);
        command.Parameters.AddWithValue("@machineCode", machineCode);
        command.Parameters.AddWithValue("@operationCode", operationCode);
        command.Parameters.AddWithValue("@sourceSampleKind", sourceSampleKind);
        command.Parameters.AddWithValue("@sourceSampleCode", sourceSampleCode);
        command.Parameters.AddWithValue("@liveOnly", liveOnly ? 1 : 0);
    }

    private static string BuildSearchOrderByClause(ParteAcabadoFilter filter)
    {
        var column = filter.SortColumn switch
        {
            nameof(ParteAcabadoListItemDto.OrderNumber) => "header.order_number",
            nameof(ParteAcabadoListItemDto.Date) => "header.work_date",
            nameof(ParteAcabadoListItemDto.Status) => "header.status",
            nameof(ParteAcabadoListItemDto.ClientName) => "header.client_name",
            nameof(ParteAcabadoListItemDto.FinisherName) => "header.finisher_name",
            nameof(ParteAcabadoListItemDto.MachineName) => "header.machine_name",
            nameof(ParteAcabadoListItemDto.OperationName) => "header.operation_name",
            nameof(ParteAcabadoListItemDto.PrimaryFabricCode) => "header.primary_fabric_code",
            nameof(ParteAcabadoListItemDto.PrimaryColor) => "header.primary_color",
            nameof(ParteAcabadoListItemDto.TotalKilograms) => "header.total_kilograms",
            nameof(ParteAcabadoListItemDto.TotalPieces) => "header.total_pieces",
            nameof(ParteAcabadoListItemDto.Origin) => "header.origin",
            _ => string.Empty
        };

        if (string.IsNullOrWhiteSpace(column))
        {
            return "ORDER BY header.work_date DESC, header.order_number DESC";
        }

        var direction = filter.SortDescending ? "DESC" : "ASC";
        return $"ORDER BY {column} {direction}, header.order_number DESC";
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

        throw new InvalidOperationException("No tienes permisos para editar órdenes / partes de acabado en este tenant.");
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

    private static DateTime? GetNullableDateTime(MySqlDataReader reader, string columnName)
    {
        var ordinal = reader.GetOrdinal(columnName);
        if (reader.IsDBNull(ordinal))
        {
            return null;
        }

        var value = reader.GetValue(ordinal);
        return value switch
        {
            DateTime dateTime => dateTime,
            string stringValue when DateTime.TryParse(stringValue, out var parsed) => parsed,
            _ => Convert.ToDateTime(value)
        };
    }

    private static object DbValue(string value) =>
        string.IsNullOrWhiteSpace(value) ? DBNull.Value : value;
}
