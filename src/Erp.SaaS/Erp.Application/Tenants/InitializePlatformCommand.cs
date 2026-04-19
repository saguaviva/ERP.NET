namespace Erp.Application.Tenants;

public sealed class InitializePlatformCommand
{
    public string TenantName { get; set; } = string.Empty;
    public string TenantSlug { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string CompanySlug { get; set; } = string.Empty;
    public string LegacyCenterCode { get; set; } = string.Empty;
    public IReadOnlyCollection<InitialCompanyInput> InitialCompanies { get; set; } = [];
    public string AdminDisplayName { get; set; } = string.Empty;
    public string AdminEmail { get; set; } = string.Empty;
    public string AdminPassword { get; set; } = string.Empty;
}
