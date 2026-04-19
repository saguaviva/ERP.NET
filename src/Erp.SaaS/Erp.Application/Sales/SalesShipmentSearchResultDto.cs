namespace Erp.Application.Sales;

public sealed class SalesShipmentSearchResultDto
{
    public IReadOnlyCollection<SalesOrderShipmentDto> Items { get; set; } = [];
    public int TotalCount { get; set; }
}
