namespace Erp.Application.Stock;

public sealed class StockTransferFilter
{
    public string Search { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public bool IncludeClosed { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 25;
    public string SortColumn { get; set; } = string.Empty;
    public bool SortDescending { get; set; }
}
