namespace Erp.Application.Acabados;

public sealed class ParteAcabadoFilter
{
    public string Search { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public int? FinisherCode { get; set; }
    public int? MachineCode { get; set; }
    public int? OperationCode { get; set; }
    public string SourceSampleKind { get; set; } = string.Empty;
    public string SourceSampleCode { get; set; } = string.Empty;
    public bool LiveOnly { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 25;
    public string SortColumn { get; set; } = nameof(ParteAcabadoListItemDto.Date);
    public bool SortDescending { get; set; } = true;
}
