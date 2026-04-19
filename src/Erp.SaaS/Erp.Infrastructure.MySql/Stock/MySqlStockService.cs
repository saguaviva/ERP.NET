using Erp.Application.Auditing;
using Erp.Application.Companies;
using Erp.Application.Contexts;
using Erp.Application.LegacySync;
using Erp.Application.Stock;
using Erp.Domain.Common;
using Erp.Infrastructure.MySql.Database;
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
                FROM inventory_movements
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND (
                        @warehouse = ''
                        OR COALESCE(warehouse, '') = @warehouse
                      )
                  AND (
                        @search = ''
                        OR COALESCE(item_code, '') LIKE @likeSearch
                        OR item_description LIKE @likeSearch
                        OR COALESCE(supplier_name, '') LIKE @likeSearch
                        OR COALESCE(supplier_reference, '') LIKE @likeSearch
                        OR COALESCE(vehicle_plate, '') LIKE @likeSearch
                        OR CAST(source_document_number AS CHAR) LIKE @likeSearch
                      );
                """;
            countCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            countCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
            countCommand.Parameters.AddWithValue("@warehouse", warehouse);
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
                        @warehouse = ''
                        OR COALESCE(warehouse, '') = @warehouse
                      )
                  AND (
                        @search = ''
                        OR COALESCE(item_code, '') LIKE @likeSearch
                        OR item_description LIKE @likeSearch
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
            command.Parameters.AddWithValue("@warehouse", warehouse);
            command.Parameters.AddWithValue("@search", search);
            command.Parameters.AddWithValue("@likeSearch", likeSearch);
            command.Parameters.AddWithValue("@pageSize", pageSize);
            command.Parameters.AddWithValue("@offset", offset);

            var items = await ReadMovementListAsync(command, cancellationToken);
            return new StockMovementSearchResultDto
            {
                Items = items,
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
        var likeSearch = $"%{search}%";
        var pageSize = Math.Clamp(filter.PageSize, 10, 200);
        var page = Math.Max(filter.Page, 1);
        var offset = (page - 1) * pageSize;

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var legacySyncInfo = await LoadLegacySyncInfoAsync(connection, tenantId, companyId, cancellationToken);
        if (legacySyncInfo.IsActive)
        {
            return await SearchLegacyBalancesAsync(connection, tenantId, companyId, filter, search, warehouse, likeSearch, pageSize, offset, cancellationToken);
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
        await EnsureLegacyStockWriteAllowedAsync(connection, command.TenantId, command.CompanyId, cancellationToken);
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
                cancellationToken);

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
            Details = $"Tipo={command.AdjustmentType}; Almacen={command.Warehouse}; Articulo={command.ItemCode}; Descripcion={command.ItemDescription}; Cantidad={command.Quantity:0.###}; Unidad={command.UnitOfMeasure}{(string.IsNullOrWhiteSpace(command.Notes) ? string.Empty : $"; Notas={command.Notes}")}"
        }, cancellationToken);

        return movementId;
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
        CancellationToken cancellationToken)
    {
        await using var countCommand = connection.CreateCommand();
        countCommand.CommandText =
            """
            SELECT COUNT(*)
            FROM legacy_stock_balances
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
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
              AND COALESCE(current_stock, 0) <> 0;
            """;
        countCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        countCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
        countCommand.Parameters.AddWithValue("@warehouse", warehouse);
        countCommand.Parameters.AddWithValue("@search", search);
        countCommand.Parameters.AddWithValue("@likeSearch", likeSearch);

        var totalCount = Convert.ToInt32(await countCommand.ExecuteScalarAsync(cancellationToken));
        if (totalCount == 0)
        {
            return new StockBalanceSearchResultDto();
        }

        await using var command = connection.CreateCommand();
        var orderBy = BuildLegacyStockBalanceSearchOrderByClause(filter);
        command.CommandText =
            $"""
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
                    @warehouse = ''
                    OR COALESCE(warehouse, '') = @warehouse
                  )
              AND (
                    @search = ''
                    OR COALESCE(item_code, '') LIKE @likeSearch
                    OR item_description LIKE @likeSearch
                    OR COALESCE(warehouse, '') LIKE @likeSearch
                  )
              AND COALESCE(current_stock, 0) <> 0
            {orderBy}
            LIMIT @pageSize OFFSET @offset;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
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
                MovementCount = reader.GetInt32OrDefault("movement_count"),
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

    private static async Task EnsureLegacyStockWriteAllowedAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        CancellationToken cancellationToken)
    {
        var syncInfo = await LoadLegacySyncInfoAsync(connection, tenantId, companyId, cancellationToken);
        if (syncInfo.IsActive)
        {
            throw new InvalidOperationException("Almacén / Stock está en convivencia con legacy para esta empresa. Mientras el módulo esté sincronizado, los ajustes manuales quedan en solo lectura.");
        }
    }

    private static void NormalizeAndValidate(CreateStockAdjustmentCommand command)
    {
        command.Warehouse = command.Warehouse.Trim();
        command.ItemCode = command.ItemCode.Trim();
        command.ItemDescription = command.ItemDescription.Trim();
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
        "ManualStockAdjustment" => "Ajuste manual",
        _ when sourceNumber.HasValue => $"{sourceType} {sourceNumber.Value}",
        _ => sourceType
    };

    private static string BuildStockMovementSearchOrderByClause(StockMovementFilter filter)
    {
        var column = filter.SortColumn switch
        {
            nameof(StockMovementListItemDto.MovementDate) => "movement_date",
            nameof(StockMovementListItemDto.MovementType) => "movement_type",
            nameof(StockMovementListItemDto.Warehouse) => "warehouse",
            nameof(StockMovementListItemDto.ItemCode) => "item_code",
            nameof(StockMovementListItemDto.ItemDescription) => "item_description",
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

    private static object DbValue(string value) => string.IsNullOrWhiteSpace(value) ? DBNull.Value : value;
}
