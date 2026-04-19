namespace Erp.Application.LegacySync;

public sealed class LegacySyncModuleRunResult
{
    public int RecordsInserted { get; set; }
    public int RecordsUpdated { get; set; }
    public int RecordsSkipped { get; set; }
    public string? NewCheckpointValue { get; set; }
    public string Summary { get; set; } = string.Empty;
    public IReadOnlyCollection<LegacySyncMappingRecord> Mappings { get; set; } = [];
    public IReadOnlyCollection<LegacySyncErrorRecord> Errors { get; set; } = [];
}
