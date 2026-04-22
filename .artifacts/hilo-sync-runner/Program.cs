using Erp.Application.Contexts;
using Erp.Application.LegacySync;
using Erp.Domain.Common;
using Erp.Infrastructure.MySql;
using Erp.Infrastructure.MySql.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var appSettingsPath = @"C:\Users\sagua\source\repos\ERP.NET\src\Erp.SaaS\Erp.App";
var configuration = new ConfigurationBuilder()
    .SetBasePath(appSettingsPath)
    .AddJsonFile("appsettings.json", optional: false)
    .AddUserSecrets("erp-saas-shared-dev-secrets")
    .Build();

var fakeCurrentUser = new FakeCurrentUserContext();
var fakeTenant = new FakeTenantContext();
var fakeActiveCompany = new FakeActiveCompanyContext();

var services = new ServiceCollection();
services.AddLogging();
services.AddSingleton<ICurrentUserContext>(fakeCurrentUser);
services.AddSingleton<ITenantContext>(fakeTenant);
services.AddSingleton<IActiveCompanyContext>(fakeActiveCompany);
services.AddMySqlInfrastructure(configuration);

await using var provider = services.BuildServiceProvider();
await using var scope = provider.CreateAsyncScope();
var connectionFactory = scope.ServiceProvider.GetRequiredService<MySqlConnectionFactory>();
await using var connection = await connectionFactory.OpenConnectionAsync(CancellationToken.None);
await EnsureTejidosSchemaAsync(connection);

Guid tenantId;
await using (var tenantCommand = connection.CreateCommand())
{
    tenantCommand.CommandText =
        """
        SELECT c.tenant_id
        FROM companies c
        WHERE c.legacy_center_code IN ('C', 'M')
        GROUP BY c.tenant_id
        ORDER BY COUNT(*) DESC, c.tenant_id
        LIMIT 1;
        """;
    var tenantValue = await tenantCommand.ExecuteScalarAsync();
    if (tenantValue is null)
    {
        throw new InvalidOperationException("No se ha encontrado ningún tenant en la SaaS local.");
    }

    tenantId = Guid.Parse(Convert.ToString(tenantValue)!);
}

fakeTenant.TenantIdValue = tenantId;
var syncService = scope.ServiceProvider.GetRequiredService<ILegacySyncService>();
var result = await syncService.RunAsync(new RunLegacySyncCommand
{
    TenantId = tenantId,
    ModuleKey = LegacySyncModuleKeys.ArticleTejidos
});

Console.WriteLine($"Companies={result.CompaniesProcessed}");
Console.WriteLine($"Inserted={result.RecordsInserted}");
Console.WriteLine($"Updated={result.RecordsUpdated}");
Console.WriteLine($"Skipped={result.RecordsSkipped}");
Console.WriteLine($"Errors={result.ErrorsCount}");
foreach (var job in result.Jobs)
{
    Console.WriteLine($"JobCompany={job.CompanyName} ({job.CompanyLegacyCenterCode})");
    Console.WriteLine($"JobStatus={job.Status}");
    Console.WriteLine($"JobInserted={job.RecordsInserted}");
    Console.WriteLine($"JobUpdated={job.RecordsUpdated}");
    Console.WriteLine($"JobSkipped={job.RecordsSkipped}");
    Console.WriteLine($"JobErrors={job.ErrorsCount}");
}

await using (var errorCommand = connection.CreateCommand())
{
    errorCommand.CommandText =
        """
        SELECT e.stage, e.legacy_entity_key, e.error_message
        FROM legacy_sync_errors e
        INNER JOIN legacy_sync_jobs j ON j.id = e.job_id
        WHERE j.module_key = @moduleKey
        ORDER BY e.created_utc DESC
        LIMIT 10;
        """;
    errorCommand.Parameters.AddWithValue("@moduleKey", LegacySyncModuleKeys.ArticleTejidos);

    await using var errorReader = await errorCommand.ExecuteReaderAsync();
    while (await errorReader.ReadAsync())
    {
        Console.WriteLine($"ErrorStage={Convert.ToString(errorReader["stage"])}");
        Console.WriteLine($"ErrorKey={Convert.ToString(errorReader["legacy_entity_key"])}");
        Console.WriteLine($"ErrorMessage={Convert.ToString(errorReader["error_message"])}");
    }
}

await using (var countCommand = connection.CreateCommand())
{
    countCommand.CommandText =
        """
        SELECT CENTRO, COUNT(*) AS total
        FROM teixits
        WHERE is_deleted = 0
        GROUP BY CENTRO
        ORDER BY CENTRO;
        """;
    await using var countReader = await countCommand.ExecuteReaderAsync();
    while (await countReader.ReadAsync())
    {
        Console.WriteLine($"LocalTeixits={Convert.ToString(countReader["CENTRO"])}:{Convert.ToString(countReader["total"])}");
    }
}

await using (var detailCountCommand = connection.CreateCommand())
{
    detailCountCommand.CommandText =
        """
        SELECT CENTRO, COUNT(*) AS total
        FROM teixits_color_detail
        GROUP BY CENTRO
        ORDER BY CENTRO;
        """;
    await using var detailReader = await detailCountCommand.ExecuteReaderAsync();
    while (await detailReader.ReadAsync())
    {
        Console.WriteLine($"LocalTeixitsColorDetail={Convert.ToString(detailReader["CENTRO"])}:{Convert.ToString(detailReader["total"])}");
    }
}

await using (var compCountCommand = connection.CreateCommand())
{
    compCountCommand.CommandText =
        """
        SELECT CENTRO, COUNT(*) AS total
        FROM teixits_composition_detail
        GROUP BY CENTRO
        ORDER BY CENTRO;
        """;
    await using var compReader = await compCountCommand.ExecuteReaderAsync();
    while (await compReader.ReadAsync())
    {
        Console.WriteLine($"LocalTeixitsCompositionDetail={Convert.ToString(compReader["CENTRO"])}:{Convert.ToString(compReader["total"])}");
    }
}

await using (var finishCountCommand = connection.CreateCommand())
{
    finishCountCommand.CommandText =
        """
        SELECT CENTRO, COUNT(*) AS total
        FROM teixits_finish_detail
        GROUP BY CENTRO
        ORDER BY CENTRO;
        """;
    await using var finishReader = await finishCountCommand.ExecuteReaderAsync();
    while (await finishReader.ReadAsync())
    {
        Console.WriteLine($"LocalTeixitsFinishDetail={Convert.ToString(finishReader["CENTRO"])}:{Convert.ToString(finishReader["total"])}");
    }
}

static async Task EnsureTejidosSchemaAsync(MySqlConnector.MySqlConnection connection)
{
    var statements = new[]
    {
        """
        CREATE TABLE IF NOT EXISTS teixits (
            CODI VARCHAR(10) NOT NULL,
            CENTRO CHAR(1) NOT NULL,
            DESCRI VARCHAR(255) NOT NULL,
            NRO VARCHAR(120) NULL,
            MAQUI INT NULL,
            MATERIA DECIMAL(12,4) NOT NULL DEFAULT 0,
            OBSERV MEDIUMTEXT NULL,
            IVA VARCHAR(20) NULL,
            TEIXIDOR INT NULL,
            PTEIXIR DECIMAL(12,4) NOT NULL DEFAULT 0,
            ESTAMPADOR INT NULL,
            PESTAM DECIMAL(12,4) NOT NULL DEFAULT 0,
            ACABADOR INT NULL,
            ACABAT VARCHAR(255) NULL,
            PACA DECIMAL(12,4) NOT NULL DEFAULT 0,
            CRU DECIMAL(12,4) NOT NULL DEFAULT 0,
            AMPLE VARCHAR(40) NULL,
            RENDIMENT DECIMAL(12,4) NOT NULL DEFAULT 0,
            MARGE DECIMAL(12,4) NOT NULL DEFAULT 0,
            GRAMA DECIMAL(12,4) NOT NULL DEFAULT 0,
            PREUM DECIMAL(12,4) NOT NULL DEFAULT 0,
            PREUK DECIMAL(12,4) NOT NULL DEFAULT 0,
            STCRUM DECIMAL(18,3) NOT NULL DEFAULT 0,
            STDISPM DECIMAL(18,3) NOT NULL DEFAULT 0,
            STCRUK DECIMAL(18,3) NOT NULL DEFAULT 0,
            STDISPK DECIMAL(18,3) NOT NULL DEFAULT 0,
            PREUPERMODEL DECIMAL(12,4) NOT NULL DEFAULT 0,
            TUBULAR TINYINT(1) NOT NULL DEFAULT 0,
            AMPLE2 DECIMAL(12,4) NOT NULL DEFAULT 0,
            origin VARCHAR(20) NOT NULL DEFAULT 'legacy',
            is_deleted TINYINT(1) NOT NULL DEFAULT 0,
            synced_utc DATETIME(6) NULL,
            PRIMARY KEY (CENTRO, CODI),
            KEY ix_teixits_descri (CENTRO, DESCRI),
            KEY ix_teixits_weaver (CENTRO, TEIXIDOR),
            KEY ix_teixits_finisher (CENTRO, ACABADOR)
        );
        """,
        """
        CREATE TABLE IF NOT EXISTS teixits_color_detail (
            CENTRO CHAR(1) NOT NULL,
            TEIXIT_CODI VARCHAR(10) NOT NULL,
            LINE_NUMBER INT NOT NULL,
            PROVE INT NOT NULL DEFAULT 0,
            COLOR VARCHAR(120) NULL,
            ACTUAL DECIMAL(18,3) NOT NULL DEFAULT 0,
            MINIM DECIMAL(18,3) NOT NULL DEFAULT 0,
            TINTAR DECIMAL(12,4) NOT NULL DEFAULT 0,
            PREU DECIMAL(12,4) NOT NULL DEFAULT 0,
            METRES DECIMAL(18,3) NOT NULL DEFAULT 0,
            KG DECIMAL(18,3) NOT NULL DEFAULT 0,
            OBSERV VARCHAR(255) NULL,
            PRIMARY KEY (CENTRO, TEIXIT_CODI, LINE_NUMBER),
            KEY ix_teixits_color_lookup (CENTRO, TEIXIT_CODI),
            KEY ix_teixits_color_color (CENTRO, COLOR)
        );
        """,
        """
        CREATE TABLE IF NOT EXISTS teixits_composition_detail (
            CENTRO CHAR(1) NOT NULL,
            TEIXIT_CODI VARCHAR(10) NOT NULL,
            LINE_NUMBER INT NOT NULL,
            COMP VARCHAR(30) NULL,
            PER INT NOT NULL DEFAULT 0,
            PROVE INT NOT NULL DEFAULT 0,
            PREU DECIMAL(12,4) NOT NULL DEFAULT 0,
            IMPORTE DECIMAL(12,4) NOT NULL DEFAULT 0,
            PRIMARY KEY (CENTRO, TEIXIT_CODI, LINE_NUMBER),
            KEY ix_teixits_comp_lookup (CENTRO, TEIXIT_CODI),
            KEY ix_teixits_comp_component (CENTRO, COMP)
        );
        """,
        """
        CREATE TABLE IF NOT EXISTS teixits_finish_detail (
            CENTRO CHAR(1) NOT NULL,
            TEIXIT_CODI VARCHAR(10) NOT NULL,
            LINE_NUMBER INT NOT NULL,
            ACABAT VARCHAR(50) NULL,
            PROVE INT NOT NULL DEFAULT 0,
            ORDEN INT NOT NULL DEFAULT 0,
            PREUM DECIMAL(12,4) NOT NULL DEFAULT 0,
            PREUK DECIMAL(12,4) NOT NULL DEFAULT 0,
            PRIMARY KEY (CENTRO, TEIXIT_CODI, LINE_NUMBER),
            KEY ix_teixits_finish_lookup (CENTRO, TEIXIT_CODI),
            KEY ix_teixits_finish_code (CENTRO, ACABAT)
        );
        """
    };

    foreach (var statement in statements)
    {
        await using var command = connection.CreateCommand();
        command.CommandText = statement;
        await command.ExecuteNonQueryAsync();
    }

    await EnsureColumnAsync(connection, "teixits", "AMPLE2", "ALTER TABLE teixits ADD COLUMN AMPLE2 DECIMAL(12,4) NOT NULL DEFAULT 0;");
    await EnsureColumnAsync(connection, "teixits", "origin", "ALTER TABLE teixits ADD COLUMN origin VARCHAR(20) NOT NULL DEFAULT 'legacy';");
    await EnsureColumnAsync(connection, "teixits", "is_deleted", "ALTER TABLE teixits ADD COLUMN is_deleted TINYINT(1) NOT NULL DEFAULT 0;");
    await EnsureColumnAsync(connection, "teixits", "synced_utc", "ALTER TABLE teixits ADD COLUMN synced_utc DATETIME(6) NULL;");
}

static async Task EnsureColumnAsync(MySqlConnector.MySqlConnection connection, string tableName, string columnName, string alterSql)
{
    await using var existsCommand = connection.CreateCommand();
    existsCommand.CommandText =
        """
        SELECT COUNT(*)
        FROM information_schema.columns
        WHERE table_schema = DATABASE()
          AND table_name = @tableName
          AND column_name = @columnName;
        """;
    existsCommand.Parameters.AddWithValue("@tableName", tableName);
    existsCommand.Parameters.AddWithValue("@columnName", columnName);

    var exists = Convert.ToInt32(await existsCommand.ExecuteScalarAsync()) > 0;
    if (exists)
    {
        return;
    }

    await using var alterCommand = connection.CreateCommand();
    alterCommand.CommandText = alterSql;
    await alterCommand.ExecuteNonQueryAsync();
}

sealed class FakeCurrentUserContext : ICurrentUserContext
{
    public Guid? UserId => Guid.Parse("11111111-1111-1111-1111-111111111111");
    public string Email => "codex@local";
    public string DisplayName => "Codex";
    public bool IsAuthenticated => true;
    public bool IsPlatformAdmin => true;
    public IReadOnlyCollection<string> Roles { get; } = [PlatformRoles.PlatformAdmin];
}

sealed class FakeTenantContext : ITenantContext
{
    public Guid? TenantId => TenantIdValue;
    public Guid? TenantIdValue { get; set; }
}

sealed class FakeActiveCompanyContext : IActiveCompanyContext
{
    public Guid? CompanyId => null;
}
