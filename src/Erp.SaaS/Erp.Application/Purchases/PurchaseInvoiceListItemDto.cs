namespace Erp.Application.Purchases;

public sealed class PurchaseInvoiceListItemDto
{
    public string InvoiceSeries { get; set; } = string.Empty;
    public int InvoiceNumber { get; set; }
    public int SupplierCode { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public string SupplierDocumentNumber { get; set; } = string.Empty;
    public DateTime DocumentDate { get; set; }
    public DateTime? DueDate { get; set; }
    public string Status { get; set; } = PurchaseInvoiceStatuses.Draft;
    public int ReceiptCount { get; set; }
    public int OrderCount { get; set; }
    public int LineCount { get; set; }
    public decimal TotalNetAmount { get; set; }
    public decimal TotalTaxAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal AmountPaid { get; set; }
    public decimal OutstandingAmount { get; set; }
    public DateTime? LastPaymentUtc { get; set; }
    public bool IsReconciled { get; set; }
    public int ReconciliationDifferenceCount { get; set; }
    public decimal ReconciliationDifferenceAmount { get; set; }
    public string Origin { get; set; } = "saas";

    public string DisplayNumber => string.IsNullOrWhiteSpace(InvoiceSeries)
        ? InvoiceNumber.ToString()
        : $"{InvoiceSeries}/{InvoiceNumber:000000}";
}
