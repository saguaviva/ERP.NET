namespace Erp.Application.Stock;

public sealed class StockCountListItemDto
{
    public int CountNumber { get; set; }
    public DateTime CountDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Warehouse { get; set; } = string.Empty;
    public bool IsBlindCount { get; set; }
    public bool IsBlindCountRevealed { get; set; }
    public int LineCount { get; set; }
    public int DifferenceLineCount { get; set; }
    public decimal ExpectedTotalQuantity { get; set; }
    public decimal CountedTotalQuantity { get; set; }
    public decimal DifferenceTotalQuantity { get; set; }
    public string Notes { get; set; } = string.Empty;
    public string Origin { get; set; } = string.Empty;
}
