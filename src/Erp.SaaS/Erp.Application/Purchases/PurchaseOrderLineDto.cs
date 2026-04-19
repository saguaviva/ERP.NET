namespace Erp.Application.Purchases;

public sealed class PurchaseOrderLineDto
{
    public int LineNumber { get; set; }
    public string ItemCode { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public string UnitOfMeasure { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public DateTime? ExpectedDate { get; set; }
    public string Notes { get; set; } = string.Empty;
    public decimal ReceivedQuantity { get; set; }
    public DateTime? LastReceivedUtc { get; set; }
    public decimal PendingQuantity => Math.Max(0m, Quantity - ReceivedQuantity);
    public bool IsFullyReceived => PendingQuantity <= 0;
    public decimal LineTotal => decimal.Round(Quantity * UnitPrice, 2, MidpointRounding.AwayFromZero);
}
