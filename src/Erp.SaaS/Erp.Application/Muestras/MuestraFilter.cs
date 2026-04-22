namespace Erp.Application.Muestras;

public sealed class MuestraFilter
{
    public string Search { get; set; } = string.Empty;
    public int? MachineCode { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 25;
    public string SortColumn { get; set; } = nameof(MuestraListItemDto.Code);
    public bool SortDescending { get; set; }
}
