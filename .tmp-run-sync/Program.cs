using Erp.Application.Companies;
using Erp.Application.Contexts;
using Erp.Application.LegacySync;
using Erp.Infrastructure.MySql.Configuration;
using Erp.Infrastructure.MySql.Clients;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Representatives;
using Erp.Infrastructure.MySql.Suppliers;
using Erp.Infrastructure.MySql.Talleres;
using Erp.Infrastructure.MySql.LegacySync;
using Erp.Infrastructure.MySql.Fornituras;
using Erp.Infrastructure.MySql.Transportistas;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

var saasOptions = Options.Create(new SaasDatabaseOptions
{
    Host = "localhost",
    Port = 3306,
    Database = "completex",
    Username = "completex",
    Password = "completex314",
    BootstrapOnStartup = true
});

var legacyOptions = Options.Create(new LegacySourceDatabaseOptions
{
    Host = "ns346061.ip-5-196-80.eu",
    Port = 3306,
    Database = "completex",
    Username = "completexlectura",
    Password = "completex314"
});

var saasFactory = new MySqlConnectionFactory(saasOptions);
var legacyFactory = new LegacyMySqlConnectionFactory(legacyOptions);
var bootstrapper = new SchemaBootstrapper(
    saasFactory,
    saasOptions,
    Options.Create(new BootstrapSeedOptions()),
    NullLogger<SchemaBootstrapper>.Instance);
await bootstrapper.StartAsync(CancellationToken.None);
var checkpointRepository = new MySqlLegacySyncCheckpointRepository(saasFactory);
var handlers = new ILegacyModuleSyncHandler[]
{
    new MySqlFornituraLegacySyncHandler(saasFactory, legacyFactory),
    new MySqlRepresentativeLegacySyncHandler(saasFactory, legacyFactory),
    new MySqlTransportistaLegacySyncHandler(saasFactory, legacyFactory),
    new MySqlTallerLegacySyncHandler(saasFactory, legacyFactory),
    new MySqlClienteLegacySyncHandler(saasFactory, legacyFactory),
    new MySqlProveedorLegacySyncHandler(saasFactory, legacyFactory)
};
var jobRunner = new MySqlLegacySyncJobRunner(saasFactory, checkpointRepository, handlers);
var syncService = new MySqlLegacySyncService(
    saasFactory,
    jobRunner,
    new StubCompanyAccessService(),
    new StubCurrentUserContext(),
    new StubTenantContext(),
    handlers);

Guid tenantId;
await using (var conn = await saasFactory.OpenConnectionAsync())
await using (var cmd = conn.CreateCommand())
{
    cmd.CommandText = "SELECT tenant_id FROM companies WHERE legacy_center_code IN ('C','M') ORDER BY name LIMIT 1;";
    var value = await cmd.ExecuteScalarAsync();
    tenantId = value switch
    {
        Guid guid => guid,
        string s => Guid.Parse(s),
        _ => throw new InvalidOperationException("No se ha encontrado tenant para las companies C/M.")
    };
}

var modules = new[]
{
    LegacySyncModuleKeys.ArticleFornituras,
    LegacySyncModuleKeys.CrmRepresentatives,
    LegacySyncModuleKeys.CrmCarriers,
    LegacySyncModuleKeys.CrmWorkshops,
    LegacySyncModuleKeys.CrmClients,
    LegacySyncModuleKeys.CrmSuppliers
};

foreach (var moduleKey in modules)
{
    var result = await syncService.RunAsync(new RunLegacySyncCommand
    {
        TenantId = tenantId,
        CompanyId = null,
        ModuleKey = moduleKey,
        ForceFullRefresh = true,
        TriggeredByScheduler = true
    });

    Console.WriteLine($"Module: {result.ModuleKey}");
    Console.WriteLine($"Companies: {result.CompaniesProcessed}");
    Console.WriteLine($"Inserted: {result.RecordsInserted}");
    Console.WriteLine($"Updated: {result.RecordsUpdated}");
    Console.WriteLine($"Skipped: {result.RecordsSkipped}");
    Console.WriteLine($"Errors: {result.ErrorsCount}");
    foreach (var job in result.Jobs)
    {
        Console.WriteLine($"{job.CompanyName} ({job.CompanyLegacyCenterCode}) => {job.Status} | ins={job.RecordsInserted} upd={job.RecordsUpdated} skip={job.RecordsSkipped} err={job.ErrorsCount}");
    }

    Console.WriteLine();
}

sealed class StubCompanyAccessService : ICompanyAccessService
{
    public Task<IReadOnlyCollection<AllowedCompanyDto>> GetAllowedCompaniesAsync(Guid userId, Guid tenantId, CancellationToken cancellationToken = default)
        => Task.FromResult<IReadOnlyCollection<AllowedCompanyDto>>([]);
}

sealed class StubCurrentUserContext : ICurrentUserContext
{
    public Guid? UserId => null;
    public string Email => string.Empty;
    public string DisplayName => "Scheduler";
    public bool IsAuthenticated => false;
    public bool IsPlatformAdmin => true;
    public IReadOnlyCollection<string> Roles => Array.Empty<string>();
}

sealed class StubTenantContext : ITenantContext
{
    public Guid? TenantId => null;
}
