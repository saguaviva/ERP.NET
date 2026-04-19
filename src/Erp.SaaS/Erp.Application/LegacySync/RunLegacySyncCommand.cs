namespace Erp.Application.LegacySync;

public sealed class RunLegacySyncCommand
{
    public Guid TenantId { get; set; }
    public Guid? CompanyId { get; set; }
    public string ModuleKey { get; set; } = string.Empty;
    public bool ForceFullRefresh { get; set; }
    public bool TriggeredByScheduler { get; set; }
}
