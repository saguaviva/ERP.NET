namespace Erp.Application.Reporting;

public sealed class OperationalDocumentFilter
{
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public string Search { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string TypeKey { get; set; } = string.Empty;
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 25;
    public string SortColumn { get; set; } = string.Empty;
    public bool SortDescending { get; set; } = true;
}
