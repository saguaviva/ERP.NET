namespace Erp.Application.Tenants;

public sealed class UpdateCompanyLegacyCenterCommand
{
    public Guid CompanyId { get; set; }
    public Guid TenantId { get; set; }
    public string LegacyCenterCode { get; set; } = string.Empty;
    public bool ForceChange { get; set; }
}
