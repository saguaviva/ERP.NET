namespace Erp.Application.Purchases;

public sealed class PurchaseOrderReceiptLineDto
{
    public int LineNumber { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal ReceivedQuantity { get; set; }
}
