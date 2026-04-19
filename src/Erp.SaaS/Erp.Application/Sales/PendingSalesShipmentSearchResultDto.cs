namespace Erp.Application.Sales;

public sealed class PendingSalesShipmentSearchResultDto
{
    public IReadOnlyCollection<PendingSalesShipmentDto> Items { get; set; } = [];
    public int TotalCount { get; set; }
}
