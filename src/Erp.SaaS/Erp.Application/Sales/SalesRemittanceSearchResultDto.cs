namespace Erp.Application.Sales;

public sealed class SalesRemittanceSearchResultDto
{
    public IReadOnlyCollection<SalesRemittanceListItemDto> Items { get; set; } = [];
    public int TotalCount { get; set; }
}
