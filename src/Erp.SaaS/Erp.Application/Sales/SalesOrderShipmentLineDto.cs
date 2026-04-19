namespace Erp.Application.Sales;

public sealed class SalesOrderShipmentLineDto
{
    public int LineNumber { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal ShippedQuantity { get; set; }
}
