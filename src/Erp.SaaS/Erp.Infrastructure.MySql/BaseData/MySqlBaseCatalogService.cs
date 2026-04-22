using Erp.Application.Auditing;
using Erp.Application.BaseData;
using Erp.Application.Companies;
using Erp.Application.Contexts;
using Erp.Domain.Common;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Support;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.BaseData;

public sealed class MySqlBaseCatalogService : IBaseCatalogQueries, IBaseCatalogService
{
    private readonly MySqlConnectionFactory _connectionFactory;
    private readonly IAuditLogService _auditLogService;
    private readonly ICompanyAccessService _companyAccessService;
    private readonly ICurrentUserContext _currentUserContext;
    private readonly ITenantContext _tenantContext;
    private readonly IActiveCompanyContext _activeCompanyContext;

    public MySqlBaseCatalogService(
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

    public async Task<BaseCatalogSearchResultDto> SearchAsync(Guid tenantId, Guid companyId, string catalogKey, BaseCatalogFilter filter, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return new BaseCatalogSearchResultDto();
        }

        ValidateCatalogKey(catalogKey);
        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);

        var pageSize = Math.Clamp(filter.PageSize, 10, 200);
        var page = Math.Max(filter.Page, 1);
        var offset = (page - 1) * pageSize;
        var search = filter.Search?.Trim() ?? string.Empty;
        var likeSearch = $"%{search}%";

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var countCommand = connection.CreateCommand();
        countCommand.CommandText =
            """
            SELECT COUNT(*)
            FROM base_catalog_items
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND catalog_key = @catalogKey
              AND COALESCE(is_deleted, 0) = 0
              AND (@includeInactive = 1 OR is_active = 1)
              AND (
                    @search = ''
                    OR code LIKE @likeSearch
                    OR name LIKE @likeSearch
                    OR description LIKE @likeSearch
                    OR reference_value LIKE @likeSearch
                    OR secondary_reference_value LIKE @likeSearch
                    OR notes LIKE @likeSearch
                  );
            """;
        countCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        countCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
        countCommand.Parameters.AddWithValue("@catalogKey", catalogKey);
        countCommand.Parameters.AddWithValue("@includeInactive", filter.IncludeInactive);
        countCommand.Parameters.AddWithValue("@search", search);
        countCommand.Parameters.AddWithValue("@likeSearch", likeSearch);

        var totalCount = Convert.ToInt32(await countCommand.ExecuteScalarAsync(cancellationToken));
        if (totalCount == 0)
        {
            return new BaseCatalogSearchResultDto();
        }

        var items = new List<BaseCatalogListItemDto>();
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT catalog_key,
                   code,
                   name,
                   description,
                   reference_value,
                   secondary_reference_value,
                   numeric_value,
                   secondary_numeric_value,
                   is_active,
                   origin
            FROM base_catalog_items
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND catalog_key = @catalogKey
              AND COALESCE(is_deleted, 0) = 0
              AND (@includeInactive = 1 OR is_active = 1)
              AND (
                    @search = ''
                    OR code LIKE @likeSearch
                    OR name LIKE @likeSearch
                    OR description LIKE @likeSearch
                    OR reference_value LIKE @likeSearch
                    OR secondary_reference_value LIKE @likeSearch
                    OR notes LIKE @likeSearch
                  )
            ORDER BY is_active DESC, name ASC, code ASC
            LIMIT @limit OFFSET @offset;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@catalogKey", catalogKey);
        command.Parameters.AddWithValue("@includeInactive", filter.IncludeInactive);
        command.Parameters.AddWithValue("@search", search);
        command.Parameters.AddWithValue("@likeSearch", likeSearch);
        command.Parameters.AddWithValue("@limit", pageSize);
        command.Parameters.AddWithValue("@offset", offset);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new BaseCatalogListItemDto
            {
                CatalogKey = reader.GetString("catalog_key"),
                Code = reader.GetString("code"),
                Name = reader.GetStringOrEmpty("name"),
                Description = reader.GetStringOrEmpty("description"),
                Reference = reader.GetStringOrEmpty("reference_value"),
                SecondaryReference = reader.GetStringOrEmpty("secondary_reference_value"),
                NumericValue = reader.IsDBNull(reader.GetOrdinal("numeric_value")) ? null : reader.GetDecimal(reader.GetOrdinal("numeric_value")),
                SecondaryNumericValue = reader.IsDBNull(reader.GetOrdinal("secondary_numeric_value")) ? null : reader.GetDecimal(reader.GetOrdinal("secondary_numeric_value")),
                IsActive = reader.GetBoolean(reader.GetOrdinal("is_active")),
                Origin = reader.GetStringOrEmpty("origin")
            });
        }

        return new BaseCatalogSearchResultDto
        {
            Items = items,
            TotalCount = totalCount
        };
    }

    public async Task<BaseCatalogDetailDto?> GetByCodeAsync(Guid tenantId, Guid companyId, string catalogKey, string code, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return null;
        }

        ValidateCatalogKey(catalogKey);
        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT catalog_key, code, name, description, reference_value, secondary_reference_value, numeric_value, secondary_numeric_value, notes, is_active, origin
            FROM base_catalog_items
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND catalog_key = @catalogKey
              AND code = @code
              AND COALESCE(is_deleted, 0) = 0
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@catalogKey", catalogKey);
        command.Parameters.AddWithValue("@code", code);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        return new BaseCatalogDetailDto
        {
            CatalogKey = reader.GetString("catalog_key"),
            Code = reader.GetString("code"),
            Name = reader.GetStringOrEmpty("name"),
            Description = reader.GetStringOrEmpty("description"),
            Reference = reader.GetStringOrEmpty("reference_value"),
            SecondaryReference = reader.GetStringOrEmpty("secondary_reference_value"),
            NumericValue = reader.IsDBNull(reader.GetOrdinal("numeric_value")) ? null : reader.GetDecimal(reader.GetOrdinal("numeric_value")),
            SecondaryNumericValue = reader.IsDBNull(reader.GetOrdinal("secondary_numeric_value")) ? null : reader.GetDecimal(reader.GetOrdinal("secondary_numeric_value")),
            Notes = reader.GetStringOrEmpty("notes"),
            IsActive = reader.GetBoolean(reader.GetOrdinal("is_active")),
            Origin = reader.GetStringOrEmpty("origin")
        };
    }

    public async Task<string> SaveAsync(SaveBaseCatalogItemCommand command, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return string.Empty;
        }

        Validate(command);
        await EnsureCompanyAccessAsync(command.TenantId, command.CompanyId, cancellationToken);
        EnsureTenantWriteAccess();

        var previous = !string.IsNullOrWhiteSpace(command.OriginalCode)
            ? await GetByCodeAsync(command.TenantId, command.CompanyId, command.CatalogKey, command.OriginalCode, cancellationToken)
            : null;

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var commandSql = connection.CreateCommand();
        commandSql.CommandText = previous is null
            ? """
              INSERT INTO base_catalog_items (
                  tenant_id,
                  company_id,
                  catalog_key,
                  code,
                  name,
                  description,
                  reference_value,
                  secondary_reference_value,
                  numeric_value,
                  secondary_numeric_value,
                  notes,
                  is_active,
                  origin,
                  is_deleted,
                  synced_utc,
                  created_utc,
                  updated_utc)
              VALUES (
                  @tenantId,
                  @companyId,
                  @catalogKey,
                  @code,
                  @name,
                  @description,
                  @referenceValue,
                  @secondaryReferenceValue,
                  @numericValue,
                  @secondaryNumericValue,
                  @notes,
                  @isActive,
                  'local',
                  0,
                  NULL,
                  @nowUtc,
                  @nowUtc);
              """
            : """
              UPDATE base_catalog_items
              SET code = @code,
                  name = @name,
                  description = @description,
                  reference_value = @referenceValue,
                  secondary_reference_value = @secondaryReferenceValue,
                  numeric_value = @numericValue,
                  secondary_numeric_value = @secondaryNumericValue,
                  notes = @notes,
                  is_active = @isActive,
                  origin = 'local',
                  is_deleted = 0,
                  synced_utc = NULL,
                  updated_utc = @nowUtc
              WHERE tenant_id = @tenantId
                AND company_id = @companyId
                AND catalog_key = @catalogKey
                AND code = @originalCode;
              """;
        commandSql.Parameters.AddWithValue("@tenantId", command.TenantId.ToString());
        commandSql.Parameters.AddWithValue("@companyId", command.CompanyId.ToString());
        commandSql.Parameters.AddWithValue("@catalogKey", command.CatalogKey);
        commandSql.Parameters.AddWithValue("@code", command.Code);
        commandSql.Parameters.AddWithValue("@name", command.Name);
        commandSql.Parameters.AddWithValue("@description", DbValue(command.Description));
        commandSql.Parameters.AddWithValue("@referenceValue", DbValue(command.Reference));
        commandSql.Parameters.AddWithValue("@secondaryReferenceValue", DbValue(command.SecondaryReference));
        commandSql.Parameters.AddWithValue("@numericValue", command.NumericValue.HasValue ? command.NumericValue.Value : DBNull.Value);
        commandSql.Parameters.AddWithValue("@secondaryNumericValue", command.SecondaryNumericValue.HasValue ? command.SecondaryNumericValue.Value : DBNull.Value);
        commandSql.Parameters.AddWithValue("@notes", DbValue(command.Notes));
        commandSql.Parameters.AddWithValue("@isActive", command.IsActive);
        commandSql.Parameters.AddWithValue("@nowUtc", DateTime.UtcNow);
        if (previous is not null)
        {
            commandSql.Parameters.AddWithValue("@originalCode", command.OriginalCode);
        }

        await commandSql.ExecuteNonQueryAsync(cancellationToken);

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = command.TenantId,
            CompanyId = command.CompanyId,
            UserId = _currentUserContext.UserId,
            Action = previous is null ? "BaseCatalogItemCreated" : "BaseCatalogItemUpdated",
            EntityName = command.CatalogKey,
            EntityId = command.Code,
            Details = $"{command.CatalogKey} · {command.Code} · {command.Name}"
        }, cancellationToken);

        return command.Code;
    }

    public async Task DeleteAsync(Guid tenantId, Guid companyId, string catalogKey, string code, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return;
        }

        ValidateCatalogKey(catalogKey);
        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);
        EnsureTenantWriteAccess();

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            UPDATE base_catalog_items
            SET origin = 'local',
                is_deleted = 1,
                synced_utc = NULL,
                updated_utc = @nowUtc
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND catalog_key = @catalogKey
              AND code = @code;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@catalogKey", catalogKey);
        command.Parameters.AddWithValue("@code", code);
        command.Parameters.AddWithValue("@nowUtc", DateTime.UtcNow);
        var affected = await command.ExecuteNonQueryAsync(cancellationToken);
        if (affected == 0)
        {
            throw new InvalidOperationException("No se ha encontrado el registro a eliminar.");
        }

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = tenantId,
            CompanyId = companyId,
            UserId = _currentUserContext.UserId,
            Action = "BaseCatalogItemDeleted",
            EntityName = catalogKey,
            EntityId = code,
            Details = $"{catalogKey} · {code}"
        }, cancellationToken);
    }

    private static void Validate(SaveBaseCatalogItemCommand command)
    {
        command.CatalogKey = command.CatalogKey.Trim().ToLowerInvariant();
        command.OriginalCode = command.OriginalCode?.Trim();
        command.Code = command.Code.Trim().ToUpperInvariant();
        command.Name = command.Name.Trim();
        command.Description = command.Description.Trim();
        command.Reference = command.Reference.Trim();
        command.SecondaryReference = command.SecondaryReference.Trim();
        command.Notes = command.Notes.Trim();

        ValidateCatalogKey(command.CatalogKey);

        if (string.IsNullOrWhiteSpace(command.Code))
        {
            throw new InvalidOperationException("Debes indicar un código.");
        }

        if (string.IsNullOrWhiteSpace(command.Name))
        {
            throw new InvalidOperationException("Debes indicar un nombre.");
        }
    }

    private static void ValidateCatalogKey(string catalogKey)
    {
        if (!BaseCatalogKeys.All.Contains(catalogKey, StringComparer.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("El catálogo solicitado no es válido.");
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

        var allowedCompanies = await _companyAccessService.GetAllowedCompaniesAsync(_currentUserContext.UserId.Value, tenantId, cancellationToken);
        if (!allowedCompanies.Any(company => company.CompanyId == companyId))
        {
            throw new InvalidOperationException("No tienes acceso a la empresa activa.");
        }
    }

    private static object DbValue(string value) => string.IsNullOrWhiteSpace(value) ? DBNull.Value : value;
}
