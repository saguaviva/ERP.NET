namespace Erp.Application.Search;

public interface IGlobalSearchService
{
    Task<GlobalSearchResultDto> SearchAsync(
        Guid tenantId,
        Guid companyId,
        GlobalSearchFilter filter,
        CancellationToken cancellationToken = default);
}
