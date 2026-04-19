namespace Erp.Application.Sales;

public sealed class SalesInvoiceSearchResultDto
{
    public IReadOnlyCollection<SalesInvoiceListItemDto> Items { get; set; } = [];
    public int TotalCount { get; set; }
}
