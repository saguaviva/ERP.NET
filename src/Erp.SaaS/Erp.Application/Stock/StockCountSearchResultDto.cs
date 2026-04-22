namespace Erp.Application.Stock;

public sealed class StockCountSearchResultDto
{
    public IReadOnlyCollection<StockCountListItemDto> Items { get; set; } = [];
    public int TotalCount { get; set; }
}
