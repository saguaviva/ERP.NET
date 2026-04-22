namespace Erp.Application.Disposiciones;

public interface IDisposicionService
{
    Task<int> SaveAsync(SaveDisposicionCommand command, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid tenantId, Guid companyId, int code, CancellationToken cancellationToken = default);
}
