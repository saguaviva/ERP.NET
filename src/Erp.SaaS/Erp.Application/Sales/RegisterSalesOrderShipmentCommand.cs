namespace Erp.Application.Sales;

public sealed class RegisterSalesOrderShipmentCommand
{
    public Guid TenantId { get; set; }
    public Guid CompanyId { get; set; }
    public int OrderNumber { get; set; }
    public DateTime ShipmentDate { get; set; } = DateTime.Today;
    public string Warehouse { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public List<RegisterSalesOrderShipmentLineDto> Lines { get; set; } = [];
}
