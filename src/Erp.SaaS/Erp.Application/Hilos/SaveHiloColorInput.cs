namespace Erp.Application.Hilos;

public sealed class SaveHiloColorInput
{
    public int LineNumber { get; set; }
    public int SupplierCode { get; set; }
    public string Color { get; set; } = string.Empty;
    public decimal CurrentStock { get; set; }
    public decimal MinimumStock { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal CostPrice { get; set; }
    public decimal DyeingPrice { get; set; }
    public decimal Meters { get; set; }
    public decimal Kilograms { get; set; }
    public string Notes { get; set; } = string.Empty;
}
