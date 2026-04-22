namespace Erp.Application.Acabados;

public sealed class ParteAcabadoListItemDto
{
    public int OrderNumber { get; set; }
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
    public string SourceSampleKind { get; set; } = string.Empty;
    public string SourceSampleCode { get; set; } = string.Empty;
    public int? SourceSampleLineNumber { get; set; }
    public string PrimaryFabricCode { get; set; } = string.Empty;
    public string PrimaryFabricDescription { get; set; } = string.Empty;
    public string PrimaryColor { get; set; } = string.Empty;
    public decimal TotalKilograms { get; set; }
    public decimal TotalPieces { get; set; }
    public int LinesCount { get; set; }
    public string Origin { get; set; } = string.Empty;
}
