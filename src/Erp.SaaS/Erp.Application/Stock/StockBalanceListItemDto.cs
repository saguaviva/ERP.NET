namespace Erp.Application.Stock;

public sealed class StockBalanceListItemDto
{
    public string Warehouse { get; set; } = string.Empty;
    public string ItemCode { get; set; } = string.Empty;
    public string ItemDescription { get; set; } = string.Empty;
    public string UnitOfMeasure { get; set; } = string.Empty;
    public decimal CurrentStock { get; set; }
    public int MovementCount { get; set; }
    public DateTime? LastMovementDate { get; set; }
}
