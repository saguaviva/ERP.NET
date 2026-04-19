namespace Erp.Application.Purchases;

public sealed class SavePurchaseOrderCommand
{
    public Guid TenantId { get; set; }
    public Guid CompanyId { get; set; }
    public int? OrderNumber { get; set; }
    public int SupplierCode { get; set; }
    public DateTime DocumentDate { get; set; } = DateTime.Today;
    public DateTime? ExpectedDate { get; set; }
    public string Status { get; set; } = PurchaseOrderStatuses.Draft;
    public string Notes { get; set; } = string.Empty;
    public List<PurchaseOrderLineInputDto> Lines { get; set; } = [];
}
