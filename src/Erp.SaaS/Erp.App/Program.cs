using System.Security.Claims;
using System.Globalization;
using Erp.App.Components;
using Erp.App.Components.Pages;
using Erp.App.Formatting;
using Erp.App.Localization;
using Erp.App.Security;
using Erp.Application.Auth;
using Erp.Application.Auditing;
using Erp.Application.Companies;
using Erp.Application.Contexts;
using Erp.Application.DemoAccess;
using Erp.Application.Intrastat;
using Erp.Application.Reporting;
using Erp.Application.Search;
using Erp.Infrastructure.MySql;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddLocalization();
builder.Services.AddHttpContextAccessor();
builder.Services.Configure<PreviewAccessOptions>(builder.Configuration.GetSection(PreviewAccessOptions.SectionName));
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = AppLanguages.Supported
        .Select(option => new CultureInfo(option.Key))
        .ToArray();

    options.DefaultRequestCulture = new RequestCulture(AppLanguages.Spanish);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.RequestCultureProviders =
    [
        new CookieRequestCultureProvider()
    ];
});
builder.Services.AddSingleton<PreviewAccessCookieProtector>();
builder.Services.AddScoped<AppText>();
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
builder.Services.AddScoped<IGlobalSearchService, GlobalSearchService>();
builder.Services.AddMySqlInfrastructure(builder.Configuration);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
app.Use(async (context, next) =>
{
    var language = AppDocumentLanguage.UsesDocumentLanguage(context.Request.Path)
        ? AppDocumentLanguage.GetPreferredLanguage(context.Request, AppLanguages.Normalize(CultureInfo.CurrentUICulture))
        : AppLanguages.Normalize(CultureInfo.CurrentUICulture);

    var culture = AppNumber.CreateDisplayCulture(new CultureInfo(language));
    CultureInfo.CurrentCulture = culture;
    CultureInfo.CurrentUICulture = culture;
    CultureInfo.DefaultThreadCurrentCulture = culture;
    CultureInfo.DefaultThreadCurrentUICulture = culture;

    await next();
});
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

app.MapPost("/account/set-language", ([FromForm] SetLanguageForm form, HttpContext httpContext) =>
{
    var culture = AppLanguages.Normalize(form.Culture);
    var requestCulture = new RequestCulture(culture);
    var returnUrl = ResolvePostReturnUrl(httpContext.Request, form.ReturnUrl);

    httpContext.Response.Cookies.Append(
        CookieRequestCultureProvider.DefaultCookieName,
        CookieRequestCultureProvider.MakeCookieValue(requestCulture),
        new CookieOptions
        {
            HttpOnly = false,
            IsEssential = true,
            SameSite = SameSiteMode.Lax,
            Secure = httpContext.Request.IsHttps,
            Path = "/",
            Expires = DateTimeOffset.UtcNow.AddYears(1)
        });

    return Results.Redirect(returnUrl);
});

app.MapPost("/account/set-output-language", ([FromForm] SetOutputLanguageForm form, HttpContext httpContext) =>
{
    var culture = AppLanguages.Normalize(form.Culture);
    var returnUrl = ResolvePostReturnUrl(httpContext.Request, form.ReturnUrl);

    httpContext.Response.Cookies.Append(
        AppDocumentLanguage.CookieName,
        culture,
        new CookieOptions
        {
            HttpOnly = false,
            IsEssential = true,
            SameSite = SameSiteMode.Lax,
            Secure = httpContext.Request.IsHttps,
            Path = "/",
            Expires = DateTimeOffset.UtcNow.AddYears(1)
        });

    return Results.Redirect(returnUrl);
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

    var returnUrl = ResolvePostReturnUrl(httpContext.Request, form.ReturnUrl);

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

    return Results.Redirect(returnUrl);
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

app.MapGet("/ventas/intrastat/export/{kind}", async Task<IResult> (
    HttpContext httpContext,
    string kind,
    [FromQuery] int? month,
    [FromQuery] int? year,
    [FromQuery] string? search,
    [FromQuery(Name = "classified")] bool? onlyClassified,
    IIntrastatQueries intrastatQueries,
    CancellationToken cancellationToken) =>
{
    if (!(httpContext.User.Identity?.IsAuthenticated ?? false))
    {
        return Results.Redirect("/login");
    }

    var tenantId = httpContext.User.GetTenantId();
    var companyId = httpContext.User.GetActiveCompanyId();
    if (!tenantId.HasValue || !companyId.HasValue)
    {
        return Results.BadRequest("No hay tenant o empresa activa para exportar Intrastat.");
    }

    var filter = new IntrastatFilter
    {
        Month = Math.Clamp(month ?? DateTime.Today.Month, 1, 12),
        Year = year ?? DateTime.Today.Year,
        Search = search ?? string.Empty,
        OnlyClassified = onlyClassified ?? false,
        Page = 1,
        PageSize = 50000,
        SortColumn = nameof(IntrastatLineDto.IssueDate),
        SortDescending = true
    };

    var report = await intrastatQueries.GetReportAsync(tenantId.Value, companyId.Value, filter, cancellationToken);
    var exportLanguage = AppDocumentLanguage.GetPreferredLanguage(httpContext.Request, AppLanguages.Normalize(CultureInfo.CurrentUICulture));
    if (string.Equals(kind, "excel", StringComparison.OrdinalIgnoreCase) ||
        string.Equals(kind, "xlsx", StringComparison.OrdinalIgnoreCase))
    {
        var workbookPayload = IntrastatExcelExporter.BuildWorkbook(report, filter.Year, filter.Month, exportLanguage);
        var workbookName = $"intrastat-{filter.Year}-{filter.Month:00}.xlsx";
        return Results.File(
            workbookPayload,
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            workbookName);
    }

    var safeKind = string.Equals(kind, "summary", StringComparison.OrdinalIgnoreCase) ? "summary" : "detail";
    var fileName = $"intrastat-{safeKind}-{filter.Year}-{filter.Month:00}.csv";
    var payload = safeKind == "summary"
        ? IntrastatCsvExporter.BuildSummaryCsv(report, exportLanguage)
        : IntrastatCsvExporter.BuildDetailCsv(report, exportLanguage);

    return Results.File(payload, "text/csv; charset=utf-8", fileName);
}).RequireAuthorization();

app.MapGet("/listados/compras-ventas-ordenes/export/csv", async Task<IResult> (
    HttpContext httpContext,
    [FromQuery] DateTime? from,
    [FromQuery] DateTime? to,
    [FromQuery] string? search,
    [FromQuery] string? category,
    [FromQuery] string? type,
    IReportingQueries reportingQueries,
    CancellationToken cancellationToken) =>
{
    if (!(httpContext.User.Identity?.IsAuthenticated ?? false))
    {
        return Results.Redirect("/login");
    }

    var tenantId = httpContext.User.GetTenantId();
    var companyId = httpContext.User.GetActiveCompanyId();
    if (!tenantId.HasValue || !companyId.HasValue)
    {
        return Results.BadRequest("No hay tenant o empresa activa para exportar listados.");
    }

    var filter = new OperationalDocumentFilter
    {
        DateFrom = from,
        DateTo = to,
        Search = search ?? string.Empty,
        Category = category ?? string.Empty,
        TypeKey = type ?? string.Empty,
        Page = 1,
        PageSize = 50000,
        SortColumn = nameof(OperationalDocumentListItemDto.DocumentDate),
        SortDescending = true
    };

    var result = await reportingQueries.SearchOperationalDocumentsAsync(tenantId.Value, companyId.Value, filter, cancellationToken);
    var exportLanguage = AppDocumentLanguage.GetPreferredLanguage(httpContext.Request, AppLanguages.Normalize(CultureInfo.CurrentUICulture));
    var payload = ReportingCsvExporter.BuildOperationalDocumentsCsv(result.Items.ToArray(), exportLanguage);
    var areaSuffix = (category ?? string.Empty).Trim() switch
    {
        "Sales" => "ventas",
        "Purchases" => "compras",
        "Production" => "produccion",
        "Warehouse" => "almacen",
        "Finance" => "finanzas",
        _ => "operativos"
    };
    var fileName = $"listados-{areaSuffix}-{DateTime.Today:yyyyMMdd}.csv";
    return Results.File(payload, "text/csv; charset=utf-8", fileName);
}).RequireAuthorization();

app.MapGet("/estadisticas/export/csv", async Task<IResult> (
    HttpContext httpContext,
    [FromQuery] DateTime? from,
    [FromQuery] DateTime? to,
    [FromQuery] string? area,
    IReportingQueries reportingQueries,
    CancellationToken cancellationToken) =>
{
    if (!(httpContext.User.Identity?.IsAuthenticated ?? false))
    {
        return Results.Redirect("/login");
    }

    var tenantId = httpContext.User.GetTenantId();
    var companyId = httpContext.User.GetActiveCompanyId();
    if (!tenantId.HasValue || !companyId.HasValue)
    {
        return Results.BadRequest("No hay tenant o empresa activa para exportar estadísticas.");
    }

    var filter = new BusinessStatisticsFilter
    {
        DateFrom = from,
        DateTo = to
    };

    var stats = await reportingQueries.GetBusinessStatisticsAsync(tenantId.Value, companyId.Value, filter, cancellationToken);
    var exportLanguage = AppDocumentLanguage.GetPreferredLanguage(httpContext.Request, AppLanguages.Normalize(CultureInfo.CurrentUICulture));
    var payload = ReportingCsvExporter.BuildStatisticsCsv(stats, exportLanguage, area);
    var areaSuffix = (area ?? string.Empty).Trim().ToLowerInvariant() switch
    {
        "sales" => "ventas",
        "purchases" => "compras",
        "production" => "produccion",
        "warehouse" => "almacen",
        _ => "global"
    };
    var fileName = $"estadisticas-{areaSuffix}-{DateTime.Today:yyyyMMdd}.csv";
    return Results.File(payload, "text/csv; charset=utf-8", fileName);
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

static string ResolvePostReturnUrl(HttpRequest request, string? formReturnUrl)
{
    var normalizedFormReturnUrl = NormalizeReturnUrl(formReturnUrl);
    if (!string.Equals(normalizedFormReturnUrl, "/", StringComparison.Ordinal) ||
        string.Equals(formReturnUrl, "/", StringComparison.Ordinal))
    {
        return normalizedFormReturnUrl;
    }

    if (Uri.TryCreate(request.Headers.Referer.ToString(), UriKind.Absolute, out var refererUri))
    {
        var refererPathAndQuery = string.IsNullOrWhiteSpace(refererUri.PathAndQuery)
            ? "/"
            : refererUri.PathAndQuery;

        return NormalizeReturnUrl(refererPathAndQuery);
    }

    return "/";
}

static bool IsPreviewExemptPath(PathString path) =>
    path.StartsWithSegments("/preview-access") ||
    path.StartsWithSegments("/_framework") ||
    path.StartsWithSegments("/_blazor") ||
    path.StartsWithSegments("/favicon") ||
    Path.HasExtension(path.Value);
