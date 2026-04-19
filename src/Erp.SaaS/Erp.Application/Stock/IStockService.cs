namespace Erp.Application.Stock;

public interface IStockService
{
    Task<Guid> CreateAdjustmentAsync(CreateStockAdjustmentCommand command, CancellationToken cancellationToken = default);
}
