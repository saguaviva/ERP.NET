namespace Erp.Application.Stock;

public sealed class CreateStockAdjustmentCommand
{
    public Guid TenantId { get; set; }
    public Guid CompanyId { get; set; }
    public string Warehouse { get; set; } = string.Empty;
    public string ItemCode { get; set; } = string.Empty;
    public string ItemDescription { get; set; } = string.Empty;
    public string UnitOfMeasure { get; set; } = string.Empty;
    public string AdjustmentType { get; set; } = StockMovementTypes.InboundManualAdjustment;
    public decimal Quantity { get; set; }
    public DateTime MovementDate { get; set; } = DateTime.Today;
    public string Notes { get; set; } = string.Empty;
}
