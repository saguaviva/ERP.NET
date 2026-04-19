namespace Erp.Application.Purchases;

public sealed class PurchaseOrderLineInputDto
{
    public int LineNumber { get; set; }
    public string ItemCode { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public string UnitOfMeasure { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public DateTime? ExpectedDate { get; set; }
    public string Notes { get; set; } = string.Empty;
}
