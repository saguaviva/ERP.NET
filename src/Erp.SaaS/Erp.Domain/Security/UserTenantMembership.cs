namespace Erp.Domain.Security;

public sealed class UserTenantMembership
{
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public DateTimeOffset CreatedUtc { get; set; }
}
