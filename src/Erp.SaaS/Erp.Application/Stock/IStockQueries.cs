namespace Erp.Application.Stock;

public interface IStockQueries
{
    Task<StockMovementSearchResultDto> SearchMovementsAsync(Guid tenantId, Guid companyId, StockMovementFilter filter, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<StockMovementListItemDto>> GetByPurchaseReceiptAsync(Guid tenantId, Guid companyId, int receiptNumber, CancellationToken cancellationToken = default);
    Task<StockBalanceSearchResultDto> SearchBalancesAsync(Guid tenantId, Guid companyId, StockBalanceFilter filter, CancellationToken cancellationToken = default);
    Task<StockLegacySyncInfoDto> GetLegacySyncInfoAsync(Guid tenantId, Guid companyId, CancellationToken cancellationToken = default);
}
