namespace Erp.Application.Sales;

public sealed class SalesInvoiceDraftDto
{
    public Guid DraftId { get; set; }
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
    public string Status { get; set; } = SalesInvoiceDraftStatuses.Draft;
    public string Notes { get; set; } = string.Empty;
    public Guid? InvoiceId { get; set; }
    public string InvoiceSeries { get; set; } = string.Empty;
    public int? InvoiceNumber { get; set; }
    public DateTime? IssuedUtc { get; set; }
    public int ShipmentCount { get; set; }
    public decimal TotalQuantity { get; set; }
    public decimal TotalAmount { get; set; }
    public IReadOnlyCollection<SalesInvoiceDraftShipmentDto> Shipments { get; set; } = [];
    public IReadOnlyCollection<SalesInvoiceDraftLineDto> Lines { get; set; } = [];

    public string DisplayNumber => string.IsNullOrWhiteSpace(DraftSeries)
        ? DraftNumber.ToString()
        : $"{DraftSeries}/{DraftNumber:000000}";

    public string? IssuedInvoiceDisplayNumber => InvoiceNumber.HasValue
        ? (string.IsNullOrWhiteSpace(InvoiceSeries)
            ? InvoiceNumber.Value.ToString()
            : $"{InvoiceSeries}/{InvoiceNumber.Value:000000}")
        : null;
}
