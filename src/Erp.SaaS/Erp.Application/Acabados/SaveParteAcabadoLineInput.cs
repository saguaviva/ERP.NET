namespace Erp.Application.Acabados;

public sealed class SaveParteAcabadoLineInput
{
    public int LineNumber { get; set; }
    public string FabricCode { get; set; } = string.Empty;
    public string FabricDescription { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public decimal TotalKilograms { get; set; }
    public decimal TotalPieces { get; set; }
    public string Status { get; set; } = ParteAcabadoStatuses.Pending;
    public string Notes { get; set; } = string.Empty;
}
