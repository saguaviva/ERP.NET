namespace Erp.Infrastructure.MySql.Configuration;

public sealed class BootstrapSeedOptions
{
    public const string SectionName = "BootstrapSeed";

    public string PlatformAdminEmail { get; set; } = "admin@erp.local";
    public string PlatformAdminDisplayName { get; set; } = "Platform Admin";
    public string PlatformAdminPassword { get; set; } = string.Empty;
    public string InitialTenantName { get; set; } = "Grupo demo";
    public string InitialTenantSlug { get; set; } = "grupo-demo";
    public string InitialCompanyName { get; set; } = "Empresa demo";
    public string InitialCompanySlug { get; set; } = "empresa-demo";
    public string InitialCompanyLegacyCenterCode { get; set; } = "M";

    public bool HasPlatformAdminSeed =>
        !string.IsNullOrWhiteSpace(PlatformAdminEmail) &&
        !string.IsNullOrWhiteSpace(PlatformAdminPassword);

    public bool HasInitialCompanySeed =>
        !string.IsNullOrWhiteSpace(InitialTenantName) &&
        !string.IsNullOrWhiteSpace(InitialCompanyName) &&
        !string.IsNullOrWhiteSpace(InitialCompanyLegacyCenterCode);
}
