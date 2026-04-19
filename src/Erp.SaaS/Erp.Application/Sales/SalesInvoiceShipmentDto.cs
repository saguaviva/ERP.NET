namespace Erp.Application.Sales;

public sealed class SalesInvoiceShipmentDto
{
    public Guid ShipmentId { get; set; }
    public string ShipmentSeries { get; set; } = string.Empty;
    public int ShipmentNumber { get; set; }
    public int OrderNumber { get; set; }
    public DateTime ShipmentDate { get; set; }
    public string Warehouse { get; set; } = string.Empty;
    public decimal TotalShippedQuantity { get; set; }
    public decimal EstimatedAmount { get; set; }

    public string DisplayNumber => string.IsNullOrWhiteSpace(ShipmentSeries)
        ? ShipmentNumber.ToString()
        : $"{ShipmentSeries}/{ShipmentNumber:000000}";
}
