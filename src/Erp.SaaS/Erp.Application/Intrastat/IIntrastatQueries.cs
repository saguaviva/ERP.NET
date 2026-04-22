namespace Erp.Application.Intrastat;

public interface IIntrastatQueries
{
    Task<IntrastatPeriodDto?> GetLatestPeriodAsync(Guid tenantId, Guid companyId, CancellationToken cancellationToken = default);
    Task<IntrastatReportDto> GetReportAsync(Guid tenantId, Guid companyId, IntrastatFilter filter, CancellationToken cancellationToken = default);
}
