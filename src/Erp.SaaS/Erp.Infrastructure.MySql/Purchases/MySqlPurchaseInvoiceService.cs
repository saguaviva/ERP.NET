using Erp.Application.Auditing;
using Erp.Application.Companies;
using Erp.Application.Contexts;
using Erp.Application.Purchases;
using Erp.Domain.Common;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Support;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Purchases;

public sealed class MySqlPurchaseInvoiceService : IPurchaseInvoiceQueries, IPurchaseInvoiceService
{
    private readonly MySqlConnectionFactory _connectionFactory;
    private readonly IAuditLogService _auditLogService;
    private readonly ICompanyAccessService _companyAccessService;
    private readonly ICurrentUserContext _currentUserContext;
    private readonly ITenantContext _tenantContext;
    private readonly IActiveCompanyContext _activeCompanyContext;

    public MySqlPurchaseInvoiceService(
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

    public async Task<PurchaseInvoiceSearchResultDto> SearchInvoicesAsync(
        Guid tenantId,
        Guid companyId,
        PurchaseInvoiceFilter filter,
        CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return new PurchaseInvoiceSearchResultDto();
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);

        var pageSize = Math.Clamp(filter.PageSize, 10, 200);
        var page = Math.Max(filter.Page, 1);
        var offset = (page - 1) * pageSize;
        var search = filter.Search?.Trim() ?? string.Empty;
        var likeSearch = $"%{search}%";
        var status = filter.Status?.Trim() ?? string.Empty;

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var centerCode = await ResolveCompanyCenterCodeAsync(connection, tenantId, companyId, cancellationToken);

        await using (var countCommand = connection.CreateCommand())
        {
            countCommand.CommandText =
                """
                SELECT COUNT(*)
                FROM purchase_invoices pi
                LEFT JOIN prove p
                  ON p.CODI = pi.supplier_code
                 AND p.CENTRO = @centerCode
                WHERE pi.tenant_id = @tenantId
                  AND pi.company_id = @companyId
                  AND COALESCE(pi.is_deleted, 0) = 0
                  AND (
                        @includeClosed = 1
                        OR pi.status NOT IN ('Paid', 'Cancelled')
                      )
                  AND (
                        @status = ''
                        OR pi.status = @status
                      )
                  AND (
                        @search = ''
                        OR CAST(pi.invoice_number AS CHAR) LIKE @likeSearch
                        OR COALESCE(NULLIF(pi.supplier_name, ''), p.NOM, '') LIKE @likeSearch
                        OR COALESCE(pi.supplier_document_number, '') LIKE @likeSearch
                        OR COALESCE(pi.notes, '') LIKE @likeSearch
                      );
                """;
            countCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            countCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
            countCommand.Parameters.AddWithValue("@centerCode", centerCode);
            countCommand.Parameters.AddWithValue("@includeClosed", filter.IncludeClosed);
            countCommand.Parameters.AddWithValue("@status", status);
            countCommand.Parameters.AddWithValue("@search", search);
            countCommand.Parameters.AddWithValue("@likeSearch", likeSearch);

            var totalCount = Convert.ToInt32(await countCommand.ExecuteScalarAsync(cancellationToken));
            if (totalCount == 0)
            {
                return new PurchaseInvoiceSearchResultDto();
            }

            await using var command = connection.CreateCommand();
            command.CommandText =
                $"""
                SELECT pi.invoice_series,
                       pi.invoice_number,
                       pi.supplier_code,
                       COALESCE(NULLIF(pi.supplier_name, ''), p.NOM, '') AS supplier_name,
                       COALESCE(pi.supplier_document_number, '') AS supplier_document_number,
                       pi.document_date,
                       pi.due_date,
                       pi.status,
                       pi.total_net_amount,
                       pi.total_tax_amount,
                       pi.total_amount,
                       pi.amount_paid,
                       pi.outstanding_amount,
                       pi.last_payment_utc,
                       pi.origin,
                       COALESCE(invoice_lines.line_count, 0) AS line_count,
                       COALESCE(invoice_receipts.receipt_count, 0) AS receipt_count,
                       COALESCE(invoice_receipts.order_count, 0) AS order_count
                FROM purchase_invoices pi
                LEFT JOIN prove p
                  ON p.CODI = pi.supplier_code
                 AND p.CENTRO = @centerCode
                LEFT JOIN (
                    SELECT invoice_id, COUNT(*) AS line_count
                    FROM purchase_invoice_lines
                    GROUP BY invoice_id
                ) invoice_lines
                  ON invoice_lines.invoice_id = pi.invoice_id
                LEFT JOIN (
                    SELECT invoice_id,
                           COUNT(*) AS receipt_count,
                           COUNT(DISTINCT order_number) AS order_count
                    FROM purchase_invoice_receipts
                    GROUP BY invoice_id
                ) invoice_receipts
                  ON invoice_receipts.invoice_id = pi.invoice_id
                WHERE pi.tenant_id = @tenantId
                  AND pi.company_id = @companyId
                  AND COALESCE(pi.is_deleted, 0) = 0
                  AND (
                        @includeClosed = 1
                        OR pi.status NOT IN ('Paid', 'Cancelled')
                      )
                  AND (
                        @status = ''
                        OR pi.status = @status
                      )
                  AND (
                        @search = ''
                        OR CAST(pi.invoice_number AS CHAR) LIKE @likeSearch
                        OR COALESCE(NULLIF(pi.supplier_name, ''), p.NOM, '') LIKE @likeSearch
                        OR COALESCE(pi.supplier_document_number, '') LIKE @likeSearch
                        OR COALESCE(pi.notes, '') LIKE @likeSearch
                      )
                {BuildPurchaseInvoiceSearchOrderByClause(filter)}
                LIMIT @limit OFFSET @offset;
                """;
            command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            command.Parameters.AddWithValue("@companyId", companyId.ToString());
            command.Parameters.AddWithValue("@centerCode", centerCode);
            command.Parameters.AddWithValue("@includeClosed", filter.IncludeClosed);
            command.Parameters.AddWithValue("@status", status);
            command.Parameters.AddWithValue("@search", search);
            command.Parameters.AddWithValue("@likeSearch", likeSearch);
            command.Parameters.AddWithValue("@limit", pageSize);
            command.Parameters.AddWithValue("@offset", offset);

            var items = new List<PurchaseInvoiceListItemDto>();
            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                items.Add(new PurchaseInvoiceListItemDto
                {
                    InvoiceSeries = reader.GetStringOrEmpty("invoice_series"),
                    InvoiceNumber = reader.GetInt32(reader.GetOrdinal("invoice_number")),
                    SupplierCode = reader.GetInt32(reader.GetOrdinal("supplier_code")),
                    SupplierName = reader.GetStringOrEmpty("supplier_name"),
                    SupplierDocumentNumber = reader.GetStringOrEmpty("supplier_document_number"),
                    DocumentDate = reader.GetDateTime(reader.GetOrdinal("document_date")),
                    DueDate = reader.IsDBNull(reader.GetOrdinal("due_date")) ? null : reader.GetDateTime(reader.GetOrdinal("due_date")),
                    Status = reader.GetStringOrEmpty("status"),
                    TotalNetAmount = reader.GetDecimal(reader.GetOrdinal("total_net_amount")),
                    TotalTaxAmount = reader.GetDecimal(reader.GetOrdinal("total_tax_amount")),
                    TotalAmount = reader.GetDecimal(reader.GetOrdinal("total_amount")),
                    AmountPaid = reader.GetDecimal(reader.GetOrdinal("amount_paid")),
                    OutstandingAmount = reader.GetDecimal(reader.GetOrdinal("outstanding_amount")),
                    LastPaymentUtc = reader.IsDBNull(reader.GetOrdinal("last_payment_utc")) ? null : reader.GetDateTime(reader.GetOrdinal("last_payment_utc")),
                    Origin = reader.GetStringOrEmpty("origin"),
                    LineCount = reader.GetInt32(reader.GetOrdinal("line_count")),
                    ReceiptCount = reader.GetInt32(reader.GetOrdinal("receipt_count")),
                    OrderCount = reader.GetInt32(reader.GetOrdinal("order_count"))
                });
            }

            await reader.DisposeAsync();
            await ApplyReconciliationSummaryAsync(connection, tenantId, companyId, items, cancellationToken);

            return new PurchaseInvoiceSearchResultDto
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }

    public async Task<PurchaseInvoiceDetailDto?> GetInvoiceByNumberAsync(
        Guid tenantId,
        Guid companyId,
        int invoiceNumber,
        CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return null;
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var centerCode = await ResolveCompanyCenterCodeAsync(connection, tenantId, companyId, cancellationToken);

        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT pi.invoice_id,
                   pi.invoice_series,
                   pi.invoice_number,
                   t.name AS tenant_name,
                   c.name AS company_name,
                   c.legacy_center_code,
                   pi.supplier_code,
                   COALESCE(NULLIF(pi.supplier_name, ''), p.NOM, '') AS supplier_name,
                   COALESCE(NULLIF(pi.supplier_tax_id, ''), p.NIF, '') AS supplier_tax_id,
                   COALESCE(p.ADRE, '') AS supplier_address,
                   COALESCE(p.CP, '') AS supplier_postal_code,
                   COALESCE(p.POBLACIO, '') AS supplier_city,
                   COALESCE(p.PROVINCIA, '') AS supplier_province,
                   COALESCE(p.PAIS, '') AS supplier_country,
                   COALESCE(pi.supplier_document_number, '') AS supplier_document_number,
                   pi.document_date,
                   pi.due_date,
                   pi.status,
                   pi.total_net_amount,
                   pi.total_tax_amount,
                   pi.total_amount,
                   pi.amount_paid,
                   pi.outstanding_amount,
                   pi.last_payment_utc,
                   pi.notes,
                   pi.origin
            FROM purchase_invoices pi
            LEFT JOIN tenants t
              ON t.id = pi.tenant_id
            LEFT JOIN companies c
              ON c.id = pi.company_id
             AND c.tenant_id = pi.tenant_id
            LEFT JOIN prove p
              ON p.CODI = pi.supplier_code
             AND p.CENTRO = @centerCode
            WHERE pi.tenant_id = @tenantId
              AND pi.company_id = @companyId
              AND pi.invoice_number = @invoiceNumber
              AND COALESCE(pi.is_deleted, 0) = 0
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@invoiceNumber", invoiceNumber);
        command.Parameters.AddWithValue("@centerCode", centerCode);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        var detail = new PurchaseInvoiceDetailDto
        {
            InvoiceId = reader.GetGuid("invoice_id"),
            InvoiceSeries = reader.GetStringOrEmpty("invoice_series"),
            InvoiceNumber = reader.GetInt32(reader.GetOrdinal("invoice_number")),
            TenantName = reader.GetStringOrEmpty("tenant_name"),
            CompanyName = reader.GetStringOrEmpty("company_name"),
            CompanyLegacyCenterCode = reader.GetStringOrEmpty("legacy_center_code"),
            SupplierCode = reader.GetInt32(reader.GetOrdinal("supplier_code")),
            SupplierName = reader.GetStringOrEmpty("supplier_name"),
            SupplierTaxId = reader.GetStringOrEmpty("supplier_tax_id"),
            SupplierAddress = reader.GetStringOrEmpty("supplier_address"),
            SupplierPostalCode = reader.GetStringOrEmpty("supplier_postal_code"),
            SupplierCity = reader.GetStringOrEmpty("supplier_city"),
            SupplierProvince = reader.GetStringOrEmpty("supplier_province"),
            SupplierCountry = reader.GetStringOrEmpty("supplier_country"),
            SupplierDocumentNumber = reader.GetStringOrEmpty("supplier_document_number"),
            DocumentDate = reader.GetDateTime(reader.GetOrdinal("document_date")),
            DueDate = reader.IsDBNull(reader.GetOrdinal("due_date")) ? null : reader.GetDateTime(reader.GetOrdinal("due_date")),
            Status = reader.GetStringOrEmpty("status"),
            TotalNetAmount = reader.GetDecimal(reader.GetOrdinal("total_net_amount")),
            TotalTaxAmount = reader.GetDecimal(reader.GetOrdinal("total_tax_amount")),
            TotalAmount = reader.GetDecimal(reader.GetOrdinal("total_amount")),
            AmountPaid = reader.GetDecimal(reader.GetOrdinal("amount_paid")),
            OutstandingAmount = reader.GetDecimal(reader.GetOrdinal("outstanding_amount")),
            LastPaymentUtc = reader.IsDBNull(reader.GetOrdinal("last_payment_utc")) ? null : reader.GetDateTime(reader.GetOrdinal("last_payment_utc")),
            Notes = reader.GetStringOrEmpty("notes"),
            Origin = reader.GetStringOrEmpty("origin")
        };
        await reader.DisposeAsync();

        detail.Lines = await LoadInvoiceLinesAsync(connection, detail.InvoiceId, cancellationToken);
        detail.Receipts = await LoadInvoiceReceiptsAsync(connection, detail.InvoiceId, cancellationToken);
        detail.Payments = await LoadInvoicePaymentsAsync(connection, tenantId, companyId, detail.InvoiceId, cancellationToken);
        return detail;
    }

    public async Task<IReadOnlyCollection<PurchaseInvoiceListItemDto>> GetInvoicesByReceiptAsync(
        Guid tenantId,
        Guid companyId,
        int receiptNumber,
        CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return [];
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var items = new List<PurchaseInvoiceListItemDto>();

        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT pi.invoice_series,
                   pi.invoice_number,
                   pi.supplier_code,
                   pi.supplier_name,
                   pi.supplier_document_number,
                   pi.document_date,
                   pi.due_date,
                   pi.status,
                   pi.total_net_amount,
                   pi.total_tax_amount,
                   pi.total_amount,
                   pi.amount_paid,
                   pi.outstanding_amount,
                   pi.last_payment_utc,
                   pi.origin,
                   COALESCE(invoice_lines.line_count, 0) AS line_count
            FROM purchase_invoice_receipts pir
            INNER JOIN purchase_invoices pi
              ON pi.invoice_id = pir.invoice_id
            LEFT JOIN (
                SELECT invoice_id, COUNT(*) AS line_count
                FROM purchase_invoice_lines
                GROUP BY invoice_id
            ) invoice_lines
              ON invoice_lines.invoice_id = pi.invoice_id
            WHERE pi.tenant_id = @tenantId
              AND pi.company_id = @companyId
              AND COALESCE(pi.is_deleted, 0) = 0
              AND pir.receipt_number = @receiptNumber
            ORDER BY pi.document_date DESC, pi.invoice_number DESC;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@receiptNumber", receiptNumber);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new PurchaseInvoiceListItemDto
            {
                InvoiceSeries = reader.GetStringOrEmpty("invoice_series"),
                InvoiceNumber = reader.GetInt32(reader.GetOrdinal("invoice_number")),
                SupplierCode = reader.GetInt32(reader.GetOrdinal("supplier_code")),
                SupplierName = reader.GetStringOrEmpty("supplier_name"),
                SupplierDocumentNumber = reader.GetStringOrEmpty("supplier_document_number"),
                DocumentDate = reader.GetDateTime(reader.GetOrdinal("document_date")),
                DueDate = reader.IsDBNull(reader.GetOrdinal("due_date")) ? null : reader.GetDateTime(reader.GetOrdinal("due_date")),
                Status = reader.GetStringOrEmpty("status"),
                TotalNetAmount = reader.GetDecimal(reader.GetOrdinal("total_net_amount")),
                TotalTaxAmount = reader.GetDecimal(reader.GetOrdinal("total_tax_amount")),
                TotalAmount = reader.GetDecimal(reader.GetOrdinal("total_amount")),
                AmountPaid = reader.GetDecimal(reader.GetOrdinal("amount_paid")),
                OutstandingAmount = reader.GetDecimal(reader.GetOrdinal("outstanding_amount")),
                LastPaymentUtc = reader.IsDBNull(reader.GetOrdinal("last_payment_utc")) ? null : reader.GetDateTime(reader.GetOrdinal("last_payment_utc")),
                Origin = reader.GetStringOrEmpty("origin"),
                LineCount = reader.GetInt32(reader.GetOrdinal("line_count")),
                ReceiptCount = 1
            });
        }

        return items;
    }

    public async Task<int> SaveInvoiceAsync(SavePurchaseInvoiceCommand command, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return 0;
        }

        EnsureTenantWriteAccess();
        await EnsureCompanyAccessAsync(command.TenantId, command.CompanyId, cancellationToken);
        NormalizeAndValidateInvoice(command);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var centerCode = await ResolveCompanyCenterCodeAsync(connection, command.TenantId, command.CompanyId, cancellationToken);
        var supplier = await ResolveSupplierSnapshotAsync(connection, centerCode, command.SupplierCode, cancellationToken);

        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        var previous = command.InvoiceNumber.HasValue
            ? await LoadHeaderSnapshotAsync(connection, transaction, command.TenantId, command.CompanyId, command.InvoiceNumber.Value, cancellationToken)
            : null;

        if (command.InvoiceNumber.HasValue && previous is null)
        {
            throw new InvalidOperationException("La factura de proveedor indicada no existe.");
        }

        var invoiceId = previous?.InvoiceId ?? Guid.NewGuid();
        var invoiceNumber = previous?.InvoiceNumber ?? await GetNextInvoiceNumberAsync(connection, transaction, command.TenantId, command.CompanyId, cancellationToken);
        var now = DateTime.UtcNow;
        var totals = CalculateTotals(command.Lines);
        var resolvedReceipts = await ResolveReceiptLinksAsync(connection, transaction, command.TenantId, command.CompanyId, command.Receipts, cancellationToken);
        var amountPaid = decimal.Round(previous?.AmountPaid ?? 0m, 2, MidpointRounding.AwayFromZero);
        var outstandingAmount = decimal.Round(Math.Max(0m, totals.TotalAmount - amountPaid), 2, MidpointRounding.AwayFromZero);
        var effectiveStatus = DetermineEffectiveInvoiceStatus(command.Status, totals.TotalAmount, outstandingAmount);

        await using (var headerCommand = connection.CreateCommand())
        {
            headerCommand.Transaction = transaction;
            headerCommand.CommandText = previous is null
                ? """
                  INSERT INTO purchase_invoices (
                      invoice_id,
                      invoice_series,
                      invoice_number,
                      tenant_id,
                      company_id,
                      supplier_code,
                      supplier_name,
                      supplier_tax_id,
                      supplier_document_number,
                      document_date,
                      due_date,
                      status,
                      total_net_amount,
                      total_tax_amount,
                      total_amount,
                      amount_paid,
                      outstanding_amount,
                      last_payment_utc,
                      origin,
                      notes,
                      created_utc,
                      updated_utc)
                  VALUES (
                      @invoiceId,
                      @invoiceSeries,
                      @invoiceNumber,
                      @tenantId,
                      @companyId,
                      @supplierCode,
                      @supplierName,
                      @supplierTaxId,
                      @supplierDocumentNumber,
                      @documentDate,
                      @dueDate,
                      @status,
                      @totalNetAmount,
                      @totalTaxAmount,
                      @totalAmount,
                      @amountPaid,
                      @outstandingAmount,
                      @lastPaymentUtc,
                      'local',
                      @notes,
                      @createdUtc,
                      @updatedUtc);
                  """
                : """
                  UPDATE purchase_invoices
                  SET supplier_code = @supplierCode,
                      supplier_name = @supplierName,
                      supplier_tax_id = @supplierTaxId,
                      supplier_document_number = @supplierDocumentNumber,
                      document_date = @documentDate,
                      due_date = @dueDate,
                      status = @status,
                      total_net_amount = @totalNetAmount,
                      total_tax_amount = @totalTaxAmount,
                      total_amount = @totalAmount,
                      amount_paid = @amountPaid,
                      outstanding_amount = @outstandingAmount,
                      last_payment_utc = @lastPaymentUtc,
                      notes = @notes,
                      origin = 'local',
                      updated_utc = @updatedUtc
                  WHERE invoice_id = @invoiceId;
                  """;

            headerCommand.Parameters.AddWithValue("@invoiceId", invoiceId.ToString());
            headerCommand.Parameters.AddWithValue("@invoiceSeries", BuildInvoiceSeries(centerCode));
            headerCommand.Parameters.AddWithValue("@invoiceNumber", invoiceNumber);
            headerCommand.Parameters.AddWithValue("@tenantId", command.TenantId.ToString());
            headerCommand.Parameters.AddWithValue("@companyId", command.CompanyId.ToString());
            headerCommand.Parameters.AddWithValue("@supplierCode", command.SupplierCode);
            headerCommand.Parameters.AddWithValue("@supplierName", supplier.Name);
            headerCommand.Parameters.AddWithValue("@supplierTaxId", DbValue(string.IsNullOrWhiteSpace(command.SupplierTaxId) ? supplier.TaxId : command.SupplierTaxId));
            headerCommand.Parameters.AddWithValue("@supplierDocumentNumber", DbValue(command.SupplierDocumentNumber));
            headerCommand.Parameters.AddWithValue("@documentDate", command.DocumentDate);
            headerCommand.Parameters.AddWithValue("@dueDate", DbValue(command.DueDate));
            headerCommand.Parameters.AddWithValue("@status", effectiveStatus);
            headerCommand.Parameters.AddWithValue("@totalNetAmount", totals.TotalNet);
            headerCommand.Parameters.AddWithValue("@totalTaxAmount", totals.TotalTax);
            headerCommand.Parameters.AddWithValue("@totalAmount", totals.TotalAmount);
            headerCommand.Parameters.AddWithValue("@amountPaid", amountPaid);
            headerCommand.Parameters.AddWithValue("@outstandingAmount", outstandingAmount);
            headerCommand.Parameters.AddWithValue("@lastPaymentUtc", DbValue(previous?.LastPaymentUtc));
            headerCommand.Parameters.AddWithValue("@notes", DbValue(command.Notes));
            headerCommand.Parameters.AddWithValue("@createdUtc", now);
            headerCommand.Parameters.AddWithValue("@updatedUtc", now);
            await headerCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        await ReplaceInvoiceLinesAsync(connection, transaction, invoiceId, command.Lines, cancellationToken);
        await ReplaceInvoiceReceiptsAsync(connection, transaction, invoiceId, resolvedReceipts, cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = command.TenantId,
            CompanyId = command.CompanyId,
            UserId = _currentUserContext.UserId,
            Action = previous is null ? "PurchaseInvoiceCreated" : "PurchaseInvoiceUpdated",
            EntityName = "PurchaseInvoice",
            EntityId = invoiceNumber.ToString(),
            Details = $"{supplier.Name} · {effectiveStatus} · Total={totals.TotalAmount:0.00} € · Pagado={amountPaid:0.00} € · Pendiente={outstandingAmount:0.00} €"
        }, cancellationToken);

        return invoiceNumber;
    }

    public async Task RegisterPaymentAsync(RegisterPurchaseInvoicePaymentCommand command, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return;
        }

        EnsureTenantWriteAccess();
        await EnsureCompanyAccessAsync(command.TenantId, command.CompanyId, cancellationToken);
        NormalizeAndValidateInvoicePayment(command);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        var invoiceHeader = await LoadInvoicePaymentHeaderAsync(connection, transaction, command.TenantId, command.CompanyId, command.InvoiceNumber, cancellationToken)
            ?? throw new InvalidOperationException("No se ha encontrado la factura indicada.");

        if (string.Equals(invoiceHeader.Status, PurchaseInvoiceStatuses.Cancelled, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("No puedes registrar pagos sobre una factura anulada.");
        }

        var outstandingAmount = decimal.Round(invoiceHeader.OutstandingAmount, 2, MidpointRounding.AwayFromZero);
        if (outstandingAmount <= 0m)
        {
            throw new InvalidOperationException("La factura ya no tiene pendiente de pago.");
        }

        if (command.Amount > outstandingAmount)
        {
            throw new InvalidOperationException($"El pago supera el pendiente actual de la factura ({outstandingAmount:0.00} €).");
        }

        var paymentNumber = await GetNextInvoicePaymentNumberAsync(connection, transaction, invoiceHeader.InvoiceId, command.TenantId, command.CompanyId, cancellationToken);
        var paymentId = Guid.NewGuid();
        var nowUtc = DateTime.UtcNow;

        await using (var insertPaymentCommand = connection.CreateCommand())
        {
            insertPaymentCommand.Transaction = transaction;
            insertPaymentCommand.CommandText =
                """
                INSERT INTO purchase_invoice_payments (
                    payment_id,
                    invoice_id,
                    tenant_id,
                    company_id,
                    payment_number,
                    payment_date,
                    amount,
                    method,
                    reference,
                    notes,
                    created_utc)
                VALUES (
                    @paymentId,
                    @invoiceId,
                    @tenantId,
                    @companyId,
                    @paymentNumber,
                    @paymentDate,
                    @amount,
                    @method,
                    @reference,
                    @notes,
                    @createdUtc);
                """;
            insertPaymentCommand.Parameters.AddWithValue("@paymentId", paymentId.ToString());
            insertPaymentCommand.Parameters.AddWithValue("@invoiceId", invoiceHeader.InvoiceId.ToString());
            insertPaymentCommand.Parameters.AddWithValue("@tenantId", command.TenantId.ToString());
            insertPaymentCommand.Parameters.AddWithValue("@companyId", command.CompanyId.ToString());
            insertPaymentCommand.Parameters.AddWithValue("@paymentNumber", paymentNumber);
            insertPaymentCommand.Parameters.AddWithValue("@paymentDate", command.PaymentDate.Date);
            insertPaymentCommand.Parameters.AddWithValue("@amount", command.Amount);
            insertPaymentCommand.Parameters.AddWithValue("@method", DbValue(command.Method));
            insertPaymentCommand.Parameters.AddWithValue("@reference", DbValue(command.Reference));
            insertPaymentCommand.Parameters.AddWithValue("@notes", DbValue(command.Notes));
            insertPaymentCommand.Parameters.AddWithValue("@createdUtc", nowUtc);
            await insertPaymentCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        var amountPaid = decimal.Round(invoiceHeader.AmountPaid + command.Amount, 2, MidpointRounding.AwayFromZero);
        var remainingAmount = decimal.Round(Math.Max(0m, invoiceHeader.TotalAmount - amountPaid), 2, MidpointRounding.AwayFromZero);
        var status = remainingAmount <= 0m && invoiceHeader.TotalAmount > 0m
            ? PurchaseInvoiceStatuses.Paid
            : PurchaseInvoiceStatuses.Registered;

        await using (var updateInvoiceCommand = connection.CreateCommand())
        {
            updateInvoiceCommand.Transaction = transaction;
            updateInvoiceCommand.CommandText =
                """
                UPDATE purchase_invoices
                SET status = @status,
                    amount_paid = @amountPaid,
                    outstanding_amount = @outstandingAmount,
                    last_payment_utc = @lastPaymentUtc,
                    origin = 'local',
                    is_deleted = 0,
                    synced_utc = NULL,
                    updated_utc = @updatedUtc
                WHERE invoice_id = @invoiceId
                  AND tenant_id = @tenantId
                  AND company_id = @companyId;
                """;
            updateInvoiceCommand.Parameters.AddWithValue("@status", status);
            updateInvoiceCommand.Parameters.AddWithValue("@amountPaid", amountPaid);
            updateInvoiceCommand.Parameters.AddWithValue("@outstandingAmount", remainingAmount);
            updateInvoiceCommand.Parameters.AddWithValue("@lastPaymentUtc", nowUtc);
            updateInvoiceCommand.Parameters.AddWithValue("@updatedUtc", nowUtc);
            updateInvoiceCommand.Parameters.AddWithValue("@invoiceId", invoiceHeader.InvoiceId.ToString());
            updateInvoiceCommand.Parameters.AddWithValue("@tenantId", command.TenantId.ToString());
            updateInvoiceCommand.Parameters.AddWithValue("@companyId", command.CompanyId.ToString());
            await updateInvoiceCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        await transaction.CommitAsync(cancellationToken);

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = command.TenantId,
            CompanyId = command.CompanyId,
            UserId = _currentUserContext.UserId,
            Action = "PurchaseInvoicePaymentRegistered",
            EntityName = "PurchaseInvoice",
            EntityId = command.InvoiceNumber.ToString(),
            Details = $"Factura={invoiceHeader.InvoiceDisplayNumber}; Pago={paymentNumber}; Fecha={command.PaymentDate:yyyy-MM-dd}; Importe={command.Amount:0.00}; Estado={status}; Pendiente={remainingAmount:0.00}{(string.IsNullOrWhiteSpace(command.Method) ? string.Empty : $"; Metodo={command.Method}")}{(string.IsNullOrWhiteSpace(command.Reference) ? string.Empty : $"; Referencia={command.Reference}")}"
        }, cancellationToken);
    }

    public async Task DeleteInvoiceAsync(Guid tenantId, Guid companyId, int invoiceNumber, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return;
        }

        EnsureTenantWriteAccess();
        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        var previous = await LoadHeaderSnapshotAsync(connection, transaction, tenantId, companyId, invoiceNumber, cancellationToken);
        if (previous is null)
        {
            throw new InvalidOperationException("La factura de proveedor indicada no existe.");
        }

        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            UPDATE purchase_invoices
            SET is_deleted = 1,
                origin = 'local',
                updated_utc = @updatedUtc
            WHERE invoice_id = @invoiceId;
            """;
        command.Parameters.AddWithValue("@invoiceId", previous.InvoiceId.ToString());
        command.Parameters.AddWithValue("@updatedUtc", DateTime.UtcNow);
        await command.ExecuteNonQueryAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = tenantId,
            CompanyId = companyId,
            UserId = _currentUserContext.UserId,
            Action = "PurchaseInvoiceDeleted",
            EntityName = "PurchaseInvoice",
            EntityId = invoiceNumber.ToString(),
            Details = previous.SupplierName
        }, cancellationToken);
    }

    private static (decimal TotalNet, decimal TotalTax, decimal TotalAmount) CalculateTotals(IEnumerable<SavePurchaseInvoiceLineInputDto> lines)
    {
        var totalNet = decimal.Round(lines.Sum(line => decimal.Round(line.Quantity * line.UnitPrice, 2, MidpointRounding.AwayFromZero)), 2, MidpointRounding.AwayFromZero);
        var totalTax = decimal.Round(lines.Sum(line =>
        {
            var subtotal = decimal.Round(line.Quantity * line.UnitPrice, 2, MidpointRounding.AwayFromZero);
            return decimal.Round(subtotal * (line.TaxRate / 100m), 2, MidpointRounding.AwayFromZero);
        }), 2, MidpointRounding.AwayFromZero);

        return (totalNet, totalTax, totalNet + totalTax);
    }

    private async Task<IReadOnlyCollection<PurchaseInvoiceLineDto>> LoadInvoiceLinesAsync(
        MySqlConnection connection,
        Guid invoiceId,
        CancellationToken cancellationToken)
    {
        var items = new List<PurchaseInvoiceLineDto>();
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT line_number,
                   item_code,
                   description,
                   quantity,
                   unit_of_measure,
                   unit_price,
                   tax_rate,
                   source_order_number,
                   source_order_line_number,
                   source_receipt_number
            FROM purchase_invoice_lines
            WHERE invoice_id = @invoiceId
            ORDER BY line_number;
            """;
        command.Parameters.AddWithValue("@invoiceId", invoiceId.ToString());

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new PurchaseInvoiceLineDto
            {
                LineNumber = reader.GetInt32(reader.GetOrdinal("line_number")),
                ItemCode = reader.GetStringOrEmpty("item_code"),
                Description = reader.GetStringOrEmpty("description"),
                Quantity = reader.GetDecimal(reader.GetOrdinal("quantity")),
                UnitOfMeasure = reader.GetStringOrEmpty("unit_of_measure"),
                UnitPrice = reader.GetDecimal(reader.GetOrdinal("unit_price")),
                TaxRate = reader.GetDecimal(reader.GetOrdinal("tax_rate")),
                SourceOrderNumber = reader.IsDBNull(reader.GetOrdinal("source_order_number")) ? null : reader.GetInt32(reader.GetOrdinal("source_order_number")),
                SourceOrderLineNumber = reader.IsDBNull(reader.GetOrdinal("source_order_line_number")) ? null : reader.GetInt32(reader.GetOrdinal("source_order_line_number")),
                SourceReceiptNumber = reader.IsDBNull(reader.GetOrdinal("source_receipt_number")) ? null : reader.GetInt32(reader.GetOrdinal("source_receipt_number"))
            });
        }

        return items;
    }

    private async Task<IReadOnlyCollection<PurchaseInvoiceReceiptLinkDto>> LoadInvoiceReceiptsAsync(
        MySqlConnection connection,
        Guid invoiceId,
        CancellationToken cancellationToken)
    {
        var items = new List<PurchaseInvoiceReceiptLinkDto>();
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT receipt_id,
                   receipt_series,
                   receipt_number,
                   order_number,
                   receipt_date,
                   total_received_quantity
            FROM purchase_invoice_receipts
            WHERE invoice_id = @invoiceId
            ORDER BY receipt_date DESC, receipt_number DESC;
            """;
        command.Parameters.AddWithValue("@invoiceId", invoiceId.ToString());

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new PurchaseInvoiceReceiptLinkDto
            {
                ReceiptId = reader.IsDBNull(reader.GetOrdinal("receipt_id")) ? null : reader.GetGuid("receipt_id"),
                ReceiptSeries = reader.GetStringOrEmpty("receipt_series"),
                ReceiptNumber = reader.GetInt32(reader.GetOrdinal("receipt_number")),
                OrderNumber = reader.GetInt32(reader.GetOrdinal("order_number")),
                ReceiptDate = reader.GetDateTime(reader.GetOrdinal("receipt_date")),
                TotalReceivedQuantity = reader.GetDecimal(reader.GetOrdinal("total_received_quantity"))
            });
        }

        return items;
    }

    private async Task<IReadOnlyCollection<PurchaseInvoicePaymentDto>> LoadInvoicePaymentsAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        Guid invoiceId,
        CancellationToken cancellationToken)
    {
        var items = new List<PurchaseInvoicePaymentDto>();
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT payment_id,
                   payment_number,
                   payment_date,
                   amount,
                   method,
                   reference,
                   notes,
                   created_utc
            FROM purchase_invoice_payments
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND invoice_id = @invoiceId
            ORDER BY payment_number;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@invoiceId", invoiceId.ToString());

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new PurchaseInvoicePaymentDto
            {
                PaymentId = reader.GetGuid("payment_id"),
                PaymentNumber = reader.GetInt32(reader.GetOrdinal("payment_number")),
                PaymentDate = reader.GetDateTime(reader.GetOrdinal("payment_date")),
                Amount = reader.GetDecimal(reader.GetOrdinal("amount")),
                Method = reader.GetStringOrEmpty("method"),
                Reference = reader.GetStringOrEmpty("reference"),
                Notes = reader.GetStringOrEmpty("notes"),
                CreatedUtc = reader.GetDateTime(reader.GetOrdinal("created_utc"))
            });
        }

        return items;
    }

    private static async Task ApplyReconciliationSummaryAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        IReadOnlyCollection<PurchaseInvoiceListItemDto> items,
        CancellationToken cancellationToken)
    {
        if (items.Count == 0)
        {
            return;
        }

        var invoiceNumbers = items
            .Select(item => item.InvoiceNumber)
            .Distinct()
            .OrderBy(number => number)
            .ToArray();
        var inClause = string.Join(", ", invoiceNumbers);

        await using var command = connection.CreateCommand();
        command.CommandText =
            $"""
            SELECT pi.invoice_number,
                   SUM(
                       CASE
                           WHEN ABS(COALESCE(receipt_totals.received_quantity, 0) - pil.quantity) > 0.0005
                                OR ABS(COALESCE(pol.unit_price, 0) - pil.unit_price) > 0.00005
                           THEN 1
                           ELSE 0
                       END
                   ) AS difference_count,
                   SUM(
                       CASE
                           WHEN ABS(COALESCE(receipt_totals.received_quantity, 0) - pil.quantity) > 0.0005
                                OR ABS(COALESCE(pol.unit_price, 0) - pil.unit_price) > 0.00005
                           THEN ABS(ROUND((pil.quantity * pil.unit_price) - (COALESCE(receipt_totals.received_quantity, 0) * COALESCE(pol.unit_price, 0)), 2))
                           ELSE 0
                       END
                   ) AS difference_amount
            FROM purchase_invoices pi
            INNER JOIN purchase_invoice_lines pil
              ON pil.invoice_id = pi.invoice_id
            LEFT JOIN purchase_order_lines pol
              ON pol.tenant_id = pi.tenant_id
             AND pol.company_id = pi.company_id
             AND pol.order_number = pil.source_order_number
             AND pol.line_number = pil.source_order_line_number
            LEFT JOIN (
                SELECT pir.invoice_id,
                       pir.order_number,
                       prl.line_number,
                       SUM(prl.received_quantity) AS received_quantity
                FROM purchase_invoice_receipts pir
                INNER JOIN purchase_order_receipts pr
                  ON pr.tenant_id = pir.tenant_id
                 AND pr.company_id = pir.company_id
                 AND pr.receipt_number = pir.receipt_number
                 AND COALESCE(pr.is_deleted, 0) = 0
                INNER JOIN purchase_order_receipt_lines prl
                  ON prl.receipt_id = pr.receipt_id
                 AND prl.tenant_id = pir.tenant_id
                 AND prl.company_id = pir.company_id
                 AND prl.order_number = pir.order_number
                GROUP BY pir.invoice_id, pir.order_number, prl.line_number
            ) receipt_totals
              ON receipt_totals.invoice_id = pi.invoice_id
             AND receipt_totals.order_number = pil.source_order_number
             AND receipt_totals.line_number = pil.source_order_line_number
            WHERE pi.tenant_id = @tenantId
              AND pi.company_id = @companyId
              AND pi.invoice_number IN ({inClause})
            GROUP BY pi.invoice_number;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());

        var summaries = new Dictionary<int, (int DifferenceCount, decimal DifferenceAmount)>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            summaries[reader.GetInt32(reader.GetOrdinal("invoice_number"))] = (
                reader.IsDBNull(reader.GetOrdinal("difference_count")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("difference_count"))),
                reader.IsDBNull(reader.GetOrdinal("difference_amount")) ? 0m : reader.GetDecimal(reader.GetOrdinal("difference_amount")));
        }

        foreach (var item in items)
        {
            if (summaries.TryGetValue(item.InvoiceNumber, out var summary))
            {
                item.ReconciliationDifferenceCount = summary.DifferenceCount;
                item.ReconciliationDifferenceAmount = decimal.Round(summary.DifferenceAmount, 2, MidpointRounding.AwayFromZero);
                item.IsReconciled = summary.DifferenceCount == 0;
            }
            else
            {
                item.ReconciliationDifferenceCount = 0;
                item.ReconciliationDifferenceAmount = 0m;
                item.IsReconciled = true;
            }
        }
    }

    private async Task ReplaceInvoiceLinesAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid invoiceId,
        IReadOnlyCollection<SavePurchaseInvoiceLineInputDto> lines,
        CancellationToken cancellationToken)
    {
        await using (var deleteCommand = connection.CreateCommand())
        {
            deleteCommand.Transaction = transaction;
            deleteCommand.CommandText = "DELETE FROM purchase_invoice_lines WHERE invoice_id = @invoiceId;";
            deleteCommand.Parameters.AddWithValue("@invoiceId", invoiceId.ToString());
            await deleteCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        foreach (var line in lines)
        {
            await using var insertCommand = connection.CreateCommand();
            insertCommand.Transaction = transaction;
            insertCommand.CommandText =
                """
                INSERT INTO purchase_invoice_lines (
                    invoice_id,
                    line_number,
                    item_code,
                    description,
                    quantity,
                    unit_of_measure,
                    unit_price,
                    tax_rate,
                    source_order_number,
                    source_order_line_number,
                    source_receipt_number)
                VALUES (
                    @invoiceId,
                    @lineNumber,
                    @itemCode,
                    @description,
                    @quantity,
                    @unitOfMeasure,
                    @unitPrice,
                    @taxRate,
                    @sourceOrderNumber,
                    @sourceOrderLineNumber,
                    @sourceReceiptNumber);
                """;
            insertCommand.Parameters.AddWithValue("@invoiceId", invoiceId.ToString());
            insertCommand.Parameters.AddWithValue("@lineNumber", line.LineNumber);
            insertCommand.Parameters.AddWithValue("@itemCode", DbValue(line.ItemCode));
            insertCommand.Parameters.AddWithValue("@description", line.Description);
            insertCommand.Parameters.AddWithValue("@quantity", line.Quantity);
            insertCommand.Parameters.AddWithValue("@unitOfMeasure", DbValue(line.UnitOfMeasure));
            insertCommand.Parameters.AddWithValue("@unitPrice", line.UnitPrice);
            insertCommand.Parameters.AddWithValue("@taxRate", line.TaxRate);
            insertCommand.Parameters.AddWithValue("@sourceOrderNumber", DbValue(line.SourceOrderNumber));
            insertCommand.Parameters.AddWithValue("@sourceOrderLineNumber", DbValue(line.SourceOrderLineNumber));
            insertCommand.Parameters.AddWithValue("@sourceReceiptNumber", DbValue(line.SourceReceiptNumber));
            await insertCommand.ExecuteNonQueryAsync(cancellationToken);
        }
    }

    private async Task ReplaceInvoiceReceiptsAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid invoiceId,
        IReadOnlyCollection<PurchaseInvoiceReceiptLinkDto> receipts,
        CancellationToken cancellationToken)
    {
        await using (var deleteCommand = connection.CreateCommand())
        {
            deleteCommand.Transaction = transaction;
            deleteCommand.CommandText = "DELETE FROM purchase_invoice_receipts WHERE invoice_id = @invoiceId;";
            deleteCommand.Parameters.AddWithValue("@invoiceId", invoiceId.ToString());
            await deleteCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        foreach (var receipt in receipts)
        {
            await using var insertCommand = connection.CreateCommand();
            insertCommand.Transaction = transaction;
            insertCommand.CommandText =
                """
                INSERT INTO purchase_invoice_receipts (
                    invoice_id,
                    receipt_id,
                    receipt_series,
                    receipt_number,
                    order_number,
                    receipt_date,
                    total_received_quantity)
                VALUES (
                    @invoiceId,
                    @receiptId,
                    @receiptSeries,
                    @receiptNumber,
                    @orderNumber,
                    @receiptDate,
                    @totalReceivedQuantity);
                """;
            insertCommand.Parameters.AddWithValue("@invoiceId", invoiceId.ToString());
            insertCommand.Parameters.AddWithValue("@receiptId", receipt.ReceiptId.HasValue ? receipt.ReceiptId.Value.ToString() : DBNull.Value);
            insertCommand.Parameters.AddWithValue("@receiptSeries", DbValue(receipt.ReceiptSeries));
            insertCommand.Parameters.AddWithValue("@receiptNumber", receipt.ReceiptNumber);
            insertCommand.Parameters.AddWithValue("@orderNumber", receipt.OrderNumber);
            insertCommand.Parameters.AddWithValue("@receiptDate", receipt.ReceiptDate);
            insertCommand.Parameters.AddWithValue("@totalReceivedQuantity", receipt.TotalReceivedQuantity);
            await insertCommand.ExecuteNonQueryAsync(cancellationToken);
        }
    }

    private static void NormalizeAndValidateInvoice(SavePurchaseInvoiceCommand command)
    {
        command.SupplierName = command.SupplierName.Trim();
        command.SupplierTaxId = command.SupplierTaxId.Trim();
        command.SupplierDocumentNumber = command.SupplierDocumentNumber.Trim();
        command.Status = command.Status.Trim();
        command.Notes = command.Notes.Trim();

        if (command.SupplierCode <= 0)
        {
            throw new InvalidOperationException("Debes seleccionar un proveedor.");
        }

        if (command.DocumentDate == default)
        {
            command.DocumentDate = DateTime.Today;
        }

        if (!PurchaseInvoiceStatuses.All.Contains(command.Status, StringComparer.OrdinalIgnoreCase))
        {
            command.Status = PurchaseInvoiceStatuses.Draft;
        }

        var normalizedLines = new List<SavePurchaseInvoiceLineInputDto>();
        var lineNumber = 1;
        foreach (var line in command.Lines)
        {
            if (string.IsNullOrWhiteSpace(line.Description) && string.IsNullOrWhiteSpace(line.ItemCode))
            {
                continue;
            }

            if (string.IsNullOrWhiteSpace(line.Description))
            {
                throw new InvalidOperationException("Todas las líneas deben tener descripción.");
            }

            if (line.Quantity <= 0)
            {
                throw new InvalidOperationException("La cantidad de cada línea debe ser mayor que cero.");
            }

            if (line.UnitPrice < 0)
            {
                throw new InvalidOperationException("El precio unitario no puede ser negativo.");
            }

            if (line.TaxRate < 0)
            {
                throw new InvalidOperationException("El IVA de la línea no puede ser negativo.");
            }

            normalizedLines.Add(new SavePurchaseInvoiceLineInputDto
            {
                LineNumber = lineNumber++,
                ItemCode = line.ItemCode.Trim(),
                Description = line.Description.Trim(),
                Quantity = decimal.Round(line.Quantity, 3, MidpointRounding.AwayFromZero),
                UnitOfMeasure = line.UnitOfMeasure.Trim(),
                UnitPrice = decimal.Round(line.UnitPrice, 4, MidpointRounding.AwayFromZero),
                TaxRate = decimal.Round(line.TaxRate, 4, MidpointRounding.AwayFromZero),
                SourceOrderNumber = line.SourceOrderNumber,
                SourceOrderLineNumber = line.SourceOrderLineNumber,
                SourceReceiptNumber = line.SourceReceiptNumber
            });
        }

        if (normalizedLines.Count == 0)
        {
            throw new InvalidOperationException("Debes indicar al menos una línea en la factura de proveedor.");
        }

        command.Lines = normalizedLines;

        var normalizedReceipts = command.Receipts
            .Where(receipt => receipt.ReceiptNumber > 0)
            .GroupBy(receipt => receipt.ReceiptNumber)
            .Select(group => group.First())
            .ToList();

        command.Receipts = normalizedReceipts;
    }

    private async Task<IReadOnlyCollection<PurchaseInvoiceReceiptLinkDto>> ResolveReceiptLinksAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid tenantId,
        Guid companyId,
        IReadOnlyCollection<PurchaseInvoiceReceiptLinkDto> receipts,
        CancellationToken cancellationToken)
    {
        if (receipts.Count == 0)
        {
            return [];
        }

        var items = new List<PurchaseInvoiceReceiptLinkDto>();
        foreach (var receipt in receipts)
        {
            if (receipt.ReceiptNumber <= 0)
            {
                continue;
            }

            if (receipt.ReceiptId.HasValue && receipt.ReceiptDate != default)
            {
                items.Add(receipt);
                continue;
            }

            await using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText =
                """
                SELECT pr.receipt_id,
                       pr.receipt_series,
                       pr.receipt_number,
                       pr.order_number,
                       pr.receipt_date,
                       COALESCE(SUM(prl.received_quantity), 0) AS total_received_quantity
                FROM purchase_order_receipts pr
                LEFT JOIN purchase_order_receipt_lines prl
                  ON prl.receipt_id = pr.receipt_id
                WHERE pr.tenant_id = @tenantId
                  AND pr.company_id = @companyId
                  AND COALESCE(pr.is_deleted, 0) = 0
                  AND pr.receipt_number = @receiptNumber
                GROUP BY pr.receipt_id, pr.receipt_series, pr.receipt_number, pr.order_number, pr.receipt_date
                LIMIT 1;
                """;
            command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            command.Parameters.AddWithValue("@companyId", companyId.ToString());
            command.Parameters.AddWithValue("@receiptNumber", receipt.ReceiptNumber);

            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            if (!await reader.ReadAsync(cancellationToken))
            {
                throw new InvalidOperationException($"La recepción {receipt.ReceiptNumber} no existe o no pertenece a la empresa activa.");
            }

            items.Add(new PurchaseInvoiceReceiptLinkDto
            {
                ReceiptId = reader.GetGuid("receipt_id"),
                ReceiptSeries = reader.GetStringOrEmpty("receipt_series"),
                ReceiptNumber = reader.GetInt32(reader.GetOrdinal("receipt_number")),
                OrderNumber = reader.GetInt32(reader.GetOrdinal("order_number")),
                ReceiptDate = reader.GetDateTime(reader.GetOrdinal("receipt_date")),
                TotalReceivedQuantity = reader.GetDecimal(reader.GetOrdinal("total_received_quantity"))
            });
        }

        return items;
    }

    private async Task<HeaderSnapshot?> LoadHeaderSnapshotAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid tenantId,
        Guid companyId,
        int invoiceNumber,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            SELECT invoice_id,
                   invoice_series,
                   invoice_number,
                   supplier_name,
                   status,
                   total_amount,
                   amount_paid,
                   outstanding_amount,
                   last_payment_utc
            FROM purchase_invoices
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND invoice_number = @invoiceNumber
              AND COALESCE(is_deleted, 0) = 0
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@invoiceNumber", invoiceNumber);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        return new HeaderSnapshot(
            reader.GetGuid("invoice_id"),
            reader.GetStringOrEmpty("invoice_series"),
            reader.GetInt32(reader.GetOrdinal("invoice_number")),
            reader.GetStringOrEmpty("supplier_name"),
            reader.GetStringOrEmpty("status"),
            reader.GetDecimal(reader.GetOrdinal("total_amount")),
            reader.GetDecimal(reader.GetOrdinal("amount_paid")),
            reader.GetDecimal(reader.GetOrdinal("outstanding_amount")),
            reader.IsDBNull(reader.GetOrdinal("last_payment_utc")) ? null : reader.GetDateTime(reader.GetOrdinal("last_payment_utc")));
    }

    private async Task<InvoicePaymentHeader?> LoadInvoicePaymentHeaderAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid tenantId,
        Guid companyId,
        int invoiceNumber,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            SELECT invoice_id,
                   invoice_series,
                   invoice_number,
                   status,
                   total_amount,
                   amount_paid,
                   outstanding_amount
            FROM purchase_invoices
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND invoice_number = @invoiceNumber
              AND COALESCE(is_deleted, 0) = 0
            LIMIT 1
            FOR UPDATE;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@invoiceNumber", invoiceNumber);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        return new InvoicePaymentHeader(
            reader.GetGuid("invoice_id"),
            reader.GetStringOrEmpty("invoice_series"),
            reader.GetInt32(reader.GetOrdinal("invoice_number")),
            reader.GetStringOrEmpty("status"),
            reader.GetDecimal(reader.GetOrdinal("total_amount")),
            reader.GetDecimal(reader.GetOrdinal("amount_paid")),
            reader.GetDecimal(reader.GetOrdinal("outstanding_amount")));
    }

    private async Task<int> GetNextInvoiceNumberAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid tenantId,
        Guid companyId,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            SELECT COALESCE(MAX(invoice_number), 0) + 1
            FROM purchase_invoices
            WHERE tenant_id = @tenantId
              AND company_id = @companyId;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        return Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));
    }

    private static async Task<int> GetNextInvoicePaymentNumberAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid invoiceId,
        Guid tenantId,
        Guid companyId,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            SELECT COALESCE(MAX(payment_number), 0) + 1
            FROM purchase_invoice_payments
            WHERE invoice_id = @invoiceId
              AND tenant_id = @tenantId
              AND company_id = @companyId;
            """;
        command.Parameters.AddWithValue("@invoiceId", invoiceId.ToString());
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        return Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));
    }

    private static async Task<string> ResolveCompanyCenterCodeAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT legacy_center_code
            FROM companies
            WHERE id = @companyId
              AND tenant_id = @tenantId
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        var result = await command.ExecuteScalarAsync(cancellationToken);
        return result?.ToString()?.Trim().ToUpperInvariant() ?? string.Empty;
    }

    private static async Task<SupplierSnapshot> ResolveSupplierSnapshotAsync(
        MySqlConnection connection,
        string centerCode,
        int supplierCode,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT CODI, NOM, NIF
            FROM prove
            WHERE CODI = @supplierCode
              AND CENTRO = @centerCode
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@supplierCode", supplierCode);
        command.Parameters.AddWithValue("@centerCode", centerCode);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return new SupplierSnapshot(supplierCode, string.Empty, string.Empty);
        }

        return new SupplierSnapshot(
            reader.GetInt32(reader.GetOrdinal("CODI")),
            reader.GetStringOrEmpty("NOM"),
            reader.GetStringOrEmpty("NIF"));
    }

    private static string DetermineEffectiveInvoiceStatus(string requestedStatus, decimal totalAmount, decimal outstandingAmount)
    {
        if (string.Equals(requestedStatus, PurchaseInvoiceStatuses.Cancelled, StringComparison.OrdinalIgnoreCase))
        {
            return PurchaseInvoiceStatuses.Cancelled;
        }

        if (totalAmount > 0m && outstandingAmount <= 0m)
        {
            return PurchaseInvoiceStatuses.Paid;
        }

        if (string.Equals(requestedStatus, PurchaseInvoiceStatuses.Paid, StringComparison.OrdinalIgnoreCase))
        {
            return PurchaseInvoiceStatuses.Registered;
        }

        return requestedStatus;
    }

    private static void NormalizeAndValidateInvoicePayment(RegisterPurchaseInvoicePaymentCommand command)
    {
        command.Method = command.Method.Trim();
        command.Reference = command.Reference.Trim();
        command.Notes = command.Notes.Trim();

        if (command.InvoiceNumber <= 0)
        {
            throw new InvalidOperationException("Debes indicar una factura válida.");
        }

        if (command.PaymentDate == default)
        {
            command.PaymentDate = DateTime.Today;
        }

        command.PaymentDate = command.PaymentDate.Date;
        command.Amount = decimal.Round(command.Amount, 2, MidpointRounding.AwayFromZero);

        if (command.Amount <= 0m)
        {
            throw new InvalidOperationException("El importe del pago debe ser mayor que cero.");
        }
    }

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

    private static string BuildInvoiceSeries(string centerCode) =>
        $"FCP-{(string.IsNullOrWhiteSpace(centerCode) ? "GEN" : centerCode.Trim().ToUpperInvariant())}";

    private static object DbValue(string value) => string.IsNullOrWhiteSpace(value) ? DBNull.Value : value;
    private static object DbValue(DateTime? value) => value.HasValue ? value.Value : DBNull.Value;
    private static object DbValue(int? value) => value.HasValue ? value.Value : DBNull.Value;
    private static string BuildInvoiceDisplayNumber(string series, int number) =>
        string.IsNullOrWhiteSpace(series) ? number.ToString() : $"{series}/{number:000000}";

    private static string BuildPurchaseInvoiceSearchOrderByClause(PurchaseInvoiceFilter filter)
    {
        var column = filter.SortColumn switch
        {
            nameof(PurchaseInvoiceListItemDto.InvoiceNumber) => "pi.invoice_number",
            nameof(PurchaseInvoiceListItemDto.SupplierName) => "supplier_name",
            nameof(PurchaseInvoiceListItemDto.DocumentDate) => "pi.document_date",
            nameof(PurchaseInvoiceListItemDto.DueDate) => "pi.due_date",
            nameof(PurchaseInvoiceListItemDto.Status) => "pi.status",
            nameof(PurchaseInvoiceListItemDto.ReceiptCount) => "receipt_count",
            nameof(PurchaseInvoiceListItemDto.OrderCount) => "order_count",
            nameof(PurchaseInvoiceListItemDto.TotalAmount) => "pi.total_amount",
            _ => string.Empty
        };

        if (string.IsNullOrWhiteSpace(column))
        {
            return "ORDER BY pi.document_date DESC, pi.invoice_number DESC";
        }

        var direction = filter.SortDescending ? "DESC" : "ASC";
        return $"ORDER BY {column} {direction}, pi.invoice_number DESC";
    }

    private sealed record SupplierSnapshot(int Code, string Name, string TaxId);
    private sealed record HeaderSnapshot(
        Guid InvoiceId,
        string InvoiceSeries,
        int InvoiceNumber,
        string SupplierName,
        string Status,
        decimal TotalAmount,
        decimal AmountPaid,
        decimal OutstandingAmount,
        DateTime? LastPaymentUtc);
    private sealed record InvoicePaymentHeader(
        Guid InvoiceId,
        string InvoiceSeries,
        int InvoiceNumber,
        string Status,
        decimal TotalAmount,
        decimal AmountPaid,
        decimal OutstandingAmount)
    {
        public string InvoiceDisplayNumber => BuildInvoiceDisplayNumber(InvoiceSeries, InvoiceNumber);
    }
}
