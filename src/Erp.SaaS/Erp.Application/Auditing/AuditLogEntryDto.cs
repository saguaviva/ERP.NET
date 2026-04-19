namespace Erp.Application.Auditing;

public sealed class AuditLogEntryDto
{
    public Guid Id { get; init; }
    public Guid? TenantId { get; init; }
    public Guid? CompanyId { get; init; }
    public Guid? UserId { get; init; }
    public string Action { get; init; } = string.Empty;
    public string EntityName { get; init; } = string.Empty;
    public string EntityId { get; init; } = string.Empty;
    public string Details { get; init; } = string.Empty;
    public DateTimeOffset CreatedUtc { get; init; }
    public string ActorDisplayName { get; init; } = string.Empty;
    public string ActorEmail { get; init; } = string.Empty;
    public string TenantName { get; init; } = string.Empty;
    public string CompanyName { get; init; } = string.Empty;
}
