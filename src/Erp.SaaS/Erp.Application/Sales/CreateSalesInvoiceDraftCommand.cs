namespace Erp.Application.Sales;

public sealed class CreateSalesInvoiceDraftCommand
{
    public Guid TenantId { get; set; }
    public Guid CompanyId { get; set; }
    public DateTime IssueDate { get; set; } = DateTime.Today;
    public DateTime? DueDate { get; set; }
    public string Notes { get; set; } = string.Empty;
    public List<Guid> ShipmentIds { get; set; } = [];
}
