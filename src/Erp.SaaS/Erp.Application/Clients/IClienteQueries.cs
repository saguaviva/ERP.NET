namespace Erp.Application.Clients;

public interface IClienteQueries
{
    Task<ClienteSearchResultDto> SearchAsync(Guid tenantId, Guid companyId, ClienteFilter filter, CancellationToken cancellationToken = default);
    Task<ClienteDetailDto?> GetByCodeAsync(Guid tenantId, Guid companyId, int code, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<ClienteDuplicateDto>> FindDuplicatesAsync(Guid tenantId, Guid companyId, SaveClienteCommand command, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<ClienteDuplicatePairDto>> GetDuplicateInboxAsync(Guid tenantId, Guid companyId, CancellationToken cancellationToken = default);
}
