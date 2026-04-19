namespace Erp.Application.LegacySync;

public interface ILegacySyncJobRunner
{
    Task<LegacySyncJobSummaryDto> RunAsync(LegacySyncJobRequest request, CancellationToken cancellationToken = default);
}
