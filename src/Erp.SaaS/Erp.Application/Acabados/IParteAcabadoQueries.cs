namespace Erp.Application.Acabados;

public interface IParteAcabadoQueries
{
    Task<ParteAcabadoSearchResultDto> SearchAsync(Guid tenantId, Guid companyId, ParteAcabadoFilter filter, CancellationToken cancellationToken = default);
    Task<ParteAcabadoDetailDto?> GetByNumberAsync(Guid tenantId, Guid companyId, int orderNumber, CancellationToken cancellationToken = default);
}
