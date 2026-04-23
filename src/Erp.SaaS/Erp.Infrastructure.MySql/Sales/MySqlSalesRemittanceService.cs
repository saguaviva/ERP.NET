using Erp.Application.Auditing;
using Erp.Application.Companies;
using Erp.Application.Contexts;
using Erp.Application.Numbering;
using Erp.Application.Sales;
using Erp.Domain.Common;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Numbering;
using Erp.Infrastructure.MySql.Support;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Sales;

public sealed class MySqlSalesRemittanceService : ISalesRemittanceQueries, ISalesRemittanceService
{
    private readonly MySqlConnectionFactory _saasConnectionFactory;
    private readonly IAuditLogService _auditLogService;
    private readonly ICompanyAccessService _companyAccessService;
    private readonly ICurrentUserContext _currentUserContext;
    private readonly ITenantContext _tenantContext;
    private readonly IActiveCompanyContext _activeCompanyContext;

    public MySqlSalesRemittanceService(
        MySqlConnectionFactory saasConnectionFactory,
        IAuditLogService auditLogService,
        ICompanyAccessService companyAccessService,
        ICurrentUserContext currentUserContext,
        ITenantContext tenantContext,
        IActiveCompanyContext activeCompanyContext)
    {
        _saasConnectionFactory = saasConnectionFactory;
        _auditLogService = auditLogService;
        _companyAccessService = companyAccessService;
        _currentUserContext = currentUserContext;
        _tenantContext = tenantContext;
        _activeCompanyContext = activeCompanyContext;
    }

    public async Task<SalesRemittanceSearchResultDto> SearchAsync(
        Guid tenantId,
        Guid companyId,
        SalesRemittanceFilter filter,
        CancellationToken cancellationToken = default)
    {
        if (!_saasConnectionFactory.IsConfigured)
        {
            return new SalesRemittanceSearchResultDto();
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);

        var pageSize = Math.Clamp(filter.PageSize, 10, 200);
        var page = Math.Max(filter.Page, 1);
        var offset = (page - 1) * pageSize;
        var search = filter.Search?.Trim() ?? string.Empty;
        var likeSearch = $"%{search}%";
        var status = NormalizeStatus(filter.Status);

        await using var connection = await _saasConnectionFactory.OpenConnectionAsync(cancellationToken);
        await using (var countCommand = connection.CreateCommand())
        {
            countCommand.CommandText =
                """
                SELECT COUNT(*)
                FROM sales_remittances sr
                WHERE sr.tenant_id = @tenantId
                  AND sr.company_id = @companyId
                  AND (
                        @includeClosed = 1
                        OR (
                            sr.status NOT IN ('Collected', 'Cancelled')
                            AND COALESCE(sr.is_deleted, 0) = 0
                        )
                      )
                  AND (
                        @status = ''
                        OR sr.status = @status
                      )
                  AND (
                        @search = ''
                        OR CAST(sr.remittance_number AS CHAR) LIKE @likeSearch
                        OR COALESCE(sr.bank_name, '') LIKE @likeSearch
                        OR sr.notes LIKE @likeSearch
                      );
                """;
            countCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            countCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
            countCommand.Parameters.AddWithValue("@includeClosed", filter.IncludeClosed);
            countCommand.Parameters.AddWithValue("@status", status);
            countCommand.Parameters.AddWithValue("@search", search);
            countCommand.Parameters.AddWithValue("@likeSearch", likeSearch);

            var totalCount = Convert.ToInt32(await countCommand.ExecuteScalarAsync(cancellationToken));
            if (totalCount == 0)
            {
                return new SalesRemittanceSearchResultDto();
            }

            await using var command = connection.CreateCommand();
            command.CommandText =
                $"""
                SELECT sr.remittance_id,
                       sr.remittance_series,
                       sr.remittance_number,
                       sr.remittance_date,
                       sr.due_date,
                       sr.status,
                       sr.bank_name,
                       sr.invoice_count,
                       sr.client_count,
                       sr.total_amount,
                       sr.collected_amount,
                       sr.outstanding_amount,
                       sr.notes,
                       sr.sent_utc,
                       sr.collected_utc
                FROM sales_remittances sr
                WHERE sr.tenant_id = @tenantId
                  AND sr.company_id = @companyId
                  AND (
                        @includeClosed = 1
                        OR (
                            sr.status NOT IN ('Collected', 'Cancelled')
                            AND COALESCE(sr.is_deleted, 0) = 0
                        )
                      )
                  AND (
                        @status = ''
                        OR sr.status = @status
                      )
                  AND (
                        @search = ''
                        OR CAST(sr.remittance_number AS CHAR) LIKE @likeSearch
                        OR COALESCE(sr.bank_name, '') LIKE @likeSearch
                        OR sr.notes LIKE @likeSearch
                      )
                {BuildSearchOrderByClause(filter)}
                LIMIT @limit OFFSET @offset;
                """;
            command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            command.Parameters.AddWithValue("@companyId", companyId.ToString());
            command.Parameters.AddWithValue("@includeClosed", filter.IncludeClosed);
            command.Parameters.AddWithValue("@status", status);
            command.Parameters.AddWithValue("@search", search);
            command.Parameters.AddWithValue("@likeSearch", likeSearch);
            command.Parameters.AddWithValue("@limit", pageSize);
            command.Parameters.AddWithValue("@offset", offset);

            var items = new List<SalesRemittanceListItemDto>();
            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                items.Add(MapListItem(reader));
            }

            return new SalesRemittanceSearchResultDto
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }

    public async Task<SalesRemittanceDetailDto?> GetByNumberAsync(
        Guid tenantId,
        Guid companyId,
        int remittanceNumber,
        CancellationToken cancellationToken = default)
    {
        if (!_saasConnectionFactory.IsConfigured)
        {
            return null;
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);

        await using var connection = await _saasConnectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT sr.remittance_id,
                   sr.remittance_series,
                   sr.remittance_number,
                   sr.remittance_date,
                   sr.due_date,
                   sr.status,
                   sr.bank_name,
                   sr.invoice_count,
                   sr.client_count,
                   sr.total_amount,
                   sr.collected_amount,
                   sr.outstanding_amount,
                   sr.notes,
                   sr.sent_utc,
                   sr.collected_utc
            FROM sales_remittances sr
            WHERE sr.tenant_id = @tenantId
              AND sr.company_id = @companyId
              AND sr.remittance_number = @remittanceNumber
              AND COALESCE(sr.is_deleted, 0) = 0
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@remittanceNumber", remittanceNumber);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        var detail = new SalesRemittanceDetailDto
        {
            RemittanceId = reader.GetGuid("remittance_id"),
            RemittanceSeries = reader.GetStringOrEmpty("remittance_series"),
            RemittanceNumber = reader.GetInt32(reader.GetOrdinal("remittance_number")),
            RemittanceDate = reader.GetDateTime(reader.GetOrdinal("remittance_date")),
            DueDate = reader.IsDBNull(reader.GetOrdinal("due_date")) ? null : reader.GetDateTime(reader.GetOrdinal("due_date")),
            Status = reader.GetStringOrEmpty("status"),
            BankName = reader.GetStringOrEmpty("bank_name"),
            InvoiceCount = reader.GetInt32OrDefault("invoice_count"),
            ClientCount = reader.GetInt32OrDefault("client_count"),
            TotalAmount = reader.GetDecimalOrDefault("total_amount"),
            CollectedAmount = reader.GetDecimalOrDefault("collected_amount"),
            OutstandingAmount = reader.GetDecimalOrDefault("outstanding_amount"),
            Notes = reader.GetStringOrEmpty("notes"),
            SentUtc = reader.IsDBNull(reader.GetOrdinal("sent_utc")) ? null : reader.GetDateTime(reader.GetOrdinal("sent_utc")),
            CollectedUtc = reader.IsDBNull(reader.GetOrdinal("collected_utc")) ? null : reader.GetDateTime(reader.GetOrdinal("collected_utc"))
        };
        await reader.DisposeAsync();

        detail.Invoices = await LoadRemittanceInvoicesAsync(connection, tenantId, companyId, detail.RemittanceId, cancellationToken);
        return detail;
    }

    public async Task<SalesRemittanceCandidateSearchResultDto> SearchCandidateInvoicesAsync(
        Guid tenantId,
        Guid companyId,
        SalesRemittanceCandidateFilter filter,
        CancellationToken cancellationToken = default)
    {
        if (!_saasConnectionFactory.IsConfigured)
        {
            return new SalesRemittanceCandidateSearchResultDto();
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);

        var pageSize = Math.Clamp(filter.PageSize, 10, 100);
        var page = Math.Max(filter.Page, 1);
        var offset = (page - 1) * pageSize;
        var search = filter.Search?.Trim() ?? string.Empty;
        var likeSearch = $"%{search}%";

        await using var connection = await _saasConnectionFactory.OpenConnectionAsync(cancellationToken);
        await using (var countCommand = connection.CreateCommand())
        {
            countCommand.CommandText =
                """
                SELECT COUNT(*)
                FROM sales_invoices si
                LEFT JOIN sales_remittance_invoices sri
                  ON sri.tenant_id = si.tenant_id
                 AND sri.company_id = si.company_id
                 AND sri.invoice_id = si.invoice_id
                LEFT JOIN sales_remittances sr
                  ON sr.remittance_id = sri.remittance_id
                 AND sr.tenant_id = sri.tenant_id
                 AND sr.company_id = sri.company_id
                 AND COALESCE(sr.is_deleted, 0) = 0
                 AND sr.status <> 'Cancelled'
                WHERE si.tenant_id = @tenantId
                  AND si.company_id = @companyId
                  AND COALESCE(si.is_deleted, 0) = 0
                  AND si.status <> 'Cancelled'
                  AND si.outstanding_amount > 0
                  AND sr.remittance_id IS NULL
                  AND (
                        @search = ''
                        OR CAST(si.invoice_number AS CHAR) LIKE @likeSearch
                        OR COALESCE(si.client_name, '') LIKE @likeSearch
                        OR COALESCE(si.notes, '') LIKE @likeSearch
                      );
                """;
            countCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            countCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
            countCommand.Parameters.AddWithValue("@search", search);
            countCommand.Parameters.AddWithValue("@likeSearch", likeSearch);

            var totalCount = Convert.ToInt32(await countCommand.ExecuteScalarAsync(cancellationToken));
            if (totalCount == 0)
            {
                return new SalesRemittanceCandidateSearchResultDto();
            }

            await using var command = connection.CreateCommand();
            command.CommandText =
                """
                SELECT si.invoice_id,
                       si.invoice_series,
                       si.invoice_number,
                       si.client_code,
                       si.client_name,
                       si.issue_date,
                       si.due_date,
                       si.total_amount,
                       si.amount_paid,
                       si.outstanding_amount,
                       si.payment_status,
                       si.notes
                FROM sales_invoices si
                LEFT JOIN sales_remittance_invoices sri
                  ON sri.tenant_id = si.tenant_id
                 AND sri.company_id = si.company_id
                 AND sri.invoice_id = si.invoice_id
                LEFT JOIN sales_remittances sr
                  ON sr.remittance_id = sri.remittance_id
                 AND sr.tenant_id = sri.tenant_id
                 AND sr.company_id = sri.company_id
                 AND COALESCE(sr.is_deleted, 0) = 0
                 AND sr.status <> 'Cancelled'
                WHERE si.tenant_id = @tenantId
                  AND si.company_id = @companyId
                  AND COALESCE(si.is_deleted, 0) = 0
                  AND si.status <> 'Cancelled'
                  AND si.outstanding_amount > 0
                  AND sr.remittance_id IS NULL
                  AND (
                        @search = ''
                        OR CAST(si.invoice_number AS CHAR) LIKE @likeSearch
                        OR COALESCE(si.client_name, '') LIKE @likeSearch
                        OR COALESCE(si.notes, '') LIKE @likeSearch
                      )
                ORDER BY COALESCE(si.due_date, si.issue_date) ASC, si.invoice_number ASC
                LIMIT @limit OFFSET @offset;
                """;
            command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            command.Parameters.AddWithValue("@companyId", companyId.ToString());
            command.Parameters.AddWithValue("@search", search);
            command.Parameters.AddWithValue("@likeSearch", likeSearch);
            command.Parameters.AddWithValue("@limit", pageSize);
            command.Parameters.AddWithValue("@offset", offset);

            var items = new List<SalesRemittanceCandidateInvoiceDto>();
            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                items.Add(MapCandidateInvoice(reader));
            }

            return new SalesRemittanceCandidateSearchResultDto
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }

    public async Task<SalesRemittanceCandidateInvoiceDto?> GetCandidateInvoiceByNumberAsync(
        Guid tenantId,
        Guid companyId,
        int invoiceNumber,
        CancellationToken cancellationToken = default)
    {
        if (!_saasConnectionFactory.IsConfigured)
        {
            return null;
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);

        await using var connection = await _saasConnectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT si.invoice_id,
                   si.invoice_series,
                   si.invoice_number,
                   si.client_code,
                   si.client_name,
                   si.issue_date,
                   si.due_date,
                   si.total_amount,
                   si.amount_paid,
                   si.outstanding_amount,
                   si.payment_status,
                   si.notes
            FROM sales_invoices si
            LEFT JOIN sales_remittance_invoices sri
              ON sri.tenant_id = si.tenant_id
             AND sri.company_id = si.company_id
             AND sri.invoice_id = si.invoice_id
            LEFT JOIN sales_remittances sr
              ON sr.remittance_id = sri.remittance_id
             AND sr.tenant_id = sri.tenant_id
             AND sr.company_id = sri.company_id
             AND COALESCE(sr.is_deleted, 0) = 0
             AND sr.status <> 'Cancelled'
            WHERE si.tenant_id = @tenantId
              AND si.company_id = @companyId
              AND COALESCE(si.is_deleted, 0) = 0
              AND si.status <> 'Cancelled'
              AND si.outstanding_amount > 0
              AND sr.remittance_id IS NULL
              AND si.invoice_number = @invoiceNumber
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@invoiceNumber", invoiceNumber);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        return await reader.ReadAsync(cancellationToken) ? MapCandidateInvoice(reader) : null;
    }

    public async Task<int> SaveAsync(
        Guid tenantId,
        Guid companyId,
        SaveSalesRemittanceCommand command,
        CancellationToken cancellationToken = default)
    {
        if (!_saasConnectionFactory.IsConfigured)
        {
            throw new InvalidOperationException("La base de datos SaaS no está configurada.");
        }

        EnsureTenantWriteAccess();
        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);
        NormalizeAndValidate(command);

        await using var connection = await _saasConnectionFactory.OpenConnectionAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        RemittanceHeader? existingHeader = null;
        HashSet<int> existingInvoiceNumbers = [];

        if (command.RemittanceNumber.HasValue)
        {
            existingHeader = await LoadHeaderForUpdateAsync(connection, transaction, tenantId, companyId, command.RemittanceNumber.Value, cancellationToken)
                ?? throw new InvalidOperationException("La remesa indicada no existe o ya no está disponible.");
            existingInvoiceNumbers = await LoadExistingInvoiceNumbersAsync(connection, transaction, tenantId, companyId, existingHeader.RemittanceId, cancellationToken);
        }

        var invoices = await LoadInvoiceSnapshotsAsync(
            connection,
            transaction,
            tenantId,
            companyId,
            command.Invoices.Select(item => item.InvoiceNumber).ToArray(),
            cancellationToken);

        if (invoices.Count != command.Invoices.Count)
        {
            throw new InvalidOperationException("Alguna factura ya no está disponible para remesar.");
        }

        if (existingHeader is null)
        {
            if (invoices.Any(item => item.OutstandingAmount <= 0m))
            {
                throw new InvalidOperationException("Solo puedes crear remesas con facturas que todavía tengan importe pendiente.");
            }
        }
        else
        {
            var invalidNewInvoice = invoices.FirstOrDefault(item => item.OutstandingAmount <= 0m && !existingInvoiceNumbers.Contains(item.InvoiceNumber));
            if (invalidNewInvoice is not null)
            {
                throw new InvalidOperationException($"La factura {invalidNewInvoice.DisplayNumber} ya no tiene importe pendiente y no se puede añadir a la remesa.");
            }
        }

        await EnsureInvoicesNotLinkedToOtherActiveRemittancesAsync(
            connection,
            transaction,
            tenantId,
            companyId,
            invoices.Select(item => item.InvoiceId).ToArray(),
            existingHeader?.RemittanceId,
            cancellationToken);

        var totals = BuildTotals(invoices);
        var status = NormalizeStatus(command.Status);
        var remittanceId = existingHeader?.RemittanceId ?? Guid.NewGuid();
        var remittanceNumber = existingHeader?.RemittanceNumber ?? await GetNextRemittanceNumberAsync(connection, transaction, tenantId, companyId, cancellationToken);
        var remittanceSeries = string.IsNullOrWhiteSpace(command.RemittanceSeries)
            ? BuildRemittanceSeries(companyId)
            : command.RemittanceSeries.Trim().ToUpperInvariant();
        var remittanceDate = command.RemittanceDate == default ? DateTime.Today : command.RemittanceDate.Date;
        var dueDate = command.DueDate?.Date;
        var nowUtc = DateTime.UtcNow;
        var sentUtc = status switch
        {
            SalesRemittanceStatuses.Sent => existingHeader?.SentUtc ?? nowUtc,
            SalesRemittanceStatuses.Collected => existingHeader?.SentUtc ?? nowUtc,
            _ => existingHeader?.SentUtc
        };
        var collectedUtc = status == SalesRemittanceStatuses.Collected
            ? existingHeader?.CollectedUtc ?? nowUtc
            : status == SalesRemittanceStatuses.Cancelled
                ? null
                : existingHeader?.CollectedUtc;

        await UpsertHeaderAsync(
            connection,
            transaction,
            tenantId,
            companyId,
            remittanceId,
            remittanceSeries,
            remittanceNumber,
            remittanceDate,
            dueDate,
            status,
            command.BankName,
            command.Notes,
            totals,
            sentUtc,
            collectedUtc,
            nowUtc,
            existingHeader is not null,
            cancellationToken);

        await DeleteLinesAsync(connection, transaction, remittanceId, cancellationToken);
        await InsertLinesAsync(connection, transaction, tenantId, companyId, remittanceId, invoices, cancellationToken);

        await transaction.CommitAsync(cancellationToken);

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = tenantId,
            CompanyId = companyId,
            UserId = _currentUserContext.UserId,
            Action = existingHeader is null ? "sales-remittance.create" : "sales-remittance.update",
            EntityName = "SalesRemittance",
            EntityId = remittanceId.ToString(),
            Details = $"Remesa {remittanceSeries}/{remittanceNumber:000000} · Facturas={totals.InvoiceCount} · Pendiente={totals.OutstandingAmount:0.00}"
        }, cancellationToken);

        return remittanceNumber;
    }

    private async Task<IReadOnlyCollection<SalesRemittanceInvoiceDto>> LoadRemittanceInvoicesAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        Guid remittanceId,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT line_number,
                   invoice_id,
                   invoice_series,
                   invoice_number,
                   client_code,
                   client_name,
                   issue_date,
                   due_date,
                   total_amount,
                   amount_paid,
                   outstanding_amount,
                   payment_status,
                   notes
            FROM sales_remittance_invoices
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND remittance_id = @remittanceId
            ORDER BY line_number;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@remittanceId", remittanceId.ToString());

        var items = new List<SalesRemittanceInvoiceDto>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new SalesRemittanceInvoiceDto
            {
                LineNumber = reader.GetInt32(reader.GetOrdinal("line_number")),
                InvoiceId = reader.GetGuid("invoice_id"),
                InvoiceSeries = reader.GetStringOrEmpty("invoice_series"),
                InvoiceNumber = reader.GetInt32(reader.GetOrdinal("invoice_number")),
                ClientCode = reader.GetInt32(reader.GetOrdinal("client_code")),
                ClientName = reader.GetStringOrEmpty("client_name"),
                IssueDate = reader.GetDateTime(reader.GetOrdinal("issue_date")),
                DueDate = reader.IsDBNull(reader.GetOrdinal("due_date")) ? null : reader.GetDateTime(reader.GetOrdinal("due_date")),
                TotalAmount = reader.GetDecimalOrDefault("total_amount"),
                AmountPaid = reader.GetDecimalOrDefault("amount_paid"),
                OutstandingAmount = reader.GetDecimalOrDefault("outstanding_amount"),
                PaymentStatus = reader.GetStringOrEmpty("payment_status"),
                Notes = reader.GetStringOrEmpty("notes")
            });
        }

        return items;
    }

    private static SalesRemittanceListItemDto MapListItem(MySqlDataReader reader) =>
        new()
        {
            RemittanceId = reader.GetGuid("remittance_id"),
            RemittanceSeries = reader.GetStringOrEmpty("remittance_series"),
            RemittanceNumber = reader.GetInt32(reader.GetOrdinal("remittance_number")),
            RemittanceDate = reader.GetDateTime(reader.GetOrdinal("remittance_date")),
            DueDate = reader.IsDBNull(reader.GetOrdinal("due_date")) ? null : reader.GetDateTime(reader.GetOrdinal("due_date")),
            Status = reader.GetStringOrEmpty("status"),
            BankName = reader.GetStringOrEmpty("bank_name"),
            InvoiceCount = reader.GetInt32OrDefault("invoice_count"),
            ClientCount = reader.GetInt32OrDefault("client_count"),
            TotalAmount = reader.GetDecimalOrDefault("total_amount"),
            CollectedAmount = reader.GetDecimalOrDefault("collected_amount"),
            OutstandingAmount = reader.GetDecimalOrDefault("outstanding_amount"),
            Notes = reader.GetStringOrEmpty("notes"),
            SentUtc = reader.IsDBNull(reader.GetOrdinal("sent_utc")) ? null : reader.GetDateTime(reader.GetOrdinal("sent_utc")),
            CollectedUtc = reader.IsDBNull(reader.GetOrdinal("collected_utc")) ? null : reader.GetDateTime(reader.GetOrdinal("collected_utc"))
        };

    private static SalesRemittanceCandidateInvoiceDto MapCandidateInvoice(MySqlDataReader reader) =>
        new()
        {
            InvoiceId = reader.GetGuid("invoice_id"),
            InvoiceSeries = reader.GetStringOrEmpty("invoice_series"),
            InvoiceNumber = reader.GetInt32(reader.GetOrdinal("invoice_number")),
            ClientCode = reader.GetInt32(reader.GetOrdinal("client_code")),
            ClientName = reader.GetStringOrEmpty("client_name"),
            IssueDate = reader.GetDateTime(reader.GetOrdinal("issue_date")),
            DueDate = reader.IsDBNull(reader.GetOrdinal("due_date")) ? null : reader.GetDateTime(reader.GetOrdinal("due_date")),
            TotalAmount = reader.GetDecimalOrDefault("total_amount"),
            AmountPaid = reader.GetDecimalOrDefault("amount_paid"),
            OutstandingAmount = reader.GetDecimalOrDefault("outstanding_amount"),
            PaymentStatus = reader.GetStringOrEmpty("payment_status"),
            Notes = reader.GetStringOrEmpty("notes")
        };

    private static async Task<int> GetNextRemittanceNumberAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid tenantId,
        Guid companyId,
        CancellationToken cancellationToken)
        => await DocumentNumberingSqlHelper.ReserveNextNumberAsync(
            connection,
            transaction,
            tenantId,
            companyId,
            DocumentNumberingKeys.SalesRemittance,
            cancellationToken);

    private static async Task<RemittanceHeader?> LoadHeaderForUpdateAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid tenantId,
        Guid companyId,
        int remittanceNumber,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            SELECT remittance_id,
                   remittance_number,
                   status,
                   sent_utc,
                   collected_utc
            FROM sales_remittances
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND remittance_number = @remittanceNumber
              AND COALESCE(is_deleted, 0) = 0
            LIMIT 1
            FOR UPDATE;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@remittanceNumber", remittanceNumber);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        return new RemittanceHeader(
            reader.GetGuid("remittance_id"),
            reader.GetInt32(reader.GetOrdinal("remittance_number")),
            reader.GetStringOrEmpty("status"),
            reader.IsDBNull(reader.GetOrdinal("sent_utc")) ? null : reader.GetDateTime(reader.GetOrdinal("sent_utc")),
            reader.IsDBNull(reader.GetOrdinal("collected_utc")) ? null : reader.GetDateTime(reader.GetOrdinal("collected_utc")));
    }

    private static async Task<HashSet<int>> LoadExistingInvoiceNumbersAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid tenantId,
        Guid companyId,
        Guid remittanceId,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            SELECT invoice_number
            FROM sales_remittance_invoices
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND remittance_id = @remittanceId;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@remittanceId", remittanceId.ToString());

        var items = new HashSet<int>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(reader.GetInt32(reader.GetOrdinal("invoice_number")));
        }

        return items;
    }

    private static async Task<IReadOnlyCollection<InvoiceSnapshot>> LoadInvoiceSnapshotsAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid tenantId,
        Guid companyId,
        IReadOnlyCollection<int> invoiceNumbers,
        CancellationToken cancellationToken)
    {
        if (invoiceNumbers.Count == 0)
        {
            return [];
        }

        var parameterNames = invoiceNumbers.Select((_, index) => $"@invoiceNumber{index}").ToArray();
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            $"""
            SELECT invoice_id,
                   invoice_series,
                   invoice_number,
                   client_code,
                   client_name,
                   issue_date,
                   due_date,
                   status,
                   payment_status,
                   total_amount,
                   amount_paid,
                   outstanding_amount,
                   notes
            FROM sales_invoices
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND COALESCE(is_deleted, 0) = 0
              AND invoice_number IN ({string.Join(", ", parameterNames)});
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        for (var index = 0; index < invoiceNumbers.Count; index++)
        {
            command.Parameters.AddWithValue(parameterNames[index], invoiceNumbers.ElementAt(index));
        }

        var items = new List<InvoiceSnapshot>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var status = reader.GetStringOrEmpty("status");
            if (string.Equals(status, SalesInvoiceStatuses.Cancelled, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            items.Add(new InvoiceSnapshot(
                reader.GetGuid("invoice_id"),
                reader.GetStringOrEmpty("invoice_series"),
                reader.GetInt32(reader.GetOrdinal("invoice_number")),
                reader.GetInt32(reader.GetOrdinal("client_code")),
                reader.GetStringOrEmpty("client_name"),
                reader.GetDateTime(reader.GetOrdinal("issue_date")),
                reader.IsDBNull(reader.GetOrdinal("due_date")) ? null : reader.GetDateTime(reader.GetOrdinal("due_date")),
                reader.GetDecimalOrDefault("total_amount"),
                reader.GetDecimalOrDefault("amount_paid"),
                reader.GetDecimalOrDefault("outstanding_amount"),
                reader.GetStringOrEmpty("payment_status"),
                reader.GetStringOrEmpty("notes")));
        }

        return items;
    }

    private static async Task EnsureInvoicesNotLinkedToOtherActiveRemittancesAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid tenantId,
        Guid companyId,
        IReadOnlyCollection<Guid> invoiceIds,
        Guid? currentRemittanceId,
        CancellationToken cancellationToken)
    {
        if (invoiceIds.Count == 0)
        {
            return;
        }

        var parameterNames = invoiceIds.Select((_, index) => $"@invoiceId{index}").ToArray();
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            $"""
            SELECT sri.invoice_number
            FROM sales_remittance_invoices sri
            INNER JOIN sales_remittances sr
              ON sr.remittance_id = sri.remittance_id
             AND sr.tenant_id = sri.tenant_id
             AND sr.company_id = sri.company_id
            WHERE sri.tenant_id = @tenantId
              AND sri.company_id = @companyId
              AND sri.invoice_id IN ({string.Join(", ", parameterNames)})
              AND COALESCE(sr.is_deleted, 0) = 0
              AND sr.status <> 'Cancelled'
              AND (@currentRemittanceId = '' OR sr.remittance_id <> @currentRemittanceId)
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@currentRemittanceId", currentRemittanceId?.ToString() ?? string.Empty);
        for (var index = 0; index < invoiceIds.Count; index++)
        {
            command.Parameters.AddWithValue(parameterNames[index], invoiceIds.ElementAt(index).ToString());
        }

        var alreadyLinkedInvoice = await command.ExecuteScalarAsync(cancellationToken);
        if (alreadyLinkedInvoice is not null)
        {
            throw new InvalidOperationException($"La factura {alreadyLinkedInvoice} ya está incluida en otra remesa activa.");
        }
    }

    private static RemittanceTotals BuildTotals(IReadOnlyCollection<InvoiceSnapshot> invoices)
    {
        var totalAmount = invoices.Sum(item => item.TotalAmount);
        var collectedAmount = invoices.Sum(item => item.AmountPaid);
        var outstandingAmount = invoices.Sum(item => item.OutstandingAmount);
        var invoiceCount = invoices.Count;
        var clientCount = invoices.Select(item => item.ClientCode).Distinct().Count();
        return new RemittanceTotals(invoiceCount, clientCount, totalAmount, collectedAmount, outstandingAmount);
    }

    private static async Task UpsertHeaderAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid tenantId,
        Guid companyId,
        Guid remittanceId,
        string remittanceSeries,
        int remittanceNumber,
        DateTime remittanceDate,
        DateTime? dueDate,
        string status,
        string bankName,
        string notes,
        RemittanceTotals totals,
        DateTime? sentUtc,
        DateTime? collectedUtc,
        DateTime nowUtc,
        bool isUpdate,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = isUpdate
            ? """
              UPDATE sales_remittances
              SET remittance_series = @remittanceSeries,
                  remittance_date = @remittanceDate,
                  due_date = @dueDate,
                  status = @status,
                  bank_name = @bankName,
                  invoice_count = @invoiceCount,
                  client_count = @clientCount,
                  total_amount = @totalAmount,
                  collected_amount = @collectedAmount,
                  outstanding_amount = @outstandingAmount,
                  notes = @notes,
                  sent_utc = @sentUtc,
                  collected_utc = @collectedUtc,
                  updated_utc = @updatedUtc
              WHERE remittance_id = @remittanceId;
              """
            : """
              INSERT INTO sales_remittances (
                  remittance_id,
                  remittance_series,
                  remittance_number,
                  tenant_id,
                  company_id,
                  remittance_date,
                  due_date,
                  status,
                  bank_name,
                  invoice_count,
                  client_count,
                  total_amount,
                  collected_amount,
                  outstanding_amount,
                  notes,
                  sent_utc,
                  collected_utc,
                  origin,
                  is_deleted,
                  created_utc,
                  updated_utc
              )
              VALUES (
                  @remittanceId,
                  @remittanceSeries,
                  @remittanceNumber,
                  @tenantId,
                  @companyId,
                  @remittanceDate,
                  @dueDate,
                  @status,
                  @bankName,
                  @invoiceCount,
                  @clientCount,
                  @totalAmount,
                  @collectedAmount,
                  @outstandingAmount,
                  @notes,
                  @sentUtc,
                  @collectedUtc,
                  'saas',
                  0,
                  @createdUtc,
                  @updatedUtc
              );
              """;
        command.Parameters.AddWithValue("@remittanceId", remittanceId.ToString());
        command.Parameters.AddWithValue("@remittanceSeries", DbValue(remittanceSeries));
        command.Parameters.AddWithValue("@remittanceNumber", remittanceNumber);
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@remittanceDate", remittanceDate);
        command.Parameters.AddWithValue("@dueDate", DbValue(dueDate));
        command.Parameters.AddWithValue("@status", status);
        command.Parameters.AddWithValue("@bankName", DbValue(bankName));
        command.Parameters.AddWithValue("@invoiceCount", totals.InvoiceCount);
        command.Parameters.AddWithValue("@clientCount", totals.ClientCount);
        command.Parameters.AddWithValue("@totalAmount", totals.TotalAmount);
        command.Parameters.AddWithValue("@collectedAmount", totals.CollectedAmount);
        command.Parameters.AddWithValue("@outstandingAmount", totals.OutstandingAmount);
        command.Parameters.AddWithValue("@notes", DbValue(notes));
        command.Parameters.AddWithValue("@sentUtc", DbValue(sentUtc));
        command.Parameters.AddWithValue("@collectedUtc", DbValue(collectedUtc));
        command.Parameters.AddWithValue("@createdUtc", nowUtc);
        command.Parameters.AddWithValue("@updatedUtc", nowUtc);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task DeleteLinesAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid remittanceId,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = "DELETE FROM sales_remittance_invoices WHERE remittance_id = @remittanceId;";
        command.Parameters.AddWithValue("@remittanceId", remittanceId.ToString());
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task InsertLinesAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid tenantId,
        Guid companyId,
        Guid remittanceId,
        IReadOnlyCollection<InvoiceSnapshot> invoices,
        CancellationToken cancellationToken)
    {
        foreach (var item in invoices.OrderBy(item => item.DueDate ?? item.IssueDate).ThenBy(item => item.InvoiceNumber).Select((invoice, index) => new { invoice, lineNumber = index + 1 }))
        {
            await using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText =
                """
                INSERT INTO sales_remittance_invoices (
                    remittance_id,
                    tenant_id,
                    company_id,
                    line_number,
                    invoice_id,
                    invoice_series,
                    invoice_number,
                    client_code,
                    client_name,
                    issue_date,
                    due_date,
                    total_amount,
                    amount_paid,
                    outstanding_amount,
                    payment_status,
                    notes
                )
                VALUES (
                    @remittanceId,
                    @tenantId,
                    @companyId,
                    @lineNumber,
                    @invoiceId,
                    @invoiceSeries,
                    @invoiceNumber,
                    @clientCode,
                    @clientName,
                    @issueDate,
                    @dueDate,
                    @totalAmount,
                    @amountPaid,
                    @outstandingAmount,
                    @paymentStatus,
                    @notes
                );
                """;
            command.Parameters.AddWithValue("@remittanceId", remittanceId.ToString());
            command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            command.Parameters.AddWithValue("@companyId", companyId.ToString());
            command.Parameters.AddWithValue("@lineNumber", item.lineNumber);
            command.Parameters.AddWithValue("@invoiceId", item.invoice.InvoiceId.ToString());
            command.Parameters.AddWithValue("@invoiceSeries", DbValue(item.invoice.InvoiceSeries));
            command.Parameters.AddWithValue("@invoiceNumber", item.invoice.InvoiceNumber);
            command.Parameters.AddWithValue("@clientCode", item.invoice.ClientCode);
            command.Parameters.AddWithValue("@clientName", item.invoice.ClientName);
            command.Parameters.AddWithValue("@issueDate", item.invoice.IssueDate);
            command.Parameters.AddWithValue("@dueDate", DbValue(item.invoice.DueDate));
            command.Parameters.AddWithValue("@totalAmount", item.invoice.TotalAmount);
            command.Parameters.AddWithValue("@amountPaid", item.invoice.AmountPaid);
            command.Parameters.AddWithValue("@outstandingAmount", item.invoice.OutstandingAmount);
            command.Parameters.AddWithValue("@paymentStatus", item.invoice.PaymentStatus);
            command.Parameters.AddWithValue("@notes", DbValue(item.invoice.Notes));
            await command.ExecuteNonQueryAsync(cancellationToken);
        }
    }

    private static string NormalizeStatus(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return SalesRemittanceStatuses.Draft;
        }

        return SalesRemittanceStatuses.All.FirstOrDefault(item => string.Equals(item, value, StringComparison.OrdinalIgnoreCase))
            ?? SalesRemittanceStatuses.Draft;
    }

    private static void NormalizeAndValidate(SaveSalesRemittanceCommand command)
    {
        command.RemittanceSeries = command.RemittanceSeries.Trim();
        command.BankName = command.BankName.Trim();
        command.Notes = command.Notes.Trim();
        command.Status = NormalizeStatus(command.Status);
        command.RemittanceDate = command.RemittanceDate == default ? DateTime.Today : command.RemittanceDate.Date;
        command.Invoices = command.Invoices
            .Where(item => item.InvoiceNumber > 0)
            .GroupBy(item => item.InvoiceNumber)
            .Select(group => group.First())
            .ToArray();

        if (command.Invoices.Count == 0)
        {
            throw new InvalidOperationException("Debes seleccionar al menos una factura para crear la remesa.");
        }
    }

    private static string BuildRemittanceSeries(Guid companyId) =>
        $"RM-{companyId.ToString("N")[..3].ToUpperInvariant()}";

    private void EnsureTenantWriteAccess()
    {
        if (!_currentUserContext.IsAuthenticated)
        {
            throw new InvalidOperationException("Debes iniciar sesión para modificar datos.");
        }

        if (_currentUserContext.IsPlatformAdmin ||
            _currentUserContext.Roles.Contains(PlatformRoles.TenantAdmin, StringComparer.OrdinalIgnoreCase))
        {
            return;
        }

        throw new InvalidOperationException("No tienes permisos de escritura en este tenant.");
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

        var allowedCompanies = await _companyAccessService.GetAllowedCompaniesAsync(
            _currentUserContext.UserId.Value,
            tenantId,
            cancellationToken);

        if (!allowedCompanies.Any(company => company.CompanyId == companyId))
        {
            throw new InvalidOperationException("No tienes acceso a la empresa activa.");
        }
    }

    private static string BuildSearchOrderByClause(SalesRemittanceFilter filter)
    {
        var column = filter.SortColumn switch
        {
            nameof(SalesRemittanceListItemDto.RemittanceNumber) => "sr.remittance_number",
            nameof(SalesRemittanceListItemDto.RemittanceDate) => "sr.remittance_date",
            nameof(SalesRemittanceListItemDto.DueDate) => "sr.due_date",
            nameof(SalesRemittanceListItemDto.Status) => "sr.status",
            nameof(SalesRemittanceListItemDto.BankName) => "sr.bank_name",
            nameof(SalesRemittanceListItemDto.InvoiceCount) => "sr.invoice_count",
            nameof(SalesRemittanceListItemDto.TotalAmount) => "sr.total_amount",
            nameof(SalesRemittanceListItemDto.OutstandingAmount) => "sr.outstanding_amount",
            _ => string.Empty
        };

        if (string.IsNullOrWhiteSpace(column))
        {
            return "ORDER BY sr.remittance_date DESC, sr.remittance_number DESC";
        }

        var direction = filter.SortDescending ? "DESC" : "ASC";
        return $"ORDER BY {column} {direction}, sr.remittance_number DESC";
    }

    private static object DbValue(string value) => string.IsNullOrWhiteSpace(value) ? DBNull.Value : value;
    private static object DbValue(DateTime? value) => value.HasValue ? value.Value : DBNull.Value;

    private sealed record InvoiceSnapshot(
        Guid InvoiceId,
        string InvoiceSeries,
        int InvoiceNumber,
        int ClientCode,
        string ClientName,
        DateTime IssueDate,
        DateTime? DueDate,
        decimal TotalAmount,
        decimal AmountPaid,
        decimal OutstandingAmount,
        string PaymentStatus,
        string Notes)
    {
        public string DisplayNumber => string.IsNullOrWhiteSpace(InvoiceSeries)
            ? InvoiceNumber.ToString()
            : $"{InvoiceSeries}/{InvoiceNumber:000000}";
    }

    private sealed record RemittanceTotals(
        int InvoiceCount,
        int ClientCount,
        decimal TotalAmount,
        decimal CollectedAmount,
        decimal OutstandingAmount);

    private sealed record RemittanceHeader(
        Guid RemittanceId,
        int RemittanceNumber,
        string Status,
        DateTime? SentUtc,
        DateTime? CollectedUtc);
}
