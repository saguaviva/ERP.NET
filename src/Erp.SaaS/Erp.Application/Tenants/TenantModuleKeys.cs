namespace Erp.Application.Tenants;

public static class TenantModuleKeys
{
    public const string CrmClients = "crm.clients";

    public static readonly IReadOnlyCollection<string> All = [CrmClients];
}
