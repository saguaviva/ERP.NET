namespace Erp.Application.Sales;

public sealed class SalesRemittanceCandidateFilter
{
    public string Search { get; set; } = string.Empty;
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 25;
}
