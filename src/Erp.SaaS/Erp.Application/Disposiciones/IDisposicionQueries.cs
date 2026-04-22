namespace Erp.Application.Disposiciones;

public interface IDisposicionQueries
{
    Task<DisposicionSearchResultDto> SearchAsync(Guid tenantId, Guid companyId, DisposicionFilter filter, CancellationToken cancellationToken = default);
    Task<DisposicionDetailDto?> GetByCodeAsync(Guid tenantId, Guid companyId, int code, CancellationToken cancellationToken = default);
}
