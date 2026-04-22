namespace Erp.Application.Muestras;

public sealed class SaveMuestraBreakdownInput
{
    public int SampleLineNumber { get; set; }
    public DateTime? WorkDate { get; set; }
    public int ClientCode { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public int MachineCode { get; set; }
    public string MachineName { get; set; } = string.Empty;
    public int OperationCode { get; set; }
    public string OperationName { get; set; } = string.Empty;
    public decimal Needles { get; set; }
    public decimal Speed { get; set; }
    public string Disk { get; set; } = string.Empty;
    public decimal TimeMinutes { get; set; }
    public decimal MachineRate { get; set; }
    public string Cuts { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public List<SaveMuestraBreakdownLineInput> Lines { get; set; } = [];
}
