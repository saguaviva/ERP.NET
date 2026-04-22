namespace Erp.Application.Intrastat;

public sealed class IntrastatLineDto
{
    public string InvoiceSeries { get; set; } = string.Empty;
    public int InvoiceNumber { get; set; }
    public int LineNumber { get; set; }
    public DateTime IssueDate { get; set; }
    public int ClientCode { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public string ClientTaxId { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;
    public string CountryName { get; set; } = string.Empty;
    public string IntrastatCode { get; set; } = string.Empty;
    public string ItemCode { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Composition { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal UnitWeight { get; set; }
    public decimal TotalWeightKg { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal PaymentDiscountPercent { get; set; }
    public decimal NetAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal GrossAmount { get; set; }
    public string Origin { get; set; } = string.Empty;
    public bool IsTransportCharge { get; set; }

    public bool IsClassified => !string.IsNullOrWhiteSpace(IntrastatCode);

    public string DisplayInvoiceNumber => string.IsNullOrWhiteSpace(InvoiceSeries)
        ? InvoiceNumber.ToString()
        : $"{InvoiceSeries}/{InvoiceNumber:000000}";
}
