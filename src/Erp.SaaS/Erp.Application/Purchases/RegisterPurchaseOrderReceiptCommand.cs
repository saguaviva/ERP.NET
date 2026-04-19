namespace Erp.Application.Purchases;

public sealed class RegisterPurchaseOrderReceiptCommand
{
    public Guid TenantId { get; set; }
    public Guid CompanyId { get; set; }
    public int OrderNumber { get; set; }
    public DateTime ReceiptDate { get; set; } = DateTime.Today;
    public string Warehouse { get; set; } = string.Empty;
    public string Carrier { get; set; } = string.Empty;
    public string SupplierReference { get; set; } = string.Empty;
    public string VehiclePlate { get; set; } = string.Empty;
    public int? PackageCount { get; set; }
    public decimal? GrossWeightKg { get; set; }
    public string Notes { get; set; } = string.Empty;
    public List<RegisterPurchaseOrderReceiptLineDto> Lines { get; set; } = [];
}
