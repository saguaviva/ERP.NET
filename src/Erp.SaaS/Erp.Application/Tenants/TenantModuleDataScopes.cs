namespace Erp.Application.Tenants;

public static class TenantModuleDataScopes
{
    public const string Company = "Company";
    public const string TenantShared = "TenantShared";

    public static readonly IReadOnlyCollection<string> All = [Company, TenantShared];
}
