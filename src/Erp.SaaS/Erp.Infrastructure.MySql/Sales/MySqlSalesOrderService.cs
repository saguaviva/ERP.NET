using Erp.Application.Auditing;
using Erp.Application.Companies;
using Erp.Application.Contexts;
using Erp.Application.LegacySync;
using Erp.Application.Sales;
using Erp.Application.Stock;
using Erp.Domain.Common;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Support;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Sales;

public sealed class MySqlSalesOrderService : ISalesOrderQueries, ISalesOrderService, ILegacyModuleSyncHandler
{
    private readonly MySqlConnectionFactory _saasConnectionFactory;
    private readonly LegacyMySqlConnectionFactory _legacyConnectionFactory;
    private readonly IAuditLogService _auditLogService;
    private readonly ICompanyAccessService _companyAccessService;
    private readonly ICurrentUserContext _currentUserContext;
    private readonly ITenantContext _tenantContext;
    private readonly IActiveCompanyContext _activeCompanyContext;

    public MySqlSalesOrderService(
        MySqlConnectionFactory saasConnectionFactory,
        LegacyMySqlConnectionFactory legacyConnectionFactory,
        IAuditLogService auditLogService,
        ICompanyAccessService companyAccessService,
        ICurrentUserContext currentUserContext,
        ITenantContext tenantContext,
        IActiveCompanyContext activeCompanyContext)
    {
        _saasConnectionFactory = saasConnectionFactory;
        _legacyConnectionFactory = legacyConnectionFactory;
        _auditLogService = auditLogService;
        _companyAccessService = companyAccessService;
        _currentUserContext = currentUserContext;
        _tenantContext = tenantContext;
        _activeCompanyContext = activeCompanyContext;
    }

    public string ModuleKey => LegacySyncModuleKeys.SalesOrders;
    public string DisplayName => "Ventas / Pedidos";

    public async Task<SalesOrderSearchResultDto> SearchAsync(
        Guid tenantId,
        Guid companyId,
        SalesOrderFilter filter,
        CancellationToken cancellationToken = default)
    {
        if (!_saasConnectionFactory.IsConfigured)
        {
            return new SalesOrderSearchResultDto();
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);

        var pageSize = Math.Clamp(filter.PageSize, 10, 200);
        var page = Math.Max(filter.Page, 1);
        var offset = (page - 1) * pageSize;
        var search = filter.Search?.Trim() ?? string.Empty;
        var likeSearch = $"%{search}%";
        var status = filter.Status?.Trim() ?? string.Empty;

        await using var connection = await _saasConnectionFactory.OpenConnectionAsync(cancellationToken);
        await using (var countCommand = connection.CreateCommand())
        {
            countCommand.CommandText =
                """
                SELECT COUNT(*)
                FROM sales_orders so
                WHERE so.tenant_id = @tenantId
                  AND so.company_id = @companyId
                  AND (
                        @includeClosed = 1
                        OR (
                            so.status NOT IN ('Shipped', 'Cancelled')
                            AND COALESCE(so.is_deleted, 0) = 0
                        )
                      )
                  AND (
                        @status = ''
                        OR so.status = @status
                      )
                  AND (
                        @search = ''
                        OR CAST(so.order_number AS CHAR) LIKE @likeSearch
                        OR COALESCE(so.client_name, '') LIKE @likeSearch
                        OR so.notes LIKE @likeSearch
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
                return new SalesOrderSearchResultDto
                {
                    TotalCount = 0
                };
            }

            await using var command = connection.CreateCommand();
            command.CommandText =
                $"""
                SELECT so.order_number,
                       so.client_code,
                       so.client_name,
                       so.document_date,
                       so.requested_date,
                       so.status,
                       COALESCE(so.origin, 'saas') AS origin,
                       so.synced_utc,
                       so.notes,
                       COUNT(sol.line_number) AS line_count,
                       COALESCE(SUM(sol.quantity * sol.unit_price), 0) AS total_amount
                FROM sales_orders so
                LEFT JOIN sales_order_lines sol
                  ON sol.tenant_id = so.tenant_id
                 AND sol.company_id = so.company_id
                 AND sol.order_number = so.order_number
                WHERE so.tenant_id = @tenantId
                  AND so.company_id = @companyId
                  AND (
                        @includeClosed = 1
                        OR (
                            so.status NOT IN ('Shipped', 'Cancelled')
                            AND COALESCE(so.is_deleted, 0) = 0
                        )
                      )
                  AND (
                        @status = ''
                        OR so.status = @status
                      )
                  AND (
                        @search = ''
                        OR CAST(so.order_number AS CHAR) LIKE @likeSearch
                        OR COALESCE(so.client_name, '') LIKE @likeSearch
                        OR so.notes LIKE @likeSearch
                      )
                GROUP BY so.order_number, so.client_code, so.client_name, so.document_date, so.requested_date, so.status, so.origin, so.synced_utc, so.notes
                {BuildSalesOrderSearchOrderByClause(filter)}
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

            var items = new List<SalesOrderListItemDto>();
            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                items.Add(new SalesOrderListItemDto
                {
                    OrderNumber = reader.GetInt32(reader.GetOrdinal("order_number")),
                    ClientCode = reader.GetInt32(reader.GetOrdinal("client_code")),
                    ClientName = reader.GetStringOrEmpty("client_name"),
                    DocumentDate = reader.GetDateTime(reader.GetOrdinal("document_date")),
                    RequestedDate = reader.IsDBNull(reader.GetOrdinal("requested_date")) ? null : reader.GetDateTime(reader.GetOrdinal("requested_date")),
                    Status = reader.GetStringOrEmpty("status"),
                    Origin = reader.GetStringOrEmpty("origin"),
                    SyncedUtc = reader.IsDBNull(reader.GetOrdinal("synced_utc")) ? null : reader.GetDateTime(reader.GetOrdinal("synced_utc")),
                    Notes = reader.GetStringOrEmpty("notes"),
                    LineCount = reader.GetInt32(reader.GetOrdinal("line_count")),
                    TotalAmount = reader.GetDecimal(reader.GetOrdinal("total_amount"))
                });
            }

            return new SalesOrderSearchResultDto
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }

    public async Task<SalesOrderDetailDto?> GetByOrderNumberAsync(
        Guid tenantId,
        Guid companyId,
        int orderNumber,
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
            SELECT so.order_number,
                   comp.name AS company_name,
                   comp.legacy_center_code,
                   so.client_code,
                   so.client_name,
                   so.client_tax_id,
                   so.document_date,
                   so.requested_date,
                   so.status,
                   COALESCE(so.origin, 'saas') AS origin,
                   so.synced_utc,
                   so.notes
            FROM sales_orders so
            LEFT JOIN companies comp
              ON comp.id = so.company_id
             AND comp.tenant_id = so.tenant_id
            WHERE so.tenant_id = @tenantId
              AND so.company_id = @companyId
              AND so.order_number = @orderNumber
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@orderNumber", orderNumber);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        var detail = new SalesOrderDetailDto
        {
            OrderNumber = reader.GetInt32(reader.GetOrdinal("order_number")),
            CompanyName = reader.GetStringOrEmpty("company_name"),
            CompanyLegacyCenterCode = reader.GetStringOrEmpty("legacy_center_code"),
            ClientCode = reader.GetInt32(reader.GetOrdinal("client_code")),
            ClientName = reader.GetStringOrEmpty("client_name"),
            ClientTaxId = reader.GetStringOrEmpty("client_tax_id"),
            DocumentDate = reader.GetDateTime(reader.GetOrdinal("document_date")),
            RequestedDate = reader.IsDBNull(reader.GetOrdinal("requested_date")) ? null : reader.GetDateTime(reader.GetOrdinal("requested_date")),
            Status = reader.GetStringOrEmpty("status"),
            Origin = reader.GetStringOrEmpty("origin"),
            SyncedUtc = reader.IsDBNull(reader.GetOrdinal("synced_utc")) ? null : reader.GetDateTime(reader.GetOrdinal("synced_utc")),
            Notes = reader.GetStringOrEmpty("notes")
        };
        await reader.DisposeAsync();

        detail.Lines = await LoadLinesAsync(connection, tenantId, companyId, orderNumber, cancellationToken);
        detail.TotalAmount = detail.Lines.Sum(line => line.LineTotal);
        detail.TotalShippedQuantity = detail.Lines.Sum(line => line.ShippedQuantity);
        detail.TotalPendingQuantity = detail.Lines.Sum(line => line.PendingQuantity);
        return detail;
    }

    public async Task<IReadOnlyCollection<SalesOrderShipmentDto>> GetShipmentsAsync(
        Guid tenantId,
        Guid companyId,
        int orderNumber,
        CancellationToken cancellationToken = default)
    {
        if (!_saasConnectionFactory.IsConfigured)
        {
            return [];
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);

        await using var connection = await _saasConnectionFactory.OpenConnectionAsync(cancellationToken);
        var shipments = new List<SalesOrderShipmentDto>();

        await using (var command = connection.CreateCommand())
        {
            command.CommandText =
                """
                SELECT ss.shipment_id,
                       ss.shipment_series,
                       ss.shipment_number,
                       ss.shipment_date,
                       ss.warehouse,
                       ss.invoice_status,
                       ss.invoice_reference,
                       ss.invoice_id,
                       si.invoice_series,
                       si.invoice_number,
                       ss.invoice_ready_utc,
                       ss.notes
                FROM sales_order_shipments ss
                LEFT JOIN sales_invoices si
                  ON si.invoice_id = ss.invoice_id
                WHERE ss.tenant_id = @tenantId
                  AND ss.company_id = @companyId
                  AND ss.order_number = @orderNumber
                  AND COALESCE(ss.is_deleted, 0) = 0
                ORDER BY shipment_date DESC, shipment_id DESC;
                """;
            command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            command.Parameters.AddWithValue("@companyId", companyId.ToString());
            command.Parameters.AddWithValue("@orderNumber", orderNumber);

            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                shipments.Add(new SalesOrderShipmentDto
                {
                    ShipmentId = reader.GetGuid("shipment_id"),
                    ShipmentSeries = reader.GetStringOrEmpty("shipment_series"),
                    ShipmentNumber = reader.IsDBNull(reader.GetOrdinal("shipment_number")) ? 0 : reader.GetInt32(reader.GetOrdinal("shipment_number")),
                    OrderNumber = orderNumber,
                    ShipmentDate = reader.GetDateTime(reader.GetOrdinal("shipment_date")),
                    Warehouse = reader.GetStringOrEmpty("warehouse"),
                    InvoiceStatus = reader.GetStringOrEmpty("invoice_status"),
                    InvoiceReference = reader.GetStringOrEmpty("invoice_reference"),
                    InvoiceId = reader.IsDBNull(reader.GetOrdinal("invoice_id")) ? null : reader.GetGuid("invoice_id"),
                    InvoiceSeries = reader.GetStringOrEmpty("invoice_series"),
                    InvoiceNumber = reader.IsDBNull(reader.GetOrdinal("invoice_number")) ? null : reader.GetInt32(reader.GetOrdinal("invoice_number")),
                    InvoiceReadyUtc = reader.IsDBNull(reader.GetOrdinal("invoice_ready_utc")) ? null : reader.GetDateTime(reader.GetOrdinal("invoice_ready_utc")),
                    Notes = reader.GetStringOrEmpty("notes")
                });
            }
        }

        if (shipments.Count == 0)
        {
            return shipments;
        }

        await using var linesCommand = connection.CreateCommand();
        linesCommand.CommandText =
            """
            SELECT shipment_id, line_number, description, shipped_quantity
            FROM sales_order_shipment_lines
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND order_number = @orderNumber
            ORDER BY shipment_id DESC, line_number;
            """;
        linesCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        linesCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
        linesCommand.Parameters.AddWithValue("@orderNumber", orderNumber);

        var linesByShipment = new Dictionary<Guid, List<SalesOrderShipmentLineDto>>();
        await using var linesReader = await linesCommand.ExecuteReaderAsync(cancellationToken);
        while (await linesReader.ReadAsync(cancellationToken))
        {
            var shipmentId = linesReader.GetGuid("shipment_id");
            if (!linesByShipment.TryGetValue(shipmentId, out var lines))
            {
                lines = [];
                linesByShipment[shipmentId] = lines;
            }

            lines.Add(new SalesOrderShipmentLineDto
            {
                LineNumber = linesReader.GetInt32(linesReader.GetOrdinal("line_number")),
                Description = linesReader.GetStringOrEmpty("description"),
                ShippedQuantity = linesReader.GetDecimal(linesReader.GetOrdinal("shipped_quantity"))
            });
        }

        foreach (var shipment in shipments)
        {
            if (linesByShipment.TryGetValue(shipment.ShipmentId, out var lines))
            {
                shipment.Lines = lines;
                shipment.TotalShippedQuantity = lines.Sum(line => line.ShippedQuantity);
            }
        }

        return shipments;
    }

    public async Task<SalesShipmentSearchResultDto> SearchShipmentsAsync(
        Guid tenantId,
        Guid companyId,
        SalesOrderFilter filter,
        CancellationToken cancellationToken = default)
    {
        if (!_saasConnectionFactory.IsConfigured)
        {
            return new SalesShipmentSearchResultDto();
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);

        var pageSize = Math.Clamp(filter.PageSize, 10, 200);
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
                FROM sales_order_shipments ss
                LEFT JOIN sales_orders so
                  ON so.tenant_id = ss.tenant_id
                 AND so.company_id = ss.company_id
                 AND so.order_number = ss.order_number
                WHERE ss.tenant_id = @tenantId
                  AND ss.company_id = @companyId
                  AND COALESCE(ss.is_deleted, 0) = 0
                  AND (
                        @search = ''
                        OR CAST(ss.shipment_number AS CHAR) LIKE @likeSearch
                        OR CAST(ss.order_number AS CHAR) LIKE @likeSearch
                        OR COALESCE(so.client_name, '') LIKE @likeSearch
                        OR COALESCE(ss.warehouse, '') LIKE @likeSearch
                        OR COALESCE(ss.invoice_reference, '') LIKE @likeSearch
                        OR ss.notes LIKE @likeSearch
                      );
                """;
            countCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            countCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
            countCommand.Parameters.AddWithValue("@search", search);
            countCommand.Parameters.AddWithValue("@likeSearch", likeSearch);

            var totalCount = Convert.ToInt32(await countCommand.ExecuteScalarAsync(cancellationToken));
            if (totalCount == 0)
            {
                return new SalesShipmentSearchResultDto();
            }

            await using var command = connection.CreateCommand();
            var orderBy = BuildSalesShipmentSearchOrderByClause(filter);
            command.CommandText =
                $"""
                SELECT ss.shipment_id,
                       ss.shipment_series,
                       ss.shipment_number,
                       ss.order_number,
                       ss.shipment_date,
                       ss.warehouse,
                       ss.invoice_status,
                       ss.invoice_reference,
                       ss.invoice_id,
                       si.invoice_series,
                       si.invoice_number,
                       ss.invoice_ready_utc,
                       ss.notes,
                       so.client_code,
                       so.client_name,
                       COUNT(shl.line_number) AS line_count,
                       COALESCE(SUM(shl.shipped_quantity), 0) AS total_shipped_quantity
                FROM sales_order_shipments ss
                LEFT JOIN sales_orders so
                  ON so.tenant_id = ss.tenant_id
                 AND so.company_id = ss.company_id
                 AND so.order_number = ss.order_number
                LEFT JOIN sales_invoices si
                  ON si.invoice_id = ss.invoice_id
                LEFT JOIN sales_order_shipment_lines shl
                  ON shl.shipment_id = ss.shipment_id
                WHERE ss.tenant_id = @tenantId
                  AND ss.company_id = @companyId
                  AND COALESCE(ss.is_deleted, 0) = 0
                  AND (
                        @search = ''
                        OR CAST(ss.shipment_number AS CHAR) LIKE @likeSearch
                        OR CAST(ss.order_number AS CHAR) LIKE @likeSearch
                        OR COALESCE(so.client_name, '') LIKE @likeSearch
                        OR COALESCE(ss.warehouse, '') LIKE @likeSearch
                        OR COALESCE(ss.invoice_reference, '') LIKE @likeSearch
                        OR ss.notes LIKE @likeSearch
                      )
                GROUP BY ss.shipment_id, ss.shipment_series, ss.shipment_number, ss.order_number, ss.shipment_date, ss.warehouse, ss.invoice_status, ss.invoice_reference, ss.invoice_id, si.invoice_series, si.invoice_number, ss.invoice_ready_utc, ss.notes, so.client_code, so.client_name
                {orderBy}
                LIMIT @pageSize OFFSET @offset;
                """;
            command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            command.Parameters.AddWithValue("@companyId", companyId.ToString());
            command.Parameters.AddWithValue("@search", search);
            command.Parameters.AddWithValue("@likeSearch", likeSearch);
            command.Parameters.AddWithValue("@pageSize", pageSize);
            command.Parameters.AddWithValue("@offset", offset);

            var items = new List<SalesOrderShipmentDto>();
            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                items.Add(new SalesOrderShipmentDto
                {
                    ShipmentId = reader.GetGuid("shipment_id"),
                    ShipmentSeries = reader.GetStringOrEmpty("shipment_series"),
                    ShipmentNumber = reader.IsDBNull(reader.GetOrdinal("shipment_number")) ? 0 : reader.GetInt32(reader.GetOrdinal("shipment_number")),
                    OrderNumber = reader.GetInt32(reader.GetOrdinal("order_number")),
                    ShipmentDate = reader.GetDateTime(reader.GetOrdinal("shipment_date")),
                    Warehouse = reader.GetStringOrEmpty("warehouse"),
                    InvoiceStatus = reader.GetStringOrEmpty("invoice_status"),
                    InvoiceReference = reader.GetStringOrEmpty("invoice_reference"),
                    InvoiceId = reader.IsDBNull(reader.GetOrdinal("invoice_id")) ? null : reader.GetGuid("invoice_id"),
                    InvoiceSeries = reader.GetStringOrEmpty("invoice_series"),
                    InvoiceNumber = reader.IsDBNull(reader.GetOrdinal("invoice_number")) ? null : reader.GetInt32(reader.GetOrdinal("invoice_number")),
                    InvoiceReadyUtc = reader.IsDBNull(reader.GetOrdinal("invoice_ready_utc")) ? null : reader.GetDateTime(reader.GetOrdinal("invoice_ready_utc")),
                    Notes = reader.GetStringOrEmpty("notes"),
                    ClientCode = reader.IsDBNull(reader.GetOrdinal("client_code")) ? 0 : reader.GetInt32(reader.GetOrdinal("client_code")),
                    ClientName = reader.GetStringOrEmpty("client_name"),
                    TotalShippedQuantity = reader.GetDecimal(reader.GetOrdinal("total_shipped_quantity"))
                });
            }

            return new SalesShipmentSearchResultDto
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }

    public async Task<SalesOrderShipmentDto?> GetShipmentByNumberAsync(
        Guid tenantId,
        Guid companyId,
        int shipmentNumber,
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
            SELECT ss.shipment_id,
                   ss.shipment_series,
                   ss.shipment_number,
                   ss.order_number,
                   ss.shipment_date,
                   ss.warehouse,
                   ss.invoice_status,
                   ss.invoice_reference,
                   ss.invoice_id,
                   si.invoice_series,
                   si.invoice_number,
                   ss.invoice_ready_utc,
                   ss.notes,
                   so.client_code,
                   so.client_name,
                   so.client_tax_id,
                   c.name AS company_name,
                   c.legacy_center_code,
                   t.name AS tenant_name
            FROM sales_order_shipments ss
            LEFT JOIN sales_orders so
              ON so.tenant_id = ss.tenant_id
             AND so.company_id = ss.company_id
             AND so.order_number = ss.order_number
            LEFT JOIN sales_invoices si
              ON si.invoice_id = ss.invoice_id
            LEFT JOIN companies c
              ON c.id = ss.company_id
             AND c.tenant_id = ss.tenant_id
            LEFT JOIN tenants t
              ON t.id = ss.tenant_id
            WHERE ss.tenant_id = @tenantId
              AND ss.company_id = @companyId
              AND COALESCE(ss.is_deleted, 0) = 0
              AND ss.shipment_number = @shipmentNumber
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@shipmentNumber", shipmentNumber);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        var shipment = new SalesOrderShipmentDto
        {
            ShipmentId = reader.GetGuid("shipment_id"),
            ShipmentSeries = reader.GetStringOrEmpty("shipment_series"),
            ShipmentNumber = reader.IsDBNull(reader.GetOrdinal("shipment_number")) ? 0 : reader.GetInt32(reader.GetOrdinal("shipment_number")),
            OrderNumber = reader.GetInt32(reader.GetOrdinal("order_number")),
            ClientCode = reader.IsDBNull(reader.GetOrdinal("client_code")) ? 0 : reader.GetInt32(reader.GetOrdinal("client_code")),
            ClientName = reader.GetStringOrEmpty("client_name"),
            ClientTaxId = reader.GetStringOrEmpty("client_tax_id"),
            CompanyName = reader.GetStringOrEmpty("company_name"),
            CompanyLegacyCenterCode = reader.GetStringOrEmpty("legacy_center_code"),
            TenantName = reader.GetStringOrEmpty("tenant_name"),
            ShipmentDate = reader.GetDateTime(reader.GetOrdinal("shipment_date")),
            Warehouse = reader.GetStringOrEmpty("warehouse"),
            InvoiceStatus = reader.GetStringOrEmpty("invoice_status"),
            InvoiceReference = reader.GetStringOrEmpty("invoice_reference"),
            InvoiceId = reader.IsDBNull(reader.GetOrdinal("invoice_id")) ? null : reader.GetGuid("invoice_id"),
            InvoiceSeries = reader.GetStringOrEmpty("invoice_series"),
            InvoiceNumber = reader.IsDBNull(reader.GetOrdinal("invoice_number")) ? null : reader.GetInt32(reader.GetOrdinal("invoice_number")),
            InvoiceReadyUtc = reader.IsDBNull(reader.GetOrdinal("invoice_ready_utc")) ? null : reader.GetDateTime(reader.GetOrdinal("invoice_ready_utc")),
            Notes = reader.GetStringOrEmpty("notes")
        };
        await reader.DisposeAsync();

        await using var linesCommand = connection.CreateCommand();
        linesCommand.CommandText =
            """
            SELECT line_number, description, shipped_quantity
            FROM sales_order_shipment_lines
            WHERE shipment_id = @shipmentId
            ORDER BY line_number;
            """;
        linesCommand.Parameters.AddWithValue("@shipmentId", shipment.ShipmentId.ToString());

        var lines = new List<SalesOrderShipmentLineDto>();
        await using var linesReader = await linesCommand.ExecuteReaderAsync(cancellationToken);
        while (await linesReader.ReadAsync(cancellationToken))
        {
            lines.Add(new SalesOrderShipmentLineDto
            {
                LineNumber = linesReader.GetInt32(linesReader.GetOrdinal("line_number")),
                Description = linesReader.GetStringOrEmpty("description"),
                ShippedQuantity = linesReader.GetDecimal(linesReader.GetOrdinal("shipped_quantity"))
            });
        }

        shipment.Lines = lines;
        shipment.TotalShippedQuantity = lines.Sum(line => line.ShippedQuantity);

        var clientSnapshot = await GetClientSnapshotAsync(shipment.CompanyLegacyCenterCode, shipment.ClientCode, cancellationToken);
        shipment.ClientAddress = clientSnapshot?.Address ?? string.Empty;
        shipment.ClientPostalCode = clientSnapshot?.PostalCode ?? string.Empty;
        shipment.ClientCity = clientSnapshot?.City ?? string.Empty;
        shipment.ClientProvince = clientSnapshot?.Province ?? string.Empty;
        shipment.ClientCountry = clientSnapshot?.Country ?? string.Empty;

        return shipment;
    }

    public async Task<PendingSalesShipmentSearchResultDto> SearchPendingShipmentsAsync(
        Guid tenantId,
        Guid companyId,
        SalesPreInvoiceFilter filter,
        CancellationToken cancellationToken = default)
    {
        if (!_saasConnectionFactory.IsConfigured)
        {
            return new PendingSalesShipmentSearchResultDto();
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);

        var pageSize = Math.Clamp(filter.PageSize, 10, 200);
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
                FROM sales_order_shipments ss
                INNER JOIN sales_orders so
                  ON so.tenant_id = ss.tenant_id
                 AND so.company_id = ss.company_id
                 AND so.order_number = ss.order_number
                WHERE ss.tenant_id = @tenantId
                  AND ss.company_id = @companyId
                  AND COALESCE(ss.is_deleted, 0) = 0
                  AND ss.invoice_status = 'Pending'
                  AND (
                        @search = ''
                        OR CAST(ss.shipment_number AS CHAR) LIKE @likeSearch
                        OR CAST(ss.order_number AS CHAR) LIKE @likeSearch
                        OR COALESCE(so.client_name, '') LIKE @likeSearch
                        OR COALESCE(so.client_tax_id, '') LIKE @likeSearch
                        OR COALESCE(ss.warehouse, '') LIKE @likeSearch
                        OR COALESCE(ss.notes, '') LIKE @likeSearch
                      );
                """;
            countCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            countCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
            countCommand.Parameters.AddWithValue("@search", search);
            countCommand.Parameters.AddWithValue("@likeSearch", likeSearch);

            var totalCount = Convert.ToInt32(await countCommand.ExecuteScalarAsync(cancellationToken));
            if (totalCount == 0)
            {
                return new PendingSalesShipmentSearchResultDto();
            }

            await using var command = connection.CreateCommand();
            var orderBy = BuildPendingShipmentSearchOrderByClause(filter);
            command.CommandText =
                $"""
                SELECT ss.shipment_id,
                       ss.shipment_series,
                       ss.shipment_number,
                       ss.order_number,
                       ss.shipment_date,
                       ss.warehouse,
                       ss.invoice_ready_utc,
                       ss.notes,
                       so.client_code,
                       so.client_name,
                       so.client_tax_id,
                       COALESCE(SUM(shl.shipped_quantity), 0) AS total_shipped_quantity,
                       COALESCE(SUM(shl.shipped_quantity * COALESCE(sol.unit_price, 0)), 0) AS estimated_amount
                FROM sales_order_shipments ss
                INNER JOIN sales_orders so
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
                  AND ss.invoice_status = 'Pending'
                  AND (
                        @search = ''
                        OR CAST(ss.shipment_number AS CHAR) LIKE @likeSearch
                        OR CAST(ss.order_number AS CHAR) LIKE @likeSearch
                        OR COALESCE(so.client_name, '') LIKE @likeSearch
                        OR COALESCE(so.client_tax_id, '') LIKE @likeSearch
                        OR COALESCE(ss.warehouse, '') LIKE @likeSearch
                        OR COALESCE(ss.notes, '') LIKE @likeSearch
                      )
                GROUP BY ss.shipment_id, ss.shipment_series, ss.shipment_number, ss.order_number, ss.shipment_date, ss.warehouse, ss.invoice_ready_utc, ss.notes, so.client_code, so.client_name, so.client_tax_id
                {orderBy}
                LIMIT @pageSize OFFSET @offset;
                """;
            command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            command.Parameters.AddWithValue("@companyId", companyId.ToString());
            command.Parameters.AddWithValue("@search", search);
            command.Parameters.AddWithValue("@likeSearch", likeSearch);
            command.Parameters.AddWithValue("@pageSize", pageSize);
            command.Parameters.AddWithValue("@offset", offset);

            var items = new List<PendingSalesShipmentDto>();
            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                items.Add(new PendingSalesShipmentDto
                {
                    ShipmentId = reader.GetGuid("shipment_id"),
                    ShipmentSeries = reader.GetStringOrEmpty("shipment_series"),
                    ShipmentNumber = reader.GetInt32(reader.GetOrdinal("shipment_number")),
                    OrderNumber = reader.GetInt32(reader.GetOrdinal("order_number")),
                    ClientCode = reader.GetInt32(reader.GetOrdinal("client_code")),
                    ClientName = reader.GetStringOrEmpty("client_name"),
                    ClientTaxId = reader.GetStringOrEmpty("client_tax_id"),
                    ShipmentDate = reader.GetDateTime(reader.GetOrdinal("shipment_date")),
                    Warehouse = reader.GetStringOrEmpty("warehouse"),
                    InvoiceReadyUtc = reader.IsDBNull(reader.GetOrdinal("invoice_ready_utc")) ? null : reader.GetDateTime(reader.GetOrdinal("invoice_ready_utc")),
                    Notes = reader.GetStringOrEmpty("notes"),
                    TotalShippedQuantity = reader.GetDecimal(reader.GetOrdinal("total_shipped_quantity")),
                    EstimatedAmount = reader.GetDecimal(reader.GetOrdinal("estimated_amount"))
                });
            }

            return new PendingSalesShipmentSearchResultDto
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }

    public async Task<SalesInvoiceDraftSearchResultDto> SearchInvoiceDraftsAsync(
        Guid tenantId,
        Guid companyId,
        SalesPreInvoiceFilter filter,
        CancellationToken cancellationToken = default)
    {
        if (!_saasConnectionFactory.IsConfigured)
        {
            return new SalesInvoiceDraftSearchResultDto();
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);

        var pageSize = Math.Clamp(filter.PageSize, 10, 200);
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
                FROM sales_invoice_drafts
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND (
                        @search = ''
                        OR CAST(draft_number AS CHAR) LIKE @likeSearch
                        OR client_name LIKE @likeSearch
                        OR COALESCE(notes, '') LIKE @likeSearch
                      );
                """;
            countCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            countCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
            countCommand.Parameters.AddWithValue("@search", search);
            countCommand.Parameters.AddWithValue("@likeSearch", likeSearch);

            var totalCount = Convert.ToInt32(await countCommand.ExecuteScalarAsync(cancellationToken));
            if (totalCount == 0)
            {
                return new SalesInvoiceDraftSearchResultDto();
            }

            await using var command = connection.CreateCommand();
            var orderBy = BuildInvoiceDraftSearchOrderByClause(filter);
            command.CommandText =
                $"""
                SELECT sid.draft_id,
                       sid.draft_series,
                       sid.draft_number,
                       sid.client_code,
                       sid.client_name,
                       sid.issue_date,
                       sid.due_date,
                       sid.status,
                       sid.invoice_id,
                       sid.issued_utc,
                       si.invoice_series,
                       si.invoice_number,
                       sid.shipment_count,
                       sid.total_quantity,
                       sid.total_amount,
                       sid.notes
                FROM sales_invoice_drafts sid
                LEFT JOIN sales_invoices si
                  ON si.invoice_id = sid.invoice_id
                WHERE sid.tenant_id = @tenantId
                  AND sid.company_id = @companyId
                  AND (
                        @search = ''
                        OR CAST(sid.draft_number AS CHAR) LIKE @likeSearch
                        OR sid.client_name LIKE @likeSearch
                        OR COALESCE(sid.notes, '') LIKE @likeSearch
                      )
                {orderBy}
                LIMIT @pageSize OFFSET @offset;
                """;
            command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            command.Parameters.AddWithValue("@companyId", companyId.ToString());
            command.Parameters.AddWithValue("@search", search);
            command.Parameters.AddWithValue("@likeSearch", likeSearch);
            command.Parameters.AddWithValue("@pageSize", pageSize);
            command.Parameters.AddWithValue("@offset", offset);

            var items = new List<SalesInvoiceDraftListItemDto>();
            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                items.Add(new SalesInvoiceDraftListItemDto
                {
                    DraftId = reader.GetGuid("draft_id"),
                    DraftSeries = reader.GetStringOrEmpty("draft_series"),
                    DraftNumber = reader.GetInt32(reader.GetOrdinal("draft_number")),
                    ClientCode = reader.GetInt32(reader.GetOrdinal("client_code")),
                    ClientName = reader.GetStringOrEmpty("client_name"),
                    IssueDate = reader.GetDateTime(reader.GetOrdinal("issue_date")),
                    DueDate = reader.IsDBNull(reader.GetOrdinal("due_date")) ? null : reader.GetDateTime(reader.GetOrdinal("due_date")),
                    Status = reader.GetStringOrEmpty("status"),
                    InvoiceId = reader.IsDBNull(reader.GetOrdinal("invoice_id")) ? null : reader.GetGuid("invoice_id"),
                    InvoiceSeries = reader.GetStringOrEmpty("invoice_series"),
                    InvoiceNumber = reader.IsDBNull(reader.GetOrdinal("invoice_number")) ? null : reader.GetInt32(reader.GetOrdinal("invoice_number")),
                    ShipmentCount = reader.GetInt32(reader.GetOrdinal("shipment_count")),
                    TotalQuantity = reader.GetDecimal(reader.GetOrdinal("total_quantity")),
                    TotalAmount = reader.GetDecimal(reader.GetOrdinal("total_amount")),
                    Notes = reader.GetStringOrEmpty("notes")
                });
            }

            return new SalesInvoiceDraftSearchResultDto
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }

    public async Task<SalesInvoiceDraftDto?> GetInvoiceDraftByNumberAsync(
        Guid tenantId,
        Guid companyId,
        int draftNumber,
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
            SELECT sd.draft_id,
                   sd.draft_series,
                   sd.draft_number,
                   sd.client_code,
                   sd.client_name,
                   sd.client_tax_id,
                   sd.issue_date,
                   sd.due_date,
                   sd.status,
                   sd.invoice_id,
                   sd.issued_utc,
                   si.invoice_series,
                   si.invoice_number,
                   sd.shipment_count,
                   sd.total_quantity,
                   sd.total_amount,
                   sd.notes,
                   c.name AS company_name,
                   c.legacy_center_code,
                   t.name AS tenant_name
            FROM sales_invoice_drafts sd
            LEFT JOIN sales_invoices si
              ON si.invoice_id = sd.invoice_id
            LEFT JOIN companies c
              ON c.id = sd.company_id
             AND c.tenant_id = sd.tenant_id
            LEFT JOIN tenants t
              ON t.id = sd.tenant_id
            WHERE sd.tenant_id = @tenantId
              AND sd.company_id = @companyId
              AND sd.draft_number = @draftNumber
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@draftNumber", draftNumber);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        var draft = new SalesInvoiceDraftDto
        {
            DraftId = reader.GetGuid("draft_id"),
            DraftSeries = reader.GetStringOrEmpty("draft_series"),
            DraftNumber = reader.GetInt32(reader.GetOrdinal("draft_number")),
            ClientCode = reader.GetInt32(reader.GetOrdinal("client_code")),
            ClientName = reader.GetStringOrEmpty("client_name"),
            ClientTaxId = reader.GetStringOrEmpty("client_tax_id"),
            CompanyName = reader.GetStringOrEmpty("company_name"),
            CompanyLegacyCenterCode = reader.GetStringOrEmpty("legacy_center_code"),
            TenantName = reader.GetStringOrEmpty("tenant_name"),
            IssueDate = reader.GetDateTime(reader.GetOrdinal("issue_date")),
            DueDate = reader.IsDBNull(reader.GetOrdinal("due_date")) ? null : reader.GetDateTime(reader.GetOrdinal("due_date")),
            Status = reader.GetStringOrEmpty("status"),
            InvoiceId = reader.IsDBNull(reader.GetOrdinal("invoice_id")) ? null : reader.GetGuid("invoice_id"),
            IssuedUtc = reader.IsDBNull(reader.GetOrdinal("issued_utc")) ? null : reader.GetDateTime(reader.GetOrdinal("issued_utc")),
            InvoiceSeries = reader.GetStringOrEmpty("invoice_series"),
            InvoiceNumber = reader.IsDBNull(reader.GetOrdinal("invoice_number")) ? null : reader.GetInt32(reader.GetOrdinal("invoice_number")),
            ShipmentCount = reader.GetInt32(reader.GetOrdinal("shipment_count")),
            TotalQuantity = reader.GetDecimal(reader.GetOrdinal("total_quantity")),
            TotalAmount = reader.GetDecimal(reader.GetOrdinal("total_amount")),
            Notes = reader.GetStringOrEmpty("notes")
        };
        await reader.DisposeAsync();

        var clientSnapshot = await GetClientSnapshotAsync(draft.CompanyLegacyCenterCode, draft.ClientCode, cancellationToken);
        draft.ClientAddress = clientSnapshot?.Address ?? string.Empty;
        draft.ClientPostalCode = clientSnapshot?.PostalCode ?? string.Empty;
        draft.ClientCity = clientSnapshot?.City ?? string.Empty;
        draft.ClientProvince = clientSnapshot?.Province ?? string.Empty;
        draft.ClientCountry = clientSnapshot?.Country ?? string.Empty;

        await using var shipmentsCommand = connection.CreateCommand();
        shipmentsCommand.CommandText =
            """
            SELECT shipment_id, shipment_series, shipment_number, order_number, shipment_date, warehouse, shipped_quantity, estimated_amount
            FROM sales_invoice_draft_shipments
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND draft_id = @draftId
            ORDER BY shipment_date, shipment_number;
            """;
        shipmentsCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        shipmentsCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
        shipmentsCommand.Parameters.AddWithValue("@draftId", draft.DraftId.ToString());

        var shipments = new List<SalesInvoiceDraftShipmentDto>();
        await using (var shipmentsReader = await shipmentsCommand.ExecuteReaderAsync(cancellationToken))
        {
            while (await shipmentsReader.ReadAsync(cancellationToken))
            {
                shipments.Add(new SalesInvoiceDraftShipmentDto
                {
                    ShipmentId = shipmentsReader.GetGuid("shipment_id"),
                    ShipmentSeries = shipmentsReader.GetStringOrEmpty("shipment_series"),
                    ShipmentNumber = shipmentsReader.GetInt32(shipmentsReader.GetOrdinal("shipment_number")),
                    OrderNumber = shipmentsReader.GetInt32(shipmentsReader.GetOrdinal("order_number")),
                    ShipmentDate = shipmentsReader.GetDateTime(shipmentsReader.GetOrdinal("shipment_date")),
                    Warehouse = shipmentsReader.GetStringOrEmpty("warehouse"),
                    TotalShippedQuantity = shipmentsReader.GetDecimal(shipmentsReader.GetOrdinal("shipped_quantity")),
                    EstimatedAmount = shipmentsReader.GetDecimal(shipmentsReader.GetOrdinal("estimated_amount"))
                });
            }
        }

        await using var linesCommand = connection.CreateCommand();
        linesCommand.CommandText =
            """
            SELECT line_number, item_code, description, quantity, unit_of_measure, unit_price, line_total, source_summary
            FROM sales_invoice_draft_lines
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND draft_id = @draftId
            ORDER BY line_number;
            """;
        linesCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        linesCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
        linesCommand.Parameters.AddWithValue("@draftId", draft.DraftId.ToString());

        var lines = new List<SalesInvoiceDraftLineDto>();
        await using (var linesReader = await linesCommand.ExecuteReaderAsync(cancellationToken))
        {
            while (await linesReader.ReadAsync(cancellationToken))
            {
                lines.Add(new SalesInvoiceDraftLineDto
                {
                    LineNumber = linesReader.GetInt32(linesReader.GetOrdinal("line_number")),
                    ItemCode = linesReader.GetStringOrEmpty("item_code"),
                    Description = linesReader.GetStringOrEmpty("description"),
                    Quantity = linesReader.GetDecimal(linesReader.GetOrdinal("quantity")),
                    UnitOfMeasure = linesReader.GetStringOrEmpty("unit_of_measure"),
                    UnitPrice = linesReader.GetDecimal(linesReader.GetOrdinal("unit_price")),
                    LineTotal = linesReader.GetDecimal(linesReader.GetOrdinal("line_total")),
                    SourceSummary = linesReader.GetStringOrEmpty("source_summary")
                });
            }
        }

        draft.Shipments = shipments;
        draft.Lines = lines;
        return draft;
    }

    public async Task<SalesInvoiceSearchResultDto> SearchInvoicesAsync(
        Guid tenantId,
        Guid companyId,
        SalesPreInvoiceFilter filter,
        CancellationToken cancellationToken = default)
    {
        if (!_saasConnectionFactory.IsConfigured)
        {
            return new SalesInvoiceSearchResultDto();
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);

        var pageSize = Math.Clamp(filter.PageSize, 10, 200);
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
                WHERE si.tenant_id = @tenantId
                  AND si.company_id = @companyId
                  AND COALESCE(si.is_deleted, 0) = 0
                  AND (
                        @search = ''
                        OR CAST(si.invoice_number AS CHAR) LIKE @likeSearch
                        OR CAST(si.draft_number AS CHAR) LIKE @likeSearch
                        OR si.client_name LIKE @likeSearch
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
                return new SalesInvoiceSearchResultDto();
            }

            await using var command = connection.CreateCommand();
            var orderBy = BuildInvoiceSearchOrderByClause(filter);
            command.CommandText =
                $"""
                SELECT si.invoice_id,
                       si.invoice_series,
                       si.invoice_number,
                       si.draft_number,
                       COALESCE(si.origin, 'saas') AS origin,
                       si.client_code,
                       si.client_name,
                       si.issue_date,
                       si.due_date,
                       si.status,
                       si.payment_status,
                       si.shipment_count,
                       si.total_quantity,
                       si.subtotal_amount,
                       si.tax_amount,
                       si.total_amount,
                       si.amount_paid,
                       si.outstanding_amount,
                       si.accounting_status,
                       si.notes
                FROM sales_invoices si
                WHERE si.tenant_id = @tenantId
                  AND si.company_id = @companyId
                  AND COALESCE(si.is_deleted, 0) = 0
                  AND (
                        @search = ''
                        OR CAST(si.invoice_number AS CHAR) LIKE @likeSearch
                        OR CAST(si.draft_number AS CHAR) LIKE @likeSearch
                        OR si.client_name LIKE @likeSearch
                        OR COALESCE(si.notes, '') LIKE @likeSearch
                      )
                {orderBy}
                LIMIT @pageSize OFFSET @offset;
                """;
            command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            command.Parameters.AddWithValue("@companyId", companyId.ToString());
            command.Parameters.AddWithValue("@search", search);
            command.Parameters.AddWithValue("@likeSearch", likeSearch);
            command.Parameters.AddWithValue("@pageSize", pageSize);
            command.Parameters.AddWithValue("@offset", offset);

            var items = new List<SalesInvoiceListItemDto>();
            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                items.Add(new SalesInvoiceListItemDto
                {
                    InvoiceId = reader.GetGuid("invoice_id"),
                    InvoiceSeries = reader.GetStringOrEmpty("invoice_series"),
                    InvoiceNumber = reader.GetInt32(reader.GetOrdinal("invoice_number")),
                    DraftNumber = reader.GetInt32(reader.GetOrdinal("draft_number")),
                    Origin = reader.GetStringOrEmpty("origin"),
                    ClientCode = reader.GetInt32(reader.GetOrdinal("client_code")),
                    ClientName = reader.GetStringOrEmpty("client_name"),
                    IssueDate = reader.GetDateTime(reader.GetOrdinal("issue_date")),
                    DueDate = reader.IsDBNull(reader.GetOrdinal("due_date")) ? null : reader.GetDateTime(reader.GetOrdinal("due_date")),
                    Status = reader.GetStringOrEmpty("status"),
                    PaymentStatus = reader.GetStringOrEmpty("payment_status"),
                    ShipmentCount = reader.GetInt32(reader.GetOrdinal("shipment_count")),
                    TotalQuantity = reader.GetDecimal(reader.GetOrdinal("total_quantity")),
                    SubtotalAmount = reader.GetDecimal(reader.GetOrdinal("subtotal_amount")),
                    TaxAmount = reader.GetDecimal(reader.GetOrdinal("tax_amount")),
                    TotalAmount = reader.GetDecimal(reader.GetOrdinal("total_amount")),
                    AmountPaid = reader.GetDecimal(reader.GetOrdinal("amount_paid")),
                    OutstandingAmount = reader.GetDecimal(reader.GetOrdinal("outstanding_amount")),
                    AccountingStatus = reader.GetStringOrEmpty("accounting_status"),
                    Notes = reader.GetStringOrEmpty("notes")
                });
            }

            return new SalesInvoiceSearchResultDto
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }

    public async Task<SalesInvoiceDto?> GetInvoiceByNumberAsync(
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
                   si.draft_id,
                   si.draft_series,
                   si.draft_number,
                   COALESCE(si.origin, 'saas') AS origin,
                   si.client_code,
                   si.client_name,
                   si.client_tax_id,
                   si.issue_date,
                   si.due_date,
                   si.status,
                   si.payment_status,
                   si.shipment_count,
                   si.total_quantity,
                   si.subtotal_amount,
                   si.tax_amount,
                   si.total_amount,
                   si.amount_paid,
                   si.outstanding_amount,
                   si.last_payment_utc,
                   si.accounting_status,
                   si.accounting_reference,
                   si.accounting_ready_utc,
                   si.notes,
                   si.issued_utc,
                   c.name AS company_name,
                   c.legacy_center_code,
                   t.name AS tenant_name
            FROM sales_invoices si
            LEFT JOIN companies c
              ON c.id = si.company_id
             AND c.tenant_id = si.tenant_id
            LEFT JOIN tenants t
              ON t.id = si.tenant_id
            WHERE si.tenant_id = @tenantId
              AND si.company_id = @companyId
              AND COALESCE(si.is_deleted, 0) = 0
              AND si.invoice_number = @invoiceNumber
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

        var invoice = new SalesInvoiceDto
        {
            InvoiceId = reader.GetGuid("invoice_id"),
            InvoiceSeries = reader.GetStringOrEmpty("invoice_series"),
            InvoiceNumber = reader.GetInt32(reader.GetOrdinal("invoice_number")),
            DraftId = reader.GetGuid("draft_id"),
            DraftSeries = reader.GetStringOrEmpty("draft_series"),
            DraftNumber = reader.GetInt32(reader.GetOrdinal("draft_number")),
            Origin = reader.GetStringOrEmpty("origin"),
            ClientCode = reader.GetInt32(reader.GetOrdinal("client_code")),
            ClientName = reader.GetStringOrEmpty("client_name"),
            ClientTaxId = reader.GetStringOrEmpty("client_tax_id"),
            IssueDate = reader.GetDateTime(reader.GetOrdinal("issue_date")),
            DueDate = reader.IsDBNull(reader.GetOrdinal("due_date")) ? null : reader.GetDateTime(reader.GetOrdinal("due_date")),
            Status = reader.GetStringOrEmpty("status"),
            PaymentStatus = reader.GetStringOrEmpty("payment_status"),
            ShipmentCount = reader.GetInt32(reader.GetOrdinal("shipment_count")),
            TotalQuantity = reader.GetDecimal(reader.GetOrdinal("total_quantity")),
            SubtotalAmount = reader.GetDecimal(reader.GetOrdinal("subtotal_amount")),
            TaxAmount = reader.GetDecimal(reader.GetOrdinal("tax_amount")),
            TotalAmount = reader.GetDecimal(reader.GetOrdinal("total_amount")),
            AmountPaid = reader.GetDecimal(reader.GetOrdinal("amount_paid")),
            OutstandingAmount = reader.GetDecimal(reader.GetOrdinal("outstanding_amount")),
            LastPaymentUtc = reader.IsDBNull(reader.GetOrdinal("last_payment_utc")) ? null : reader.GetDateTime(reader.GetOrdinal("last_payment_utc")),
            AccountingStatus = reader.GetStringOrEmpty("accounting_status"),
            AccountingReference = reader.GetStringOrEmpty("accounting_reference"),
            AccountingReadyUtc = reader.IsDBNull(reader.GetOrdinal("accounting_ready_utc")) ? null : reader.GetDateTime(reader.GetOrdinal("accounting_ready_utc")),
            Notes = reader.GetStringOrEmpty("notes"),
            IssuedUtc = reader.GetDateTime(reader.GetOrdinal("issued_utc")),
            CompanyName = reader.GetStringOrEmpty("company_name"),
            CompanyLegacyCenterCode = reader.GetStringOrEmpty("legacy_center_code"),
            TenantName = reader.GetStringOrEmpty("tenant_name")
        };
        await reader.DisposeAsync();

        var clientSnapshot = await GetClientSnapshotAsync(invoice.CompanyLegacyCenterCode, invoice.ClientCode, cancellationToken);
        invoice.ClientAddress = clientSnapshot?.Address ?? string.Empty;
        invoice.ClientPostalCode = clientSnapshot?.PostalCode ?? string.Empty;
        invoice.ClientCity = clientSnapshot?.City ?? string.Empty;
        invoice.ClientProvince = clientSnapshot?.Province ?? string.Empty;
        invoice.ClientCountry = clientSnapshot?.Country ?? string.Empty;

        await using var shipmentsCommand = connection.CreateCommand();
        shipmentsCommand.CommandText =
            """
            SELECT shipment_id, shipment_series, shipment_number, order_number, shipment_date, warehouse, shipped_quantity, estimated_amount
            FROM sales_invoice_shipments
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND invoice_id = @invoiceId
            ORDER BY shipment_date, shipment_number;
            """;
        shipmentsCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        shipmentsCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
        shipmentsCommand.Parameters.AddWithValue("@invoiceId", invoice.InvoiceId.ToString());

        var shipments = new List<SalesInvoiceShipmentDto>();
        await using (var shipmentsReader = await shipmentsCommand.ExecuteReaderAsync(cancellationToken))
        {
            while (await shipmentsReader.ReadAsync(cancellationToken))
            {
                shipments.Add(new SalesInvoiceShipmentDto
                {
                    ShipmentId = shipmentsReader.GetGuid("shipment_id"),
                    ShipmentSeries = shipmentsReader.GetStringOrEmpty("shipment_series"),
                    ShipmentNumber = shipmentsReader.GetInt32(shipmentsReader.GetOrdinal("shipment_number")),
                    OrderNumber = shipmentsReader.GetInt32(shipmentsReader.GetOrdinal("order_number")),
                    ShipmentDate = shipmentsReader.GetDateTime(shipmentsReader.GetOrdinal("shipment_date")),
                    Warehouse = shipmentsReader.GetStringOrEmpty("warehouse"),
                    TotalShippedQuantity = shipmentsReader.GetDecimal(shipmentsReader.GetOrdinal("shipped_quantity")),
                    EstimatedAmount = shipmentsReader.GetDecimal(shipmentsReader.GetOrdinal("estimated_amount"))
                });
            }
        }

        await using var linesCommand = connection.CreateCommand();
        linesCommand.CommandText =
            """
            SELECT line_number, item_code, description, quantity, unit_of_measure, unit_price, line_subtotal, tax_rate, tax_amount, line_total, source_summary
            FROM sales_invoice_lines
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND invoice_id = @invoiceId
            ORDER BY line_number;
            """;
        linesCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        linesCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
        linesCommand.Parameters.AddWithValue("@invoiceId", invoice.InvoiceId.ToString());

        var lines = new List<SalesInvoiceLineDto>();
        await using (var linesReader = await linesCommand.ExecuteReaderAsync(cancellationToken))
        {
            while (await linesReader.ReadAsync(cancellationToken))
            {
                lines.Add(new SalesInvoiceLineDto
                {
                    LineNumber = linesReader.GetInt32(linesReader.GetOrdinal("line_number")),
                    ItemCode = linesReader.GetStringOrEmpty("item_code"),
                    Description = linesReader.GetStringOrEmpty("description"),
                    Quantity = linesReader.GetDecimal(linesReader.GetOrdinal("quantity")),
                    UnitOfMeasure = linesReader.GetStringOrEmpty("unit_of_measure"),
                    UnitPrice = linesReader.GetDecimal(linesReader.GetOrdinal("unit_price")),
                    LineSubtotal = linesReader.GetDecimal(linesReader.GetOrdinal("line_subtotal")),
                    TaxRate = linesReader.GetDecimal(linesReader.GetOrdinal("tax_rate")),
                    TaxAmount = linesReader.GetDecimal(linesReader.GetOrdinal("tax_amount")),
                    LineTotal = linesReader.GetDecimal(linesReader.GetOrdinal("line_total")),
                    SourceSummary = linesReader.GetStringOrEmpty("source_summary")
                });
            }
        }

        var payments = new List<SalesInvoicePaymentDto>();
        await using (var paymentsCommand = connection.CreateCommand())
        {
            paymentsCommand.CommandText =
                """
                SELECT payment_id, payment_number, payment_date, amount, method, reference, notes, created_utc
                FROM sales_invoice_payments
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND invoice_id = @invoiceId
                ORDER BY payment_number;
                """;
            paymentsCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            paymentsCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
            paymentsCommand.Parameters.AddWithValue("@invoiceId", invoice.InvoiceId.ToString());

            await using var paymentsReader = await paymentsCommand.ExecuteReaderAsync(cancellationToken);
            while (await paymentsReader.ReadAsync(cancellationToken))
            {
                payments.Add(new SalesInvoicePaymentDto
                {
                    PaymentId = paymentsReader.GetGuid("payment_id"),
                    PaymentNumber = paymentsReader.GetInt32(paymentsReader.GetOrdinal("payment_number")),
                    PaymentDate = paymentsReader.GetDateTime(paymentsReader.GetOrdinal("payment_date")),
                    Amount = paymentsReader.GetDecimal(paymentsReader.GetOrdinal("amount")),
                    Method = paymentsReader.GetStringOrEmpty("method"),
                    Reference = paymentsReader.GetStringOrEmpty("reference"),
                    Notes = paymentsReader.GetStringOrEmpty("notes"),
                    CreatedUtc = paymentsReader.GetDateTime(paymentsReader.GetOrdinal("created_utc"))
                });
            }
        }

        invoice.Shipments = shipments;
        invoice.Lines = lines;
        invoice.Payments = payments;
        return invoice;
    }

    public async Task<LegacySyncModuleRunResult> RunAsync(
        LegacySyncModuleContext context,
        CancellationToken cancellationToken = default)
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
        var headers = await LoadLegacyOrderHeadersAsync(legacyConnection, context.CompanyLegacyCenterCode, cancellationToken);
        var linesByOrder = await LoadLegacyOrderLinesAsync(legacyConnection, context.CompanyLegacyCenterCode, cancellationToken);
        var existingOrders = await LoadExistingSalesOrderOriginsAsync(saasConnection, context.TenantId, context.CompanyId, cancellationToken);

        var inserted = 0;
        var updated = 0;
        var skipped = 0;
        var mappings = new List<LegacySyncMappingRecord>();
        var errors = new List<LegacySyncErrorRecord>();
        var seenLegacyOrderNumbers = new HashSet<int>();

        foreach (var header in headers)
        {
            if (!linesByOrder.TryGetValue(header.OrderNumber, out var legacyLines) || legacyLines.Count == 0)
            {
                skipped++;
                continue;
            }

            var normalizedLines = NormalizeLegacyLines(legacyLines);
            if (normalizedLines.Count == 0)
            {
                skipped++;
                continue;
            }

            if (existingOrders.TryGetValue(header.OrderNumber, out var existingOrigin) &&
                !string.Equals(existingOrigin, SalesOrderOrigins.Legacy, StringComparison.OrdinalIgnoreCase))
            {
                skipped++;
                errors.Add(new LegacySyncErrorRecord
                {
                    Stage = "UpsertOrder",
                    LegacyEntityKey = $"{context.CompanyLegacyCenterCode}/C/{header.OrderNumber}",
                    ErrorMessage = "Existe un pedido SaaS con el mismo número y no se puede sobreescribir desde la sincronización legacy.",
                    Payload = $"OrderNumber={header.OrderNumber}; Origin={existingOrigin}"
                });
                continue;
            }

            await using var transaction = await saasConnection.BeginTransactionAsync(cancellationToken);
            try
            {
                var nowUtc = DateTime.UtcNow;
                var status = DetermineLegacyImportedStatus(normalizedLines);
                var exists = existingOrders.ContainsKey(header.OrderNumber);

                await UpsertImportedSalesOrderHeaderAsync(
                    saasConnection,
                    transaction,
                    context.TenantId,
                    context.CompanyId,
                    context.CompanyLegacyCenterCode,
                    header,
                    status,
                    nowUtc,
                    cancellationToken);

                await ReplaceImportedSalesOrderLinesAsync(
                    saasConnection,
                    transaction,
                    context.TenantId,
                    context.CompanyId,
                    context.CompanyLegacyCenterCode,
                    header.OrderNumber,
                    normalizedLines,
                    nowUtc,
                    cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                if (exists)
                {
                    updated++;
                }
                else
                {
                    inserted++;
                }

                existingOrders[header.OrderNumber] = SalesOrderOrigins.Legacy;
                seenLegacyOrderNumbers.Add(header.OrderNumber);

                mappings.Add(new LegacySyncMappingRecord
                {
                    LegacyCenterCode = context.CompanyLegacyCenterCode,
                    LegacyDocumentType = "C",
                    LegacyDocumentNumber = header.OrderNumber.ToString(),
                    TargetEntityName = "SalesOrder",
                    TargetEntityId = header.OrderNumber.ToString()
                });

                foreach (var line in normalizedLines)
                {
                    mappings.Add(new LegacySyncMappingRecord
                    {
                        LegacyCenterCode = context.CompanyLegacyCenterCode,
                        LegacyDocumentType = "C",
                        LegacyDocumentNumber = header.OrderNumber.ToString(),
                        LegacyLineNumber = line.LineNumber,
                        TargetEntityName = "SalesOrderLine",
                        TargetEntityId = $"{header.OrderNumber}:{line.LineNumber}"
                    });
                }
            }
            catch (Exception exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                skipped++;
                errors.Add(new LegacySyncErrorRecord
                {
                    Stage = "UpsertOrder",
                    LegacyEntityKey = $"{context.CompanyLegacyCenterCode}/C/{header.OrderNumber}",
                    ErrorMessage = exception.Message,
                    Payload = $"OrderNumber={header.OrderNumber}; ClientCode={header.ClientCode}"
                });
            }
        }

        updated += await MarkMissingImportedSalesOrdersAsDeletedAsync(
            saasConnection,
            context.TenantId,
            context.CompanyId,
            seenLegacyOrderNumbers,
            cancellationToken);

        var checkpointValue = $"FULL@{DateTime.UtcNow:O}";
        return new LegacySyncModuleRunResult
        {
            RecordsInserted = inserted,
            RecordsUpdated = updated,
            RecordsSkipped = skipped,
            NewCheckpointValue = checkpointValue,
            Summary = $"Headers={headers.Count}; Insertados={inserted}; Actualizados={updated}; Omitidos={skipped}; Errores={errors.Count}",
            Mappings = mappings,
            Errors = errors
        };
    }

    public async Task<int> SaveAsync(SaveSalesOrderCommand command, CancellationToken cancellationToken = default)
    {
        if (!_saasConnectionFactory.IsConfigured || !_legacyConnectionFactory.IsConfigured)
        {
            return 0;
        }

        await EnsureCompanyAccessAsync(command.TenantId, command.CompanyId, cancellationToken);
        EnsureTenantWriteAccess();
        NormalizeAndValidate(command);

        await using var connection = await _saasConnectionFactory.OpenConnectionAsync(cancellationToken);
        await EnsureSalesOrdersWriteAllowedAsync(connection, command.TenantId, command.CompanyId, cancellationToken);
        var centerCode = await ResolveCompanyCenterCodeAsync(connection, command.TenantId, command.CompanyId, cancellationToken);
        await using var legacyConnection = await _legacyConnectionFactory.OpenConnectionAsync(cancellationToken);
        var clientSnapshot = await GetClientSnapshotAsync(legacyConnection, centerCode, command.ClientCode, cancellationToken);
        if (clientSnapshot is null)
        {
            throw new InvalidOperationException("El cliente seleccionado no existe en la empresa activa.");
        }

        SalesOrderDetailDto? previous = null;
        if (command.OrderNumber.HasValue)
        {
            previous = await GetByOrderNumberAsync(command.TenantId, command.CompanyId, command.OrderNumber.Value, cancellationToken);
            if (previous is null)
            {
                throw new InvalidOperationException("No se ha encontrado el pedido de cliente que intentas modificar.");
            }

            if (string.Equals(previous.Origin, SalesOrderOrigins.Legacy, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("Este pedido está sincronizado desde legacy y no se puede editar desde la web mientras el módulo siga en convivencia.");
            }
        }

        var existingLineState = previous?.Lines.ToDictionary(line => line.LineNumber) ?? [];
        ValidateAgainstShippedLines(existingLineState, command);

        var orderNumber = command.OrderNumber ?? await GetNextOrderNumberAsync(connection, command.TenantId, command.CompanyId, cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        if (command.OrderNumber.HasValue)
        {
            await using var updateCommand = connection.CreateCommand();
            updateCommand.Transaction = transaction;
            updateCommand.CommandText =
                """
                UPDATE sales_orders
                SET client_code = @clientCode,
                    client_name = @clientName,
                    client_tax_id = @clientTaxId,
                    document_date = @documentDate,
                    requested_date = @requestedDate,
                    status = @status,
                    notes = @notes,
                    updated_utc = @updatedUtc
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND order_number = @orderNumber;
                """;
            FillHeaderParameters(updateCommand, command, orderNumber, clientSnapshot);
            updateCommand.Parameters.AddWithValue("@updatedUtc", DateTime.UtcNow);
            if (await updateCommand.ExecuteNonQueryAsync(cancellationToken) == 0)
            {
                throw new InvalidOperationException("No se ha podido actualizar el pedido de cliente.");
            }
        }
        else
        {
            await using var insertCommand = connection.CreateCommand();
            insertCommand.Transaction = transaction;
            insertCommand.CommandText =
                """
                INSERT INTO sales_orders (
                    tenant_id,
                    company_id,
                    order_number,
                    client_code,
                    client_name,
                    client_tax_id,
                    document_date,
                    requested_date,
                    status,
                    notes,
                    origin,
                    synced_utc,
                    created_utc,
                    updated_utc)
                VALUES (
                    @tenantId,
                    @companyId,
                    @orderNumber,
                    @clientCode,
                    @clientName,
                    @clientTaxId,
                    @documentDate,
                    @requestedDate,
                    @status,
                    @notes,
                    @origin,
                    NULL,
                    @createdUtc,
                    @updatedUtc);
                """;
            FillHeaderParameters(insertCommand, command, orderNumber, clientSnapshot);
            insertCommand.Parameters.AddWithValue("@origin", SalesOrderOrigins.Saas);
            insertCommand.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
            insertCommand.Parameters.AddWithValue("@updatedUtc", DateTime.UtcNow);
            await insertCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        await using (var deleteLinesCommand = connection.CreateCommand())
        {
            deleteLinesCommand.Transaction = transaction;
            deleteLinesCommand.CommandText =
                """
                DELETE FROM sales_order_lines
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND order_number = @orderNumber;
                """;
            deleteLinesCommand.Parameters.AddWithValue("@tenantId", command.TenantId.ToString());
            deleteLinesCommand.Parameters.AddWithValue("@companyId", command.CompanyId.ToString());
            deleteLinesCommand.Parameters.AddWithValue("@orderNumber", orderNumber);
            await deleteLinesCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        foreach (var line in command.Lines)
        {
            var previousLine = existingLineState.GetValueOrDefault(line.LineNumber);
            await using var insertLineCommand = connection.CreateCommand();
            insertLineCommand.Transaction = transaction;
            insertLineCommand.CommandText =
                """
                INSERT INTO sales_order_lines (
                    tenant_id,
                    company_id,
                    order_number,
                    line_number,
                    item_code,
                    description,
                    quantity,
                    shipped_quantity,
                    unit_of_measure,
                    unit_price,
                    requested_date,
                    last_shipped_utc,
                    notes)
                VALUES (
                    @tenantId,
                    @companyId,
                    @orderNumber,
                    @lineNumber,
                    @itemCode,
                    @description,
                    @quantity,
                    @shippedQuantity,
                    @unitOfMeasure,
                    @unitPrice,
                    @requestedDate,
                    @lastShippedUtc,
                    @notes);
                """;
            insertLineCommand.Parameters.AddWithValue("@tenantId", command.TenantId.ToString());
            insertLineCommand.Parameters.AddWithValue("@companyId", command.CompanyId.ToString());
            insertLineCommand.Parameters.AddWithValue("@orderNumber", orderNumber);
            insertLineCommand.Parameters.AddWithValue("@lineNumber", line.LineNumber);
            insertLineCommand.Parameters.AddWithValue("@itemCode", DbValue(line.ItemCode));
            insertLineCommand.Parameters.AddWithValue("@description", line.Description);
            insertLineCommand.Parameters.AddWithValue("@quantity", line.Quantity);
            insertLineCommand.Parameters.AddWithValue("@shippedQuantity", previousLine?.ShippedQuantity ?? 0m);
            insertLineCommand.Parameters.AddWithValue("@unitOfMeasure", DbValue(line.UnitOfMeasure));
            insertLineCommand.Parameters.AddWithValue("@unitPrice", line.UnitPrice);
            insertLineCommand.Parameters.AddWithValue("@requestedDate", DbValue(line.RequestedDate));
            insertLineCommand.Parameters.AddWithValue("@lastShippedUtc", DbValue(previousLine?.LastShippedUtc));
            insertLineCommand.Parameters.AddWithValue("@notes", DbValue(line.Notes));
            await insertLineCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        await transaction.CommitAsync(cancellationToken);
        await AuditAsync(previous, command, clientSnapshot, orderNumber, cancellationToken);
        return orderNumber;
    }

    public async Task ShipAsync(RegisterSalesOrderShipmentCommand command, CancellationToken cancellationToken = default)
    {
        if (!_saasConnectionFactory.IsConfigured)
        {
            return;
        }

        await EnsureCompanyAccessAsync(command.TenantId, command.CompanyId, cancellationToken);
        EnsureTenantWriteAccess();
        NormalizeAndValidateShipment(command);

        await using var connection = await _saasConnectionFactory.OpenConnectionAsync(cancellationToken);
        await EnsureSalesShipmentsWriteAllowedAsync(connection, command.TenantId, command.CompanyId, cancellationToken);

        var currentOrder = await GetByOrderNumberAsync(command.TenantId, command.CompanyId, command.OrderNumber, cancellationToken)
            ?? throw new InvalidOperationException("No se ha encontrado el pedido de cliente a expedir.");

        if (string.Equals(currentOrder.Origin, SalesOrderOrigins.Legacy, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Este pedido está sincronizado desde legacy y no se puede expedir desde la web mientras el módulo siga en convivencia.");
        }

        if (string.Equals(currentOrder.Status, SalesOrderStatuses.Cancelled, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("No puedes expedir un pedido cancelado.");
        }

        var lineLookup = currentOrder.Lines.ToDictionary(line => line.LineNumber);
        foreach (var shippedLine in command.Lines)
        {
            if (!lineLookup.TryGetValue(shippedLine.LineNumber, out var currentLine))
            {
                throw new InvalidOperationException($"La línea {shippedLine.LineNumber} ya no existe en el pedido.");
            }

            if (shippedLine.ShippedQuantity > currentLine.PendingQuantity)
            {
                throw new InvalidOperationException($"La expedición de la línea {shippedLine.LineNumber} supera la cantidad pendiente.");
            }
        }

        foreach (var groupedLine in command.Lines.GroupBy(line => line.LineNumber))
        {
            var currentLine = lineLookup[groupedLine.Key];
            var currentStock = await GetCurrentStockAsync(
                connection,
                command.TenantId,
                command.CompanyId,
                command.Warehouse,
                currentLine.ItemCode,
                currentLine.Description,
                cancellationToken);

            var requestedQuantity = groupedLine.Sum(line => line.ShippedQuantity);
            if (currentStock < requestedQuantity)
            {
                throw new InvalidOperationException($"No hay stock suficiente en '{command.Warehouse}' para '{currentLine.Description}'. Disponible: {currentStock:0.###}, solicitado: {requestedQuantity:0.###}.");
            }
        }

        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);
        var shipmentId = Guid.NewGuid();
        var shipmentNumber = await GetNextShipmentNumberAsync(connection, command.TenantId, command.CompanyId, cancellationToken);
        var shipmentSeries = BuildShipmentSeries(currentOrder.CompanyLegacyCenterCode);

        await using (var insertShipmentCommand = connection.CreateCommand())
        {
            insertShipmentCommand.Transaction = transaction;
            insertShipmentCommand.CommandText =
                """
                INSERT INTO sales_order_shipments (
                    shipment_id,
                    shipment_series,
                    shipment_number,
                    tenant_id,
                    company_id,
                    order_number,
                    shipment_date,
                    warehouse,
                    invoice_status,
                    invoice_reference,
                    invoice_ready_utc,
                    notes,
                    created_utc)
                VALUES (
                    @shipmentId,
                    @shipmentSeries,
                    @shipmentNumber,
                    @tenantId,
                    @companyId,
                    @orderNumber,
                    @shipmentDate,
                    @warehouse,
                    @invoiceStatus,
                    @invoiceReference,
                    @invoiceReadyUtc,
                    @notes,
                    @createdUtc);
                """;
            insertShipmentCommand.Parameters.AddWithValue("@shipmentId", shipmentId.ToString());
            insertShipmentCommand.Parameters.AddWithValue("@shipmentSeries", shipmentSeries);
            insertShipmentCommand.Parameters.AddWithValue("@shipmentNumber", shipmentNumber);
            insertShipmentCommand.Parameters.AddWithValue("@tenantId", command.TenantId.ToString());
            insertShipmentCommand.Parameters.AddWithValue("@companyId", command.CompanyId.ToString());
            insertShipmentCommand.Parameters.AddWithValue("@orderNumber", command.OrderNumber);
            insertShipmentCommand.Parameters.AddWithValue("@shipmentDate", command.ShipmentDate.Date);
            insertShipmentCommand.Parameters.AddWithValue("@warehouse", DbValue(command.Warehouse));
            insertShipmentCommand.Parameters.AddWithValue("@invoiceStatus", "Pending");
            insertShipmentCommand.Parameters.AddWithValue("@invoiceReference", DBNull.Value);
            insertShipmentCommand.Parameters.AddWithValue("@invoiceReadyUtc", DateTime.UtcNow);
            insertShipmentCommand.Parameters.AddWithValue("@notes", DbValue(command.Notes));
            insertShipmentCommand.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
            await insertShipmentCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        foreach (var shippedLine in command.Lines)
        {
            var currentLine = lineLookup[shippedLine.LineNumber];

            await using (var insertShipmentLineCommand = connection.CreateCommand())
            {
                insertShipmentLineCommand.Transaction = transaction;
                insertShipmentLineCommand.CommandText =
                    """
                    INSERT INTO sales_order_shipment_lines (
                        shipment_id,
                        tenant_id,
                        company_id,
                        order_number,
                        line_number,
                        description,
                        shipped_quantity)
                    VALUES (
                        @shipmentId,
                        @tenantId,
                        @companyId,
                        @orderNumber,
                        @lineNumber,
                        @description,
                        @shippedQuantity);
                    """;
                insertShipmentLineCommand.Parameters.AddWithValue("@shipmentId", shipmentId.ToString());
                insertShipmentLineCommand.Parameters.AddWithValue("@tenantId", command.TenantId.ToString());
                insertShipmentLineCommand.Parameters.AddWithValue("@companyId", command.CompanyId.ToString());
                insertShipmentLineCommand.Parameters.AddWithValue("@orderNumber", command.OrderNumber);
                insertShipmentLineCommand.Parameters.AddWithValue("@lineNumber", shippedLine.LineNumber);
                insertShipmentLineCommand.Parameters.AddWithValue("@description", currentLine.Description);
                insertShipmentLineCommand.Parameters.AddWithValue("@shippedQuantity", shippedLine.ShippedQuantity);
                await insertShipmentLineCommand.ExecuteNonQueryAsync(cancellationToken);
            }

            await using (var insertMovementCommand = connection.CreateCommand())
            {
                insertMovementCommand.Transaction = transaction;
                insertMovementCommand.CommandText =
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
                        @quantity,
                        @unitOfMeasure,
                        @sourceDocumentType,
                        @sourceDocumentId,
                        @sourceDocumentNumber,
                        @sourceLineNumber,
                        @notes,
                        @createdUtc);
                    """;
                insertMovementCommand.Parameters.AddWithValue("@movementId", Guid.NewGuid().ToString());
                insertMovementCommand.Parameters.AddWithValue("@tenantId", command.TenantId.ToString());
                insertMovementCommand.Parameters.AddWithValue("@companyId", command.CompanyId.ToString());
                insertMovementCommand.Parameters.AddWithValue("@movementType", StockMovementTypes.OutboundSalesShipment);
                insertMovementCommand.Parameters.AddWithValue("@movementDate", command.ShipmentDate.Date);
                insertMovementCommand.Parameters.AddWithValue("@warehouse", DbValue(command.Warehouse));
                insertMovementCommand.Parameters.AddWithValue("@itemCode", DbValue(currentLine.ItemCode));
                insertMovementCommand.Parameters.AddWithValue("@itemDescription", currentLine.Description);
                insertMovementCommand.Parameters.AddWithValue("@quantity", shippedLine.ShippedQuantity);
                insertMovementCommand.Parameters.AddWithValue("@unitOfMeasure", DbValue(currentLine.UnitOfMeasure));
                insertMovementCommand.Parameters.AddWithValue("@sourceDocumentType", "SalesShipment");
                insertMovementCommand.Parameters.AddWithValue("@sourceDocumentId", shipmentId.ToString());
                insertMovementCommand.Parameters.AddWithValue("@sourceDocumentNumber", shipmentNumber);
                insertMovementCommand.Parameters.AddWithValue("@sourceLineNumber", shippedLine.LineNumber);
                insertMovementCommand.Parameters.AddWithValue("@notes", DbValue(command.Notes));
                insertMovementCommand.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
                await insertMovementCommand.ExecuteNonQueryAsync(cancellationToken);
            }

            await using var updateLineCommand = connection.CreateCommand();
            updateLineCommand.Transaction = transaction;
            updateLineCommand.CommandText =
                """
                UPDATE sales_order_lines
                SET shipped_quantity = shipped_quantity + @shippedQuantity,
                    last_shipped_utc = @lastShippedUtc
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND order_number = @orderNumber
                  AND line_number = @lineNumber;
                """;
            updateLineCommand.Parameters.AddWithValue("@shippedQuantity", shippedLine.ShippedQuantity);
            updateLineCommand.Parameters.AddWithValue("@lastShippedUtc", DateTime.UtcNow);
            updateLineCommand.Parameters.AddWithValue("@tenantId", command.TenantId.ToString());
            updateLineCommand.Parameters.AddWithValue("@companyId", command.CompanyId.ToString());
            updateLineCommand.Parameters.AddWithValue("@orderNumber", command.OrderNumber);
            updateLineCommand.Parameters.AddWithValue("@lineNumber", shippedLine.LineNumber);
            await updateLineCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        var refreshedLines = currentOrder.Lines
            .Select(line =>
            {
                var shippedNow = command.Lines.FirstOrDefault(candidate => candidate.LineNumber == line.LineNumber)?.ShippedQuantity ?? 0m;
                return new SalesOrderLineDto
                {
                    LineNumber = line.LineNumber,
                    ItemCode = line.ItemCode,
                    Description = line.Description,
                    Quantity = line.Quantity,
                    UnitOfMeasure = line.UnitOfMeasure,
                    UnitPrice = line.UnitPrice,
                    RequestedDate = line.RequestedDate,
                    Notes = line.Notes,
                    ShippedQuantity = line.ShippedQuantity + shippedNow,
                    LastShippedUtc = shippedNow > 0 ? DateTime.UtcNow : line.LastShippedUtc
                };
            })
            .ToArray();

        var newStatus = DetermineStatusAfterShipment(currentOrder.Status, refreshedLines);
        await using (var updateOrderCommand = connection.CreateCommand())
        {
            updateOrderCommand.Transaction = transaction;
            updateOrderCommand.CommandText =
                """
                UPDATE sales_orders
                SET status = @status,
                    updated_utc = @updatedUtc
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND order_number = @orderNumber;
                """;
            updateOrderCommand.Parameters.AddWithValue("@status", newStatus);
            updateOrderCommand.Parameters.AddWithValue("@updatedUtc", DateTime.UtcNow);
            updateOrderCommand.Parameters.AddWithValue("@tenantId", command.TenantId.ToString());
            updateOrderCommand.Parameters.AddWithValue("@companyId", command.CompanyId.ToString());
            updateOrderCommand.Parameters.AddWithValue("@orderNumber", command.OrderNumber);
            await updateOrderCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        await transaction.CommitAsync(cancellationToken);

        var detailSummary = string.Join("; ", command.Lines.Select(line =>
            $"Linea {line.LineNumber}: -{line.ShippedQuantity:0.###}"));
        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = command.TenantId,
            CompanyId = command.CompanyId,
            UserId = _currentUserContext.UserId,
            Action = "SalesOrderShipmentRegistered",
            EntityName = "PedidoVenta",
            EntityId = command.OrderNumber.ToString(),
            Details = $"Salida={shipmentSeries}/{shipmentNumber:000000}; Fecha={command.ShipmentDate:yyyy-MM-dd}; Almacen={command.Warehouse}; Estado={newStatus}; Facturacion=Pending; {detailSummary}{(string.IsNullOrWhiteSpace(command.Notes) ? string.Empty : $"; Notas={command.Notes}")}"
        }, cancellationToken);
    }

    public async Task<int> CreateInvoiceDraftAsync(CreateSalesInvoiceDraftCommand command, CancellationToken cancellationToken = default)
    {
        if (!_saasConnectionFactory.IsConfigured)
        {
            return 0;
        }

        await EnsureCompanyAccessAsync(command.TenantId, command.CompanyId, cancellationToken);
        EnsureTenantWriteAccess();
        NormalizeAndValidateInvoiceDraft(command);

        await using var connection = await _saasConnectionFactory.OpenConnectionAsync(cancellationToken);
        await EnsureSalesShipmentsWriteAllowedAsync(connection, command.TenantId, command.CompanyId, cancellationToken);
        var centerCode = await ResolveCompanyCenterCodeAsync(connection, command.TenantId, command.CompanyId, cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        var selection = await LoadPendingShipmentSelectionAsync(
            connection,
            transaction,
            command.TenantId,
            command.CompanyId,
            command.ShipmentIds,
            cancellationToken);

        if (selection.Count != command.ShipmentIds.Count)
        {
            throw new InvalidOperationException("Alguno de los albaranes seleccionados ya no está pendiente de facturar.");
        }

        var firstShipment = selection[0];
        if (selection.Any(candidate => candidate.ClientCode != firstShipment.ClientCode))
        {
            throw new InvalidOperationException("Solo puedes agrupar albaranes del mismo cliente en el mismo borrador.");
        }

        var draftId = Guid.NewGuid();
        var draftNumber = await GetNextInvoiceDraftNumberAsync(connection, transaction, command.TenantId, command.CompanyId, cancellationToken);
        var draftSeries = BuildInvoiceDraftSeries(centerCode);
        var draftReference = $"{draftSeries}/{draftNumber:000000}";
        var nowUtc = DateTime.UtcNow;

        var aggregatedLines = selection
            .SelectMany(shipment => shipment.Lines.Select(line => new
            {
                shipment.DisplayNumber,
                line.ItemCode,
                line.Description,
                line.UnitOfMeasure,
                line.UnitPrice,
                line.Quantity
            }))
            .GroupBy(item => new
            {
                item.ItemCode,
                item.Description,
                item.UnitOfMeasure,
                item.UnitPrice
            })
            .Select((group, index) => new DraftAggregatedLine(
                LineNumber: index + 1,
                ItemCode: group.Key.ItemCode,
                Description: group.Key.Description,
                UnitOfMeasure: group.Key.UnitOfMeasure,
                UnitPrice: group.Key.UnitPrice,
                Quantity: decimal.Round(group.Sum(item => item.Quantity), 3, MidpointRounding.AwayFromZero),
                LineTotal: decimal.Round(group.Sum(item => item.Quantity * item.UnitPrice), 2, MidpointRounding.AwayFromZero),
                SourceSummary: string.Join(", ", group.Select(item => item.DisplayNumber).Distinct().OrderBy(value => value))))
            .ToArray();

        var totalQuantity = decimal.Round(selection.Sum(item => item.TotalShippedQuantity), 3, MidpointRounding.AwayFromZero);
        var totalAmount = decimal.Round(aggregatedLines.Sum(line => line.LineTotal), 2, MidpointRounding.AwayFromZero);

        await using (var insertDraftCommand = connection.CreateCommand())
        {
            insertDraftCommand.Transaction = transaction;
            insertDraftCommand.CommandText =
                """
                INSERT INTO sales_invoice_drafts (
                    draft_id,
                    draft_series,
                    draft_number,
                    tenant_id,
                    company_id,
                    client_code,
                    client_name,
                    client_tax_id,
                    issue_date,
                    due_date,
                    status,
                    shipment_count,
                    total_quantity,
                    total_amount,
                    notes,
                    created_utc,
                    updated_utc)
                VALUES (
                    @draftId,
                    @draftSeries,
                    @draftNumber,
                    @tenantId,
                    @companyId,
                    @clientCode,
                    @clientName,
                    @clientTaxId,
                    @issueDate,
                    @dueDate,
                    @status,
                    @shipmentCount,
                    @totalQuantity,
                    @totalAmount,
                    @notes,
                    @createdUtc,
                    @updatedUtc);
                """;
            insertDraftCommand.Parameters.AddWithValue("@draftId", draftId.ToString());
            insertDraftCommand.Parameters.AddWithValue("@draftSeries", draftSeries);
            insertDraftCommand.Parameters.AddWithValue("@draftNumber", draftNumber);
            insertDraftCommand.Parameters.AddWithValue("@tenantId", command.TenantId.ToString());
            insertDraftCommand.Parameters.AddWithValue("@companyId", command.CompanyId.ToString());
            insertDraftCommand.Parameters.AddWithValue("@clientCode", firstShipment.ClientCode);
            insertDraftCommand.Parameters.AddWithValue("@clientName", firstShipment.ClientName);
            insertDraftCommand.Parameters.AddWithValue("@clientTaxId", DbValue(firstShipment.ClientTaxId));
            insertDraftCommand.Parameters.AddWithValue("@issueDate", command.IssueDate.Date);
            insertDraftCommand.Parameters.AddWithValue("@dueDate", DbValue(command.DueDate?.Date));
            insertDraftCommand.Parameters.AddWithValue("@status", SalesInvoiceDraftStatuses.Draft);
            insertDraftCommand.Parameters.AddWithValue("@shipmentCount", selection.Count);
            insertDraftCommand.Parameters.AddWithValue("@totalQuantity", totalQuantity);
            insertDraftCommand.Parameters.AddWithValue("@totalAmount", totalAmount);
            insertDraftCommand.Parameters.AddWithValue("@notes", DbValue(command.Notes));
            insertDraftCommand.Parameters.AddWithValue("@createdUtc", nowUtc);
            insertDraftCommand.Parameters.AddWithValue("@updatedUtc", nowUtc);
            await insertDraftCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        foreach (var shipment in selection)
        {
            await using (var insertShipmentCommand = connection.CreateCommand())
            {
                insertShipmentCommand.Transaction = transaction;
                insertShipmentCommand.CommandText =
                    """
                    INSERT INTO sales_invoice_draft_shipments (
                        draft_id,
                        tenant_id,
                        company_id,
                        shipment_id,
                        shipment_series,
                        shipment_number,
                        order_number,
                        shipment_date,
                        warehouse,
                        shipped_quantity,
                        estimated_amount)
                    VALUES (
                        @draftId,
                        @tenantId,
                        @companyId,
                        @shipmentId,
                        @shipmentSeries,
                        @shipmentNumber,
                        @orderNumber,
                        @shipmentDate,
                        @warehouse,
                        @shippedQuantity,
                        @estimatedAmount);
                    """;
                insertShipmentCommand.Parameters.AddWithValue("@draftId", draftId.ToString());
                insertShipmentCommand.Parameters.AddWithValue("@tenantId", command.TenantId.ToString());
                insertShipmentCommand.Parameters.AddWithValue("@companyId", command.CompanyId.ToString());
                insertShipmentCommand.Parameters.AddWithValue("@shipmentId", shipment.ShipmentId.ToString());
                insertShipmentCommand.Parameters.AddWithValue("@shipmentSeries", DbValue(shipment.ShipmentSeries));
                insertShipmentCommand.Parameters.AddWithValue("@shipmentNumber", shipment.ShipmentNumber);
                insertShipmentCommand.Parameters.AddWithValue("@orderNumber", shipment.OrderNumber);
                insertShipmentCommand.Parameters.AddWithValue("@shipmentDate", shipment.ShipmentDate.Date);
                insertShipmentCommand.Parameters.AddWithValue("@warehouse", DbValue(shipment.Warehouse));
                insertShipmentCommand.Parameters.AddWithValue("@shippedQuantity", shipment.TotalShippedQuantity);
                insertShipmentCommand.Parameters.AddWithValue("@estimatedAmount", shipment.EstimatedAmount);
                await insertShipmentCommand.ExecuteNonQueryAsync(cancellationToken);
            }

            await using var updateShipmentCommand = connection.CreateCommand();
            updateShipmentCommand.Transaction = transaction;
            updateShipmentCommand.CommandText =
                """
                UPDATE sales_order_shipments
                SET invoice_status = @invoiceStatus,
                    invoice_reference = @invoiceReference,
                    invoice_draft_id = @invoiceDraftId
                WHERE shipment_id = @shipmentId
                  AND tenant_id = @tenantId
                  AND company_id = @companyId
                  AND invoice_status = 'Pending';
                """;
            updateShipmentCommand.Parameters.AddWithValue("@invoiceStatus", "Drafted");
            updateShipmentCommand.Parameters.AddWithValue("@invoiceReference", draftReference);
            updateShipmentCommand.Parameters.AddWithValue("@invoiceDraftId", draftId.ToString());
            updateShipmentCommand.Parameters.AddWithValue("@shipmentId", shipment.ShipmentId.ToString());
            updateShipmentCommand.Parameters.AddWithValue("@tenantId", command.TenantId.ToString());
            updateShipmentCommand.Parameters.AddWithValue("@companyId", command.CompanyId.ToString());

            var affectedRows = await updateShipmentCommand.ExecuteNonQueryAsync(cancellationToken);
            if (affectedRows != 1)
            {
                throw new InvalidOperationException("No se ha podido reservar uno de los albaranes para el borrador.");
            }
        }

        foreach (var line in aggregatedLines)
        {
            await using var insertLineCommand = connection.CreateCommand();
            insertLineCommand.Transaction = transaction;
            insertLineCommand.CommandText =
                """
                INSERT INTO sales_invoice_draft_lines (
                    draft_id,
                    tenant_id,
                    company_id,
                    line_number,
                    item_code,
                    description,
                    quantity,
                    unit_of_measure,
                    unit_price,
                    line_total,
                    source_summary)
                VALUES (
                    @draftId,
                    @tenantId,
                    @companyId,
                    @lineNumber,
                    @itemCode,
                    @description,
                    @quantity,
                    @unitOfMeasure,
                    @unitPrice,
                    @lineTotal,
                    @sourceSummary);
                """;
            insertLineCommand.Parameters.AddWithValue("@draftId", draftId.ToString());
            insertLineCommand.Parameters.AddWithValue("@tenantId", command.TenantId.ToString());
            insertLineCommand.Parameters.AddWithValue("@companyId", command.CompanyId.ToString());
            insertLineCommand.Parameters.AddWithValue("@lineNumber", line.LineNumber);
            insertLineCommand.Parameters.AddWithValue("@itemCode", DbValue(line.ItemCode));
            insertLineCommand.Parameters.AddWithValue("@description", line.Description);
            insertLineCommand.Parameters.AddWithValue("@quantity", line.Quantity);
            insertLineCommand.Parameters.AddWithValue("@unitOfMeasure", DbValue(line.UnitOfMeasure));
            insertLineCommand.Parameters.AddWithValue("@unitPrice", line.UnitPrice);
            insertLineCommand.Parameters.AddWithValue("@lineTotal", line.LineTotal);
            insertLineCommand.Parameters.AddWithValue("@sourceSummary", DbValue(line.SourceSummary));
            await insertLineCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        await transaction.CommitAsync(cancellationToken);

        var shipmentSummary = string.Join(", ", selection.Select(item => item.DisplayNumber));
        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = command.TenantId,
            CompanyId = command.CompanyId,
            UserId = _currentUserContext.UserId,
            Action = "SalesInvoiceDraftCreated",
            EntityName = "PreFacturaVenta",
            EntityId = draftNumber.ToString(),
            Details = $"Borrador={draftReference}; Cliente={firstShipment.ClientName} ({firstShipment.ClientCode}); Albaranes={shipmentSummary}; Total={totalAmount:0.00}"
        }, cancellationToken);

        return draftNumber;
    }

    public async Task<int> IssueInvoiceDraftAsync(IssueSalesInvoiceDraftCommand command, CancellationToken cancellationToken = default)
    {
        if (!_saasConnectionFactory.IsConfigured)
        {
            return 0;
        }

        await EnsureCompanyAccessAsync(command.TenantId, command.CompanyId, cancellationToken);
        EnsureTenantWriteAccess();

        if (command.DraftNumber <= 0)
        {
            throw new InvalidOperationException("Debes indicar un borrador válido para emitir la factura.");
        }

        await using var connection = await _saasConnectionFactory.OpenConnectionAsync(cancellationToken);
        await EnsureSalesInvoicesWriteAllowedAsync(connection, command.TenantId, command.CompanyId, cancellationToken);
        var centerCode = await ResolveCompanyCenterCodeAsync(connection, command.TenantId, command.CompanyId, cancellationToken);
        var nowUtc = DateTime.UtcNow;

        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        DraftIssueHeader? header;
        await using (var draftCommand = connection.CreateCommand())
        {
            draftCommand.Transaction = transaction;
            draftCommand.CommandText =
                """
                SELECT draft_id,
                       draft_series,
                       draft_number,
                       client_code,
                       client_name,
                       client_tax_id,
                       issue_date,
                       due_date,
                       status,
                       shipment_count,
                       total_quantity,
                       total_amount,
                       notes
                FROM sales_invoice_drafts
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND draft_number = @draftNumber
                FOR UPDATE;
                """;
            draftCommand.Parameters.AddWithValue("@tenantId", command.TenantId.ToString());
            draftCommand.Parameters.AddWithValue("@companyId", command.CompanyId.ToString());
            draftCommand.Parameters.AddWithValue("@draftNumber", command.DraftNumber);

            await using var reader = await draftCommand.ExecuteReaderAsync(cancellationToken);
            if (!await reader.ReadAsync(cancellationToken))
            {
                throw new InvalidOperationException("No se ha encontrado el borrador que intentas emitir.");
            }

            header = new DraftIssueHeader(
                DraftId: reader.GetGuid("draft_id"),
                DraftSeries: reader.GetStringOrEmpty("draft_series"),
                DraftNumber: reader.GetInt32(reader.GetOrdinal("draft_number")),
                ClientCode: reader.GetInt32(reader.GetOrdinal("client_code")),
                ClientName: reader.GetStringOrEmpty("client_name"),
                ClientTaxId: reader.GetStringOrEmpty("client_tax_id"),
                IssueDate: reader.GetDateTime(reader.GetOrdinal("issue_date")),
                DueDate: reader.IsDBNull(reader.GetOrdinal("due_date")) ? null : reader.GetDateTime(reader.GetOrdinal("due_date")),
                Status: reader.GetStringOrEmpty("status"),
                ShipmentCount: reader.GetInt32(reader.GetOrdinal("shipment_count")),
                TotalQuantity: reader.GetDecimal(reader.GetOrdinal("total_quantity")),
                TotalAmount: reader.GetDecimal(reader.GetOrdinal("total_amount")),
                Notes: reader.GetStringOrEmpty("notes"));
        }

        if (!string.Equals(header.Status, SalesInvoiceDraftStatuses.Draft, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Este borrador ya no está disponible para emitir una factura.");
        }

        var shipments = new List<SalesInvoiceShipmentDto>();
        await using (var shipmentsCommand = connection.CreateCommand())
        {
            shipmentsCommand.Transaction = transaction;
            shipmentsCommand.CommandText =
                """
                SELECT shipment_id, shipment_series, shipment_number, order_number, shipment_date, warehouse, shipped_quantity, estimated_amount
                FROM sales_invoice_draft_shipments
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND draft_id = @draftId
                ORDER BY shipment_date, shipment_number
                FOR UPDATE;
                """;
            shipmentsCommand.Parameters.AddWithValue("@tenantId", command.TenantId.ToString());
            shipmentsCommand.Parameters.AddWithValue("@companyId", command.CompanyId.ToString());
            shipmentsCommand.Parameters.AddWithValue("@draftId", header.DraftId.ToString());

            await using var reader = await shipmentsCommand.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                shipments.Add(new SalesInvoiceShipmentDto
                {
                    ShipmentId = reader.GetGuid("shipment_id"),
                    ShipmentSeries = reader.GetStringOrEmpty("shipment_series"),
                    ShipmentNumber = reader.GetInt32(reader.GetOrdinal("shipment_number")),
                    OrderNumber = reader.GetInt32(reader.GetOrdinal("order_number")),
                    ShipmentDate = reader.GetDateTime(reader.GetOrdinal("shipment_date")),
                    Warehouse = reader.GetStringOrEmpty("warehouse"),
                    TotalShippedQuantity = reader.GetDecimal(reader.GetOrdinal("shipped_quantity")),
                    EstimatedAmount = reader.GetDecimal(reader.GetOrdinal("estimated_amount"))
                });
            }
        }

        if (shipments.Count == 0)
        {
            throw new InvalidOperationException("El borrador no tiene albaranes asociados y no se puede emitir.");
        }

        var lines = new List<SalesInvoiceLineDto>();
        await using (var linesCommand = connection.CreateCommand())
        {
            linesCommand.Transaction = transaction;
            linesCommand.CommandText =
                """
                SELECT line_number, item_code, description, quantity, unit_of_measure, unit_price, line_total, source_summary
                FROM sales_invoice_draft_lines
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND draft_id = @draftId
                ORDER BY line_number
                FOR UPDATE;
                """;
            linesCommand.Parameters.AddWithValue("@tenantId", command.TenantId.ToString());
            linesCommand.Parameters.AddWithValue("@companyId", command.CompanyId.ToString());
            linesCommand.Parameters.AddWithValue("@draftId", header.DraftId.ToString());

            await using var reader = await linesCommand.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                var lineSubtotal = reader.GetDecimal(reader.GetOrdinal("line_total"));
                lines.Add(new SalesInvoiceLineDto
                {
                    LineNumber = reader.GetInt32(reader.GetOrdinal("line_number")),
                    ItemCode = reader.GetStringOrEmpty("item_code"),
                    Description = reader.GetStringOrEmpty("description"),
                    Quantity = reader.GetDecimal(reader.GetOrdinal("quantity")),
                    UnitOfMeasure = reader.GetStringOrEmpty("unit_of_measure"),
                    UnitPrice = reader.GetDecimal(reader.GetOrdinal("unit_price")),
                    LineSubtotal = lineSubtotal,
                    TaxRate = 0m,
                    TaxAmount = 0m,
                    LineTotal = lineSubtotal,
                    SourceSummary = reader.GetStringOrEmpty("source_summary")
                });
            }
        }

        var invoiceId = Guid.NewGuid();
        var invoiceNumber = await GetNextInvoiceNumberAsync(connection, transaction, command.TenantId, command.CompanyId, cancellationToken);
        var invoiceSeries = BuildInvoiceSeries(centerCode);
        var invoiceReference = $"{invoiceSeries}/{invoiceNumber:000000}";
        var subtotalAmount = decimal.Round(lines.Sum(line => line.LineSubtotal), 2, MidpointRounding.AwayFromZero);
        var taxAmount = decimal.Round(lines.Sum(line => line.TaxAmount), 2, MidpointRounding.AwayFromZero);
        var totalAmount = decimal.Round(subtotalAmount + taxAmount, 2, MidpointRounding.AwayFromZero);

        await using (var insertInvoiceCommand = connection.CreateCommand())
        {
            insertInvoiceCommand.Transaction = transaction;
            insertInvoiceCommand.CommandText =
                """
                INSERT INTO sales_invoices (
                    invoice_id,
                    invoice_series,
                    invoice_number,
                    draft_id,
                    draft_series,
                    draft_number,
                    tenant_id,
                    company_id,
                    client_code,
                    client_name,
                    client_tax_id,
                    issue_date,
                    due_date,
                    status,
                    shipment_count,
                    total_quantity,
                    subtotal_amount,
                    tax_amount,
                    total_amount,
                    payment_status,
                    amount_paid,
                    outstanding_amount,
                    last_payment_utc,
                    accounting_status,
                    accounting_reference,
                    accounting_ready_utc,
                    notes,
                    issued_utc,
                    created_utc,
                    updated_utc)
                VALUES (
                    @invoiceId,
                    @invoiceSeries,
                    @invoiceNumber,
                    @draftId,
                    @draftSeries,
                    @draftNumber,
                    @tenantId,
                    @companyId,
                    @clientCode,
                    @clientName,
                    @clientTaxId,
                    @issueDate,
                    @dueDate,
                    @status,
                    @shipmentCount,
                    @totalQuantity,
                    @subtotalAmount,
                    @taxAmount,
                    @totalAmount,
                    @paymentStatus,
                    @amountPaid,
                    @outstandingAmount,
                    @lastPaymentUtc,
                    @accountingStatus,
                    @accountingReference,
                    @accountingReadyUtc,
                    @notes,
                    @issuedUtc,
                    @createdUtc,
                    @updatedUtc);
                """;
            insertInvoiceCommand.Parameters.AddWithValue("@invoiceId", invoiceId.ToString());
            insertInvoiceCommand.Parameters.AddWithValue("@invoiceSeries", invoiceSeries);
            insertInvoiceCommand.Parameters.AddWithValue("@invoiceNumber", invoiceNumber);
            insertInvoiceCommand.Parameters.AddWithValue("@draftId", header.DraftId.ToString());
            insertInvoiceCommand.Parameters.AddWithValue("@draftSeries", header.DraftSeries);
            insertInvoiceCommand.Parameters.AddWithValue("@draftNumber", header.DraftNumber);
            insertInvoiceCommand.Parameters.AddWithValue("@tenantId", command.TenantId.ToString());
            insertInvoiceCommand.Parameters.AddWithValue("@companyId", command.CompanyId.ToString());
            insertInvoiceCommand.Parameters.AddWithValue("@clientCode", header.ClientCode);
            insertInvoiceCommand.Parameters.AddWithValue("@clientName", header.ClientName);
            insertInvoiceCommand.Parameters.AddWithValue("@clientTaxId", DbValue(header.ClientTaxId));
            insertInvoiceCommand.Parameters.AddWithValue("@issueDate", header.IssueDate.Date);
            insertInvoiceCommand.Parameters.AddWithValue("@dueDate", DbValue(header.DueDate?.Date));
            insertInvoiceCommand.Parameters.AddWithValue("@status", SalesInvoiceStatuses.Issued);
            insertInvoiceCommand.Parameters.AddWithValue("@shipmentCount", header.ShipmentCount);
            insertInvoiceCommand.Parameters.AddWithValue("@totalQuantity", header.TotalQuantity);
            insertInvoiceCommand.Parameters.AddWithValue("@subtotalAmount", subtotalAmount);
            insertInvoiceCommand.Parameters.AddWithValue("@taxAmount", taxAmount);
            insertInvoiceCommand.Parameters.AddWithValue("@totalAmount", totalAmount);
            insertInvoiceCommand.Parameters.AddWithValue("@paymentStatus", SalesInvoicePaymentStatuses.Pending);
            insertInvoiceCommand.Parameters.AddWithValue("@amountPaid", 0m);
            insertInvoiceCommand.Parameters.AddWithValue("@outstandingAmount", totalAmount);
            insertInvoiceCommand.Parameters.AddWithValue("@lastPaymentUtc", DBNull.Value);
            insertInvoiceCommand.Parameters.AddWithValue("@accountingStatus", SalesInvoiceAccountingStatuses.Ready);
            insertInvoiceCommand.Parameters.AddWithValue("@accountingReference", DBNull.Value);
            insertInvoiceCommand.Parameters.AddWithValue("@accountingReadyUtc", nowUtc);
            insertInvoiceCommand.Parameters.AddWithValue("@notes", DbValue(header.Notes));
            insertInvoiceCommand.Parameters.AddWithValue("@issuedUtc", nowUtc);
            insertInvoiceCommand.Parameters.AddWithValue("@createdUtc", nowUtc);
            insertInvoiceCommand.Parameters.AddWithValue("@updatedUtc", nowUtc);
            await insertInvoiceCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        foreach (var shipment in shipments)
        {
            await using (var insertShipmentCommand = connection.CreateCommand())
            {
                insertShipmentCommand.Transaction = transaction;
                insertShipmentCommand.CommandText =
                    """
                    INSERT INTO sales_invoice_shipments (
                        invoice_id,
                        tenant_id,
                        company_id,
                        shipment_id,
                        shipment_series,
                        shipment_number,
                        order_number,
                        shipment_date,
                        warehouse,
                        shipped_quantity,
                        estimated_amount)
                    VALUES (
                        @invoiceId,
                        @tenantId,
                        @companyId,
                        @shipmentId,
                        @shipmentSeries,
                        @shipmentNumber,
                        @orderNumber,
                        @shipmentDate,
                        @warehouse,
                        @shippedQuantity,
                        @estimatedAmount);
                    """;
                insertShipmentCommand.Parameters.AddWithValue("@invoiceId", invoiceId.ToString());
                insertShipmentCommand.Parameters.AddWithValue("@tenantId", command.TenantId.ToString());
                insertShipmentCommand.Parameters.AddWithValue("@companyId", command.CompanyId.ToString());
                insertShipmentCommand.Parameters.AddWithValue("@shipmentId", shipment.ShipmentId.ToString());
                insertShipmentCommand.Parameters.AddWithValue("@shipmentSeries", DbValue(shipment.ShipmentSeries));
                insertShipmentCommand.Parameters.AddWithValue("@shipmentNumber", shipment.ShipmentNumber);
                insertShipmentCommand.Parameters.AddWithValue("@orderNumber", shipment.OrderNumber);
                insertShipmentCommand.Parameters.AddWithValue("@shipmentDate", shipment.ShipmentDate.Date);
                insertShipmentCommand.Parameters.AddWithValue("@warehouse", DbValue(shipment.Warehouse));
                insertShipmentCommand.Parameters.AddWithValue("@shippedQuantity", shipment.TotalShippedQuantity);
                insertShipmentCommand.Parameters.AddWithValue("@estimatedAmount", shipment.EstimatedAmount);
                await insertShipmentCommand.ExecuteNonQueryAsync(cancellationToken);
            }

            await using var updateShipmentCommand = connection.CreateCommand();
            updateShipmentCommand.Transaction = transaction;
            updateShipmentCommand.CommandText =
                """
                UPDATE sales_order_shipments
                SET invoice_status = @invoiceStatus,
                    invoice_reference = @invoiceReference,
                    invoice_id = @invoiceId
                WHERE shipment_id = @shipmentId
                  AND tenant_id = @tenantId
                  AND company_id = @companyId
                  AND invoice_draft_id = @draftId
                  AND invoice_status = 'Drafted';
                """;
            updateShipmentCommand.Parameters.AddWithValue("@invoiceStatus", "Invoiced");
            updateShipmentCommand.Parameters.AddWithValue("@invoiceReference", invoiceReference);
            updateShipmentCommand.Parameters.AddWithValue("@invoiceId", invoiceId.ToString());
            updateShipmentCommand.Parameters.AddWithValue("@shipmentId", shipment.ShipmentId.ToString());
            updateShipmentCommand.Parameters.AddWithValue("@tenantId", command.TenantId.ToString());
            updateShipmentCommand.Parameters.AddWithValue("@companyId", command.CompanyId.ToString());
            updateShipmentCommand.Parameters.AddWithValue("@draftId", header.DraftId.ToString());

            var affectedRows = await updateShipmentCommand.ExecuteNonQueryAsync(cancellationToken);
            if (affectedRows != 1)
            {
                throw new InvalidOperationException("No se ha podido marcar uno de los albaranes como facturado.");
            }
        }

        foreach (var line in lines)
        {
            await using var insertLineCommand = connection.CreateCommand();
            insertLineCommand.Transaction = transaction;
            insertLineCommand.CommandText =
                """
                INSERT INTO sales_invoice_lines (
                    invoice_id,
                    tenant_id,
                    company_id,
                    line_number,
                    item_code,
                    description,
                    quantity,
                    unit_of_measure,
                    unit_price,
                    line_subtotal,
                    tax_rate,
                    tax_amount,
                    line_total,
                    source_summary)
                VALUES (
                    @invoiceId,
                    @tenantId,
                    @companyId,
                    @lineNumber,
                    @itemCode,
                    @description,
                    @quantity,
                    @unitOfMeasure,
                    @unitPrice,
                    @lineSubtotal,
                    @taxRate,
                    @taxAmount,
                    @lineTotal,
                    @sourceSummary);
                """;
            insertLineCommand.Parameters.AddWithValue("@invoiceId", invoiceId.ToString());
            insertLineCommand.Parameters.AddWithValue("@tenantId", command.TenantId.ToString());
            insertLineCommand.Parameters.AddWithValue("@companyId", command.CompanyId.ToString());
            insertLineCommand.Parameters.AddWithValue("@lineNumber", line.LineNumber);
            insertLineCommand.Parameters.AddWithValue("@itemCode", DbValue(line.ItemCode));
            insertLineCommand.Parameters.AddWithValue("@description", line.Description);
            insertLineCommand.Parameters.AddWithValue("@quantity", line.Quantity);
            insertLineCommand.Parameters.AddWithValue("@unitOfMeasure", DbValue(line.UnitOfMeasure));
            insertLineCommand.Parameters.AddWithValue("@unitPrice", line.UnitPrice);
            insertLineCommand.Parameters.AddWithValue("@lineSubtotal", line.LineSubtotal);
            insertLineCommand.Parameters.AddWithValue("@taxRate", line.TaxRate);
            insertLineCommand.Parameters.AddWithValue("@taxAmount", line.TaxAmount);
            insertLineCommand.Parameters.AddWithValue("@lineTotal", line.LineTotal);
            insertLineCommand.Parameters.AddWithValue("@sourceSummary", DbValue(line.SourceSummary));
            await insertLineCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        await using (var updateDraftCommand = connection.CreateCommand())
        {
            updateDraftCommand.Transaction = transaction;
            updateDraftCommand.CommandText =
                """
                UPDATE sales_invoice_drafts
                SET status = @status,
                    invoice_id = @invoiceId,
                    issued_utc = @issuedUtc,
                    updated_utc = @updatedUtc
                WHERE draft_id = @draftId
                  AND tenant_id = @tenantId
                  AND company_id = @companyId
                  AND status = @currentStatus;
                """;
            updateDraftCommand.Parameters.AddWithValue("@status", SalesInvoiceDraftStatuses.Issued);
            updateDraftCommand.Parameters.AddWithValue("@invoiceId", invoiceId.ToString());
            updateDraftCommand.Parameters.AddWithValue("@issuedUtc", nowUtc);
            updateDraftCommand.Parameters.AddWithValue("@updatedUtc", nowUtc);
            updateDraftCommand.Parameters.AddWithValue("@draftId", header.DraftId.ToString());
            updateDraftCommand.Parameters.AddWithValue("@tenantId", command.TenantId.ToString());
            updateDraftCommand.Parameters.AddWithValue("@companyId", command.CompanyId.ToString());
            updateDraftCommand.Parameters.AddWithValue("@currentStatus", SalesInvoiceDraftStatuses.Draft);
            var affectedRows = await updateDraftCommand.ExecuteNonQueryAsync(cancellationToken);
            if (affectedRows != 1)
            {
                throw new InvalidOperationException("No se ha podido cerrar el borrador como emitido.");
            }
        }

        await transaction.CommitAsync(cancellationToken);

        var shipmentSummary = string.Join(", ", shipments.Select(item => item.DisplayNumber));
        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = command.TenantId,
            CompanyId = command.CompanyId,
            UserId = _currentUserContext.UserId,
            Action = "SalesInvoiceIssued",
            EntityName = "FacturaVenta",
            EntityId = invoiceNumber.ToString(),
            Details = $"Factura={invoiceReference}; Borrador={header.DraftSeries}/{header.DraftNumber:000000}; Cliente={header.ClientName} ({header.ClientCode}); Albaranes={shipmentSummary}; Total={totalAmount:0.00}"
        }, cancellationToken);

        return invoiceNumber;
    }

    public async Task RegisterInvoicePaymentAsync(RegisterSalesInvoicePaymentCommand command, CancellationToken cancellationToken = default)
    {
        if (!_saasConnectionFactory.IsConfigured)
        {
            return;
        }

        await EnsureCompanyAccessAsync(command.TenantId, command.CompanyId, cancellationToken);
        EnsureTenantWriteAccess();
        NormalizeAndValidateInvoicePayment(command);

        await using var connection = await _saasConnectionFactory.OpenConnectionAsync(cancellationToken);
        await EnsureSalesInvoicesWriteAllowedAsync(connection, command.TenantId, command.CompanyId, cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        var invoiceHeader = await LoadInvoicePaymentHeaderAsync(connection, transaction, command.TenantId, command.CompanyId, command.InvoiceNumber, cancellationToken)
            ?? throw new InvalidOperationException("No se ha encontrado la factura indicada.");

        if (string.Equals(invoiceHeader.Status, SalesInvoiceStatuses.Cancelled, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("No puedes registrar cobros sobre una factura anulada.");
        }

        var outstandingAmount = decimal.Round(invoiceHeader.OutstandingAmount, 2, MidpointRounding.AwayFromZero);
        if (command.Amount > outstandingAmount)
        {
            throw new InvalidOperationException($"El cobro supera el pendiente actual de la factura ({outstandingAmount:0.00} €).");
        }

        var paymentNumber = await GetNextInvoicePaymentNumberAsync(connection, transaction, invoiceHeader.InvoiceId, command.TenantId, command.CompanyId, cancellationToken);
        var paymentId = Guid.NewGuid();
        var nowUtc = DateTime.UtcNow;

        await using (var insertPaymentCommand = connection.CreateCommand())
        {
            insertPaymentCommand.Transaction = transaction;
            insertPaymentCommand.CommandText =
                """
                INSERT INTO sales_invoice_payments (
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
        var remainingAmount = decimal.Round(invoiceHeader.TotalAmount - amountPaid, 2, MidpointRounding.AwayFromZero);
        if (remainingAmount < 0m)
        {
            remainingAmount = 0m;
        }

        var paymentStatus = DetermineInvoicePaymentStatus(amountPaid, invoiceHeader.TotalAmount);

        await using (var updateInvoiceCommand = connection.CreateCommand())
        {
            updateInvoiceCommand.Transaction = transaction;
            updateInvoiceCommand.CommandText =
                """
                UPDATE sales_invoices
                SET payment_status = @paymentStatus,
                    amount_paid = @amountPaid,
                    outstanding_amount = @outstandingAmount,
                    last_payment_utc = @lastPaymentUtc,
                    updated_utc = @updatedUtc
                WHERE invoice_id = @invoiceId
                  AND tenant_id = @tenantId
                  AND company_id = @companyId;
                """;
            updateInvoiceCommand.Parameters.AddWithValue("@paymentStatus", paymentStatus);
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
            Action = "SalesInvoicePaymentRegistered",
            EntityName = "FacturaVenta",
            EntityId = command.InvoiceNumber.ToString(),
            Details = $"Factura={invoiceHeader.InvoiceDisplayNumber}; Cobro={paymentNumber}; Fecha={command.PaymentDate:yyyy-MM-dd}; Importe={command.Amount:0.00}; Estado={paymentStatus}; Pendiente={remainingAmount:0.00}{(string.IsNullOrWhiteSpace(command.Method) ? string.Empty : $"; Metodo={command.Method}")}{(string.IsNullOrWhiteSpace(command.Reference) ? string.Empty : $"; Referencia={command.Reference}")}"
        }, cancellationToken);
    }

    private async Task<IReadOnlyCollection<SalesOrderLineDto>> LoadLinesAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        int orderNumber,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT line_number, item_code, description, quantity, shipped_quantity, unit_of_measure, unit_price, requested_date, last_shipped_utc, notes
            FROM sales_order_lines
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND order_number = @orderNumber
            ORDER BY line_number;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@orderNumber", orderNumber);

        var items = new List<SalesOrderLineDto>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new SalesOrderLineDto
            {
                LineNumber = reader.GetInt32(reader.GetOrdinal("line_number")),
                ItemCode = reader.GetStringOrEmpty("item_code"),
                Description = reader.GetStringOrEmpty("description"),
                Quantity = reader.GetDecimal(reader.GetOrdinal("quantity")),
                ShippedQuantity = reader.GetDecimal(reader.GetOrdinal("shipped_quantity")),
                UnitOfMeasure = reader.GetStringOrEmpty("unit_of_measure"),
                UnitPrice = reader.GetDecimal(reader.GetOrdinal("unit_price")),
                RequestedDate = reader.IsDBNull(reader.GetOrdinal("requested_date")) ? null : reader.GetDateTime(reader.GetOrdinal("requested_date")),
                LastShippedUtc = reader.IsDBNull(reader.GetOrdinal("last_shipped_utc")) ? null : reader.GetDateTime(reader.GetOrdinal("last_shipped_utc")),
                Notes = reader.GetStringOrEmpty("notes")
            });
        }

        return items;
    }

    private async Task<List<ImportCompanyContext>> LoadImportCompaniesAsync(
        MySqlConnection connection,
        ImportLegacySalesOrdersCommand command,
        CancellationToken cancellationToken)
    {
        await using var query = connection.CreateCommand();
        query.CommandText =
            """
            SELECT id, legacy_center_code
            FROM companies
            WHERE tenant_id = @tenantId
              AND is_active = 1
              AND (
                    @companyId = ''
                    OR id = @companyId
                  )
            ORDER BY created_utc;
            """;
        query.Parameters.AddWithValue("@tenantId", command.TenantId.ToString());
        query.Parameters.AddWithValue("@companyId", command.CompanyId?.ToString() ?? string.Empty);

        var companies = new List<ImportCompanyContext>();
        await using var reader = await query.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            companies.Add(new ImportCompanyContext(
                reader.GetGuid("id"),
                reader.GetStringOrEmpty("legacy_center_code")));
        }

        return companies;
    }

    private static async Task<Dictionary<int, string>> LoadExistingSalesOrderOriginsAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        CancellationToken cancellationToken)
    {
        await using var query = connection.CreateCommand();
        query.CommandText =
            """
            SELECT order_number,
                   COALESCE(origin, 'saas') AS origin
            FROM sales_orders
            WHERE tenant_id = @tenantId
              AND company_id = @companyId;
            """;
        query.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        query.Parameters.AddWithValue("@companyId", companyId.ToString());

        var orderOrigins = new Dictionary<int, string>();
        await using var reader = await query.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            orderOrigins[reader.GetInt32(reader.GetOrdinal("order_number"))] = reader.GetStringOrEmpty("origin");
        }

        return orderOrigins;
    }

    private static async Task<List<LegacySalesOrderHeader>> LoadLegacyOrderHeadersAsync(
        MySqlConnection connection,
        string centerCode,
        CancellationToken cancellationToken)
    {
        await using var query = connection.CreateCommand();
        query.CommandText =
            """
            SELECT f.FRA,
                   f.CLIENT,
                   COALESCE(NULLIF(c.NOM, ''), CONCAT('Cliente ', CAST(f.CLIENT AS CHAR))) AS client_name,
                   COALESCE(c.NIF, '') AS client_tax_id,
                   f.DATA,
                   f.ALBCLI,
                   f.OBSERV
            FROM factur f
            LEFT JOIN clients c
              ON c.CENTRO = f.CENTRO
             AND c.CODI = f.CLIENT
            WHERE f.DOCUMENT = 'C'
              AND f.CENTRO = @centerCode
            ORDER BY f.FRA;
            """;
        query.Parameters.AddWithValue("@centerCode", centerCode);

        var headers = new List<LegacySalesOrderHeader>();
        await using var reader = await query.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            headers.Add(new LegacySalesOrderHeader(
                reader.GetInt32(reader.GetOrdinal("FRA")),
                reader.GetInt32(reader.GetOrdinal("CLIENT")),
                reader.GetStringOrEmpty("client_name"),
                reader.GetStringOrEmpty("client_tax_id"),
                reader.GetDateTime(reader.GetOrdinal("DATA")),
                BuildLegacyOrderNotes(reader.GetStringOrEmpty("ALBCLI"), reader.GetStringOrEmpty("OBSERV"))));
        }

        return headers;
    }

    private static async Task<Dictionary<int, List<LegacySalesOrderLine>>> LoadLegacyOrderLinesAsync(
        MySqlConnection connection,
        string centerCode,
        CancellationToken cancellationToken)
    {
        await using var query = connection.CreateCommand();
        query.CommandText =
            """
            SELECT FRA,
                   NLINEA,
                   COALESCE(NULLIF(MOSTRA, ''), NULLIF(NCCODE, ''), '') AS item_code,
                   DESCRI,
                   UNITATS,
                   PREU,
                   PERREBRE,
                   REBUT,
                   DATA
            FROM dfactu
            WHERE DOCUMENT = 'C'
              AND CENTRO = @centerCode
            ORDER BY FRA, NLINEA;
            """;
        query.Parameters.AddWithValue("@centerCode", centerCode);

        var linesByOrder = new Dictionary<int, List<LegacySalesOrderLine>>();
        await using var reader = await query.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var orderNumber = reader.GetInt32(reader.GetOrdinal("FRA"));
            if (!linesByOrder.TryGetValue(orderNumber, out var lines))
            {
                lines = [];
                linesByOrder[orderNumber] = lines;
            }

            lines.Add(new LegacySalesOrderLine(
                reader.GetInt32(reader.GetOrdinal("NLINEA")),
                reader.GetStringOrEmpty("item_code"),
                reader.GetStringOrEmpty("DESCRI"),
                reader.GetDecimalOrDefault("UNITATS"),
                reader.GetDecimalOrDefault("PREU"),
                reader.GetDecimalOrDefault("PERREBRE"),
                reader.GetDecimalOrDefault("REBUT"),
                reader.IsDBNull(reader.GetOrdinal("DATA")) ? null : reader.GetDateTime(reader.GetOrdinal("DATA"))));
        }

        return linesByOrder;
    }

    private static List<ImportedSalesOrderLine> NormalizeLegacyLines(IEnumerable<LegacySalesOrderLine> legacyLines)
    {
        var lines = new List<ImportedSalesOrderLine>();
        foreach (var legacyLine in legacyLines)
        {
            var quantity = Math.Max(
                Math.Abs(legacyLine.Quantity),
                Math.Abs(legacyLine.PendingQuantity) + Math.Abs(legacyLine.ShippedQuantity));

            if (quantity <= 0)
            {
                continue;
            }

            var shippedQuantity = Math.Min(quantity, Math.Abs(legacyLine.ShippedQuantity));
            var description = string.IsNullOrWhiteSpace(legacyLine.Description)
                ? (!string.IsNullOrWhiteSpace(legacyLine.ItemCode) ? legacyLine.ItemCode : $"Línea {legacyLine.LineNumber}")
                : legacyLine.Description.Trim();

            lines.Add(new ImportedSalesOrderLine(
                legacyLine.LineNumber,
                legacyLine.ItemCode.Trim(),
                description,
                quantity,
                shippedQuantity,
                decimal.Round(legacyLine.UnitPrice, 4, MidpointRounding.AwayFromZero),
                legacyLine.RequestedDate?.Date));
        }

        return lines;
    }

    private static string DetermineLegacyImportedStatus(IEnumerable<ImportedSalesOrderLine> lines)
    {
        var normalizedLines = lines.ToArray();
        var totalQuantity = normalizedLines.Sum(line => line.Quantity);
        var totalShipped = normalizedLines.Sum(line => line.ShippedQuantity);

        if (totalQuantity <= 0 || totalShipped <= 0)
        {
            return SalesOrderStatuses.Confirmed;
        }

        if (normalizedLines.All(line => line.ShippedQuantity >= line.Quantity))
        {
            return SalesOrderStatuses.Shipped;
        }

        return SalesOrderStatuses.PartiallyShipped;
    }

    private static async Task UpsertImportedSalesOrderHeaderAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid tenantId,
        Guid companyId,
        string legacyCenterCode,
        LegacySalesOrderHeader header,
        string status,
        DateTime nowUtc,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            INSERT INTO sales_orders (
                tenant_id,
                company_id,
                order_number,
                client_code,
                client_name,
                client_tax_id,
                document_date,
                requested_date,
                status,
                origin,
                is_deleted,
                legacy_source_system,
                legacy_center_code,
                legacy_document_type,
                legacy_document_number,
                synced_utc,
                notes,
                created_utc,
                updated_utc)
            VALUES (
                @tenantId,
                @companyId,
                @orderNumber,
                @clientCode,
                @clientName,
                @clientTaxId,
                @documentDate,
                @requestedDate,
                @status,
                @origin,
                0,
                @legacySourceSystem,
                @legacyCenterCode,
                @legacyDocumentType,
                @legacyDocumentNumber,
                @syncedUtc,
                @notes,
                @createdUtc,
                @updatedUtc)
            ON DUPLICATE KEY UPDATE
                client_code = VALUES(client_code),
                client_name = VALUES(client_name),
                client_tax_id = VALUES(client_tax_id),
                document_date = VALUES(document_date),
                requested_date = VALUES(requested_date),
                status = VALUES(status),
                origin = VALUES(origin),
                is_deleted = VALUES(is_deleted),
                legacy_source_system = VALUES(legacy_source_system),
                legacy_center_code = VALUES(legacy_center_code),
                legacy_document_type = VALUES(legacy_document_type),
                legacy_document_number = VALUES(legacy_document_number),
                synced_utc = VALUES(synced_utc),
                notes = VALUES(notes),
                updated_utc = VALUES(updated_utc);
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@orderNumber", header.OrderNumber);
        command.Parameters.AddWithValue("@clientCode", header.ClientCode);
        command.Parameters.AddWithValue("@clientName", header.ClientName);
        command.Parameters.AddWithValue("@clientTaxId", DbValue(header.ClientTaxId));
        command.Parameters.AddWithValue("@documentDate", header.DocumentDate.Date);
        command.Parameters.AddWithValue("@requestedDate", DBNull.Value);
        command.Parameters.AddWithValue("@status", status);
        command.Parameters.AddWithValue("@origin", SalesOrderOrigins.Legacy);
        command.Parameters.AddWithValue("@legacySourceSystem", "legacy");
        command.Parameters.AddWithValue("@legacyCenterCode", legacyCenterCode);
        command.Parameters.AddWithValue("@legacyDocumentType", "C");
        command.Parameters.AddWithValue("@legacyDocumentNumber", header.OrderNumber.ToString());
        command.Parameters.AddWithValue("@syncedUtc", nowUtc);
        command.Parameters.AddWithValue("@notes", DbValue(header.Notes));
        command.Parameters.AddWithValue("@createdUtc", nowUtc);
        command.Parameters.AddWithValue("@updatedUtc", nowUtc);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task ReplaceImportedSalesOrderLinesAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid tenantId,
        Guid companyId,
        string legacyCenterCode,
        int orderNumber,
        IReadOnlyCollection<ImportedSalesOrderLine> lines,
        DateTime nowUtc,
        CancellationToken cancellationToken)
    {
        await using (var deleteCommand = connection.CreateCommand())
        {
            deleteCommand.Transaction = transaction;
            deleteCommand.CommandText =
                """
                DELETE FROM sales_order_lines
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND order_number = @orderNumber;
                """;
            deleteCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            deleteCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
            deleteCommand.Parameters.AddWithValue("@orderNumber", orderNumber);
            await deleteCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        foreach (var line in lines)
        {
            await using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText =
                """
                INSERT INTO sales_order_lines (
                    tenant_id,
                    company_id,
                    order_number,
                    line_number,
                    item_code,
                    description,
                    quantity,
                    shipped_quantity,
                    unit_of_measure,
                    unit_price,
                    requested_date,
                    legacy_source_system,
                    legacy_center_code,
                    legacy_document_type,
                    legacy_document_number,
                    legacy_line_number,
                    synced_utc,
                    last_shipped_utc,
                    notes)
                VALUES (
                    @tenantId,
                    @companyId,
                    @orderNumber,
                    @lineNumber,
                    @itemCode,
                    @description,
                    @quantity,
                    @shippedQuantity,
                    @unitOfMeasure,
                    @unitPrice,
                    @requestedDate,
                    @legacySourceSystem,
                    @legacyCenterCode,
                    @legacyDocumentType,
                    @legacyDocumentNumber,
                    @legacyLineNumber,
                    @syncedUtc,
                    @lastShippedUtc,
                    @notes);
                """;
            command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            command.Parameters.AddWithValue("@companyId", companyId.ToString());
            command.Parameters.AddWithValue("@orderNumber", orderNumber);
            command.Parameters.AddWithValue("@lineNumber", line.LineNumber);
            command.Parameters.AddWithValue("@itemCode", DbValue(line.ItemCode));
            command.Parameters.AddWithValue("@description", line.Description);
            command.Parameters.AddWithValue("@quantity", line.Quantity);
            command.Parameters.AddWithValue("@shippedQuantity", line.ShippedQuantity);
            command.Parameters.AddWithValue("@unitOfMeasure", DBNull.Value);
            command.Parameters.AddWithValue("@unitPrice", line.UnitPrice);
            command.Parameters.AddWithValue("@requestedDate", DbValue(line.RequestedDate));
            command.Parameters.AddWithValue("@legacySourceSystem", "legacy");
            command.Parameters.AddWithValue("@legacyCenterCode", legacyCenterCode);
            command.Parameters.AddWithValue("@legacyDocumentType", "C");
            command.Parameters.AddWithValue("@legacyDocumentNumber", orderNumber.ToString());
            command.Parameters.AddWithValue("@legacyLineNumber", line.LineNumber);
            command.Parameters.AddWithValue("@syncedUtc", nowUtc);
            command.Parameters.AddWithValue("@lastShippedUtc", line.ShippedQuantity > 0 ? line.RequestedDate ?? DateTime.UtcNow : DBNull.Value);
            command.Parameters.AddWithValue("@notes", DBNull.Value);
            await command.ExecuteNonQueryAsync(cancellationToken);
        }
    }

    private static async Task<int> MarkMissingImportedSalesOrdersAsDeletedAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        IReadOnlyCollection<int> visibleLegacyOrderNumbers,
        CancellationToken cancellationToken)
    {
        await using var selectCommand = connection.CreateCommand();
        selectCommand.CommandText =
            """
            SELECT order_number
            FROM sales_orders
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND COALESCE(origin, 'saas') = @origin
              AND COALESCE(is_deleted, 0) = 0;
            """;
        selectCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        selectCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
        selectCommand.Parameters.AddWithValue("@origin", SalesOrderOrigins.Legacy);

        var missingOrders = new List<int>();
        await using (var reader = await selectCommand.ExecuteReaderAsync(cancellationToken))
        {
            while (await reader.ReadAsync(cancellationToken))
            {
                var orderNumber = reader.GetInt32(reader.GetOrdinal("order_number"));
                if (!visibleLegacyOrderNumbers.Contains(orderNumber))
                {
                    missingOrders.Add(orderNumber);
                }
            }
        }

        if (missingOrders.Count == 0)
        {
            return 0;
        }

        foreach (var orderNumber in missingOrders)
        {
            await using var updateCommand = connection.CreateCommand();
            updateCommand.CommandText =
                """
                UPDATE sales_orders
                SET status = @status,
                    is_deleted = 1,
                    synced_utc = @syncedUtc,
                    updated_utc = @updatedUtc
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND order_number = @orderNumber;
                """;
            updateCommand.Parameters.AddWithValue("@status", SalesOrderStatuses.Cancelled);
            updateCommand.Parameters.AddWithValue("@syncedUtc", DateTime.UtcNow);
            updateCommand.Parameters.AddWithValue("@updatedUtc", DateTime.UtcNow);
            updateCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            updateCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
            updateCommand.Parameters.AddWithValue("@orderNumber", orderNumber);
            await updateCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        return missingOrders.Count;
    }

    private static void FillHeaderParameters(MySqlCommand command, SaveSalesOrderCommand request, int orderNumber, ClientSnapshot clientSnapshot)
    {
        command.Parameters.AddWithValue("@tenantId", request.TenantId.ToString());
        command.Parameters.AddWithValue("@companyId", request.CompanyId.ToString());
        command.Parameters.AddWithValue("@orderNumber", orderNumber);
        command.Parameters.AddWithValue("@clientCode", request.ClientCode);
        command.Parameters.AddWithValue("@clientName", clientSnapshot.Name);
        command.Parameters.AddWithValue("@clientTaxId", DbValue(clientSnapshot.TaxId));
        command.Parameters.AddWithValue("@documentDate", request.DocumentDate.Date);
        command.Parameters.AddWithValue("@requestedDate", DbValue(request.RequestedDate?.Date));
        command.Parameters.AddWithValue("@status", request.Status);
        command.Parameters.AddWithValue("@notes", DbValue(request.Notes));
    }

    private async Task<string> ResolveCompanyCenterCodeAsync(Guid tenantId, Guid companyId, CancellationToken cancellationToken)
    {
        await using var connection = await _saasConnectionFactory.OpenConnectionAsync(cancellationToken);
        return await ResolveCompanyCenterCodeAsync(connection, tenantId, companyId, cancellationToken);
    }

    private static async Task<int> GetNextOrderNumberAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT COALESCE(MAX(order_number), 0) + 1
            FROM sales_orders
            WHERE tenant_id = @tenantId
              AND company_id = @companyId;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        return Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));
    }

    private static async Task<int> GetNextShipmentNumberAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT COALESCE(MAX(shipment_number), 0) + 1
            FROM sales_order_shipments
            WHERE tenant_id = @tenantId
              AND company_id = @companyId;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        return Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));
    }

    private static async Task<int> GetNextInvoiceDraftNumberAsync(
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
            SELECT COALESCE(MAX(draft_number), 0) + 1
            FROM sales_invoice_drafts
            WHERE tenant_id = @tenantId
              AND company_id = @companyId;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        return Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));
    }

    private static async Task<int> GetNextInvoiceNumberAsync(
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
            FROM sales_invoices
            WHERE tenant_id = @tenantId
              AND company_id = @companyId;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        return Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));
    }

    private static async Task<List<DraftShipmentSelection>> LoadPendingShipmentSelectionAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid tenantId,
        Guid companyId,
        IReadOnlyCollection<Guid> shipmentIds,
        CancellationToken cancellationToken)
    {
        var parameterNames = new List<string>(shipmentIds.Count);
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;

        var index = 0;
        foreach (var shipmentId in shipmentIds)
        {
            var parameterName = $"@shipmentId{index++}";
            parameterNames.Add(parameterName);
            command.Parameters.AddWithValue(parameterName, shipmentId.ToString());
        }

        command.CommandText =
            $"""
            SELECT ss.shipment_id,
                   ss.shipment_series,
                   ss.shipment_number,
                   ss.order_number,
                   ss.shipment_date,
                   ss.warehouse,
                   ss.notes,
                   so.client_code,
                   so.client_name,
                   so.client_tax_id,
                   shl.line_number,
                   shl.description,
                   shl.shipped_quantity,
                   COALESCE(sol.item_code, '') AS item_code,
                   COALESCE(sol.unit_of_measure, '') AS unit_of_measure,
                   COALESCE(sol.unit_price, 0) AS unit_price
            FROM sales_order_shipments ss
            INNER JOIN sales_orders so
              ON so.tenant_id = ss.tenant_id
             AND so.company_id = ss.company_id
             AND so.order_number = ss.order_number
            INNER JOIN sales_order_shipment_lines shl
              ON shl.shipment_id = ss.shipment_id
            LEFT JOIN sales_order_lines sol
              ON sol.tenant_id = shl.tenant_id
             AND sol.company_id = shl.company_id
             AND sol.order_number = shl.order_number
             AND sol.line_number = shl.line_number
            WHERE ss.tenant_id = @tenantId
              AND ss.company_id = @companyId
              AND ss.invoice_status = 'Pending'
              AND ss.shipment_id IN ({string.Join(", ", parameterNames)})
            ORDER BY ss.shipment_number, shl.line_number
            FOR UPDATE;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());

        var selections = new List<DraftShipmentSelection>();
        var byShipment = new Dictionary<Guid, DraftShipmentSelection>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var shipmentId = reader.GetGuid("shipment_id");
            if (!byShipment.TryGetValue(shipmentId, out var shipment))
            {
                shipment = new DraftShipmentSelection
                {
                    ShipmentId = shipmentId,
                    ShipmentSeries = reader.GetStringOrEmpty("shipment_series"),
                    ShipmentNumber = reader.GetInt32(reader.GetOrdinal("shipment_number")),
                    OrderNumber = reader.GetInt32(reader.GetOrdinal("order_number")),
                    ShipmentDate = reader.GetDateTime(reader.GetOrdinal("shipment_date")),
                    Warehouse = reader.GetStringOrEmpty("warehouse"),
                    Notes = reader.GetStringOrEmpty("notes"),
                    ClientCode = reader.GetInt32(reader.GetOrdinal("client_code")),
                    ClientName = reader.GetStringOrEmpty("client_name"),
                    ClientTaxId = reader.GetStringOrEmpty("client_tax_id")
                };
                selections.Add(shipment);
                byShipment[shipmentId] = shipment;
            }

            var quantity = reader.GetDecimal(reader.GetOrdinal("shipped_quantity"));
            var unitPrice = reader.GetDecimal(reader.GetOrdinal("unit_price"));
            shipment.Lines.Add(new DraftShipmentSelectionLine(
                reader.GetStringOrEmpty("item_code"),
                reader.GetStringOrEmpty("description"),
                reader.GetStringOrEmpty("unit_of_measure"),
                quantity,
                unitPrice));
            shipment.TotalShippedQuantity += quantity;
            shipment.EstimatedAmount += decimal.Round(quantity * unitPrice, 2, MidpointRounding.AwayFromZero);
        }

        return selections;
    }

    private async Task<string> ResolveCompanyCenterCodeAsync(
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
              AND is_active = 1
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        var scalar = await command.ExecuteScalarAsync(cancellationToken);

        var centerCode = Convert.ToString(scalar)?.Trim();
        if (string.IsNullOrWhiteSpace(centerCode))
        {
            throw new InvalidOperationException("La empresa activa no está enlazada a un centro legacy.");
        }

        return centerCode;
    }

    private static async Task<decimal> GetCurrentStockAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        string warehouse,
        string itemCode,
        string itemDescription,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT COALESCE(SUM(
                CASE
                    WHEN movement_type LIKE 'Inbound%' THEN quantity
                    ELSE -quantity
                END
            ), 0) AS current_stock
            FROM inventory_movements
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND COALESCE(warehouse, '') = @warehouse
              AND COALESCE(item_code, '') = @itemCode
              AND item_description = @itemDescription;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@warehouse", warehouse);
        command.Parameters.AddWithValue("@itemCode", itemCode);
        command.Parameters.AddWithValue("@itemDescription", itemDescription);
        return Convert.ToDecimal(await command.ExecuteScalarAsync(cancellationToken));
    }

    private async Task<ClientSnapshot?> GetClientSnapshotAsync(
        string centerCode,
        int clientCode,
        CancellationToken cancellationToken)
    {
        if (!_legacyConnectionFactory.IsConfigured)
        {
            return null;
        }

        await using var connection = await _legacyConnectionFactory.OpenConnectionAsync(cancellationToken);
        return await GetClientSnapshotAsync(connection, centerCode, clientCode, cancellationToken);
    }

    private static async Task<ClientSnapshot?> GetClientSnapshotAsync(
        MySqlConnection connection,
        string centerCode,
        int clientCode,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT CODI, NOM, NIF, DOM, CP, POB, PROV, PAIS
            FROM clients
            WHERE CENTRO = @centerCode
              AND CODI = @clientCode
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@clientCode", clientCode);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        return new ClientSnapshot(
            reader.GetInt32(reader.GetOrdinal("CODI")),
            reader.GetStringOrEmpty("NOM"),
            reader.GetStringOrEmpty("NIF"),
            reader.GetStringOrEmpty("DOM"),
            reader.GetStringOrEmpty("CP"),
            reader.GetStringOrEmpty("POB"),
            reader.GetStringOrEmpty("PROV"),
            reader.GetStringOrEmpty("PAIS"));
    }

    private async Task AuditAsync(
        SalesOrderDetailDto? previous,
        SaveSalesOrderCommand current,
        ClientSnapshot clientSnapshot,
        int orderNumber,
        CancellationToken cancellationToken)
    {
        var changes = new List<string>();
        var currentTotal = current.Lines.Sum(line => decimal.Round(line.Quantity * line.UnitPrice, 2, MidpointRounding.AwayFromZero));

        if (previous is null)
        {
            changes.Add($"Numero={orderNumber}");
            changes.Add($"Cliente={clientSnapshot.Name} ({clientSnapshot.Code})");
            changes.Add($"Estado={current.Status}");
            changes.Add($"Lineas={current.Lines.Count}");
            changes.Add($"Total={currentTotal:0.00}");
        }
        else
        {
            AppendChange(changes, "Cliente", $"{previous.ClientName} ({previous.ClientCode})", $"{clientSnapshot.Name} ({clientSnapshot.Code})");
            AppendChange(changes, "Fecha", previous.DocumentDate.ToString("yyyy-MM-dd"), current.DocumentDate.ToString("yyyy-MM-dd"));
            AppendChange(changes, "Solicitada", FormatDate(previous.RequestedDate), FormatDate(current.RequestedDate));
            AppendChange(changes, "Estado", previous.Status, current.Status);
            AppendChange(changes, "Notas", previous.Notes, current.Notes);
            AppendChange(changes, "Lineas", previous.Lines.Count.ToString(), current.Lines.Count.ToString());
            AppendChange(changes, "Total", previous.TotalAmount.ToString("0.00"), currentTotal.ToString("0.00"));
        }

        if (previous is not null && changes.Count == 0)
        {
            return;
        }

        var action = previous is null
            ? "SalesOrderCreated"
            : !string.Equals(previous.Status, current.Status, StringComparison.OrdinalIgnoreCase)
                ? "SalesOrderStatusChanged"
                : "SalesOrderUpdated";

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = current.TenantId,
            CompanyId = current.CompanyId,
            UserId = _currentUserContext.UserId,
            Action = action,
            EntityName = "PedidoVenta",
            EntityId = orderNumber.ToString(),
            Details = string.Join("; ", changes)
        }, cancellationToken);
    }

    private static void NormalizeAndValidate(SaveSalesOrderCommand command)
    {
        command.Status = string.IsNullOrWhiteSpace(command.Status) ? SalesOrderStatuses.Draft : command.Status.Trim();
        command.Notes = command.Notes.Trim();

        if (!SalesOrderStatuses.All.Contains(command.Status, StringComparer.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("El estado del pedido no es válido.");
        }

        if (command.ClientCode <= 0)
        {
            throw new InvalidOperationException("Debes seleccionar un cliente válido.");
        }

        if (command.DocumentDate == default)
        {
            command.DocumentDate = DateTime.Today;
        }

        if (command.Lines.Count == 0)
        {
            throw new InvalidOperationException("El pedido debe tener al menos una línea.");
        }

        var normalizedLines = new List<SalesOrderLineInputDto>();
        var lineNumber = 1;
        foreach (var line in command.Lines)
        {
            var normalized = new SalesOrderLineInputDto
            {
                LineNumber = lineNumber++,
                ItemCode = line.ItemCode.Trim(),
                Description = line.Description.Trim(),
                Quantity = decimal.Round(line.Quantity, 3, MidpointRounding.AwayFromZero),
                UnitOfMeasure = line.UnitOfMeasure.Trim(),
                UnitPrice = decimal.Round(line.UnitPrice, 4, MidpointRounding.AwayFromZero),
                RequestedDate = line.RequestedDate?.Date,
                Notes = line.Notes.Trim()
            };

            if (string.IsNullOrWhiteSpace(normalized.Description))
            {
                throw new InvalidOperationException("Todas las líneas deben tener descripción.");
            }

            if (normalized.Quantity <= 0)
            {
                throw new InvalidOperationException("La cantidad de cada línea debe ser mayor que cero.");
            }

            if (normalized.UnitPrice < 0)
            {
                throw new InvalidOperationException("El precio unitario no puede ser negativo.");
            }

            normalizedLines.Add(normalized);
        }

        command.Lines = normalizedLines;
    }

    private static void NormalizeAndValidateShipment(RegisterSalesOrderShipmentCommand command)
    {
        command.Warehouse = command.Warehouse.Trim();
        command.Notes = command.Notes.Trim();

        if (command.OrderNumber <= 0)
        {
            throw new InvalidOperationException("El pedido a expedir no es válido.");
        }

        if (command.ShipmentDate == default)
        {
            command.ShipmentDate = DateTime.Today;
        }

        if (string.IsNullOrWhiteSpace(command.Warehouse))
        {
            throw new InvalidOperationException("Debes indicar un almacén para la salida.");
        }

        var normalizedLines = command.Lines
            .Where(line => line.ShippedQuantity > 0)
            .Select(line => new RegisterSalesOrderShipmentLineDto
            {
                LineNumber = line.LineNumber,
                ShippedQuantity = decimal.Round(line.ShippedQuantity, 3, MidpointRounding.AwayFromZero)
            })
            .ToList();

        if (normalizedLines.Count == 0)
        {
            throw new InvalidOperationException("Debes indicar al menos una cantidad expedida.");
        }

        if (normalizedLines.Any(line => line.LineNumber <= 0))
        {
            throw new InvalidOperationException("Hay líneas de expedición no válidas.");
        }

        if (normalizedLines.GroupBy(line => line.LineNumber).Any(group => group.Count() > 1))
        {
            throw new InvalidOperationException("No puedes repetir la misma línea en la misma salida.");
        }

        command.Lines = normalizedLines;
    }

    private static void NormalizeAndValidateInvoiceDraft(CreateSalesInvoiceDraftCommand command)
    {
        command.Notes = command.Notes.Trim();

        if (command.IssueDate == default)
        {
            command.IssueDate = DateTime.Today;
        }

        command.ShipmentIds = command.ShipmentIds
            .Where(id => id != Guid.Empty)
            .Distinct()
            .ToList();

        if (command.ShipmentIds.Count == 0)
        {
            throw new InvalidOperationException("Debes seleccionar al menos un albarán pendiente para crear el borrador.");
        }
    }

    private static void NormalizeAndValidateInvoicePayment(RegisterSalesInvoicePaymentCommand command)
    {
        command.Method = command.Method.Trim();
        command.Reference = command.Reference.Trim();
        command.Notes = command.Notes.Trim();

        if (command.InvoiceNumber <= 0)
        {
            throw new InvalidOperationException("La factura indicada no es válida.");
        }

        if (command.PaymentDate == default)
        {
            command.PaymentDate = DateTime.Today;
        }

        command.Amount = decimal.Round(command.Amount, 2, MidpointRounding.AwayFromZero);
        if (command.Amount <= 0)
        {
            throw new InvalidOperationException("El importe del cobro debe ser mayor que cero.");
        }
    }

    private static void ValidateAgainstShippedLines(
        IReadOnlyDictionary<int, SalesOrderLineDto> existingLines,
        SaveSalesOrderCommand command)
    {
        if (existingLines.Count == 0)
        {
            return;
        }

        foreach (var existingLine in existingLines.Values.Where(line => line.ShippedQuantity > 0))
        {
            var matchingLine = command.Lines.FirstOrDefault(line => line.LineNumber == existingLine.LineNumber);
            if (matchingLine is null)
            {
                throw new InvalidOperationException($"No puedes quitar la línea {existingLine.LineNumber} porque ya tiene salidas registradas.");
            }

            if (matchingLine.Quantity < existingLine.ShippedQuantity)
            {
                throw new InvalidOperationException($"La línea {existingLine.LineNumber} no puede quedar por debajo de la cantidad ya expedida.");
            }
        }
    }

    private static string DetermineStatusAfterShipment(string currentStatus, IReadOnlyCollection<SalesOrderLineDto> lines)
    {
        if (string.Equals(currentStatus, SalesOrderStatuses.Cancelled, StringComparison.OrdinalIgnoreCase))
        {
            return SalesOrderStatuses.Cancelled;
        }

        var totalQuantity = lines.Sum(line => line.Quantity);
        var shippedQuantity = lines.Sum(line => line.ShippedQuantity);

        if (shippedQuantity <= 0 || totalQuantity <= 0)
        {
            return string.Equals(currentStatus, SalesOrderStatuses.Confirmed, StringComparison.OrdinalIgnoreCase)
                ? SalesOrderStatuses.Confirmed
                : SalesOrderStatuses.Draft;
        }

        return lines.All(line => line.IsFullyShipped)
            ? SalesOrderStatuses.Shipped
            : SalesOrderStatuses.PartiallyShipped;
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

    private static async Task<bool> IsLegacySyncActiveForCompanyAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        string moduleKey,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT COUNT(*)
            FROM legacy_sync_checkpoints
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND module_key = @moduleKey
              AND last_status IN ('Completed', 'CompletedWithErrors');
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@moduleKey", moduleKey);
        return Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken)) > 0;
    }

    private static async Task EnsureSalesOrdersWriteAllowedAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        CancellationToken cancellationToken)
    {
        if (await IsLegacySyncActiveForCompanyAsync(connection, tenantId, companyId, LegacySyncModuleKeys.SalesOrders, cancellationToken))
        {
            throw new InvalidOperationException("Ventas / Pedidos está en convivencia con legacy para esta empresa. Mientras el módulo esté sincronizado, la web queda en solo lectura.");
        }
    }

    private static async Task EnsureSalesShipmentsWriteAllowedAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        CancellationToken cancellationToken)
    {
        if (await IsLegacySyncActiveForCompanyAsync(connection, tenantId, companyId, LegacySyncModuleKeys.SalesShipments, cancellationToken))
        {
            throw new InvalidOperationException("Ventas / Albaranes está en convivencia con legacy para esta empresa. Mientras el módulo esté sincronizado, la web queda en solo lectura.");
        }
    }

    private static async Task EnsureSalesInvoicesWriteAllowedAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        CancellationToken cancellationToken)
    {
        if (await IsLegacySyncActiveForCompanyAsync(connection, tenantId, companyId, LegacySyncModuleKeys.SalesInvoices, cancellationToken))
        {
            throw new InvalidOperationException("Ventas / Facturas está en convivencia con legacy para esta empresa. Mientras el módulo esté sincronizado, la web queda en solo lectura.");
        }
    }

    private static async Task<InvoicePaymentHeader?> LoadInvoicePaymentHeaderAsync(
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
            FROM sales_invoices
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND invoice_number = @invoiceNumber
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
            InvoiceId: reader.GetGuid("invoice_id"),
            InvoiceSeries: reader.GetStringOrEmpty("invoice_series"),
            InvoiceNumber: reader.GetInt32(reader.GetOrdinal("invoice_number")),
            Status: reader.GetStringOrEmpty("status"),
            TotalAmount: reader.GetDecimal(reader.GetOrdinal("total_amount")),
            AmountPaid: reader.GetDecimal(reader.GetOrdinal("amount_paid")),
            OutstandingAmount: reader.GetDecimal(reader.GetOrdinal("outstanding_amount")));
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
            FROM sales_invoice_payments
            WHERE invoice_id = @invoiceId
              AND tenant_id = @tenantId
              AND company_id = @companyId;
            """;
        command.Parameters.AddWithValue("@invoiceId", invoiceId.ToString());
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        return Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));
    }

    private static string DetermineInvoicePaymentStatus(decimal amountPaid, decimal totalAmount)
    {
        if (amountPaid <= 0m)
        {
            return SalesInvoicePaymentStatuses.Pending;
        }

        return amountPaid >= totalAmount
            ? SalesInvoicePaymentStatuses.Paid
            : SalesInvoicePaymentStatuses.PartiallyPaid;
    }

    private static void AppendChange(List<string> changes, string label, string previous, string current)
    {
        if (string.Equals(previous?.Trim(), current?.Trim(), StringComparison.Ordinal))
        {
            return;
        }

        changes.Add($"{label}: '{previous}' -> '{current}'");
    }

    private static string FormatDate(DateTime? value) => value?.ToString("yyyy-MM-dd") ?? string.Empty;
    private static object DbValue(string value) => string.IsNullOrWhiteSpace(value) ? DBNull.Value : value;
    private static object DbValue(DateTime? value) => value.HasValue ? value.Value : DBNull.Value;
    private static string BuildLegacyOrderNotes(string customerReference, string legacyNotes)
    {
        var parts = new List<string>();
        if (!string.IsNullOrWhiteSpace(customerReference))
        {
            parts.Add($"Ref. cliente: {customerReference.Trim()}");
        }

        if (!string.IsNullOrWhiteSpace(legacyNotes))
        {
            parts.Add(legacyNotes.Trim());
        }

        return string.Join(Environment.NewLine, parts);
    }
    private static string BuildInvoiceDraftSeries(string companyLegacyCenterCode) =>
        $"PF-{(string.IsNullOrWhiteSpace(companyLegacyCenterCode) ? "GEN" : companyLegacyCenterCode.Trim().ToUpperInvariant())}";
    private static string BuildInvoiceSeries(string companyLegacyCenterCode) =>
        $"FV-{(string.IsNullOrWhiteSpace(companyLegacyCenterCode) ? "GEN" : companyLegacyCenterCode.Trim().ToUpperInvariant())}";
    private static string BuildShipmentSeries(string companyLegacyCenterCode) =>
        $"AV-{(string.IsNullOrWhiteSpace(companyLegacyCenterCode) ? "GEN" : companyLegacyCenterCode.Trim().ToUpperInvariant())}";

    private static string BuildSalesOrderSearchOrderByClause(SalesOrderFilter filter)
    {
        var column = filter.SortColumn switch
        {
            nameof(SalesOrderListItemDto.OrderNumber) => "so.order_number",
            nameof(SalesOrderListItemDto.ClientName) => "so.client_name",
            nameof(SalesOrderListItemDto.DocumentDate) => "so.document_date",
            nameof(SalesOrderListItemDto.RequestedDate) => "so.requested_date",
            nameof(SalesOrderListItemDto.Status) => "so.status",
            nameof(SalesOrderListItemDto.LineCount) => "line_count",
            nameof(SalesOrderListItemDto.TotalAmount) => "total_amount",
            _ => string.Empty
        };

        if (string.IsNullOrWhiteSpace(column))
        {
            return "ORDER BY so.order_number DESC";
        }

        var direction = filter.SortDescending ? "DESC" : "ASC";
        return $"ORDER BY {column} {direction}, so.order_number DESC";
    }

    private static string BuildSalesShipmentSearchOrderByClause(SalesOrderFilter filter)
    {
        var column = filter.SortColumn switch
        {
            nameof(SalesOrderShipmentDto.ShipmentNumber) => "ss.shipment_number",
            nameof(SalesOrderShipmentDto.OrderNumber) => "ss.order_number",
            nameof(SalesOrderShipmentDto.ClientName) => "so.client_name",
            nameof(SalesOrderShipmentDto.ShipmentDate) => "ss.shipment_date",
            nameof(SalesOrderShipmentDto.Warehouse) => "ss.warehouse",
            nameof(SalesOrderShipmentDto.TotalShippedQuantity) => "total_shipped_quantity",
            nameof(SalesOrderShipmentDto.InvoiceStatus) => "ss.invoice_status",
            _ => string.Empty
        };

        if (string.IsNullOrWhiteSpace(column))
        {
            return "ORDER BY ss.shipment_date DESC, ss.shipment_number DESC";
        }

        var direction = filter.SortDescending ? "DESC" : "ASC";
        return $"ORDER BY {column} {direction}, ss.shipment_number DESC";
    }

    private static string BuildPendingShipmentSearchOrderByClause(SalesPreInvoiceFilter filter)
    {
        var column = filter.SortColumn switch
        {
            nameof(PendingSalesShipmentDto.ShipmentNumber) => "ss.shipment_number",
            nameof(PendingSalesShipmentDto.OrderNumber) => "ss.order_number",
            nameof(PendingSalesShipmentDto.ClientName) => "so.client_name",
            nameof(PendingSalesShipmentDto.ShipmentDate) => "ss.shipment_date",
            nameof(PendingSalesShipmentDto.Warehouse) => "ss.warehouse",
            nameof(PendingSalesShipmentDto.TotalShippedQuantity) => "total_shipped_quantity",
            nameof(PendingSalesShipmentDto.EstimatedAmount) => "estimated_amount",
            _ => string.Empty
        };

        if (string.IsNullOrWhiteSpace(column))
        {
            return "ORDER BY COALESCE(ss.invoice_ready_utc, ss.shipment_date) DESC, ss.shipment_number DESC";
        }

        var direction = filter.SortDescending ? "DESC" : "ASC";
        return $"ORDER BY {column} {direction}, ss.shipment_number DESC";
    }

    private static string BuildInvoiceDraftSearchOrderByClause(SalesPreInvoiceFilter filter)
    {
        var column = filter.SortColumn switch
        {
            nameof(SalesInvoiceDraftListItemDto.DraftNumber) => "sid.draft_number",
            nameof(SalesInvoiceDraftListItemDto.ClientName) => "sid.client_name",
            nameof(SalesInvoiceDraftListItemDto.IssueDate) => "sid.issue_date",
            nameof(SalesInvoiceDraftListItemDto.DueDate) => "sid.due_date",
            nameof(SalesInvoiceDraftListItemDto.Status) => "sid.status",
            nameof(SalesInvoiceDraftListItemDto.ShipmentCount) => "sid.shipment_count",
            nameof(SalesInvoiceDraftListItemDto.TotalAmount) => "sid.total_amount",
            _ => string.Empty
        };

        if (string.IsNullOrWhiteSpace(column))
        {
            return "ORDER BY sid.issue_date DESC, sid.draft_number DESC";
        }

        var direction = filter.SortDescending ? "DESC" : "ASC";
        return $"ORDER BY {column} {direction}, sid.draft_number DESC";
    }

    private static string BuildInvoiceSearchOrderByClause(SalesPreInvoiceFilter filter)
    {
        var column = filter.SortColumn switch
        {
            nameof(SalesInvoiceListItemDto.InvoiceNumber) => "si.invoice_number",
            nameof(SalesInvoiceListItemDto.DraftNumber) => "si.draft_number",
            nameof(SalesInvoiceListItemDto.ClientName) => "si.client_name",
            nameof(SalesInvoiceListItemDto.IssueDate) => "si.issue_date",
            nameof(SalesInvoiceListItemDto.DueDate) => "si.due_date",
            nameof(SalesInvoiceListItemDto.Status) => "si.status",
            nameof(SalesInvoiceListItemDto.PaymentStatus) => "si.payment_status",
            nameof(SalesInvoiceListItemDto.ShipmentCount) => "si.shipment_count",
            nameof(SalesInvoiceListItemDto.OutstandingAmount) => "si.outstanding_amount",
            nameof(SalesInvoiceListItemDto.AccountingStatus) => "si.accounting_status",
            nameof(SalesInvoiceListItemDto.TotalAmount) => "si.total_amount",
            _ => string.Empty
        };

        if (string.IsNullOrWhiteSpace(column))
        {
            return "ORDER BY si.issue_date DESC, si.invoice_number DESC";
        }

        var direction = filter.SortDescending ? "DESC" : "ASC";
        return $"ORDER BY {column} {direction}, si.invoice_number DESC";
    }

    private sealed class DraftShipmentSelection
    {
        public Guid ShipmentId { get; set; }
        public string ShipmentSeries { get; set; } = string.Empty;
        public int ShipmentNumber { get; set; }
        public int OrderNumber { get; set; }
        public DateTime ShipmentDate { get; set; }
        public string Warehouse { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public int ClientCode { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public string ClientTaxId { get; set; } = string.Empty;
        public decimal TotalShippedQuantity { get; set; }
        public decimal EstimatedAmount { get; set; }
        public List<DraftShipmentSelectionLine> Lines { get; } = [];

        public string DisplayNumber => string.IsNullOrWhiteSpace(ShipmentSeries)
            ? ShipmentNumber.ToString()
            : $"{ShipmentSeries}/{ShipmentNumber:000000}";
    }

    private sealed record DraftShipmentSelectionLine(
        string ItemCode,
        string Description,
        string UnitOfMeasure,
        decimal Quantity,
        decimal UnitPrice);

    private sealed record DraftAggregatedLine(
        int LineNumber,
        string ItemCode,
        string Description,
        string UnitOfMeasure,
        decimal UnitPrice,
        decimal Quantity,
        decimal LineTotal,
        string SourceSummary);

    private sealed record DraftIssueHeader(
        Guid DraftId,
        string DraftSeries,
        int DraftNumber,
        int ClientCode,
        string ClientName,
        string ClientTaxId,
        DateTime IssueDate,
        DateTime? DueDate,
        string Status,
        int ShipmentCount,
        decimal TotalQuantity,
        decimal TotalAmount,
        string Notes);

    private sealed record InvoicePaymentHeader(
        Guid InvoiceId,
        string InvoiceSeries,
        int InvoiceNumber,
        string Status,
        decimal TotalAmount,
        decimal AmountPaid,
        decimal OutstandingAmount)
    {
        public string InvoiceDisplayNumber => string.IsNullOrWhiteSpace(InvoiceSeries)
            ? InvoiceNumber.ToString()
            : $"{InvoiceSeries}/{InvoiceNumber:000000}";
    }

    private sealed record ClientSnapshot(
        int Code,
        string Name,
        string TaxId,
        string Address,
        string PostalCode,
        string City,
        string Province,
        string Country);
    private sealed record ImportCompanyContext(Guid CompanyId, string LegacyCenterCode);
    private sealed record LegacySalesOrderHeader(int OrderNumber, int ClientCode, string ClientName, string ClientTaxId, DateTime DocumentDate, string Notes);
    private sealed record LegacySalesOrderLine(int LineNumber, string ItemCode, string Description, decimal Quantity, decimal UnitPrice, decimal PendingQuantity, decimal ShippedQuantity, DateTime? RequestedDate);
    private sealed record ImportedSalesOrderLine(int LineNumber, string ItemCode, string Description, decimal Quantity, decimal ShippedQuantity, decimal UnitPrice, DateTime? RequestedDate);
}

