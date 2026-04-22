namespace Erp.Application.Tejidos;

public sealed class TejidoColorDetailDto
{
    public int LineNumber { get; set; }
    public int SupplierCode { get; set; }
    public string Color { get; set; } = string.Empty;
    public decimal CurrentStock { get; set; }
    public decimal MinimumStock { get; set; }
    public decimal DyeingPrice { get; set; }
    public decimal UnitCost { get; set; }
    public decimal MetersPrice { get; set; }
    public decimal KilogramsPrice { get; set; }
    public string Notes { get; set; } = string.Empty;
}
