namespace Erp.Application.Tejidos;

public interface ITejidoQueries
{
    Task<TejidoSearchResultDto> SearchAsync(Guid tenantId, Guid companyId, TejidoFilter filter, CancellationToken cancellationToken = default);
    Task<TejidoDetailDto?> GetByCodeAsync(Guid tenantId, Guid companyId, string code, CancellationToken cancellationToken = default);
}
