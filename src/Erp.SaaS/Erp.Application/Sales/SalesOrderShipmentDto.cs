namespace Erp.Application.Sales;

public sealed class SalesOrderShipmentDto
{
    public Guid ShipmentId { get; set; }
    public string ShipmentSeries { get; set; } = string.Empty;
    public int ShipmentNumber { get; set; }
    public int OrderNumber { get; set; }
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
    public DateTime ShipmentDate { get; set; }
    public string Warehouse { get; set; } = string.Empty;
    public string InvoiceStatus { get; set; } = "Pending";
    public string InvoiceReference { get; set; } = string.Empty;
    public Guid? InvoiceId { get; set; }
    public string InvoiceSeries { get; set; } = string.Empty;
    public int? InvoiceNumber { get; set; }
    public DateTime? InvoiceReadyUtc { get; set; }
    public string Notes { get; set; } = string.Empty;
    public decimal TotalShippedQuantity { get; set; }
    public IReadOnlyCollection<SalesOrderShipmentLineDto> Lines { get; set; } = [];
    public string DisplayNumber => string.IsNullOrWhiteSpace(ShipmentSeries)
        ? ShipmentNumber.ToString()
        : $"{ShipmentSeries}/{ShipmentNumber:000000}";

    public string? InvoiceDisplayNumber => InvoiceNumber.HasValue
        ? (string.IsNullOrWhiteSpace(InvoiceSeries)
            ? InvoiceNumber.Value.ToString()
            : $"{InvoiceSeries}/{InvoiceNumber.Value:000000}")
        : null;
}
