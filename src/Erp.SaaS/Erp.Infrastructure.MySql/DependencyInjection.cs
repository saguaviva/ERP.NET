using Erp.Application.Auth;
using Erp.Application.Clients;
using Erp.Application.Companies;
using Erp.Application.Leads;
using Erp.Application.Pricing;
using Erp.Application.Tenants;
using Erp.Infrastructure.MySql.Auth;
using Erp.Infrastructure.MySql.Clients;
using Erp.Infrastructure.MySql.Companies;
using Erp.Infrastructure.MySql.Configuration;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Leads;
using Erp.Infrastructure.MySql.Pricing;
using Erp.Infrastructure.MySql.Tenants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Erp.Infrastructure.MySql;

public static class DependencyInjection
{
    public static IServiceCollection AddMySqlInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ErpDatabaseOptions>(configuration.GetSection(ErpDatabaseOptions.SectionName));
        services.Configure<BootstrapSeedOptions>(configuration.GetSection(BootstrapSeedOptions.SectionName));

        services.AddSingleton<MySqlConnectionFactory>();
        services.AddHostedService<SchemaBootstrapper>();

        services.AddScoped<IAuthService, MySqlAuthService>();
        services.AddScoped<ICompanyAccessService, MySqlCompanyAccessService>();
        services.AddScoped<ITenantAdminService, MySqlTenantAdminService>();
        services.AddScoped<IPlanCatalogService, MySqlPlanCatalogService>();
        services.AddScoped<ILeadCaptureService, MySqlLeadCaptureService>();
        services.AddScoped<MySqlClienteService>();
        services.AddScoped<IClienteQueries>(provider => provider.GetRequiredService<MySqlClienteService>());
        services.AddScoped<IClienteService>(provider => provider.GetRequiredService<MySqlClienteService>());

        return services;
    }
}
