using Erp.Application.LegacySync;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Support;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.LegacySync;

public sealed class MySqlLegacySyncJobRunner : ILegacySyncJobRunner
{
    private readonly MySqlConnectionFactory _connectionFactory;
    private readonly ILegacySyncCheckpointRepository _checkpointRepository;
    private readonly IEnumerable<ILegacyModuleSyncHandler> _handlers;

    public MySqlLegacySyncJobRunner(
        MySqlConnectionFactory connectionFactory,
        ILegacySyncCheckpointRepository checkpointRepository,
        IEnumerable<ILegacyModuleSyncHandler> handlers)
    {
        _connectionFactory = connectionFactory;
        _checkpointRepository = checkpointRepository;
        _handlers = handlers;
    }

    public async Task<LegacySyncJobSummaryDto> RunAsync(LegacySyncJobRequest request, CancellationToken cancellationToken = default)
    {
        var handler = _handlers.FirstOrDefault(candidate =>
            string.Equals(candidate.ModuleKey, request.ModuleKey, StringComparison.OrdinalIgnoreCase));
        if (handler is null)
        {
            throw new InvalidOperationException($"No hay ningún sincronizador registrado para el módulo '{request.ModuleKey}'.");
        }

        var checkpoint = await _checkpointRepository.GetAsync(request.TenantId, request.CompanyId, request.ModuleKey, cancellationToken);
        var job = new LegacySyncJobSummaryDto
        {
            JobId = Guid.NewGuid(),
            TenantId = request.TenantId,
            CompanyId = request.CompanyId,
            CompanyName = request.CompanyName,
            CompanyLegacyCenterCode = request.CompanyLegacyCenterCode,
            ModuleKey = request.ModuleKey,
            ModuleDisplayName = handler.DisplayName,
            Status = LegacySyncJobStatuses.Running,
            TriggeredByScheduler = request.TriggeredByScheduler,
            TriggeredByUserId = request.TriggeredByUserId,
            CheckpointBefore = checkpoint?.CheckpointValue ?? string.Empty,
            StartedUtc = DateTime.UtcNow
        };

        await InsertJobAsync(job, cancellationToken);

        LegacySyncModuleRunResult runResult;
        try
        {
            runResult = await handler.RunAsync(new LegacySyncModuleContext
            {
                JobId = job.JobId,
                TenantId = request.TenantId,
                CompanyId = request.CompanyId,
                CompanyName = request.CompanyName,
                CompanyLegacyCenterCode = request.CompanyLegacyCenterCode,
                ModuleKey = request.ModuleKey,
                CheckpointValue = checkpoint?.CheckpointValue,
                ForceFullRefresh = request.ForceFullRefresh
            }, cancellationToken);
        }
        catch (Exception exception)
        {
            await InsertErrorAsync(
                job.JobId,
                request.TenantId,
                request.CompanyId,
                request.ModuleKey,
                "JobRunner",
                $"{request.CompanyLegacyCenterCode}/global",
                exception.Message,
                string.Empty,
                cancellationToken);

            job.Status = LegacySyncJobStatuses.Failed;
            job.FinishedUtc = DateTime.UtcNow;
            job.ErrorsCount = 1;
            job.Summary = exception.Message;
            await UpdateJobAsync(job, cancellationToken);
            await _checkpointRepository.SaveAsync(new LegacySyncCheckpointUpdate
            {
                TenantId = request.TenantId,
                CompanyId = request.CompanyId,
                ModuleKey = request.ModuleKey,
                CheckpointValue = checkpoint?.CheckpointValue,
                JobId = job.JobId,
                StartedUtc = job.StartedUtc,
                CompletedUtc = job.FinishedUtc,
                Status = job.Status,
                ErrorsCount = job.ErrorsCount
            }, cancellationToken);
            return job;
        }

        if (runResult.Mappings.Count > 0)
        {
            await UpsertMappingsAsync(job.JobId, request.TenantId, request.CompanyId, request.ModuleKey, runResult.Mappings, cancellationToken);
        }

        if (runResult.Errors.Count > 0)
        {
            foreach (var error in runResult.Errors)
            {
                await InsertErrorAsync(
                    job.JobId,
                    request.TenantId,
                    request.CompanyId,
                    request.ModuleKey,
                    error.Stage,
                    error.LegacyEntityKey,
                    error.ErrorMessage,
                    error.Payload,
                    cancellationToken);
            }
        }

        job.Status = runResult.Errors.Count == 0 ? LegacySyncJobStatuses.Completed : LegacySyncJobStatuses.CompletedWithErrors;
        job.CheckpointAfter = runResult.NewCheckpointValue ?? string.Empty;
        job.RecordsInserted = runResult.RecordsInserted;
        job.RecordsUpdated = runResult.RecordsUpdated;
        job.RecordsSkipped = runResult.RecordsSkipped;
        job.ErrorsCount = runResult.Errors.Count;
        job.Summary = runResult.Summary;
        job.FinishedUtc = DateTime.UtcNow;

        await UpdateJobAsync(job, cancellationToken);
        await _checkpointRepository.SaveAsync(new LegacySyncCheckpointUpdate
        {
            TenantId = request.TenantId,
            CompanyId = request.CompanyId,
            ModuleKey = request.ModuleKey,
            CheckpointValue = runResult.NewCheckpointValue ?? checkpoint?.CheckpointValue,
            JobId = job.JobId,
            StartedUtc = job.StartedUtc,
            CompletedUtc = job.FinishedUtc,
            Status = job.Status,
            RecordsInserted = job.RecordsInserted,
            RecordsUpdated = job.RecordsUpdated,
            RecordsSkipped = job.RecordsSkipped,
            ErrorsCount = job.ErrorsCount
        }, cancellationToken);

        return job;
    }

    private async Task InsertJobAsync(LegacySyncJobSummaryDto job, CancellationToken cancellationToken)
    {
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            INSERT INTO legacy_sync_jobs (
                id,
                tenant_id,
                company_id,
                module_key,
                module_display_name,
                status,
                triggered_by_user_id,
                triggered_by_scheduler,
                checkpoint_before,
                checkpoint_after,
                records_inserted,
                records_updated,
                records_skipped,
                errors_count,
                summary,
                started_utc,
                finished_utc
            )
            VALUES (
                @id,
                @tenantId,
                @companyId,
                @moduleKey,
                @moduleDisplayName,
                @status,
                @triggeredByUserId,
                @triggeredByScheduler,
                @checkpointBefore,
                NULL,
                0,
                0,
                0,
                0,
                '',
                @startedUtc,
                NULL
            );
            """;
        command.Parameters.AddWithValue("@id", job.JobId.ToString());
        command.Parameters.AddWithValue("@tenantId", job.TenantId.ToString());
        command.Parameters.AddWithValue("@companyId", job.CompanyId.ToString());
        command.Parameters.AddWithValue("@moduleKey", job.ModuleKey);
        command.Parameters.AddWithValue("@moduleDisplayName", job.ModuleDisplayName);
        command.Parameters.AddWithValue("@status", job.Status);
        command.Parameters.AddWithValue("@triggeredByUserId", job.TriggeredByUserId.HasValue ? job.TriggeredByUserId.Value.ToString() : DBNull.Value);
        command.Parameters.AddWithValue("@triggeredByScheduler", job.TriggeredByScheduler);
        command.Parameters.AddWithValue("@checkpointBefore", string.IsNullOrWhiteSpace(job.CheckpointBefore) ? DBNull.Value : job.CheckpointBefore);
        command.Parameters.AddWithValue("@startedUtc", job.StartedUtc);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private async Task UpdateJobAsync(LegacySyncJobSummaryDto job, CancellationToken cancellationToken)
    {
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            UPDATE legacy_sync_jobs
            SET status = @status,
                checkpoint_after = @checkpointAfter,
                records_inserted = @recordsInserted,
                records_updated = @recordsUpdated,
                records_skipped = @recordsSkipped,
                errors_count = @errorsCount,
                summary = @summary,
                finished_utc = @finishedUtc
            WHERE id = @id;
            """;
        command.Parameters.AddWithValue("@id", job.JobId.ToString());
        command.Parameters.AddWithValue("@status", job.Status);
        command.Parameters.AddWithValue("@checkpointAfter", string.IsNullOrWhiteSpace(job.CheckpointAfter) ? DBNull.Value : job.CheckpointAfter);
        command.Parameters.AddWithValue("@recordsInserted", job.RecordsInserted);
        command.Parameters.AddWithValue("@recordsUpdated", job.RecordsUpdated);
        command.Parameters.AddWithValue("@recordsSkipped", job.RecordsSkipped);
        command.Parameters.AddWithValue("@errorsCount", job.ErrorsCount);
        command.Parameters.AddWithValue("@summary", string.IsNullOrWhiteSpace(job.Summary) ? DBNull.Value : job.Summary);
        command.Parameters.AddWithValue("@finishedUtc", job.FinishedUtc.HasValue ? job.FinishedUtc.Value : DBNull.Value);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private async Task InsertErrorAsync(
        Guid jobId,
        Guid tenantId,
        Guid companyId,
        string moduleKey,
        string stage,
        string legacyEntityKey,
        string errorMessage,
        string payload,
        CancellationToken cancellationToken)
    {
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            INSERT INTO legacy_sync_errors (
                id,
                job_id,
                tenant_id,
                company_id,
                module_key,
                stage,
                legacy_entity_key,
                error_message,
                payload,
                created_utc
            )
            VALUES (
                @id,
                @jobId,
                @tenantId,
                @companyId,
                @moduleKey,
                @stage,
                @legacyEntityKey,
                @errorMessage,
                @payload,
                @createdUtc
            );
            """;
        command.Parameters.AddWithValue("@id", Guid.NewGuid().ToString());
        command.Parameters.AddWithValue("@jobId", jobId.ToString());
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@moduleKey", moduleKey);
        command.Parameters.AddWithValue("@stage", stage);
        command.Parameters.AddWithValue("@legacyEntityKey", legacyEntityKey);
        command.Parameters.AddWithValue("@errorMessage", errorMessage);
        command.Parameters.AddWithValue("@payload", string.IsNullOrWhiteSpace(payload) ? DBNull.Value : payload);
        command.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private async Task UpsertMappingsAsync(
        Guid jobId,
        Guid tenantId,
        Guid companyId,
        string moduleKey,
        IReadOnlyCollection<LegacySyncMappingRecord> mappings,
        CancellationToken cancellationToken)
    {
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        foreach (var mapping in mappings)
        {
            await using var command = connection.CreateCommand();
            command.CommandText =
                """
                INSERT INTO legacy_sync_mappings (
                    tenant_id,
                    company_id,
                    module_key,
                    legacy_source_system,
                    legacy_center_code,
                    legacy_document_type,
                    legacy_document_number,
                    legacy_line_number,
                    target_entity_name,
                    target_entity_id,
                    job_id,
                    synced_utc
                )
                VALUES (
                    @tenantId,
                    @companyId,
                    @moduleKey,
                    @legacySourceSystem,
                    @legacyCenterCode,
                    @legacyDocumentType,
                    @legacyDocumentNumber,
                    @legacyLineNumber,
                    @targetEntityName,
                    @targetEntityId,
                    @jobId,
                    @syncedUtc
                )
                ON DUPLICATE KEY UPDATE
                    target_entity_id = VALUES(target_entity_id),
                    job_id = VALUES(job_id),
                    synced_utc = VALUES(synced_utc);
                """;
            command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            command.Parameters.AddWithValue("@companyId", companyId.ToString());
            command.Parameters.AddWithValue("@moduleKey", moduleKey);
            command.Parameters.AddWithValue("@legacySourceSystem", mapping.LegacySourceSystem);
            command.Parameters.AddWithValue("@legacyCenterCode", mapping.LegacyCenterCode);
            command.Parameters.AddWithValue("@legacyDocumentType", mapping.LegacyDocumentType);
            command.Parameters.AddWithValue("@legacyDocumentNumber", mapping.LegacyDocumentNumber);
            command.Parameters.AddWithValue("@legacyLineNumber", mapping.LegacyLineNumber ?? 0);
            command.Parameters.AddWithValue("@targetEntityName", mapping.TargetEntityName);
            command.Parameters.AddWithValue("@targetEntityId", mapping.TargetEntityId);
            command.Parameters.AddWithValue("@jobId", jobId.ToString());
            command.Parameters.AddWithValue("@syncedUtc", DateTime.UtcNow);
            await command.ExecuteNonQueryAsync(cancellationToken);
        }
    }
}
