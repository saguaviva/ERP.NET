namespace Erp.Application.Intrastat;

public sealed class IntrastatFilter
{
    public int Year { get; set; } = DateTime.Today.Year;
    public int Month { get; set; } = DateTime.Today.Month;
    public string Search { get; set; } = string.Empty;
    public bool OnlyClassified { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 50;
    public string SortColumn { get; set; } = nameof(IntrastatLineDto.IssueDate);
    public bool SortDescending { get; set; } = true;
}
