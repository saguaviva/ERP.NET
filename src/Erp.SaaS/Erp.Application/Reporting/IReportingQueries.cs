namespace Erp.Application.Reporting;

public interface IReportingQueries
{
    Task<ReportingOverviewDto> GetOverviewAsync(Guid tenantId, Guid companyId, CancellationToken cancellationToken = default);
    Task<OperationalDocumentSearchResultDto> SearchOperationalDocumentsAsync(Guid tenantId, Guid companyId, OperationalDocumentFilter filter, CancellationToken cancellationToken = default);
    Task<BusinessStatisticsDto> GetBusinessStatisticsAsync(Guid tenantId, Guid companyId, BusinessStatisticsFilter filter, CancellationToken cancellationToken = default);
}
