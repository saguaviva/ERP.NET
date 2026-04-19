namespace Erp.Application.Sales;

public sealed class SalesInvoiceDraftSearchResultDto
{
    public IReadOnlyCollection<SalesInvoiceDraftListItemDto> Items { get; set; } = [];
    public int TotalCount { get; set; }
}
