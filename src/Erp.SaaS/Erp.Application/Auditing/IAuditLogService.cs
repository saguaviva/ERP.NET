namespace Erp.Application.Auditing;

public interface IAuditLogService
{
    Task WriteAsync(WriteAuditLogCommand command, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<AuditLogEntryDto>> GetRecentAsync(
        int limit = 20,
        Guid? tenantId = null,
        Guid? companyId = null,
        string? entityName = null,
        string? entityId = null,
        CancellationToken cancellationToken = default);
}
