namespace Erp.Application.Sales;

public sealed class ImportLegacySalesOrdersCommand
{
    public Guid TenantId { get; set; }
    public Guid? CompanyId { get; set; }
}
