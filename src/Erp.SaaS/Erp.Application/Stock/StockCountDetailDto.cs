namespace Erp.Application.Stock;

public sealed class StockCountDetailDto
{
    public Guid CountId { get; set; }
    public int? CountNumber { get; set; }
    public DateTime CountDate { get; set; } = DateTime.Today;
    public string Status { get; set; } = StockCountStatuses.Draft;
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
    public List<StockCountLineDto> Lines { get; set; } = [];
}
