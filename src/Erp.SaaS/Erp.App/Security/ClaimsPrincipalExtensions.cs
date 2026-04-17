using System.Security.Claims;

namespace Erp.App.Security;

public static class ClaimsPrincipalExtensions
{
    public static Guid? GetUserId(this ClaimsPrincipal user)
    {
        var value = user.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.TryParse(value, out var result) ? result : null;
    }

    public static Guid? GetTenantId(this ClaimsPrincipal user)
    {
        var value = user.FindFirstValue(AppClaimTypes.TenantId);
        return Guid.TryParse(value, out var result) ? result : null;
    }

    public static Guid? GetActiveCompanyId(this ClaimsPrincipal user)
    {
        var value = user.FindFirstValue(AppClaimTypes.ActiveCompanyId);
        return Guid.TryParse(value, out var result) ? result : null;
    }

    public static bool IsPlatformAdmin(this ClaimsPrincipal user) =>
        string.Equals(user.FindFirstValue(AppClaimTypes.IsPlatformAdmin), bool.TrueString, StringComparison.OrdinalIgnoreCase);
}
