namespace Erp.Application.Search;

public sealed class GlobalSearchGroupDto
{
    public string Key { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public int TotalCount { get; init; }
    public IReadOnlyCollection<GlobalSearchItemDto> Items { get; init; } = [];
}
