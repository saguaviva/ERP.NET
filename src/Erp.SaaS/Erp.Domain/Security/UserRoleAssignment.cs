namespace Erp.Domain.Security;

public sealed class UserRoleAssignment
{
    public Guid UserId { get; set; }
    public Guid? TenantId { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public DateTimeOffset CreatedUtc { get; set; }
}
