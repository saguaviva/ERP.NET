namespace Erp.Application.Stock;

public interface IStockService
{
    Task<Guid> CreateAdjustmentAsync(CreateStockAdjustmentCommand command, CancellationToken cancellationToken = default);
    Task<int> SaveCountAsync(SaveStockCountCommand command, CancellationToken cancellationToken = default);
    Task DeleteCountAsync(Guid tenantId, Guid companyId, int countNumber, CancellationToken cancellationToken = default);
    Task<int> SaveTransferAsync(SaveStockTransferCommand command, CancellationToken cancellationToken = default);
    Task DeleteTransferAsync(Guid tenantId, Guid companyId, int transferNumber, CancellationToken cancellationToken = default);
}
