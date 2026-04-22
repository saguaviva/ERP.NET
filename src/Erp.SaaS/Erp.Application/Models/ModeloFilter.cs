namespace Erp.Application.Models;

public sealed class ModeloFilter
{
    public string Search { get; set; } = string.Empty;
    public string Season { get; set; } = string.Empty;
    public string Series { get; set; } = string.Empty;
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 25;
    public string SortColumn { get; set; } = nameof(ModeloListItemDto.Season);
    public bool SortDescending { get; set; }
}
