namespace Erp.Application.Muestras;

public sealed class MuestraLineDto
{
    public int LineNumber { get; set; }
    public string SizeCode { get; set; } = string.Empty;
    public string SizeHigh { get; set; } = string.Empty;
    public string SizeLow { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal CostPrice { get; set; }
    public decimal SalePrice { get; set; }
    public string Color { get; set; } = string.Empty;
    public int ClientCode { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public string NcCode { get; set; } = string.Empty;
    public MuestraBreakdownDto Breakdown { get; set; } = new();
}
