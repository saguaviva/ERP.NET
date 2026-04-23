namespace Erp.Application.Numbering;

public sealed class DocumentNumberingSequenceDto
{
    public string SequenceKey { get; set; } = string.Empty;
    public string Series { get; set; } = string.Empty;
    public int NextNumber { get; set; }
    public int SuggestedNextNumber { get; set; }
    public int LastNumber { get; set; }
    public bool IsActive { get; set; }
    public string Notes { get; set; } = string.Empty;
}
