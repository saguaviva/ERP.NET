namespace Erp.Application.LegacySync;

public sealed class LegacySyncMappingRecord
{
    public string LegacySourceSystem { get; set; } = "legacy";
    public string LegacyCenterCode { get; set; } = string.Empty;
    public string LegacyDocumentType { get; set; } = string.Empty;
    public string LegacyDocumentNumber { get; set; } = string.Empty;
    public int? LegacyLineNumber { get; set; }
    public string TargetEntityName { get; set; } = string.Empty;
    public string TargetEntityId { get; set; } = string.Empty;
}
