namespace Erp.Application.LegacySync;

public sealed class LegacySyncJobSummaryDto
{
    public Guid JobId { get; set; }
    public Guid TenantId { get; set; }
    public Guid CompanyId { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string CompanyLegacyCenterCode { get; set; } = string.Empty;
    public string ModuleKey { get; set; } = string.Empty;
    public string ModuleDisplayName { get; set; } = string.Empty;
    public string Status { get; set; } = LegacySyncJobStatuses.Running;
    public bool TriggeredByScheduler { get; set; }
    public Guid? TriggeredByUserId { get; set; }
    public string CheckpointBefore { get; set; } = string.Empty;
    public string CheckpointAfter { get; set; } = string.Empty;
    public int RecordsInserted { get; set; }
    public int RecordsUpdated { get; set; }
    public int RecordsSkipped { get; set; }
    public int ErrorsCount { get; set; }
    public string Summary { get; set; } = string.Empty;
    public DateTime StartedUtc { get; set; }
    public DateTime? FinishedUtc { get; set; }
}
