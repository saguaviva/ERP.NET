namespace Erp.Application.Purchases;

public sealed class PurchaseInvoiceDetailDto
{
    public string InvoiceSeries { get; set; } = string.Empty;
    public int InvoiceNumber { get; set; }
    public Guid InvoiceId { get; set; }
    public string TenantName { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string CompanyLegacyCenterCode { get; set; } = string.Empty;
    public int SupplierCode { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public string SupplierTaxId { get; set; } = string.Empty;
    public string SupplierAddress { get; set; } = string.Empty;
    public string SupplierPostalCode { get; set; } = string.Empty;
    public string SupplierCity { get; set; } = string.Empty;
    public string SupplierProvince { get; set; } = string.Empty;
    public string SupplierCountry { get; set; } = string.Empty;
    public string SupplierDocumentNumber { get; set; } = string.Empty;
    public DateTime DocumentDate { get; set; } = DateTime.Today;
    public DateTime? DueDate { get; set; }
    public string Status { get; set; } = PurchaseInvoiceStatuses.Draft;
    public string Notes { get; set; } = string.Empty;
    public decimal TotalNetAmount { get; set; }
    public decimal TotalTaxAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal AmountPaid { get; set; }
    public decimal OutstandingAmount { get; set; }
    public DateTime? LastPaymentUtc { get; set; }
    public string Origin { get; set; } = "saas";
    public IReadOnlyCollection<PurchaseInvoiceLineDto> Lines { get; set; } = [];
    public IReadOnlyCollection<PurchaseInvoiceReceiptLinkDto> Receipts { get; set; } = [];
    public IReadOnlyCollection<PurchaseInvoicePaymentDto> Payments { get; set; } = [];

    public string DisplayNumber => string.IsNullOrWhiteSpace(InvoiceSeries)
        ? InvoiceNumber.ToString()
        : $"{InvoiceSeries}/{InvoiceNumber:000000}";
}
