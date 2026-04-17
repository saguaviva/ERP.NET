namespace Erp.Application.Tenants;

public sealed class UserSummaryDto
{
    public Guid Id { get; init; }
    public Guid? TenantId { get; init; }
    public string Email { get; init; } = string.Empty;
    public string DisplayName { get; init; } = string.Empty;
    public bool IsActive { get; init; }
    public IReadOnlyCollection<string> Roles { get; set; } = [];
    public IReadOnlyCollection<Guid> CompanyIds { get; set; } = [];
}
