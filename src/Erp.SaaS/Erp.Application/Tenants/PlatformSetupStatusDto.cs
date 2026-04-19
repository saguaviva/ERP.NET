namespace Erp.Application.Tenants;

public sealed class PlatformSetupStatusDto
{
    public bool IsDatabaseConfigured { get; init; }
    public bool HasUsers { get; init; }
    public bool HasTenants { get; init; }
    public bool HasCompanies { get; init; }
    public bool HasPlatformAdmin { get; init; }
    public bool CanRunInitialSetup { get; init; }
}
