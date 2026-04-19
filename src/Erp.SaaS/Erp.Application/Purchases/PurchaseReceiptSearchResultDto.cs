namespace Erp.Application.Purchases;

public sealed class PurchaseReceiptSearchResultDto
{
    public IReadOnlyCollection<PurchaseReceiptListItemDto> Items { get; set; } = [];
    public int TotalCount { get; set; }
}
