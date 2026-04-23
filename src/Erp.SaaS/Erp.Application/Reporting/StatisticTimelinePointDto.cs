namespace Erp.Application.Reporting;

public sealed class StatisticTimelinePointDto
{
    public DateTime BucketStart { get; set; }
    public string Label { get; set; } = string.Empty;
    public int SalesInvoiceCount { get; set; }
    public decimal SalesInvoiceAmount { get; set; }
    public int PurchaseInvoiceCount { get; set; }
    public decimal PurchaseInvoiceAmount { get; set; }
    public int StockMovementCount { get; set; }
    public int FinishOrderCount { get; set; }
}
