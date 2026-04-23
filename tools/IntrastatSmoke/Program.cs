using System.Globalization;
using System.Text.Json;
using Erp.Application.Companies;
using Erp.Application.Contexts;
using Erp.Application.Intrastat;
using Erp.Infrastructure.MySql.Configuration;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Intrastat;
using Microsoft.Extensions.Options;
using MySqlConnector;

var exitCode = await IntrastatSmokeRunner.RunAsync();
Environment.ExitCode = exitCode;

internal static class IntrastatSmokeRunner
{
    private const string UserSecretsId = "erp-saas-shared-dev-secrets";
    private static readonly HashSet<string> EuCountryCodes =
    [
        "AT", "BE", "BG", "CY", "CZ", "DE", "DK", "EE", "EL", "FI", "FR",
        "HR", "HU", "IE", "IT", "LT", "LU", "LV", "MT", "NL", "PL", "PT", "RO",
        "SE", "SI", "SK"
    ];

    public static async Task<int> RunAsync()
    {
        var root = ResolveRepositoryRoot();
        var appSettingsPath = Path.Combine(root, @"src\Erp.SaaS\Erp.App\appsettings.json");
        var saasOptions = LoadSaasDatabaseOptions(appSettingsPath);
        var legacyOptions = LoadLegacyDatabaseOptions(appSettingsPath);

        var saasFactory = new MySqlConnectionFactory(Options.Create(saasOptions));
        var legacyFactory = new LegacyMySqlConnectionFactory(Options.Create(legacyOptions));

        if (!saasFactory.IsConfigured || !legacyFactory.IsConfigured)
        {
            Console.Error.WriteLine("SaasDatabase o LegacySourceDatabase no están configuradas.");
            return 2;
        }

        var companies = await LoadActiveCompaniesAsync(saasFactory);
        if (companies.Count == 0)
        {
            Console.WriteLine("No hay empresas activas para probar Intrastat.");
            return 0;
        }

        var failures = new List<string>();

        await using var legacyConnection = await legacyFactory.OpenConnectionAsync();
        foreach (var company in companies)
        {
            Console.WriteLine($"[company] {company.Name} ({company.LegacyCenterCode}) {company.CompanyId}");

            var intrastat = CreateIntrastatService(saasFactory, legacyFactory, company);
            var periods = await LoadTopLegacyPeriodsAsync(legacyConnection, company.LegacyCenterCode, 2);
            if (periods.Count == 0)
            {
                Console.WriteLine("  [info] sin períodos legacy con datos intrastat para latest/top periods");
            }

            var latestPeriod = await intrastat.GetLatestPeriodAsync(company.TenantId, company.CompanyId);
            if (periods.Count > 0 && latestPeriod is null)
            {
                failures.Add($"  [fail] {company.Name} latest-period -> null");
            }
            else if (periods.Count == 0 && latestPeriod is null)
            {
                Console.WriteLine("  [ok] latest-period -> null (sin datos para la empresa)");
            }
            else if (periods.Count > 0)
            {
                var topPeriod = periods[0];
                if (latestPeriod!.Year != topPeriod.Year || latestPeriod.Month != topPeriod.Month)
                {
                    failures.Add($"  [fail] {company.Name} latest-period -> app {latestPeriod.Month:00}/{latestPeriod.Year} <> legacy {topPeriod.Month:00}/{topPeriod.Year}");
                }
                else
                {
                    Console.WriteLine($"  [ok] latest-period {latestPeriod.Month:00}/{latestPeriod.Year}");
                }
            }

            foreach (var period in periods)
            {
                var label = $"{period.Month:00}/{period.Year}";
                try
                {
                    var expected = await LoadExpectedMetricsAsync(legacyConnection, company.LegacyCenterCode, period.Year, period.Month);
                    var report = await intrastat.GetReportAsync(
                        company.TenantId,
                        company.CompanyId,
                        new IntrastatFilter
                        {
                            Year = period.Year,
                            Month = period.Month,
                            Page = 1,
                            PageSize = 500
                        });

                    ValidateReport(company, period, expected, report);
                    Console.WriteLine($"  [ok] report {label} -> lines={report.TotalCount}, countries={report.CountriesCount}, net={report.TotalNetAmount.ToString("N2", CultureInfo.InvariantCulture)}");
                }
                catch (Exception ex)
                {
                    var failure = $"  [fail] {company.Name} report {label} -> {ex.GetType().Name}: {ex.Message}";
                    Console.Error.WriteLine(failure);
                    failures.Add(failure);
                }
            }

            if (company.LegacyCenterCode is "C" or "M")
            {
                const int targetYear = 2025;
                const int targetMonth = 4;

                try
                {
                    var expected = await LoadExpectedMetricsAsync(legacyConnection, company.LegacyCenterCode, targetYear, targetMonth);
                    var report = await intrastat.GetReportAsync(
                        company.TenantId,
                        company.CompanyId,
                        new IntrastatFilter
                        {
                            Year = targetYear,
                            Month = targetMonth,
                            Page = 1,
                            PageSize = 500
                        });

                    ValidateReport(company, new LegacyPeriodDto(targetYear, targetMonth, expected.LineCount), expected, report);
                    Console.WriteLine($"  [ok] report {targetMonth:00}/{targetYear} -> lines={report.TotalCount}, countries={report.CountriesCount}, net={report.TotalNetAmount.ToString("N2", CultureInfo.InvariantCulture)}");
                }
                catch (Exception ex)
                {
                    var failure = $"  [fail] {company.Name} report {targetMonth:00}/{targetYear} -> {ex.GetType().Name}: {ex.Message}";
                    Console.Error.WriteLine(failure);
                    failures.Add(failure);
                }
            }
        }

        if (failures.Count == 0)
        {
            Console.WriteLine("Intrastat smoke: OK");
            return 0;
        }

        Console.Error.WriteLine();
        Console.Error.WriteLine("Intrastat smoke: FAIL");
        foreach (var failure in failures)
        {
            Console.Error.WriteLine(failure);
        }

        return 1;
    }

    private static void ValidateReport(
        CompanyProbeDto company,
        LegacyPeriodDto period,
        ExpectedMetrics expected,
        IntrastatReportDto report)
    {
        AssertEqual(company, period, "TotalCount", expected.LineCount, report.TotalCount);
        AssertEqual(company, period, "CountriesCount", expected.CountryCount, report.CountriesCount);
        AssertEqual(company, period, "ClassifiedLinesCount", expected.ClassifiedLineCount, report.ClassifiedLinesCount);
        AssertEqual(company, period, "UnclassifiedLinesCount", expected.LineCount - expected.ClassifiedLineCount, report.UnclassifiedLinesCount);
        AssertDecimal(company, period, "TransportAmount", expected.TransportAmount, report.TransportAmount);
        AssertDecimal(company, period, "SalesAmount", expected.SalesAmount, report.SalesAmount);
        AssertDecimal(company, period, "TotalWeightKg", expected.TotalWeightKg, report.TotalWeightKg);
        AssertDecimal(company, period, "TotalNetAmount", expected.TotalNetAmount, report.TotalNetAmount);
        AssertDecimal(company, period, "TotalWithTransportAmount", expected.TotalNetAmount, report.TotalWithTransportAmount);

        if (report.MatrixRows.Count != 27)
        {
            throw new InvalidOperationException($"MatrixRows esperadas 27 y llegaron {report.MatrixRows.Count}.");
        }

        if (report.MatrixNcCodes.Count != 6)
        {
            throw new InvalidOperationException($"MatrixNcCodes esperados 6 y llegaron {report.MatrixNcCodes.Count}.");
        }
    }

    private static void AssertEqual(CompanyProbeDto company, LegacyPeriodDto period, string metric, int expected, int actual)
    {
        if (expected != actual)
        {
            throw new InvalidOperationException($"{metric}: esperado {expected} y llegó {actual} para {company.LegacyCenterCode} {period.Month:00}/{period.Year}.");
        }
    }

    private static void AssertDecimal(CompanyProbeDto company, LegacyPeriodDto period, string metric, decimal expected, decimal actual)
    {
        if (decimal.Round(expected, 2, MidpointRounding.AwayFromZero) != decimal.Round(actual, 2, MidpointRounding.AwayFromZero))
        {
            throw new InvalidOperationException($"{metric}: esperado {expected:N2} y llegó {actual:N2} para {company.LegacyCenterCode} {period.Month:00}/{period.Year}.");
        }
    }

    private static async Task<List<LegacyPeriodDto>> LoadTopLegacyPeriodsAsync(MySqlConnection connection, string centerCode, int limit)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT YEAR(f.DATA) AS year_value,
                   MONTH(f.DATA) AS month_value,
                   COUNT(*) AS line_count
            FROM factur f
            INNER JOIN dfactu df
              ON df.FRA = f.FRA
             AND df.DOCUMENT = f.DOCUMENT
            INNER JOIN clients c
              ON c.CENTRO = f.CENTRO
             AND c.CODI = f.CLIENT
            WHERE f.DOCUMENT = 'F'
              AND f.CENTRO = @centerCode
              AND LEFT(COALESCE(c.NIF, ''), 2) IN ('AT','BE','BG','CY','CZ','DE','DK','EE','EL','FI','FR','HR','HU','IE','IT','LT','LU','LV','MT','NL','PL','PT','RO','SE','SI','SK')
            GROUP BY YEAR(f.DATA), MONTH(f.DATA)
            ORDER BY YEAR(f.DATA) DESC, MONTH(f.DATA) DESC
            LIMIT @limit;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@limit", limit);

        var periods = new List<LegacyPeriodDto>();
        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            periods.Add(new LegacyPeriodDto(
                reader.GetInt32("year_value"),
                reader.GetInt32("month_value"),
                reader.GetInt32("line_count")));
        }

        return periods;
    }

    private static async Task<ExpectedMetrics> LoadExpectedMetricsAsync(MySqlConnection connection, string centerCode, int year, int month)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT COALESCE(c.NIF, '') AS client_tax_id,
                   COALESCE(df.DESCRI, '') AS description,
                   COALESCE(df.NCCODE, '') AS nc_code,
                   COALESCE(df.UNITATS, 0) AS quantity,
                   COALESCE(df.PESO, 0) AS unit_weight,
                   COALESCE(df.IMPORT, 0) AS line_import,
                   COALESCE(c.DTOFORMA, 0) AS payment_discount_percent,
                   COALESCE(f.DTE1, 0) AS discount_amount
            FROM factur f
            INNER JOIN dfactu df
              ON df.FRA = f.FRA
             AND df.DOCUMENT = f.DOCUMENT
            INNER JOIN clients c
              ON c.CENTRO = f.CENTRO
             AND c.CODI = f.CLIENT
            WHERE f.DOCUMENT = 'F'
              AND f.CENTRO = @centerCode
              AND YEAR(f.DATA) = @year
              AND MONTH(f.DATA) = @month
              AND LEFT(COALESCE(c.NIF, ''), 2) IN ('AT','BE','BG','CY','CZ','DE','DK','EE','EL','FI','FR','HR','HU','IE','IT','LT','LU','LV','MT','NL','PL','PT','RO','SE','SI','SK')
            ORDER BY f.FRA, COALESCE(df.NLINEA, 0);
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@year", year);
        command.Parameters.AddWithValue("@month", month);

        var lineCount = 0;
        var classifiedLineCount = 0;
        var countries = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var transportAmount = 0m;
        var salesAmount = 0m;
        var totalWeightKg = 0m;

        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            lineCount++;

            var clientTaxId = reader.GetString("client_tax_id");
            var countryCode = NormalizeCountryCode(clientTaxId);
            if (!string.IsNullOrWhiteSpace(countryCode))
            {
                countries.Add(countryCode);
            }

            var isTransportCharge = string.Equals(reader.GetString("description"), "TRANSIT AND COURIER CHARGES", StringComparison.OrdinalIgnoreCase);
            var lineImport = reader.GetDecimal("line_import");
            var discountAmount = reader.GetDecimal("discount_amount");
            var paymentDiscountPercent = reader.GetDecimal("payment_discount_percent");
            var effectiveAmount = isTransportCharge
                ? decimal.Round(lineImport, 2, MidpointRounding.AwayFromZero)
                : discountAmount != 0
                    ? decimal.Round(lineImport * ((100m - paymentDiscountPercent) / 100m), 2, MidpointRounding.AwayFromZero)
                    : decimal.Round(lineImport, 2, MidpointRounding.AwayFromZero);

            if (isTransportCharge)
            {
                transportAmount += effectiveAmount;
            }
            else
            {
                salesAmount += effectiveAmount;
            }

            totalWeightKg += decimal.Round(reader.GetDecimal("unit_weight") * reader.GetDecimal("quantity") / 1000m, 2, MidpointRounding.AwayFromZero);

            if (!string.IsNullOrWhiteSpace(NormalizeNcCodeCandidate(reader.GetString("nc_code"))))
            {
                classifiedLineCount++;
            }
        }

        return new ExpectedMetrics(
            lineCount,
            countries.Count,
            classifiedLineCount,
            decimal.Round(transportAmount, 2, MidpointRounding.AwayFromZero),
            decimal.Round(salesAmount, 2, MidpointRounding.AwayFromZero),
            decimal.Round(totalWeightKg, 2, MidpointRounding.AwayFromZero),
            decimal.Round(transportAmount + salesAmount, 2, MidpointRounding.AwayFromZero));
    }

    private static MySqlIntrastatService CreateIntrastatService(
        MySqlConnectionFactory saasFactory,
        LegacyMySqlConnectionFactory legacyFactory,
        CompanyProbeDto company)
    {
        var currentUser = new FakeCurrentUserContext();
        var companyAccess = new FakeCompanyAccessService(company);
        var tenantContext = new FakeTenantContext(company.TenantId);
        var activeCompanyContext = new FakeActiveCompanyContext(company.CompanyId);

        return new MySqlIntrastatService(
            saasFactory,
            legacyFactory,
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
              AND COALESCE(legacy_center_code, '') <> ''
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

    private static SaasDatabaseOptions LoadSaasDatabaseOptions(string appSettingsPath)
    {
        var options = new SaasDatabaseOptions();
        LoadDatabaseOptionsInto(appSettingsPath, SaasDatabaseOptions.SectionName, options);
        return options;
    }

    private static LegacySourceDatabaseOptions LoadLegacyDatabaseOptions(string appSettingsPath)
    {
        var options = new LegacySourceDatabaseOptions();
        LoadDatabaseOptionsInto(appSettingsPath, LegacySourceDatabaseOptions.SectionName, options);
        return options;
    }

    private static void LoadDatabaseOptionsInto(string appSettingsPath, string sectionName, MySqlDatabaseOptions options)
    {
        using var document = JsonDocument.Parse(File.ReadAllText(appSettingsPath));
        var section = document.RootElement.GetProperty(sectionName);

        options.Host = section.GetProperty("Host").GetString() ?? "localhost";
        options.Port = section.GetProperty("Port").GetInt32();
        options.Database = section.GetProperty("Database").GetString() ?? string.Empty;
        options.Username = section.GetProperty("Username").GetString() ?? string.Empty;
        options.Password = section.GetProperty("Password").GetString() ?? string.Empty;
        options.BootstrapOnStartup = section.TryGetProperty("BootstrapOnStartup", out var bootstrap)
            && bootstrap.ValueKind == JsonValueKind.True;

        ApplyUserSecrets(sectionName, options);
    }

    private static void ApplyUserSecrets(string sectionName, MySqlDatabaseOptions options)
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
        if (document.RootElement.TryGetProperty($"{sectionName}:Password", out var passwordElement))
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

    private static string NormalizeCountryCode(string taxId)
    {
        if (string.IsNullOrWhiteSpace(taxId))
        {
            return string.Empty;
        }

        var prefix = new string(taxId.Trim().ToUpperInvariant().Take(2).ToArray());
        return prefix switch
        {
            "GR" => "EL",
            _ => prefix
        };
    }

    private static string NormalizeNcCodeCandidate(string rawValue)
    {
        if (string.IsNullOrWhiteSpace(rawValue))
        {
            return string.Empty;
        }

        var digits = new string(rawValue.Where(char.IsDigit).ToArray());
        if (digits.Length != 8)
        {
            return string.Empty;
        }

        return string.Create(CultureInfo.InvariantCulture, $"{digits[..4]} {digits.Substring(4, 2)} {digits.Substring(6, 2)}");
    }

    private sealed record CompanyProbeDto(
        Guid CompanyId,
        Guid TenantId,
        string Name,
        string Slug,
        string LegacyCenterCode);

    private sealed record LegacyPeriodDto(int Year, int Month, int LineCount);

    private sealed record ExpectedMetrics(
        int LineCount,
        int CountryCount,
        int ClassifiedLineCount,
        decimal TransportAmount,
        decimal SalesAmount,
        decimal TotalWeightKg,
        decimal TotalNetAmount);

    private sealed class FakeCompanyAccessService : ICompanyAccessService
    {
        private readonly AllowedCompanyDto _company;

        public FakeCompanyAccessService(CompanyProbeDto company)
        {
            _company = new AllowedCompanyDto
            {
                CompanyId = company.CompanyId,
                TenantId = company.TenantId,
                Name = company.Name,
                Slug = company.Slug,
                LegacyCenterCode = company.LegacyCenterCode
            };
        }

        public Task<IReadOnlyCollection<AllowedCompanyDto>> GetAllowedCompaniesAsync(Guid userId, Guid tenantId, CancellationToken cancellationToken = default)
        {
            IReadOnlyCollection<AllowedCompanyDto> companies = tenantId == _company.TenantId
                ? [_company]
                : [];

            return Task.FromResult(companies);
        }
    }

    private sealed class FakeCurrentUserContext : ICurrentUserContext
    {
        public Guid? UserId { get; } = Guid.Parse("22222222-2222-2222-2222-222222222222");
        public string Email => "intrastat-smoke@local";
        public string DisplayName => "Intrastat Smoke";
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
