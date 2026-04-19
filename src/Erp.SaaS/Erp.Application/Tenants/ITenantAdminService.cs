namespace Erp.Application.Tenants;

public interface ITenantAdminService
{
    Task<PlatformSetupStatusDto> GetSetupStatusAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<TenantSummaryDto>> GetTenantsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<CompanySummaryDto>> GetCompaniesAsync(Guid? tenantId = null, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<LegacyCenterOptionDto>> GetLegacyCentersAsync(Guid? tenantId = null, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<TenantModuleSettingDto>> GetModuleSettingsAsync(Guid? tenantId = null, CancellationToken cancellationToken = default);
    Task<CompanyLegacyCenterImpactDto?> GetCompanyLegacyCenterImpactAsync(Guid companyId, Guid tenantId, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<UserSummaryDto>> GetUsersAsync(Guid? tenantId = null, CancellationToken cancellationToken = default);
    Task InitializePlatformAsync(InitializePlatformCommand command, CancellationToken cancellationToken = default);
    Task<TenantSummaryDto> CreateTenantAsync(CreateTenantCommand command, CancellationToken cancellationToken = default);
    Task<CompanySummaryDto> CreateCompanyAsync(CreateCompanyCommand command, CancellationToken cancellationToken = default);
    Task<CompanySummaryDto> UpdateCompanyLegacyCenterAsync(UpdateCompanyLegacyCenterCommand command, CancellationToken cancellationToken = default);
    Task SetModuleDataScopeAsync(SetTenantModuleDataScopeCommand command, CancellationToken cancellationToken = default);
    Task<UserSummaryDto> CreateUserAsync(CreateUserCommand command, CancellationToken cancellationToken = default);
    Task AssignUserCompaniesAsync(AssignUserCompaniesCommand command, CancellationToken cancellationToken = default);
    Task AssignUserRolesAsync(AssignUserRolesCommand command, CancellationToken cancellationToken = default);
    Task SetUserActiveAsync(SetUserActiveCommand command, CancellationToken cancellationToken = default);
    Task ResetUserPasswordAsync(ResetUserPasswordCommand command, CancellationToken cancellationToken = default);
}
