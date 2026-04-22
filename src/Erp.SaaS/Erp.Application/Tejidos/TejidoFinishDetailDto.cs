namespace Erp.Application.Tejidos;

public sealed class TejidoFinishDetailDto
{
    public int LineNumber { get; set; }
    public string FinishCode { get; set; } = string.Empty;
    public int SupplierCode { get; set; }
    public int Order { get; set; }
    public decimal PricePerMeter { get; set; }
    public decimal PricePerKilogram { get; set; }
}
