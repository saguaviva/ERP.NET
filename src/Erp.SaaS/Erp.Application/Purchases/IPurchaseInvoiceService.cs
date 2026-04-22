namespace Erp.Application.Purchases;

public interface IPurchaseInvoiceService
{
    Task<int> SaveInvoiceAsync(SavePurchaseInvoiceCommand command, CancellationToken cancellationToken = default);
    Task DeleteInvoiceAsync(Guid tenantId, Guid companyId, int invoiceNumber, CancellationToken cancellationToken = default);
    Task RegisterPaymentAsync(RegisterPurchaseInvoicePaymentCommand command, CancellationToken cancellationToken = default);
}
