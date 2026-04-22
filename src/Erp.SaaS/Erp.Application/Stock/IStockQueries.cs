namespace Erp.Application.Stock;

public interface IStockQueries
{
    Task<StockMovementSearchResultDto> SearchMovementsAsync(Guid tenantId, Guid companyId, StockMovementFilter filter, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<StockMovementListItemDto>> GetByPurchaseReceiptAsync(Guid tenantId, Guid companyId, int receiptNumber, CancellationToken cancellationToken = default);
    Task<StockBalanceSearchResultDto> SearchBalancesAsync(Guid tenantId, Guid companyId, StockBalanceFilter filter, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<StockCountLineDto>> GetCountSeedLinesAsync(Guid tenantId, Guid companyId, string warehouse, CancellationToken cancellationToken = default);
    Task<StockCountSearchResultDto> SearchCountsAsync(Guid tenantId, Guid companyId, StockCountFilter filter, CancellationToken cancellationToken = default);
    Task<StockCountDetailDto?> GetCountByNumberAsync(Guid tenantId, Guid companyId, int countNumber, CancellationToken cancellationToken = default);
    Task<StockTransferSearchResultDto> SearchTransfersAsync(Guid tenantId, Guid companyId, StockTransferFilter filter, CancellationToken cancellationToken = default);
    Task<StockTransferDetailDto?> GetTransferByNumberAsync(Guid tenantId, Guid companyId, int transferNumber, CancellationToken cancellationToken = default);
    Task<StockLegacySyncInfoDto> GetLegacySyncInfoAsync(Guid tenantId, Guid companyId, CancellationToken cancellationToken = default);
}
