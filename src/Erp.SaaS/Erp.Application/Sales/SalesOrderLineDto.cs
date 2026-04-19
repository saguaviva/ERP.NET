namespace Erp.Application.Sales;

public sealed class SalesOrderLineDto
{
    public int LineNumber { get; set; }
    public string ItemCode { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public string UnitOfMeasure { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public DateTime? RequestedDate { get; set; }
    public string Notes { get; set; } = string.Empty;
    public decimal ShippedQuantity { get; set; }
    public DateTime? LastShippedUtc { get; set; }
    public decimal PendingQuantity => Math.Max(0m, Quantity - ShippedQuantity);
    public bool IsFullyShipped => PendingQuantity <= 0;
    public decimal LineTotal => decimal.Round(Quantity * UnitPrice, 2, MidpointRounding.AwayFromZero);
}
