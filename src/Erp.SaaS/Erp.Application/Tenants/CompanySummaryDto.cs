namespace Erp.Application.Tenants;

public sealed class CompanySummaryDto
{
    public Guid Id { get; init; }
    public Guid TenantId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Slug { get; init; } = string.Empty;
    public string LegacyCenterCode { get; init; } = string.Empty;
    public bool IsActive { get; init; }
}
