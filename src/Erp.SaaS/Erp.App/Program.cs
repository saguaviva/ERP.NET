using System.Security.Claims;
using Erp.App.Components;
using Erp.App.Security;
using Erp.Application.Auth;
using Erp.Application.Companies;
using Erp.Application.Contexts;
using Erp.Infrastructure.MySql;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddHttpContextAccessor();
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
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

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

    return Results.Redirect(string.IsNullOrWhiteSpace(form.ReturnUrl) ? "/" : form.ReturnUrl);
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

    return Results.Redirect(string.IsNullOrWhiteSpace(form.ReturnUrl) ? "/" : form.ReturnUrl);
}).RequireAuthorization();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
