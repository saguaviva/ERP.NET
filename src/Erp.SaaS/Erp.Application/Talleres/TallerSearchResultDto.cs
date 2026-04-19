namespace Erp.Application.Talleres;

public sealed class TallerSearchResultDto
{
    public IReadOnlyCollection<TallerListItemDto> Items { get; init; } = [];
    public int TotalCount { get; init; }
}
