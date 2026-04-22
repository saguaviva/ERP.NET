namespace Erp.Application.Tejidos;

public interface ITejidoService
{
    Task<string> SaveAsync(SaveTejidoCommand command, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid tenantId, Guid companyId, string code, CancellationToken cancellationToken = default);
}
