namespace Erp.Application.Models;

public sealed class ModeloFornituraLineDto
{
    public int LineNumber { get; set; }
    public string FornituraCode { get; set; } = string.Empty;
    public string Measure { get; set; } = string.Empty;
    public decimal Units { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal ImportAmount { get; set; }
}
