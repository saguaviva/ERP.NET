namespace Erp.Application.Models;

public interface IModeloQueries
{
    Task<ModeloSearchResultDto> SearchAsync(Guid tenantId, Guid companyId, ModeloFilter filter, CancellationToken cancellationToken = default);
    Task<ModeloDetailDto?> GetByIdAsync(Guid tenantId, Guid companyId, Guid id, CancellationToken cancellationToken = default);
}
