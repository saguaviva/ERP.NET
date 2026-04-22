using Erp.Application.Auditing;
using Erp.Application.Companies;
using Erp.Application.Contexts;
using Erp.Application.LegacySync;
using Erp.Application.Purchases;
using Erp.Domain.Common;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Support;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Purchases;

public sealed class MySqlPurchaseOrderService : IPurchaseOrderQueries, IPurchaseOrderService
{
    private readonly MySqlConnectionFactory _connectionFactory;
    private readonly IAuditLogService _auditLogService;
    private readonly ICompanyAccessService _companyAccessService;
    private readonly ICurrentUserContext _currentUserContext;
    private readonly ITenantContext _tenantContext;
    private readonly IActiveCompanyContext _activeCompanyContext;

    public MySqlPurchaseOrderService(
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

    public async Task<PurchaseOrderSearchResultDto> SearchAsync(
        Guid tenantId,
        Guid companyId,
        PurchaseOrderFilter filter,
        CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return new PurchaseOrderSearchResultDto();
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
                FROM purchase_orders po
                LEFT JOIN prove p
                  ON p.CODI = po.supplier_code
                 AND p.CENTRO = @centerCode
                WHERE po.tenant_id = @tenantId
                  AND po.company_id = @companyId
                  AND COALESCE(po.is_deleted, 0) = 0
                  AND (
                        @includeClosed = 1
                        OR po.status NOT IN ('Received', 'Cancelled')
                      )
                  AND (
                        @status = ''
                        OR po.status = @status
                      )
                  AND (
                        @search = ''
                        OR CAST(po.order_number AS CHAR) LIKE @likeSearch
                        OR COALESCE(NULLIF(po.supplier_name, ''), p.NOM, '') LIKE @likeSearch
                        OR po.notes LIKE @likeSearch
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
                return new PurchaseOrderSearchResultDto
                {
                    TotalCount = 0
                };
            }

            await using var command = connection.CreateCommand();
            command.CommandText =
                $"""
                SELECT po.order_number,
                       po.supplier_code,
                       COALESCE(NULLIF(po.supplier_name, ''), p.NOM, '') AS supplier_name,
                       po.document_date,
                       po.expected_date,
                       po.status,
                       po.notes,
                       COUNT(pol.line_number) AS line_count,
                       COALESCE(SUM(pol.quantity * pol.unit_price), 0) AS total_amount
                FROM purchase_orders po
                LEFT JOIN purchase_order_lines pol
                  ON pol.tenant_id = po.tenant_id
                 AND pol.company_id = po.company_id
                 AND pol.order_number = po.order_number
                LEFT JOIN prove p
                  ON p.CODI = po.supplier_code
                 AND p.CENTRO = @centerCode
                WHERE po.tenant_id = @tenantId
                  AND po.company_id = @companyId
                  AND COALESCE(po.is_deleted, 0) = 0
                  AND (
                        @includeClosed = 1
                        OR po.status NOT IN ('Received', 'Cancelled')
                      )
                  AND (
                        @status = ''
                        OR po.status = @status
                      )
                  AND (
                        @search = ''
                        OR CAST(po.order_number AS CHAR) LIKE @likeSearch
                        OR COALESCE(NULLIF(po.supplier_name, ''), p.NOM, '') LIKE @likeSearch
                        OR po.notes LIKE @likeSearch
                      )
                GROUP BY po.order_number, po.supplier_code, po.supplier_name, p.NOM, po.document_date, po.expected_date, po.status, po.notes
                {BuildPurchaseOrderSearchOrderByClause(filter)}
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

            var items = new List<PurchaseOrderListItemDto>();
            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                items.Add(new PurchaseOrderListItemDto
                {
                    OrderNumber = reader.GetInt32(reader.GetOrdinal("order_number")),
                    SupplierCode = reader.GetInt32(reader.GetOrdinal("supplier_code")),
                    SupplierName = reader.GetStringOrEmpty("supplier_name"),
                    DocumentDate = reader.GetDateTime(reader.GetOrdinal("document_date")),
                    ExpectedDate = reader.IsDBNull(reader.GetOrdinal("expected_date")) ? null : reader.GetDateTime(reader.GetOrdinal("expected_date")),
                    Status = reader.GetStringOrEmpty("status"),
                    Notes = reader.GetStringOrEmpty("notes"),
                    LineCount = reader.GetInt32(reader.GetOrdinal("line_count")),
                    TotalAmount = reader.GetDecimal(reader.GetOrdinal("total_amount"))
                });
            }

            return new PurchaseOrderSearchResultDto
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }

    public async Task<PurchaseOrderDetailDto?> GetByOrderNumberAsync(
        Guid tenantId,
        Guid companyId,
        int orderNumber,
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
            SELECT po.order_number,
                   c.name AS company_name,
                   c.legacy_center_code,
                   po.supplier_code,
                   COALESCE(NULLIF(po.supplier_name, ''), p.NOM, '') AS supplier_name,
                   COALESCE(NULLIF(po.supplier_tax_id, ''), p.NIF, '') AS supplier_tax_id,
                   po.document_date,
                   po.expected_date,
                   po.status,
                   po.notes
            FROM purchase_orders po
            LEFT JOIN companies c
              ON c.id = po.company_id
             AND c.tenant_id = po.tenant_id
            LEFT JOIN prove p
              ON p.CODI = po.supplier_code
             AND p.CENTRO = @centerCode
            WHERE po.tenant_id = @tenantId
              AND po.company_id = @companyId
              AND COALESCE(po.is_deleted, 0) = 0
              AND po.order_number = @orderNumber
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

        var detail = new PurchaseOrderDetailDto
        {
            OrderNumber = reader.GetInt32(reader.GetOrdinal("order_number")),
            CompanyName = reader.GetStringOrEmpty("company_name"),
            CompanyLegacyCenterCode = reader.GetStringOrEmpty("legacy_center_code"),
            SupplierCode = reader.GetInt32(reader.GetOrdinal("supplier_code")),
            SupplierName = reader.GetStringOrEmpty("supplier_name"),
            SupplierTaxId = reader.GetStringOrEmpty("supplier_tax_id"),
            DocumentDate = reader.GetDateTime(reader.GetOrdinal("document_date")),
            ExpectedDate = reader.IsDBNull(reader.GetOrdinal("expected_date")) ? null : reader.GetDateTime(reader.GetOrdinal("expected_date")),
            Status = reader.GetStringOrEmpty("status"),
            Notes = reader.GetStringOrEmpty("notes")
        };
        await reader.DisposeAsync();

        detail.Lines = await LoadLinesAsync(connection, tenantId, companyId, orderNumber, cancellationToken);
        detail.TotalAmount = detail.Lines.Sum(line => line.LineTotal);
        detail.TotalReceivedQuantity = detail.Lines.Sum(line => line.ReceivedQuantity);
        detail.TotalPendingQuantity = detail.Lines.Sum(line => line.PendingQuantity);
        return detail;
    }

    public async Task<IReadOnlyCollection<PurchaseOrderReceiptDto>> GetReceiptsAsync(
        Guid tenantId,
        Guid companyId,
        int orderNumber,
        CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return [];
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var receipts = new List<PurchaseOrderReceiptDto>();

        await using (var command = connection.CreateCommand())
        {
            command.CommandText =
                """
                SELECT receipt_id, receipt_series, receipt_number, receipt_date, warehouse, carrier, supplier_reference, vehicle_plate, package_count, gross_weight_kg, notes
                FROM purchase_order_receipts
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND COALESCE(is_deleted, 0) = 0
                  AND order_number = @orderNumber
                ORDER BY receipt_date DESC, receipt_id DESC;
                """;
            command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            command.Parameters.AddWithValue("@companyId", companyId.ToString());
            command.Parameters.AddWithValue("@orderNumber", orderNumber);

            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                receipts.Add(new PurchaseOrderReceiptDto
                {
                    ReceiptId = reader.GetGuid("receipt_id"),
                    ReceiptSeries = reader.GetStringOrEmpty("receipt_series"),
                    ReceiptNumber = reader.IsDBNull(reader.GetOrdinal("receipt_number")) ? 0 : reader.GetInt32(reader.GetOrdinal("receipt_number")),
                    OrderNumber = orderNumber,
                    ReceiptDate = reader.GetDateTime(reader.GetOrdinal("receipt_date")),
                    Warehouse = reader.GetStringOrEmpty("warehouse"),
                    Carrier = reader.GetStringOrEmpty("carrier"),
                    SupplierReference = reader.GetStringOrEmpty("supplier_reference"),
                    VehiclePlate = reader.GetStringOrEmpty("vehicle_plate"),
                    PackageCount = reader.IsDBNull(reader.GetOrdinal("package_count")) ? null : reader.GetInt32(reader.GetOrdinal("package_count")),
                    GrossWeightKg = reader.IsDBNull(reader.GetOrdinal("gross_weight_kg")) ? null : reader.GetDecimal(reader.GetOrdinal("gross_weight_kg")),
                    Notes = reader.GetStringOrEmpty("notes")
                });
            }
        }

        if (receipts.Count == 0)
        {
            return receipts;
        }

        var receiptLookup = receipts.ToDictionary(receipt => receipt.ReceiptId);

        await using (var linesCommand = connection.CreateCommand())
        {
            linesCommand.CommandText =
                """
                SELECT receipt_id, line_number, description, received_quantity
                FROM purchase_order_receipt_lines
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND order_number = @orderNumber
                ORDER BY receipt_id DESC, line_number;
                """;
            linesCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            linesCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
            linesCommand.Parameters.AddWithValue("@orderNumber", orderNumber);

            await using var reader = await linesCommand.ExecuteReaderAsync(cancellationToken);
            var linesByReceipt = new Dictionary<Guid, List<PurchaseOrderReceiptLineDto>>();
            while (await reader.ReadAsync(cancellationToken))
            {
                var receiptId = reader.GetGuid("receipt_id");
                if (!linesByReceipt.TryGetValue(receiptId, out var lines))
                {
                    lines = [];
                    linesByReceipt[receiptId] = lines;
                }

                lines.Add(new PurchaseOrderReceiptLineDto
                {
                    LineNumber = reader.GetInt32(reader.GetOrdinal("line_number")),
                    Description = reader.GetStringOrEmpty("description"),
                    ReceivedQuantity = reader.GetDecimal(reader.GetOrdinal("received_quantity"))
                });
            }

            foreach (var receipt in receipts)
            {
                if (linesByReceipt.TryGetValue(receipt.ReceiptId, out var lines))
                {
                    receipt.Lines = lines;
                    receipt.TotalReceivedQuantity = lines.Sum(line => line.ReceivedQuantity);
                }
            }
        }

        return receipts;
    }

    public async Task<PurchaseReceiptSearchResultDto> SearchReceiptsAsync(
        Guid tenantId,
        Guid companyId,
        PurchaseReceiptFilter filter,
        CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return new PurchaseReceiptSearchResultDto();
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);

        var pageSize = Math.Clamp(filter.PageSize, 10, 200);
        var page = Math.Max(filter.Page, 1);
        var offset = (page - 1) * pageSize;
        var search = filter.Search?.Trim() ?? string.Empty;
        var likeSearch = $"%{search}%";
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using (var countCommand = connection.CreateCommand())
        {
            countCommand.CommandText =
                """
                SELECT COUNT(*)
                FROM purchase_order_receipts pr
                LEFT JOIN purchase_orders po
                  ON po.tenant_id = pr.tenant_id
                 AND po.company_id = pr.company_id
                 AND po.order_number = pr.order_number
                WHERE pr.tenant_id = @tenantId
                  AND pr.company_id = @companyId
                  AND COALESCE(pr.is_deleted, 0) = 0
                  AND (
                        @search = ''
                        OR CAST(pr.receipt_number AS CHAR) LIKE @likeSearch
                        OR CAST(pr.order_number AS CHAR) LIKE @likeSearch
                        OR COALESCE(po.supplier_name, '') LIKE @likeSearch
                        OR COALESCE(pr.warehouse, '') LIKE @likeSearch
                        OR COALESCE(pr.carrier, '') LIKE @likeSearch
                        OR COALESCE(pr.supplier_reference, '') LIKE @likeSearch
                        OR pr.notes LIKE @likeSearch
                      );
                """;
            countCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            countCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
            countCommand.Parameters.AddWithValue("@search", search);
            countCommand.Parameters.AddWithValue("@likeSearch", likeSearch);

            var totalCount = Convert.ToInt32(await countCommand.ExecuteScalarAsync(cancellationToken));
            if (totalCount == 0)
            {
                return new PurchaseReceiptSearchResultDto();
            }

            await using var command = connection.CreateCommand();
            var orderBy = BuildPurchaseReceiptSearchOrderByClause(filter);
            command.CommandText =
                $"""
                SELECT pr.receipt_series,
                       pr.receipt_number,
                       pr.order_number,
                       po.supplier_code,
                       po.supplier_name,
                       pr.receipt_date,
                       pr.warehouse,
                       pr.carrier,
                       pr.supplier_reference,
                       pr.notes,
                       COUNT(prl.line_number) AS line_count,
                       COALESCE(SUM(prl.received_quantity), 0) AS total_received_quantity
                FROM purchase_order_receipts pr
                LEFT JOIN purchase_orders po
                  ON po.tenant_id = pr.tenant_id
                 AND po.company_id = pr.company_id
                 AND po.order_number = pr.order_number
                LEFT JOIN purchase_order_receipt_lines prl
                  ON prl.receipt_id = pr.receipt_id
                WHERE pr.tenant_id = @tenantId
                  AND pr.company_id = @companyId
                  AND COALESCE(pr.is_deleted, 0) = 0
                  AND (
                        @search = ''
                        OR CAST(pr.receipt_number AS CHAR) LIKE @likeSearch
                        OR CAST(pr.order_number AS CHAR) LIKE @likeSearch
                        OR COALESCE(po.supplier_name, '') LIKE @likeSearch
                        OR COALESCE(pr.warehouse, '') LIKE @likeSearch
                        OR COALESCE(pr.carrier, '') LIKE @likeSearch
                        OR COALESCE(pr.supplier_reference, '') LIKE @likeSearch
                        OR pr.notes LIKE @likeSearch
                      )
                GROUP BY pr.receipt_series, pr.receipt_number, pr.order_number, po.supplier_code, po.supplier_name, pr.receipt_date, pr.warehouse, pr.carrier, pr.supplier_reference, pr.notes
                {orderBy}
                LIMIT @pageSize OFFSET @offset;
                """;
            command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            command.Parameters.AddWithValue("@companyId", companyId.ToString());
            command.Parameters.AddWithValue("@search", search);
            command.Parameters.AddWithValue("@likeSearch", likeSearch);
            command.Parameters.AddWithValue("@pageSize", pageSize);
            command.Parameters.AddWithValue("@offset", offset);

            var items = new List<PurchaseReceiptListItemDto>();
            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                items.Add(new PurchaseReceiptListItemDto
                {
                    ReceiptSeries = reader.GetStringOrEmpty("receipt_series"),
                    ReceiptNumber = reader.IsDBNull(reader.GetOrdinal("receipt_number")) ? 0 : reader.GetInt32(reader.GetOrdinal("receipt_number")),
                    OrderNumber = reader.GetInt32(reader.GetOrdinal("order_number")),
                    SupplierCode = reader.IsDBNull(reader.GetOrdinal("supplier_code")) ? 0 : reader.GetInt32(reader.GetOrdinal("supplier_code")),
                    SupplierName = reader.GetStringOrEmpty("supplier_name"),
                    ReceiptDate = reader.GetDateTime(reader.GetOrdinal("receipt_date")),
                    Warehouse = reader.GetStringOrEmpty("warehouse"),
                    Carrier = reader.GetStringOrEmpty("carrier"),
                    SupplierReference = reader.GetStringOrEmpty("supplier_reference"),
                    Notes = reader.GetStringOrEmpty("notes"),
                    LineCount = reader.GetInt32(reader.GetOrdinal("line_count")),
                    TotalReceivedQuantity = reader.GetDecimal(reader.GetOrdinal("total_received_quantity"))
                });
            }

            return new PurchaseReceiptSearchResultDto
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }

    public async Task<PurchaseOrderReceiptDto?> GetReceiptByNumberAsync(
        Guid tenantId,
        Guid companyId,
        int receiptNumber,
        CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return null;
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT pr.receipt_id,
                   pr.receipt_series,
                   pr.receipt_number,
                   pr.order_number,
                   pr.receipt_date,
                   pr.warehouse,
                   pr.carrier,
                   pr.supplier_reference,
                   pr.vehicle_plate,
                   pr.package_count,
                   pr.gross_weight_kg,
                   pr.notes,
                   po.supplier_code,
                   po.supplier_name,
                   po.supplier_tax_id,
                   c.name AS company_name,
                   c.legacy_center_code,
                   t.name AS tenant_name,
                   p.DOM,
                   p.CP,
                   p.POB,
                   p.PROV,
                   p.PAIS
            FROM purchase_order_receipts pr
            LEFT JOIN purchase_orders po
              ON po.tenant_id = pr.tenant_id
             AND po.company_id = pr.company_id
             AND po.order_number = pr.order_number
            LEFT JOIN companies c
              ON c.id = pr.company_id
             AND c.tenant_id = pr.tenant_id
            LEFT JOIN tenants t
              ON t.id = pr.tenant_id
            LEFT JOIN prove p
              ON p.CODI = po.supplier_code
             AND p.CENTRO = c.legacy_center_code
            WHERE pr.tenant_id = @tenantId
              AND pr.company_id = @companyId
              AND COALESCE(pr.is_deleted, 0) = 0
              AND pr.receipt_number = @receiptNumber
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@receiptNumber", receiptNumber);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        var receipt = new PurchaseOrderReceiptDto
        {
            ReceiptId = reader.GetGuid("receipt_id"),
            ReceiptSeries = reader.GetStringOrEmpty("receipt_series"),
            ReceiptNumber = reader.IsDBNull(reader.GetOrdinal("receipt_number")) ? 0 : reader.GetInt32(reader.GetOrdinal("receipt_number")),
            OrderNumber = reader.GetInt32(reader.GetOrdinal("order_number")),
            SupplierCode = reader.IsDBNull(reader.GetOrdinal("supplier_code")) ? 0 : reader.GetInt32(reader.GetOrdinal("supplier_code")),
            SupplierName = reader.GetStringOrEmpty("supplier_name"),
            SupplierTaxId = reader.GetStringOrEmpty("supplier_tax_id"),
            SupplierAddress = reader.GetStringOrEmpty("DOM"),
            SupplierPostalCode = reader.GetStringOrEmpty("CP"),
            SupplierCity = reader.GetStringOrEmpty("POB"),
            SupplierProvince = reader.GetStringOrEmpty("PROV"),
            SupplierCountry = reader.GetStringOrEmpty("PAIS"),
            CompanyName = reader.GetStringOrEmpty("company_name"),
            CompanyLegacyCenterCode = reader.GetStringOrEmpty("legacy_center_code"),
            TenantName = reader.GetStringOrEmpty("tenant_name"),
            ReceiptDate = reader.GetDateTime(reader.GetOrdinal("receipt_date")),
            Warehouse = reader.GetStringOrEmpty("warehouse"),
            Carrier = reader.GetStringOrEmpty("carrier"),
            SupplierReference = reader.GetStringOrEmpty("supplier_reference"),
            VehiclePlate = reader.GetStringOrEmpty("vehicle_plate"),
            PackageCount = reader.IsDBNull(reader.GetOrdinal("package_count")) ? null : reader.GetInt32(reader.GetOrdinal("package_count")),
            GrossWeightKg = reader.IsDBNull(reader.GetOrdinal("gross_weight_kg")) ? null : reader.GetDecimal(reader.GetOrdinal("gross_weight_kg")),
            Notes = reader.GetStringOrEmpty("notes")
        };
        await reader.DisposeAsync();

        await using var linesCommand = connection.CreateCommand();
        linesCommand.CommandText =
            """
            SELECT line_number, description, received_quantity
            FROM purchase_order_receipt_lines
            WHERE receipt_id = @receiptId
            ORDER BY line_number;
            """;
        linesCommand.Parameters.AddWithValue("@receiptId", receipt.ReceiptId.ToString());

        var lines = new List<PurchaseOrderReceiptLineDto>();
        await using var linesReader = await linesCommand.ExecuteReaderAsync(cancellationToken);
        while (await linesReader.ReadAsync(cancellationToken))
        {
            lines.Add(new PurchaseOrderReceiptLineDto
            {
                LineNumber = linesReader.GetInt32(linesReader.GetOrdinal("line_number")),
                Description = linesReader.GetStringOrEmpty("description"),
                ReceivedQuantity = linesReader.GetDecimal(linesReader.GetOrdinal("received_quantity"))
            });
        }

        receipt.Lines = lines;
        receipt.TotalReceivedQuantity = lines.Sum(line => line.ReceivedQuantity);
        return receipt;
    }

    public async Task<int> SaveAsync(SavePurchaseOrderCommand command, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return 0;
        }

        await EnsureCompanyAccessAsync(command.TenantId, command.CompanyId, cancellationToken);
        EnsureTenantWriteAccess();
        NormalizeAndValidate(command);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var centerCode = await ResolveCompanyCenterCodeAsync(connection, command.TenantId, command.CompanyId, cancellationToken);
        var supplierSnapshot = await GetSupplierSnapshotAsync(connection, centerCode, command.SupplierCode, cancellationToken);
        if (supplierSnapshot is null)
        {
            throw new InvalidOperationException("El proveedor seleccionado no existe en la empresa activa.");
        }

        PurchaseOrderDetailDto? previous = null;
        if (command.OrderNumber.HasValue)
        {
            previous = await GetByOrderNumberAsync(command.TenantId, command.CompanyId, command.OrderNumber.Value, cancellationToken);
            if (previous is null)
            {
                throw new InvalidOperationException("No se ha encontrado el pedido de compra que intentas modificar.");
            }
        }

        var existingLineState = previous?.Lines.ToDictionary(line => line.LineNumber) ?? [];
        ValidateAgainstReceivedLines(existingLineState, command);

        var orderNumber = command.OrderNumber ?? await GetNextOrderNumberAsync(connection, command.TenantId, command.CompanyId, cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        if (command.OrderNumber.HasValue)
        {
            await using var updateCommand = connection.CreateCommand();
            updateCommand.Transaction = transaction;
            updateCommand.CommandText =
                """
                UPDATE purchase_orders
                SET supplier_code = @supplierCode,
                    supplier_name = @supplierName,
                    supplier_tax_id = @supplierTaxId,
                    document_date = @documentDate,
                    expected_date = @expectedDate,
                    status = @status,
                    notes = @notes,
                    origin = 'local',
                    is_deleted = 0,
                    synced_utc = NULL,
                    updated_utc = @updatedUtc
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND order_number = @orderNumber;
                """;
            FillHeaderParameters(updateCommand, command, orderNumber, supplierSnapshot);
            updateCommand.Parameters.AddWithValue("@updatedUtc", DateTime.UtcNow);
            var affectedRows = await updateCommand.ExecuteNonQueryAsync(cancellationToken);
            if (affectedRows == 0)
            {
                throw new InvalidOperationException("No se ha podido actualizar el pedido de compra.");
            }
        }
        else
        {
            await using var insertCommand = connection.CreateCommand();
            insertCommand.Transaction = transaction;
            insertCommand.CommandText =
                """
                INSERT INTO purchase_orders (
                    tenant_id,
                    company_id,
                    order_number,
                    supplier_code,
                    supplier_name,
                    supplier_tax_id,
                    document_date,
                    expected_date,
                    status,
                    notes,
                    origin,
                    is_deleted,
                    synced_utc,
                    created_utc,
                    updated_utc)
                VALUES (
                    @tenantId,
                    @companyId,
                    @orderNumber,
                    @supplierCode,
                    @supplierName,
                    @supplierTaxId,
                    @documentDate,
                    @expectedDate,
                    @status,
                    @notes,
                    'local',
                    0,
                    NULL,
                    @createdUtc,
                    @updatedUtc);
                """;
            FillHeaderParameters(insertCommand, command, orderNumber, supplierSnapshot);
            insertCommand.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
            insertCommand.Parameters.AddWithValue("@updatedUtc", DateTime.UtcNow);
            await insertCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        await using (var deleteLinesCommand = connection.CreateCommand())
        {
            deleteLinesCommand.Transaction = transaction;
            deleteLinesCommand.CommandText =
                """
                DELETE FROM purchase_order_lines
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
            await using var insertLineCommand = connection.CreateCommand();
            insertLineCommand.Transaction = transaction;
            insertLineCommand.CommandText =
                """
                INSERT INTO purchase_order_lines (
                    tenant_id,
                    company_id,
                    order_number,
                    line_number,
                    item_code,
                    description,
                    quantity,
                    received_quantity,
                    unit_of_measure,
                    unit_price,
                    expected_date,
                    last_received_utc,
                    notes)
                VALUES (
                    @tenantId,
                    @companyId,
                    @orderNumber,
                    @lineNumber,
                    @itemCode,
                    @description,
                    @quantity,
                    @receivedQuantity,
                    @unitOfMeasure,
                    @unitPrice,
                    @expectedDate,
                    @lastReceivedUtc,
                    @notes);
                """;
            insertLineCommand.Parameters.AddWithValue("@tenantId", command.TenantId.ToString());
            insertLineCommand.Parameters.AddWithValue("@companyId", command.CompanyId.ToString());
            insertLineCommand.Parameters.AddWithValue("@orderNumber", orderNumber);
            insertLineCommand.Parameters.AddWithValue("@lineNumber", line.LineNumber);
            insertLineCommand.Parameters.AddWithValue("@itemCode", DbValue(line.ItemCode));
            insertLineCommand.Parameters.AddWithValue("@description", line.Description);
            insertLineCommand.Parameters.AddWithValue("@quantity", line.Quantity);
            var previousLine = existingLineState.GetValueOrDefault(line.LineNumber);
            insertLineCommand.Parameters.AddWithValue("@receivedQuantity", previousLine?.ReceivedQuantity ?? 0m);
            insertLineCommand.Parameters.AddWithValue("@unitOfMeasure", DbValue(line.UnitOfMeasure));
            insertLineCommand.Parameters.AddWithValue("@unitPrice", line.UnitPrice);
            insertLineCommand.Parameters.AddWithValue("@expectedDate", DbValue(line.ExpectedDate));
            insertLineCommand.Parameters.AddWithValue("@lastReceivedUtc", DbValue(previousLine?.LastReceivedUtc));
            insertLineCommand.Parameters.AddWithValue("@notes", DbValue(line.Notes));
            await insertLineCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        await transaction.CommitAsync(cancellationToken);
        await AuditAsync(previous, command, supplierSnapshot, orderNumber, cancellationToken);
        return orderNumber;
    }

    public async Task ReceiveAsync(RegisterPurchaseOrderReceiptCommand command, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return;
        }

        await EnsureCompanyAccessAsync(command.TenantId, command.CompanyId, cancellationToken);
        EnsureTenantWriteAccess();
        NormalizeAndValidateReceipt(command);

        var currentOrder = await GetByOrderNumberAsync(command.TenantId, command.CompanyId, command.OrderNumber, cancellationToken)
            ?? throw new InvalidOperationException("No se ha encontrado el pedido de compra a recepcionar.");

        if (string.Equals(currentOrder.Status, PurchaseOrderStatuses.Cancelled, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("No puedes recepcionar un pedido cancelado.");
        }

        var lineLookup = currentOrder.Lines.ToDictionary(line => line.LineNumber);
        foreach (var receivedLine in command.Lines)
        {
            if (!lineLookup.TryGetValue(receivedLine.LineNumber, out var currentLine))
            {
                throw new InvalidOperationException($"La línea {receivedLine.LineNumber} ya no existe en el pedido.");
            }

            if (receivedLine.ReceivedQuantity > currentLine.PendingQuantity)
            {
                throw new InvalidOperationException($"La recepción de la línea {receivedLine.LineNumber} supera la cantidad pendiente.");
            }
        }

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        var receiptId = Guid.NewGuid();
        var receiptNumber = await GetNextReceiptNumberAsync(connection, command.TenantId, command.CompanyId, cancellationToken);
        var receiptSeries = BuildReceiptSeries(currentOrder.CompanyLegacyCenterCode);
        await using (var insertReceiptCommand = connection.CreateCommand())
        {
            insertReceiptCommand.Transaction = transaction;
            insertReceiptCommand.CommandText =
                """
                INSERT INTO purchase_order_receipts (
                    receipt_id,
                    receipt_series,
                    receipt_number,
                    tenant_id,
                    company_id,
                    order_number,
                    receipt_date,
                    warehouse,
                    carrier,
                    supplier_reference,
                    vehicle_plate,
                    package_count,
                    gross_weight_kg,
                    notes,
                    origin,
                    is_deleted,
                    synced_utc,
                    created_utc)
                VALUES (
                    @receiptId,
                    @receiptSeries,
                    @receiptNumber,
                    @tenantId,
                    @companyId,
                    @orderNumber,
                    @receiptDate,
                    @warehouse,
                    @carrier,
                    @supplierReference,
                    @vehiclePlate,
                    @packageCount,
                    @grossWeightKg,
                    @notes,
                    'local',
                    0,
                    NULL,
                    @createdUtc);
            """;
            insertReceiptCommand.Parameters.AddWithValue("@receiptId", receiptId.ToString());
            insertReceiptCommand.Parameters.AddWithValue("@receiptSeries", receiptSeries);
            insertReceiptCommand.Parameters.AddWithValue("@receiptNumber", receiptNumber);
            insertReceiptCommand.Parameters.AddWithValue("@tenantId", command.TenantId.ToString());
            insertReceiptCommand.Parameters.AddWithValue("@companyId", command.CompanyId.ToString());
            insertReceiptCommand.Parameters.AddWithValue("@orderNumber", command.OrderNumber);
            insertReceiptCommand.Parameters.AddWithValue("@receiptDate", command.ReceiptDate.Date);
            insertReceiptCommand.Parameters.AddWithValue("@warehouse", DbValue(command.Warehouse));
            insertReceiptCommand.Parameters.AddWithValue("@carrier", DbValue(command.Carrier));
            insertReceiptCommand.Parameters.AddWithValue("@supplierReference", DbValue(command.SupplierReference));
            insertReceiptCommand.Parameters.AddWithValue("@vehiclePlate", DbValue(command.VehiclePlate));
            insertReceiptCommand.Parameters.AddWithValue("@packageCount", DbValue(command.PackageCount));
            insertReceiptCommand.Parameters.AddWithValue("@grossWeightKg", DbValue(command.GrossWeightKg));
            insertReceiptCommand.Parameters.AddWithValue("@notes", DbValue(command.Notes));
            insertReceiptCommand.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
            await insertReceiptCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        foreach (var receivedLine in command.Lines)
        {
            var currentLine = lineLookup[receivedLine.LineNumber];

            await using (var insertReceiptLineCommand = connection.CreateCommand())
            {
                insertReceiptLineCommand.Transaction = transaction;
                insertReceiptLineCommand.CommandText =
                    """
                    INSERT INTO purchase_order_receipt_lines (
                        receipt_id,
                        tenant_id,
                        company_id,
                        order_number,
                        line_number,
                        description,
                        received_quantity)
                    VALUES (
                        @receiptId,
                        @tenantId,
                        @companyId,
                        @orderNumber,
                        @lineNumber,
                        @description,
                        @receivedQuantity);
                    """;
                insertReceiptLineCommand.Parameters.AddWithValue("@receiptId", receiptId.ToString());
                insertReceiptLineCommand.Parameters.AddWithValue("@tenantId", command.TenantId.ToString());
                insertReceiptLineCommand.Parameters.AddWithValue("@companyId", command.CompanyId.ToString());
                insertReceiptLineCommand.Parameters.AddWithValue("@orderNumber", command.OrderNumber);
                insertReceiptLineCommand.Parameters.AddWithValue("@lineNumber", receivedLine.LineNumber);
                insertReceiptLineCommand.Parameters.AddWithValue("@description", currentLine.Description);
                insertReceiptLineCommand.Parameters.AddWithValue("@receivedQuantity", receivedLine.ReceivedQuantity);
                await insertReceiptLineCommand.ExecuteNonQueryAsync(cancellationToken);
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
                        supplier_code,
                        supplier_name,
                        supplier_reference,
                        vehicle_plate,
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
                        @supplierCode,
                        @supplierName,
                        @supplierReference,
                        @vehiclePlate,
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
                insertMovementCommand.Parameters.AddWithValue("@movementType", "InboundPurchaseReceipt");
                insertMovementCommand.Parameters.AddWithValue("@movementDate", command.ReceiptDate.Date);
                insertMovementCommand.Parameters.AddWithValue("@warehouse", DbValue(command.Warehouse));
                insertMovementCommand.Parameters.AddWithValue("@itemCode", DbValue(currentLine.ItemCode));
                insertMovementCommand.Parameters.AddWithValue("@itemDescription", currentLine.Description);
                insertMovementCommand.Parameters.AddWithValue("@quantity", receivedLine.ReceivedQuantity);
                insertMovementCommand.Parameters.AddWithValue("@unitOfMeasure", DbValue(currentLine.UnitOfMeasure));
                insertMovementCommand.Parameters.AddWithValue("@supplierCode", currentOrder.SupplierCode <= 0 ? DBNull.Value : currentOrder.SupplierCode);
                insertMovementCommand.Parameters.AddWithValue("@supplierName", DbValue(currentOrder.SupplierName));
                insertMovementCommand.Parameters.AddWithValue("@supplierReference", DbValue(command.SupplierReference));
                insertMovementCommand.Parameters.AddWithValue("@vehiclePlate", DbValue(command.VehiclePlate));
                insertMovementCommand.Parameters.AddWithValue("@sourceDocumentType", "PurchaseReceipt");
                insertMovementCommand.Parameters.AddWithValue("@sourceDocumentId", receiptId.ToString());
                insertMovementCommand.Parameters.AddWithValue("@sourceDocumentNumber", receiptNumber);
                insertMovementCommand.Parameters.AddWithValue("@sourceLineNumber", receivedLine.LineNumber);
                insertMovementCommand.Parameters.AddWithValue("@notes", DbValue(command.Notes));
                insertMovementCommand.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
                await insertMovementCommand.ExecuteNonQueryAsync(cancellationToken);
            }

            await using var updateLineCommand = connection.CreateCommand();
            updateLineCommand.Transaction = transaction;
            updateLineCommand.CommandText =
                """
                UPDATE purchase_order_lines
                SET received_quantity = received_quantity + @receivedQuantity,
                    last_received_utc = @lastReceivedUtc
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND order_number = @orderNumber
                  AND line_number = @lineNumber;
                """;
            updateLineCommand.Parameters.AddWithValue("@receivedQuantity", receivedLine.ReceivedQuantity);
            updateLineCommand.Parameters.AddWithValue("@lastReceivedUtc", DateTime.UtcNow);
            updateLineCommand.Parameters.AddWithValue("@tenantId", command.TenantId.ToString());
            updateLineCommand.Parameters.AddWithValue("@companyId", command.CompanyId.ToString());
            updateLineCommand.Parameters.AddWithValue("@orderNumber", command.OrderNumber);
            updateLineCommand.Parameters.AddWithValue("@lineNumber", receivedLine.LineNumber);
            await updateLineCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        var refreshedLines = currentOrder.Lines
            .Select(line =>
            {
                var receivedNow = command.Lines.FirstOrDefault(candidate => candidate.LineNumber == line.LineNumber)?.ReceivedQuantity ?? 0m;
                return new PurchaseOrderLineDto
                {
                    LineNumber = line.LineNumber,
                    ItemCode = line.ItemCode,
                    Description = line.Description,
                    Quantity = line.Quantity,
                    UnitOfMeasure = line.UnitOfMeasure,
                    UnitPrice = line.UnitPrice,
                    ExpectedDate = line.ExpectedDate,
                    Notes = line.Notes,
                    ReceivedQuantity = line.ReceivedQuantity + receivedNow,
                    LastReceivedUtc = receivedNow > 0 ? DateTime.UtcNow : line.LastReceivedUtc
                };
            })
            .ToArray();

        var newStatus = DetermineStatusAfterReceipt(currentOrder.Status, refreshedLines);
        await using (var updateOrderCommand = connection.CreateCommand())
        {
            updateOrderCommand.Transaction = transaction;
            updateOrderCommand.CommandText =
                """
                UPDATE purchase_orders
                SET status = @status,
                    origin = 'local',
                    is_deleted = 0,
                    synced_utc = NULL,
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
            $"Linea {line.LineNumber}: +{line.ReceivedQuantity:0.###}"));
        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = command.TenantId,
            CompanyId = command.CompanyId,
            UserId = _currentUserContext.UserId,
            Action = "PurchaseOrderReceiptRegistered",
            EntityName = "PedidoCompra",
            EntityId = command.OrderNumber.ToString(),
            Details = $"Entrada={receiptSeries}/{receiptNumber:000000}; Fecha={command.ReceiptDate:yyyy-MM-dd}; Estado={newStatus}; {detailSummary}{(string.IsNullOrWhiteSpace(command.Warehouse) ? string.Empty : $"; Almacen={command.Warehouse}")}{(string.IsNullOrWhiteSpace(command.Carrier) ? string.Empty : $"; Transportista={command.Carrier}")}{(string.IsNullOrWhiteSpace(command.SupplierReference) ? string.Empty : $"; RefProveedor={command.SupplierReference}")}{(string.IsNullOrWhiteSpace(command.VehiclePlate) ? string.Empty : $"; Matricula={command.VehiclePlate}")}{(command.PackageCount.HasValue ? $"; Bultos={command.PackageCount.Value}" : string.Empty)}{(command.GrossWeightKg.HasValue ? $"; PesoKg={command.GrossWeightKg.Value:0.###}" : string.Empty)}{(string.IsNullOrWhiteSpace(command.Notes) ? string.Empty : $"; Notas={command.Notes}")}"
        }, cancellationToken);
    }

    private async Task<IReadOnlyCollection<PurchaseOrderLineDto>> LoadLinesAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        int orderNumber,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT line_number, item_code, description, quantity, received_quantity, unit_of_measure, unit_price, expected_date, last_received_utc, notes
            FROM purchase_order_lines
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND order_number = @orderNumber
            ORDER BY line_number;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@orderNumber", orderNumber);

        var items = new List<PurchaseOrderLineDto>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new PurchaseOrderLineDto
            {
                LineNumber = reader.GetInt32(reader.GetOrdinal("line_number")),
                ItemCode = reader.GetStringOrEmpty("item_code"),
                Description = reader.GetStringOrEmpty("description"),
                Quantity = reader.GetDecimal(reader.GetOrdinal("quantity")),
                ReceivedQuantity = reader.GetDecimal(reader.GetOrdinal("received_quantity")),
                UnitOfMeasure = reader.GetStringOrEmpty("unit_of_measure"),
                UnitPrice = reader.GetDecimal(reader.GetOrdinal("unit_price")),
                ExpectedDate = reader.IsDBNull(reader.GetOrdinal("expected_date")) ? null : reader.GetDateTime(reader.GetOrdinal("expected_date")),
                LastReceivedUtc = reader.IsDBNull(reader.GetOrdinal("last_received_utc")) ? null : reader.GetDateTime(reader.GetOrdinal("last_received_utc")),
                Notes = reader.GetStringOrEmpty("notes")
            });
        }

        return items;
    }

    private static void FillHeaderParameters(MySqlCommand command, SavePurchaseOrderCommand request, int orderNumber, SupplierSnapshot supplierSnapshot)
    {
        command.Parameters.AddWithValue("@tenantId", request.TenantId.ToString());
        command.Parameters.AddWithValue("@companyId", request.CompanyId.ToString());
        command.Parameters.AddWithValue("@orderNumber", orderNumber);
        command.Parameters.AddWithValue("@supplierCode", request.SupplierCode);
        command.Parameters.AddWithValue("@supplierName", supplierSnapshot.Name);
        command.Parameters.AddWithValue("@supplierTaxId", DbValue(supplierSnapshot.TaxId));
        command.Parameters.AddWithValue("@documentDate", request.DocumentDate.Date);
        command.Parameters.AddWithValue("@expectedDate", DbValue(request.ExpectedDate?.Date));
        command.Parameters.AddWithValue("@status", request.Status);
        command.Parameters.AddWithValue("@notes", DbValue(request.Notes));
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
            throw new InvalidOperationException("The selected company is not active or is not linked to a legacy center.");
        }

        return centerCode;
    }

    private async Task<SupplierSnapshot?> GetSupplierSnapshotAsync(
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
            WHERE CENTRO = @centerCode
              AND CODI = @supplierCode
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@supplierCode", supplierCode);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        return new SupplierSnapshot(
            reader.GetInt32(reader.GetOrdinal("CODI")),
            reader.GetStringOrEmpty("NOM"),
            reader.GetStringOrEmpty("NIF"));
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
            FROM purchase_orders
            WHERE tenant_id = @tenantId
              AND company_id = @companyId;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        return Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));
    }

    private static async Task<int> GetNextReceiptNumberAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT COALESCE(MAX(receipt_number), 0) + 1
            FROM purchase_order_receipts
            WHERE tenant_id = @tenantId
              AND company_id = @companyId;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        return Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));
    }

    private async Task AuditAsync(
        PurchaseOrderDetailDto? previous,
        SavePurchaseOrderCommand current,
        SupplierSnapshot supplierSnapshot,
        int orderNumber,
        CancellationToken cancellationToken)
    {
        var changes = new List<string>();
        var currentTotal = current.Lines.Sum(line => decimal.Round(line.Quantity * line.UnitPrice, 2, MidpointRounding.AwayFromZero));

        if (previous is null)
        {
            changes.Add($"Numero={orderNumber}");
            changes.Add($"Proveedor={supplierSnapshot.Name} ({supplierSnapshot.Code})");
            changes.Add($"Estado={current.Status}");
            changes.Add($"Lineas={current.Lines.Count}");
            changes.Add($"Total={currentTotal:0.00}");
        }
        else
        {
            AppendChange(changes, "Proveedor", $"{previous.SupplierName} ({previous.SupplierCode})", $"{supplierSnapshot.Name} ({supplierSnapshot.Code})");
            AppendChange(changes, "Fecha", previous.DocumentDate.ToString("yyyy-MM-dd"), current.DocumentDate.ToString("yyyy-MM-dd"));
            AppendChange(changes, "Prevista", FormatDate(previous.ExpectedDate), FormatDate(current.ExpectedDate));
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
            ? "PurchaseOrderCreated"
            : !string.Equals(previous.Status, current.Status, StringComparison.OrdinalIgnoreCase)
                ? "PurchaseOrderStatusChanged"
                : "PurchaseOrderUpdated";

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = current.TenantId,
            CompanyId = current.CompanyId,
            UserId = _currentUserContext.UserId,
            Action = action,
            EntityName = "PedidoCompra",
            EntityId = orderNumber.ToString(),
            Details = string.Join("; ", changes)
        }, cancellationToken);
    }

    private static void NormalizeAndValidate(SavePurchaseOrderCommand command)
    {
        command.Status = string.IsNullOrWhiteSpace(command.Status) ? PurchaseOrderStatuses.Draft : command.Status.Trim();
        command.Notes = command.Notes.Trim();

        if (!PurchaseOrderStatuses.All.Contains(command.Status, StringComparer.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("El estado del pedido no es válido.");
        }

        if (command.SupplierCode <= 0)
        {
            throw new InvalidOperationException("Debes seleccionar un proveedor válido.");
        }

        if (command.DocumentDate == default)
        {
            command.DocumentDate = DateTime.Today;
        }

        if (command.Lines.Count == 0)
        {
            throw new InvalidOperationException("El pedido debe tener al menos una línea.");
        }

        var normalizedLines = new List<PurchaseOrderLineInputDto>();
        var lineNumber = 1;
        foreach (var line in command.Lines)
        {
            var normalized = new PurchaseOrderLineInputDto
            {
                LineNumber = lineNumber++,
                ItemCode = line.ItemCode.Trim(),
                Description = line.Description.Trim(),
                Quantity = decimal.Round(line.Quantity, 3, MidpointRounding.AwayFromZero),
                UnitOfMeasure = line.UnitOfMeasure.Trim(),
                UnitPrice = decimal.Round(line.UnitPrice, 4, MidpointRounding.AwayFromZero),
                ExpectedDate = line.ExpectedDate?.Date,
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

    private static void NormalizeAndValidateReceipt(RegisterPurchaseOrderReceiptCommand command)
    {
        command.Warehouse = command.Warehouse.Trim();
        command.Carrier = command.Carrier.Trim();
        command.SupplierReference = command.SupplierReference.Trim();
        command.VehiclePlate = command.VehiclePlate.Trim();
        command.Notes = command.Notes.Trim();
        if (command.OrderNumber <= 0)
        {
            throw new InvalidOperationException("El pedido a recepcionar no es válido.");
        }

        if (command.ReceiptDate == default)
        {
            command.ReceiptDate = DateTime.Today;
        }

        if (command.PackageCount.HasValue && command.PackageCount.Value < 0)
        {
            throw new InvalidOperationException("Los bultos no pueden ser negativos.");
        }

        if (command.GrossWeightKg.HasValue)
        {
            if (command.GrossWeightKg.Value < 0)
            {
                throw new InvalidOperationException("El peso no puede ser negativo.");
            }

            command.GrossWeightKg = decimal.Round(command.GrossWeightKg.Value, 3, MidpointRounding.AwayFromZero);
        }

        var normalizedLines = command.Lines
            .Where(line => line.ReceivedQuantity > 0)
            .Select(line => new RegisterPurchaseOrderReceiptLineDto
            {
                LineNumber = line.LineNumber,
                ReceivedQuantity = decimal.Round(line.ReceivedQuantity, 3, MidpointRounding.AwayFromZero)
            })
            .ToList();

        if (normalizedLines.Count == 0)
        {
            throw new InvalidOperationException("Debes indicar al menos una cantidad recibida.");
        }

        if (normalizedLines.Any(line => line.LineNumber <= 0))
        {
            throw new InvalidOperationException("Hay líneas de recepción no válidas.");
        }

        if (normalizedLines.GroupBy(line => line.LineNumber).Any(group => group.Count() > 1))
        {
            throw new InvalidOperationException("No puedes repetir la misma línea en la misma recepción.");
        }

        command.Lines = normalizedLines;
    }

    private static void ValidateAgainstReceivedLines(
        IReadOnlyDictionary<int, PurchaseOrderLineDto> existingLines,
        SavePurchaseOrderCommand command)
    {
        if (existingLines.Count == 0)
        {
            return;
        }

        foreach (var existingLine in existingLines.Values.Where(line => line.ReceivedQuantity > 0))
        {
            var matchingLine = command.Lines.FirstOrDefault(line => line.LineNumber == existingLine.LineNumber);
            if (matchingLine is null)
            {
                throw new InvalidOperationException($"No puedes quitar la línea {existingLine.LineNumber} porque ya tiene recepción registrada.");
            }

            if (matchingLine.Quantity < existingLine.ReceivedQuantity)
            {
                throw new InvalidOperationException($"La línea {existingLine.LineNumber} no puede quedar por debajo de la cantidad ya recibida.");
            }
        }
    }

    private static string DetermineStatusAfterReceipt(string currentStatus, IReadOnlyCollection<PurchaseOrderLineDto> lines)
    {
        if (string.Equals(currentStatus, PurchaseOrderStatuses.Cancelled, StringComparison.OrdinalIgnoreCase))
        {
            return PurchaseOrderStatuses.Cancelled;
        }

        var totalQuantity = lines.Sum(line => line.Quantity);
        var receivedQuantity = lines.Sum(line => line.ReceivedQuantity);

        if (receivedQuantity <= 0 || totalQuantity <= 0)
        {
            return string.Equals(currentStatus, PurchaseOrderStatuses.Sent, StringComparison.OrdinalIgnoreCase)
                ? PurchaseOrderStatuses.Sent
                : PurchaseOrderStatuses.Draft;
        }

        return lines.All(line => line.IsFullyReceived)
            ? PurchaseOrderStatuses.Received
            : PurchaseOrderStatuses.PartiallyReceived;
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
    private static object DbValue(int? value) => value.HasValue ? value.Value : DBNull.Value;
    private static object DbValue(decimal? value) => value.HasValue ? value.Value : DBNull.Value;
    private static string BuildReceiptSeries(string companyLegacyCenterCode) =>
        $"AC-{(string.IsNullOrWhiteSpace(companyLegacyCenterCode) ? "GEN" : companyLegacyCenterCode.Trim().ToUpperInvariant())}";

    private static string BuildPurchaseOrderSearchOrderByClause(PurchaseOrderFilter filter)
    {
        var column = filter.SortColumn switch
        {
            nameof(PurchaseOrderListItemDto.OrderNumber) => "po.order_number",
            nameof(PurchaseOrderListItemDto.SupplierName) => "supplier_name",
            nameof(PurchaseOrderListItemDto.DocumentDate) => "po.document_date",
            nameof(PurchaseOrderListItemDto.ExpectedDate) => "po.expected_date",
            nameof(PurchaseOrderListItemDto.Status) => "po.status",
            nameof(PurchaseOrderListItemDto.LineCount) => "line_count",
            nameof(PurchaseOrderListItemDto.TotalAmount) => "total_amount",
            _ => string.Empty
        };

        if (string.IsNullOrWhiteSpace(column))
        {
            return "ORDER BY po.order_number DESC";
        }

        var direction = filter.SortDescending ? "DESC" : "ASC";
        return $"ORDER BY {column} {direction}, po.order_number DESC";
    }

    private static string BuildPurchaseReceiptSearchOrderByClause(PurchaseReceiptFilter filter)
    {
        var column = filter.SortColumn switch
        {
            nameof(PurchaseReceiptListItemDto.ReceiptNumber) => "pr.receipt_number",
            nameof(PurchaseReceiptListItemDto.OrderNumber) => "pr.order_number",
            nameof(PurchaseReceiptListItemDto.SupplierName) => "po.supplier_name",
            nameof(PurchaseReceiptListItemDto.ReceiptDate) => "pr.receipt_date",
            nameof(PurchaseReceiptListItemDto.LineCount) => "line_count",
            nameof(PurchaseReceiptListItemDto.TotalReceivedQuantity) => "total_received_quantity",
            _ => string.Empty
        };

        if (string.IsNullOrWhiteSpace(column))
        {
            return "ORDER BY pr.receipt_date DESC, pr.receipt_number DESC";
        }

        var direction = filter.SortDescending ? "DESC" : "ASC";
        return $"ORDER BY {column} {direction}, pr.receipt_number DESC";
    }

    private sealed record SupplierSnapshot(int Code, string Name, string TaxId);
}
