namespace Erp.Application.Sales;

public sealed class SalesInvoiceListItemDto
{
    public Guid InvoiceId { get; set; }
    public string InvoiceSeries { get; set; } = string.Empty;
    public int InvoiceNumber { get; set; }
    public int DraftNumber { get; set; }
    public int ClientCode { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public DateTime IssueDate { get; set; }
    public DateTime? DueDate { get; set; }
    public string Status { get; set; } = SalesInvoiceStatuses.Issued;
    public string Origin { get; set; } = SalesOrderOrigins.Local;
    public string PaymentStatus { get; set; } = SalesInvoicePaymentStatuses.Pending;
    public int ShipmentCount { get; set; }
    public decimal TotalQuantity { get; set; }
    public decimal SubtotalAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal AmountPaid { get; set; }
    public decimal OutstandingAmount { get; set; }
    public string AccountingStatus { get; set; } = SalesInvoiceAccountingStatuses.Pending;
    public string Notes { get; set; } = string.Empty;

    public string DisplayNumber => string.IsNullOrWhiteSpace(InvoiceSeries)
        ? InvoiceNumber.ToString()
        : $"{InvoiceSeries}/{InvoiceNumber:000000}";

    public bool IsOverdue =>
        !string.Equals(Status, SalesInvoiceStatuses.Cancelled, StringComparison.OrdinalIgnoreCase) &&
        !string.Equals(PaymentStatus, SalesInvoicePaymentStatuses.Paid, StringComparison.OrdinalIgnoreCase) &&
        DueDate.HasValue &&
        DueDate.Value.Date < DateTime.Today;
}
