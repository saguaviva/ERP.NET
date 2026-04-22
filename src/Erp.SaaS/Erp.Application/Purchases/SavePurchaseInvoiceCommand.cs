namespace Erp.Application.Purchases;

public sealed class SavePurchaseInvoiceCommand
{
    public Guid TenantId { get; set; }
    public Guid CompanyId { get; set; }
    public int? InvoiceNumber { get; set; }
    public int SupplierCode { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public string SupplierTaxId { get; set; } = string.Empty;
    public string SupplierDocumentNumber { get; set; } = string.Empty;
    public DateTime DocumentDate { get; set; } = DateTime.Today;
    public DateTime? DueDate { get; set; }
    public string Status { get; set; } = PurchaseInvoiceStatuses.Draft;
    public string Notes { get; set; } = string.Empty;
    public List<SavePurchaseInvoiceLineInputDto> Lines { get; set; } = [];
    public List<PurchaseInvoiceReceiptLinkDto> Receipts { get; set; } = [];
}
