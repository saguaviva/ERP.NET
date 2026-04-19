using Erp.Application.LegacySync;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Support;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.LegacySync;

public sealed class MySqlLegacySyncCheckpointRepository : ILegacySyncCheckpointRepository
{
    private readonly MySqlConnectionFactory _connectionFactory;

    public MySqlLegacySyncCheckpointRepository(MySqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<LegacySyncCheckpointDto?> GetAsync(
        Guid tenantId,
        Guid companyId,
        string moduleKey,
        CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return null;
        }

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
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND module_key = @moduleKey
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@moduleKey", moduleKey);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        return new LegacySyncCheckpointDto
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
        };
    }

    public async Task SaveAsync(LegacySyncCheckpointUpdate update, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return;
        }

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            INSERT INTO legacy_sync_checkpoints (
                tenant_id,
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
            )
            VALUES (
                @tenantId,
                @companyId,
                @moduleKey,
                @checkpointValue,
                @lastSuccessfulJobId,
                @lastStartedUtc,
                @lastCompletedUtc,
                @lastStatus,
                @lastInserted,
                @lastUpdated,
                @lastSkipped,
                @lastErrors
            )
            ON DUPLICATE KEY UPDATE
                checkpoint_value = VALUES(checkpoint_value),
                last_successful_job_id = VALUES(last_successful_job_id),
                last_started_utc = VALUES(last_started_utc),
                last_completed_utc = VALUES(last_completed_utc),
                last_status = VALUES(last_status),
                last_inserted = VALUES(last_inserted),
                last_updated = VALUES(last_updated),
                last_skipped = VALUES(last_skipped),
                last_errors = VALUES(last_errors);
            """;
        command.Parameters.AddWithValue("@tenantId", update.TenantId.ToString());
        command.Parameters.AddWithValue("@companyId", update.CompanyId.ToString());
        command.Parameters.AddWithValue("@moduleKey", update.ModuleKey);
        command.Parameters.AddWithValue("@checkpointValue", string.IsNullOrWhiteSpace(update.CheckpointValue) ? DBNull.Value : update.CheckpointValue);
        command.Parameters.AddWithValue("@lastSuccessfulJobId",
            string.Equals(update.Status, LegacySyncJobStatuses.Completed, StringComparison.OrdinalIgnoreCase) ||
            string.Equals(update.Status, LegacySyncJobStatuses.CompletedWithErrors, StringComparison.OrdinalIgnoreCase)
                ? update.JobId.ToString()
                : DBNull.Value);
        command.Parameters.AddWithValue("@lastStartedUtc", update.StartedUtc);
        command.Parameters.AddWithValue("@lastCompletedUtc", update.CompletedUtc.HasValue ? update.CompletedUtc.Value : DBNull.Value);
        command.Parameters.AddWithValue("@lastStatus", update.Status);
        command.Parameters.AddWithValue("@lastInserted", update.RecordsInserted);
        command.Parameters.AddWithValue("@lastUpdated", update.RecordsUpdated);
        command.Parameters.AddWithValue("@lastSkipped", update.RecordsSkipped);
        command.Parameters.AddWithValue("@lastErrors", update.ErrorsCount);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }
}
