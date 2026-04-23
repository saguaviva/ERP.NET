namespace Erp.Application.Numbering;

public sealed class SaveDocumentNumberingSetupCommand
{
    public Guid TenantId { get; set; }
    public Guid CompanyId { get; set; }
    public string DispositionYear { get; set; } = string.Empty;
    public int DispositionNextNumber { get; set; }
    public List<SaveDocumentNumberingSequenceInput> Sequences { get; set; } = [];
}
