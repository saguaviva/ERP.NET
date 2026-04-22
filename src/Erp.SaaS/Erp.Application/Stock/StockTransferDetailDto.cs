namespace Erp.Application.Stock;

public sealed class StockTransferDetailDto
{
    public Guid TransferId { get; set; }
    public int TransferNumber { get; set; }
    public DateTime TransferDate { get; set; } = DateTime.Today;
    public string Status { get; set; } = StockTransferStatuses.Draft;
    public string FromWarehouse { get; set; } = string.Empty;
    public string ToWarehouse { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public decimal TotalQuantity { get; set; }
    public string Origin { get; set; } = "local";
    public List<StockTransferLineDto> Lines { get; set; } = [];
}
