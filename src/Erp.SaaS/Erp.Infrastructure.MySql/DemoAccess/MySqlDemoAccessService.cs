using System.Text.RegularExpressions;
using Erp.Application.Auditing;
using Erp.Application.DemoAccess;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Support;

namespace Erp.Infrastructure.MySql.DemoAccess;

public sealed class MySqlDemoAccessService : IDemoAccessService
{
    private static readonly Regex EmailSplitRegex = new("""[\s,;]+""", RegexOptions.Compiled);

    private readonly MySqlConnectionFactory _connectionFactory;
    private readonly IAuditLogService _auditLogService;

    public MySqlDemoAccessService(MySqlConnectionFactory connectionFactory, IAuditLogService auditLogService)
    {
        _connectionFactory = connectionFactory;
        _auditLogService = auditLogService;
    }

    public async Task CaptureRequestAsync(CreateDemoAccessRequest request, CancellationToken cancellationToken = default)
    {
        ValidateRequest(request);

        if (!_connectionFactory.IsConfigured)
        {
            return;
        }

        var requestId = Guid.NewGuid();
        var requestedEmails = NormalizeEmails(request.TesterEmails);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            INSERT INTO demo_access_requests (
                id,
                contact_name,
                company_name,
                email,
                phone,
                requested_users,
                tester_emails,
                message,
                status,
                created_utc,
                reviewed_utc,
                reviewed_by_user_id)
            VALUES (
                @id,
                @contactName,
                @companyName,
                @email,
                @phone,
                @requestedUsers,
                @testerEmails,
                @message,
                @status,
                @createdUtc,
                NULL,
                NULL);
            """;
        command.Parameters.AddWithValue("@id", requestId.ToString());
        command.Parameters.AddWithValue("@contactName", request.ContactName.Trim());
        command.Parameters.AddWithValue("@companyName", request.CompanyName.Trim());
        command.Parameters.AddWithValue("@email", request.Email.Trim().ToLowerInvariant());
        command.Parameters.AddWithValue("@phone", request.Phone.Trim());
        command.Parameters.AddWithValue("@requestedUsers", Math.Max(1, request.RequestedUsers));
        command.Parameters.AddWithValue("@testerEmails", string.Join('\n', requestedEmails));
        command.Parameters.AddWithValue("@message", request.Message.Trim());
        command.Parameters.AddWithValue("@status", "Pending");
        command.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
        await command.ExecuteNonQueryAsync(cancellationToken);

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            Action = "DemoAccessRequested",
            EntityName = "DemoAccessRequest",
            EntityId = requestId.ToString(),
            Details = $"Company={request.CompanyName.Trim()}; Contact={request.ContactName.Trim()}; RequestedUsers={Math.Max(1, request.RequestedUsers)}; TesterEmails={string.Join(", ", requestedEmails)}"
        }, cancellationToken);
    }

    public async Task<IReadOnlyCollection<DemoAccessRequestSummaryDto>> GetRecentRequestsAsync(CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return [];
        }

        var items = new List<DemoAccessRequestSummaryDto>();

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT id, contact_name, company_name, email, phone, requested_users, tester_emails, message, status, created_utc, reviewed_utc, reviewed_by_user_id
            FROM demo_access_requests
            ORDER BY created_utc DESC
            LIMIT 50;
            """;

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new DemoAccessRequestSummaryDto
            {
                Id = reader.GetGuid("id"),
                ContactName = reader.GetStringOrEmpty("contact_name"),
                CompanyName = reader.GetStringOrEmpty("company_name"),
                Email = reader.GetStringOrEmpty("email"),
                Phone = reader.GetStringOrEmpty("phone"),
                RequestedUsers = reader.GetInt32(reader.GetOrdinal("requested_users")),
                Message = reader.GetStringOrEmpty("message"),
                Status = reader.GetStringOrEmpty("status"),
                CreatedUtc = reader.GetDateTime(reader.GetOrdinal("created_utc")),
                ReviewedUtc = reader.IsDBNull(reader.GetOrdinal("reviewed_utc")) ? null : reader.GetDateTime(reader.GetOrdinal("reviewed_utc")),
                ReviewedByUserId = reader.GetNullableGuid("reviewed_by_user_id"),
                RequestedTesterEmails = NormalizeEmails(reader.GetStringOrEmpty("tester_emails"))
            });
        }

        return items;
    }

    public async Task<IReadOnlyCollection<PreviewInviteSummaryDto>> GetRecentInvitesAsync(CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return [];
        }

        var items = new List<PreviewInviteSummaryDto>();

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT id, email, display_name, company_name, notes, source_request_id, is_active, created_utc, updated_utc
            FROM preview_access_invites
            ORDER BY updated_utc DESC, created_utc DESC
            LIMIT 100;
            """;

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new PreviewInviteSummaryDto
            {
                Id = reader.GetGuid("id"),
                Email = reader.GetStringOrEmpty("email"),
                DisplayName = reader.GetStringOrEmpty("display_name"),
                CompanyName = reader.GetStringOrEmpty("company_name"),
                Notes = reader.GetStringOrEmpty("notes"),
                SourceRequestId = reader.GetNullableGuid("source_request_id"),
                IsActive = reader.GetBooleanValue(reader.GetOrdinal("is_active")),
                CreatedUtc = reader.GetDateTime(reader.GetOrdinal("created_utc")),
                UpdatedUtc = reader.GetDateTime(reader.GetOrdinal("updated_utc"))
            });
        }

        return items;
    }

    public async Task<bool> IsEmailAllowedAsync(string email, CancellationToken cancellationToken = default)
    {
        var normalizedEmail = NormalizeSingleEmail(email);
        if (string.IsNullOrWhiteSpace(normalizedEmail) || !_connectionFactory.IsConfigured)
        {
            return false;
        }

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT COUNT(*)
            FROM preview_access_invites
            WHERE email = @email
              AND is_active = 1;
            """;
        command.Parameters.AddWithValue("@email", normalizedEmail);
        return Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken)) > 0;
    }

    public async Task CreateInviteAsync(CreatePreviewInviteCommand command, CancellationToken cancellationToken = default)
    {
        var normalizedEmail = NormalizeSingleEmail(command.Email);
        if (string.IsNullOrWhiteSpace(normalizedEmail))
        {
            throw new InvalidOperationException("El email de la invitación es obligatorio.");
        }

        if (!_connectionFactory.IsConfigured)
        {
            return;
        }

        var inviteId = Guid.NewGuid();
        var nowUtc = DateTime.UtcNow;

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await UpsertInviteAsync(
            connection,
            inviteId,
            normalizedEmail,
            command.DisplayName,
            command.CompanyName,
            command.Notes,
            command.SourceRequestId,
            command.CreatedByUserId,
            nowUtc,
            cancellationToken);

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            UserId = command.CreatedByUserId,
            Action = "PreviewAccessInviteCreated",
            EntityName = "PreviewAccessInvite",
            EntityId = normalizedEmail,
            Details = $"Company={command.CompanyName.Trim()}; SourceRequestId={command.SourceRequestId?.ToString() ?? "Manual"}"
        }, cancellationToken);
    }

    public async Task CreateInvitesFromRequestAsync(CreatePreviewInvitesFromRequestCommand command, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return;
        }

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var request = await GetRequestAsync(connection, command.RequestId, cancellationToken);
        if (request is null)
        {
            throw new InvalidOperationException("La solicitud de acceso de prueba no existe.");
        }

        var emails = request.RequestedTesterEmails
            .Concat([request.Email])
            .Where(email => !string.IsNullOrWhiteSpace(email))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();

        if (emails.Length == 0)
        {
            throw new InvalidOperationException("La solicitud no contiene ningún email válido para invitar.");
        }

        var nowUtc = DateTime.UtcNow;
        foreach (var email in emails)
        {
            await UpsertInviteAsync(
                connection,
                Guid.NewGuid(),
                email,
                request.ContactName,
                request.CompanyName,
                $"Invitación generada desde solicitud {request.Id}",
                request.Id,
                command.CreatedByUserId,
                nowUtc,
                cancellationToken);
        }

        await using var updateRequestCommand = connection.CreateCommand();
        updateRequestCommand.CommandText =
            """
            UPDATE demo_access_requests
            SET status = @status,
                reviewed_utc = @reviewedUtc,
                reviewed_by_user_id = @reviewedByUserId
            WHERE id = @requestId;
            """;
        updateRequestCommand.Parameters.AddWithValue("@status", "Invited");
        updateRequestCommand.Parameters.AddWithValue("@reviewedUtc", nowUtc);
        updateRequestCommand.Parameters.AddWithValue("@reviewedByUserId", command.CreatedByUserId?.ToString());
        updateRequestCommand.Parameters.AddWithValue("@requestId", command.RequestId.ToString());
        await updateRequestCommand.ExecuteNonQueryAsync(cancellationToken);

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            UserId = command.CreatedByUserId,
            Action = "DemoAccessRequestInvited",
            EntityName = "DemoAccessRequest",
            EntityId = command.RequestId.ToString(),
            Details = $"Invites={string.Join(", ", emails)}"
        }, cancellationToken);
    }

    public async Task SetInviteActiveAsync(SetPreviewInviteActiveCommand command, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return;
        }

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var updateCommand = connection.CreateCommand();
        updateCommand.CommandText =
            """
            UPDATE preview_access_invites
            SET is_active = @isActive,
                updated_utc = @updatedUtc
            WHERE id = @inviteId;
            """;
        updateCommand.Parameters.AddWithValue("@isActive", command.IsActive);
        updateCommand.Parameters.AddWithValue("@updatedUtc", DateTime.UtcNow);
        updateCommand.Parameters.AddWithValue("@inviteId", command.InviteId.ToString());

        var affected = await updateCommand.ExecuteNonQueryAsync(cancellationToken);
        if (affected == 0)
        {
            throw new InvalidOperationException("La invitación de pruebas ya no existe.");
        }

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            UserId = command.UpdatedByUserId,
            Action = command.IsActive ? "PreviewAccessInviteActivated" : "PreviewAccessInviteDeactivated",
            EntityName = "PreviewAccessInvite",
            EntityId = command.InviteId.ToString()
        }, cancellationToken);
    }

    private static void ValidateRequest(CreateDemoAccessRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.ContactName) ||
            string.IsNullOrWhiteSpace(request.CompanyName) ||
            string.IsNullOrWhiteSpace(request.Email) ||
            request.RequestedUsers <= 0)
        {
            throw new InvalidOperationException("Nombre, empresa, email y número de usuarios son obligatorios.");
        }

        if (NormalizeSingleEmail(request.Email) is null)
        {
            throw new InvalidOperationException("El email de contacto no es válido.");
        }
    }

    private static string? NormalizeSingleEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return null;
        }

        var normalized = email.Trim().ToLowerInvariant();
        return normalized.Contains('@') ? normalized : null;
    }

    private static string[] NormalizeEmails(string? emails) =>
        EmailSplitRegex.Split(emails ?? string.Empty)
            .Select(NormalizeSingleEmail)
            .Where(static email => !string.IsNullOrWhiteSpace(email))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray()!;

    private static async Task UpsertInviteAsync(
        global::MySqlConnector.MySqlConnection connection,
        Guid inviteId,
        string email,
        string displayName,
        string companyName,
        string notes,
        Guid? sourceRequestId,
        Guid? createdByUserId,
        DateTime nowUtc,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            INSERT INTO preview_access_invites (
                id,
                email,
                display_name,
                company_name,
                notes,
                source_request_id,
                created_by_user_id,
                is_active,
                created_utc,
                updated_utc)
            VALUES (
                @id,
                @email,
                @displayName,
                @companyName,
                @notes,
                @sourceRequestId,
                @createdByUserId,
                1,
                @createdUtc,
                @updatedUtc)
            ON DUPLICATE KEY UPDATE
                display_name = VALUES(display_name),
                company_name = VALUES(company_name),
                notes = VALUES(notes),
                source_request_id = VALUES(source_request_id),
                created_by_user_id = VALUES(created_by_user_id),
                is_active = 1,
                updated_utc = VALUES(updated_utc);
            """;
        command.Parameters.AddWithValue("@id", inviteId.ToString());
        command.Parameters.AddWithValue("@email", email);
        command.Parameters.AddWithValue("@displayName", displayName.Trim());
        command.Parameters.AddWithValue("@companyName", companyName.Trim());
        command.Parameters.AddWithValue("@notes", notes.Trim());
        command.Parameters.AddWithValue("@sourceRequestId", sourceRequestId?.ToString());
        command.Parameters.AddWithValue("@createdByUserId", createdByUserId?.ToString());
        command.Parameters.AddWithValue("@createdUtc", nowUtc);
        command.Parameters.AddWithValue("@updatedUtc", nowUtc);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task<DemoAccessRequestSummaryDto?> GetRequestAsync(
        global::MySqlConnector.MySqlConnection connection,
        Guid requestId,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT id, contact_name, company_name, email, phone, requested_users, tester_emails, message, status, created_utc, reviewed_utc, reviewed_by_user_id
            FROM demo_access_requests
            WHERE id = @requestId
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@requestId", requestId.ToString());

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        return new DemoAccessRequestSummaryDto
        {
            Id = reader.GetGuid("id"),
            ContactName = reader.GetStringOrEmpty("contact_name"),
            CompanyName = reader.GetStringOrEmpty("company_name"),
            Email = reader.GetStringOrEmpty("email"),
            Phone = reader.GetStringOrEmpty("phone"),
            RequestedUsers = reader.GetInt32(reader.GetOrdinal("requested_users")),
            Message = reader.GetStringOrEmpty("message"),
            Status = reader.GetStringOrEmpty("status"),
            CreatedUtc = reader.GetDateTime(reader.GetOrdinal("created_utc")),
            ReviewedUtc = reader.IsDBNull(reader.GetOrdinal("reviewed_utc")) ? null : reader.GetDateTime(reader.GetOrdinal("reviewed_utc")),
            ReviewedByUserId = reader.GetNullableGuid("reviewed_by_user_id"),
            RequestedTesterEmails = NormalizeEmails(reader.GetStringOrEmpty("tester_emails"))
        };
    }
}
