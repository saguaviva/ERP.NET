using Erp.Application.Auditing;
using Erp.Application.Contexts;
using Erp.Domain.Common;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Support;

namespace Erp.Infrastructure.MySql.Auditing;

public sealed class MySqlAuditLogService : IAuditLogService
{
    private readonly MySqlConnectionFactory _connectionFactory;
    private readonly ICurrentUserContext _currentUserContext;
    private readonly ITenantContext _tenantContext;

    public MySqlAuditLogService(
        MySqlConnectionFactory connectionFactory,
        ICurrentUserContext currentUserContext,
        ITenantContext tenantContext)
    {
        _connectionFactory = connectionFactory;
        _currentUserContext = currentUserContext;
        _tenantContext = tenantContext;
    }

    public async Task WriteAsync(WriteAuditLogCommand command, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured ||
            string.IsNullOrWhiteSpace(command.Action) ||
            string.IsNullOrWhiteSpace(command.EntityName))
        {
            return;
        }

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var insertCommand = connection.CreateCommand();
        insertCommand.CommandText =
            """
            INSERT INTO audit_logs (id, tenant_id, company_id, user_id, action, entity_name, entity_id, details, created_utc)
            VALUES (@id, @tenantId, @companyId, @userId, @action, @entityName, @entityId, @details, @createdUtc);
            """;
        insertCommand.Parameters.AddWithValue("@id", Guid.NewGuid().ToString());
        insertCommand.Parameters.AddWithValue("@tenantId", command.TenantId?.ToString());
        insertCommand.Parameters.AddWithValue("@companyId", command.CompanyId?.ToString());
        insertCommand.Parameters.AddWithValue("@userId", (command.UserId ?? _currentUserContext.UserId)?.ToString());
        insertCommand.Parameters.AddWithValue("@action", command.Action.Trim());
        insertCommand.Parameters.AddWithValue("@entityName", command.EntityName.Trim());
        insertCommand.Parameters.AddWithValue("@entityId", command.EntityId?.Trim() ?? string.Empty);
        insertCommand.Parameters.AddWithValue("@details", command.Details?.Trim() ?? string.Empty);
        insertCommand.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
        await insertCommand.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<AuditLogEntryDto>> GetRecentAsync(
        int limit = 20,
        Guid? tenantId = null,
        Guid? companyId = null,
        string? entityName = null,
        string? entityId = null,
        CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return [];
        }

        var effectiveTenantId = EnsureAuditReadAccess(tenantId);
        var items = new List<AuditLogEntryDto>();

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT
                a.id,
                a.tenant_id,
                a.company_id,
                a.user_id,
                a.action,
                a.entity_name,
                a.entity_id,
                COALESCE(a.details, '') AS details,
                a.created_utc,
                COALESCE(u.display_name, '') AS actor_display_name,
                COALESCE(u.email, '') AS actor_email,
                COALESCE(t.name, '') AS tenant_name,
                COALESCE(c.name, '') AS company_name
            FROM audit_logs a
            LEFT JOIN app_users u ON u.id = a.user_id
            LEFT JOIN tenants t ON t.id = a.tenant_id
            LEFT JOIN companies c ON c.id = a.company_id
            WHERE (@tenantId IS NULL OR a.tenant_id = @tenantId)
              AND (@companyId IS NULL OR a.company_id = @companyId)
              AND (@entityName = '' OR a.entity_name = @entityName)
              AND (@entityId = '' OR a.entity_id = @entityId)
            ORDER BY a.created_utc DESC
            LIMIT @limit;
            """;
        command.Parameters.AddWithValue("@limit", Math.Max(1, limit));
        command.Parameters.AddWithValue("@tenantId", effectiveTenantId?.ToString());
        command.Parameters.AddWithValue("@companyId", companyId?.ToString());
        command.Parameters.AddWithValue("@entityName", entityName?.Trim() ?? string.Empty);
        command.Parameters.AddWithValue("@entityId", entityId?.Trim() ?? string.Empty);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new AuditLogEntryDto
            {
                Id = reader.GetGuid("id"),
                TenantId = reader.GetNullableGuid("tenant_id"),
                CompanyId = reader.GetNullableGuid("company_id"),
                UserId = reader.GetNullableGuid("user_id"),
                Action = reader.GetStringOrEmpty("action"),
                EntityName = reader.GetStringOrEmpty("entity_name"),
                EntityId = reader.GetStringOrEmpty("entity_id"),
                Details = reader.GetStringOrEmpty("details"),
                CreatedUtc = new DateTimeOffset(reader.GetDateTime(reader.GetOrdinal("created_utc")), TimeSpan.Zero),
                ActorDisplayName = reader.GetStringOrEmpty("actor_display_name"),
                ActorEmail = reader.GetStringOrEmpty("actor_email"),
                TenantName = reader.GetStringOrEmpty("tenant_name"),
                CompanyName = reader.GetStringOrEmpty("company_name")
            });
        }

        return items;
    }

    private Guid? EnsureAuditReadAccess(Guid? requestedTenantId)
    {
        if (_currentUserContext.IsPlatformAdmin)
        {
            return requestedTenantId;
        }

        if (!_currentUserContext.IsAuthenticated)
        {
            throw new InvalidOperationException("Debes iniciar sesión para consultar auditoría.");
        }

        if (!_currentUserContext.Roles.Contains(PlatformRoles.TenantAdmin, StringComparer.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("No tienes permisos para consultar auditoría administrativa.");
        }

        if (!_tenantContext.TenantId.HasValue)
        {
            throw new InvalidOperationException("Tu sesión no tiene un tenant activo.");
        }

        if (requestedTenantId.HasValue && requestedTenantId.Value != _tenantContext.TenantId.Value)
        {
            throw new InvalidOperationException("No puedes consultar auditoría de otro tenant.");
        }

        return _tenantContext.TenantId.Value;
    }
}
