namespace Erp.Application.BaseData;

public sealed class BaseCatalogListItemDto
{
    public string CatalogKey { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Reference { get; set; } = string.Empty;
    public string SecondaryReference { get; set; } = string.Empty;
    public decimal? NumericValue { get; set; }
    public decimal? SecondaryNumericValue { get; set; }
    public bool IsActive { get; set; } = true;
    public string Origin { get; set; } = "local";
}
