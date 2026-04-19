namespace Erp.Application.Sales;

public sealed class SaveSalesOrderCommand
{
    public Guid TenantId { get; set; }
    public Guid CompanyId { get; set; }
    public int? OrderNumber { get; set; }
    public int ClientCode { get; set; }
    public DateTime DocumentDate { get; set; } = DateTime.Today;
    public DateTime? RequestedDate { get; set; }
    public string Status { get; set; } = SalesOrderStatuses.Draft;
    public string Notes { get; set; } = string.Empty;
    public List<SalesOrderLineInputDto> Lines { get; set; } = [];
}
