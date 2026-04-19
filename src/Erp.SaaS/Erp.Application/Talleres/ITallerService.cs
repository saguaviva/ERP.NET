namespace Erp.Application.Talleres;

public interface ITallerService
{
    Task<int> SaveAsync(SaveTallerCommand command, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid tenantId, Guid companyId, int code, CancellationToken cancellationToken = default);
}
