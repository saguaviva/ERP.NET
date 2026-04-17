namespace Erp.Application.Companies;

public interface ICompanyAccessService
{
    Task<IReadOnlyCollection<AllowedCompanyDto>> GetAllowedCompaniesAsync(Guid userId, Guid tenantId, CancellationToken cancellationToken = default);
}
