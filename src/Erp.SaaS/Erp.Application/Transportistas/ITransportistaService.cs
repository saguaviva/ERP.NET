namespace Erp.Application.Transportistas;

public interface ITransportistaService
{
    Task<int> SaveAsync(SaveTransportistaCommand command, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid tenantId, Guid companyId, int code, CancellationToken cancellationToken = default);
}
