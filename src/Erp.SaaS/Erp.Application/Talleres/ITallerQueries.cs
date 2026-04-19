namespace Erp.Application.Talleres;

public interface ITallerQueries
{
    Task<TallerSearchResultDto> SearchAsync(Guid tenantId, Guid companyId, TallerFilter filter, CancellationToken cancellationToken = default);
    Task<TallerDetailDto?> GetByCodeAsync(Guid tenantId, Guid companyId, int code, CancellationToken cancellationToken = default);
}
