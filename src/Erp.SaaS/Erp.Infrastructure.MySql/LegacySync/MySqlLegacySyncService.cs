using Erp.Application.Companies;
using Erp.Application.Contexts;
using Erp.Application.LegacySync;
using Erp.Domain.Common;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Support;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.LegacySync;

public sealed class MySqlLegacySyncService : ILegacySyncService
{
    private readonly MySqlConnectionFactory _connectionFactory;
    private readonly ILegacySyncJobRunner _jobRunner;
    private readonly ICompanyAccessService _companyAccessService;
    private readonly ICurrentUserContext _currentUserContext;
    private readonly ITenantContext _tenantContext;
    private readonly IEnumerable<ILegacyModuleSyncHandler> _handlers;

    public MySqlLegacySyncService(
        MySqlConnectionFactory connectionFactory,
        ILegacySyncJobRunner jobRunner,
        ICompanyAccessService companyAccessService,
        ICurrentUserContext currentUserContext,
        ITenantContext tenantContext,
        IEnumerable<ILegacyModuleSyncHandler> handlers)
    {
        _connectionFactory = connectionFactory;
        _jobRunner = jobRunner;
        _companyAccessService = companyAccessService;
        _currentUserContext = currentUserContext;
        _tenantContext = tenantContext;
        _handlers = handlers;
    }

    public async Task<LegacySyncResultDto> RunAsync(RunLegacySyncCommand command, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return new LegacySyncResultDto
            {
                ModuleKey = command.ModuleKey,
                TenantId = command.TenantId,
                CompanyId = command.CompanyId,
                StartedUtc = DateTime.UtcNow,
                FinishedUtc = DateTime.UtcNow
            };
        }

        EnsureAccess(command);

        var handler = _handlers.FirstOrDefault(candidate =>
            string.Equals(candidate.ModuleKey, command.ModuleKey, StringComparison.OrdinalIgnoreCase));
        if (handler is null)
        {
            throw new InvalidOperationException($"No hay ningún sincronizador registrado para el módulo '{command.ModuleKey}'.");
        }

        var startedUtc = DateTime.UtcNow;
        var companies = await LoadCompaniesAsync(command.TenantId, command.CompanyId, cancellationToken);
        var jobs = new List<LegacySyncJobSummaryDto>();
        foreach (var company in companies)
        {
            jobs.Add(await _jobRunner.RunAsync(new LegacySyncJobRequest
            {
                TenantId = command.TenantId,
                CompanyId = company.CompanyId,
                CompanyName = company.CompanyName,
                CompanyLegacyCenterCode = company.LegacyCenterCode,
                ModuleKey = command.ModuleKey,
                ForceFullRefresh = command.ForceFullRefresh,
                TriggeredByScheduler = command.TriggeredByScheduler,
                TriggeredByUserId = command.TriggeredByScheduler ? null : _currentUserContext.UserId
            }, cancellationToken));
        }

        return new LegacySyncResultDto
        {
            ModuleKey = command.ModuleKey,
            ModuleDisplayName = handler.DisplayName,
            TenantId = command.TenantId,
            CompanyId = command.CompanyId,
            CompaniesProcessed = jobs.Count,
            RecordsInserted = jobs.Sum(job => job.RecordsInserted),
            RecordsUpdated = jobs.Sum(job => job.RecordsUpdated),
            RecordsSkipped = jobs.Sum(job => job.RecordsSkipped),
            ErrorsCount = jobs.Sum(job => job.ErrorsCount),
            StartedUtc = startedUtc,
            FinishedUtc = DateTime.UtcNow,
            Jobs = jobs
        };
    }

    public async Task<IReadOnlyCollection<LegacySyncJobSummaryDto>> GetRecentJobsAsync(
        Guid tenantId,
        string? moduleKey = null,
        int limit = 20,
        CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return [];
        }

        EnsureReadAccess(tenantId);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT j.id,
                   j.tenant_id,
                   j.company_id,
                   COALESCE(c.name, '') AS company_name,
                   COALESCE(c.legacy_center_code, '') AS company_legacy_center_code,
                   j.module_key,
                   j.module_display_name,
                   j.status,
                   j.triggered_by_user_id,
                   j.triggered_by_scheduler,
                   j.checkpoint_before,
                   j.checkpoint_after,
                   j.records_inserted,
                   j.records_updated,
                   j.records_skipped,
                   j.errors_count,
                   j.summary,
                   j.started_utc,
                   j.finished_utc
            FROM legacy_sync_jobs j
            LEFT JOIN companies c
              ON c.id = j.company_id
             AND c.tenant_id = j.tenant_id
            WHERE j.tenant_id = @tenantId
              AND (@moduleKey = '' OR j.module_key = @moduleKey)
            ORDER BY j.started_utc DESC, j.id DESC
            LIMIT @limit;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@moduleKey", moduleKey?.Trim() ?? string.Empty);
        command.Parameters.AddWithValue("@limit", Math.Clamp(limit, 1, 100));

        var items = new List<LegacySyncJobSummaryDto>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new LegacySyncJobSummaryDto
            {
                JobId = reader.GetGuid("id"),
                TenantId = reader.GetGuid("tenant_id"),
                CompanyId = reader.GetGuid("company_id"),
                CompanyName = reader.GetStringOrEmpty("company_name"),
                CompanyLegacyCenterCode = reader.GetStringOrEmpty("company_legacy_center_code"),
                ModuleKey = reader.GetStringOrEmpty("module_key"),
                ModuleDisplayName = reader.GetStringOrEmpty("module_display_name"),
                Status = reader.GetStringOrEmpty("status"),
                TriggeredByUserId = reader.GetNullableGuid("triggered_by_user_id"),
                TriggeredByScheduler = reader.GetBooleanValue("triggered_by_scheduler"),
                CheckpointBefore = reader.GetStringOrEmpty("checkpoint_before"),
                CheckpointAfter = reader.GetStringOrEmpty("checkpoint_after"),
                RecordsInserted = reader.GetInt32OrDefault("records_inserted"),
                RecordsUpdated = reader.GetInt32OrDefault("records_updated"),
                RecordsSkipped = reader.GetInt32OrDefault("records_skipped"),
                ErrorsCount = reader.GetInt32OrDefault("errors_count"),
                Summary = reader.GetStringOrEmpty("summary"),
                StartedUtc = reader.GetDateTime(reader.GetOrdinal("started_utc")),
                FinishedUtc = reader.IsDBNull(reader.GetOrdinal("finished_utc")) ? null : reader.GetDateTime(reader.GetOrdinal("finished_utc"))
            });
        }

        return items;
    }

    public async Task<IReadOnlyCollection<LegacySyncModuleStatusDto>> GetModuleStatusesAsync(Guid tenantId, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return [];
        }

        EnsureReadAccess(tenantId);

        var handlers = _handlers
            .Select(handler => new { handler.ModuleKey, handler.DisplayName })
            .ToArray();

        var companies = await LoadCompaniesAsync(tenantId, null, cancellationToken);
        var checkpoints = await LoadCheckpointsAsync(tenantId, cancellationToken);
        var statuses = new List<LegacySyncModuleStatusDto>();

        foreach (var company in companies)
        {
            foreach (var handler in handlers)
            {
                var checkpoint = checkpoints.FirstOrDefault(item =>
                    item.CompanyId == company.CompanyId &&
                    string.Equals(item.ModuleKey, handler.ModuleKey, StringComparison.OrdinalIgnoreCase));

                statuses.Add(new LegacySyncModuleStatusDto
                {
                    TenantId = tenantId,
                    CompanyId = company.CompanyId,
                    CompanyName = company.CompanyName,
                    CompanyLegacyCenterCode = company.LegacyCenterCode,
                    ModuleKey = handler.ModuleKey,
                    ModuleDisplayName = handler.DisplayName,
                    LastStatus = checkpoint?.LastStatus ?? string.Empty,
                    CheckpointValue = checkpoint?.CheckpointValue ?? string.Empty,
                    LastCompletedUtc = checkpoint?.LastCompletedUtc,
                    LastInserted = checkpoint?.LastInserted ?? 0,
                    LastUpdated = checkpoint?.LastUpdated ?? 0,
                    LastSkipped = checkpoint?.LastSkipped ?? 0,
                    LastErrors = checkpoint?.LastErrors ?? 0
                });
            }
        }

        return statuses;
    }

    private void EnsureAccess(RunLegacySyncCommand command)
    {
        if (command.TriggeredByScheduler)
        {
            return;
        }

        if (!_currentUserContext.IsAuthenticated)
        {
            throw new InvalidOperationException("Debes iniciar sesión para sincronizar datos legacy.");
        }

        if (!_tenantContext.TenantId.HasValue || _tenantContext.TenantId.Value != command.TenantId)
        {
            throw new InvalidOperationException("El tenant solicitado no coincide con tu sesión activa.");
        }

        if (_currentUserContext.IsPlatformAdmin ||
            _currentUserContext.Roles.Contains(PlatformRoles.TenantAdmin, StringComparer.OrdinalIgnoreCase))
        {
            return;
        }

        throw new InvalidOperationException("No tienes permisos para lanzar sincronizaciones legacy en este tenant.");
    }

    private void EnsureReadAccess(Guid tenantId)
    {
        if (!_currentUserContext.IsAuthenticated)
        {
            throw new InvalidOperationException("Debes iniciar sesión para consultar sincronizaciones legacy.");
        }

        if (_currentUserContext.IsPlatformAdmin)
        {
            return;
        }

        if (!_tenantContext.TenantId.HasValue || _tenantContext.TenantId.Value != tenantId)
        {
            throw new InvalidOperationException("El tenant solicitado no coincide con tu sesión activa.");
        }

        if (_currentUserContext.Roles.Contains(PlatformRoles.TenantAdmin, StringComparer.OrdinalIgnoreCase) ||
            _currentUserContext.Roles.Contains(PlatformRoles.TenantReader, StringComparer.OrdinalIgnoreCase))
        {
            return;
        }

        throw new InvalidOperationException("No tienes permisos para consultar sincronizaciones legacy en este tenant.");
    }

    private async Task<IReadOnlyCollection<SyncCompanyContext>> LoadCompaniesAsync(Guid tenantId, Guid? companyId, CancellationToken cancellationToken)
    {
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);

        HashSet<Guid>? allowedCompanyIds = null;
        if (!_currentUserContext.IsPlatformAdmin && _currentUserContext.UserId.HasValue)
        {
            var allowedCompanies = await _companyAccessService.GetAllowedCompaniesAsync(_currentUserContext.UserId.Value, tenantId, cancellationToken);
            allowedCompanyIds = allowedCompanies.Select(company => company.CompanyId).ToHashSet();
        }

        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT id, name, legacy_center_code
            FROM companies
            WHERE tenant_id = @tenantId
              AND is_active = 1
              AND (@companyId IS NULL OR id = @companyId)
            ORDER BY name;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.HasValue ? companyId.Value.ToString() : DBNull.Value);

        var items = new List<SyncCompanyContext>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var currentCompanyId = reader.GetGuid("id");
            if (allowedCompanyIds is not null && !allowedCompanyIds.Contains(currentCompanyId))
            {
                continue;
            }

            items.Add(new SyncCompanyContext(
                currentCompanyId,
                reader.GetStringOrEmpty("name"),
                reader.GetStringOrEmpty("legacy_center_code")));
        }

        if (companyId.HasValue && items.Count == 0)
        {
            throw new InvalidOperationException("La company seleccionada no existe o no está activa para este tenant.");
        }

        return items;
    }

    private async Task<IReadOnlyCollection<LegacySyncCheckpointDto>> LoadCheckpointsAsync(Guid tenantId, CancellationToken cancellationToken)
    {
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT tenant_id,
                   company_id,
                   module_key,
                   checkpoint_value,
                   last_successful_job_id,
                   last_started_utc,
                   last_completed_utc,
                   last_status,
                   last_inserted,
                   last_updated,
                   last_skipped,
                   last_errors
            FROM legacy_sync_checkpoints
            WHERE tenant_id = @tenantId;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());

        var items = new List<LegacySyncCheckpointDto>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new LegacySyncCheckpointDto
            {
                TenantId = reader.GetGuid("tenant_id"),
                CompanyId = reader.GetGuid("company_id"),
                ModuleKey = reader.GetStringOrEmpty("module_key"),
                CheckpointValue = reader.GetStringOrEmpty("checkpoint_value"),
                LastSuccessfulJobId = reader.GetNullableGuid("last_successful_job_id"),
                LastStartedUtc = reader.IsDBNull(reader.GetOrdinal("last_started_utc")) ? null : reader.GetDateTime(reader.GetOrdinal("last_started_utc")),
                LastCompletedUtc = reader.IsDBNull(reader.GetOrdinal("last_completed_utc")) ? null : reader.GetDateTime(reader.GetOrdinal("last_completed_utc")),
                LastStatus = reader.GetStringOrEmpty("last_status"),
                LastInserted = reader.GetInt32OrDefault("last_inserted"),
                LastUpdated = reader.GetInt32OrDefault("last_updated"),
                LastSkipped = reader.GetInt32OrDefault("last_skipped"),
                LastErrors = reader.GetInt32OrDefault("last_errors")
            });
        }

        return items;
    }

    private sealed record SyncCompanyContext(Guid CompanyId, string CompanyName, string LegacyCenterCode);
}
