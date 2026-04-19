using System.Security.Claims;
using Erp.Application.Auth;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Erp.App.Security;

public static class AuthCookieFactory
{
    public static ClaimsPrincipal CreatePrincipal(AuthenticatedSession session, Guid? activeCompanyId)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, session.UserId.ToString()),
            new(ClaimTypes.Name, session.DisplayName),
            new(ClaimTypes.Email, session.Email),
            new(AppClaimTypes.IsPlatformAdmin, session.IsPlatformAdmin.ToString()),
            new(AppClaimTypes.RequirePasswordChange, session.RequirePasswordChange.ToString())
        };

        if (session.TenantId.HasValue)
        {
            claims.Add(new Claim(AppClaimTypes.TenantId, session.TenantId.Value.ToString()));
        }

        if (activeCompanyId.HasValue)
        {
            claims.Add(new Claim(AppClaimTypes.ActiveCompanyId, activeCompanyId.Value.ToString()));
        }

        claims.AddRange(session.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

        return new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
    }
}
