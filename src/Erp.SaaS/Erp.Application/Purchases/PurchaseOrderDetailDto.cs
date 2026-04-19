namespace Erp.Application.Purchases;

public sealed class PurchaseOrderDetailDto
{
    public int OrderNumber { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string CompanyLegacyCenterCode { get; set; } = string.Empty;
    public int SupplierCode { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public string SupplierTaxId { get; set; } = string.Empty;
    public DateTime DocumentDate { get; set; } = DateTime.Today;
    public DateTime? ExpectedDate { get; set; }
    public string Status { get; set; } = PurchaseOrderStatuses.Draft;
    public string Notes { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public decimal TotalReceivedQuantity { get; set; }
    public decimal TotalPendingQuantity { get; set; }
    public IReadOnlyCollection<PurchaseOrderLineDto> Lines { get; set; } = [];
}
