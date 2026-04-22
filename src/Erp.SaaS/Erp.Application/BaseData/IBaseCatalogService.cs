namespace Erp.Application.BaseData;

public interface IBaseCatalogService
{
    Task<string> SaveAsync(SaveBaseCatalogItemCommand command, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid tenantId, Guid companyId, string catalogKey, string code, CancellationToken cancellationToken = default);
}
