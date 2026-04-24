namespace Erp.Application.Search;

public sealed class GlobalSearchResultDto
{
    public string Query { get; init; } = string.Empty;
    public IReadOnlyCollection<GlobalSearchGroupDto> Groups { get; init; } = [];
    public int TotalCount => Groups.Sum(group => group.TotalCount);

    public static GlobalSearchResultDto Empty(string query) => new()
    {
        Query = query,
        Groups = []
    };
}
