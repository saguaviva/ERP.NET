namespace Erp.Application.Representatives;

public sealed class RepresentativeSearchResultDto
{
    public IReadOnlyCollection<RepresentativeListItemDto> Items { get; init; } = [];
    public int TotalCount { get; init; }
}
