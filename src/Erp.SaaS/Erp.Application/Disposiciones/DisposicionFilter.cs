namespace Erp.Application.Disposiciones;

public sealed class DisposicionFilter
{
    public string Search { get; set; } = string.Empty;
    public bool IncludeCancelled { get; set; }
    public string ReceivedMode { get; set; } = string.Empty;
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 25;
    public string SortColumn { get; set; } = nameof(DisposicionListItemDto.Date);
    public bool SortDescending { get; set; } = true;
}
