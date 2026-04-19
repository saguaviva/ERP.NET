namespace Erp.Application.Fornituras;

public sealed class FornituraVariantDto
{
    public int LineNumber { get; set; }
    public int SupplierCode { get; set; }
    public string SupplierItemCode { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string Measure { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public decimal CurrentStock { get; set; }
    public decimal MinimumStock { get; set; }
    public decimal CostPrice { get; set; }
}
