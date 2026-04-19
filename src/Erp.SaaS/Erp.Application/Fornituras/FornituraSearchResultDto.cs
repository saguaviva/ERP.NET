namespace Erp.Application.Fornituras;

public sealed class FornituraSearchResultDto
{
    public IReadOnlyCollection<FornituraListItemDto> Items { get; init; } = [];
    public int TotalCount { get; init; }
}
