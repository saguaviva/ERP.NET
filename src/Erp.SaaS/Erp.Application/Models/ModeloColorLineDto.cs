namespace Erp.Application.Models;

public sealed class ModeloColorLineDto
{
    public int LineNumber { get; set; }
    public string ModelColorCode { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string ColorTitle { get; set; } = string.Empty;
}
