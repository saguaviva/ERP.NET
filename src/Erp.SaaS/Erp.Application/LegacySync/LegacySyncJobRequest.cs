namespace Erp.Application.LegacySync;

public sealed class LegacySyncJobRequest
{
    public Guid TenantId { get; set; }
    public Guid CompanyId { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string CompanyLegacyCenterCode { get; set; } = string.Empty;
    public string ModuleKey { get; set; } = string.Empty;
    public bool ForceFullRefresh { get; set; }
    public bool TriggeredByScheduler { get; set; }
    public Guid? TriggeredByUserId { get; set; }
}
