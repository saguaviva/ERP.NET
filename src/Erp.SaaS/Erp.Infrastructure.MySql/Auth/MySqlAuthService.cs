using Erp.Application.Auth;
using Erp.Application.Companies;
using Erp.Domain.Common;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Support;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Auth;

public sealed class MySqlAuthService : IAuthService
{
    private readonly MySqlConnectionFactory _connectionFactory;
    private readonly ICompanyAccessService _companyAccessService;

    public MySqlAuthService(MySqlConnectionFactory connectionFactory, ICompanyAccessService companyAccessService)
    {
        _connectionFactory = connectionFactory;
        _companyAccessService = companyAccessService;
    }

    public async Task<AuthenticatedSession?> AuthenticateAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured ||
            string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.Password))
        {
            return null;
        }

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT id, email, display_name, password_hash
            FROM app_users
            WHERE email = @email
              AND is_active = 1
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@email", request.Email.Trim().ToLowerInvariant());

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        var userId = reader.GetGuid("id");
        var email = reader.GetStringOrEmpty("email");
        var displayName = reader.GetStringOrEmpty("display_name");
        var passwordHash = reader.GetStringOrEmpty("password_hash");

        await reader.DisposeAsync();

        if (!PasswordHasher.Verify(request.Password, passwordHash))
        {
            return null;
        }

        var tenantId = await GetDefaultTenantIdAsync(connection, userId, cancellationToken);
        var roles = await GetRolesAsync(connection, userId, tenantId, cancellationToken);
        var allowedCompanies = tenantId.HasValue
            ? await _companyAccessService.GetAllowedCompaniesAsync(userId, tenantId.Value, cancellationToken)
            : [];

        return new AuthenticatedSession
        {
            UserId = userId,
            Email = email,
            DisplayName = displayName,
            TenantId = tenantId,
            IsPlatformAdmin = roles.Contains(PlatformRoles.PlatformAdmin, StringComparer.OrdinalIgnoreCase),
            Roles = roles,
            AllowedCompanies = allowedCompanies
        };
    }

    private static async Task<Guid?> GetDefaultTenantIdAsync(MySqlConnection connection, Guid userId, CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT tenant_id
            FROM user_tenant_memberships
            WHERE user_id = @userId
            ORDER BY is_default DESC, created_utc ASC
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@userId", userId.ToString());
        var scalar = await command.ExecuteScalarAsync(cancellationToken);
        return scalar is null ? null : Guid.Parse(Convert.ToString(scalar)!);
    }

    private static async Task<IReadOnlyCollection<string>> GetRolesAsync(
        MySqlConnection connection,
        Guid userId,
        Guid? tenantId,
        CancellationToken cancellationToken)
    {
        var roles = new List<string>();

        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT role_name
            FROM user_role_assignments
            WHERE user_id = @userId
              AND (tenant_id = @tenantId OR tenant_id IS NULL);
            """;
        command.Parameters.AddWithValue("@userId", userId.ToString());
        command.Parameters.AddWithValue("@tenantId", tenantId?.ToString());

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            roles.Add(reader.GetStringOrEmpty("role_name"));
        }

        return roles
            .Where(role => !string.IsNullOrWhiteSpace(role))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();
    }
}
