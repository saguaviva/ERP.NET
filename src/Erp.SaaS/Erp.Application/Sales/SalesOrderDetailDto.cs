namespace Erp.Application.Sales;

public sealed class SalesOrderDetailDto
{
    public int OrderNumber { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string CompanyLegacyCenterCode { get; set; } = string.Empty;
    public int ClientCode { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public string ClientTaxId { get; set; } = string.Empty;
    public DateTime DocumentDate { get; set; } = DateTime.Today;
    public DateTime? RequestedDate { get; set; }
    public string Status { get; set; } = SalesOrderStatuses.Draft;
    public string Origin { get; set; } = SalesOrderOrigins.Saas;
    public DateTime? SyncedUtc { get; set; }
    public string Notes { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public decimal TotalShippedQuantity { get; set; }
    public decimal TotalPendingQuantity { get; set; }
    public IReadOnlyCollection<SalesOrderLineDto> Lines { get; set; } = [];
}
