namespace Erp.Application.Sales;

public interface ISalesOrderQueries
{
    Task<SalesOrderSearchResultDto> SearchAsync(Guid tenantId, Guid companyId, SalesOrderFilter filter, CancellationToken cancellationToken = default);
    Task<SalesOrderDetailDto?> GetByOrderNumberAsync(Guid tenantId, Guid companyId, int orderNumber, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<SalesOrderShipmentDto>> GetShipmentsAsync(Guid tenantId, Guid companyId, int orderNumber, CancellationToken cancellationToken = default);
    Task<SalesShipmentSearchResultDto> SearchShipmentsAsync(Guid tenantId, Guid companyId, SalesOrderFilter filter, CancellationToken cancellationToken = default);
    Task<SalesOrderShipmentDto?> GetShipmentByNumberAsync(Guid tenantId, Guid companyId, int shipmentNumber, CancellationToken cancellationToken = default);
    Task<PendingSalesShipmentSearchResultDto> SearchPendingShipmentsAsync(Guid tenantId, Guid companyId, SalesPreInvoiceFilter filter, CancellationToken cancellationToken = default);
    Task<SalesInvoiceDraftSearchResultDto> SearchInvoiceDraftsAsync(Guid tenantId, Guid companyId, SalesPreInvoiceFilter filter, CancellationToken cancellationToken = default);
    Task<SalesInvoiceDraftDto?> GetInvoiceDraftByNumberAsync(Guid tenantId, Guid companyId, int draftNumber, CancellationToken cancellationToken = default);
    Task<SalesInvoiceSearchResultDto> SearchInvoicesAsync(Guid tenantId, Guid companyId, SalesPreInvoiceFilter filter, CancellationToken cancellationToken = default);
    Task<SalesInvoiceDto?> GetInvoiceByNumberAsync(Guid tenantId, Guid companyId, int invoiceNumber, CancellationToken cancellationToken = default);
}
