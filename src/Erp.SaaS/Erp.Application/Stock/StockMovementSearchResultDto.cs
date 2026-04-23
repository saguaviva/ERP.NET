namespace Erp.Application.Stock;

public sealed class StockMovementSearchResultDto
{
    public IReadOnlyCollection<StockMovementListItemDto> Items { get; set; } = [];
    public IReadOnlyCollection<StockMovementGroupSummaryDto> SupplierSummaries { get; set; } = [];
    public IReadOnlyCollection<StockMovementGroupSummaryDto> ColorSummaries { get; set; } = [];
    public int TotalCount { get; set; }
}
