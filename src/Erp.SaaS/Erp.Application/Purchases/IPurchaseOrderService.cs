namespace Erp.Application.Purchases;

public interface IPurchaseOrderService
{
    Task<int> SaveAsync(SavePurchaseOrderCommand command, CancellationToken cancellationToken = default);
    Task ReceiveAsync(RegisterPurchaseOrderReceiptCommand command, CancellationToken cancellationToken = default);
}
