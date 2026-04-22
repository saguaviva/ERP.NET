namespace Erp.Application.Clients;

public interface IClienteService
{
    Task<int> SaveAsync(SaveClienteCommand command, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid tenantId, Guid companyId, int code, CancellationToken cancellationToken = default);
    Task SetDuplicateReviewAsync(SetClienteDuplicateReviewCommand command, CancellationToken cancellationToken = default);
    Task SetPreferredPrincipalAsync(SetClientePreferredPrincipalCommand command, CancellationToken cancellationToken = default);
}
