namespace Erp.Application.Fornituras;

public interface IFornituraService
{
    Task<string> SaveAsync(SaveFornituraCommand command, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid tenantId, Guid companyId, string code, CancellationToken cancellationToken = default);
}
