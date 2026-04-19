namespace Erp.Application.Purchases;

public sealed class PurchaseOrderSearchResultDto
{
    public IReadOnlyCollection<PurchaseOrderListItemDto> Items { get; set; } = [];
    public int TotalCount { get; set; }
}
