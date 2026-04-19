namespace Erp.Application.Fornituras;

public sealed class FornituraListItemDto
{
    public string Code { get; init; } = string.Empty;
    public string CompanyCenterCode { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal UnitPrice { get; init; }
    public int SupplierCode { get; init; }
    public int ClientCode { get; init; }
    public string Model { get; init; } = string.Empty;
    public string Series { get; init; } = string.Empty;
    public string Season { get; init; } = string.Empty;
}
