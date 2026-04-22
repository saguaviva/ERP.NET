namespace Erp.Application.Models;

public sealed class ModeloScandalloLineDto
{
    public int LineNumber { get; set; }
    public string Title { get; set; } = string.Empty;
    public string FabricCode { get; set; } = string.Empty;
    public decimal Consumption { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal CostPrice { get; set; }
}
