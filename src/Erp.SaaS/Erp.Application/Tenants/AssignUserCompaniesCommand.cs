namespace Erp.Application.Tenants;

public sealed class AssignUserCompaniesCommand
{
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public IReadOnlyCollection<Guid> CompanyIds { get; set; } = [];
}
