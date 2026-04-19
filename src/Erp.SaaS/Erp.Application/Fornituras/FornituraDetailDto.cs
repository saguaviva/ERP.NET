namespace Erp.Application.Fornituras;

public sealed class FornituraDetailDto
{
    public string Code { get; set; } = string.Empty;
    public string CompanyCenterCode { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int SupplierCode { get; set; }
    public string SupplierReference { get; set; } = string.Empty;
    public int ClientCode { get; set; }
    public string Model { get; set; } = string.Empty;
    public string Series { get; set; } = string.Empty;
    public string Season { get; set; } = string.Empty;
    public List<FornituraVariantDto> Variants { get; set; } = [];
}
