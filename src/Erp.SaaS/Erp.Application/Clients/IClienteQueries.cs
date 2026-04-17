namespace Erp.Application.Clients;

public interface IClienteQueries
{
    Task<IReadOnlyCollection<ClienteListItemDto>> SearchAsync(Guid tenantId, Guid companyId, ClienteFilter filter, CancellationToken cancellationToken = default);
    Task<ClienteDetailDto?> GetByCodeAsync(Guid tenantId, Guid companyId, int code, CancellationToken cancellationToken = default);
}
