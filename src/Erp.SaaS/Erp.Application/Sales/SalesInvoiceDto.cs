namespace Erp.Application.Sales;

public sealed class SalesInvoiceDto
{
    public Guid InvoiceId { get; set; }
    public Guid DraftId { get; set; }
    public string InvoiceSeries { get; set; } = string.Empty;
    public int InvoiceNumber { get; set; }
    public string DraftSeries { get; set; } = string.Empty;
    public int DraftNumber { get; set; }
    public int ClientCode { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public string ClientTaxId { get; set; } = string.Empty;
    public string ClientAddress { get; set; } = string.Empty;
    public string ClientPostalCode { get; set; } = string.Empty;
    public string ClientCity { get; set; } = string.Empty;
    public string ClientProvince { get; set; } = string.Empty;
    public string ClientCountry { get; set; } = string.Empty;
    public string TenantName { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string CompanyLegacyCenterCode { get; set; } = string.Empty;
    public DateTime IssueDate { get; set; }
    public DateTime? DueDate { get; set; }
    public string Status { get; set; } = SalesInvoiceStatuses.Issued;
    public string Origin { get; set; } = SalesOrderOrigins.Saas;
    public string PaymentStatus { get; set; } = SalesInvoicePaymentStatuses.Pending;
    public int ShipmentCount { get; set; }
    public decimal TotalQuantity { get; set; }
    public decimal SubtotalAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal AmountPaid { get; set; }
    public decimal OutstandingAmount { get; set; }
    public DateTime? LastPaymentUtc { get; set; }
    public string AccountingStatus { get; set; } = SalesInvoiceAccountingStatuses.Pending;
    public string AccountingReference { get; set; } = string.Empty;
    public DateTime? AccountingReadyUtc { get; set; }
    public string Notes { get; set; } = string.Empty;
    public DateTime IssuedUtc { get; set; }
    public IReadOnlyCollection<SalesInvoiceShipmentDto> Shipments { get; set; } = [];
    public IReadOnlyCollection<SalesInvoiceLineDto> Lines { get; set; } = [];
    public IReadOnlyCollection<SalesInvoicePaymentDto> Payments { get; set; } = [];

    public string DisplayNumber => string.IsNullOrWhiteSpace(InvoiceSeries)
        ? InvoiceNumber.ToString()
        : $"{InvoiceSeries}/{InvoiceNumber:000000}";

    public string DraftDisplayNumber => string.IsNullOrWhiteSpace(DraftSeries)
        ? DraftNumber.ToString()
        : $"{DraftSeries}/{DraftNumber:000000}";

    public bool IsOverdue =>
        !string.Equals(Status, SalesInvoiceStatuses.Cancelled, StringComparison.OrdinalIgnoreCase) &&
        !string.Equals(PaymentStatus, SalesInvoicePaymentStatuses.Paid, StringComparison.OrdinalIgnoreCase) &&
        DueDate.HasValue &&
        DueDate.Value.Date < DateTime.Today;
}
