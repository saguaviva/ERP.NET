namespace Erp.Application.Companies;

public sealed class SwitchActiveCompanyCommand
{
    public Guid TenantId { get; set; }
    public Guid CompanyId { get; set; }
}
