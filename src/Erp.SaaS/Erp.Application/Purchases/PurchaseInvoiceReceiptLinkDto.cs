namespace Erp.Application.Purchases;

public sealed class PurchaseInvoiceReceiptLinkDto
{
    public Guid? ReceiptId { get; set; }
    public string ReceiptSeries { get; set; } = string.Empty;
    public int ReceiptNumber { get; set; }
    public int OrderNumber { get; set; }
    public DateTime ReceiptDate { get; set; }
    public decimal TotalReceivedQuantity { get; set; }

    public string DisplayNumber => string.IsNullOrWhiteSpace(ReceiptSeries)
        ? ReceiptNumber.ToString()
        : $"{ReceiptSeries}/{ReceiptNumber:000000}";
}
