namespace Erp.Application.Tenants;

public sealed class TenantModuleSettingDto
{
    public Guid TenantId { get; set; }
    public string ModuleKey { get; set; } = string.Empty;
    public string DataScope { get; set; } = TenantModuleDataScopes.Company;
}
