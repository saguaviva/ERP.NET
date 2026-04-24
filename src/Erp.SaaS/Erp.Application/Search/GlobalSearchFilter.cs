namespace Erp.Application.Search;

public sealed class GlobalSearchFilter
{
    public string Search { get; set; } = string.Empty;
    public int MaxResultsPerGroup { get; set; } = 5;
}
