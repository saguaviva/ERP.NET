namespace Erp.Application.Tenants;

public sealed class InitialCompanyInput
{
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string LegacyCenterCode { get; set; } = string.Empty;
}
