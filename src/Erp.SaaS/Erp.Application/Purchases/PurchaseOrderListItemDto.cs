namespace Erp.Application.Purchases;

public sealed class PurchaseOrderListItemDto
{
    public int OrderNumber { get; set; }
    public int SupplierCode { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public DateTime DocumentDate { get; set; }
    public DateTime? ExpectedDate { get; set; }
    public string Status { get; set; } = PurchaseOrderStatuses.Draft;
    public int LineCount { get; set; }
    public decimal TotalAmount { get; set; }
    public string Notes { get; set; } = string.Empty;
}
