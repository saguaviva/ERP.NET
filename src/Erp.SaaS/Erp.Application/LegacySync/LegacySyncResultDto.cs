namespace Erp.Application.LegacySync;

public sealed class LegacySyncResultDto
{
    public string ModuleKey { get; set; } = string.Empty;
    public string ModuleDisplayName { get; set; } = string.Empty;
    public Guid TenantId { get; set; }
    public Guid? CompanyId { get; set; }
    public int CompaniesProcessed { get; set; }
    public int RecordsInserted { get; set; }
    public int RecordsUpdated { get; set; }
    public int RecordsSkipped { get; set; }
    public int ErrorsCount { get; set; }
    public DateTime StartedUtc { get; set; }
    public DateTime FinishedUtc { get; set; }
    public IReadOnlyCollection<LegacySyncJobSummaryDto> Jobs { get; set; } = [];
}
