namespace Erp.Application.Purchases;

public interface IPurchaseOrderQueries
{
    Task<PurchaseOrderSearchResultDto> SearchAsync(Guid tenantId, Guid companyId, PurchaseOrderFilter filter, CancellationToken cancellationToken = default);
    Task<PurchaseOrderDetailDto?> GetByOrderNumberAsync(Guid tenantId, Guid companyId, int orderNumber, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<PurchaseOrderReceiptDto>> GetReceiptsAsync(Guid tenantId, Guid companyId, int orderNumber, CancellationToken cancellationToken = default);
    Task<PurchaseReceiptSearchResultDto> SearchReceiptsAsync(Guid tenantId, Guid companyId, PurchaseReceiptFilter filter, CancellationToken cancellationToken = default);
    Task<PurchaseOrderReceiptDto?> GetReceiptByNumberAsync(Guid tenantId, Guid companyId, int receiptNumber, CancellationToken cancellationToken = default);
}
