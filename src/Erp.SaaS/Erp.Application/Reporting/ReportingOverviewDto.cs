namespace Erp.Application.Reporting;

public sealed class ReportingOverviewDto
{
    public int Clients { get; set; }
    public int Suppliers { get; set; }
    public int Fabrics { get; set; }
    public int Yarns { get; set; }
    public int Samples { get; set; }
    public int Models { get; set; }
    public int PendingDispositions { get; set; }
    public int LiveFinishOrders { get; set; }
    public int SalesDocumentsThisMonth { get; set; }
    public int PurchaseDocumentsThisMonth { get; set; }
    public int InventoryPositions { get; set; }
    public int InventoryMovesThisMonth { get; set; }
}
