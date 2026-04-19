namespace Erp.Application.Transportistas;

public interface ITransportistaQueries
{
    Task<TransportistaSearchResultDto> SearchAsync(Guid tenantId, Guid companyId, TransportistaFilter filter, CancellationToken cancellationToken = default);
    Task<TransportistaDetailDto?> GetByCodeAsync(Guid tenantId, Guid companyId, int code, CancellationToken cancellationToken = default);
}
