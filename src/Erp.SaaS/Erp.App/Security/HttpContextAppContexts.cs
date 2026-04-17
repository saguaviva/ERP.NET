using System.Security.Claims;
using Erp.Application.Contexts;

namespace Erp.App.Security;

public sealed class HttpContextCurrentUserContext : ICurrentUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextCurrentUserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User ?? new ClaimsPrincipal();

    public Guid? UserId => User.GetUserId();
    public string Email => User.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
    public string DisplayName => User.Identity?.Name ?? string.Empty;
    public bool IsAuthenticated => User.Identity?.IsAuthenticated ?? false;
    public bool IsPlatformAdmin => User.IsPlatformAdmin();
    public IReadOnlyCollection<string> Roles => User.FindAll(ClaimTypes.Role).Select(claim => claim.Value).ToArray();
}

public sealed class HttpContextTenantContext : ITenantContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextTenantContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? TenantId => _httpContextAccessor.HttpContext?.User.GetTenantId();
}

public sealed class HttpContextActiveCompanyContext : IActiveCompanyContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextActiveCompanyContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? CompanyId => _httpContextAccessor.HttpContext?.User.GetActiveCompanyId();
}
