namespace Erp.Application.Sales;

public sealed class SalesOrderSearchResultDto
{
    public IReadOnlyCollection<SalesOrderListItemDto> Items { get; set; } = [];
    public int TotalCount { get; set; }
}
