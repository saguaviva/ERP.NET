namespace Erp.Application.Clients;

public sealed class ClienteFilter
{
    public string Search { get; set; } = string.Empty;
    public bool IncludeBlocked { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 25;
    public string SortColumn { get; set; } = string.Empty;
    public bool SortDescending { get; set; }
}
