namespace Erp.Application.Muestras;

public interface IMuestraService
{
    Task<string> SaveAsync(SaveMuestraCommand command, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid tenantId, Guid companyId, string code, CancellationToken cancellationToken = default);
}
