namespace Erp.Application.Stock;

public sealed class StockBalanceSearchResultDto
{
    public IReadOnlyCollection<StockBalanceListItemDto> Items { get; set; } = [];
    public int TotalCount { get; set; }
}
