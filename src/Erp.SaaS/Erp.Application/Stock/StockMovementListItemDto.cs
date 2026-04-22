namespace Erp.Application.Stock;

public sealed class StockMovementListItemDto
{
    public Guid MovementId { get; set; }
    public DateTime MovementDate { get; set; }
    public string MovementType { get; set; } = string.Empty;
    public string Warehouse { get; set; } = string.Empty;
    public string ItemCode { get; set; } = string.Empty;
    public string ItemDescription { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public string UnitOfMeasure { get; set; } = string.Empty;
    public string SupplierName { get; set; } = string.Empty;
    public string SourceDocumentType { get; set; } = string.Empty;
    public int? SourceDocumentNumber { get; set; }
    public string SourceDocumentDisplay { get; set; } = string.Empty;
    public string SupplierReference { get; set; } = string.Empty;
    public string VehiclePlate { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
}
