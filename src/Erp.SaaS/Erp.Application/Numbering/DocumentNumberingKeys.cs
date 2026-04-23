namespace Erp.Application.Numbering;

public static class DocumentNumberingKeys
{
    public const string PurchaseOrder = "purchase-order";
    public const string PurchaseReceipt = "purchase-receipt";
    public const string PurchaseInvoice = "purchase-invoice";
    public const string SalesOrder = "sales-order";
    public const string SalesShipment = "sales-shipment";
    public const string SalesInvoiceDraft = "sales-invoice-draft";
    public const string SalesInvoice = "sales-invoice";
    public const string SalesRemittance = "sales-remittance";
    public const string StockTransfer = "stock-transfer";
    public const string StockCount = "stock-count";
    public const string FinishWorkOrder = "finish-work-order";

    public static IReadOnlyList<string> OrderedKeys { get; } =
    [
        PurchaseOrder,
        PurchaseReceipt,
        PurchaseInvoice,
        SalesOrder,
        SalesShipment,
        SalesInvoiceDraft,
        SalesInvoice,
        SalesRemittance,
        StockTransfer,
        StockCount,
        FinishWorkOrder
    ];
}
