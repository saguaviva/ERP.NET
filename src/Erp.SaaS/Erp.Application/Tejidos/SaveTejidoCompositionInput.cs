namespace Erp.Application.Tejidos;

public sealed class SaveTejidoCompositionInput
{
    public int LineNumber { get; set; }
    public string ComponentCode { get; set; } = string.Empty;
    public int Percentage { get; set; }
    public int SupplierCode { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Amount { get; set; }
}
