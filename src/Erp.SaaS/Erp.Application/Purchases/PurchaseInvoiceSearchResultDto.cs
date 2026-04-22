namespace Erp.Application.Purchases;

public sealed class PurchaseInvoiceSearchResultDto
{
    public IReadOnlyCollection<PurchaseInvoiceListItemDto> Items { get; set; } = [];
    public int TotalCount { get; set; }
}
