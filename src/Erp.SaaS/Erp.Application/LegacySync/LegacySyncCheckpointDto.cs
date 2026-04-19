namespace Erp.Application.LegacySync;

public sealed class LegacySyncCheckpointDto
{
    public Guid TenantId { get; set; }
    public Guid CompanyId { get; set; }
    public string ModuleKey { get; set; } = string.Empty;
    public string? CheckpointValue { get; set; }
    public Guid? LastSuccessfulJobId { get; set; }
    public DateTime? LastStartedUtc { get; set; }
    public DateTime? LastCompletedUtc { get; set; }
    public string LastStatus { get; set; } = string.Empty;
    public int LastInserted { get; set; }
    public int LastUpdated { get; set; }
    public int LastSkipped { get; set; }
    public int LastErrors { get; set; }
}
