using System.Text.Json;
using Erp.Application.Alerts;
using Erp.Application.Companies;
using Erp.Application.Contexts;
using Erp.Infrastructure.MySql.Alerts;
using Erp.Infrastructure.MySql.Configuration;
using Erp.Infrastructure.MySql.Database;
using Microsoft.Extensions.Options;

var exitCode = await AlertsSmokeRunner.RunAsync(args);
Environment.ExitCode = exitCode;

internal static class AlertsSmokeRunner
{
    private const string UserSecretsId = "erp-saas-shared-dev-secrets";

    public static async Task<int> RunAsync(string[] args)
    {
        var root = ResolveRepositoryRoot();
        var options = LoadDatabaseOptions(Path.Combine(root, @"src\Erp.SaaS\Erp.App\appsettings.json"));
        var connectionFactory = new MySqlConnectionFactory(Options.Create(options));

        if (!connectionFactory.IsConfigured)
        {
            Console.Error.WriteLine("SaasDatabase no está configurada.");
            return 2;
        }

        var companies = await LoadActiveCompaniesAsync(connectionFactory);
        if (companies.Count == 0)
        {
            Console.WriteLine("No hay empresas activas para probar alertas.");
            return 0;
        }

        var failures = new List<string>();
        foreach (var company in companies)
        {
            Console.WriteLine($"[company] {company.Name} ({company.LegacyCenterCode}) {company.CompanyId}");
            var alerts = CreateAlertsService(connectionFactory, company);

            failures.AddRange(await RunCaseAsync(
                company,
                "alerts:dashboard",
                async () =>
                {
                    var dashboard = await alerts.GetDashboardAsync(company.TenantId, company.CompanyId);
                    Console.WriteLine($"  [info] alerts={dashboard.TotalAlerts} critical={dashboard.CriticalAlerts} warning={dashboard.WarningAlerts} groups={dashboard.ActiveGroups}");
                    foreach (var group in dashboard.Groups.OrderBy(group => group.Key, StringComparer.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"    - {group.Key}: {group.TotalCount}");
                    }
                    return dashboard;
                }));
        }

        if (failures.Count == 0)
        {
            Console.WriteLine("Alerts smoke: OK");
            return 0;
        }

        Console.Error.WriteLine();
        Console.Error.WriteLine("Alerts smoke: FAIL");
        foreach (var failure in failures)
        {
            Console.Error.WriteLine(failure);
        }

        return 1;
    }

    private static async Task<List<string>> RunCaseAsync<T>(
        CompanyProbeDto company,
        string label,
        Func<Task<T>> action)
    {
        var failures = new List<string>();

        try
        {
            await action();
            Console.WriteLine($"  [ok] {label}");
        }
        catch (Exception ex)
        {
            var message = $"  [fail] {company.Name} [{label}] {ex.GetType().Name}: {ex.Message}";
            Console.Error.WriteLine(message);
            failures.Add(message);
        }

        return failures;
    }

    private static MySqlOperationalAlertService CreateAlertsService(
        MySqlConnectionFactory connectionFactory,
        CompanyProbeDto company)
    {
        var currentUser = new FakeCurrentUserContext();
        var companyAccess = new FakeCompanyAccessService(company);
        var tenantContext = new FakeTenantContext(company.TenantId);
        var activeCompanyContext = new FakeActiveCompanyContext(company.CompanyId);

        return new MySqlOperationalAlertService(
            connectionFactory,
            companyAccess,
            currentUser,
            tenantContext,
            activeCompanyContext);
    }

    private static async Task<List<CompanyProbeDto>> LoadActiveCompaniesAsync(MySqlConnectionFactory connectionFactory)
    {
        var items = new List<CompanyProbeDto>();

        await using var connection = await connectionFactory.OpenConnectionAsync();
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT id, tenant_id, name, slug, legacy_center_code
            FROM companies
            WHERE is_active = 1
            ORDER BY name;
            """;

        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            items.Add(new CompanyProbeDto(
                reader.GetGuid("id"),
                reader.GetGuid("tenant_id"),
                reader.GetString("name"),
                reader.GetString("slug"),
                reader.GetString("legacy_center_code")));
        }

        return items;
    }

    private static SaasDatabaseOptions LoadDatabaseOptions(string appSettingsPath)
    {
        using var document = JsonDocument.Parse(File.ReadAllText(appSettingsPath));
        var section = document.RootElement.GetProperty(SaasDatabaseOptions.SectionName);

        var options = new SaasDatabaseOptions
        {
            Host = section.GetProperty("Host").GetString() ?? "localhost",
            Port = section.GetProperty("Port").GetInt32(),
            Database = section.GetProperty("Database").GetString() ?? string.Empty,
            Username = section.GetProperty("Username").GetString() ?? string.Empty,
            Password = section.GetProperty("Password").GetString() ?? string.Empty,
            BootstrapOnStartup = section.TryGetProperty("BootstrapOnStartup", out var bootstrap)
                && bootstrap.ValueKind == JsonValueKind.True
        };

        ApplyUserSecrets(options);
        return options;
    }

    private static void ApplyUserSecrets(SaasDatabaseOptions options)
    {
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        if (string.IsNullOrWhiteSpace(appData))
        {
            return;
        }

        var secretsPath = Path.Combine(appData, "Microsoft", "UserSecrets", UserSecretsId, "secrets.json");
        if (!File.Exists(secretsPath))
        {
            return;
        }

        using var document = JsonDocument.Parse(File.ReadAllText(secretsPath));
        if (document.RootElement.TryGetProperty("SaasDatabase:Password", out var passwordElement))
        {
            options.Password = passwordElement.GetString() ?? options.Password;
        }
    }

    private static string ResolveRepositoryRoot()
    {
        var current = new DirectoryInfo(AppContext.BaseDirectory);
        while (current is not null)
        {
            if (Directory.Exists(Path.Combine(current.FullName, ".git")))
            {
                return current.FullName;
            }

            current = current.Parent;
        }

        throw new InvalidOperationException("No se ha podido localizar la raíz del repositorio.");
    }

    private sealed record CompanyProbeDto(
        Guid CompanyId,
        Guid TenantId,
        string Name,
        string Slug,
        string LegacyCenterCode);

    private sealed class FakeCompanyAccessService : ICompanyAccessService
    {
        private readonly AllowedCompanyDto company;

        public FakeCompanyAccessService(CompanyProbeDto source)
        {
            company = new AllowedCompanyDto
            {
                CompanyId = source.CompanyId,
                TenantId = source.TenantId,
                Name = source.Name,
                Slug = source.Slug,
                LegacyCenterCode = source.LegacyCenterCode
            };
        }

        public Task<IReadOnlyCollection<AllowedCompanyDto>> GetAllowedCompaniesAsync(Guid userId, Guid tenantId, CancellationToken cancellationToken = default)
        {
            IReadOnlyCollection<AllowedCompanyDto> companies = tenantId == company.TenantId
                ? [company]
                : [];

            return Task.FromResult(companies);
        }
    }

    private sealed class FakeCurrentUserContext : ICurrentUserContext
    {
        public Guid? UserId { get; } = Guid.Parse("22222222-2222-2222-2222-222222222222");
        public string Email => "alerts-smoke@local";
        public string DisplayName => "Alerts Smoke";
        public bool IsAuthenticated => true;
        public bool IsPlatformAdmin => true;
        public IReadOnlyCollection<string> Roles => ["PlatformAdmin"];
    }

    private sealed class FakeTenantContext : ITenantContext
    {
        public FakeTenantContext(Guid tenantId)
        {
            TenantId = tenantId;
        }

        public Guid? TenantId { get; }
    }

    private sealed class FakeActiveCompanyContext : IActiveCompanyContext
    {
        public FakeActiveCompanyContext(Guid companyId)
        {
            CompanyId = companyId;
        }

        public Guid? CompanyId { get; }
    }
}
