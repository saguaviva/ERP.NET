namespace Erp.Application.Hilos;

public interface IHiloService
{
    Task<string> SaveAsync(SaveHiloCommand command, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid tenantId, Guid companyId, string code, CancellationToken cancellationToken = default);
}
