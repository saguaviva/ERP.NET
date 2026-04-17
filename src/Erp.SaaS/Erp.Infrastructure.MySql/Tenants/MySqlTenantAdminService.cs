using Erp.Application.Tenants;
using Erp.Domain.Common;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Support;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Tenants;

public sealed class MySqlTenantAdminService : ITenantAdminService
{
    private readonly MySqlConnectionFactory _connectionFactory;

    public MySqlTenantAdminService(MySqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IReadOnlyCollection<TenantSummaryDto>> GetTenantsAsync(CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return [];
        }

        var items = new List<TenantSummaryDto>();

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = "SELECT id, name, slug, is_active FROM tenants ORDER BY name;";

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new TenantSummaryDto
            {
                Id = reader.GetGuid("id"),
                Name = reader.GetStringOrEmpty("name"),
                Slug = reader.GetStringOrEmpty("slug"),
                IsActive = reader.GetBoolean("is_active")
            });
        }

        return items;
    }

    public async Task<IReadOnlyCollection<CompanySummaryDto>> GetCompaniesAsync(Guid? tenantId = null, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return [];
        }

        var items = new List<CompanySummaryDto>();

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT id, tenant_id, name, slug, legacy_center_code, is_active
            FROM companies
            WHERE @tenantId IS NULL OR tenant_id = @tenantId
            ORDER BY name;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId?.ToString());

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new CompanySummaryDto
            {
                Id = reader.GetGuid("id"),
                TenantId = reader.GetGuid("tenant_id"),
                Name = reader.GetStringOrEmpty("name"),
                Slug = reader.GetStringOrEmpty("slug"),
                LegacyCenterCode = reader.GetStringOrEmpty("legacy_center_code"),
                IsActive = reader.GetBoolean("is_active")
            });
        }

        return items;
    }

    public async Task<IReadOnlyCollection<UserSummaryDto>> GetUsersAsync(Guid? tenantId = null, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return [];
        }

        var users = new List<UserSummaryDto>();

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT DISTINCT u.id, utm.tenant_id, u.email, u.display_name, u.is_active
            FROM app_users u
            LEFT JOIN user_tenant_memberships utm ON utm.user_id = u.id
            WHERE @tenantId IS NULL OR utm.tenant_id = @tenantId
            ORDER BY u.display_name, u.email;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId?.ToString());

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            users.Add(new UserSummaryDto
            {
                Id = reader.GetGuid("id"),
                TenantId = reader.GetNullableGuid("tenant_id"),
                Email = reader.GetStringOrEmpty("email"),
                DisplayName = reader.GetStringOrEmpty("display_name"),
                IsActive = reader.GetBoolean("is_active")
            });
        }

        await reader.DisposeAsync();

        foreach (var user in users)
        {
            user.Roles = await GetUserRolesAsync(connection, user.Id, user.TenantId, cancellationToken);
            user.CompanyIds = await GetUserCompanyIdsAsync(connection, user.Id, user.TenantId, cancellationToken);
        }

        return users;
    }

    public async Task<TenantSummaryDto> CreateTenantAsync(CreateTenantCommand command, CancellationToken cancellationToken = default)
    {
        var tenant = new TenantSummaryDto
        {
            Id = Guid.NewGuid(),
            Name = command.Name.Trim(),
            Slug = SlugGenerator.Generate(string.IsNullOrWhiteSpace(command.Slug) ? command.Name : command.Slug),
            IsActive = true
        };

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var insertCommand = connection.CreateCommand();
        insertCommand.CommandText =
            """
            INSERT INTO tenants (id, name, slug, is_active, created_utc)
            VALUES (@id, @name, @slug, 1, @createdUtc);
            """;
        insertCommand.Parameters.AddWithValue("@id", tenant.Id.ToString());
        insertCommand.Parameters.AddWithValue("@name", tenant.Name);
        insertCommand.Parameters.AddWithValue("@slug", tenant.Slug);
        insertCommand.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
        await insertCommand.ExecuteNonQueryAsync(cancellationToken);

        return tenant;
    }

    public async Task<CompanySummaryDto> CreateCompanyAsync(CreateCompanyCommand command, CancellationToken cancellationToken = default)
    {
        var company = new CompanySummaryDto
        {
            Id = Guid.NewGuid(),
            TenantId = command.TenantId,
            Name = command.Name.Trim(),
            Slug = SlugGenerator.Generate(string.IsNullOrWhiteSpace(command.Slug) ? command.Name : command.Slug),
            LegacyCenterCode = command.LegacyCenterCode.Trim().ToUpperInvariant(),
            IsActive = true
        };

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var insertCommand = connection.CreateCommand();
        insertCommand.CommandText =
            """
            INSERT INTO companies (id, tenant_id, name, slug, legacy_center_code, is_active, created_utc)
            VALUES (@id, @tenantId, @name, @slug, @legacyCenterCode, 1, @createdUtc);
            """;
        insertCommand.Parameters.AddWithValue("@id", company.Id.ToString());
        insertCommand.Parameters.AddWithValue("@tenantId", company.TenantId.ToString());
        insertCommand.Parameters.AddWithValue("@name", company.Name);
        insertCommand.Parameters.AddWithValue("@slug", company.Slug);
        insertCommand.Parameters.AddWithValue("@legacyCenterCode", company.LegacyCenterCode);
        insertCommand.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
        await insertCommand.ExecuteNonQueryAsync(cancellationToken);

        return company;
    }

    public async Task<UserSummaryDto> CreateUserAsync(CreateUserCommand command, CancellationToken cancellationToken = default)
    {
        var userId = Guid.NewGuid();

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        try
        {
            await using (var userCommand = connection.CreateCommand())
            {
                userCommand.Transaction = transaction;
                userCommand.CommandText =
                    """
                    INSERT INTO app_users (id, email, display_name, password_hash, is_active, created_utc)
                    VALUES (@id, @email, @displayName, @passwordHash, 1, @createdUtc);
                    """;
                userCommand.Parameters.AddWithValue("@id", userId.ToString());
                userCommand.Parameters.AddWithValue("@email", command.Email.Trim().ToLowerInvariant());
                userCommand.Parameters.AddWithValue("@displayName", command.DisplayName.Trim());
                userCommand.Parameters.AddWithValue("@passwordHash", PasswordHasher.Hash(command.Password));
                userCommand.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
                await userCommand.ExecuteNonQueryAsync(cancellationToken);
            }

            await EnsureTenantMembershipAsync(connection, transaction, userId, command.TenantId, cancellationToken);
            await ReplaceUserCompaniesAsync(connection, transaction, userId, command.TenantId, command.CompanyIds, cancellationToken);
            await ReplaceUserRolesAsync(connection, transaction, userId, command.TenantId, command.Roles, cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }

        return new UserSummaryDto
        {
            Id = userId,
            TenantId = command.TenantId,
            Email = command.Email.Trim().ToLowerInvariant(),
            DisplayName = command.DisplayName.Trim(),
            IsActive = true,
            Roles = command.Roles.ToArray(),
            CompanyIds = command.CompanyIds.ToArray()
        };
    }

    public async Task AssignUserCompaniesAsync(AssignUserCompaniesCommand command, CancellationToken cancellationToken = default)
    {
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        try
        {
            await EnsureTenantMembershipAsync(connection, transaction, command.UserId, command.TenantId, cancellationToken);
            await ReplaceUserCompaniesAsync(connection, transaction, command.UserId, command.TenantId, command.CompanyIds, cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task AssignUserRolesAsync(AssignUserRolesCommand command, CancellationToken cancellationToken = default)
    {
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        try
        {
            await EnsureTenantMembershipAsync(connection, transaction, command.UserId, command.TenantId, cancellationToken);
            await ReplaceUserRolesAsync(connection, transaction, command.UserId, command.TenantId, command.Roles, cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    private static async Task EnsureTenantMembershipAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid userId,
        Guid tenantId,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            INSERT IGNORE INTO user_tenant_memberships (user_id, tenant_id, is_default, created_utc)
            VALUES (@userId, @tenantId, 1, @createdUtc);
            """;
        command.Parameters.AddWithValue("@userId", userId.ToString());
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task ReplaceUserCompaniesAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid userId,
        Guid tenantId,
        IReadOnlyCollection<Guid> companyIds,
        CancellationToken cancellationToken)
    {
        await using (var deleteCommand = connection.CreateCommand())
        {
            deleteCommand.Transaction = transaction;
            deleteCommand.CommandText =
                """
                DELETE FROM user_company_memberships
                WHERE user_id = @userId AND tenant_id = @tenantId;
                """;
            deleteCommand.Parameters.AddWithValue("@userId", userId.ToString());
            deleteCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            await deleteCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        foreach (var companyId in companyIds.Distinct())
        {
            await using var insertCommand = connection.CreateCommand();
            insertCommand.Transaction = transaction;
            insertCommand.CommandText =
                """
                INSERT INTO user_company_memberships (user_id, tenant_id, company_id, created_utc)
                VALUES (@userId, @tenantId, @companyId, @createdUtc);
                """;
            insertCommand.Parameters.AddWithValue("@userId", userId.ToString());
            insertCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            insertCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
            insertCommand.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
            await insertCommand.ExecuteNonQueryAsync(cancellationToken);
        }
    }

    private static async Task ReplaceUserRolesAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid userId,
        Guid tenantId,
        IReadOnlyCollection<string> roles,
        CancellationToken cancellationToken)
    {
        await using (var deleteCommand = connection.CreateCommand())
        {
            deleteCommand.Transaction = transaction;
            deleteCommand.CommandText =
                """
                DELETE FROM user_role_assignments
                WHERE user_id = @userId
                  AND tenant_id = @tenantId;
                """;
            deleteCommand.Parameters.AddWithValue("@userId", userId.ToString());
            deleteCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            await deleteCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        foreach (var role in roles
                     .Where(role => PlatformRoles.All.Contains(role, StringComparer.OrdinalIgnoreCase))
                     .Distinct(StringComparer.OrdinalIgnoreCase))
        {
            await using var insertCommand = connection.CreateCommand();
            insertCommand.Transaction = transaction;
            insertCommand.CommandText =
                """
                INSERT INTO user_role_assignments (user_id, tenant_id, role_name, created_utc)
                VALUES (@userId, @tenantId, @roleName, @createdUtc);
                """;
            insertCommand.Parameters.AddWithValue("@userId", userId.ToString());
            insertCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            insertCommand.Parameters.AddWithValue("@roleName", role);
            insertCommand.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
            await insertCommand.ExecuteNonQueryAsync(cancellationToken);
        }
    }

    private static async Task<IReadOnlyCollection<string>> GetUserRolesAsync(
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

    private static async Task<IReadOnlyCollection<Guid>> GetUserCompanyIdsAsync(
        MySqlConnection connection,
        Guid userId,
        Guid? tenantId,
        CancellationToken cancellationToken)
    {
        if (!tenantId.HasValue)
        {
            return [];
        }

        var companyIds = new List<Guid>();

        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT company_id
            FROM user_company_memberships
            WHERE user_id = @userId
              AND tenant_id = @tenantId;
            """;
        command.Parameters.AddWithValue("@userId", userId.ToString());
        command.Parameters.AddWithValue("@tenantId", tenantId.Value.ToString());

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            companyIds.Add(reader.GetGuid("company_id"));
        }

        return companyIds;
    }
}
