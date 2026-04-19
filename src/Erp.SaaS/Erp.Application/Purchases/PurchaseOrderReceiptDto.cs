namespace Erp.Application.Purchases;

public sealed class PurchaseOrderReceiptDto
{
    public Guid ReceiptId { get; set; }
    public string ReceiptSeries { get; set; } = string.Empty;
    public int ReceiptNumber { get; set; }
    public int OrderNumber { get; set; }
    public int SupplierCode { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public string SupplierTaxId { get; set; } = string.Empty;
    public string SupplierAddress { get; set; } = string.Empty;
    public string SupplierPostalCode { get; set; } = string.Empty;
    public string SupplierCity { get; set; } = string.Empty;
    public string SupplierProvince { get; set; } = string.Empty;
    public string SupplierCountry { get; set; } = string.Empty;
    public string TenantName { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string CompanyLegacyCenterCode { get; set; } = string.Empty;
    public DateTime ReceiptDate { get; set; }
    public string Warehouse { get; set; } = string.Empty;
    public string Carrier { get; set; } = string.Empty;
    public string SupplierReference { get; set; } = string.Empty;
    public string VehiclePlate { get; set; } = string.Empty;
    public int? PackageCount { get; set; }
    public decimal? GrossWeightKg { get; set; }
    public string Notes { get; set; } = string.Empty;
    public decimal TotalReceivedQuantity { get; set; }
    public IReadOnlyCollection<PurchaseOrderReceiptLineDto> Lines { get; set; } = [];
    public string DisplayNumber => string.IsNullOrWhiteSpace(ReceiptSeries)
        ? ReceiptNumber.ToString()
        : $"{ReceiptSeries}/{ReceiptNumber:000000}";
}
