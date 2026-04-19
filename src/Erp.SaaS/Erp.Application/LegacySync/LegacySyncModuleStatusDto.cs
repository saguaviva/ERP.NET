using System;

namespace Erp.Application.LegacySync;

public sealed class LegacySyncModuleStatusDto
{
    public Guid TenantId { get; set; }
    public Guid CompanyId { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string CompanyLegacyCenterCode { get; set; } = string.Empty;
    public string ModuleKey { get; set; } = string.Empty;
    public string ModuleDisplayName { get; set; } = string.Empty;
    public string LastStatus { get; set; } = string.Empty;
    public string CheckpointValue { get; set; } = string.Empty;
    public DateTime? LastCompletedUtc { get; set; }
    public int LastInserted { get; set; }
    public int LastUpdated { get; set; }
    public int LastSkipped { get; set; }
    public int LastErrors { get; set; }
    public bool HasSuccessfulSync =>
        string.Equals(LastStatus, LegacySyncJobStatuses.Completed, StringComparison.OrdinalIgnoreCase) ||
        string.Equals(LastStatus, LegacySyncJobStatuses.CompletedWithErrors, StringComparison.OrdinalIgnoreCase);
}
