using Erp.Application.Companies;

namespace Erp.Application.Auth;

public sealed class AuthenticatedSession
{
    public Guid UserId { get; init; }
    public string Email { get; init; } = string.Empty;
    public string DisplayName { get; init; } = string.Empty;
    public Guid? TenantId { get; init; }
    public bool IsPlatformAdmin { get; init; }
    public IReadOnlyCollection<string> Roles { get; init; } = [];
    public IReadOnlyCollection<AllowedCompanyDto> AllowedCompanies { get; init; } = [];
}
