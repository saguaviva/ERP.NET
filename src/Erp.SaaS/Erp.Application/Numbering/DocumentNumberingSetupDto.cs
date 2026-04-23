namespace Erp.Application.Numbering;

public sealed class DocumentNumberingSetupDto
{
    public IReadOnlyCollection<DocumentNumberingSequenceDto> Sequences { get; set; } = [];
    public string DispositionYear { get; set; } = string.Empty;
    public string SuggestedDispositionYear { get; set; } = string.Empty;
    public int DispositionNextNumber { get; set; }
    public int SuggestedDispositionNextNumber { get; set; }
}
