namespace Erp.Application.Tenants;

public sealed class AssignUserRolesCommand
{
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public IReadOnlyCollection<string> Roles { get; set; } = [];
}
