namespace Erp.Application.Sales;

public sealed class PendingSalesShipmentDto
{
    public Guid ShipmentId { get; set; }
    public string ShipmentSeries { get; set; } = string.Empty;
    public int ShipmentNumber { get; set; }
    public int OrderNumber { get; set; }
    public int ClientCode { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public string ClientTaxId { get; set; } = string.Empty;
    public DateTime ShipmentDate { get; set; }
    public string Warehouse { get; set; } = string.Empty;
    public DateTime? InvoiceReadyUtc { get; set; }
    public string Notes { get; set; } = string.Empty;
    public decimal TotalShippedQuantity { get; set; }
    public decimal EstimatedAmount { get; set; }

    public string DisplayNumber => string.IsNullOrWhiteSpace(ShipmentSeries)
        ? ShipmentNumber.ToString()
        : $"{ShipmentSeries}/{ShipmentNumber:000000}";
}
