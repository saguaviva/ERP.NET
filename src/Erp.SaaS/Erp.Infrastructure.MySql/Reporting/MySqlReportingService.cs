using Erp.Application.Companies;
using Erp.Application.Contexts;
using Erp.Application.Reporting;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Support;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Reporting;

public sealed class MySqlReportingService : IReportingQueries
{
    private readonly MySqlConnectionFactory _connectionFactory;
    private readonly ICompanyAccessService _companyAccessService;
    private readonly ICurrentUserContext _currentUserContext;
    private readonly ITenantContext _tenantContext;
    private readonly IActiveCompanyContext _activeCompanyContext;

    public MySqlReportingService(
        MySqlConnectionFactory connectionFactory,
        ICompanyAccessService companyAccessService,
        ICurrentUserContext currentUserContext,
        ITenantContext tenantContext,
        IActiveCompanyContext activeCompanyContext)
    {
        _connectionFactory = connectionFactory;
        _companyAccessService = companyAccessService;
        _currentUserContext = currentUserContext;
        _tenantContext = tenantContext;
        _activeCompanyContext = activeCompanyContext;
    }

    public async Task<ReportingOverviewDto> GetOverviewAsync(Guid tenantId, Guid companyId, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return new ReportingOverviewDto();
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);

        var monthStart = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        var nextMonthStart = monthStart.AddMonths(1);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var centerCode = await ResolveCompanyCenterCodeAsync(tenantId, companyId, cancellationToken);
        var purchaseInvoiceDateExpression = await ResolveDateExpressionAsync(
            connection,
            "purchase_invoices",
            "pi",
            "document_date",
            "due_date",
            "created_utc",
            "updated_utc",
            "synced_utc");

        var dto = new ReportingOverviewDto
        {
            Clients = await ExecuteScalarIntAsync(connection,
                """
                SELECT COUNT(*)
                FROM clients
                WHERE CENTRO = @centerCode
                  AND COALESCE(is_deleted, 0) = 0;
                """,
                centerCode,
                cancellationToken),
            Suppliers = await ExecuteScalarIntAsync(connection,
                """
                SELECT COUNT(*)
                FROM prove
                WHERE CENTRO = @centerCode
                  AND COALESCE(is_deleted, 0) = 0;
                """,
                centerCode,
                cancellationToken),
            Fabrics = await ExecuteScalarIntAsync(connection,
                """
                SELECT COUNT(*)
                FROM teixits
                WHERE CENTRO = @centerCode
                  AND COALESCE(is_deleted, 0) = 0;
                """,
                centerCode,
                cancellationToken),
            Yarns = await ExecuteScalarIntAsync(connection,
                """
                SELECT COUNT(*)
                FROM fil
                WHERE CENTRO = @centerCode
                  AND COALESCE(is_deleted, 0) = 0;
                """,
                centerCode,
                cancellationToken),
            Samples = await ExecuteScalarIntAsync(connection,
                """
                SELECT COUNT(*)
                FROM mostres
                WHERE CENTRO = @centerCode
                  AND COALESCE(is_deleted, 0) = 0;
                """,
                centerCode,
                cancellationToken),
            Models = await ExecuteScalarIntAsync(connection,
                """
                SELECT COUNT(*)
                FROM article_models
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND COALESCE(is_deleted, 0) = 0;
                """,
                tenantId,
                companyId,
                cancellationToken),
            PendingDispositions = await ExecuteScalarIntAsync(connection,
                """
                SELECT COUNT(*)
                FROM dispos
                WHERE CENTRO = @centerCode
                  AND COALESCE(is_deleted, 0) = 0
                  AND COALESCE(ANULADA, 0) = 0
                  AND COALESCE(RECIBIDO, 0) = 0;
                """,
                centerCode,
                cancellationToken),
            LiveFinishOrders = await ExecuteScalarIntAsync(connection,
                """
                SELECT COUNT(*)
                FROM finish_work_orders
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND COALESCE(is_deleted, 0) = 0
                  AND status IN ('Pending', 'InProgress');
                """,
                tenantId,
                companyId,
                cancellationToken),
            SalesDocumentsThisMonth = await ExecuteScalarIntAsync(connection,
                """
                SELECT
                    COALESCE((
                        SELECT COUNT(*)
                        FROM sales_order_shipments
                        WHERE tenant_id = @tenantId
                          AND company_id = @companyId
                          AND COALESCE(is_deleted, 0) = 0
                          AND shipment_date >= @dateFrom
                          AND shipment_date < @dateToExclusive
                    ), 0)
                    +
                    COALESCE((
                        SELECT COUNT(*)
                        FROM sales_invoices
                        WHERE tenant_id = @tenantId
                          AND company_id = @companyId
                          AND COALESCE(is_deleted, 0) = 0
                          AND issue_date >= @dateFrom
                          AND issue_date < @dateToExclusive
                    ), 0);
                """,
                tenantId,
                companyId,
                cancellationToken,
                monthStart,
                nextMonthStart),
            PurchaseDocumentsThisMonth = await ExecuteScalarIntAsync(connection,
                $"""
                SELECT
                    COALESCE((
                        SELECT COUNT(*)
                        FROM purchase_order_receipts
                        WHERE tenant_id = @tenantId
                          AND company_id = @companyId
                          AND COALESCE(is_deleted, 0) = 0
                          AND receipt_date >= @dateFrom
                          AND receipt_date < @dateToExclusive
                    ), 0)
                    +
                    COALESCE((
                        SELECT COUNT(*)
                        FROM purchase_invoices pi
                        WHERE pi.tenant_id = @tenantId
                          AND pi.company_id = @companyId
                          AND COALESCE(pi.is_deleted, 0) = 0
                          AND {purchaseInvoiceDateExpression} >= @dateFrom
                          AND {purchaseInvoiceDateExpression} < @dateToExclusive
                    ), 0);
                """,
                tenantId,
                companyId,
                cancellationToken,
                monthStart,
                nextMonthStart),
            InventoryPositions = await ExecuteScalarIntAsync(connection,
                """
                SELECT COUNT(*)
                FROM (
                    SELECT warehouse, item_code, item_description, unit_of_measure
                    FROM inventory_movements
                    WHERE tenant_id = @tenantId
                      AND company_id = @companyId
                    GROUP BY warehouse, item_code, item_description, unit_of_measure
                    HAVING COALESCE(SUM(
                        CASE WHEN movement_type LIKE 'Inbound%' THEN quantity ELSE -quantity END
                    ), 0) <> 0
                ) balances;
                """,
                tenantId,
                companyId,
                cancellationToken),
            InventoryMovesThisMonth = await ExecuteScalarIntAsync(connection,
                """
                SELECT COUNT(*)
                FROM inventory_movements
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND movement_date >= @dateFrom
                  AND movement_date < @dateToExclusive;
                """,
                tenantId,
                companyId,
                cancellationToken,
                monthStart,
                nextMonthStart)
        };

        return dto;
    }

    public async Task<OperationalDocumentSearchResultDto> SearchOperationalDocumentsAsync(
        Guid tenantId,
        Guid companyId,
        OperationalDocumentFilter filter,
        CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return new OperationalDocumentSearchResultDto();
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);

        var search = filter.Search?.Trim() ?? string.Empty;
        var likeSearch = $"%{search}%";
        var category = NormalizeCategory(filter.Category);
        var typeKey = NormalizeTypeKey(filter.TypeKey);
        var pageSize = Math.Clamp(filter.PageSize, 10, 200);
        var page = Math.Max(filter.Page, 1);
        var offset = (page - 1) * pageSize;
        var dateFrom = filter.DateFrom?.Date;
        var dateToExclusive = filter.DateTo?.Date.AddDays(1);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var salesOrderDateExpression = await ResolveDateExpressionAsync(
            connection,
            "sales_orders",
            "so",
            "document_date",
            "requested_date",
            "created_utc",
            "updated_utc",
            "synced_utc");
        var purchaseOrderDateExpression = await ResolveDateExpressionAsync(
            connection,
            "purchase_orders",
            "po",
            "document_date",
            "expected_date",
            "created_utc",
            "updated_utc",
            "synced_utc");
        var purchaseInvoiceDateExpression = await ResolveDateExpressionAsync(
            connection,
            "purchase_invoices",
            "pi",
            "document_date",
            "due_date",
            "created_utc",
            "updated_utc",
            "synced_utc");
        var finishWorkOrderDateExpression = await ResolveDateExpressionAsync(
            connection,
            "finish_work_orders",
            "fwo",
            "work_date",
            "created_utc",
            "updated_utc",
            "synced_utc");

        await using (var countCommand = connection.CreateCommand())
        {
            countCommand.CommandText =
                $"""
                SELECT COUNT(*)
                FROM ({BuildOperationalDocumentUnionSql(false, salesOrderDateExpression, purchaseOrderDateExpression, purchaseInvoiceDateExpression, finishWorkOrderDateExpression)}) docs
                WHERE (@category = '' OR docs.category = @category)
                  AND (@typeKey = '' OR docs.type_key = @typeKey)
                  AND (@search = '' OR docs.document_display LIKE @likeSearch OR docs.party_name LIKE @likeSearch OR docs.status LIKE @likeSearch OR docs.type_label LIKE @likeSearch)
                  AND (@dateFrom IS NULL OR docs.document_date >= @dateFrom)
                  AND (@dateToExclusive IS NULL OR docs.document_date < @dateToExclusive);
                """;
            FillOperationalDocumentParameters(countCommand, tenantId, companyId, category, typeKey, search, likeSearch, dateFrom, dateToExclusive);

            var totalCount = Convert.ToInt32(await countCommand.ExecuteScalarAsync(cancellationToken));
            if (totalCount == 0)
            {
                return new OperationalDocumentSearchResultDto();
            }

            await using var command = connection.CreateCommand();
            command.CommandText =
                $"""
                SELECT docs.category,
                       docs.type_key,
                       docs.type_label,
                       docs.document_number,
                       docs.document_display,
                       docs.document_date,
                       docs.party_name,
                       docs.status,
                       docs.amount
                FROM ({BuildOperationalDocumentUnionSql(false, salesOrderDateExpression, purchaseOrderDateExpression, purchaseInvoiceDateExpression, finishWorkOrderDateExpression)}) docs
                WHERE (@category = '' OR docs.category = @category)
                  AND (@typeKey = '' OR docs.type_key = @typeKey)
                  AND (@search = '' OR docs.document_display LIKE @likeSearch OR docs.party_name LIKE @likeSearch OR docs.status LIKE @likeSearch OR docs.type_label LIKE @likeSearch)
                  AND (@dateFrom IS NULL OR docs.document_date >= @dateFrom)
                  AND (@dateToExclusive IS NULL OR docs.document_date < @dateToExclusive)
                {BuildOperationalDocumentOrderByClause(filter)}
                LIMIT @pageSize OFFSET @offset;
                """;
            FillOperationalDocumentParameters(command, tenantId, companyId, category, typeKey, search, likeSearch, dateFrom, dateToExclusive);
            command.Parameters.AddWithValue("@pageSize", pageSize);
            command.Parameters.AddWithValue("@offset", offset);

            var items = new List<OperationalDocumentListItemDto>();
            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                var item = new OperationalDocumentListItemDto
                {
                    Category = reader.GetStringOrEmpty("category"),
                    TypeKey = reader.GetStringOrEmpty("type_key"),
                    TypeLabel = reader.GetStringOrEmpty("type_label"),
                    DocumentNumber = reader.GetInt32OrDefault("document_number"),
                    DocumentDisplay = reader.GetStringOrEmpty("document_display"),
                    DocumentDate = reader.GetDateTime(reader.GetOrdinal("document_date")),
                    PartyName = reader.GetStringOrEmpty("party_name"),
                    Status = reader.GetStringOrEmpty("status"),
                    Amount = reader.GetDecimalOrDefault("amount")
                };
                item.Route = BuildDocumentRoute(item.TypeKey, item.DocumentNumber);
                items.Add(item);
            }

            return new OperationalDocumentSearchResultDto
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }

    public async Task<BusinessStatisticsDto> GetBusinessStatisticsAsync(
        Guid tenantId,
        Guid companyId,
        BusinessStatisticsFilter filter,
        CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return new BusinessStatisticsDto();
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);

        var dateFrom = filter.DateFrom?.Date;
        var dateToExclusive = filter.DateTo?.Date.AddDays(1);
        var effectiveDateFrom = dateFrom ?? DateTime.Today.AddDays(-30);
        var effectiveDateToExclusive = dateToExclusive ?? DateTime.Today.AddDays(1);
        if (effectiveDateToExclusive <= effectiveDateFrom)
        {
            effectiveDateToExclusive = effectiveDateFrom.AddDays(1);
        }

        var previousDateToExclusive = effectiveDateFrom;
        var previousDateFrom = effectiveDateFrom - (effectiveDateToExclusive - effectiveDateFrom);
        var rangeDays = Math.Max((int)(effectiveDateToExclusive - effectiveDateFrom).TotalDays, 1);
        var analyticsReferenceDate = effectiveDateToExclusive.AddDays(-1).Date;
        var monthToDateFrom = new DateTime(analyticsReferenceDate.Year, analyticsReferenceDate.Month, 1);
        var yearToDateFrom = new DateTime(analyticsReferenceDate.Year, 1, 1);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var salesOrderDateExpression = await ResolveDateExpressionAsync(
            connection,
            "sales_orders",
            "so",
            "document_date",
            "requested_date",
            "created_utc",
            "updated_utc",
            "synced_utc");
        var purchaseOrderDateExpression = await ResolveDateExpressionAsync(
            connection,
            "purchase_orders",
            "po",
            "document_date",
            "expected_date",
            "created_utc",
            "updated_utc",
            "synced_utc");
        var purchaseInvoiceDateExpression = await ResolveDateExpressionAsync(
            connection,
            "purchase_invoices",
            "pi",
            "document_date",
            "due_date",
            "created_utc",
            "updated_utc",
            "synced_utc");
        var finishWorkOrderDateExpression = await ResolveDateExpressionAsync(
            connection,
            "finish_work_orders",
            "fwo",
            "work_date",
            "created_utc",
            "updated_utc",
            "synced_utc");

        var dto = new BusinessStatisticsDto
        {
            SalesOrderCount = await ExecuteScalarIntAsync(connection,
                $"""
                SELECT COUNT(*)
                FROM sales_orders so
                WHERE so.tenant_id = @tenantId
                  AND so.company_id = @companyId
                  AND (@dateFrom IS NULL OR {salesOrderDateExpression} >= @dateFrom)
                  AND (@dateToExclusive IS NULL OR {salesOrderDateExpression} < @dateToExclusive);
                """,
                tenantId,
                companyId,
                cancellationToken,
                dateFrom,
                dateToExclusive),
            SalesOrderAmount = await ExecuteScalarDecimalAsync(connection,
                $"""
                SELECT COALESCE(SUM(order_amounts.total_amount), 0)
                FROM (
                    SELECT so.order_number,
                           COALESCE(SUM(sol.quantity * sol.unit_price), 0) AS total_amount
                    FROM sales_orders so
                    LEFT JOIN sales_order_lines sol
                      ON sol.tenant_id = so.tenant_id
                     AND sol.company_id = so.company_id
                     AND sol.order_number = so.order_number
                    WHERE so.tenant_id = @tenantId
                      AND so.company_id = @companyId
                      AND (@dateFrom IS NULL OR {salesOrderDateExpression} >= @dateFrom)
                      AND (@dateToExclusive IS NULL OR {salesOrderDateExpression} < @dateToExclusive)
                    GROUP BY so.order_number
                ) order_amounts;
                """,
                tenantId,
                companyId,
                cancellationToken,
                dateFrom,
                dateToExclusive),
            AverageSalesOrderAmount = 0m,
            SalesShipmentCount = await ExecuteScalarIntAsync(connection,
                """
                SELECT COUNT(*)
                FROM sales_order_shipments
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND COALESCE(is_deleted, 0) = 0
                  AND (@dateFrom IS NULL OR shipment_date >= @dateFrom)
                  AND (@dateToExclusive IS NULL OR shipment_date < @dateToExclusive);
                """,
                tenantId,
                companyId,
                cancellationToken,
                dateFrom,
                dateToExclusive),
            SalesShipmentAmount = await ExecuteScalarDecimalAsync(connection,
                """
                SELECT COALESCE(SUM(shl.shipped_quantity * COALESCE(sol.unit_price, 0)), 0)
                FROM sales_order_shipments ss
                LEFT JOIN sales_order_shipment_lines shl
                  ON shl.shipment_id = ss.shipment_id
                LEFT JOIN sales_order_lines sol
                  ON sol.tenant_id = shl.tenant_id
                 AND sol.company_id = shl.company_id
                 AND sol.order_number = shl.order_number
                 AND sol.line_number = shl.line_number
                WHERE ss.tenant_id = @tenantId
                  AND ss.company_id = @companyId
                  AND COALESCE(ss.is_deleted, 0) = 0
                  AND (@dateFrom IS NULL OR ss.shipment_date >= @dateFrom)
                  AND (@dateToExclusive IS NULL OR ss.shipment_date < @dateToExclusive);
                """,
                tenantId,
                companyId,
                cancellationToken,
                dateFrom,
                dateToExclusive),
            SalesInvoiceCount = await ExecuteScalarIntAsync(connection,
                """
                SELECT COUNT(*)
                FROM sales_invoices
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND COALESCE(is_deleted, 0) = 0
                  AND (@dateFrom IS NULL OR issue_date >= @dateFrom)
                  AND (@dateToExclusive IS NULL OR issue_date < @dateToExclusive);
                """,
                tenantId,
                companyId,
                cancellationToken,
                dateFrom,
                dateToExclusive),
            SalesInvoiceAmount = await ExecuteScalarDecimalAsync(connection,
                """
                SELECT COALESCE(SUM(total_amount), 0)
                FROM sales_invoices
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND COALESCE(is_deleted, 0) = 0
                  AND (@dateFrom IS NULL OR issue_date >= @dateFrom)
                  AND (@dateToExclusive IS NULL OR issue_date < @dateToExclusive);
                """,
                tenantId,
                companyId,
                cancellationToken,
                dateFrom,
                dateToExclusive),
            SalesOutstandingAmount = await ExecuteScalarDecimalAsync(connection,
                """
                SELECT COALESCE(SUM(outstanding_amount), 0)
                FROM sales_invoices
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND COALESCE(is_deleted, 0) = 0
                  AND (@dateFrom IS NULL OR issue_date >= @dateFrom)
                  AND (@dateToExclusive IS NULL OR issue_date < @dateToExclusive);
                """,
                tenantId,
                companyId,
                cancellationToken,
                dateFrom,
                dateToExclusive),
            SalesCollectedAmount = 0m,
            SalesCollectionRate = 0m,
            AverageSalesInvoiceAmount = 0m,
            PurchaseOrderCount = await ExecuteScalarIntAsync(connection,
                $"""
                SELECT COUNT(*)
                FROM purchase_orders po
                WHERE po.tenant_id = @tenantId
                  AND po.company_id = @companyId
                  AND COALESCE(po.is_deleted, 0) = 0
                  AND (@dateFrom IS NULL OR {purchaseOrderDateExpression} >= @dateFrom)
                  AND (@dateToExclusive IS NULL OR {purchaseOrderDateExpression} < @dateToExclusive);
                """,
                tenantId,
                companyId,
                cancellationToken,
                dateFrom,
                dateToExclusive),
            PurchaseOrderAmount = await ExecuteScalarDecimalAsync(connection,
                $"""
                SELECT COALESCE(SUM(order_amounts.total_amount), 0)
                FROM (
                    SELECT po.order_number,
                           COALESCE(SUM(pol.quantity * pol.unit_price), 0) AS total_amount
                    FROM purchase_orders po
                    LEFT JOIN purchase_order_lines pol
                      ON pol.tenant_id = po.tenant_id
                     AND pol.company_id = po.company_id
                     AND pol.order_number = po.order_number
                    WHERE po.tenant_id = @tenantId
                      AND po.company_id = @companyId
                      AND COALESCE(po.is_deleted, 0) = 0
                      AND (@dateFrom IS NULL OR {purchaseOrderDateExpression} >= @dateFrom)
                      AND (@dateToExclusive IS NULL OR {purchaseOrderDateExpression} < @dateToExclusive)
                    GROUP BY po.order_number
                ) order_amounts;
                """,
                tenantId,
                companyId,
                cancellationToken,
                dateFrom,
                dateToExclusive),
            AveragePurchaseOrderAmount = 0m,
            PurchaseReceiptCount = await ExecuteScalarIntAsync(connection,
                """
                SELECT COUNT(*)
                FROM purchase_order_receipts pr
                WHERE pr.tenant_id = @tenantId
                  AND pr.company_id = @companyId
                  AND COALESCE(pr.is_deleted, 0) = 0
                  AND (@dateFrom IS NULL OR pr.receipt_date >= @dateFrom)
                  AND (@dateToExclusive IS NULL OR pr.receipt_date < @dateToExclusive);
                """,
                tenantId,
                companyId,
                cancellationToken,
                dateFrom,
                dateToExclusive),
            PurchaseReceivedQuantity = await ExecuteScalarDecimalAsync(connection,
                """
                SELECT COALESCE(SUM(prl.received_quantity), 0)
                FROM purchase_order_receipt_lines prl
                INNER JOIN purchase_order_receipts pr
                  ON pr.receipt_id = prl.receipt_id
                 AND pr.tenant_id = prl.tenant_id
                 AND pr.company_id = prl.company_id
                WHERE prl.tenant_id = @tenantId
                  AND prl.company_id = @companyId
                  AND COALESCE(pr.is_deleted, 0) = 0
                  AND (@dateFrom IS NULL OR pr.receipt_date >= @dateFrom)
                  AND (@dateToExclusive IS NULL OR pr.receipt_date < @dateToExclusive);
                """,
                tenantId,
                companyId,
                cancellationToken,
                dateFrom,
                dateToExclusive),
            PurchaseInvoiceCount = await ExecuteScalarIntAsync(connection,
                $"""
                SELECT COUNT(*)
                FROM purchase_invoices pi
                WHERE pi.tenant_id = @tenantId
                  AND pi.company_id = @companyId
                  AND COALESCE(pi.is_deleted, 0) = 0
                  AND (@dateFrom IS NULL OR {purchaseInvoiceDateExpression} >= @dateFrom)
                  AND (@dateToExclusive IS NULL OR {purchaseInvoiceDateExpression} < @dateToExclusive);
                """,
                tenantId,
                companyId,
                cancellationToken,
                dateFrom,
                dateToExclusive),
            PurchaseInvoiceAmount = await ExecuteScalarDecimalAsync(connection,
                $"""
                SELECT COALESCE(SUM(total_amount), 0)
                FROM purchase_invoices pi
                WHERE pi.tenant_id = @tenantId
                  AND pi.company_id = @companyId
                  AND COALESCE(pi.is_deleted, 0) = 0
                  AND (@dateFrom IS NULL OR {purchaseInvoiceDateExpression} >= @dateFrom)
                  AND (@dateToExclusive IS NULL OR {purchaseInvoiceDateExpression} < @dateToExclusive);
                """,
                tenantId,
                companyId,
                cancellationToken,
                dateFrom,
                dateToExclusive),
            PurchaseOutstandingAmount = await ExecuteScalarDecimalAsync(connection,
                $"""
                SELECT COALESCE(SUM(outstanding_amount), 0)
                FROM purchase_invoices pi
                WHERE pi.tenant_id = @tenantId
                  AND pi.company_id = @companyId
                  AND COALESCE(pi.is_deleted, 0) = 0
                  AND (@dateFrom IS NULL OR {purchaseInvoiceDateExpression} >= @dateFrom)
                  AND (@dateToExclusive IS NULL OR {purchaseInvoiceDateExpression} < @dateToExclusive);
                """,
                tenantId,
                companyId,
                cancellationToken,
                dateFrom,
                dateToExclusive),
            PurchasePaidAmount = 0m,
            PurchasePaymentRate = 0m,
            AveragePurchaseInvoiceAmount = 0m,
            RemittanceCount = await ExecuteScalarIntAsync(connection,
                """
                SELECT COUNT(*)
                FROM sales_remittances
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND COALESCE(is_deleted, 0) = 0
                  AND (@dateFrom IS NULL OR remittance_date >= @dateFrom)
                  AND (@dateToExclusive IS NULL OR remittance_date < @dateToExclusive);
                """,
                tenantId,
                companyId,
                cancellationToken,
                dateFrom,
                dateToExclusive),
            RemittanceOutstandingAmount = await ExecuteScalarDecimalAsync(connection,
                """
                SELECT COALESCE(SUM(outstanding_amount), 0)
                FROM sales_remittances
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND COALESCE(is_deleted, 0) = 0
                  AND (@dateFrom IS NULL OR remittance_date >= @dateFrom)
                  AND (@dateToExclusive IS NULL OR remittance_date < @dateToExclusive);
                """,
                tenantId,
                companyId,
                cancellationToken,
                dateFrom,
                dateToExclusive),
            LiveFinishOrders = await ExecuteScalarIntAsync(connection,
                """
                SELECT COUNT(*)
                FROM finish_work_orders
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND COALESCE(is_deleted, 0) = 0
                  AND status IN ('Pending', 'InProgress')
                  AND (@dateFrom IS NULL OR work_date >= @dateFrom)
                  AND (@dateToExclusive IS NULL OR work_date < @dateToExclusive);
                """,
                tenantId,
                companyId,
                cancellationToken,
                dateFrom,
                dateToExclusive),
            DraftCounts = await ExecuteScalarIntAsync(connection,
                """
                SELECT COUNT(*)
                FROM stock_counts
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND COALESCE(is_deleted, 0) = 0
                  AND status = 'Draft';
                """,
                tenantId,
                companyId,
                cancellationToken),
            DraftTransfers = await ExecuteScalarIntAsync(connection,
                """
                SELECT COUNT(*)
                FROM stock_transfers
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND COALESCE(is_deleted, 0) = 0
                  AND status = 'Draft';
                """,
                tenantId,
                companyId,
                cancellationToken),
            StockPositions = await ExecuteScalarIntAsync(connection,
                """
                SELECT COUNT(*)
                FROM (
                    SELECT warehouse, item_code, item_description, unit_of_measure
                    FROM inventory_movements
                    WHERE tenant_id = @tenantId
                      AND company_id = @companyId
                    GROUP BY warehouse, item_code, item_description, unit_of_measure
                    HAVING COALESCE(SUM(
                        CASE WHEN movement_type LIKE 'Inbound%' THEN quantity ELSE -quantity END
                    ), 0) <> 0
                ) balances;
                """,
                tenantId,
                companyId,
                cancellationToken),
            StockMovementsInRange = await ExecuteScalarIntAsync(connection,
                """
                SELECT COUNT(*)
                FROM inventory_movements
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND (@dateFrom IS NULL OR movement_date >= @dateFrom)
                  AND (@dateToExclusive IS NULL OR movement_date < @dateToExclusive);
                """,
                tenantId,
                companyId,
                cancellationToken,
                dateFrom,
                dateToExclusive),
            NetBillingBalance = 0m,
            TopClients = await LoadBreakdownAsync(connection,
                """
                SELECT COALESCE(NULLIF(client_name, ''), 'Cliente sin nombre') AS label,
                       COUNT(*) AS item_count,
                       COALESCE(SUM(total_amount), 0) AS total_amount
                FROM sales_invoices
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND COALESCE(is_deleted, 0) = 0
                  AND (@dateFrom IS NULL OR issue_date >= @dateFrom)
                  AND (@dateToExclusive IS NULL OR issue_date < @dateToExclusive)
                GROUP BY COALESCE(NULLIF(client_name, ''), 'Cliente sin nombre')
                ORDER BY total_amount DESC, item_count DESC, label
                LIMIT 8;
                """,
                tenantId,
                companyId,
                cancellationToken,
                dateFrom,
                dateToExclusive),
            TopSuppliers = await LoadBreakdownAsync(connection,
                $"""
                SELECT COALESCE(NULLIF(supplier_name, ''), 'Proveedor sin nombre') AS label,
                       COUNT(*) AS item_count,
                       COALESCE(SUM(total_amount), 0) AS total_amount
                FROM purchase_invoices pi
                WHERE pi.tenant_id = @tenantId
                  AND pi.company_id = @companyId
                  AND COALESCE(pi.is_deleted, 0) = 0
                  AND (@dateFrom IS NULL OR {purchaseInvoiceDateExpression} >= @dateFrom)
                  AND (@dateToExclusive IS NULL OR {purchaseInvoiceDateExpression} < @dateToExclusive)
                GROUP BY COALESCE(NULLIF(supplier_name, ''), 'Proveedor sin nombre')
                ORDER BY total_amount DESC, item_count DESC, label
                LIMIT 6;
                """,
                tenantId,
                companyId,
                cancellationToken,
                dateFrom,
                dateToExclusive),
            TopWarehouses = await LoadBreakdownAsync(connection,
                """
                SELECT COALESCE(NULLIF(warehouse, ''), 'Sin almacén') AS label,
                       COUNT(*) AS item_count,
                       COALESCE(SUM(ABS(quantity)), 0) AS total_amount
                FROM inventory_movements
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND (@dateFrom IS NULL OR movement_date >= @dateFrom)
                  AND (@dateToExclusive IS NULL OR movement_date < @dateToExclusive)
                GROUP BY COALESCE(NULLIF(warehouse, ''), 'Sin almacén')
                ORDER BY total_amount DESC, item_count DESC, label
                LIMIT 6;
                """,
                tenantId,
                companyId,
                cancellationToken,
                dateFrom,
                dateToExclusive),
            MovementTypeBreakdown = await LoadBreakdownAsync(connection,
                """
                SELECT COALESCE(NULLIF(movement_type, ''), 'Sin tipo') AS label,
                       COUNT(*) AS item_count,
                       COALESCE(SUM(ABS(quantity)), 0) AS total_amount
                FROM inventory_movements
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND (@dateFrom IS NULL OR movement_date >= @dateFrom)
                  AND (@dateToExclusive IS NULL OR movement_date < @dateToExclusive)
                GROUP BY COALESCE(NULLIF(movement_type, ''), 'Sin tipo')
                ORDER BY item_count DESC, total_amount DESC, label
                LIMIT 8;
                """,
                tenantId,
                companyId,
                cancellationToken,
                dateFrom,
                dateToExclusive),
            TopBilledItems = await LoadBreakdownAsync(connection,
                """
                SELECT COALESCE(NULLIF(TRIM(CONCAT(COALESCE(NULLIF(sil.item_code, ''), ''), ' ', sil.description)), ''), 'Artículo sin descripción') AS label,
                       COUNT(*) AS item_count,
                       COALESCE(SUM(sil.line_total), 0) AS total_amount
                FROM sales_invoice_lines sil
                INNER JOIN sales_invoices si
                  ON si.invoice_id = sil.invoice_id
                 AND si.tenant_id = sil.tenant_id
                 AND si.company_id = sil.company_id
                WHERE sil.tenant_id = @tenantId
                  AND sil.company_id = @companyId
                  AND COALESCE(si.is_deleted, 0) = 0
                  AND (@dateFrom IS NULL OR si.issue_date >= @dateFrom)
                  AND (@dateToExclusive IS NULL OR si.issue_date < @dateToExclusive)
                GROUP BY COALESCE(NULLIF(TRIM(CONCAT(COALESCE(NULLIF(sil.item_code, ''), ''), ' ', sil.description)), ''), 'Artículo sin descripción')
                ORDER BY total_amount DESC, item_count DESC, label
                LIMIT 8;
                """,
                tenantId,
                companyId,
                cancellationToken,
                dateFrom,
                dateToExclusive),
            TopFinishers = await LoadBreakdownAsync(connection,
                """
                SELECT COALESCE(NULLIF(finisher_name, ''), 'Acabador sin nombre') AS label,
                       COUNT(*) AS item_count,
                       COALESCE(SUM(total_kilograms), 0) AS total_amount
                FROM finish_work_orders
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND COALESCE(is_deleted, 0) = 0
                  AND (@dateFrom IS NULL OR work_date >= @dateFrom)
                  AND (@dateToExclusive IS NULL OR work_date < @dateToExclusive)
                GROUP BY COALESCE(NULLIF(finisher_name, ''), 'Acabador sin nombre')
                ORDER BY total_amount DESC, item_count DESC, label
                LIMIT 8;
                """,
                tenantId,
                companyId,
                cancellationToken,
                dateFrom,
                dateToExclusive),
            SalesStatusBreakdown = await LoadBreakdownAsync(connection,
                """
                SELECT COALESCE(NULLIF(status, ''), 'Sin estado') AS label,
                       COUNT(*) AS item_count,
                       COALESCE(SUM(total_amount), 0) AS total_amount
                FROM sales_invoices
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND COALESCE(is_deleted, 0) = 0
                  AND (@dateFrom IS NULL OR issue_date >= @dateFrom)
                  AND (@dateToExclusive IS NULL OR issue_date < @dateToExclusive)
                GROUP BY COALESCE(NULLIF(status, ''), 'Sin estado')
                ORDER BY item_count DESC, total_amount DESC, label;
                """,
                tenantId,
                companyId,
                cancellationToken,
                dateFrom,
                dateToExclusive),
            PurchaseStatusBreakdown = await LoadBreakdownAsync(connection,
                $"""
                SELECT COALESCE(NULLIF(status, ''), 'Sin estado') AS label,
                       COUNT(*) AS item_count,
                       COALESCE(SUM(total_amount), 0) AS total_amount
                FROM purchase_invoices pi
                WHERE pi.tenant_id = @tenantId
                  AND pi.company_id = @companyId
                  AND COALESCE(pi.is_deleted, 0) = 0
                  AND (@dateFrom IS NULL OR {purchaseInvoiceDateExpression} >= @dateFrom)
                  AND (@dateToExclusive IS NULL OR {purchaseInvoiceDateExpression} < @dateToExclusive)
                GROUP BY COALESCE(NULLIF(status, ''), 'Sin estado')
                ORDER BY item_count DESC, total_amount DESC, label;
                """,
                tenantId,
                companyId,
                cancellationToken,
                dateFrom,
                dateToExclusive),
            FinishStatusBreakdown = await LoadBreakdownAsync(connection,
                """
                SELECT COALESCE(NULLIF(status, ''), 'Sin estado') AS label,
                       COUNT(*) AS item_count,
                       CAST(0 AS DECIMAL(18, 2)) AS total_amount
                FROM finish_work_orders
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND COALESCE(is_deleted, 0) = 0
                  AND (@dateFrom IS NULL OR work_date >= @dateFrom)
                  AND (@dateToExclusive IS NULL OR work_date < @dateToExclusive)
                GROUP BY COALESCE(NULLIF(status, ''), 'Sin estado')
                ORDER BY item_count DESC, label;
                """,
                tenantId,
                companyId,
                cancellationToken,
                dateFrom,
                dateToExclusive),
            WeeklyTimeline = await LoadWeeklyTimelineAsync(
                connection,
                tenantId,
                companyId,
                cancellationToken,
                dateFrom,
                dateToExclusive,
                purchaseInvoiceDateExpression,
                finishWorkOrderDateExpression)
        };

        dto.SalesCollectedAmount = Math.Max(dto.SalesInvoiceAmount - dto.SalesOutstandingAmount, 0m);
        dto.PurchasePaidAmount = Math.Max(dto.PurchaseInvoiceAmount - dto.PurchaseOutstandingAmount, 0m);
        dto.NetBillingBalance = dto.SalesInvoiceAmount - dto.PurchaseInvoiceAmount;
        dto.SalesCollectionRate = CalculateRate(dto.SalesCollectedAmount, dto.SalesInvoiceAmount);
        dto.PurchasePaymentRate = CalculateRate(dto.PurchasePaidAmount, dto.PurchaseInvoiceAmount);
        dto.AverageSalesOrderAmount = CalculateAverage(dto.SalesOrderAmount, dto.SalesOrderCount);
        dto.AverageSalesInvoiceAmount = CalculateAverage(dto.SalesInvoiceAmount, dto.SalesInvoiceCount);
        dto.AveragePurchaseOrderAmount = CalculateAverage(dto.PurchaseOrderAmount, dto.PurchaseOrderCount);
        dto.AveragePurchaseInvoiceAmount = CalculateAverage(dto.PurchaseInvoiceAmount, dto.PurchaseInvoiceCount);
        dto.SalesInvoiceAmountMonthToDate = await ExecuteScalarDecimalAsync(connection,
            """
            SELECT COALESCE(SUM(total_amount), 0)
            FROM sales_invoices
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND COALESCE(is_deleted, 0) = 0
              AND issue_date >= @dateFrom
              AND issue_date < @dateToExclusive;
            """,
            tenantId,
            companyId,
            cancellationToken,
            monthToDateFrom,
            effectiveDateToExclusive);
        dto.SalesInvoiceAmountYearToDate = await ExecuteScalarDecimalAsync(connection,
            """
            SELECT COALESCE(SUM(total_amount), 0)
            FROM sales_invoices
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND COALESCE(is_deleted, 0) = 0
              AND issue_date >= @dateFrom
              AND issue_date < @dateToExclusive;
            """,
            tenantId,
            companyId,
            cancellationToken,
            yearToDateFrom,
            effectiveDateToExclusive);
        dto.PurchaseInvoiceAmountMonthToDate = await ExecuteScalarDecimalAsync(connection,
            $"""
            SELECT COALESCE(SUM(total_amount), 0)
            FROM purchase_invoices pi
            WHERE pi.tenant_id = @tenantId
              AND pi.company_id = @companyId
              AND COALESCE(pi.is_deleted, 0) = 0
              AND {purchaseInvoiceDateExpression} >= @dateFrom
              AND {purchaseInvoiceDateExpression} < @dateToExclusive;
            """,
            tenantId,
            companyId,
            cancellationToken,
            monthToDateFrom,
            effectiveDateToExclusive);
        dto.PurchaseInvoiceAmountYearToDate = await ExecuteScalarDecimalAsync(connection,
            $"""
            SELECT COALESCE(SUM(total_amount), 0)
            FROM purchase_invoices pi
            WHERE pi.tenant_id = @tenantId
              AND pi.company_id = @companyId
              AND COALESCE(pi.is_deleted, 0) = 0
              AND {purchaseInvoiceDateExpression} >= @dateFrom
              AND {purchaseInvoiceDateExpression} < @dateToExclusive;
            """,
            tenantId,
            companyId,
            cancellationToken,
            yearToDateFrom,
            effectiveDateToExclusive);
        dto.SalesCustomerMix = BuildDistribution(dto.TopClients, dto.SalesInvoiceAmount);
        dto.SalesItemMix = BuildDistribution(dto.TopBilledItems, dto.SalesInvoiceAmount);
        dto.SalesTopClientSharePercent = dto.SalesCustomerMix.FirstOrDefault()?.SharePercent ?? 0m;
        dto.SalesTop5ClientsSharePercent = CalculateRate(dto.SalesCustomerMix.Take(5).Sum(item => item.Amount), dto.SalesInvoiceAmount);
        dto.SalesAgingBuckets = await LoadBreakdownAsync(connection,
            $"""
            SELECT age_bucket AS label,
                   COUNT(*) AS item_count,
                   COALESCE(SUM(outstanding_amount), 0) AS total_amount
            FROM (
                SELECT si.outstanding_amount,
                       CASE
                           WHEN DATEDIFF(DATE('{analyticsReferenceDate:yyyy-MM-dd}'), DATE(COALESCE(si.due_date, si.issue_date))) <= 0 THEN 'aging_current'
                           WHEN DATEDIFF(DATE('{analyticsReferenceDate:yyyy-MM-dd}'), DATE(COALESCE(si.due_date, si.issue_date))) <= 30 THEN 'aging_1_30'
                           WHEN DATEDIFF(DATE('{analyticsReferenceDate:yyyy-MM-dd}'), DATE(COALESCE(si.due_date, si.issue_date))) <= 60 THEN 'aging_31_60'
                           WHEN DATEDIFF(DATE('{analyticsReferenceDate:yyyy-MM-dd}'), DATE(COALESCE(si.due_date, si.issue_date))) <= 90 THEN 'aging_61_90'
                           ELSE 'aging_90_plus'
                       END AS age_bucket
                FROM sales_invoices si
                WHERE si.tenant_id = @tenantId
                  AND si.company_id = @companyId
                  AND COALESCE(si.is_deleted, 0) = 0
                  AND COALESCE(si.outstanding_amount, 0) > 0
                  AND (@dateToExclusive IS NULL OR si.issue_date < @dateToExclusive)
            ) aged
            GROUP BY age_bucket
            ORDER BY CASE age_bucket
                WHEN 'aging_current' THEN 1
                WHEN 'aging_1_30' THEN 2
                WHEN 'aging_31_60' THEN 3
                WHEN 'aging_61_90' THEN 4
                ELSE 5
            END;
            """,
            tenantId,
            companyId,
            cancellationToken,
            null,
            effectiveDateToExclusive);
        var salesOutstandingRiskBreakdown = await LoadBreakdownAsync(connection,
            """
            SELECT COALESCE(NULLIF(client_name, ''), 'Cliente sin nombre') AS label,
                   COUNT(*) AS item_count,
                   COALESCE(SUM(outstanding_amount), 0) AS total_amount
            FROM sales_invoices
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND COALESCE(is_deleted, 0) = 0
              AND COALESCE(outstanding_amount, 0) > 0
              AND (@dateToExclusive IS NULL OR issue_date < @dateToExclusive)
            GROUP BY COALESCE(NULLIF(client_name, ''), 'Cliente sin nombre')
            ORDER BY total_amount DESC, item_count DESC, label
            LIMIT 8;
            """,
            tenantId,
            companyId,
            cancellationToken,
            null,
            effectiveDateToExclusive);
        var salesOutstandingExposureAmount = salesOutstandingRiskBreakdown.Sum(item => item.Amount);
        dto.SalesOutstandingRiskByClient = BuildDistribution(salesOutstandingRiskBreakdown, salesOutstandingExposureAmount);
        dto.SalesOutstandingTopClientSharePercent = dto.SalesOutstandingRiskByClient.FirstOrDefault()?.SharePercent ?? 0m;
        dto.SalesOutstandingTop5ClientsSharePercent = CalculateRate(dto.SalesOutstandingRiskByClient.Take(5).Sum(item => item.Amount), salesOutstandingExposureAmount);
        dto.PurchaseAgingBuckets = await LoadBreakdownAsync(connection,
            $"""
            SELECT age_bucket AS label,
                   COUNT(*) AS item_count,
                   COALESCE(SUM(outstanding_amount), 0) AS total_amount
            FROM (
                SELECT pi.outstanding_amount,
                       CASE
                           WHEN DATEDIFF(DATE('{analyticsReferenceDate:yyyy-MM-dd}'), DATE(COALESCE(pi.due_date, {purchaseInvoiceDateExpression}))) <= 0 THEN 'aging_current'
                           WHEN DATEDIFF(DATE('{analyticsReferenceDate:yyyy-MM-dd}'), DATE(COALESCE(pi.due_date, {purchaseInvoiceDateExpression}))) <= 30 THEN 'aging_1_30'
                           WHEN DATEDIFF(DATE('{analyticsReferenceDate:yyyy-MM-dd}'), DATE(COALESCE(pi.due_date, {purchaseInvoiceDateExpression}))) <= 60 THEN 'aging_31_60'
                           WHEN DATEDIFF(DATE('{analyticsReferenceDate:yyyy-MM-dd}'), DATE(COALESCE(pi.due_date, {purchaseInvoiceDateExpression}))) <= 90 THEN 'aging_61_90'
                           ELSE 'aging_90_plus'
                       END AS age_bucket
                FROM purchase_invoices pi
                WHERE pi.tenant_id = @tenantId
                  AND pi.company_id = @companyId
                  AND COALESCE(pi.is_deleted, 0) = 0
                  AND COALESCE(pi.outstanding_amount, 0) > 0
                  AND (@dateToExclusive IS NULL OR {purchaseInvoiceDateExpression} < @dateToExclusive)
            ) aged
            GROUP BY age_bucket
            ORDER BY CASE age_bucket
                WHEN 'aging_current' THEN 1
                WHEN 'aging_1_30' THEN 2
                WHEN 'aging_31_60' THEN 3
                WHEN 'aging_61_90' THEN 4
                ELSE 5
            END;
            """,
            tenantId,
            companyId,
            cancellationToken,
            null,
            effectiveDateToExclusive);
        dto.PurchaseReceiptInvoiceGaps = await LoadGapAsync(connection,
            $"""
            SELECT supplier_label AS label,
                   COALESCE(SUM(receipt_amount), 0) AS expected_value,
                   COALESCE(SUM(invoice_amount), 0) AS actual_value,
                   COALESCE(SUM(invoice_amount), 0) - COALESCE(SUM(receipt_amount), 0) AS gap_value
            FROM (
                SELECT COALESCE(NULLIF(po.supplier_name, ''), 'Proveedor sin nombre') AS supplier_label,
                       COALESCE(SUM(prl.received_quantity * COALESCE(pol.unit_price, 0)), 0) AS receipt_amount,
                       0 AS invoice_amount
                FROM purchase_order_receipts pr
                INNER JOIN purchase_orders po
                  ON po.tenant_id = pr.tenant_id
                 AND po.company_id = pr.company_id
                 AND po.order_number = pr.order_number
                LEFT JOIN purchase_order_receipt_lines prl
                  ON prl.receipt_id = pr.receipt_id
                 AND prl.tenant_id = pr.tenant_id
                 AND prl.company_id = pr.company_id
                LEFT JOIN purchase_order_lines pol
                  ON pol.tenant_id = prl.tenant_id
                 AND pol.company_id = prl.company_id
                 AND pol.order_number = prl.order_number
                 AND pol.line_number = prl.line_number
                WHERE pr.tenant_id = @tenantId
                  AND pr.company_id = @companyId
                  AND COALESCE(pr.is_deleted, 0) = 0
                  AND COALESCE(po.is_deleted, 0) = 0
                  AND (@dateFrom IS NULL OR pr.receipt_date >= @dateFrom)
                  AND (@dateToExclusive IS NULL OR pr.receipt_date < @dateToExclusive)
                GROUP BY COALESCE(NULLIF(po.supplier_name, ''), 'Proveedor sin nombre')

                UNION ALL

                SELECT COALESCE(NULLIF(pi.supplier_name, ''), 'Proveedor sin nombre') AS supplier_label,
                       0 AS receipt_amount,
                       COALESCE(SUM(pi.total_amount), 0) AS invoice_amount
                FROM purchase_invoices pi
                WHERE pi.tenant_id = @tenantId
                  AND pi.company_id = @companyId
                  AND COALESCE(pi.is_deleted, 0) = 0
                  AND (@dateFrom IS NULL OR {purchaseInvoiceDateExpression} >= @dateFrom)
                  AND (@dateToExclusive IS NULL OR {purchaseInvoiceDateExpression} < @dateToExclusive)
                GROUP BY COALESCE(NULLIF(pi.supplier_name, ''), 'Proveedor sin nombre')
            ) supplier_flows
            GROUP BY supplier_label
            ORDER BY ABS(COALESCE(SUM(invoice_amount), 0) - COALESCE(SUM(receipt_amount), 0)) DESC,
                     COALESCE(SUM(invoice_amount), 0) DESC,
                     supplier_label
            LIMIT 8;
            """,
            tenantId,
            companyId,
            cancellationToken,
            dateFrom,
            dateToExclusive);
        dto.ProductionLoadByFinisher = dto.TopFinishers;
        dto.ProductionLoadByWeek = await LoadBreakdownAsync(connection,
            """
            SELECT DATE_FORMAT(week_start, '%d/%m') AS label,
                   SUM(order_count) AS item_count,
                   COALESCE(SUM(total_kilograms), 0) AS total_amount
            FROM (
                SELECT DATE_SUB(DATE(work_date), INTERVAL WEEKDAY(DATE(work_date)) DAY) AS week_start,
                       COUNT(*) AS order_count,
                       COALESCE(SUM(total_kilograms), 0) AS total_kilograms
                FROM finish_work_orders
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND COALESCE(is_deleted, 0) = 0
                  AND (@dateFrom IS NULL OR work_date >= @dateFrom)
                  AND (@dateToExclusive IS NULL OR work_date < @dateToExclusive)
                GROUP BY DATE_SUB(DATE(work_date), INTERVAL WEEKDAY(DATE(work_date)) DAY)
            ) weekly_load
            GROUP BY week_start
            ORDER BY week_start DESC
            LIMIT 8;
            """,
            tenantId,
            companyId,
            cancellationToken,
            dateFrom,
            dateToExclusive);
        dto.WarehouseRotationByWarehouse = await LoadBreakdownAsync(connection,
            """
            SELECT COALESCE(NULLIF(warehouse, ''), 'Sin almacén') AS label,
                   COUNT(DISTINCT CONCAT_WS('|', COALESCE(NULLIF(item_code, ''), '?'), COALESCE(NULLIF(item_description, ''), '?'), COALESCE(NULLIF(unit_of_measure, ''), '?'))) AS item_count,
                   COALESCE(SUM(ABS(quantity)), 0) AS total_amount
            FROM inventory_movements
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND (@dateFrom IS NULL OR movement_date >= @dateFrom)
              AND (@dateToExclusive IS NULL OR movement_date < @dateToExclusive)
            GROUP BY COALESCE(NULLIF(warehouse, ''), 'Sin almacén')
            ORDER BY total_amount DESC, item_count DESC, label
            LIMIT 8;
            """,
            tenantId,
            companyId,
            cancellationToken,
            dateFrom,
            dateToExclusive);
        dto.WarehouseStockAgeBuckets = await LoadBreakdownAsync(connection,
            """
            SELECT age_bucket AS label,
                   COUNT(*) AS item_count,
                   COALESCE(SUM(ABS(balance_quantity)), 0) AS total_amount
            FROM (
                SELECT CASE
                           WHEN DATEDIFF(CURDATE(), last_movement_date) <= 30 THEN 'stock_age_0_30'
                           WHEN DATEDIFF(CURDATE(), last_movement_date) <= 90 THEN 'stock_age_31_90'
                           WHEN DATEDIFF(CURDATE(), last_movement_date) <= 180 THEN 'stock_age_91_180'
                           ELSE 'stock_age_180_plus'
                       END AS age_bucket,
                       balance_quantity
                FROM (
                    SELECT MAX(DATE(movement_date)) AS last_movement_date,
                           COALESCE(SUM(CASE WHEN movement_type LIKE 'Inbound%' THEN quantity ELSE -quantity END), 0) AS balance_quantity
                    FROM inventory_movements
                    WHERE tenant_id = @tenantId
                      AND company_id = @companyId
                    GROUP BY warehouse, item_code, item_description, unit_of_measure
                    HAVING COALESCE(SUM(CASE WHEN movement_type LIKE 'Inbound%' THEN quantity ELSE -quantity END), 0) <> 0
                ) live_positions
            ) aged_stock
            GROUP BY age_bucket
            ORDER BY CASE age_bucket
                WHEN 'stock_age_0_30' THEN 1
                WHEN 'stock_age_31_90' THEN 2
                WHEN 'stock_age_91_180' THEN 3
                ELSE 4
            END;
            """,
            tenantId,
            companyId,
            cancellationToken);
        dto.WarehouseCoverageBuckets = await LoadBreakdownAsync(connection,
            $"""
            SELECT coverage_bucket AS label,
                   COUNT(*) AS item_count,
                   COALESCE(SUM(ABS(balance_quantity)), 0) AS total_amount
            FROM (
                SELECT balance_quantity,
                       CASE
                           WHEN outbound_quantity <= 0 THEN 'coverage_none'
                           WHEN ((ABS(balance_quantity) * {rangeDays}) / outbound_quantity) <= 30 THEN 'coverage_0_30'
                           WHEN ((ABS(balance_quantity) * {rangeDays}) / outbound_quantity) <= 90 THEN 'coverage_31_90'
                           ELSE 'coverage_90_plus'
                       END AS coverage_bucket
                FROM (
                    SELECT live_positions.warehouse,
                           live_positions.item_code,
                           live_positions.item_description,
                           live_positions.unit_of_measure,
                           live_positions.balance_quantity,
                           COALESCE(range_usage.outbound_quantity, 0) AS outbound_quantity
                    FROM (
                        SELECT warehouse,
                               item_code,
                               item_description,
                               unit_of_measure,
                               COALESCE(SUM(CASE WHEN movement_type LIKE 'Inbound%' THEN quantity ELSE -quantity END), 0) AS balance_quantity
                        FROM inventory_movements
                        WHERE tenant_id = @tenantId
                          AND company_id = @companyId
                        GROUP BY warehouse, item_code, item_description, unit_of_measure
                        HAVING COALESCE(SUM(CASE WHEN movement_type LIKE 'Inbound%' THEN quantity ELSE -quantity END), 0) <> 0
                    ) live_positions
                    LEFT JOIN (
                        SELECT warehouse,
                               item_code,
                               item_description,
                               unit_of_measure,
                               COALESCE(SUM(CASE WHEN movement_type NOT LIKE 'Inbound%' THEN ABS(quantity) ELSE 0 END), 0) AS outbound_quantity
                        FROM inventory_movements
                        WHERE tenant_id = @tenantId
                          AND company_id = @companyId
                          AND (@dateFrom IS NULL OR movement_date >= @dateFrom)
                          AND (@dateToExclusive IS NULL OR movement_date < @dateToExclusive)
                        GROUP BY warehouse, item_code, item_description, unit_of_measure
                    ) range_usage
                      ON range_usage.warehouse <=> live_positions.warehouse
                     AND range_usage.item_code <=> live_positions.item_code
                     AND range_usage.item_description <=> live_positions.item_description
                     AND range_usage.unit_of_measure <=> live_positions.unit_of_measure
                ) coverage_source
            ) coverage_buckets
            GROUP BY coverage_bucket
            ORDER BY CASE coverage_bucket
                WHEN 'coverage_none' THEN 1
                WHEN 'coverage_0_30' THEN 2
                WHEN 'coverage_31_90' THEN 3
                ELSE 4
            END;
            """,
            tenantId,
            companyId,
            cancellationToken,
            dateFrom,
            dateToExclusive);

        var previousSalesInvoiceAmount = await ExecuteScalarDecimalAsync(connection,
            """
            SELECT COALESCE(SUM(total_amount), 0)
            FROM sales_invoices
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND COALESCE(is_deleted, 0) = 0
              AND issue_date >= @dateFrom
              AND issue_date < @dateToExclusive;
            """,
            tenantId,
            companyId,
            cancellationToken,
            previousDateFrom,
            previousDateToExclusive);
        var previousPurchaseInvoiceAmount = await ExecuteScalarDecimalAsync(connection,
            $"""
            SELECT COALESCE(SUM(total_amount), 0)
            FROM purchase_invoices pi
            WHERE pi.tenant_id = @tenantId
              AND pi.company_id = @companyId
              AND COALESCE(pi.is_deleted, 0) = 0
              AND {purchaseInvoiceDateExpression} >= @dateFrom
              AND {purchaseInvoiceDateExpression} < @dateToExclusive;
            """,
            tenantId,
            companyId,
            cancellationToken,
            previousDateFrom,
            previousDateToExclusive);
        var previousSalesOutstandingAmount = await ExecuteScalarDecimalAsync(connection,
            """
            SELECT COALESCE(SUM(outstanding_amount), 0)
            FROM sales_invoices
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND COALESCE(is_deleted, 0) = 0
              AND issue_date >= @dateFrom
              AND issue_date < @dateToExclusive;
            """,
            tenantId,
            companyId,
            cancellationToken,
            previousDateFrom,
            previousDateToExclusive);
        var previousPurchaseOutstandingAmount = await ExecuteScalarDecimalAsync(connection,
            $"""
            SELECT COALESCE(SUM(outstanding_amount), 0)
            FROM purchase_invoices pi
            WHERE pi.tenant_id = @tenantId
              AND pi.company_id = @companyId
              AND COALESCE(pi.is_deleted, 0) = 0
              AND {purchaseInvoiceDateExpression} >= @dateFrom
              AND {purchaseInvoiceDateExpression} < @dateToExclusive;
            """,
            tenantId,
            companyId,
            cancellationToken,
            previousDateFrom,
            previousDateToExclusive);
        var previousStockMovements = await ExecuteScalarIntAsync(connection,
            """
            SELECT COUNT(*)
            FROM inventory_movements
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND movement_date >= @dateFrom
              AND movement_date < @dateToExclusive;
            """,
            tenantId,
            companyId,
            cancellationToken,
            previousDateFrom,
            previousDateToExclusive);
        var previousLiveFinishOrders = await ExecuteScalarIntAsync(connection,
            """
            SELECT COUNT(*)
            FROM finish_work_orders
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND COALESCE(is_deleted, 0) = 0
              AND status IN ('Pending', 'InProgress')
              AND work_date >= @dateFrom
              AND work_date < @dateToExclusive;
            """,
            tenantId,
            companyId,
            cancellationToken,
            previousDateFrom,
            previousDateToExclusive);

        dto.PeriodComparisons =
        [
            BuildComparisonItem("Facturación ventas", dto.SalesInvoiceAmount, previousSalesInvoiceAmount, "currency"),
            BuildComparisonItem("Facturación compras", dto.PurchaseInvoiceAmount, previousPurchaseInvoiceAmount, "currency"),
            BuildComparisonItem("Pendiente cobro", dto.SalesOutstandingAmount, previousSalesOutstandingAmount, "currency"),
            BuildComparisonItem("Pendiente pago", dto.PurchaseOutstandingAmount, previousPurchaseOutstandingAmount, "currency"),
            BuildComparisonItem("Movimientos stock", dto.StockMovementsInRange, previousStockMovements, "count"),
            BuildComparisonItem("Órdenes vivas", dto.LiveFinishOrders, previousLiveFinishOrders, "count")
        ];

        return dto;
    }

    private static string BuildOperationalDocumentUnionSql(
        bool includeOrderBy,
        string salesOrderDateExpression,
        string purchaseOrderDateExpression,
        string purchaseInvoiceDateExpression,
        string finishWorkOrderDateExpression)
    {
        var sql =
            $"""
            SELECT 'Sales' AS category,
                   'SalesOrder' AS type_key,
                   'Pedido venta' AS type_label,
                   so.order_number AS document_number,
                   CAST(so.order_number AS CHAR) AS document_display,
                   {salesOrderDateExpression} AS document_date,
                   COALESCE(so.client_name, '') AS party_name,
                   COALESCE(so.status, '') AS status,
                   COALESCE(SUM(sol.quantity * sol.unit_price), 0) AS amount
            FROM sales_orders so
            LEFT JOIN sales_order_lines sol
              ON sol.tenant_id = so.tenant_id
             AND sol.company_id = so.company_id
             AND sol.order_number = so.order_number
            WHERE so.tenant_id = @tenantId
              AND so.company_id = @companyId
            GROUP BY so.order_number, {salesOrderDateExpression}, so.client_name, so.status

            UNION ALL

            SELECT 'Sales' AS category,
                   'SalesShipment' AS type_key,
                   'Albarán venta' AS type_label,
                   ss.shipment_number AS document_number,
                   CAST(ss.shipment_number AS CHAR) AS document_display,
                   ss.shipment_date AS document_date,
                   COALESCE(so.client_name, '') AS party_name,
                   COALESCE(ss.invoice_status, '') AS status,
                   COALESCE(SUM(shl.shipped_quantity * COALESCE(sol.unit_price, 0)), 0) AS amount
            FROM sales_order_shipments ss
            LEFT JOIN sales_orders so
              ON so.tenant_id = ss.tenant_id
             AND so.company_id = ss.company_id
             AND so.order_number = ss.order_number
            LEFT JOIN sales_order_shipment_lines shl
              ON shl.shipment_id = ss.shipment_id
            LEFT JOIN sales_order_lines sol
              ON sol.tenant_id = shl.tenant_id
             AND sol.company_id = shl.company_id
             AND sol.order_number = shl.order_number
             AND sol.line_number = shl.line_number
            WHERE ss.tenant_id = @tenantId
              AND ss.company_id = @companyId
              AND COALESCE(ss.is_deleted, 0) = 0
            GROUP BY ss.shipment_id, ss.shipment_number, ss.shipment_date, so.client_name, ss.invoice_status

            UNION ALL

            SELECT 'Sales' AS category,
                   'SalesInvoice' AS type_key,
                   'Factura venta' AS type_label,
                   si.invoice_number AS document_number,
                   CAST(si.invoice_number AS CHAR) AS document_display,
                   si.issue_date AS document_date,
                   COALESCE(si.client_name, '') AS party_name,
                   COALESCE(si.status, '') AS status,
                   COALESCE(si.total_amount, 0) AS amount
            FROM sales_invoices si
            WHERE si.tenant_id = @tenantId
              AND si.company_id = @companyId
              AND COALESCE(si.is_deleted, 0) = 0

            UNION ALL

            SELECT 'Purchases' AS category,
                   'PurchaseOrder' AS type_key,
                   'Pedido compra' AS type_label,
                   po.order_number AS document_number,
                   CAST(po.order_number AS CHAR) AS document_display,
                   {purchaseOrderDateExpression} AS document_date,
                   COALESCE(po.supplier_name, '') AS party_name,
                   COALESCE(po.status, '') AS status,
                   COALESCE(SUM(pol.quantity * pol.unit_price), 0) AS amount
            FROM purchase_orders po
            LEFT JOIN purchase_order_lines pol
              ON pol.tenant_id = po.tenant_id
             AND pol.company_id = po.company_id
             AND pol.order_number = po.order_number
            WHERE po.tenant_id = @tenantId
              AND po.company_id = @companyId
              AND COALESCE(po.is_deleted, 0) = 0
            GROUP BY po.order_number, {purchaseOrderDateExpression}, po.supplier_name, po.status

            UNION ALL

            SELECT 'Purchases' AS category,
                   'PurchaseReceipt' AS type_key,
                   'Recepción compra' AS type_label,
                   pr.receipt_number AS document_number,
                   CAST(pr.receipt_number AS CHAR) AS document_display,
                   pr.receipt_date AS document_date,
                   COALESCE(po.supplier_name, '') AS party_name,
                   COALESCE(pr.warehouse, '') AS status,
                   COALESCE(SUM(prl.received_quantity), 0) AS amount
            FROM purchase_order_receipts pr
            LEFT JOIN purchase_orders po
              ON po.tenant_id = pr.tenant_id
             AND po.company_id = pr.company_id
             AND po.order_number = pr.order_number
            LEFT JOIN purchase_order_receipt_lines prl
              ON prl.tenant_id = pr.tenant_id
             AND prl.company_id = pr.company_id
             AND prl.receipt_id = pr.receipt_id
            WHERE pr.tenant_id = @tenantId
              AND pr.company_id = @companyId
              AND COALESCE(pr.is_deleted, 0) = 0
            GROUP BY pr.receipt_number, pr.receipt_date, po.supplier_name, pr.warehouse

            UNION ALL

            SELECT 'Purchases' AS category,
                   'PurchaseInvoice' AS type_key,
                   'Factura proveedor' AS type_label,
                   pi.invoice_number AS document_number,
                   CAST(pi.invoice_number AS CHAR) AS document_display,
                   {purchaseInvoiceDateExpression} AS document_date,
                   COALESCE(pi.supplier_name, '') AS party_name,
                   COALESCE(pi.status, '') AS status,
                   COALESCE(pi.total_amount, 0) AS amount
            FROM purchase_invoices pi
            WHERE pi.tenant_id = @tenantId
              AND pi.company_id = @companyId
              AND COALESCE(pi.is_deleted, 0) = 0

            UNION ALL

            SELECT 'Production' AS category,
                   'FinishWorkOrder' AS type_key,
                   'Orden fabricación' AS type_label,
                   fwo.order_number AS document_number,
                   CAST(fwo.order_number AS CHAR) AS document_display,
                   {finishWorkOrderDateExpression} AS document_date,
                   COALESCE(fwo.client_name, '') AS party_name,
                   COALESCE(fwo.status, '') AS status,
                   COALESCE((SELECT COUNT(*) FROM finish_work_order_lines fwol WHERE fwol.order_id = fwo.order_id), 0) AS amount
            FROM finish_work_orders fwo
            WHERE fwo.tenant_id = @tenantId
              AND fwo.company_id = @companyId
              AND COALESCE(fwo.is_deleted, 0) = 0

            UNION ALL

            SELECT 'Finance' AS category,
                   'SalesRemittance' AS type_key,
                   'Remesa cliente' AS type_label,
                   sr.remittance_number AS document_number,
                   CAST(sr.remittance_number AS CHAR) AS document_display,
                   sr.remittance_date AS document_date,
                   COALESCE(sr.bank_name, '') AS party_name,
                   COALESCE(sr.status, '') AS status,
                   COALESCE(sr.total_amount, 0) AS amount
            FROM sales_remittances sr
            WHERE sr.tenant_id = @tenantId
              AND sr.company_id = @companyId
              AND COALESCE(sr.is_deleted, 0) = 0

            UNION ALL

            SELECT 'Warehouse' AS category,
                   'StockTransfer' AS type_key,
                   'Traspaso almacén' AS type_label,
                   st.transfer_number AS document_number,
                   CAST(st.transfer_number AS CHAR) AS document_display,
                   st.transfer_date AS document_date,
                   CONCAT(COALESCE(st.from_warehouse, ''), ' -> ', COALESCE(st.to_warehouse, '')) AS party_name,
                   COALESCE(st.status, '') AS status,
                   COALESCE(st.total_quantity, 0) AS amount
            FROM stock_transfers st
            WHERE st.tenant_id = @tenantId
              AND st.company_id = @companyId
              AND COALESCE(st.is_deleted, 0) = 0

            UNION ALL

            SELECT 'Warehouse' AS category,
                   'StockCount' AS type_key,
                   'Inventario' AS type_label,
                   sc.count_number AS document_number,
                   CAST(sc.count_number AS CHAR) AS document_display,
                   sc.count_date AS document_date,
                   COALESCE(sc.warehouse, '') AS party_name,
                   COALESCE(sc.status, '') AS status,
                   ABS(COALESCE(sc.difference_total_quantity, 0)) AS amount
            FROM stock_counts sc
            WHERE sc.tenant_id = @tenantId
              AND sc.company_id = @companyId
              AND COALESCE(sc.is_deleted, 0) = 0
            """;

        if (includeOrderBy)
        {
            return $"{sql}{Environment.NewLine}ORDER BY document_date DESC, document_number DESC";
        }

        return sql;
    }

    private static void FillOperationalDocumentParameters(
        MySqlCommand command,
        Guid tenantId,
        Guid companyId,
        string category,
        string typeKey,
        string search,
        string likeSearch,
        DateTime? dateFrom,
        DateTime? dateToExclusive)
    {
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@category", category);
        command.Parameters.AddWithValue("@typeKey", typeKey);
        command.Parameters.AddWithValue("@search", search);
        command.Parameters.AddWithValue("@likeSearch", likeSearch);
        command.Parameters.AddWithValue("@dateFrom", dateFrom);
        command.Parameters.AddWithValue("@dateToExclusive", dateToExclusive);
    }

    private static string BuildOperationalDocumentOrderByClause(OperationalDocumentFilter filter)
    {
        var column = filter.SortColumn switch
        {
            nameof(OperationalDocumentListItemDto.TypeLabel) => "docs.type_label",
            nameof(OperationalDocumentListItemDto.DocumentDisplay) => "docs.document_number",
            nameof(OperationalDocumentListItemDto.DocumentDate) => "docs.document_date",
            nameof(OperationalDocumentListItemDto.PartyName) => "docs.party_name",
            nameof(OperationalDocumentListItemDto.Status) => "docs.status",
            nameof(OperationalDocumentListItemDto.Amount) => "docs.amount",
            _ => "docs.document_date"
        };

        var direction = filter.SortDescending ? "DESC" : "ASC";
        return $"ORDER BY {column} {direction}, docs.document_number DESC";
    }

    private static string NormalizeCategory(string? category) =>
        (category ?? string.Empty).Trim() switch
        {
            "Sales" => "Sales",
            "Purchases" => "Purchases",
            "Production" => "Production",
            "Finance" => "Finance",
            "Warehouse" => "Warehouse",
            _ => string.Empty
        };

    private static string NormalizeTypeKey(string? typeKey) =>
        (typeKey ?? string.Empty).Trim() switch
        {
            "SalesOrder" => "SalesOrder",
            "SalesShipment" => "SalesShipment",
            "SalesInvoice" => "SalesInvoice",
            "PurchaseOrder" => "PurchaseOrder",
            "PurchaseReceipt" => "PurchaseReceipt",
            "PurchaseInvoice" => "PurchaseInvoice",
            "FinishWorkOrder" => "FinishWorkOrder",
            "SalesRemittance" => "SalesRemittance",
            "StockTransfer" => "StockTransfer",
            "StockCount" => "StockCount",
            _ => string.Empty
        };

    private static string BuildDocumentRoute(string typeKey, int documentNumber) =>
        typeKey switch
        {
            "SalesOrder" => $"/ventas/pedidos/{documentNumber}",
            "SalesShipment" => $"/ventas/albaranes/{documentNumber}",
            "SalesInvoice" => $"/ventas/facturas/{documentNumber}",
            "PurchaseOrder" => $"/compras/pedidos/{documentNumber}",
            "PurchaseReceipt" => $"/compras/recepciones/{documentNumber}",
            "PurchaseInvoice" => $"/compras/facturas/{documentNumber}",
            "FinishWorkOrder" => $"/produccion/acabados/editar/{documentNumber}",
            "SalesRemittance" => $"/ventas/remesas/editar/{documentNumber}",
            "StockTransfer" => $"/almacen/traspasos/editar/{documentNumber}",
            "StockCount" => $"/almacen/inventarios/editar/{documentNumber}",
            _ => "/"
        };

    private static async Task<int> ExecuteScalarIntAsync(
        MySqlConnection connection,
        string sql,
        string centerCode,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        return Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));
    }

    private static async Task<int> ExecuteScalarIntAsync(
        MySqlConnection connection,
        string sql,
        Guid tenantId,
        Guid companyId,
        CancellationToken cancellationToken,
        DateTime? dateFrom = null,
        DateTime? dateToExclusive = null)
    {
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        if (sql.Contains("@dateFrom", StringComparison.Ordinal))
        {
            command.Parameters.AddWithValue("@dateFrom", dateFrom);
        }

        if (sql.Contains("@dateToExclusive", StringComparison.Ordinal))
        {
            command.Parameters.AddWithValue("@dateToExclusive", dateToExclusive);
        }

        return Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));
    }

    private static async Task<decimal> ExecuteScalarDecimalAsync(
        MySqlConnection connection,
        string sql,
        Guid tenantId,
        Guid companyId,
        CancellationToken cancellationToken,
        DateTime? dateFrom = null,
        DateTime? dateToExclusive = null)
    {
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        if (sql.Contains("@dateFrom", StringComparison.Ordinal))
        {
            command.Parameters.AddWithValue("@dateFrom", dateFrom);
        }

        if (sql.Contains("@dateToExclusive", StringComparison.Ordinal))
        {
            command.Parameters.AddWithValue("@dateToExclusive", dateToExclusive);
        }

        var value = await command.ExecuteScalarAsync(cancellationToken);
        return value is null || value is DBNull ? 0m : Convert.ToDecimal(value);
    }

    private static async Task<IReadOnlyCollection<StatisticBreakdownItemDto>> LoadBreakdownAsync(
        MySqlConnection connection,
        string sql,
        Guid tenantId,
        Guid companyId,
        CancellationToken cancellationToken,
        DateTime? dateFrom = null,
        DateTime? dateToExclusive = null)
    {
        var items = new List<StatisticBreakdownItemDto>();

        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        if (sql.Contains("@dateFrom", StringComparison.Ordinal))
        {
            command.Parameters.AddWithValue("@dateFrom", dateFrom);
        }

        if (sql.Contains("@dateToExclusive", StringComparison.Ordinal))
        {
            command.Parameters.AddWithValue("@dateToExclusive", dateToExclusive);
        }

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new StatisticBreakdownItemDto
            {
                Label = reader.GetStringOrEmpty("label"),
                Count = reader.GetInt32OrDefault("item_count"),
                Amount = reader.GetDecimalOrDefault("total_amount")
            });
        }

        return items;
    }

    private static async Task<IReadOnlyCollection<StatisticGapItemDto>> LoadGapAsync(
        MySqlConnection connection,
        string sql,
        Guid tenantId,
        Guid companyId,
        CancellationToken cancellationToken,
        DateTime? dateFrom = null,
        DateTime? dateToExclusive = null)
    {
        var items = new List<StatisticGapItemDto>();

        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        if (sql.Contains("@dateFrom", StringComparison.Ordinal))
        {
            command.Parameters.AddWithValue("@dateFrom", dateFrom);
        }

        if (sql.Contains("@dateToExclusive", StringComparison.Ordinal))
        {
            command.Parameters.AddWithValue("@dateToExclusive", dateToExclusive);
        }

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new StatisticGapItemDto
            {
                Label = reader.GetStringOrEmpty("label"),
                ExpectedValue = reader.GetDecimalOrDefault("expected_value"),
                ActualValue = reader.GetDecimalOrDefault("actual_value"),
                GapValue = reader.GetDecimalOrDefault("gap_value")
            });
        }

        return items;
    }

    private static IReadOnlyCollection<StatisticDistributionItemDto> BuildDistribution(
        IReadOnlyCollection<StatisticBreakdownItemDto> items,
        decimal totalAmount)
    {
        if (items.Count == 0)
        {
            return [];
        }

        return items
            .Select(item => new StatisticDistributionItemDto
            {
                Label = item.Label,
                Count = item.Count,
                Amount = item.Amount,
                SharePercent = CalculateRate(item.Amount, totalAmount)
            })
            .ToArray();
    }

    private static async Task<IReadOnlyCollection<StatisticTimelinePointDto>> LoadWeeklyTimelineAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        CancellationToken cancellationToken,
        DateTime? dateFrom,
        DateTime? dateToExclusive,
        string purchaseInvoiceDateExpression,
        string finishWorkOrderDateExpression)
    {
        var items = new List<StatisticTimelinePointDto>();

        await using var command = connection.CreateCommand();
        command.CommandText =
            $"""
            SELECT bucket_start,
                   DATE_FORMAT(bucket_start, '%d/%m') AS label,
                   SUM(sales_invoice_count) AS sales_invoice_count,
                   SUM(sales_invoice_amount) AS sales_invoice_amount,
                   SUM(purchase_invoice_count) AS purchase_invoice_count,
                   SUM(purchase_invoice_amount) AS purchase_invoice_amount,
                   SUM(stock_movement_count) AS stock_movement_count,
                   SUM(finish_order_count) AS finish_order_count
            FROM (
                SELECT DATE_SUB(DATE(si.issue_date), INTERVAL WEEKDAY(DATE(si.issue_date)) DAY) AS bucket_start,
                       COUNT(*) AS sales_invoice_count,
                       COALESCE(SUM(si.total_amount), 0) AS sales_invoice_amount,
                       0 AS purchase_invoice_count,
                       0 AS purchase_invoice_amount,
                       0 AS stock_movement_count,
                       0 AS finish_order_count
                FROM sales_invoices si
                WHERE si.tenant_id = @tenantId
                  AND si.company_id = @companyId
                  AND COALESCE(si.is_deleted, 0) = 0
                  AND (@dateFrom IS NULL OR si.issue_date >= @dateFrom)
                  AND (@dateToExclusive IS NULL OR si.issue_date < @dateToExclusive)
                GROUP BY DATE_SUB(DATE(si.issue_date), INTERVAL WEEKDAY(DATE(si.issue_date)) DAY)

                UNION ALL

                SELECT DATE_SUB(DATE({purchaseInvoiceDateExpression}), INTERVAL WEEKDAY(DATE({purchaseInvoiceDateExpression})) DAY) AS bucket_start,
                       0 AS sales_invoice_count,
                       0 AS sales_invoice_amount,
                       COUNT(*) AS purchase_invoice_count,
                       COALESCE(SUM(pi.total_amount), 0) AS purchase_invoice_amount,
                       0 AS stock_movement_count,
                       0 AS finish_order_count
                FROM purchase_invoices pi
                WHERE pi.tenant_id = @tenantId
                  AND pi.company_id = @companyId
                  AND COALESCE(pi.is_deleted, 0) = 0
                  AND (@dateFrom IS NULL OR {purchaseInvoiceDateExpression} >= @dateFrom)
                  AND (@dateToExclusive IS NULL OR {purchaseInvoiceDateExpression} < @dateToExclusive)
                GROUP BY DATE_SUB(DATE({purchaseInvoiceDateExpression}), INTERVAL WEEKDAY(DATE({purchaseInvoiceDateExpression})) DAY)

                UNION ALL

                SELECT DATE_SUB(DATE(im.movement_date), INTERVAL WEEKDAY(DATE(im.movement_date)) DAY) AS bucket_start,
                       0 AS sales_invoice_count,
                       0 AS sales_invoice_amount,
                       0 AS purchase_invoice_count,
                       0 AS purchase_invoice_amount,
                       COUNT(*) AS stock_movement_count,
                       0 AS finish_order_count
                FROM inventory_movements im
                WHERE im.tenant_id = @tenantId
                  AND im.company_id = @companyId
                  AND (@dateFrom IS NULL OR im.movement_date >= @dateFrom)
                  AND (@dateToExclusive IS NULL OR im.movement_date < @dateToExclusive)
                GROUP BY DATE_SUB(DATE(im.movement_date), INTERVAL WEEKDAY(DATE(im.movement_date)) DAY)

                UNION ALL

                SELECT DATE_SUB(DATE({finishWorkOrderDateExpression}), INTERVAL WEEKDAY(DATE({finishWorkOrderDateExpression})) DAY) AS bucket_start,
                       0 AS sales_invoice_count,
                       0 AS sales_invoice_amount,
                       0 AS purchase_invoice_count,
                       0 AS purchase_invoice_amount,
                       0 AS stock_movement_count,
                       COUNT(*) AS finish_order_count
                FROM finish_work_orders fwo
                WHERE fwo.tenant_id = @tenantId
                  AND fwo.company_id = @companyId
                  AND COALESCE(fwo.is_deleted, 0) = 0
                  AND (@dateFrom IS NULL OR {finishWorkOrderDateExpression} >= @dateFrom)
                  AND (@dateToExclusive IS NULL OR {finishWorkOrderDateExpression} < @dateToExclusive)
                GROUP BY DATE_SUB(DATE({finishWorkOrderDateExpression}), INTERVAL WEEKDAY(DATE({finishWorkOrderDateExpression})) DAY)
            ) buckets
            GROUP BY bucket_start
            ORDER BY bucket_start;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@dateFrom", dateFrom);
        command.Parameters.AddWithValue("@dateToExclusive", dateToExclusive);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new StatisticTimelinePointDto
            {
                BucketStart = reader.GetDateTime(reader.GetOrdinal("bucket_start")),
                Label = reader.GetStringOrEmpty("label"),
                SalesInvoiceCount = reader.GetInt32OrDefault("sales_invoice_count"),
                SalesInvoiceAmount = reader.GetDecimalOrDefault("sales_invoice_amount"),
                PurchaseInvoiceCount = reader.GetInt32OrDefault("purchase_invoice_count"),
                PurchaseInvoiceAmount = reader.GetDecimalOrDefault("purchase_invoice_amount"),
                StockMovementCount = reader.GetInt32OrDefault("stock_movement_count"),
                FinishOrderCount = reader.GetInt32OrDefault("finish_order_count")
            });
        }

        return items;
    }

    private static async Task<string> ResolveDateExpressionAsync(
        MySqlConnection connection,
        string tableName,
        string alias,
        params string[] candidateColumns)
    {
        var availableColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT column_name
            FROM information_schema.columns
            WHERE table_schema = DATABASE()
              AND table_name = @tableName;
            """;
        command.Parameters.AddWithValue("@tableName", tableName);

        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            availableColumns.Add(reader.GetString(0));
        }

        var existingCandidates = candidateColumns
            .Where(column => availableColumns.Contains(column))
            .Select(column => $"{alias}.{column}")
            .ToArray();

        return existingCandidates.Length switch
        {
            0 => "CURRENT_TIMESTAMP(6)",
            1 => existingCandidates[0],
            _ => $"COALESCE({string.Join(", ", existingCandidates)})"
        };
    }

    private static StatisticComparisonItemDto BuildComparisonItem(string label, decimal currentValue, decimal previousValue, string valueKind)
    {
        var deltaValue = currentValue - previousValue;
        var deltaPercentage = previousValue == 0m
            ? (currentValue == 0m ? 0m : 100m)
            : Math.Round((deltaValue / previousValue) * 100m, 2);

        return new StatisticComparisonItemDto
        {
            Label = label,
            CurrentValue = currentValue,
            PreviousValue = previousValue,
            DeltaValue = deltaValue,
            DeltaPercentage = deltaPercentage,
            ValueKind = valueKind
        };
    }

    private static decimal CalculateRate(decimal numerator, decimal denominator) =>
        denominator <= 0m ? 0m : Math.Round((numerator / denominator) * 100m, 2);

    private static decimal CalculateAverage(decimal amount, int count) =>
        count <= 0 ? 0m : Math.Round(amount / count, 2);

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

    private async Task<string> ResolveCompanyCenterCodeAsync(Guid tenantId, Guid companyId, CancellationToken cancellationToken)
    {
        var allowedCompanies = await _companyAccessService.GetAllowedCompaniesAsync(
            _currentUserContext.UserId!.Value,
            tenantId,
            cancellationToken);

        var company = allowedCompanies.FirstOrDefault(item => item.CompanyId == companyId);
        if (company is null || string.IsNullOrWhiteSpace(company.LegacyCenterCode))
        {
            throw new InvalidOperationException("La empresa activa no tiene centro legacy configurado.");
        }

        return company.LegacyCenterCode.Trim().ToUpperInvariant();
    }
}
