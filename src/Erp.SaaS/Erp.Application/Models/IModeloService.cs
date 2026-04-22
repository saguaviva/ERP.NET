namespace Erp.Application.Models;

public interface IModeloService
{
    Task<Guid> SaveAsync(SaveModeloCommand command, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid tenantId, Guid companyId, Guid id, CancellationToken cancellationToken = default);
}
