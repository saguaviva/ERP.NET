namespace Erp.Application.Hilos;

public sealed class HiloListItemDto
{
    public string Code { get; init; } = string.Empty;
    public string CompanyCenterCode { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public int SupplierCode { get; init; }
    public decimal CostPrice { get; init; }
    public decimal UnitPrice { get; init; }
    public string VatCode { get; init; } = string.Empty;
    public string Notes { get; init; } = string.Empty;
}
