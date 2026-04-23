namespace Erp.Application.Sales;

public sealed class SalesRemittanceInvoiceDto
{
    public int LineNumber { get; set; }
    public Guid InvoiceId { get; set; }
    public string InvoiceSeries { get; set; } = string.Empty;
    public int InvoiceNumber { get; set; }
    public int ClientCode { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public DateTime IssueDate { get; set; }
    public DateTime? DueDate { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal AmountPaid { get; set; }
    public decimal OutstandingAmount { get; set; }
    public string PaymentStatus { get; set; } = SalesInvoicePaymentStatuses.Pending;
    public string Notes { get; set; } = string.Empty;

    public string DisplayNumber => string.IsNullOrWhiteSpace(InvoiceSeries)
        ? InvoiceNumber.ToString()
        : $"{InvoiceSeries}/{InvoiceNumber:000000}";
}
