using System.Globalization;
using Erp.Application.Acabados;
using Erp.Application.Alerts;
using Erp.Application.Companies;
using Erp.Application.Contexts;
using Erp.Application.Purchases;
using Erp.Application.Sales;
using Erp.Application.Stock;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Support;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Alerts;

public sealed class MySqlOperationalAlertService : IOperationalAlertQueries
{
    private const int MaxItemsPerGroup = 8;
    private const int StaleWorkOrderDays = 7;
    private const int StaleDraftDays = 7;

    private readonly MySqlConnectionFactory _connectionFactory;
    private readonly ICompanyAccessService _companyAccessService;
    private readonly ICurrentUserContext _currentUserContext;
    private readonly ITenantContext _tenantContext;
    private readonly IActiveCompanyContext _activeCompanyContext;

    public MySqlOperationalAlertService(
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

    public async Task<OperationalAlertDashboardDto> GetDashboardAsync(Guid tenantId, Guid companyId, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return new OperationalAlertDashboardDto();
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var centerCode = await ResolveCompanyCenterCodeAsync(tenantId, companyId, cancellationToken);
        var today = DateTime.Today;

        var groups = new List<OperationalAlertGroupDto>();

        AddGroup(groups, await LoadOverdueSalesInvoicesAsync(connection, tenantId, companyId, today, cancellationToken));
        AddGroup(groups, await LoadOverduePurchaseInvoicesAsync(connection, tenantId, companyId, centerCode, today, cancellationToken));
        AddGroup(groups, await LoadDelayedSalesOrdersAsync(connection, tenantId, companyId, today, cancellationToken));
        AddGroup(groups, await LoadDelayedPurchaseOrdersAsync(connection, tenantId, companyId, centerCode, today, cancellationToken));
        AddGroup(groups, await LoadStaleFinishOrdersAsync(connection, tenantId, companyId, today, cancellationToken));
        AddGroup(groups, await LoadDraftStockCountsAsync(connection, tenantId, companyId, today, cancellationToken));
        AddGroup(groups, await LoadDraftStockTransfersAsync(connection, tenantId, companyId, today, cancellationToken));
        AddGroup(groups, await LoadLowMinimumFabricStockAsync(connection, centerCode, cancellationToken));
        AddGroup(groups, await LoadLowMinimumYarnStockAsync(connection, centerCode, cancellationToken));

        return new OperationalAlertDashboardDto
        {
            GeneratedUtc = DateTime.UtcNow,
            Groups = groups,
            ActiveGroups = groups.Count,
            TotalAlerts = groups.Sum(group => group.TotalCount),
            CriticalAlerts = groups
                .Where(group => string.Equals(group.Severity, "critical", StringComparison.OrdinalIgnoreCase))
                .Sum(group => group.TotalCount),
            WarningAlerts = groups
                .Where(group => string.Equals(group.Severity, "warning", StringComparison.OrdinalIgnoreCase))
                .Sum(group => group.TotalCount),
            InfoAlerts = groups
                .Where(group => string.Equals(group.Severity, "info", StringComparison.OrdinalIgnoreCase))
                .Sum(group => group.TotalCount)
        };
    }

    private static void AddGroup(ICollection<OperationalAlertGroupDto> groups, OperationalAlertGroupDto? group)
    {
        if (group is not null && group.TotalCount > 0)
        {
            groups.Add(group);
        }
    }

    private static async Task<int> ExecuteScalarIntAsync(
        MySqlConnection connection,
        string sql,
        Action<MySqlCommand> configure,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        configure(command);
        return Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));
    }

    private async Task<OperationalAlertGroupDto?> LoadOverdueSalesInvoicesAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        DateTime today,
        CancellationToken cancellationToken)
    {
        const string countSql =
            """
            SELECT COUNT(*)
            FROM sales_invoices si
            WHERE si.tenant_id = @tenantId
              AND si.company_id = @companyId
              AND COALESCE(si.is_deleted, 0) = 0
              AND COALESCE(si.status, '') <> 'Cancelled'
              AND COALESCE(si.outstanding_amount, 0) > 0
              AND si.due_date IS NOT NULL
              AND DATE(si.due_date) < @today;
            """;

        var totalCount = await ExecuteScalarIntAsync(connection, countSql, command =>
        {
            command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            command.Parameters.AddWithValue("@companyId", companyId.ToString());
            command.Parameters.AddWithValue("@today", today);
        }, cancellationToken);

        if (totalCount == 0)
        {
            return null;
        }

        const string itemsSql =
            """
            SELECT si.invoice_series,
                   si.invoice_number,
                   si.client_name,
                   si.status,
                   si.due_date,
                   si.outstanding_amount,
                   DATEDIFF(@today, DATE(si.due_date)) AS age_days
            FROM sales_invoices si
            WHERE si.tenant_id = @tenantId
              AND si.company_id = @companyId
              AND COALESCE(si.is_deleted, 0) = 0
              AND COALESCE(si.status, '') <> 'Cancelled'
              AND COALESCE(si.outstanding_amount, 0) > 0
              AND si.due_date IS NOT NULL
              AND DATE(si.due_date) < @today
            ORDER BY age_days DESC, si.outstanding_amount DESC, si.invoice_number DESC
            LIMIT @limit;
            """;

        var items = new List<OperationalAlertListItemDto>();
        await using (var command = connection.CreateCommand())
        {
            command.CommandText = itemsSql;
            command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            command.Parameters.AddWithValue("@companyId", companyId.ToString());
            command.Parameters.AddWithValue("@today", today);
            command.Parameters.AddWithValue("@limit", MaxItemsPerGroup);

            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                var invoiceNumber = reader.GetInt32(reader.GetOrdinal("invoice_number"));
                items.Add(new OperationalAlertListItemDto
                {
                    Title = BuildSeriesNumber(reader.GetStringOrEmpty("invoice_series"), invoiceNumber),
                    Subtitle = reader.GetStringOrEmpty("client_name"),
                    Detail = reader.GetStringOrEmpty("status"),
                    ReferenceDate = reader.IsDBNull(reader.GetOrdinal("due_date")) ? null : reader.GetDateTime(reader.GetOrdinal("due_date")),
                    AgeDays = reader.GetInt32OrDefault("age_days"),
                    MetricValue = reader.GetDecimalOrDefault("outstanding_amount"),
                    MetricKind = "currency",
                    Route = $"/ventas/facturas/{invoiceNumber}"
                });
            }
        }

        return new OperationalAlertGroupDto
        {
            Key = "overdue-sales-invoices",
            Severity = "critical",
            TotalCount = totalCount,
            DefaultRoute = "/ventas/facturas",
            Items = items
        };
    }

    private async Task<OperationalAlertGroupDto?> LoadOverduePurchaseInvoicesAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        string centerCode,
        DateTime today,
        CancellationToken cancellationToken)
    {
        const string countSql =
            """
            SELECT COUNT(*)
            FROM purchase_invoices pi
            WHERE pi.tenant_id = @tenantId
              AND pi.company_id = @companyId
              AND COALESCE(pi.is_deleted, 0) = 0
              AND COALESCE(pi.status, '') NOT IN ('Paid', 'Cancelled')
              AND COALESCE(pi.outstanding_amount, 0) > 0
              AND pi.due_date IS NOT NULL
              AND DATE(pi.due_date) < @today;
            """;

        var totalCount = await ExecuteScalarIntAsync(connection, countSql, command =>
        {
            command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            command.Parameters.AddWithValue("@companyId", companyId.ToString());
            command.Parameters.AddWithValue("@today", today);
        }, cancellationToken);

        if (totalCount == 0)
        {
            return null;
        }

        const string itemsSql =
            """
            SELECT pi.invoice_series,
                   pi.invoice_number,
                   COALESCE(NULLIF(pi.supplier_name, ''), p.NOM, '') AS supplier_name,
                   pi.status,
                   pi.due_date,
                   pi.outstanding_amount,
                   DATEDIFF(@today, DATE(pi.due_date)) AS age_days
            FROM purchase_invoices pi
            LEFT JOIN prove p
              ON p.CODI = pi.supplier_code
             AND p.CENTRO = @centerCode
            WHERE pi.tenant_id = @tenantId
              AND pi.company_id = @companyId
              AND COALESCE(pi.is_deleted, 0) = 0
              AND COALESCE(pi.status, '') NOT IN ('Paid', 'Cancelled')
              AND COALESCE(pi.outstanding_amount, 0) > 0
              AND pi.due_date IS NOT NULL
              AND DATE(pi.due_date) < @today
            ORDER BY age_days DESC, pi.outstanding_amount DESC, pi.invoice_number DESC
            LIMIT @limit;
            """;

        var items = new List<OperationalAlertListItemDto>();
        await using (var command = connection.CreateCommand())
        {
            command.CommandText = itemsSql;
            command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            command.Parameters.AddWithValue("@companyId", companyId.ToString());
            command.Parameters.AddWithValue("@centerCode", centerCode);
            command.Parameters.AddWithValue("@today", today);
            command.Parameters.AddWithValue("@limit", MaxItemsPerGroup);

            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                var invoiceNumber = reader.GetInt32(reader.GetOrdinal("invoice_number"));
                items.Add(new OperationalAlertListItemDto
                {
                    Title = BuildSeriesNumber(reader.GetStringOrEmpty("invoice_series"), invoiceNumber),
                    Subtitle = reader.GetStringOrEmpty("supplier_name"),
                    Detail = reader.GetStringOrEmpty("status"),
                    ReferenceDate = reader.IsDBNull(reader.GetOrdinal("due_date")) ? null : reader.GetDateTime(reader.GetOrdinal("due_date")),
                    AgeDays = reader.GetInt32OrDefault("age_days"),
                    MetricValue = reader.GetDecimalOrDefault("outstanding_amount"),
                    MetricKind = "currency",
                    Route = $"/compras/facturas/editar/{invoiceNumber}"
                });
            }
        }

        return new OperationalAlertGroupDto
        {
            Key = "overdue-purchase-invoices",
            Severity = "critical",
            TotalCount = totalCount,
            DefaultRoute = "/compras/facturas",
            Items = items
        };
    }

    private async Task<OperationalAlertGroupDto?> LoadDelayedSalesOrdersAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        DateTime today,
        CancellationToken cancellationToken)
    {
        const string countSql =
            """
            SELECT COUNT(*)
            FROM sales_orders so
            WHERE so.tenant_id = @tenantId
              AND so.company_id = @companyId
              AND COALESCE(so.is_deleted, 0) = 0
              AND so.requested_date IS NOT NULL
              AND DATE(so.requested_date) < @today
              AND COALESCE(so.status, '') NOT IN ('Shipped', 'Cancelled');
            """;

        var totalCount = await ExecuteScalarIntAsync(connection, countSql, command =>
        {
            command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            command.Parameters.AddWithValue("@companyId", companyId.ToString());
            command.Parameters.AddWithValue("@today", today);
        }, cancellationToken);

        if (totalCount == 0)
        {
            return null;
        }

        const string itemsSql =
            """
            SELECT so.order_number,
                   so.client_name,
                   so.status,
                   so.requested_date,
                   DATEDIFF(@today, DATE(so.requested_date)) AS age_days
            FROM sales_orders so
            WHERE so.tenant_id = @tenantId
              AND so.company_id = @companyId
              AND COALESCE(so.is_deleted, 0) = 0
              AND so.requested_date IS NOT NULL
              AND DATE(so.requested_date) < @today
              AND COALESCE(so.status, '') NOT IN ('Shipped', 'Cancelled')
            ORDER BY age_days DESC, so.order_number DESC
            LIMIT @limit;
            """;

        var items = new List<OperationalAlertListItemDto>();
        await using (var command = connection.CreateCommand())
        {
            command.CommandText = itemsSql;
            command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            command.Parameters.AddWithValue("@companyId", companyId.ToString());
            command.Parameters.AddWithValue("@today", today);
            command.Parameters.AddWithValue("@limit", MaxItemsPerGroup);

            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                var orderNumber = reader.GetInt32(reader.GetOrdinal("order_number"));
                items.Add(new OperationalAlertListItemDto
                {
                    Title = orderNumber.ToString(CultureInfo.InvariantCulture),
                    Subtitle = reader.GetStringOrEmpty("client_name"),
                    Detail = reader.GetStringOrEmpty("status"),
                    ReferenceDate = reader.IsDBNull(reader.GetOrdinal("requested_date")) ? null : reader.GetDateTime(reader.GetOrdinal("requested_date")),
                    AgeDays = reader.GetInt32OrDefault("age_days"),
                    Route = $"/ventas/pedidos/editar/{orderNumber}"
                });
            }
        }

        return new OperationalAlertGroupDto
        {
            Key = "delayed-sales-orders",
            Severity = "warning",
            TotalCount = totalCount,
            DefaultRoute = "/ventas/pedidos",
            Items = items
        };
    }

    private async Task<OperationalAlertGroupDto?> LoadDelayedPurchaseOrdersAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        string centerCode,
        DateTime today,
        CancellationToken cancellationToken)
    {
        const string countSql =
            """
            SELECT COUNT(*)
            FROM purchase_orders po
            WHERE po.tenant_id = @tenantId
              AND po.company_id = @companyId
              AND COALESCE(po.is_deleted, 0) = 0
              AND po.expected_date IS NOT NULL
              AND DATE(po.expected_date) < @today
              AND COALESCE(po.status, '') NOT IN ('Received', 'Cancelled');
            """;

        var totalCount = await ExecuteScalarIntAsync(connection, countSql, command =>
        {
            command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            command.Parameters.AddWithValue("@companyId", companyId.ToString());
            command.Parameters.AddWithValue("@today", today);
        }, cancellationToken);

        if (totalCount == 0)
        {
            return null;
        }

        const string itemsSql =
            """
            SELECT po.order_number,
                   COALESCE(NULLIF(po.supplier_name, ''), p.NOM, '') AS supplier_name,
                   po.status,
                   po.expected_date,
                   DATEDIFF(@today, DATE(po.expected_date)) AS age_days
            FROM purchase_orders po
            LEFT JOIN prove p
              ON p.CODI = po.supplier_code
             AND p.CENTRO = @centerCode
            WHERE po.tenant_id = @tenantId
              AND po.company_id = @companyId
              AND COALESCE(po.is_deleted, 0) = 0
              AND po.expected_date IS NOT NULL
              AND DATE(po.expected_date) < @today
              AND COALESCE(po.status, '') NOT IN ('Received', 'Cancelled')
            ORDER BY age_days DESC, po.order_number DESC
            LIMIT @limit;
            """;

        var items = new List<OperationalAlertListItemDto>();
        await using (var command = connection.CreateCommand())
        {
            command.CommandText = itemsSql;
            command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            command.Parameters.AddWithValue("@companyId", companyId.ToString());
            command.Parameters.AddWithValue("@centerCode", centerCode);
            command.Parameters.AddWithValue("@today", today);
            command.Parameters.AddWithValue("@limit", MaxItemsPerGroup);

            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                var orderNumber = reader.GetInt32(reader.GetOrdinal("order_number"));
                items.Add(new OperationalAlertListItemDto
                {
                    Title = orderNumber.ToString(CultureInfo.InvariantCulture),
                    Subtitle = reader.GetStringOrEmpty("supplier_name"),
                    Detail = reader.GetStringOrEmpty("status"),
                    ReferenceDate = reader.IsDBNull(reader.GetOrdinal("expected_date")) ? null : reader.GetDateTime(reader.GetOrdinal("expected_date")),
                    AgeDays = reader.GetInt32OrDefault("age_days"),
                    Route = $"/compras/pedidos/editar/{orderNumber}"
                });
            }
        }

        return new OperationalAlertGroupDto
        {
            Key = "delayed-purchase-orders",
            Severity = "warning",
            TotalCount = totalCount,
            DefaultRoute = "/compras/pedidos",
            Items = items
        };
    }

    private async Task<OperationalAlertGroupDto?> LoadStaleFinishOrdersAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        DateTime today,
        CancellationToken cancellationToken)
    {
        const string countSql =
            """
            SELECT COUNT(*)
            FROM finish_work_orders fwo
            WHERE fwo.tenant_id = @tenantId
              AND fwo.company_id = @companyId
              AND COALESCE(fwo.is_deleted, 0) = 0
              AND COALESCE(fwo.status, '') IN ('Pending', 'InProgress')
              AND DATEDIFF(@today, DATE(COALESCE(fwo.updated_utc, fwo.work_date))) >= @staleDays;
            """;

        var totalCount = await ExecuteScalarIntAsync(connection, countSql, command =>
        {
            command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            command.Parameters.AddWithValue("@companyId", companyId.ToString());
            command.Parameters.AddWithValue("@today", today);
            command.Parameters.AddWithValue("@staleDays", StaleWorkOrderDays);
        }, cancellationToken);

        if (totalCount == 0)
        {
            return null;
        }

        const string itemsSql =
            """
            SELECT fwo.order_number,
                   fwo.finisher_name,
                   fwo.status,
                   COALESCE(fwo.updated_utc, fwo.work_date) AS reference_date,
                   DATEDIFF(@today, DATE(COALESCE(fwo.updated_utc, fwo.work_date))) AS age_days
            FROM finish_work_orders fwo
            WHERE fwo.tenant_id = @tenantId
              AND fwo.company_id = @companyId
              AND COALESCE(fwo.is_deleted, 0) = 0
              AND COALESCE(fwo.status, '') IN ('Pending', 'InProgress')
              AND DATEDIFF(@today, DATE(COALESCE(fwo.updated_utc, fwo.work_date))) >= @staleDays
            ORDER BY age_days DESC, fwo.order_number DESC
            LIMIT @limit;
            """;

        var items = new List<OperationalAlertListItemDto>();
        await using (var command = connection.CreateCommand())
        {
            command.CommandText = itemsSql;
            command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            command.Parameters.AddWithValue("@companyId", companyId.ToString());
            command.Parameters.AddWithValue("@today", today);
            command.Parameters.AddWithValue("@staleDays", StaleWorkOrderDays);
            command.Parameters.AddWithValue("@limit", MaxItemsPerGroup);

            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                var orderNumber = reader.GetInt32(reader.GetOrdinal("order_number"));
                items.Add(new OperationalAlertListItemDto
                {
                    Title = orderNumber.ToString(CultureInfo.InvariantCulture),
                    Subtitle = reader.GetStringOrEmpty("finisher_name"),
                    Detail = reader.GetStringOrEmpty("status"),
                    ReferenceDate = reader.IsDBNull(reader.GetOrdinal("reference_date")) ? null : reader.GetDateTime(reader.GetOrdinal("reference_date")),
                    AgeDays = reader.GetInt32OrDefault("age_days"),
                    Route = $"/produccion/acabados/editar/{orderNumber}"
                });
            }
        }

        return new OperationalAlertGroupDto
        {
            Key = "stale-finish-orders",
            Severity = "warning",
            TotalCount = totalCount,
            DefaultRoute = "/produccion/acabados?live=true&view=board",
            Items = items
        };
    }

    private async Task<OperationalAlertGroupDto?> LoadDraftStockCountsAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        DateTime today,
        CancellationToken cancellationToken)
    {
        const string countSql =
            """
            SELECT COUNT(*)
            FROM stock_counts c
            WHERE c.tenant_id = @tenantId
              AND c.company_id = @companyId
              AND COALESCE(c.is_deleted, 0) = 0
              AND COALESCE(c.status, '') = 'Draft'
              AND DATEDIFF(@today, DATE(COALESCE(c.updated_utc, c.count_date))) >= @staleDays;
            """;

        var totalCount = await ExecuteScalarIntAsync(connection, countSql, command =>
        {
            command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            command.Parameters.AddWithValue("@companyId", companyId.ToString());
            command.Parameters.AddWithValue("@today", today);
            command.Parameters.AddWithValue("@staleDays", StaleDraftDays);
        }, cancellationToken);

        if (totalCount == 0)
        {
            return null;
        }

        const string itemsSql =
            """
            SELECT c.count_number,
                   c.warehouse,
                   c.status,
                   COALESCE(c.updated_utc, c.count_date) AS reference_date,
                   DATEDIFF(@today, DATE(COALESCE(c.updated_utc, c.count_date))) AS age_days
            FROM stock_counts c
            WHERE c.tenant_id = @tenantId
              AND c.company_id = @companyId
              AND COALESCE(c.is_deleted, 0) = 0
              AND COALESCE(c.status, '') = 'Draft'
              AND DATEDIFF(@today, DATE(COALESCE(c.updated_utc, c.count_date))) >= @staleDays
            ORDER BY age_days DESC, c.count_number DESC
            LIMIT @limit;
            """;

        var items = new List<OperationalAlertListItemDto>();
        await using (var command = connection.CreateCommand())
        {
            command.CommandText = itemsSql;
            command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            command.Parameters.AddWithValue("@companyId", companyId.ToString());
            command.Parameters.AddWithValue("@today", today);
            command.Parameters.AddWithValue("@staleDays", StaleDraftDays);
            command.Parameters.AddWithValue("@limit", MaxItemsPerGroup);

            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                var countNumber = reader.GetInt32(reader.GetOrdinal("count_number"));
                items.Add(new OperationalAlertListItemDto
                {
                    Title = countNumber.ToString(CultureInfo.InvariantCulture),
                    Subtitle = reader.GetStringOrEmpty("warehouse"),
                    Detail = reader.GetStringOrEmpty("status"),
                    ReferenceDate = reader.IsDBNull(reader.GetOrdinal("reference_date")) ? null : reader.GetDateTime(reader.GetOrdinal("reference_date")),
                    AgeDays = reader.GetInt32OrDefault("age_days"),
                    Route = $"/almacen/inventarios/editar/{countNumber}"
                });
            }
        }

        return new OperationalAlertGroupDto
        {
            Key = "draft-stock-counts",
            Severity = "warning",
            TotalCount = totalCount,
            DefaultRoute = "/almacen/inventarios",
            Items = items
        };
    }

    private async Task<OperationalAlertGroupDto?> LoadDraftStockTransfersAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        DateTime today,
        CancellationToken cancellationToken)
    {
        const string countSql =
            """
            SELECT COUNT(*)
            FROM stock_transfers t
            WHERE t.tenant_id = @tenantId
              AND t.company_id = @companyId
              AND COALESCE(t.is_deleted, 0) = 0
              AND COALESCE(t.status, '') = 'Draft'
              AND DATEDIFF(@today, DATE(COALESCE(t.updated_utc, t.transfer_date))) >= @staleDays;
            """;

        var totalCount = await ExecuteScalarIntAsync(connection, countSql, command =>
        {
            command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            command.Parameters.AddWithValue("@companyId", companyId.ToString());
            command.Parameters.AddWithValue("@today", today);
            command.Parameters.AddWithValue("@staleDays", StaleDraftDays);
        }, cancellationToken);

        if (totalCount == 0)
        {
            return null;
        }

        const string itemsSql =
            """
            SELECT t.transfer_number,
                   t.from_warehouse,
                   t.to_warehouse,
                   t.status,
                   COALESCE(t.updated_utc, t.transfer_date) AS reference_date,
                   DATEDIFF(@today, DATE(COALESCE(t.updated_utc, t.transfer_date))) AS age_days
            FROM stock_transfers t
            WHERE t.tenant_id = @tenantId
              AND t.company_id = @companyId
              AND COALESCE(t.is_deleted, 0) = 0
              AND COALESCE(t.status, '') = 'Draft'
              AND DATEDIFF(@today, DATE(COALESCE(t.updated_utc, t.transfer_date))) >= @staleDays
            ORDER BY age_days DESC, t.transfer_number DESC
            LIMIT @limit;
            """;

        var items = new List<OperationalAlertListItemDto>();
        await using (var command = connection.CreateCommand())
        {
            command.CommandText = itemsSql;
            command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            command.Parameters.AddWithValue("@companyId", companyId.ToString());
            command.Parameters.AddWithValue("@today", today);
            command.Parameters.AddWithValue("@staleDays", StaleDraftDays);
            command.Parameters.AddWithValue("@limit", MaxItemsPerGroup);

            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                var transferNumber = reader.GetInt32(reader.GetOrdinal("transfer_number"));
                items.Add(new OperationalAlertListItemDto
                {
                    Title = transferNumber.ToString(CultureInfo.InvariantCulture),
                    Subtitle = $"{reader.GetStringOrEmpty("from_warehouse")} → {reader.GetStringOrEmpty("to_warehouse")}",
                    Detail = reader.GetStringOrEmpty("status"),
                    ReferenceDate = reader.IsDBNull(reader.GetOrdinal("reference_date")) ? null : reader.GetDateTime(reader.GetOrdinal("reference_date")),
                    AgeDays = reader.GetInt32OrDefault("age_days"),
                    Route = $"/almacen/traspasos/editar/{transferNumber}"
                });
            }
        }

        return new OperationalAlertGroupDto
        {
            Key = "draft-stock-transfers",
            Severity = "warning",
            TotalCount = totalCount,
            DefaultRoute = "/almacen/traspasos",
            Items = items
        };
    }

    private async Task<OperationalAlertGroupDto?> LoadLowMinimumFabricStockAsync(
        MySqlConnection connection,
        string centerCode,
        CancellationToken cancellationToken)
    {
        const string countSql =
            """
            SELECT COUNT(*)
            FROM teixits_color_detail tcd
            WHERE tcd.CENTRO = @centerCode
              AND COALESCE(tcd.MINIM, 0) > 0
              AND COALESCE(tcd.ACTUAL, 0) < COALESCE(tcd.MINIM, 0);
            """;

        var totalCount = await ExecuteScalarIntAsync(connection, countSql, command =>
        {
            command.Parameters.AddWithValue("@centerCode", centerCode);
        }, cancellationToken);

        if (totalCount == 0)
        {
            return null;
        }

        const string itemsSql =
            """
            SELECT tcd.TEIXIT_CODI AS item_code,
                   COALESCE(NULLIF(t.DESCRI, ''), tcd.TEIXIT_CODI) AS item_name,
                   tcd.COLOR AS color,
                   COALESCE(NULLIF(p.NOM, ''), CAST(tcd.PROVE AS CHAR)) AS supplier_name,
                   COALESCE(tcd.ACTUAL, 0) AS current_stock,
                   COALESCE(tcd.MINIM, 0) - COALESCE(tcd.ACTUAL, 0) AS shortage
            FROM teixits_color_detail tcd
            LEFT JOIN teixits t
              ON t.CENTRO = tcd.CENTRO
             AND t.CODI = tcd.TEIXIT_CODI
            LEFT JOIN prove p
              ON p.CENTRO = tcd.CENTRO
             AND p.CODI = tcd.PROVE
            WHERE tcd.CENTRO = @centerCode
              AND COALESCE(tcd.MINIM, 0) > 0
              AND COALESCE(tcd.ACTUAL, 0) < COALESCE(tcd.MINIM, 0)
            ORDER BY shortage DESC, current_stock ASC, item_code ASC
            LIMIT @limit;
            """;

        var items = new List<OperationalAlertListItemDto>();
        await using (var command = connection.CreateCommand())
        {
            command.CommandText = itemsSql;
            command.Parameters.AddWithValue("@centerCode", centerCode);
            command.Parameters.AddWithValue("@limit", MaxItemsPerGroup);

            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                var itemCode = reader.GetStringOrEmpty("item_code");
                items.Add(new OperationalAlertListItemDto
                {
                    Title = itemCode,
                    Subtitle = reader.GetStringOrEmpty("item_name"),
                    Detail = $"{reader.GetStringOrEmpty("color")} · {reader.GetStringOrEmpty("supplier_name")}",
                    MetricValue = reader.GetDecimalOrDefault("shortage"),
                    MetricKind = "quantity",
                    Route = $"/articulos/tejidos/editar/{Uri.EscapeDataString(itemCode)}"
                });
            }
        }

        return new OperationalAlertGroupDto
        {
            Key = "low-fabric-stock",
            Severity = "critical",
            TotalCount = totalCount,
            DefaultRoute = "/almacen/tejidos",
            Items = items
        };
    }

    private async Task<OperationalAlertGroupDto?> LoadLowMinimumYarnStockAsync(
        MySqlConnection connection,
        string centerCode,
        CancellationToken cancellationToken)
    {
        const string countSql =
            """
            SELECT COUNT(*)
            FROM fil_detail fd
            WHERE fd.CENTRO = @centerCode
              AND COALESCE(fd.MINIM, 0) > 0
              AND COALESCE(fd.ACTUAL, 0) < COALESCE(fd.MINIM, 0);
            """;

        var totalCount = await ExecuteScalarIntAsync(connection, countSql, command =>
        {
            command.Parameters.AddWithValue("@centerCode", centerCode);
        }, cancellationToken);

        if (totalCount == 0)
        {
            return null;
        }

        const string itemsSql =
            """
            SELECT fd.FIL_CODI AS item_code,
                   COALESCE(NULLIF(f.DESCRI, ''), fd.FIL_CODI) AS item_name,
                   fd.COLOR AS color,
                   COALESCE(NULLIF(p.NOM, ''), CAST(fd.PROVE AS CHAR)) AS supplier_name,
                   COALESCE(fd.ACTUAL, 0) AS current_stock,
                   COALESCE(fd.MINIM, 0) - COALESCE(fd.ACTUAL, 0) AS shortage
            FROM fil_detail fd
            LEFT JOIN fil f
              ON f.CENTRO = fd.CENTRO
             AND f.CODI = fd.FIL_CODI
            LEFT JOIN prove p
              ON p.CENTRO = fd.CENTRO
             AND p.CODI = fd.PROVE
            WHERE fd.CENTRO = @centerCode
              AND COALESCE(fd.MINIM, 0) > 0
              AND COALESCE(fd.ACTUAL, 0) < COALESCE(fd.MINIM, 0)
            ORDER BY shortage DESC, current_stock ASC, item_code ASC
            LIMIT @limit;
            """;

        var items = new List<OperationalAlertListItemDto>();
        await using (var command = connection.CreateCommand())
        {
            command.CommandText = itemsSql;
            command.Parameters.AddWithValue("@centerCode", centerCode);
            command.Parameters.AddWithValue("@limit", MaxItemsPerGroup);

            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                var itemCode = reader.GetStringOrEmpty("item_code");
                items.Add(new OperationalAlertListItemDto
                {
                    Title = itemCode,
                    Subtitle = reader.GetStringOrEmpty("item_name"),
                    Detail = $"{reader.GetStringOrEmpty("color")} · {reader.GetStringOrEmpty("supplier_name")}",
                    MetricValue = reader.GetDecimalOrDefault("shortage"),
                    MetricKind = "quantity",
                    Route = $"/articulos/hilos/editar/{Uri.EscapeDataString(itemCode)}"
                });
            }
        }

        return new OperationalAlertGroupDto
        {
            Key = "low-yarn-stock",
            Severity = "critical",
            TotalCount = totalCount,
            DefaultRoute = "/almacen/fils",
            Items = items
        };
    }

    private static string BuildSeriesNumber(string series, int number) =>
        string.IsNullOrWhiteSpace(series)
            ? number.ToString(CultureInfo.InvariantCulture)
            : $"{series.Trim()}/{number:000000}";

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
