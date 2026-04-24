namespace Erp.Application.Search;

public sealed class GlobalSearchItemDto
{
    public string Kind { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public string Subtitle { get; init; } = string.Empty;
    public string Href { get; init; } = string.Empty;
    public string Badge { get; init; } = string.Empty;
    public DateTime? Date { get; init; }
    public decimal? Amount { get; init; }
    public int Score { get; init; }
}
