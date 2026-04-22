namespace Erp.Application.BaseData;

public interface IBaseCatalogQueries
{
    Task<BaseCatalogSearchResultDto> SearchAsync(Guid tenantId, Guid companyId, string catalogKey, BaseCatalogFilter filter, CancellationToken cancellationToken = default);
    Task<BaseCatalogDetailDto?> GetByCodeAsync(Guid tenantId, Guid companyId, string catalogKey, string code, CancellationToken cancellationToken = default);
}
