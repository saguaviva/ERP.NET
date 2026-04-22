namespace Erp.Application.Stock;

public sealed class StockTransferSearchResultDto
{
    public IReadOnlyCollection<StockTransferListItemDto> Items { get; set; } = [];
    public int TotalCount { get; set; }
}
