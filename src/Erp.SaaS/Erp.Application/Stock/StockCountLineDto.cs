namespace Erp.Application.Stock;

public sealed class StockCountLineDto
{
    public int LineNumber { get; set; }
    public string ItemCode { get; set; } = string.Empty;
    public string ItemDescription { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public decimal ExpectedQuantity { get; set; }
    public decimal CountedQuantity { get; set; }
    public decimal DifferenceQuantity { get; set; }
    public bool IsDifferenceValidated { get; set; }
    public string UnitOfMeasure { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
}
