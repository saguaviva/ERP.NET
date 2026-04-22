namespace Erp.Application.Stock;

public sealed class SaveStockTransferCommand
{
    public Guid TenantId { get; set; }
    public Guid CompanyId { get; set; }
    public int? TransferNumber { get; set; }
    public DateTime TransferDate { get; set; } = DateTime.Today;
    public string Status { get; set; } = StockTransferStatuses.Draft;
    public string FromWarehouse { get; set; } = string.Empty;
    public string ToWarehouse { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public List<StockTransferLineDto> Lines { get; set; } = [];
}
