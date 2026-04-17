using Erp.Application.Leads;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Support;

namespace Erp.Infrastructure.MySql.Leads;

public sealed class MySqlLeadCaptureService : ILeadCaptureService
{
    private readonly MySqlConnectionFactory _connectionFactory;

    public MySqlLeadCaptureService(MySqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task CaptureAsync(CreateLeadRequest request, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return;
        }

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            INSERT INTO lead_requests (id, contact_name, company_name, email, phone, requested_users, message, status, created_utc)
            VALUES (@id, @contactName, @companyName, @email, @phone, @requestedUsers, @message, @status, @createdUtc);
            """;
        command.Parameters.AddWithValue("@id", Guid.NewGuid().ToString());
        command.Parameters.AddWithValue("@contactName", request.ContactName.Trim());
        command.Parameters.AddWithValue("@companyName", request.CompanyName.Trim());
        command.Parameters.AddWithValue("@email", request.Email.Trim().ToLowerInvariant());
        command.Parameters.AddWithValue("@phone", request.Phone.Trim());
        command.Parameters.AddWithValue("@requestedUsers", Math.Max(1, request.RequestedUsers));
        command.Parameters.AddWithValue("@message", request.Message.Trim());
        command.Parameters.AddWithValue("@status", "New");
        command.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<LeadSummaryDto>> GetRecentAsync(CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return [];
        }

        var items = new List<LeadSummaryDto>();

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT id, contact_name, company_name, email, phone, requested_users, message, status, created_utc
            FROM lead_requests
            ORDER BY created_utc DESC
            LIMIT 25;
            """;

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new LeadSummaryDto
            {
                Id = reader.GetGuid("id"),
                ContactName = reader.GetStringOrEmpty("contact_name"),
                CompanyName = reader.GetStringOrEmpty("company_name"),
                Email = reader.GetStringOrEmpty("email"),
                Phone = reader.GetStringOrEmpty("phone"),
                RequestedUsers = reader.GetInt32(reader.GetOrdinal("requested_users")),
                Message = reader.GetStringOrEmpty("message"),
                Status = reader.GetStringOrEmpty("status"),
                CreatedUtc = reader.GetDateTime(reader.GetOrdinal("created_utc"))
            });
        }

        return items;
    }
}
