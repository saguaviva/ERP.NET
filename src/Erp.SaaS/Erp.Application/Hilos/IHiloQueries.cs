namespace Erp.Application.Hilos;

public interface IHiloQueries
{
    Task<HiloSearchResultDto> SearchAsync(Guid tenantId, Guid companyId, HiloFilter filter, CancellationToken cancellationToken = default);
    Task<HiloDetailDto?> GetByCodeAsync(Guid tenantId, Guid companyId, string code, CancellationToken cancellationToken = default);
}
