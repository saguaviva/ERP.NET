namespace Erp.Application.Stock;

public sealed class StockMovementGroupSummaryDto
{
    public string Label { get; set; } = string.Empty;
    public int MovementCount { get; set; }
    public decimal Quantity { get; set; }
}
