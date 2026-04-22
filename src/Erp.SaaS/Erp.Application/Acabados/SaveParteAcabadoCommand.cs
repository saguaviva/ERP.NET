namespace Erp.Application.Acabados;

public sealed class SaveParteAcabadoCommand
{
    public Guid TenantId { get; set; }
    public Guid CompanyId { get; set; }
    public int? OrderNumber { get; set; }
    public DateTime? Date { get; set; }
    public string Status { get; set; } = ParteAcabadoStatuses.Pending;
    public int ClientCode { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public int FinisherCode { get; set; }
    public string FinisherName { get; set; } = string.Empty;
    public int MachineCode { get; set; }
    public string MachineName { get; set; } = string.Empty;
    public int OperationCode { get; set; }
    public string OperationName { get; set; } = string.Empty;
    public int? DispositionCode { get; set; }
    public string DispositionLabel { get; set; } = string.Empty;
    public string SourceSampleKind { get; set; } = string.Empty;
    public string SourceSampleCode { get; set; } = string.Empty;
    public int? SourceSampleLineNumber { get; set; }
    public string Notes { get; set; } = string.Empty;
    public List<SaveParteAcabadoLineInput> Lines { get; set; } = [];
}
