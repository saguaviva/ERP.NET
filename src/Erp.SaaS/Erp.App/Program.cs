using System.Security.Claims;
using Erp.App.Components;
using Erp.App.Security;
using Erp.Application.Auth;
using Erp.Application.Auditing;
using Erp.Application.Companies;
using Erp.Application.Contexts;
using Erp.Application.DemoAccess;
using Erp.Infrastructure.MySql;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddHttpContextAccessor();
builder.Services.Configure<PreviewAccessOptions>(builder.Configuration.GetSection(PreviewAccessOptions.SectionName));
builder.Services.AddSingleton<PreviewAccessCookieProtector>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/acceso-denegado";
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromHours(10);
    });
builder.Services.AddAuthorization();
builder.Services.AddScoped<ICurrentUserContext, HttpContextCurrentUserContext>();
builder.Services.AddScoped<ITenantContext, HttpContextTenantContext>();
builder.Services.AddScoped<IActiveCompanyContext, HttpContextActiveCompanyContext>();
builder.Services.AddMySqlInfrastructure(builder.Configuration);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.Use(async (context, next) =>
{
    var options = context.RequestServices.GetRequiredService<IOptionsMonitor<PreviewAccessOptions>>().CurrentValue;
    if (!options.Enabled || IsPreviewExemptPath(context.Request.Path))
    {
        await next();
        return;
    }

    if (context.Request.Cookies.TryGetValue(options.CookieName, out var cookieValue))
    {
        var protector = context.RequestServices.GetRequiredService<PreviewAccessCookieProtector>();
        if (protector.TryValidate(cookieValue, out _))
        {
            await next();
            return;
        }
    }

    var returnUrl = BuildReturnUrl(context.Request);
    context.Response.Redirect($"/preview-access?returnUrl={Uri.EscapeDataString(returnUrl)}");
});
app.UseAuthentication();
app.Use(async (context, next) =>
{
    if (context.User.Identity?.IsAuthenticated == true &&
        context.User.RequiresPasswordChange())
    {
        var path = context.Request.Path;
        var isAllowedPath =
            path.StartsWithSegments("/cuenta/cambiar-password") ||
            path.StartsWithSegments("/account/change-password") ||
            path.StartsWithSegments("/account/logout") ||
            path.StartsWithSegments("/_framework") ||
            path.StartsWithSegments("/_blazor") ||
            path.StartsWithSegments("/favicon") ||
            Path.HasExtension(path.Value);

        if (!isAllowedPath)
        {
            var returnUrl = string.IsNullOrWhiteSpace(path.Value) ? "/" : path.Value;
            if (context.Request.QueryString.HasValue)
            {
                returnUrl += context.Request.QueryString.Value;
            }

            context.Response.Redirect($"/cuenta/cambiar-password?returnUrl={Uri.EscapeDataString(returnUrl)}");
            return;
        }
    }

    await next();
});
app.UseAuthorization();
app.UseAntiforgery();

app.MapPost("/preview-access/unlock", async Task<IResult> (
    HttpContext httpContext,
    [FromForm] PreviewAccessForm form,
    IOptionsMonitor<PreviewAccessOptions> optionsMonitor,
    PreviewAccessCookieProtector protector,
    IDemoAccessService demoAccessService,
    CancellationToken cancellationToken) =>
{
    var options = optionsMonitor.CurrentValue;
    if (!options.Enabled)
    {
        return Results.Redirect("/login");
    }

    var normalizedAllowedEmails = options.GetNormalizedAllowedEmails();
    var normalizedEmail = form.Email.Trim();
    var returnUrl = NormalizeReturnUrl(form.ReturnUrl);
    var isConfigEmailAllowed = !string.IsNullOrWhiteSpace(normalizedEmail) &&
        normalizedAllowedEmails.Contains(normalizedEmail);
    var isDatabaseInviteAllowed = options.RequireApprovedEmail &&
        !string.IsNullOrWhiteSpace(normalizedEmail) &&
        await demoAccessService.IsEmailAllowedAsync(normalizedEmail, cancellationToken);

    if (options.RequiresEmail &&
        (string.IsNullOrWhiteSpace(normalizedEmail) || (!isConfigEmailAllowed && !isDatabaseInviteAllowed)))
    {
        return Results.Redirect($"/preview-access?error=email&returnUrl={Uri.EscapeDataString(returnUrl)}");
    }

    if (options.RequiresPassword &&
        !string.Equals(form.Password, options.SharedPassword, StringComparison.Ordinal))
    {
        return Results.Redirect($"/preview-access?error=password&returnUrl={Uri.EscapeDataString(returnUrl)}");
    }

    if (!options.RequiresEmail && !options.RequiresPassword)
    {
        return Results.Redirect($"/preview-access?error=config&returnUrl={Uri.EscapeDataString(returnUrl)}");
    }

    httpContext.Response.Cookies.Append(
        options.CookieName,
        protector.Protect(normalizedEmail),
        new CookieOptions
        {
            HttpOnly = true,
            IsEssential = true,
            SameSite = SameSiteMode.Lax,
            Secure = httpContext.Request.IsHttps,
            Expires = DateTimeOffset.UtcNow.AddDays(Math.Max(1, options.CookieDays))
        });

    return Results.Redirect(returnUrl);
});

app.MapPost("/preview-access/logout", (HttpContext httpContext, IOptionsMonitor<PreviewAccessOptions> optionsMonitor) =>
{
    var options = optionsMonitor.CurrentValue;
    httpContext.Response.Cookies.Delete(options.CookieName);
    return Results.Redirect("/preview-access");
});

app.MapPost("/account/login", async Task<IResult> (
    HttpContext httpContext,
    [FromForm] LoginForm form,
    IAuthService authService,
    CancellationToken cancellationToken) =>
{
    var session = await authService.AuthenticateAsync(new LoginRequest
    {
        Email = form.Email,
        Password = form.Password
    }, cancellationToken);

    if (session is null)
    {
        return Results.Redirect($"/login?error=1&returnUrl={Uri.EscapeDataString(form.ReturnUrl ?? "/")}");
    }

    var activeCompanyId = session.AllowedCompanies.FirstOrDefault()?.CompanyId;
    var principal = AuthCookieFactory.CreatePrincipal(session, activeCompanyId);

    await httpContext.SignInAsync(
        CookieAuthenticationDefaults.AuthenticationScheme,
        principal,
        new AuthenticationProperties
        {
            IsPersistent = form.RememberMeValue,
            AllowRefresh = true
        });

    var returnUrl = string.IsNullOrWhiteSpace(form.ReturnUrl) ? "/" : form.ReturnUrl;
    if (session.RequirePasswordChange)
    {
        return Results.Redirect($"/cuenta/cambiar-password?returnUrl={Uri.EscapeDataString(returnUrl)}");
    }

    return Results.Redirect(returnUrl);
});

app.MapPost("/account/logout", async Task<IResult> (HttpContext httpContext) =>
{
    await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return Results.Redirect("/login");
});

app.MapPost("/account/switch-company", async Task<IResult> (
    HttpContext httpContext,
    [FromForm] SwitchCompanyForm form,
    ICompanyAccessService companyAccessService,
    IAuditLogService auditLogService,
    CancellationToken cancellationToken) =>
{
    if (!(httpContext.User.Identity?.IsAuthenticated ?? false))
    {
        return Results.Redirect("/login");
    }

    var userId = httpContext.User.GetUserId();
    var tenantId = httpContext.User.GetTenantId();
    if (!userId.HasValue || !tenantId.HasValue)
    {
        return Results.Redirect("/");
    }

    var allowedCompanies = await companyAccessService.GetAllowedCompaniesAsync(userId.Value, tenantId.Value, cancellationToken);
    var selectedCompany = allowedCompanies.FirstOrDefault(company => company.CompanyId == form.CompanyId);
    if (selectedCompany is null)
    {
        return Results.Redirect("/");
    }

    var roles = httpContext.User.FindAll(ClaimTypes.Role).Select(claim => claim.Value).Distinct(StringComparer.OrdinalIgnoreCase).ToArray();
    var session = new AuthenticatedSession
    {
        UserId = userId.Value,
        Email = httpContext.User.FindFirstValue(ClaimTypes.Email) ?? string.Empty,
        DisplayName = httpContext.User.Identity?.Name ?? string.Empty,
        TenantId = tenantId,
        IsPlatformAdmin = httpContext.User.IsPlatformAdmin(),
        Roles = roles,
        AllowedCompanies = allowedCompanies
    };

    await httpContext.SignInAsync(
        CookieAuthenticationDefaults.AuthenticationScheme,
        AuthCookieFactory.CreatePrincipal(session, selectedCompany.CompanyId));

    await auditLogService.WriteAsync(new WriteAuditLogCommand
    {
        TenantId = tenantId,
        CompanyId = selectedCompany.CompanyId,
        UserId = userId,
        Action = "ActiveCompanyChanged",
        EntityName = "Company",
        EntityId = selectedCompany.CompanyId.ToString(),
        Details = $"CompanyName={selectedCompany.Name}; LegacyCenter={selectedCompany.LegacyCenterCode}"
    }, cancellationToken);

    return Results.Redirect(string.IsNullOrWhiteSpace(form.ReturnUrl) ? "/" : form.ReturnUrl);
}).RequireAuthorization();

app.MapPost("/account/change-password", async Task<IResult> (
    HttpContext httpContext,
    [FromForm] ChangePasswordForm form,
    IAuthService authService,
    CancellationToken cancellationToken) =>
{
    if (!(httpContext.User.Identity?.IsAuthenticated ?? false))
    {
        return Results.Redirect("/login");
    }

    var userId = httpContext.User.GetUserId();
    if (!userId.HasValue)
    {
        return Results.Redirect("/login");
    }

    if (string.IsNullOrWhiteSpace(form.NewPassword) || !string.Equals(form.NewPassword, form.ConfirmPassword, StringComparison.Ordinal))
    {
        var invalidReturnUrl = string.IsNullOrWhiteSpace(form.ReturnUrl) ? "/" : form.ReturnUrl;
        return Results.Redirect($"/cuenta/cambiar-password?error=mismatch&returnUrl={Uri.EscapeDataString(invalidReturnUrl)}");
    }

    try
    {
        await authService.ChangeOwnPasswordAsync(userId.Value, form.NewPassword, cancellationToken);
    }
    catch (InvalidOperationException)
    {
        var invalidReturnUrl = string.IsNullOrWhiteSpace(form.ReturnUrl) ? "/" : form.ReturnUrl;
        return Results.Redirect($"/cuenta/cambiar-password?error=invalid&returnUrl={Uri.EscapeDataString(invalidReturnUrl)}");
    }

    var session = await authService.GetSessionAsync(userId.Value, cancellationToken);
    if (session is null)
    {
        await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Results.Redirect("/login");
    }

    var currentActiveCompanyId = httpContext.User.GetActiveCompanyId();
    var activeCompanyId = session.AllowedCompanies.Any(company => company.CompanyId == currentActiveCompanyId)
        ? currentActiveCompanyId
        : session.AllowedCompanies.FirstOrDefault()?.CompanyId;

    await httpContext.SignInAsync(
        CookieAuthenticationDefaults.AuthenticationScheme,
        AuthCookieFactory.CreatePrincipal(session, activeCompanyId));

    return Results.Redirect(string.IsNullOrWhiteSpace(form.ReturnUrl) ? "/" : form.ReturnUrl);
}).RequireAuthorization();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

static string BuildReturnUrl(HttpRequest request)
{
    var path = string.IsNullOrWhiteSpace(request.Path.Value) ? "/" : request.Path.Value;
    return request.QueryString.HasValue
        ? $"{path}{request.QueryString.Value}"
        : path;
}

static string NormalizeReturnUrl(string? returnUrl)
{
    if (string.IsNullOrWhiteSpace(returnUrl))
    {
        return "/";
    }

    return returnUrl.StartsWith('/') ? returnUrl : "/";
}

static bool IsPreviewExemptPath(PathString path) =>
    path.StartsWithSegments("/preview-access") ||
    path.StartsWithSegments("/_framework") ||
    path.StartsWithSegments("/_blazor") ||
    path.StartsWithSegments("/favicon") ||
    Path.HasExtension(path.Value);
