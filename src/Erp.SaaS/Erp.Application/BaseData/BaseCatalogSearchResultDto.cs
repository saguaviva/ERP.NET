namespace Erp.Application.BaseData;

public sealed class BaseCatalogSearchResultDto
{
    public IReadOnlyCollection<BaseCatalogListItemDto> Items { get; set; } = [];
    public int TotalCount { get; set; }
}
