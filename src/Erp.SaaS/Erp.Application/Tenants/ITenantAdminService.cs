namespace Erp.Application.Tenants;

public interface ITenantAdminService
{
    Task<IReadOnlyCollection<TenantSummaryDto>> GetTenantsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<CompanySummaryDto>> GetCompaniesAsync(Guid? tenantId = null, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<UserSummaryDto>> GetUsersAsync(Guid? tenantId = null, CancellationToken cancellationToken = default);
    Task<TenantSummaryDto> CreateTenantAsync(CreateTenantCommand command, CancellationToken cancellationToken = default);
    Task<CompanySummaryDto> CreateCompanyAsync(CreateCompanyCommand command, CancellationToken cancellationToken = default);
    Task<UserSummaryDto> CreateUserAsync(CreateUserCommand command, CancellationToken cancellationToken = default);
    Task AssignUserCompaniesAsync(AssignUserCompaniesCommand command, CancellationToken cancellationToken = default);
    Task AssignUserRolesAsync(AssignUserRolesCommand command, CancellationToken cancellationToken = default);
}
