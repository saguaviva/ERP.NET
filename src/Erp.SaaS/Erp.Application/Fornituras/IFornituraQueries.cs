namespace Erp.Application.Fornituras;

public interface IFornituraQueries
{
    Task<FornituraSearchResultDto> SearchAsync(Guid tenantId, Guid companyId, FornituraFilter filter, CancellationToken cancellationToken = default);
    Task<FornituraDetailDto?> GetByCodeAsync(Guid tenantId, Guid companyId, string code, CancellationToken cancellationToken = default);
}
