namespace Erp.Domain.Common;

public static class PlatformRoles
{
    public const string PlatformAdmin = "PlatformAdmin";
    public const string TenantAdmin = "TenantAdmin";
    public const string TenantReader = "TenantReader";

    public static readonly string[] All =
    [
        PlatformAdmin,
        TenantAdmin,
        TenantReader
    ];
}
