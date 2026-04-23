using Erp.Application.Auditing;
using Erp.Application.Companies;
using Erp.Application.Contexts;
using Erp.Application.LegacySync;
using Erp.Application.Numbering;
using Erp.Application.Stock;
using Erp.Domain.Common;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Numbering;
using Erp.Infrastructure.MySql.Support;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Stock;

public sealed class MySqlStockService : IStockQueries, IStockService
{
    private readonly MySqlConnectionFactory _connectionFactory;
    private readonly IAuditLogService _auditLogService;
    private readonly ICompanyAccessService _companyAccessService;
    private readonly ICurrentUserContext _currentUserContext;
    private readonly ITenantContext _tenantContext;
    private readonly IActiveCompanyContext _activeCompanyContext;

    public MySqlStockService(
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

    public async Task<StockMovementSearchResultDto> SearchMovementsAsync(
        Guid tenantId,
        Guid companyId,
        StockMovementFilter filter,
        CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return new StockMovementSearchResultDto();
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);

        var search = filter.Search?.Trim() ?? string.Empty;
        var warehouse = filter.Warehouse?.Trim() ?? string.Empty;
        var catalogScope = NormalizeCatalogScope(filter.CatalogScope);
        var supplierName = filter.SupplierName?.Trim() ?? string.Empty;
        var color = filter.Color?.Trim() ?? string.Empty;
        var movementType = filter.MovementType?.Trim() ?? string.Empty;
        var likeSearch = $"%{search}%";
        var likeSupplierName = $"%{supplierName}%";
        var likeColor = $"%{color}%";
        var pageSize = Math.Clamp(filter.PageSize, 10, 200);
        var page = Math.Max(filter.Page, 1);
        var offset = (page - 1) * pageSize;
        var centerCode = string.IsNullOrWhiteSpace(catalogScope)
            ? string.Empty
            : await ResolveCompanyCenterCodeAsync(tenantId, companyId, cancellationToken);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using (var countCommand = connection.CreateCommand())
        {
            countCommand.CommandText =
                """
                SELECT COUNT(*)
                FROM inventory_movements
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND (
                        @catalogScope = ''
                        OR (
                            @catalogScope = 'Hilos'
                            AND EXISTS (
                                SELECT 1
                                FROM fil f
                                WHERE f.CENTRO = @centerCode
                                  AND COALESCE(f.is_deleted, 0) = 0
                                  AND f.CODI = COALESCE(item_code, '')
                            )
                        )
                        OR (
                            @catalogScope = 'Tejidos'
                            AND EXISTS (
                                SELECT 1
                                FROM teixits t
                                WHERE t.CENTRO = @centerCode
                                  AND COALESCE(t.is_deleted, 0) = 0
                                  AND t.CODI = COALESCE(item_code, '')
                            )
                        )
                        OR (
                            @catalogScope = 'Models'
                            AND EXISTS (
                                SELECT 1
                                FROM article_models am
                                WHERE am.CENTRO = @centerCode
                                  AND COALESCE(am.is_deleted, 0) = 0
                                  AND am.CODI = COALESCE(item_code, '')
                            )
                        )
                      )
                  AND (
                        @warehouse = ''
                        OR COALESCE(warehouse, '') = @warehouse
                      )
                  AND (
                        @supplierName = ''
                        OR COALESCE(supplier_name, '') LIKE @likeSupplierName
                      )
                  AND (
                        @color = ''
                        OR COALESCE(color, '') LIKE @likeColor
                      )
                  AND (
                        @movementType = ''
                        OR COALESCE(movement_type, '') = @movementType
                      )
                  AND (
                        @search = ''
                        OR COALESCE(item_code, '') LIKE @likeSearch
                        OR item_description LIKE @likeSearch
                        OR COALESCE(color, '') LIKE @likeSearch
                        OR COALESCE(supplier_name, '') LIKE @likeSearch
                        OR COALESCE(supplier_reference, '') LIKE @likeSearch
                        OR COALESCE(vehicle_plate, '') LIKE @likeSearch
                        OR CAST(source_document_number AS CHAR) LIKE @likeSearch
                      );
                """;
            countCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            countCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
            countCommand.Parameters.AddWithValue("@catalogScope", catalogScope);
            countCommand.Parameters.AddWithValue("@centerCode", centerCode);
            countCommand.Parameters.AddWithValue("@warehouse", warehouse);
            countCommand.Parameters.AddWithValue("@supplierName", supplierName);
            countCommand.Parameters.AddWithValue("@likeSupplierName", likeSupplierName);
            countCommand.Parameters.AddWithValue("@color", color);
            countCommand.Parameters.AddWithValue("@likeColor", likeColor);
            countCommand.Parameters.AddWithValue("@movementType", movementType);
            countCommand.Parameters.AddWithValue("@search", search);
            countCommand.Parameters.AddWithValue("@likeSearch", likeSearch);

            var totalCount = Convert.ToInt32(await countCommand.ExecuteScalarAsync(cancellationToken));
            if (totalCount == 0)
            {
                return new StockMovementSearchResultDto();
            }

            await using var command = connection.CreateCommand();
            var orderBy = BuildStockMovementSearchOrderByClause(filter);
            command.CommandText =
                $"""
                SELECT movement_id,
                       movement_date,
                       movement_type,
                       warehouse,
                       item_code,
                       item_description,
                       color,
                       quantity,
                       unit_of_measure,
                       supplier_name,
                       supplier_reference,
                       vehicle_plate,
                       source_document_type,
                       source_document_number,
                       notes
                FROM inventory_movements
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND (
                        @catalogScope = ''
                        OR (
                            @catalogScope = 'Hilos'
                            AND EXISTS (
                                SELECT 1
                                FROM fil f
                                WHERE f.CENTRO = @centerCode
                                  AND COALESCE(f.is_deleted, 0) = 0
                                  AND f.CODI = COALESCE(item_code, '')
                            )
                        )
                        OR (
                            @catalogScope = 'Tejidos'
                            AND EXISTS (
                                SELECT 1
                                FROM teixits t
                                WHERE t.CENTRO = @centerCode
                                  AND COALESCE(t.is_deleted, 0) = 0
                                  AND t.CODI = COALESCE(item_code, '')
                            )
                        )
                        OR (
                            @catalogScope = 'Models'
                            AND EXISTS (
                                SELECT 1
                                FROM article_models am
                                WHERE am.CENTRO = @centerCode
                                  AND COALESCE(am.is_deleted, 0) = 0
                                  AND am.CODI = COALESCE(item_code, '')
                            )
                        )
                      )
                  AND (
                        @warehouse = ''
                        OR COALESCE(warehouse, '') = @warehouse
                      )
                  AND (
                        @supplierName = ''
                        OR COALESCE(supplier_name, '') LIKE @likeSupplierName
                      )
                  AND (
                        @color = ''
                        OR COALESCE(color, '') LIKE @likeColor
                      )
                  AND (
                        @movementType = ''
                        OR COALESCE(movement_type, '') = @movementType
                      )
                  AND (
                        @search = ''
                        OR COALESCE(item_code, '') LIKE @likeSearch
                        OR item_description LIKE @likeSearch
                        OR COALESCE(color, '') LIKE @likeSearch
                        OR COALESCE(supplier_name, '') LIKE @likeSearch
                        OR COALESCE(supplier_reference, '') LIKE @likeSearch
                        OR COALESCE(vehicle_plate, '') LIKE @likeSearch
                        OR CAST(source_document_number AS CHAR) LIKE @likeSearch
                      )
                {orderBy}
                LIMIT @pageSize OFFSET @offset;
                """;
            command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            command.Parameters.AddWithValue("@companyId", companyId.ToString());
            command.Parameters.AddWithValue("@catalogScope", catalogScope);
            command.Parameters.AddWithValue("@centerCode", centerCode);
            command.Parameters.AddWithValue("@warehouse", warehouse);
            command.Parameters.AddWithValue("@supplierName", supplierName);
            command.Parameters.AddWithValue("@likeSupplierName", likeSupplierName);
            command.Parameters.AddWithValue("@color", color);
            command.Parameters.AddWithValue("@likeColor", likeColor);
            command.Parameters.AddWithValue("@movementType", movementType);
            command.Parameters.AddWithValue("@search", search);
            command.Parameters.AddWithValue("@likeSearch", likeSearch);
            command.Parameters.AddWithValue("@pageSize", pageSize);
            command.Parameters.AddWithValue("@offset", offset);

            var items = await ReadMovementListAsync(command, cancellationToken);
            var supplierSummaries = await LoadMovementGroupSummaryAsync(
                connection,
                tenantId,
                companyId,
                catalogScope,
                centerCode,
                warehouse,
                supplierName,
                color,
                movementType,
                search,
                likeSearch,
                likeSupplierName,
                likeColor,
                "supplier_name",
                cancellationToken);
            var colorSummaries = await LoadMovementGroupSummaryAsync(
                connection,
                tenantId,
                companyId,
                catalogScope,
                centerCode,
                warehouse,
                supplierName,
                color,
                movementType,
                search,
                likeSearch,
                likeSupplierName,
                likeColor,
                "color",
                cancellationToken);

            return new StockMovementSearchResultDto
            {
                Items = items,
                SupplierSummaries = supplierSummaries,
                ColorSummaries = colorSummaries,
                TotalCount = totalCount
            };
        }
    }

    public async Task<IReadOnlyCollection<StockMovementListItemDto>> GetByPurchaseReceiptAsync(
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
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT movement_id,
                   movement_date,
                   movement_type,
                   warehouse,
                   item_code,
                   item_description,
                   color,
                   quantity,
                   unit_of_measure,
                   supplier_name,
                   supplier_reference,
                   vehicle_plate,
                   source_document_type,
                   source_document_number,
                   notes
            FROM inventory_movements
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND source_document_type = 'PurchaseReceipt'
              AND source_document_number = @receiptNumber
            ORDER BY movement_date DESC, created_utc DESC, source_line_number;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@receiptNumber", receiptNumber);

        return await ReadMovementListAsync(command, cancellationToken);
    }

    public async Task<StockBalanceSearchResultDto> SearchBalancesAsync(
        Guid tenantId,
        Guid companyId,
        StockBalanceFilter filter,
        CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return new StockBalanceSearchResultDto();
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);

        var search = filter.Search?.Trim() ?? string.Empty;
        var warehouse = filter.Warehouse?.Trim() ?? string.Empty;
        var catalogScope = NormalizeCatalogScope(filter.CatalogScope);
        var likeSearch = $"%{search}%";
        var pageSize = Math.Clamp(filter.PageSize, 10, 200);
        var page = Math.Max(filter.Page, 1);
        var offset = (page - 1) * pageSize;

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var centerCode = string.IsNullOrWhiteSpace(catalogScope)
            ? string.Empty
            : await ResolveCompanyCenterCodeAsync(tenantId, companyId, cancellationToken);
        var legacySyncInfo = await LoadLegacySyncInfoAsync(connection, tenantId, companyId, cancellationToken);
        if (legacySyncInfo.IsActive)
        {
            return await SearchLegacyBalancesAsync(connection, tenantId, companyId, filter, search, warehouse, likeSearch, pageSize, offset, centerCode, cancellationToken);
        }

        await using (var countCommand = connection.CreateCommand())
        {
            countCommand.CommandText =
                """
                SELECT COUNT(*)
                FROM (
                    SELECT warehouse, item_code, item_description, unit_of_measure
                    FROM inventory_movements
                    WHERE tenant_id = @tenantId
                      AND company_id = @companyId
                      AND (
                            @catalogScope = ''
                            OR (
                                @catalogScope = 'Hilos'
                                AND EXISTS (
                                    SELECT 1
                                    FROM fil f
                                    WHERE f.CENTRO = @centerCode
                                      AND COALESCE(f.is_deleted, 0) = 0
                                      AND f.CODI = COALESCE(item_code, '')
                                )
                            )
                            OR (
                                @catalogScope = 'Tejidos'
                                AND EXISTS (
                                    SELECT 1
                                    FROM teixits t
                                    WHERE t.CENTRO = @centerCode
                                      AND COALESCE(t.is_deleted, 0) = 0
                                      AND t.CODI = COALESCE(item_code, '')
                                )
                            )
                            OR (
                                @catalogScope = 'Models'
                                AND EXISTS (
                                    SELECT 1
                                    FROM article_models am
                                    WHERE am.CENTRO = @centerCode
                                      AND COALESCE(am.is_deleted, 0) = 0
                                      AND am.CODI = COALESCE(item_code, '')
                                )
                            )
                          )
                      AND (
                            @warehouse = ''
                            OR COALESCE(warehouse, '') = @warehouse
                          )
                      AND (
                            @search = ''
                            OR COALESCE(item_code, '') LIKE @likeSearch
                            OR item_description LIKE @likeSearch
                            OR COALESCE(warehouse, '') LIKE @likeSearch
                          )
                    GROUP BY warehouse, item_code, item_description, unit_of_measure
                    HAVING COALESCE(SUM(
                        CASE
                            WHEN movement_type LIKE 'Inbound%' THEN quantity
                            ELSE -quantity
                        END
                    ), 0) <> 0
                ) balances;
                """;
            countCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            countCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
            countCommand.Parameters.AddWithValue("@catalogScope", catalogScope);
            countCommand.Parameters.AddWithValue("@centerCode", centerCode);
            countCommand.Parameters.AddWithValue("@warehouse", warehouse);
            countCommand.Parameters.AddWithValue("@search", search);
            countCommand.Parameters.AddWithValue("@likeSearch", likeSearch);

            var totalCount = Convert.ToInt32(await countCommand.ExecuteScalarAsync(cancellationToken));
            if (totalCount == 0)
            {
                return new StockBalanceSearchResultDto();
            }

            await using var command = connection.CreateCommand();
            var orderBy = BuildStockBalanceSearchOrderByClause(filter);
            command.CommandText =
                $"""
                SELECT warehouse,
                       item_code,
                       item_description,
                       unit_of_measure,
                       COUNT(*) AS movement_count,
                       MAX(movement_date) AS last_movement_date,
                       COALESCE(SUM(
                           CASE
                               WHEN movement_type LIKE 'Inbound%' THEN quantity
                               ELSE -quantity
                           END
                       ), 0) AS current_stock
                FROM inventory_movements
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND (
                        @catalogScope = ''
                        OR (
                            @catalogScope = 'Hilos'
                            AND EXISTS (
                                SELECT 1
                                FROM fil f
                                WHERE f.CENTRO = @centerCode
                                  AND COALESCE(f.is_deleted, 0) = 0
                                  AND f.CODI = COALESCE(item_code, '')
                            )
                        )
                        OR (
                            @catalogScope = 'Tejidos'
                            AND EXISTS (
                                SELECT 1
                                FROM teixits t
                                WHERE t.CENTRO = @centerCode
                                  AND COALESCE(t.is_deleted, 0) = 0
                                  AND t.CODI = COALESCE(item_code, '')
                            )
                        )
                        OR (
                            @catalogScope = 'Models'
                            AND EXISTS (
                                SELECT 1
                                FROM article_models am
                                WHERE am.CENTRO = @centerCode
                                  AND COALESCE(am.is_deleted, 0) = 0
                                  AND am.CODI = COALESCE(item_code, '')
                            )
                        )
                      )
                  AND (
                        @warehouse = ''
                        OR COALESCE(warehouse, '') = @warehouse
                      )
                  AND (
                        @search = ''
                        OR COALESCE(item_code, '') LIKE @likeSearch
                        OR item_description LIKE @likeSearch
                        OR COALESCE(warehouse, '') LIKE @likeSearch
                      )
                GROUP BY warehouse, item_code, item_description, unit_of_measure
                HAVING current_stock <> 0
                {orderBy}
                LIMIT @pageSize OFFSET @offset;
                """;
            command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            command.Parameters.AddWithValue("@companyId", companyId.ToString());
            command.Parameters.AddWithValue("@catalogScope", catalogScope);
            command.Parameters.AddWithValue("@centerCode", centerCode);
            command.Parameters.AddWithValue("@warehouse", warehouse);
            command.Parameters.AddWithValue("@search", search);
            command.Parameters.AddWithValue("@likeSearch", likeSearch);
            command.Parameters.AddWithValue("@pageSize", pageSize);
            command.Parameters.AddWithValue("@offset", offset);

            var items = new List<StockBalanceListItemDto>();
            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                items.Add(new StockBalanceListItemDto
                {
                    Warehouse = reader.GetStringOrEmpty("warehouse"),
                    ItemCode = reader.GetStringOrEmpty("item_code"),
                    ItemDescription = reader.GetStringOrEmpty("item_description"),
                    UnitOfMeasure = reader.GetStringOrEmpty("unit_of_measure"),
                    MovementCount = reader.GetInt32(reader.GetOrdinal("movement_count")),
                    LastMovementDate = reader.IsDBNull(reader.GetOrdinal("last_movement_date"))
                        ? null
                        : reader.GetDateTime(reader.GetOrdinal("last_movement_date")),
                    CurrentStock = reader.GetDecimal(reader.GetOrdinal("current_stock"))
                });
            }

            return new StockBalanceSearchResultDto
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }

    public async Task<IReadOnlyCollection<StockCountLineDto>> GetCountSeedLinesAsync(
        Guid tenantId,
        Guid companyId,
        string warehouse,
        CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return [];
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);
        var normalizedWarehouse = warehouse?.Trim() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(normalizedWarehouse))
        {
            return [];
        }

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var balances = await LoadWarehouseBalanceSnapshotAsync(connection, tenantId, companyId, normalizedWarehouse, cancellationToken);
        return balances
            .Where(item => item.CurrentStock != 0m)
            .OrderBy(item => item.ItemCode, StringComparer.OrdinalIgnoreCase)
            .ThenBy(item => item.ItemDescription, StringComparer.OrdinalIgnoreCase)
            .Select((item, index) => new StockCountLineDto
            {
                LineNumber = index + 1,
                ItemCode = item.ItemCode,
                ItemDescription = item.ItemDescription,
                ExpectedQuantity = item.CurrentStock,
                CountedQuantity = item.CurrentStock,
                DifferenceQuantity = 0m,
                UnitOfMeasure = item.UnitOfMeasure
            })
            .ToArray();
    }

    public async Task<StockCountSearchResultDto> SearchCountsAsync(
        Guid tenantId,
        Guid companyId,
        StockCountFilter filter,
        CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return new StockCountSearchResultDto();
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);

        var search = filter.Search?.Trim() ?? string.Empty;
        var warehouse = filter.Warehouse?.Trim() ?? string.Empty;
        var status = filter.Status?.Trim() ?? string.Empty;
        var likeSearch = $"%{search}%";
        var pageSize = Math.Clamp(filter.PageSize, 10, 200);
        var page = Math.Max(filter.Page, 1);
        var offset = (page - 1) * pageSize;

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using (var countCommand = connection.CreateCommand())
        {
            countCommand.CommandText =
                """
                SELECT COUNT(*)
                FROM stock_counts c
                WHERE c.tenant_id = @tenantId
                  AND c.company_id = @companyId
                  AND COALESCE(c.is_deleted, 0) = 0
                  AND (
                        @warehouse = ''
                        OR COALESCE(c.warehouse, '') = @warehouse
                      )
                  AND (
                        @status = ''
                        OR c.status = @status
                      )
                  AND (
                        @includeClosed = 1
                        OR c.status NOT IN ('Completed', 'Cancelled')
                      )
                  AND (
                        @search = ''
                        OR CAST(c.count_number AS CHAR) LIKE @likeSearch
                        OR COALESCE(c.warehouse, '') LIKE @likeSearch
                        OR COALESCE(c.notes, '') LIKE @likeSearch
                        OR EXISTS (
                            SELECT 1
                            FROM stock_count_lines line
                            WHERE line.count_id = c.count_id
                              AND (
                                    COALESCE(line.item_code, '') LIKE @likeSearch
                                    OR COALESCE(line.item_description, '') LIKE @likeSearch
                                    OR COALESCE(line.color, '') LIKE @likeSearch
                                  )
                        )
                      );
                """;
            countCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            countCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
            countCommand.Parameters.AddWithValue("@warehouse", warehouse);
            countCommand.Parameters.AddWithValue("@status", status);
            countCommand.Parameters.AddWithValue("@includeClosed", filter.IncludeClosed);
            countCommand.Parameters.AddWithValue("@search", search);
            countCommand.Parameters.AddWithValue("@likeSearch", likeSearch);

            var totalCount = Convert.ToInt32(await countCommand.ExecuteScalarAsync(cancellationToken));
            if (totalCount == 0)
            {
                return new StockCountSearchResultDto();
            }

            await using var command = connection.CreateCommand();
            var orderBy = BuildStockCountSearchOrderByClause(filter);
            command.CommandText =
                $"""
                SELECT c.count_id,
                       c.count_number,
                       c.count_date,
                       c.status,
                       c.warehouse,
                       c.is_blind_count,
                       c.is_blind_count_revealed,
                       c.line_count,
                       c.difference_line_count,
                       c.expected_total_quantity,
                       c.counted_total_quantity,
                       c.difference_total_quantity,
                       c.notes,
                       c.origin
                FROM stock_counts c
                WHERE c.tenant_id = @tenantId
                  AND c.company_id = @companyId
                  AND COALESCE(c.is_deleted, 0) = 0
                  AND (
                        @warehouse = ''
                        OR COALESCE(c.warehouse, '') = @warehouse
                      )
                  AND (
                        @status = ''
                        OR c.status = @status
                      )
                  AND (
                        @includeClosed = 1
                        OR c.status NOT IN ('Completed', 'Cancelled')
                      )
                  AND (
                        @search = ''
                        OR CAST(c.count_number AS CHAR) LIKE @likeSearch
                        OR COALESCE(c.warehouse, '') LIKE @likeSearch
                        OR COALESCE(c.notes, '') LIKE @likeSearch
                        OR EXISTS (
                            SELECT 1
                            FROM stock_count_lines line
                            WHERE line.count_id = c.count_id
                              AND (
                                    COALESCE(line.item_code, '') LIKE @likeSearch
                                    OR COALESCE(line.item_description, '') LIKE @likeSearch
                                    OR COALESCE(line.color, '') LIKE @likeSearch
                                  )
                        )
                      )
                {orderBy}
                LIMIT @pageSize OFFSET @offset;
                """;
            command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            command.Parameters.AddWithValue("@companyId", companyId.ToString());
            command.Parameters.AddWithValue("@warehouse", warehouse);
            command.Parameters.AddWithValue("@status", status);
            command.Parameters.AddWithValue("@includeClosed", filter.IncludeClosed);
            command.Parameters.AddWithValue("@search", search);
            command.Parameters.AddWithValue("@likeSearch", likeSearch);
            command.Parameters.AddWithValue("@pageSize", pageSize);
            command.Parameters.AddWithValue("@offset", offset);

            var items = new List<StockCountListItemDto>();
            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                items.Add(new StockCountListItemDto
                {
                    CountNumber = reader.GetInt32(reader.GetOrdinal("count_number")),
                    CountDate = reader.GetDateTime(reader.GetOrdinal("count_date")),
                    Status = reader.GetStringOrEmpty("status"),
                    Warehouse = reader.GetStringOrEmpty("warehouse"),
                    IsBlindCount = reader.GetBoolean(reader.GetOrdinal("is_blind_count")),
                    IsBlindCountRevealed = reader.GetBoolean(reader.GetOrdinal("is_blind_count_revealed")),
                    LineCount = reader.GetInt32OrDefault("line_count"),
                    DifferenceLineCount = reader.GetInt32OrDefault("difference_line_count"),
                    ExpectedTotalQuantity = reader.GetDecimal(reader.GetOrdinal("expected_total_quantity")),
                    CountedTotalQuantity = reader.GetDecimal(reader.GetOrdinal("counted_total_quantity")),
                    DifferenceTotalQuantity = reader.GetDecimal(reader.GetOrdinal("difference_total_quantity")),
                    Notes = reader.GetStringOrEmpty("notes"),
                    Origin = reader.GetStringOrEmpty("origin")
                });
            }

            return new StockCountSearchResultDto
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }

    public async Task<StockCountDetailDto?> GetCountByNumberAsync(
        Guid tenantId,
        Guid companyId,
        int countNumber,
        CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return null;
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var count = await LoadCountByNumberAsync(connection, null, tenantId, companyId, countNumber, cancellationToken);
        if (count is null)
        {
            return null;
        }

        count.Lines = (await LoadCountLinesAsync(connection, null, count.CountId, cancellationToken)).ToList();
        return count;
    }

    public async Task<StockTransferSearchResultDto> SearchTransfersAsync(
        Guid tenantId,
        Guid companyId,
        StockTransferFilter filter,
        CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return new StockTransferSearchResultDto();
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);

        var search = filter.Search?.Trim() ?? string.Empty;
        var status = filter.Status?.Trim() ?? string.Empty;
        var likeSearch = $"%{search}%";
        var pageSize = Math.Clamp(filter.PageSize, 10, 200);
        var page = Math.Max(filter.Page, 1);
        var offset = (page - 1) * pageSize;

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using (var countCommand = connection.CreateCommand())
        {
            countCommand.CommandText =
                """
                SELECT COUNT(*)
                FROM stock_transfers t
                WHERE t.tenant_id = @tenantId
                  AND t.company_id = @companyId
                  AND COALESCE(t.is_deleted, 0) = 0
                  AND (
                        @status = ''
                        OR t.status = @status
                      )
                  AND (
                        @includeClosed = 1
                        OR t.status NOT IN ('Completed', 'Cancelled')
                      )
                  AND (
                        @search = ''
                        OR CAST(t.transfer_number AS CHAR) LIKE @likeSearch
                        OR COALESCE(t.from_warehouse, '') LIKE @likeSearch
                        OR COALESCE(t.to_warehouse, '') LIKE @likeSearch
                        OR COALESCE(t.notes, '') LIKE @likeSearch
                        OR EXISTS (
                            SELECT 1
                            FROM stock_transfer_lines line
                            WHERE line.transfer_id = t.transfer_id
                              AND (
                                    COALESCE(line.item_code, '') LIKE @likeSearch
                                    OR COALESCE(line.item_description, '') LIKE @likeSearch
                                    OR COALESCE(line.color, '') LIKE @likeSearch
                                  )
                        )
                      );
                """;
            countCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            countCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
            countCommand.Parameters.AddWithValue("@status", status);
            countCommand.Parameters.AddWithValue("@includeClosed", filter.IncludeClosed);
            countCommand.Parameters.AddWithValue("@search", search);
            countCommand.Parameters.AddWithValue("@likeSearch", likeSearch);

            var totalCount = Convert.ToInt32(await countCommand.ExecuteScalarAsync(cancellationToken));
            if (totalCount == 0)
            {
                return new StockTransferSearchResultDto();
            }

            await using var command = connection.CreateCommand();
            var orderBy = BuildStockTransferSearchOrderByClause(filter);
            command.CommandText =
                $"""
                SELECT t.transfer_id,
                       t.transfer_number,
                       t.transfer_date,
                       t.status,
                       t.from_warehouse,
                       t.to_warehouse,
                       t.line_count,
                       t.total_quantity,
                       t.notes,
                       t.origin
                FROM stock_transfers t
                WHERE t.tenant_id = @tenantId
                  AND t.company_id = @companyId
                  AND COALESCE(t.is_deleted, 0) = 0
                  AND (
                        @status = ''
                        OR t.status = @status
                      )
                  AND (
                        @includeClosed = 1
                        OR t.status NOT IN ('Completed', 'Cancelled')
                      )
                  AND (
                        @search = ''
                        OR CAST(t.transfer_number AS CHAR) LIKE @likeSearch
                        OR COALESCE(t.from_warehouse, '') LIKE @likeSearch
                        OR COALESCE(t.to_warehouse, '') LIKE @likeSearch
                        OR COALESCE(t.notes, '') LIKE @likeSearch
                        OR EXISTS (
                            SELECT 1
                            FROM stock_transfer_lines line
                            WHERE line.transfer_id = t.transfer_id
                              AND (
                                    COALESCE(line.item_code, '') LIKE @likeSearch
                                    OR COALESCE(line.item_description, '') LIKE @likeSearch
                                    OR COALESCE(line.color, '') LIKE @likeSearch
                                  )
                        )
                      )
                {orderBy}
                LIMIT @pageSize OFFSET @offset;
                """;
            command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            command.Parameters.AddWithValue("@companyId", companyId.ToString());
            command.Parameters.AddWithValue("@status", status);
            command.Parameters.AddWithValue("@includeClosed", filter.IncludeClosed);
            command.Parameters.AddWithValue("@search", search);
            command.Parameters.AddWithValue("@likeSearch", likeSearch);
            command.Parameters.AddWithValue("@pageSize", pageSize);
            command.Parameters.AddWithValue("@offset", offset);

            var items = new List<StockTransferListItemDto>();
            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                items.Add(new StockTransferListItemDto
                {
                    TransferId = reader.GetGuid("transfer_id"),
                    TransferNumber = reader.GetInt32(reader.GetOrdinal("transfer_number")),
                    TransferDate = reader.GetDateTime(reader.GetOrdinal("transfer_date")),
                    Status = reader.GetStringOrEmpty("status"),
                    FromWarehouse = reader.GetStringOrEmpty("from_warehouse"),
                    ToWarehouse = reader.GetStringOrEmpty("to_warehouse"),
                    LineCount = reader.GetInt32OrDefault("line_count"),
                    TotalQuantity = reader.GetDecimal(reader.GetOrdinal("total_quantity")),
                    Notes = reader.GetStringOrEmpty("notes"),
                    Origin = reader.GetStringOrEmpty("origin")
                });
            }

            return new StockTransferSearchResultDto
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }

    public async Task<StockTransferDetailDto?> GetTransferByNumberAsync(
        Guid tenantId,
        Guid companyId,
        int transferNumber,
        CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return null;
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var transfer = await LoadTransferByNumberAsync(connection, null, tenantId, companyId, transferNumber, cancellationToken);
        if (transfer is null)
        {
            return null;
        }

        transfer.Lines = (await LoadTransferLinesAsync(connection, null, transfer.TransferId, cancellationToken)).ToList();
        return transfer;
    }

    public async Task<StockLegacySyncInfoDto> GetLegacySyncInfoAsync(
        Guid tenantId,
        Guid companyId,
        CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return new StockLegacySyncInfoDto();
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        return await LoadLegacySyncInfoAsync(connection, tenantId, companyId, cancellationToken);
    }

    public async Task<Guid> CreateAdjustmentAsync(
        CreateStockAdjustmentCommand command,
        CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return Guid.Empty;
        }

        await EnsureCompanyAccessAsync(command.TenantId, command.CompanyId, cancellationToken);
        EnsureWriteAccess();
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        NormalizeAndValidate(command);

        if (string.Equals(command.AdjustmentType, StockMovementTypes.OutboundManualAdjustment, StringComparison.OrdinalIgnoreCase))
        {
            var currentStock = await GetCurrentStockAsync(
                connection,
                command.TenantId,
                command.CompanyId,
                command.Warehouse,
                command.ItemCode,
                command.ItemDescription,
                command.Color,
                cancellationToken);

            if (command.CurrentStockHint.HasValue && command.CurrentStockHint.Value > currentStock)
            {
                currentStock = command.CurrentStockHint.Value;
            }

            if (currentStock < command.Quantity)
            {
                throw new InvalidOperationException($"No hay stock suficiente para sacar {command.Quantity:0.###}. Stock disponible: {currentStock:0.###}.");
            }
        }

        var movementId = Guid.NewGuid();
        await using var commandDb = connection.CreateCommand();
        commandDb.CommandText =
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
                @sourceLineNumber,
                @notes,
                @createdUtc);
            """;
        commandDb.Parameters.AddWithValue("@movementId", movementId.ToString());
        commandDb.Parameters.AddWithValue("@tenantId", command.TenantId.ToString());
        commandDb.Parameters.AddWithValue("@companyId", command.CompanyId.ToString());
        commandDb.Parameters.AddWithValue("@movementType", command.AdjustmentType);
        commandDb.Parameters.AddWithValue("@movementDate", command.MovementDate.Date);
        commandDb.Parameters.AddWithValue("@warehouse", DbValue(command.Warehouse));
        commandDb.Parameters.AddWithValue("@itemCode", DbValue(command.ItemCode));
        commandDb.Parameters.AddWithValue("@itemDescription", command.ItemDescription);
        commandDb.Parameters.AddWithValue("@color", DbValue(command.Color));
        commandDb.Parameters.AddWithValue("@quantity", command.Quantity);
        commandDb.Parameters.AddWithValue("@unitOfMeasure", DbValue(command.UnitOfMeasure));
        commandDb.Parameters.AddWithValue("@sourceDocumentType", "ManualStockAdjustment");
        commandDb.Parameters.AddWithValue("@sourceDocumentId", movementId.ToString());
        commandDb.Parameters.AddWithValue("@sourceLineNumber", 1);
        commandDb.Parameters.AddWithValue("@notes", DbValue(command.Notes));
        commandDb.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
        await commandDb.ExecuteNonQueryAsync(cancellationToken);

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = command.TenantId,
            CompanyId = command.CompanyId,
            UserId = _currentUserContext.UserId,
            Action = "StockManualAdjustmentCreated",
            EntityName = "Stock",
            EntityId = movementId.ToString(),
            Details = $"Tipo={command.AdjustmentType}; Almacen={command.Warehouse}; Articulo={command.ItemCode}; Descripcion={command.ItemDescription}{(string.IsNullOrWhiteSpace(command.Color) ? string.Empty : $"; Color={command.Color}")}; Cantidad={command.Quantity:0.###}; Unidad={command.UnitOfMeasure}{(string.IsNullOrWhiteSpace(command.Notes) ? string.Empty : $"; Notas={command.Notes}")}"
        }, cancellationToken);

        return movementId;
    }

    public async Task<int> SaveCountAsync(
        SaveStockCountCommand command,
        CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return 0;
        }

        await EnsureCompanyAccessAsync(command.TenantId, command.CompanyId, cancellationToken);
        EnsureWriteAccess();
        NormalizeAndValidateCount(command);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        var existing = command.CountNumber.HasValue
            ? await LoadCountByNumberAsync(connection, transaction, command.TenantId, command.CompanyId, command.CountNumber.Value, cancellationToken)
            : null;

        if (existing is not null &&
            !string.Equals(existing.Status, StockCountStatuses.Draft, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Solo se pueden modificar inventarios en borrador.");
        }

        var nowUtc = DateTime.UtcNow;
        var countId = existing?.CountId ?? Guid.NewGuid();
        var countNumber = command.CountNumber ?? await GetNextCountNumberAsync(connection, transaction, command.TenantId, command.CompanyId, cancellationToken);

        foreach (var line in command.Lines)
        {
            if (string.Equals(command.Status, StockCountStatuses.Completed, StringComparison.OrdinalIgnoreCase))
            {
                line.ExpectedQuantity = await GetCurrentStockAsync(
                    connection,
                    command.TenantId,
                    command.CompanyId,
                    command.Warehouse,
                    line.ItemCode,
                    line.ItemDescription,
                    line.Color,
                    cancellationToken);
            }

            line.DifferenceQuantity = decimal.Round(line.CountedQuantity - line.ExpectedQuantity, 3, MidpointRounding.AwayFromZero);
            if (line.DifferenceQuantity == 0m)
            {
                line.IsDifferenceValidated = true;
            }
        }

        if (string.Equals(command.Status, StockCountStatuses.Completed, StringComparison.OrdinalIgnoreCase) &&
            command.Lines.Any(line => line.DifferenceQuantity != 0m && !line.IsDifferenceValidated))
        {
            throw new InvalidOperationException("Valida todas las diferencias antes de completar el inventario.");
        }

        if (string.Equals(command.Status, StockCountStatuses.Completed, StringComparison.OrdinalIgnoreCase) &&
            command.IsBlindCount &&
            !command.IsBlindCountRevealed)
        {
            throw new InvalidOperationException("Revela el stock esperado antes de completar un conteo ciego.");
        }

        var expectedTotal = command.Lines.Sum(line => line.ExpectedQuantity);
        var countedTotal = command.Lines.Sum(line => line.CountedQuantity);
        var differenceTotal = command.Lines.Sum(line => line.DifferenceQuantity);
        var differenceLineCount = command.Lines.Count(line => line.DifferenceQuantity != 0m);

        if (existing is null)
        {
            await using var insertCommand = connection.CreateCommand();
            insertCommand.Transaction = transaction;
            insertCommand.CommandText =
                """
                INSERT INTO stock_counts (
                    count_id,
                    tenant_id,
                    company_id,
                    count_number,
                    count_date,
                    status,
                    warehouse,
                    is_blind_count,
                    is_blind_count_revealed,
                    line_count,
                    difference_line_count,
                    expected_total_quantity,
                    counted_total_quantity,
                    difference_total_quantity,
                    notes,
                    origin,
                    is_deleted,
                    created_utc,
                    updated_utc)
                VALUES (
                    @countId,
                    @tenantId,
                    @companyId,
                    @countNumber,
                    @countDate,
                    @status,
                    @warehouse,
                    @isBlindCount,
                    @isBlindCountRevealed,
                    @lineCount,
                    @differenceLineCount,
                    @expectedTotal,
                    @countedTotal,
                    @differenceTotal,
                    @notes,
                    'local',
                    0,
                    @nowUtc,
                    @nowUtc);
                """;
            insertCommand.Parameters.AddWithValue("@countId", countId.ToString());
            insertCommand.Parameters.AddWithValue("@tenantId", command.TenantId.ToString());
            insertCommand.Parameters.AddWithValue("@companyId", command.CompanyId.ToString());
            insertCommand.Parameters.AddWithValue("@countNumber", countNumber);
            insertCommand.Parameters.AddWithValue("@countDate", command.CountDate.Date);
            insertCommand.Parameters.AddWithValue("@status", command.Status);
            insertCommand.Parameters.AddWithValue("@warehouse", command.Warehouse);
            insertCommand.Parameters.AddWithValue("@isBlindCount", command.IsBlindCount);
            insertCommand.Parameters.AddWithValue("@isBlindCountRevealed", command.IsBlindCountRevealed);
            insertCommand.Parameters.AddWithValue("@lineCount", command.Lines.Count);
            insertCommand.Parameters.AddWithValue("@differenceLineCount", differenceLineCount);
            insertCommand.Parameters.AddWithValue("@expectedTotal", expectedTotal);
            insertCommand.Parameters.AddWithValue("@countedTotal", countedTotal);
            insertCommand.Parameters.AddWithValue("@differenceTotal", differenceTotal);
            insertCommand.Parameters.AddWithValue("@notes", DbValue(command.Notes));
            insertCommand.Parameters.AddWithValue("@nowUtc", nowUtc);
            await insertCommand.ExecuteNonQueryAsync(cancellationToken);
        }
        else
        {
            await using var updateCommand = connection.CreateCommand();
            updateCommand.Transaction = transaction;
            updateCommand.CommandText =
                """
                UPDATE stock_counts
                SET count_date = @countDate,
                    status = @status,
                    warehouse = @warehouse,
                    is_blind_count = @isBlindCount,
                    is_blind_count_revealed = @isBlindCountRevealed,
                    line_count = @lineCount,
                    difference_line_count = @differenceLineCount,
                    expected_total_quantity = @expectedTotal,
                    counted_total_quantity = @countedTotal,
                    difference_total_quantity = @differenceTotal,
                    notes = @notes,
                    updated_utc = @nowUtc
                WHERE count_id = @countId;
                """;
            updateCommand.Parameters.AddWithValue("@countId", countId.ToString());
            updateCommand.Parameters.AddWithValue("@countDate", command.CountDate.Date);
            updateCommand.Parameters.AddWithValue("@status", command.Status);
            updateCommand.Parameters.AddWithValue("@warehouse", command.Warehouse);
            updateCommand.Parameters.AddWithValue("@isBlindCount", command.IsBlindCount);
            updateCommand.Parameters.AddWithValue("@isBlindCountRevealed", command.IsBlindCountRevealed);
            updateCommand.Parameters.AddWithValue("@lineCount", command.Lines.Count);
            updateCommand.Parameters.AddWithValue("@differenceLineCount", differenceLineCount);
            updateCommand.Parameters.AddWithValue("@expectedTotal", expectedTotal);
            updateCommand.Parameters.AddWithValue("@countedTotal", countedTotal);
            updateCommand.Parameters.AddWithValue("@differenceTotal", differenceTotal);
            updateCommand.Parameters.AddWithValue("@notes", DbValue(command.Notes));
            updateCommand.Parameters.AddWithValue("@nowUtc", nowUtc);
            await updateCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        await DeleteCountLinesAsync(connection, transaction, countId, cancellationToken);
        await InsertCountLinesAsync(connection, transaction, countId, command.Lines, cancellationToken);
        await DeleteCountMovementsAsync(connection, transaction, countId, cancellationToken);

        if (string.Equals(command.Status, StockCountStatuses.Completed, StringComparison.OrdinalIgnoreCase))
        {
            foreach (var line in command.Lines.Where(line => line.DifferenceQuantity != 0m).OrderBy(line => line.LineNumber))
            {
                await InsertCountMovementAsync(
                    connection,
                    transaction,
                    command.TenantId,
                    command.CompanyId,
                    countId,
                    countNumber,
                    line,
                    command.CountDate,
                    command.Warehouse,
                    command.Notes,
                    cancellationToken);
            }
        }

        await transaction.CommitAsync(cancellationToken);

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = command.TenantId,
            CompanyId = command.CompanyId,
            UserId = _currentUserContext.UserId,
            Action = existing is null ? "StockCountCreated" : "StockCountUpdated",
            EntityName = "StockCount",
            EntityId = countId.ToString(),
            Details = $"Numero={countNumber}; Estado={command.Status}; Almacen={command.Warehouse}; Lineas={command.Lines.Count}; Diferencias={differenceLineCount}; Delta={differenceTotal:0.###}"
        }, cancellationToken);

        return countNumber;
    }

    public async Task DeleteCountAsync(
        Guid tenantId,
        Guid companyId,
        int countNumber,
        CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return;
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);
        EnsureWriteAccess();

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);
        var existing = await LoadCountByNumberAsync(connection, transaction, tenantId, companyId, countNumber, cancellationToken);
        if (existing is null)
        {
            return;
        }

        if (string.Equals(existing.Status, StockCountStatuses.Completed, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("No se puede eliminar un inventario completado.");
        }

        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            UPDATE stock_counts
            SET is_deleted = 1,
                updated_utc = @nowUtc
            WHERE count_id = @countId;
            """;
        command.Parameters.AddWithValue("@countId", existing.CountId.ToString());
        command.Parameters.AddWithValue("@nowUtc", DateTime.UtcNow);
        await command.ExecuteNonQueryAsync(cancellationToken);

        await DeleteCountMovementsAsync(connection, transaction, existing.CountId, cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = tenantId,
            CompanyId = companyId,
            UserId = _currentUserContext.UserId,
            Action = "StockCountDeleted",
            EntityName = "StockCount",
            EntityId = existing.CountId.ToString(),
            Details = $"Numero={countNumber}; Estado={existing.Status}"
        }, cancellationToken);
    }

    public async Task<int> SaveTransferAsync(
        SaveStockTransferCommand command,
        CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return 0;
        }

        await EnsureCompanyAccessAsync(command.TenantId, command.CompanyId, cancellationToken);
        EnsureWriteAccess();
        NormalizeAndValidate(command);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        var existing = command.TransferNumber.HasValue
            ? await LoadTransferByNumberAsync(connection, transaction, command.TenantId, command.CompanyId, command.TransferNumber.Value, cancellationToken)
            : null;

        if (existing is not null &&
            !string.Equals(existing.Status, StockTransferStatuses.Draft, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Solo se pueden modificar traspasos en borrador.");
        }

        var nowUtc = DateTime.UtcNow;
        var transferId = existing?.TransferId ?? Guid.NewGuid();
        var transferNumber = command.TransferNumber ?? await GetNextTransferNumberAsync(connection, transaction, command.TenantId, command.CompanyId, cancellationToken);
        var totalQuantity = command.Lines.Sum(line => line.Quantity);

        if (existing is null)
        {
            await using var insertCommand = connection.CreateCommand();
            insertCommand.Transaction = transaction;
            insertCommand.CommandText =
                """
                INSERT INTO stock_transfers (
                    transfer_id,
                    tenant_id,
                    company_id,
                    transfer_number,
                    transfer_date,
                    status,
                    from_warehouse,
                    to_warehouse,
                    line_count,
                    total_quantity,
                    notes,
                    origin,
                    is_deleted,
                    created_utc,
                    updated_utc)
                VALUES (
                    @transferId,
                    @tenantId,
                    @companyId,
                    @transferNumber,
                    @transferDate,
                    @status,
                    @fromWarehouse,
                    @toWarehouse,
                    @lineCount,
                    @totalQuantity,
                    @notes,
                    'local',
                    0,
                    @nowUtc,
                    @nowUtc);
                """;
            insertCommand.Parameters.AddWithValue("@transferId", transferId.ToString());
            insertCommand.Parameters.AddWithValue("@tenantId", command.TenantId.ToString());
            insertCommand.Parameters.AddWithValue("@companyId", command.CompanyId.ToString());
            insertCommand.Parameters.AddWithValue("@transferNumber", transferNumber);
            insertCommand.Parameters.AddWithValue("@transferDate", command.TransferDate.Date);
            insertCommand.Parameters.AddWithValue("@status", command.Status);
            insertCommand.Parameters.AddWithValue("@fromWarehouse", command.FromWarehouse);
            insertCommand.Parameters.AddWithValue("@toWarehouse", command.ToWarehouse);
            insertCommand.Parameters.AddWithValue("@lineCount", command.Lines.Count);
            insertCommand.Parameters.AddWithValue("@totalQuantity", totalQuantity);
            insertCommand.Parameters.AddWithValue("@notes", DbValue(command.Notes));
            insertCommand.Parameters.AddWithValue("@nowUtc", nowUtc);
            await insertCommand.ExecuteNonQueryAsync(cancellationToken);
        }
        else
        {
            await using var updateCommand = connection.CreateCommand();
            updateCommand.Transaction = transaction;
            updateCommand.CommandText =
                """
                UPDATE stock_transfers
                SET transfer_date = @transferDate,
                    status = @status,
                    from_warehouse = @fromWarehouse,
                    to_warehouse = @toWarehouse,
                    line_count = @lineCount,
                    total_quantity = @totalQuantity,
                    notes = @notes,
                    updated_utc = @nowUtc
                WHERE transfer_id = @transferId;
                """;
            updateCommand.Parameters.AddWithValue("@transferId", transferId.ToString());
            updateCommand.Parameters.AddWithValue("@transferDate", command.TransferDate.Date);
            updateCommand.Parameters.AddWithValue("@status", command.Status);
            updateCommand.Parameters.AddWithValue("@fromWarehouse", command.FromWarehouse);
            updateCommand.Parameters.AddWithValue("@toWarehouse", command.ToWarehouse);
            updateCommand.Parameters.AddWithValue("@lineCount", command.Lines.Count);
            updateCommand.Parameters.AddWithValue("@totalQuantity", totalQuantity);
            updateCommand.Parameters.AddWithValue("@notes", DbValue(command.Notes));
            updateCommand.Parameters.AddWithValue("@nowUtc", nowUtc);
            await updateCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        await DeleteTransferLinesAsync(connection, transaction, transferId, cancellationToken);
        await InsertTransferLinesAsync(connection, transaction, transferId, command.Lines, cancellationToken);
        await DeleteTransferMovementsAsync(connection, transaction, transferId, cancellationToken);

        if (string.Equals(command.Status, StockTransferStatuses.Completed, StringComparison.OrdinalIgnoreCase))
        {
            foreach (var line in command.Lines.OrderBy(line => line.LineNumber))
            {
                var availableStock = await GetAvailableStockForTransferAsync(
                    connection,
                    command.TenantId,
                    command.CompanyId,
                    command.FromWarehouse,
                    line.ItemCode,
                    line.ItemDescription,
                    line.Color,
                    cancellationToken);

                if (availableStock < line.Quantity)
                {
                    throw new InvalidOperationException(
                        $"No hay stock suficiente en {command.FromWarehouse} para {line.ItemDescription} ({line.Quantity:0.###}). Disponible: {availableStock:0.###}.");
                }

                await InsertTransferMovementAsync(
                    connection,
                    transaction,
                    command.TenantId,
                    command.CompanyId,
                    transferId,
                    transferNumber,
                    line,
                    command.TransferDate,
                    command.FromWarehouse,
                    StockMovementTypes.OutboundWarehouseTransfer,
                    "StockTransferOutbound",
                    command.Notes,
                    cancellationToken);

                await InsertTransferMovementAsync(
                    connection,
                    transaction,
                    command.TenantId,
                    command.CompanyId,
                    transferId,
                    transferNumber,
                    line,
                    command.TransferDate,
                    command.ToWarehouse,
                    StockMovementTypes.InboundWarehouseTransfer,
                    "StockTransferInbound",
                    command.Notes,
                    cancellationToken);
            }
        }

        await transaction.CommitAsync(cancellationToken);

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = command.TenantId,
            CompanyId = command.CompanyId,
            UserId = _currentUserContext.UserId,
            Action = existing is null ? "StockTransferCreated" : "StockTransferUpdated",
            EntityName = "StockTransfer",
            EntityId = transferId.ToString(),
            Details = $"Numero={transferNumber}; Estado={command.Status}; Origen={command.FromWarehouse}; Destino={command.ToWarehouse}; Lineas={command.Lines.Count}; Total={totalQuantity:0.###}"
        }, cancellationToken);

        return transferNumber;
    }

    public async Task DeleteTransferAsync(
        Guid tenantId,
        Guid companyId,
        int transferNumber,
        CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return;
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);
        EnsureWriteAccess();

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);
        var existing = await LoadTransferByNumberAsync(connection, transaction, tenantId, companyId, transferNumber, cancellationToken);
        if (existing is null)
        {
            return;
        }

        if (string.Equals(existing.Status, StockTransferStatuses.Completed, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("No se puede eliminar un traspaso completado.");
        }

        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            UPDATE stock_transfers
            SET is_deleted = 1,
                updated_utc = @nowUtc
            WHERE transfer_id = @transferId;
            """;
        command.Parameters.AddWithValue("@transferId", existing.TransferId.ToString());
        command.Parameters.AddWithValue("@nowUtc", DateTime.UtcNow);
        await command.ExecuteNonQueryAsync(cancellationToken);

        await DeleteTransferMovementsAsync(connection, transaction, existing.TransferId, cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = tenantId,
            CompanyId = companyId,
            UserId = _currentUserContext.UserId,
            Action = "StockTransferDeleted",
            EntityName = "StockTransfer",
            EntityId = existing.TransferId.ToString(),
            Details = $"Numero={transferNumber}; Estado={existing.Status}"
        }, cancellationToken);
    }

    private static async Task<StockBalanceSearchResultDto> SearchLegacyBalancesAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        StockBalanceFilter filter,
        string search,
        string warehouse,
        string likeSearch,
        int pageSize,
        int offset,
        string centerCode,
        CancellationToken cancellationToken)
    {
        var catalogScope = NormalizeCatalogScope(filter.CatalogScope);
        var balances = new Dictionary<string, StockBalanceListItemDto>(StringComparer.OrdinalIgnoreCase);

        await using (var legacyCommand = connection.CreateCommand())
        {
            legacyCommand.CommandText =
                """
                SELECT warehouse,
                       item_code,
                       item_description,
                       unit_of_measure,
                       movement_count,
                       last_movement_date,
                       current_stock
                FROM legacy_stock_balances
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND (
                        @catalogScope = ''
                        OR (
                            @catalogScope = 'Hilos'
                            AND EXISTS (
                                SELECT 1
                                FROM fil f
                                WHERE f.CENTRO = @centerCode
                                  AND COALESCE(f.is_deleted, 0) = 0
                                  AND f.CODI = COALESCE(item_code, '')
                            )
                        )
                        OR (
                            @catalogScope = 'Tejidos'
                            AND EXISTS (
                                SELECT 1
                                FROM teixits t
                                WHERE t.CENTRO = @centerCode
                                  AND COALESCE(t.is_deleted, 0) = 0
                                  AND t.CODI = COALESCE(item_code, '')
                            )
                        )
                        OR (
                            @catalogScope = 'Models'
                            AND EXISTS (
                                SELECT 1
                                FROM article_models am
                                WHERE am.CENTRO = @centerCode
                                  AND COALESCE(am.is_deleted, 0) = 0
                                  AND am.CODI = COALESCE(item_code, '')
                            )
                        )
                      )
                  AND (
                        @warehouse = ''
                        OR COALESCE(warehouse, '') = @warehouse
                      )
                  AND (
                        @search = ''
                        OR COALESCE(item_code, '') LIKE @likeSearch
                        OR item_description LIKE @likeSearch
                        OR COALESCE(warehouse, '') LIKE @likeSearch
                      );
                """;
            legacyCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            legacyCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
            legacyCommand.Parameters.AddWithValue("@catalogScope", catalogScope);
            legacyCommand.Parameters.AddWithValue("@centerCode", centerCode);
            legacyCommand.Parameters.AddWithValue("@warehouse", warehouse);
            legacyCommand.Parameters.AddWithValue("@search", search);
            legacyCommand.Parameters.AddWithValue("@likeSearch", likeSearch);

            await using var reader = await legacyCommand.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                var item = new StockBalanceListItemDto
                {
                    Warehouse = reader.GetStringOrEmpty("warehouse"),
                    ItemCode = reader.GetStringOrEmpty("item_code"),
                    ItemDescription = reader.GetStringOrEmpty("item_description"),
                    UnitOfMeasure = reader.GetStringOrEmpty("unit_of_measure"),
                    MovementCount = reader.GetInt32OrDefault("movement_count"),
                    LastMovementDate = reader.IsDBNull(reader.GetOrdinal("last_movement_date"))
                        ? null
                        : reader.GetDateTime(reader.GetOrdinal("last_movement_date")),
                    CurrentStock = reader.GetDecimal(reader.GetOrdinal("current_stock"))
                };
                balances[BuildBalanceKey(item.Warehouse, item.ItemCode, item.ItemDescription, item.UnitOfMeasure)] = item;
            }
        }

        await using (var movementCommand = connection.CreateCommand())
        {
            movementCommand.CommandText =
                """
                SELECT warehouse,
                       item_code,
                       item_description,
                       unit_of_measure,
                       COUNT(*) AS movement_count,
                       MAX(movement_date) AS last_movement_date,
                       COALESCE(SUM(
                           CASE
                               WHEN movement_type LIKE 'Inbound%' THEN quantity
                               ELSE -quantity
                           END
                       ), 0) AS current_stock
                FROM inventory_movements
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND (
                        @catalogScope = ''
                        OR (
                            @catalogScope = 'Hilos'
                            AND EXISTS (
                                SELECT 1
                                FROM fil f
                                WHERE f.CENTRO = @centerCode
                                  AND COALESCE(f.is_deleted, 0) = 0
                                  AND f.CODI = COALESCE(item_code, '')
                            )
                        )
                        OR (
                            @catalogScope = 'Tejidos'
                            AND EXISTS (
                                SELECT 1
                                FROM teixits t
                                WHERE t.CENTRO = @centerCode
                                  AND COALESCE(t.is_deleted, 0) = 0
                                  AND t.CODI = COALESCE(item_code, '')
                            )
                        )
                        OR (
                            @catalogScope = 'Models'
                            AND EXISTS (
                                SELECT 1
                                FROM article_models am
                                WHERE am.CENTRO = @centerCode
                                  AND COALESCE(am.is_deleted, 0) = 0
                                  AND am.CODI = COALESCE(item_code, '')
                            )
                        )
                      )
                  AND (
                        @warehouse = ''
                        OR COALESCE(warehouse, '') = @warehouse
                      )
                  AND (
                        @search = ''
                        OR COALESCE(item_code, '') LIKE @likeSearch
                        OR item_description LIKE @likeSearch
                        OR COALESCE(warehouse, '') LIKE @likeSearch
                      )
                GROUP BY warehouse, item_code, item_description, unit_of_measure;
                """;
            movementCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            movementCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
            movementCommand.Parameters.AddWithValue("@catalogScope", catalogScope);
            movementCommand.Parameters.AddWithValue("@centerCode", centerCode);
            movementCommand.Parameters.AddWithValue("@warehouse", warehouse);
            movementCommand.Parameters.AddWithValue("@search", search);
            movementCommand.Parameters.AddWithValue("@likeSearch", likeSearch);

            await using var reader = await movementCommand.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                var movementWarehouse = reader.GetStringOrEmpty("warehouse");
                var movementItemCode = reader.GetStringOrEmpty("item_code");
                var movementDescription = reader.GetStringOrEmpty("item_description");
                var movementUnit = reader.GetStringOrEmpty("unit_of_measure");
                var key = BuildBalanceKey(movementWarehouse, movementItemCode, movementDescription, movementUnit);

                if (!balances.TryGetValue(key, out var current))
                {
                    current = new StockBalanceListItemDto
                    {
                        Warehouse = movementWarehouse,
                        ItemCode = movementItemCode,
                        ItemDescription = movementDescription,
                        UnitOfMeasure = movementUnit
                    };
                }

                current.MovementCount += reader.GetInt32(reader.GetOrdinal("movement_count"));
                current.LastMovementDate = MaxDate(
                    current.LastMovementDate,
                    reader.IsDBNull(reader.GetOrdinal("last_movement_date")) ? null : reader.GetDateTime(reader.GetOrdinal("last_movement_date")));
                current.CurrentStock += reader.GetDecimal(reader.GetOrdinal("current_stock"));
                balances[key] = current;
            }
        }

        var orderedItems = SortStockBalancesForDisplay(balances.Values.Where(item => item.CurrentStock != 0m), filter).ToArray();
        if (orderedItems.Length == 0)
        {
            return new StockBalanceSearchResultDto();
        }

        return new StockBalanceSearchResultDto
        {
            Items = orderedItems.Skip(offset).Take(pageSize).ToArray(),
            TotalCount = orderedItems.Length
        };
    }

    private static async Task<IReadOnlyCollection<StockMovementListItemDto>> ReadMovementListAsync(
        MySqlCommand command,
        CancellationToken cancellationToken)
    {
        var items = new List<StockMovementListItemDto>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var sourceType = reader.GetString("source_document_type");
            int? sourceNumber = reader.IsDBNull(reader.GetOrdinal("source_document_number"))
                ? null
                : reader.GetInt32(reader.GetOrdinal("source_document_number"));

            items.Add(new StockMovementListItemDto
            {
                MovementId = reader.GetGuid("movement_id"),
                MovementDate = reader.GetDateTime(reader.GetOrdinal("movement_date")),
                MovementType = reader.GetStringOrEmpty("movement_type"),
                Warehouse = reader.GetStringOrEmpty("warehouse"),
                ItemCode = reader.GetStringOrEmpty("item_code"),
                ItemDescription = reader.GetStringOrEmpty("item_description"),
                Color = reader.GetStringOrEmpty("color"),
                Quantity = reader.GetDecimal(reader.GetOrdinal("quantity")),
                UnitOfMeasure = reader.GetStringOrEmpty("unit_of_measure"),
                SupplierName = reader.GetStringOrEmpty("supplier_name"),
                SupplierReference = reader.GetStringOrEmpty("supplier_reference"),
                VehiclePlate = reader.GetStringOrEmpty("vehicle_plate"),
                SourceDocumentType = sourceType,
                SourceDocumentNumber = sourceNumber,
                SourceDocumentDisplay = BuildSourceDocumentDisplay(sourceType, sourceNumber),
                Notes = reader.GetStringOrEmpty("notes")
            });
        }

        return items;
    }

    private static async Task<IReadOnlyCollection<StockMovementGroupSummaryDto>> LoadMovementGroupSummaryAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        string catalogScope,
        string centerCode,
        string warehouse,
        string supplierName,
        string color,
        string movementType,
        string search,
        string likeSearch,
        string likeSupplierName,
        string likeColor,
        string groupColumn,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            $"""
            SELECT COALESCE({groupColumn}, '') AS group_label,
                   COUNT(*) AS movement_count,
                   COALESCE(SUM(quantity), 0) AS total_quantity
            FROM inventory_movements
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND (
                    @catalogScope = ''
                        OR (
                            @catalogScope = 'Hilos'
                            AND EXISTS (
                                SELECT 1
                                FROM fil f
                            WHERE f.CENTRO = @centerCode
                              AND COALESCE(f.is_deleted, 0) = 0
                                  AND f.CODI = COALESCE(item_code, '')
                            )
                        )
                        OR (
                            @catalogScope = 'Tejidos'
                            AND EXISTS (
                                SELECT 1
                                FROM teixits t
                                WHERE t.CENTRO = @centerCode
                                  AND COALESCE(t.is_deleted, 0) = 0
                                  AND t.CODI = COALESCE(item_code, '')
                            )
                        )
                        OR (
                            @catalogScope = 'Models'
                            AND EXISTS (
                                SELECT 1
                                FROM article_models am
                            WHERE am.CENTRO = @centerCode
                              AND COALESCE(am.is_deleted, 0) = 0
                              AND am.CODI = COALESCE(item_code, '')
                        )
                    )
                  )
              AND (
                    @warehouse = ''
                    OR COALESCE(warehouse, '') = @warehouse
                  )
              AND (
                    @supplierName = ''
                    OR COALESCE(supplier_name, '') LIKE @likeSupplierName
                  )
              AND (
                    @color = ''
                    OR COALESCE(color, '') LIKE @likeColor
                  )
              AND (
                    @movementType = ''
                    OR COALESCE(movement_type, '') = @movementType
                  )
              AND (
                    @search = ''
                    OR COALESCE(item_code, '') LIKE @likeSearch
                    OR item_description LIKE @likeSearch
                    OR COALESCE(color, '') LIKE @likeSearch
                    OR COALESCE(supplier_name, '') LIKE @likeSearch
                    OR COALESCE(supplier_reference, '') LIKE @likeSearch
                    OR COALESCE(vehicle_plate, '') LIKE @likeSearch
                    OR CAST(source_document_number AS CHAR) LIKE @likeSearch
                  )
              AND COALESCE({groupColumn}, '') <> ''
            GROUP BY COALESCE({groupColumn}, '')
            ORDER BY movement_count DESC, total_quantity DESC, group_label
            LIMIT 8;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@catalogScope", catalogScope);
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@warehouse", warehouse);
        command.Parameters.AddWithValue("@supplierName", supplierName);
        command.Parameters.AddWithValue("@likeSupplierName", likeSupplierName);
        command.Parameters.AddWithValue("@color", color);
        command.Parameters.AddWithValue("@likeColor", likeColor);
        command.Parameters.AddWithValue("@movementType", movementType);
        command.Parameters.AddWithValue("@search", search);
        command.Parameters.AddWithValue("@likeSearch", likeSearch);

        var items = new List<StockMovementGroupSummaryDto>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new StockMovementGroupSummaryDto
            {
                Label = reader.GetStringOrEmpty("group_label"),
                MovementCount = reader.GetInt32(reader.GetOrdinal("movement_count")),
                Quantity = reader.GetDecimal(reader.GetOrdinal("total_quantity"))
            });
        }

        return items;
    }

    private static async Task<decimal> GetCurrentStockAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        string warehouse,
        string itemCode,
        string itemDescription,
        string color,
        CancellationToken cancellationToken)
    {
        decimal legacyStock = 0m;
        if (string.IsNullOrWhiteSpace(color))
        {
            await using var legacyCommand = connection.CreateCommand();
            legacyCommand.CommandText =
                """
                SELECT COALESCE(current_stock, 0)
                FROM legacy_stock_balances
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND COALESCE(warehouse, '') = @warehouse
                  AND COALESCE(item_code, '') = @itemCode
                  AND item_description = @itemDescription
                LIMIT 1;
                """;
            legacyCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            legacyCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
            legacyCommand.Parameters.AddWithValue("@warehouse", warehouse);
            legacyCommand.Parameters.AddWithValue("@itemCode", itemCode);
            legacyCommand.Parameters.AddWithValue("@itemDescription", itemDescription);
            var legacyScalar = await legacyCommand.ExecuteScalarAsync(cancellationToken);
            if (legacyScalar is not null && legacyScalar != DBNull.Value)
            {
                legacyStock = Convert.ToDecimal(legacyScalar);
            }
        }

        await using var movementCommand = connection.CreateCommand();
        movementCommand.CommandText =
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
              AND item_description = @itemDescription
              AND COALESCE(color, '') = @color;
            """;
        movementCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        movementCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
        movementCommand.Parameters.AddWithValue("@warehouse", warehouse);
        movementCommand.Parameters.AddWithValue("@itemCode", itemCode);
        movementCommand.Parameters.AddWithValue("@itemDescription", itemDescription);
        movementCommand.Parameters.AddWithValue("@color", color);
        var localDelta = Convert.ToDecimal(await movementCommand.ExecuteScalarAsync(cancellationToken));
        return legacyStock + localDelta;
    }

    private static async Task<StockLegacySyncInfoDto> LoadLegacySyncInfoAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT last_status,
                   last_completed_utc,
                   last_inserted,
                   last_updated,
                   last_skipped,
                   last_errors
            FROM legacy_sync_checkpoints
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND module_key = @moduleKey
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@moduleKey", LegacySyncModuleKeys.StockItems);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return new StockLegacySyncInfoDto();
        }

        var status = reader.GetStringOrEmpty("last_status");
        return new StockLegacySyncInfoDto
        {
            IsActive = string.Equals(status, "Completed", StringComparison.OrdinalIgnoreCase) ||
                       string.Equals(status, "CompletedWithErrors", StringComparison.OrdinalIgnoreCase),
            LastStatus = status,
            LastCompletedUtc = reader.IsDBNull(reader.GetOrdinal("last_completed_utc"))
                ? null
                : reader.GetDateTime(reader.GetOrdinal("last_completed_utc")),
            LastInserted = reader.GetInt32OrDefault("last_inserted"),
            LastUpdated = reader.GetInt32OrDefault("last_updated"),
            LastSkipped = reader.GetInt32OrDefault("last_skipped"),
            LastErrors = reader.GetInt32OrDefault("last_errors")
        };
    }

    private static async Task<IReadOnlyCollection<StockBalanceListItemDto>> LoadWarehouseBalanceSnapshotAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        string warehouse,
        CancellationToken cancellationToken)
    {
        var normalizedWarehouse = warehouse.Trim();
        if (string.IsNullOrWhiteSpace(normalizedWarehouse))
        {
            return [];
        }

        var legacySyncInfo = await LoadLegacySyncInfoAsync(connection, tenantId, companyId, cancellationToken);
        var balances = new Dictionary<string, StockBalanceListItemDto>(StringComparer.OrdinalIgnoreCase);

        if (legacySyncInfo.IsActive)
        {
            await using var legacyCommand = connection.CreateCommand();
            legacyCommand.CommandText =
                """
                SELECT warehouse,
                       item_code,
                       item_description,
                       unit_of_measure,
                       movement_count,
                       last_movement_date,
                       current_stock
                FROM legacy_stock_balances
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND COALESCE(warehouse, '') = @warehouse;
                """;
            legacyCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            legacyCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
            legacyCommand.Parameters.AddWithValue("@warehouse", normalizedWarehouse);

            await using var reader = await legacyCommand.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                var item = new StockBalanceListItemDto
                {
                    Warehouse = reader.GetStringOrEmpty("warehouse"),
                    ItemCode = reader.GetStringOrEmpty("item_code"),
                    ItemDescription = reader.GetStringOrEmpty("item_description"),
                    UnitOfMeasure = reader.GetStringOrEmpty("unit_of_measure"),
                    MovementCount = reader.GetInt32OrDefault("movement_count"),
                    LastMovementDate = reader.IsDBNull(reader.GetOrdinal("last_movement_date"))
                        ? null
                        : reader.GetDateTime(reader.GetOrdinal("last_movement_date")),
                    CurrentStock = reader.GetDecimal(reader.GetOrdinal("current_stock"))
                };
                balances[BuildBalanceKey(item.Warehouse, item.ItemCode, item.ItemDescription, item.UnitOfMeasure)] = item;
            }
        }

        await using (var movementCommand = connection.CreateCommand())
        {
            movementCommand.CommandText =
                """
                SELECT warehouse,
                       item_code,
                       item_description,
                       unit_of_measure,
                       COUNT(*) AS movement_count,
                       MAX(movement_date) AS last_movement_date,
                       COALESCE(SUM(
                           CASE
                               WHEN movement_type LIKE 'Inbound%' THEN quantity
                               ELSE -quantity
                           END
                       ), 0) AS current_stock
                FROM inventory_movements
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND COALESCE(warehouse, '') = @warehouse
                GROUP BY warehouse, item_code, item_description, unit_of_measure;
                """;
            movementCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            movementCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
            movementCommand.Parameters.AddWithValue("@warehouse", normalizedWarehouse);

            await using var reader = await movementCommand.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                var movementWarehouse = reader.GetStringOrEmpty("warehouse");
                var movementItemCode = reader.GetStringOrEmpty("item_code");
                var movementDescription = reader.GetStringOrEmpty("item_description");
                var movementUnit = reader.GetStringOrEmpty("unit_of_measure");
                var key = BuildBalanceKey(movementWarehouse, movementItemCode, movementDescription, movementUnit);

                if (!balances.TryGetValue(key, out var current))
                {
                    current = new StockBalanceListItemDto
                    {
                        Warehouse = movementWarehouse,
                        ItemCode = movementItemCode,
                        ItemDescription = movementDescription,
                        UnitOfMeasure = movementUnit
                    };
                }

                current.MovementCount += reader.GetInt32(reader.GetOrdinal("movement_count"));
                current.LastMovementDate = MaxDate(
                    current.LastMovementDate,
                    reader.IsDBNull(reader.GetOrdinal("last_movement_date")) ? null : reader.GetDateTime(reader.GetOrdinal("last_movement_date")));
                current.CurrentStock += reader.GetDecimal(reader.GetOrdinal("current_stock"));
                balances[key] = current;
            }
        }

        return balances.Values
            .Where(item => item.CurrentStock != 0m)
            .OrderBy(item => item.ItemCode, StringComparer.OrdinalIgnoreCase)
            .ThenBy(item => item.ItemDescription, StringComparer.OrdinalIgnoreCase)
            .ToArray();
    }

    private static async Task<StockCountDetailDto?> LoadCountByNumberAsync(
        MySqlConnection connection,
        MySqlTransaction? transaction,
        Guid tenantId,
        Guid companyId,
        int countNumber,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            SELECT count_id,
                   count_number,
                   count_date,
                   status,
                   warehouse,
                   is_blind_count,
                   is_blind_count_revealed,
                   line_count,
                   difference_line_count,
                   expected_total_quantity,
                   counted_total_quantity,
                   difference_total_quantity,
                   notes,
                   origin
            FROM stock_counts
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND count_number = @countNumber
              AND COALESCE(is_deleted, 0) = 0
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@countNumber", countNumber);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        return new StockCountDetailDto
        {
            CountId = reader.GetGuid("count_id"),
            CountNumber = reader.GetInt32(reader.GetOrdinal("count_number")),
            CountDate = reader.GetDateTime(reader.GetOrdinal("count_date")),
            Status = reader.GetStringOrEmpty("status"),
            Warehouse = reader.GetStringOrEmpty("warehouse"),
            IsBlindCount = reader.GetBoolean(reader.GetOrdinal("is_blind_count")),
            IsBlindCountRevealed = reader.GetBoolean(reader.GetOrdinal("is_blind_count_revealed")),
            LineCount = reader.GetInt32OrDefault("line_count"),
            DifferenceLineCount = reader.GetInt32OrDefault("difference_line_count"),
            ExpectedTotalQuantity = reader.GetDecimal(reader.GetOrdinal("expected_total_quantity")),
            CountedTotalQuantity = reader.GetDecimal(reader.GetOrdinal("counted_total_quantity")),
            DifferenceTotalQuantity = reader.GetDecimal(reader.GetOrdinal("difference_total_quantity")),
            Notes = reader.GetStringOrEmpty("notes"),
            Origin = reader.GetStringOrEmpty("origin")
        };
    }

    private static async Task<IReadOnlyCollection<StockCountLineDto>> LoadCountLinesAsync(
        MySqlConnection connection,
        MySqlTransaction? transaction,
        Guid countId,
        CancellationToken cancellationToken)
    {
        var items = new List<StockCountLineDto>();
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            SELECT line_number,
                   item_code,
                   item_description,
                   color,
                   expected_quantity,
                   counted_quantity,
                   difference_quantity,
                   is_difference_validated,
                   unit_of_measure,
                   notes
            FROM stock_count_lines
            WHERE count_id = @countId
            ORDER BY line_number;
            """;
        command.Parameters.AddWithValue("@countId", countId.ToString());

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new StockCountLineDto
            {
                LineNumber = reader.GetInt32(reader.GetOrdinal("line_number")),
                ItemCode = reader.GetStringOrEmpty("item_code"),
                ItemDescription = reader.GetStringOrEmpty("item_description"),
                Color = reader.GetStringOrEmpty("color"),
                ExpectedQuantity = reader.GetDecimal(reader.GetOrdinal("expected_quantity")),
                CountedQuantity = reader.GetDecimal(reader.GetOrdinal("counted_quantity")),
                DifferenceQuantity = reader.GetDecimal(reader.GetOrdinal("difference_quantity")),
                IsDifferenceValidated = reader.GetBoolean(reader.GetOrdinal("is_difference_validated")),
                UnitOfMeasure = reader.GetStringOrEmpty("unit_of_measure"),
                Notes = reader.GetStringOrEmpty("notes")
            });
        }

        return items;
    }

    private static async Task<int> GetNextCountNumberAsync(
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
            DocumentNumberingKeys.StockCount,
            cancellationToken);

    private static async Task DeleteCountLinesAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid countId,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = "DELETE FROM stock_count_lines WHERE count_id = @countId;";
        command.Parameters.AddWithValue("@countId", countId.ToString());
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task InsertCountLinesAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid countId,
        IReadOnlyCollection<StockCountLineDto> lines,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            INSERT INTO stock_count_lines (
                count_id,
                line_number,
                item_code,
                item_description,
                color,
                expected_quantity,
                counted_quantity,
                difference_quantity,
                is_difference_validated,
                unit_of_measure,
                notes)
            VALUES (
                @countId,
                @lineNumber,
                @itemCode,
                @itemDescription,
                @color,
                @expectedQuantity,
                @countedQuantity,
                @differenceQuantity,
                @isDifferenceValidated,
                @unitOfMeasure,
                @notes);
            """;
        command.Parameters.AddWithValue("@countId", countId.ToString());
        command.Parameters.Add(new MySqlParameter("@lineNumber", 0));
        command.Parameters.Add(new MySqlParameter("@itemCode", DBNull.Value));
        command.Parameters.Add(new MySqlParameter("@itemDescription", string.Empty));
        command.Parameters.Add(new MySqlParameter("@color", DBNull.Value));
        command.Parameters.Add(new MySqlParameter("@expectedQuantity", 0m));
        command.Parameters.Add(new MySqlParameter("@countedQuantity", 0m));
        command.Parameters.Add(new MySqlParameter("@differenceQuantity", 0m));
        command.Parameters.Add(new MySqlParameter("@isDifferenceValidated", false));
        command.Parameters.Add(new MySqlParameter("@unitOfMeasure", DBNull.Value));
        command.Parameters.Add(new MySqlParameter("@notes", DBNull.Value));

        foreach (var line in lines.OrderBy(line => line.LineNumber))
        {
            command.Parameters["@lineNumber"].Value = line.LineNumber;
            command.Parameters["@itemCode"].Value = DbValue(line.ItemCode);
            command.Parameters["@itemDescription"].Value = line.ItemDescription;
            command.Parameters["@color"].Value = DbValue(line.Color);
            command.Parameters["@expectedQuantity"].Value = line.ExpectedQuantity;
            command.Parameters["@countedQuantity"].Value = line.CountedQuantity;
            command.Parameters["@differenceQuantity"].Value = line.DifferenceQuantity;
            command.Parameters["@isDifferenceValidated"].Value = line.IsDifferenceValidated;
            command.Parameters["@unitOfMeasure"].Value = DbValue(line.UnitOfMeasure);
            command.Parameters["@notes"].Value = DbValue(line.Notes);
            await command.ExecuteNonQueryAsync(cancellationToken);
        }
    }

    private static async Task DeleteCountMovementsAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid countId,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            DELETE FROM inventory_movements
            WHERE source_document_id = @countId
              AND source_document_type = 'StockCount';
            """;
        command.Parameters.AddWithValue("@countId", countId.ToString());
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task InsertCountMovementAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid tenantId,
        Guid companyId,
        Guid countId,
        int countNumber,
        StockCountLineDto line,
        DateTime countDate,
        string warehouse,
        string countNotes,
        CancellationToken cancellationToken)
    {
        var quantity = Math.Abs(line.DifferenceQuantity);
        if (quantity == 0m)
        {
            return;
        }

        var movementType = line.DifferenceQuantity > 0m
            ? StockMovementTypes.InboundInventoryCountAdjustment
            : StockMovementTypes.OutboundInventoryCountAdjustment;

        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
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
        command.Parameters.AddWithValue("@movementId", Guid.NewGuid().ToString());
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@movementType", movementType);
        command.Parameters.AddWithValue("@movementDate", countDate.Date);
        command.Parameters.AddWithValue("@warehouse", DbValue(warehouse));
        command.Parameters.AddWithValue("@itemCode", DbValue(line.ItemCode));
        command.Parameters.AddWithValue("@itemDescription", line.ItemDescription);
        command.Parameters.AddWithValue("@color", DbValue(line.Color));
        command.Parameters.AddWithValue("@quantity", quantity);
        command.Parameters.AddWithValue("@unitOfMeasure", DbValue(line.UnitOfMeasure));
        command.Parameters.AddWithValue("@sourceDocumentType", "StockCount");
        command.Parameters.AddWithValue("@sourceDocumentId", countId.ToString());
        command.Parameters.AddWithValue("@sourceDocumentNumber", countNumber);
        command.Parameters.AddWithValue("@sourceLineNumber", line.LineNumber);
        command.Parameters.AddWithValue("@notes", DbValue(BuildCountMovementNotes(warehouse, line, countNotes)));
        command.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task<StockTransferDetailDto?> LoadTransferByNumberAsync(
        MySqlConnection connection,
        MySqlTransaction? transaction,
        Guid tenantId,
        Guid companyId,
        int transferNumber,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            SELECT transfer_id,
                   transfer_number,
                   transfer_date,
                   status,
                   from_warehouse,
                   to_warehouse,
                   total_quantity,
                   notes,
                   origin
            FROM stock_transfers
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND transfer_number = @transferNumber
              AND COALESCE(is_deleted, 0) = 0
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@transferNumber", transferNumber);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        return new StockTransferDetailDto
        {
            TransferId = reader.GetGuid("transfer_id"),
            TransferNumber = reader.GetInt32(reader.GetOrdinal("transfer_number")),
            TransferDate = reader.GetDateTime(reader.GetOrdinal("transfer_date")),
            Status = reader.GetStringOrEmpty("status"),
            FromWarehouse = reader.GetStringOrEmpty("from_warehouse"),
            ToWarehouse = reader.GetStringOrEmpty("to_warehouse"),
            TotalQuantity = reader.GetDecimal(reader.GetOrdinal("total_quantity")),
            Notes = reader.GetStringOrEmpty("notes"),
            Origin = reader.GetStringOrEmpty("origin")
        };
    }

    private static async Task<IReadOnlyCollection<StockTransferLineDto>> LoadTransferLinesAsync(
        MySqlConnection connection,
        MySqlTransaction? transaction,
        Guid transferId,
        CancellationToken cancellationToken)
    {
        var items = new List<StockTransferLineDto>();
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            SELECT line_number,
                   item_code,
                   item_description,
                   color,
                   quantity,
                   unit_of_measure,
                   notes
            FROM stock_transfer_lines
            WHERE transfer_id = @transferId
            ORDER BY line_number;
            """;
        command.Parameters.AddWithValue("@transferId", transferId.ToString());

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new StockTransferLineDto
            {
                LineNumber = reader.GetInt32(reader.GetOrdinal("line_number")),
                ItemCode = reader.GetStringOrEmpty("item_code"),
                ItemDescription = reader.GetStringOrEmpty("item_description"),
                Color = reader.GetStringOrEmpty("color"),
                Quantity = reader.GetDecimal(reader.GetOrdinal("quantity")),
                UnitOfMeasure = reader.GetStringOrEmpty("unit_of_measure"),
                Notes = reader.GetStringOrEmpty("notes")
            });
        }

        return items;
    }

    private static async Task<int> GetNextTransferNumberAsync(
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
            DocumentNumberingKeys.StockTransfer,
            cancellationToken);

    private static async Task DeleteTransferLinesAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid transferId,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = "DELETE FROM stock_transfer_lines WHERE transfer_id = @transferId;";
        command.Parameters.AddWithValue("@transferId", transferId.ToString());
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task InsertTransferLinesAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid transferId,
        IReadOnlyCollection<StockTransferLineDto> lines,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            INSERT INTO stock_transfer_lines (
                transfer_id,
                line_number,
                item_code,
                item_description,
                color,
                quantity,
                unit_of_measure,
                notes)
            VALUES (
                @transferId,
                @lineNumber,
                @itemCode,
                @itemDescription,
                @color,
                @quantity,
                @unitOfMeasure,
                @notes);
            """;
        command.Parameters.AddWithValue("@transferId", transferId.ToString());
        command.Parameters.Add(new MySqlParameter("@lineNumber", 0));
        command.Parameters.Add(new MySqlParameter("@itemCode", DBNull.Value));
        command.Parameters.Add(new MySqlParameter("@itemDescription", string.Empty));
        command.Parameters.Add(new MySqlParameter("@color", DBNull.Value));
        command.Parameters.Add(new MySqlParameter("@quantity", 0m));
        command.Parameters.Add(new MySqlParameter("@unitOfMeasure", DBNull.Value));
        command.Parameters.Add(new MySqlParameter("@notes", DBNull.Value));

        foreach (var line in lines.OrderBy(line => line.LineNumber))
        {
            command.Parameters["@lineNumber"].Value = line.LineNumber;
            command.Parameters["@itemCode"].Value = DbValue(line.ItemCode);
            command.Parameters["@itemDescription"].Value = line.ItemDescription;
            command.Parameters["@color"].Value = DbValue(line.Color);
            command.Parameters["@quantity"].Value = line.Quantity;
            command.Parameters["@unitOfMeasure"].Value = DbValue(line.UnitOfMeasure);
            command.Parameters["@notes"].Value = DbValue(line.Notes);
            await command.ExecuteNonQueryAsync(cancellationToken);
        }
    }

    private static async Task DeleteTransferMovementsAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid transferId,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            DELETE FROM inventory_movements
            WHERE source_document_id = @transferId
              AND source_document_type IN ('StockTransferOutbound', 'StockTransferInbound');
            """;
        command.Parameters.AddWithValue("@transferId", transferId.ToString());
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task InsertTransferMovementAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid tenantId,
        Guid companyId,
        Guid transferId,
        int transferNumber,
        StockTransferLineDto line,
        DateTime transferDate,
        string warehouse,
        string movementType,
        string sourceDocumentType,
        string transferNotes,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
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
        command.Parameters.AddWithValue("@movementId", Guid.NewGuid().ToString());
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@movementType", movementType);
        command.Parameters.AddWithValue("@movementDate", transferDate.Date);
        command.Parameters.AddWithValue("@warehouse", DbValue(warehouse));
        command.Parameters.AddWithValue("@itemCode", DbValue(line.ItemCode));
        command.Parameters.AddWithValue("@itemDescription", line.ItemDescription);
        command.Parameters.AddWithValue("@color", DbValue(line.Color));
        command.Parameters.AddWithValue("@quantity", line.Quantity);
        command.Parameters.AddWithValue("@unitOfMeasure", DbValue(line.UnitOfMeasure));
        command.Parameters.AddWithValue("@sourceDocumentType", sourceDocumentType);
        command.Parameters.AddWithValue("@sourceDocumentId", transferId.ToString());
        command.Parameters.AddWithValue("@sourceDocumentNumber", transferNumber);
        command.Parameters.AddWithValue("@sourceLineNumber", line.LineNumber);
        command.Parameters.AddWithValue("@notes", DbValue(BuildTransferMovementNotes(warehouse, transferNotes, line.Notes)));
        command.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task<decimal> GetAvailableStockForTransferAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        string warehouse,
        string itemCode,
        string itemDescription,
        string color,
        CancellationToken cancellationToken)
    {
        var exactStock = await GetCurrentStockAsync(connection, tenantId, companyId, warehouse, itemCode, itemDescription, color, cancellationToken);
        if (exactStock > 0m || string.IsNullOrWhiteSpace(color))
        {
            return exactStock;
        }

        return await GetCurrentStockAsync(connection, tenantId, companyId, warehouse, itemCode, itemDescription, string.Empty, cancellationToken);
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

    private void EnsureWriteAccess()
    {
        if (!_currentUserContext.IsAuthenticated)
        {
            throw new InvalidOperationException("Debes iniciar sesión para modificar stock.");
        }

        if (_currentUserContext.IsPlatformAdmin ||
            _currentUserContext.Roles.Contains(PlatformRoles.TenantAdmin, StringComparer.OrdinalIgnoreCase))
        {
            return;
        }

        throw new InvalidOperationException("No tienes permisos para registrar ajustes de almacén.");
    }

    private static void NormalizeAndValidateCount(SaveStockCountCommand command)
    {
        command.Warehouse = command.Warehouse.Trim();
        command.Notes = command.Notes.Trim();
        command.Status = string.IsNullOrWhiteSpace(command.Status)
            ? StockCountStatuses.Draft
            : command.Status.Trim();
        command.IsBlindCountRevealed = !command.IsBlindCount || command.IsBlindCountRevealed;

        if (command.CountDate == default)
        {
            command.CountDate = DateTime.Today;
        }

        if (!StockCountStatuses.All.Contains(command.Status, StringComparer.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("El estado del inventario no es válido.");
        }

        if (string.IsNullOrWhiteSpace(command.Warehouse))
        {
            throw new InvalidOperationException("El almacén del inventario es obligatorio.");
        }

        command.Lines = command.Lines
            .Where(line =>
                !string.IsNullOrWhiteSpace(line.ItemDescription) ||
                !string.IsNullOrWhiteSpace(line.ItemCode) ||
                line.ExpectedQuantity != 0m ||
                line.CountedQuantity != 0m)
            .Select((line, index) =>
            {
                line.LineNumber = index + 1;
                line.ItemCode = line.ItemCode.Trim();
                line.ItemDescription = line.ItemDescription.Trim();
                line.Color = line.Color.Trim();
                line.UnitOfMeasure = line.UnitOfMeasure.Trim();
                line.Notes = line.Notes.Trim();
                line.ExpectedQuantity = decimal.Round(line.ExpectedQuantity, 3, MidpointRounding.AwayFromZero);
                line.CountedQuantity = decimal.Round(line.CountedQuantity, 3, MidpointRounding.AwayFromZero);
                line.DifferenceQuantity = decimal.Round(line.CountedQuantity - line.ExpectedQuantity, 3, MidpointRounding.AwayFromZero);
                line.IsDifferenceValidated = line.DifferenceQuantity == 0m || line.IsDifferenceValidated;
                return line;
            })
            .ToList();

        if (command.Lines.Count == 0)
        {
            throw new InvalidOperationException("Añade al menos una línea al inventario.");
        }

        foreach (var line in command.Lines)
        {
            if (string.IsNullOrWhiteSpace(line.ItemDescription))
            {
                throw new InvalidOperationException($"La descripción es obligatoria en la línea {line.LineNumber}.");
            }

            if (line.CountedQuantity < 0m)
            {
                throw new InvalidOperationException($"La cantidad contada no puede ser negativa en la línea {line.LineNumber}.");
            }
        }
    }

    private static void NormalizeAndValidate(SaveStockTransferCommand command)
    {
        command.FromWarehouse = command.FromWarehouse.Trim();
        command.ToWarehouse = command.ToWarehouse.Trim();
        command.Notes = command.Notes.Trim();
        command.Status = string.IsNullOrWhiteSpace(command.Status)
            ? StockTransferStatuses.Draft
            : command.Status.Trim();

        if (command.TransferDate == default)
        {
            command.TransferDate = DateTime.Today;
        }

        if (!StockTransferStatuses.All.Contains(command.Status, StringComparer.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("El estado del traspaso no es válido.");
        }

        if (string.IsNullOrWhiteSpace(command.FromWarehouse))
        {
            throw new InvalidOperationException("El almacén de origen es obligatorio.");
        }

        if (string.IsNullOrWhiteSpace(command.ToWarehouse))
        {
            throw new InvalidOperationException("El almacén de destino es obligatorio.");
        }

        if (string.Equals(command.FromWarehouse, command.ToWarehouse, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("El almacén de origen y destino no pueden ser el mismo.");
        }

        command.Lines = command.Lines
            .Where(line =>
                !string.IsNullOrWhiteSpace(line.ItemDescription) ||
                !string.IsNullOrWhiteSpace(line.ItemCode) ||
                line.Quantity > 0)
            .Select((line, index) =>
            {
                line.LineNumber = index + 1;
                line.ItemCode = line.ItemCode.Trim();
                line.ItemDescription = line.ItemDescription.Trim();
                line.Color = line.Color.Trim();
                line.UnitOfMeasure = line.UnitOfMeasure.Trim();
                line.Notes = line.Notes.Trim();
                line.Quantity = decimal.Round(line.Quantity, 3, MidpointRounding.AwayFromZero);
                return line;
            })
            .ToList();

        if (command.Lines.Count == 0)
        {
            throw new InvalidOperationException("Añade al menos una línea al traspaso.");
        }

        foreach (var line in command.Lines)
        {
            if (string.IsNullOrWhiteSpace(line.ItemDescription))
            {
                throw new InvalidOperationException($"La descripción es obligatoria en la línea {line.LineNumber}.");
            }

            if (line.Quantity <= 0)
            {
                throw new InvalidOperationException($"La cantidad debe ser mayor que cero en la línea {line.LineNumber}.");
            }
        }
    }

    private static void NormalizeAndValidate(CreateStockAdjustmentCommand command)
    {
        command.Warehouse = command.Warehouse.Trim();
        command.ItemCode = command.ItemCode.Trim();
        command.ItemDescription = command.ItemDescription.Trim();
        command.Color = command.Color.Trim();
        command.UnitOfMeasure = command.UnitOfMeasure.Trim();
        command.Notes = command.Notes.Trim();
        command.AdjustmentType = string.IsNullOrWhiteSpace(command.AdjustmentType)
            ? StockMovementTypes.InboundManualAdjustment
            : command.AdjustmentType.Trim();

        if (command.MovementDate == default)
        {
            command.MovementDate = DateTime.Today;
        }

        if (!string.Equals(command.AdjustmentType, StockMovementTypes.InboundManualAdjustment, StringComparison.OrdinalIgnoreCase) &&
            !string.Equals(command.AdjustmentType, StockMovementTypes.OutboundManualAdjustment, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("El tipo de ajuste de stock no es válido.");
        }

        if (string.IsNullOrWhiteSpace(command.ItemDescription))
        {
            throw new InvalidOperationException("La descripción del artículo es obligatoria.");
        }

        if (command.Quantity <= 0)
        {
            throw new InvalidOperationException("La cantidad del ajuste debe ser mayor que cero.");
        }

        command.Quantity = decimal.Round(command.Quantity, 3, MidpointRounding.AwayFromZero);
    }

    private static string BuildSourceDocumentDisplay(string sourceType, int? sourceNumber) => sourceType switch
    {
        "PurchaseReceipt" when sourceNumber.HasValue => $"Albarán {sourceNumber.Value}",
        "SalesShipment" when sourceNumber.HasValue => $"Salida venta {sourceNumber.Value}",
        "StockTransferOutbound" when sourceNumber.HasValue => $"Traspaso {sourceNumber.Value}",
        "StockTransferInbound" when sourceNumber.HasValue => $"Traspaso {sourceNumber.Value}",
        "StockCount" when sourceNumber.HasValue => $"Inventario {sourceNumber.Value}",
        "ManualStockAdjustment" => "Ajuste manual",
        _ when sourceNumber.HasValue => $"{sourceType} {sourceNumber.Value}",
        _ => sourceType
    };

    private static string BuildTransferMovementNotes(string warehouse, string transferNotes, string lineNotes)
    {
        var parts = new List<string> { $"Almacén {warehouse}" };
        if (!string.IsNullOrWhiteSpace(transferNotes))
        {
            parts.Add(transferNotes.Trim());
        }

        if (!string.IsNullOrWhiteSpace(lineNotes))
        {
            parts.Add(lineNotes.Trim());
        }

        return string.Join(" · ", parts);
    }

    private static string BuildCountMovementNotes(string warehouse, StockCountLineDto line, string countNotes)
    {
        var parts = new List<string>
        {
            $"Almacén {warehouse}",
            $"Esperado {line.ExpectedQuantity:0.###}",
            $"Contado {line.CountedQuantity:0.###}",
            $"Diferencia {line.DifferenceQuantity:0.###}"
        };

        if (!string.IsNullOrWhiteSpace(countNotes))
        {
            parts.Add(countNotes.Trim());
        }

        if (!string.IsNullOrWhiteSpace(line.Notes))
        {
            parts.Add(line.Notes.Trim());
        }

        return string.Join(" · ", parts);
    }

    private static string BuildBalanceKey(string warehouse, string itemCode, string itemDescription, string unitOfMeasure) =>
        $"{warehouse}\u001f{itemCode}\u001f{itemDescription}\u001f{unitOfMeasure}";

    private static DateTime? MaxDate(DateTime? left, DateTime? right) =>
        left is null ? right : right is null ? left : (left >= right ? left : right);

    private static IEnumerable<StockBalanceListItemDto> SortStockBalancesForDisplay(
        IEnumerable<StockBalanceListItemDto> source,
        StockBalanceFilter filter)
    {
        var descending = filter.SortDescending;
        var ordered = filter.SortColumn switch
        {
            nameof(StockBalanceListItemDto.ItemCode) => descending
                ? source.OrderByDescending(item => item.ItemCode, StringComparer.OrdinalIgnoreCase)
                : source.OrderBy(item => item.ItemCode, StringComparer.OrdinalIgnoreCase),
            nameof(StockBalanceListItemDto.ItemDescription) => descending
                ? source.OrderByDescending(item => item.ItemDescription, StringComparer.OrdinalIgnoreCase)
                : source.OrderBy(item => item.ItemDescription, StringComparer.OrdinalIgnoreCase),
            nameof(StockBalanceListItemDto.CurrentStock) => descending
                ? source.OrderByDescending(item => item.CurrentStock)
                : source.OrderBy(item => item.CurrentStock),
            nameof(StockBalanceListItemDto.MovementCount) => descending
                ? source.OrderByDescending(item => item.MovementCount)
                : source.OrderBy(item => item.MovementCount),
            nameof(StockBalanceListItemDto.LastMovementDate) => descending
                ? source.OrderByDescending(item => item.LastMovementDate)
                : source.OrderBy(item => item.LastMovementDate),
            _ => descending
                ? source.OrderByDescending(item => item.Warehouse, StringComparer.OrdinalIgnoreCase)
                : source.OrderBy(item => item.Warehouse, StringComparer.OrdinalIgnoreCase)
        };

        return ordered
            .ThenBy(item => item.ItemCode, StringComparer.OrdinalIgnoreCase)
            .ThenBy(item => item.ItemDescription, StringComparer.OrdinalIgnoreCase);
    }

    private static string BuildStockMovementSearchOrderByClause(StockMovementFilter filter)
    {
        var column = filter.SortColumn switch
        {
            nameof(StockMovementListItemDto.MovementDate) => "movement_date",
            nameof(StockMovementListItemDto.MovementType) => "movement_type",
            nameof(StockMovementListItemDto.Warehouse) => "warehouse",
            nameof(StockMovementListItemDto.ItemCode) => "item_code",
            nameof(StockMovementListItemDto.ItemDescription) => "item_description",
            nameof(StockMovementListItemDto.Color) => "color",
            nameof(StockMovementListItemDto.Quantity) => "quantity",
            nameof(StockMovementListItemDto.SupplierName) => "supplier_name",
            nameof(StockMovementListItemDto.SourceDocumentDisplay) => "source_document_number",
            _ => string.Empty
        };

        if (string.IsNullOrWhiteSpace(column))
        {
            return "ORDER BY movement_date DESC, created_utc DESC";
        }

        var direction = filter.SortDescending ? "DESC" : "ASC";
        return $"ORDER BY {column} {direction}, movement_date DESC, created_utc DESC";
    }

    private static string BuildStockTransferSearchOrderByClause(StockTransferFilter filter)
    {
        var column = filter.SortColumn switch
        {
            nameof(StockTransferListItemDto.TransferNumber) => "transfer_number",
            nameof(StockTransferListItemDto.TransferDate) => "transfer_date",
            nameof(StockTransferListItemDto.Status) => "status",
            nameof(StockTransferListItemDto.FromWarehouse) => "from_warehouse",
            nameof(StockTransferListItemDto.ToWarehouse) => "to_warehouse",
            nameof(StockTransferListItemDto.LineCount) => "line_count",
            nameof(StockTransferListItemDto.TotalQuantity) => "total_quantity",
            _ => string.Empty
        };

        if (string.IsNullOrWhiteSpace(column))
        {
            return "ORDER BY transfer_date DESC, transfer_number DESC";
        }

        var direction = filter.SortDescending ? "DESC" : "ASC";
        return $"ORDER BY {column} {direction}, transfer_date DESC, transfer_number DESC";
    }

    private static string BuildStockCountSearchOrderByClause(StockCountFilter filter)
    {
        var column = filter.SortColumn switch
        {
            nameof(StockCountListItemDto.CountNumber) => "count_number",
            nameof(StockCountListItemDto.CountDate) => "count_date",
            nameof(StockCountListItemDto.Status) => "status",
            nameof(StockCountListItemDto.Warehouse) => "warehouse",
            nameof(StockCountListItemDto.LineCount) => "line_count",
            nameof(StockCountListItemDto.DifferenceLineCount) => "difference_line_count",
            nameof(StockCountListItemDto.DifferenceTotalQuantity) => "difference_total_quantity",
            _ => string.Empty
        };

        if (string.IsNullOrWhiteSpace(column))
        {
            return "ORDER BY count_date DESC, count_number DESC";
        }

        var direction = filter.SortDescending ? "DESC" : "ASC";
        return $"ORDER BY {column} {direction}, count_date DESC, count_number DESC";
    }

    private static string BuildStockBalanceSearchOrderByClause(StockBalanceFilter filter)
    {
        var column = filter.SortColumn switch
        {
            nameof(StockBalanceListItemDto.Warehouse) => "warehouse",
            nameof(StockBalanceListItemDto.ItemCode) => "item_code",
            nameof(StockBalanceListItemDto.ItemDescription) => "item_description",
            nameof(StockBalanceListItemDto.CurrentStock) => "current_stock",
            nameof(StockBalanceListItemDto.MovementCount) => "movement_count",
            nameof(StockBalanceListItemDto.LastMovementDate) => "last_movement_date",
            _ => string.Empty
        };

        if (string.IsNullOrWhiteSpace(column))
        {
            return "ORDER BY warehouse ASC, item_description ASC, item_code ASC";
        }

        var direction = filter.SortDescending ? "DESC" : "ASC";
        return $"ORDER BY {column} {direction}, warehouse ASC, item_description ASC, item_code ASC";
    }

    private static string BuildLegacyStockBalanceSearchOrderByClause(StockBalanceFilter filter)
        => BuildStockBalanceSearchOrderByClause(filter);

    private static string NormalizeCatalogScope(string? catalogScope) =>
        catalogScope?.Trim() switch
        {
            var scope when string.Equals(scope, "Hilos", StringComparison.OrdinalIgnoreCase) => "Hilos",
            var scope when string.Equals(scope, "Tejidos", StringComparison.OrdinalIgnoreCase) => "Tejidos",
            var scope when string.Equals(scope, "Models", StringComparison.OrdinalIgnoreCase) => "Models",
            _ => string.Empty
        };

    private static object DbValue(string value) => string.IsNullOrWhiteSpace(value) ? DBNull.Value : value;
}
