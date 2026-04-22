namespace Erp.Application.Purchases;

public interface IPurchaseInvoiceQueries
{
    Task<PurchaseInvoiceSearchResultDto> SearchInvoicesAsync(Guid tenantId, Guid companyId, PurchaseInvoiceFilter filter, CancellationToken cancellationToken = default);
    Task<PurchaseInvoiceDetailDto?> GetInvoiceByNumberAsync(Guid tenantId, Guid companyId, int invoiceNumber, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<PurchaseInvoiceListItemDto>> GetInvoicesByReceiptAsync(Guid tenantId, Guid companyId, int receiptNumber, CancellationToken cancellationToken = default);
}
