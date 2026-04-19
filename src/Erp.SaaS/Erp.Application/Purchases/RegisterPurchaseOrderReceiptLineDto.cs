namespace Erp.Application.Purchases;

public sealed class RegisterPurchaseOrderReceiptLineDto
{
    public int LineNumber { get; set; }
    public decimal ReceivedQuantity { get; set; }
}
