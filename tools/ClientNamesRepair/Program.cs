using System.Text.Json;
using Erp.Infrastructure.MySql.Configuration;
using Erp.Infrastructure.MySql.Database;
using Microsoft.Extensions.Options;
using MySqlConnector;

var exitCode = await ClientNamesRepairRunner.RunAsync();
Environment.ExitCode = exitCode;

internal static class ClientNamesRepairRunner
{
    private const string UserSecretsId = "erp-saas-shared-dev-secrets";

    public static async Task<int> RunAsync()
    {
        var root = ResolveRepositoryRoot();
        var options = LoadDatabaseOptions(Path.Combine(root, @"src\Erp.SaaS\Erp.App\appsettings.json"));
        var connectionFactory = new MySqlConnectionFactory(Options.Create(options));

        if (!connectionFactory.IsConfigured)
        {
            Console.Error.WriteLine("SaasDatabase no está configurada.");
            return 2;
        }

        await using var connection = await connectionFactory.OpenConnectionAsync();
        var results = await ClientNameRepair.RunAsync(connection, CancellationToken.None);

        foreach (var result in results)
        {
            Console.WriteLine($"[repair] {result.Target}: {result.RowsAffected}");
        }

        Console.WriteLine();
        foreach (var target in new[]
                 {
                     "sales_orders",
                     "sales_invoice_drafts",
                     "sales_invoices",
                     "sales_remittance_invoices",
                     "article_models",
                     "mostres",
                     "mostres_detail",
                     "mostres_breakdown"
                 })
        {
            Console.WriteLine($"[leftover] {target}: {await CountPlaceholderNamesAsync(connection, target)}");
        }

        Console.WriteLine();
        await PrintSalesDiagnosticsAsync(connection);

        Console.WriteLine("Client names repair: OK");
        return 0;
    }

    private static async Task<int> CountPlaceholderNamesAsync(MySqlConnection connection, string tableName)
    {
        var (nameColumn, codeColumn) = tableName switch
        {
            "sales_orders" => ("client_name", "client_code"),
            "sales_invoice_drafts" => ("client_name", "client_code"),
            "sales_invoices" => ("client_name", "client_code"),
            "sales_remittance_invoices" => ("client_name", "client_code"),
            _ => ("NOMCLIENT", "CLIENT")
        };

        await using var command = connection.CreateCommand();
        command.CommandText =
            $"""
            SELECT COUNT(*)
            FROM {tableName}
            WHERE {codeColumn} > 0
              AND (
                    COALESCE(TRIM({nameColumn}), '') = ''
                    OR {nameColumn} REGEXP '^(Cliente|Client) [0-9]+$'
                    OR TRIM({nameColumn}) = CAST({codeColumn} AS CHAR)
                  );
            """;

        return Convert.ToInt32(await command.ExecuteScalarAsync());
    }

    private static async Task PrintSalesDiagnosticsAsync(MySqlConnection connection)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT si.invoice_number,
                   comp.name AS company_name,
                   comp.legacy_center_code AS company_center_code,
                   si.legacy_center_code AS invoice_center_code,
                   si.client_code,
                   si.client_name,
                   c_company.NOM AS company_center_client_name,
                   c_invoice.NOM AS invoice_center_client_name
            FROM sales_invoices si
            LEFT JOIN companies comp
              ON comp.id = si.company_id
             AND comp.tenant_id = si.tenant_id
            LEFT JOIN clients c_company
              ON c_company.CODI = si.client_code
             AND c_company.CENTRO = comp.legacy_center_code
            LEFT JOIN clients c_invoice
              ON c_invoice.CODI = si.client_code
             AND c_invoice.CENTRO = si.legacy_center_code
            WHERE (
                    COALESCE(TRIM(si.client_name), '') = ''
                    OR si.client_name REGEXP '^(Cliente|Client) [0-9]+$'
                    OR TRIM(si.client_name) = CAST(si.client_code AS CHAR)
                  )
            ORDER BY si.issue_date DESC, si.invoice_number DESC
            LIMIT 10;
            """;

        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            var invoiceNumber = reader.GetInt32("invoice_number");
            var companyName = reader.GetString("company_name");
            var companyCenter = GetStringOrEmpty(reader, "company_center_code");
            var invoiceCenter = GetStringOrEmpty(reader, "invoice_center_code");
            var clientCode = reader.GetInt32("client_code");
            var currentName = GetStringOrEmpty(reader, "client_name");
            var byCompany = GetStringOrEmpty(reader, "company_center_client_name");
            var byRow = GetStringOrEmpty(reader, "invoice_center_client_name");
            Console.WriteLine(
                $"[diag] invoice={invoiceNumber} company={companyName} compCenter={companyCenter} rowCenter={invoiceCenter} client={clientCode} current='{currentName}' byCompany='{byCompany}' byRow='{byRow}'");
        }
    }

    private static string GetStringOrEmpty(MySqlDataReader reader, string columnName)
    {
        var ordinal = reader.GetOrdinal(columnName);
        return reader.IsDBNull(ordinal) ? string.Empty : reader.GetString(ordinal);
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
}
