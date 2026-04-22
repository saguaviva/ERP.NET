namespace Erp.Application.BaseData;

public sealed class SaveBaseCatalogItemCommand
{
    public Guid TenantId { get; set; }
    public Guid CompanyId { get; set; }
    public string CatalogKey { get; set; } = string.Empty;
    public string? OriginalCode { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Reference { get; set; } = string.Empty;
    public string SecondaryReference { get; set; } = string.Empty;
    public decimal? NumericValue { get; set; }
    public decimal? SecondaryNumericValue { get; set; }
    public string Notes { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}
