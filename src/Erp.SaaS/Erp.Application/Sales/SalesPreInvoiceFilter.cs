namespace Erp.Application.Sales;

public sealed class SalesPreInvoiceFilter
{
    public string Search { get; set; } = string.Empty;
    public string CatalogScope { get; set; } = string.Empty;
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 25;
    public string SortColumn { get; set; } = string.Empty;
    public bool SortDescending { get; set; }
}
