namespace Erp.Application.Transportistas;

public sealed class TransportistaSearchResultDto
{
    public IReadOnlyCollection<TransportistaListItemDto> Items { get; init; } = [];
    public int TotalCount { get; init; }
}
