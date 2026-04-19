namespace Erp.Application.Sales;

public interface ISalesOrderService
{
    Task<int> SaveAsync(SaveSalesOrderCommand command, CancellationToken cancellationToken = default);
    Task ShipAsync(RegisterSalesOrderShipmentCommand command, CancellationToken cancellationToken = default);
    Task<int> CreateInvoiceDraftAsync(CreateSalesInvoiceDraftCommand command, CancellationToken cancellationToken = default);
    Task<int> IssueInvoiceDraftAsync(IssueSalesInvoiceDraftCommand command, CancellationToken cancellationToken = default);
    Task RegisterInvoicePaymentAsync(RegisterSalesInvoicePaymentCommand command, CancellationToken cancellationToken = default);
}
