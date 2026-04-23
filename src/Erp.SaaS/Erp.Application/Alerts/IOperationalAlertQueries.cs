namespace Erp.Application.Alerts;

public interface IOperationalAlertQueries
{
    Task<OperationalAlertDashboardDto> GetDashboardAsync(Guid tenantId, Guid companyId, CancellationToken cancellationToken = default);
}
