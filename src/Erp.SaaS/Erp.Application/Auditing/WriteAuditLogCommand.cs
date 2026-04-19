namespace Erp.Application.Auditing;

public sealed class WriteAuditLogCommand
{
    public Guid? TenantId { get; set; }
    public Guid? CompanyId { get; set; }
    public Guid? UserId { get; set; }
    public string Action { get; set; } = string.Empty;
    public string EntityName { get; set; } = string.Empty;
    public string EntityId { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
}
