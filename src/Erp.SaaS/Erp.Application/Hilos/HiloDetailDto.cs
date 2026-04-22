namespace Erp.Application.Hilos;

public sealed class HiloDetailDto
{
    public string Code { get; set; } = string.Empty;
    public string CompanyCenterCode { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int SupplierCode { get; set; }
    public decimal CostPrice { get; set; }
    public decimal UnitPrice { get; set; }
    public string VatCode { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public List<HiloColorDetailDto> Colors { get; set; } = [];
}
