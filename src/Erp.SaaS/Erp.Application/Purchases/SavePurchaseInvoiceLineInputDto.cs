namespace Erp.Application.Purchases;

public sealed class SavePurchaseInvoiceLineInputDto
{
    public int LineNumber { get; set; }
    public string ItemCode { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public string UnitOfMeasure { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public decimal TaxRate { get; set; } = 21m;
    public int? SourceOrderNumber { get; set; }
    public int? SourceOrderLineNumber { get; set; }
    public int? SourceReceiptNumber { get; set; }
}
