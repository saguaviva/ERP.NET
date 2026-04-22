namespace Erp.Application.Stock;

public sealed class SaveStockCountCommand
{
    public Guid TenantId { get; set; }
    public Guid CompanyId { get; set; }
    public int? CountNumber { get; set; }
    public DateTime CountDate { get; set; } = DateTime.Today;
    public string Status { get; set; } = StockCountStatuses.Draft;
    public string Warehouse { get; set; } = string.Empty;
    public bool IsBlindCount { get; set; }
    public bool IsBlindCountRevealed { get; set; }
    public string Notes { get; set; } = string.Empty;
    public List<StockCountLineDto> Lines { get; set; } = [];
}
