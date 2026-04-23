namespace Erp.Application.Numbering;

public sealed class SaveDocumentNumberingSequenceInput
{
    public string SequenceKey { get; set; } = string.Empty;
    public string Series { get; set; } = string.Empty;
    public int NextNumber { get; set; }
    public bool IsActive { get; set; } = true;
    public string Notes { get; set; } = string.Empty;
}
