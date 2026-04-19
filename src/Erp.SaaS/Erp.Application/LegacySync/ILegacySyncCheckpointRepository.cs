namespace Erp.Application.LegacySync;

public interface ILegacySyncCheckpointRepository
{
    Task<LegacySyncCheckpointDto?> GetAsync(Guid tenantId, Guid companyId, string moduleKey, CancellationToken cancellationToken = default);
    Task SaveAsync(LegacySyncCheckpointUpdate update, CancellationToken cancellationToken = default);
}
