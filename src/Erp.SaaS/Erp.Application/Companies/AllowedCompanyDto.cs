namespace Erp.Application.Companies;

public sealed class AllowedCompanyDto
{
    public Guid CompanyId { get; init; }
    public Guid TenantId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Slug { get; init; } = string.Empty;
    public string LegacyCenterCode { get; init; } = string.Empty;
}
