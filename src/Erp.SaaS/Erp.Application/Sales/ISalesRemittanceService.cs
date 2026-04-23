namespace Erp.Application.Sales;

public interface ISalesRemittanceService
{
    Task<int> SaveAsync(Guid tenantId, Guid companyId, SaveSalesRemittanceCommand command, CancellationToken cancellationToken = default);
}
