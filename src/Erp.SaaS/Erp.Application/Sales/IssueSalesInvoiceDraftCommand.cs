namespace Erp.Application.Sales;

public sealed class IssueSalesInvoiceDraftCommand
{
    public Guid TenantId { get; set; }
    public Guid CompanyId { get; set; }
    public int DraftNumber { get; set; }
}
