namespace Erp.Application.Representatives;

public interface IRepresentativeService
{
    Task<int> SaveAsync(SaveRepresentativeCommand command, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid tenantId, Guid companyId, int code, CancellationToken cancellationToken = default);
}
