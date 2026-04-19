namespace Erp.Application.Purchases;

public sealed class PurchaseReceiptListItemDto
{
    public string ReceiptSeries { get; set; } = string.Empty;
    public int ReceiptNumber { get; set; }
    public int OrderNumber { get; set; }
    public int SupplierCode { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public DateTime ReceiptDate { get; set; }
    public string Warehouse { get; set; } = string.Empty;
    public string Carrier { get; set; } = string.Empty;
    public string SupplierReference { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public decimal TotalReceivedQuantity { get; set; }
    public int LineCount { get; set; }
    public string DisplayNumber => string.IsNullOrWhiteSpace(ReceiptSeries)
        ? ReceiptNumber.ToString()
        : $"{ReceiptSeries}/{ReceiptNumber:000000}";
}
