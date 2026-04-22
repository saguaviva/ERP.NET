namespace Erp.Application.Stock;

public sealed class StockTransferListItemDto
{
    public Guid TransferId { get; set; }
    public int TransferNumber { get; set; }
    public DateTime TransferDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public string FromWarehouse { get; set; } = string.Empty;
    public string ToWarehouse { get; set; } = string.Empty;
    public int LineCount { get; set; }
    public decimal TotalQuantity { get; set; }
    public string Notes { get; set; } = string.Empty;
    public string Origin { get; set; } = string.Empty;
}
