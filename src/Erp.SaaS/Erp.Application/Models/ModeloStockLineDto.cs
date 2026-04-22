namespace Erp.Application.Models;

public sealed class ModeloStockLineDto
{
    public int LineNumber { get; set; }
    public string Color { get; set; } = string.Empty;
    public string SizeText { get; set; } = string.Empty;
    public decimal SizeQuantity01 { get; set; }
    public decimal SizeQuantity02 { get; set; }
    public decimal SizeQuantity03 { get; set; }
    public decimal SizeQuantity04 { get; set; }
    public decimal SizeQuantity05 { get; set; }
    public decimal SizeQuantity06 { get; set; }
    public decimal SizeQuantity07 { get; set; }
    public decimal SizeQuantity08 { get; set; }
    public decimal SizeQuantity09 { get; set; }
    public decimal SizeQuantity10 { get; set; }
}
