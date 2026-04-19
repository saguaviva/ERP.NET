namespace Erp.Application.Representatives;

public interface IRepresentativeQueries
{
    Task<RepresentativeSearchResultDto> SearchAsync(Guid tenantId, Guid companyId, RepresentativeFilter filter, CancellationToken cancellationToken = default);
    Task<RepresentativeDetailDto?> GetByCodeAsync(Guid tenantId, Guid companyId, int code, CancellationToken cancellationToken = default);
}
