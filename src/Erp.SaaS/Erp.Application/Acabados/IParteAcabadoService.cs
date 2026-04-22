namespace Erp.Application.Acabados;

public interface IParteAcabadoService
{
    Task<int> SaveAsync(SaveParteAcabadoCommand command, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid tenantId, Guid companyId, int orderNumber, CancellationToken cancellationToken = default);
}
