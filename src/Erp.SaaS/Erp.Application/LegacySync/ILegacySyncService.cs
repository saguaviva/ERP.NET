namespace Erp.Application.LegacySync;

public interface ILegacySyncService
{
    Task<LegacySyncResultDto> RunAsync(RunLegacySyncCommand command, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<LegacySyncJobSummaryDto>> GetRecentJobsAsync(Guid tenantId, string? moduleKey = null, int limit = 20, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<LegacySyncModuleStatusDto>> GetModuleStatusesAsync(Guid tenantId, CancellationToken cancellationToken = default);
}
