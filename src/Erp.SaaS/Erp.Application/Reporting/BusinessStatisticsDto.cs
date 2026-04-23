namespace Erp.Application.Reporting;

public sealed class BusinessStatisticsDto
{
    public int SalesOrderCount { get; set; }
    public decimal SalesOrderAmount { get; set; }
    public decimal AverageSalesOrderAmount { get; set; }
    public int SalesShipmentCount { get; set; }
    public decimal SalesShipmentAmount { get; set; }
    public int SalesInvoiceCount { get; set; }
    public decimal SalesInvoiceAmount { get; set; }
    public decimal SalesOutstandingAmount { get; set; }
    public decimal SalesCollectedAmount { get; set; }
    public decimal SalesCollectionRate { get; set; }
    public decimal AverageSalesInvoiceAmount { get; set; }
    public decimal SalesInvoiceAmountMonthToDate { get; set; }
    public decimal SalesInvoiceAmountYearToDate { get; set; }
    public int PurchaseOrderCount { get; set; }
    public decimal PurchaseOrderAmount { get; set; }
    public decimal AveragePurchaseOrderAmount { get; set; }
    public int PurchaseReceiptCount { get; set; }
    public decimal PurchaseReceivedQuantity { get; set; }
    public int PurchaseInvoiceCount { get; set; }
    public decimal PurchaseInvoiceAmount { get; set; }
    public decimal PurchaseOutstandingAmount { get; set; }
    public decimal PurchasePaidAmount { get; set; }
    public decimal PurchasePaymentRate { get; set; }
    public decimal AveragePurchaseInvoiceAmount { get; set; }
    public decimal PurchaseInvoiceAmountMonthToDate { get; set; }
    public decimal PurchaseInvoiceAmountYearToDate { get; set; }
    public int RemittanceCount { get; set; }
    public decimal RemittanceOutstandingAmount { get; set; }
    public int LiveFinishOrders { get; set; }
    public int DraftCounts { get; set; }
    public int DraftTransfers { get; set; }
    public int StockPositions { get; set; }
    public int StockMovementsInRange { get; set; }
    public decimal NetBillingBalance { get; set; }
    public IReadOnlyCollection<StatisticComparisonItemDto> PeriodComparisons { get; set; } = [];
    public IReadOnlyCollection<StatisticTimelinePointDto> WeeklyTimeline { get; set; } = [];
    public IReadOnlyCollection<StatisticBreakdownItemDto> TopClients { get; set; } = [];
    public IReadOnlyCollection<StatisticBreakdownItemDto> TopSuppliers { get; set; } = [];
    public IReadOnlyCollection<StatisticBreakdownItemDto> SalesStatusBreakdown { get; set; } = [];
    public IReadOnlyCollection<StatisticBreakdownItemDto> PurchaseStatusBreakdown { get; set; } = [];
    public IReadOnlyCollection<StatisticBreakdownItemDto> FinishStatusBreakdown { get; set; } = [];
    public IReadOnlyCollection<StatisticBreakdownItemDto> TopWarehouses { get; set; } = [];
    public IReadOnlyCollection<StatisticBreakdownItemDto> MovementTypeBreakdown { get; set; } = [];
    public IReadOnlyCollection<StatisticBreakdownItemDto> TopBilledItems { get; set; } = [];
    public IReadOnlyCollection<StatisticBreakdownItemDto> TopFinishers { get; set; } = [];
    public IReadOnlyCollection<StatisticDistributionItemDto> SalesCustomerMix { get; set; } = [];
    public IReadOnlyCollection<StatisticDistributionItemDto> SalesItemMix { get; set; } = [];
    public decimal SalesTopClientSharePercent { get; set; }
    public decimal SalesTop5ClientsSharePercent { get; set; }
    public IReadOnlyCollection<StatisticBreakdownItemDto> SalesAgingBuckets { get; set; } = [];
    public IReadOnlyCollection<StatisticDistributionItemDto> SalesOutstandingRiskByClient { get; set; } = [];
    public decimal SalesOutstandingTopClientSharePercent { get; set; }
    public decimal SalesOutstandingTop5ClientsSharePercent { get; set; }
    public IReadOnlyCollection<StatisticBreakdownItemDto> PurchaseAgingBuckets { get; set; } = [];
    public IReadOnlyCollection<StatisticGapItemDto> PurchaseReceiptInvoiceGaps { get; set; } = [];
    public IReadOnlyCollection<StatisticBreakdownItemDto> ProductionLoadByFinisher { get; set; } = [];
    public IReadOnlyCollection<StatisticBreakdownItemDto> ProductionLoadByWeek { get; set; } = [];
    public IReadOnlyCollection<StatisticBreakdownItemDto> WarehouseRotationByWarehouse { get; set; } = [];
    public IReadOnlyCollection<StatisticBreakdownItemDto> WarehouseStockAgeBuckets { get; set; } = [];
    public IReadOnlyCollection<StatisticBreakdownItemDto> WarehouseCoverageBuckets { get; set; } = [];
}
