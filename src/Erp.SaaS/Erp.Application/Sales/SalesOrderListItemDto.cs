namespace Erp.Application.Sales;

public sealed class SalesOrderListItemDto
{
    public int OrderNumber { get; set; }
    public int ClientCode { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public DateTime DocumentDate { get; set; }
    public DateTime? RequestedDate { get; set; }
    public string Status { get; set; } = SalesOrderStatuses.Draft;
    public string Origin { get; set; } = SalesOrderOrigins.Saas;
    public DateTime? SyncedUtc { get; set; }
    public int LineCount { get; set; }
    public decimal TotalAmount { get; set; }
    public string Notes { get; set; } = string.Empty;
}
