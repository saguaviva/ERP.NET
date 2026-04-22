namespace Erp.Application.Muestras;

public interface IMuestraQueries
{
    Task<MuestraSearchResultDto> SearchAsync(Guid tenantId, Guid companyId, MuestraFilter filter, CancellationToken cancellationToken = default);
    Task<MuestraDetailDto?> GetByCodeAsync(Guid tenantId, Guid companyId, string code, CancellationToken cancellationToken = default);
    Task<MuestraDetailDto?> GetByIdentityAsync(Guid tenantId, Guid companyId, string code, int clientCode, CancellationToken cancellationToken = default);
}
