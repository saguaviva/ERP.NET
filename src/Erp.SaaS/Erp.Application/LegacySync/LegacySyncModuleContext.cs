namespace Erp.Application.LegacySync;

public sealed class LegacySyncModuleContext
{
    public Guid JobId { get; set; }
    public Guid TenantId { get; set; }
    public Guid CompanyId { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string CompanyLegacyCenterCode { get; set; } = string.Empty;
    public string ModuleKey { get; set; } = string.Empty;
    public string? CheckpointValue { get; set; }
    public bool ForceFullRefresh { get; set; }
}
