namespace Erp.Application.BaseData;

public sealed class BaseCatalogFilter
{
    public string Search { get; set; } = string.Empty;
    public bool IncludeInactive { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 25;
}
