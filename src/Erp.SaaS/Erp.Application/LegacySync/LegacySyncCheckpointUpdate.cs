namespace Erp.Application.LegacySync;

public sealed class LegacySyncCheckpointUpdate
{
    public Guid TenantId { get; set; }
    public Guid CompanyId { get; set; }
    public string ModuleKey { get; set; } = string.Empty;
    public string? CheckpointValue { get; set; }
    public Guid JobId { get; set; }
    public DateTime StartedUtc { get; set; }
    public DateTime? CompletedUtc { get; set; }
    public string Status { get; set; } = LegacySyncJobStatuses.Completed;
    public int RecordsInserted { get; set; }
    public int RecordsUpdated { get; set; }
    public int RecordsSkipped { get; set; }
    public int ErrorsCount { get; set; }
}
