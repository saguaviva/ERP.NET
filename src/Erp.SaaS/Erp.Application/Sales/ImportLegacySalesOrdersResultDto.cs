namespace Erp.Application.Sales;

public sealed class ImportLegacySalesOrdersResultDto
{
    public int CompaniesProcessed { get; set; }
    public int OrdersInserted { get; set; }
    public int OrdersUpdated { get; set; }
    public int OrdersSkipped { get; set; }
    public int LinesImported { get; set; }
}
