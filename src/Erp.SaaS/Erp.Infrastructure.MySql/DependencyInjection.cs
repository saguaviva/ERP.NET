using Erp.Application.Auth;
using Erp.Application.Auditing;
using Erp.Application.Clients;
using Erp.Application.Companies;
using Erp.Application.DemoAccess;
using Erp.Application.Fornituras;
using Erp.Application.Leads;
using Erp.Application.LegacySync;
using Erp.Application.Pricing;
using Erp.Application.Purchases;
using Erp.Application.Representatives;
using Erp.Application.Sales;
using Erp.Application.Suppliers;
using Erp.Application.Tenants;
using Erp.Application.Stock;
using Erp.Application.Talleres;
using Erp.Application.Transportistas;
using Erp.Infrastructure.MySql.Auditing;
using Erp.Infrastructure.MySql.Auth;
using Erp.Infrastructure.MySql.Clients;
using Erp.Infrastructure.MySql.Companies;
using Erp.Infrastructure.MySql.Configuration;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.DemoAccess;
using Erp.Infrastructure.MySql.Fornituras;
using Erp.Infrastructure.MySql.Leads;
using Erp.Infrastructure.MySql.LegacySync;
using Erp.Infrastructure.MySql.Pricing;
using Erp.Infrastructure.MySql.Purchases;
using Erp.Infrastructure.MySql.Representatives;
using Erp.Infrastructure.MySql.Sales;
using Erp.Infrastructure.MySql.Suppliers;
using Erp.Infrastructure.MySql.Tenants;
using Erp.Infrastructure.MySql.Stock;
using Erp.Infrastructure.MySql.Talleres;
using Erp.Infrastructure.MySql.Transportistas;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Erp.Infrastructure.MySql;

public static class DependencyInjection
{
    public static IServiceCollection AddMySqlInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ErpDatabaseOptions>(configuration.GetSection(ErpDatabaseOptions.SectionName));
        services.AddOptions<SaasDatabaseOptions>()
            .Configure(options =>
            {
                configuration.GetSection(SaasDatabaseOptions.SectionName).Bind(options);
                var fallback = configuration.GetSection(ErpDatabaseOptions.SectionName).Get<ErpDatabaseOptions>() ?? new ErpDatabaseOptions();
                options.ApplyFrom(fallback);
            });
        services.AddOptions<LegacySourceDatabaseOptions>()
            .Configure(options =>
            {
                configuration.GetSection(LegacySourceDatabaseOptions.SectionName).Bind(options);
                var fallback = configuration.GetSection(ErpDatabaseOptions.SectionName).Get<ErpDatabaseOptions>() ?? new ErpDatabaseOptions();
                options.ApplyFrom(fallback);
            });
        services.Configure<BootstrapSeedOptions>(configuration.GetSection(BootstrapSeedOptions.SectionName));
        services.Configure<LegacySyncOptions>(configuration.GetSection(LegacySyncOptions.SectionName));

        services.AddSingleton<MySqlConnectionFactory>();
        services.AddSingleton<LegacyMySqlConnectionFactory>();
        services.AddHostedService<SchemaBootstrapper>();
        services.AddHostedService<LegacySyncScheduler>();

        services.AddScoped<IAuditLogService, MySqlAuditLogService>();
        services.AddScoped<IAuthService, MySqlAuthService>();
        services.AddScoped<ICompanyAccessService, MySqlCompanyAccessService>();
        services.AddScoped<ILegacySyncCheckpointRepository, MySqlLegacySyncCheckpointRepository>();
        services.AddScoped<ILegacySyncJobRunner, MySqlLegacySyncJobRunner>();
        services.AddScoped<ILegacySyncService, MySqlLegacySyncService>();
        services.AddScoped<ITenantAdminService, MySqlTenantAdminService>();
        services.AddScoped<IPlanCatalogService, MySqlPlanCatalogService>();
        services.AddScoped<ILeadCaptureService, MySqlLeadCaptureService>();
        services.AddScoped<IDemoAccessService, MySqlDemoAccessService>();
        services.AddScoped<MySqlFornituraService>();
        services.AddScoped<IFornituraQueries>(provider => provider.GetRequiredService<MySqlFornituraService>());
        services.AddScoped<IFornituraService>(provider => provider.GetRequiredService<MySqlFornituraService>());
        services.AddScoped<MySqlFornituraLegacySyncHandler>();
        services.AddScoped<ILegacyModuleSyncHandler>(provider => provider.GetRequiredService<MySqlFornituraLegacySyncHandler>());
        services.AddScoped<MySqlPurchaseOrderService>();
        services.AddScoped<IPurchaseOrderQueries>(provider => provider.GetRequiredService<MySqlPurchaseOrderService>());
        services.AddScoped<IPurchaseOrderService>(provider => provider.GetRequiredService<MySqlPurchaseOrderService>());
        services.AddScoped<MySqlPurchaseOrderLegacySyncHandler>();
        services.AddScoped<ILegacyModuleSyncHandler>(provider => provider.GetRequiredService<MySqlPurchaseOrderLegacySyncHandler>());
        services.AddScoped<MySqlPurchaseReceiptLegacySyncHandler>();
        services.AddScoped<ILegacyModuleSyncHandler>(provider => provider.GetRequiredService<MySqlPurchaseReceiptLegacySyncHandler>());
        services.AddScoped<MySqlSalesOrderService>();
        services.AddScoped<ISalesOrderQueries>(provider => provider.GetRequiredService<MySqlSalesOrderService>());
        services.AddScoped<ISalesOrderService>(provider => provider.GetRequiredService<MySqlSalesOrderService>());
        services.AddScoped<ILegacyModuleSyncHandler>(provider => provider.GetRequiredService<MySqlSalesOrderService>());
        services.AddScoped<MySqlSalesShipmentLegacySyncHandler>();
        services.AddScoped<ILegacyModuleSyncHandler>(provider => provider.GetRequiredService<MySqlSalesShipmentLegacySyncHandler>());
        services.AddScoped<MySqlSalesInvoiceLegacySyncHandler>();
        services.AddScoped<ILegacyModuleSyncHandler>(provider => provider.GetRequiredService<MySqlSalesInvoiceLegacySyncHandler>());
        services.AddScoped<MySqlClienteService>();
        services.AddScoped<IClienteQueries>(provider => provider.GetRequiredService<MySqlClienteService>());
        services.AddScoped<IClienteService>(provider => provider.GetRequiredService<MySqlClienteService>());
        services.AddScoped<MySqlClienteLegacySyncHandler>();
        services.AddScoped<ILegacyModuleSyncHandler>(provider => provider.GetRequiredService<MySqlClienteLegacySyncHandler>());
        services.AddScoped<MySqlRepresentativeService>();
        services.AddScoped<IRepresentativeQueries>(provider => provider.GetRequiredService<MySqlRepresentativeService>());
        services.AddScoped<IRepresentativeService>(provider => provider.GetRequiredService<MySqlRepresentativeService>());
        services.AddScoped<MySqlRepresentativeLegacySyncHandler>();
        services.AddScoped<ILegacyModuleSyncHandler>(provider => provider.GetRequiredService<MySqlRepresentativeLegacySyncHandler>());
        services.AddScoped<MySqlTransportistaService>();
        services.AddScoped<ITransportistaQueries>(provider => provider.GetRequiredService<MySqlTransportistaService>());
        services.AddScoped<ITransportistaService>(provider => provider.GetRequiredService<MySqlTransportistaService>());
        services.AddScoped<MySqlTransportistaLegacySyncHandler>();
        services.AddScoped<ILegacyModuleSyncHandler>(provider => provider.GetRequiredService<MySqlTransportistaLegacySyncHandler>());
        services.AddScoped<MySqlTallerService>();
        services.AddScoped<ITallerQueries>(provider => provider.GetRequiredService<MySqlTallerService>());
        services.AddScoped<ITallerService>(provider => provider.GetRequiredService<MySqlTallerService>());
        services.AddScoped<MySqlTallerLegacySyncHandler>();
        services.AddScoped<ILegacyModuleSyncHandler>(provider => provider.GetRequiredService<MySqlTallerLegacySyncHandler>());
        services.AddScoped<MySqlProveedorService>();
        services.AddScoped<IProveedorQueries>(provider => provider.GetRequiredService<MySqlProveedorService>());
        services.AddScoped<IProveedorService>(provider => provider.GetRequiredService<MySqlProveedorService>());
        services.AddScoped<MySqlProveedorLegacySyncHandler>();
        services.AddScoped<ILegacyModuleSyncHandler>(provider => provider.GetRequiredService<MySqlProveedorLegacySyncHandler>());
        services.AddScoped<MySqlStockService>();
        services.AddScoped<IStockQueries>(provider => provider.GetRequiredService<MySqlStockService>());
        services.AddScoped<IStockService>(provider => provider.GetRequiredService<MySqlStockService>());
        services.AddScoped<MySqlStockLegacySyncHandler>();
        services.AddScoped<ILegacyModuleSyncHandler>(provider => provider.GetRequiredService<MySqlStockLegacySyncHandler>());

        return services;
    }
}
