using Erp.Domain.Common;
using Erp.Infrastructure.MySql.Configuration;
using Erp.Infrastructure.MySql.Support;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Database;

public sealed class SchemaBootstrapper : IHostedService
{
    private readonly MySqlConnectionFactory _connectionFactory;
    private readonly IOptions<ErpDatabaseOptions> _databaseOptions;
    private readonly IOptions<BootstrapSeedOptions> _seedOptions;
    private readonly ILogger<SchemaBootstrapper> _logger;

    public SchemaBootstrapper(
        MySqlConnectionFactory connectionFactory,
        IOptions<ErpDatabaseOptions> databaseOptions,
        IOptions<BootstrapSeedOptions> seedOptions,
        ILogger<SchemaBootstrapper> logger)
    {
        _connectionFactory = connectionFactory;
        _databaseOptions = databaseOptions;
        _seedOptions = seedOptions;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var settings = _databaseOptions.Value;
        if (!settings.IsConfigured || !settings.BootstrapOnStartup)
        {
            _logger.LogInformation("MySQL bootstrap skipped. Configured: {Configured}, BootstrapOnStartup: {BootstrapOnStartup}",
                settings.IsConfigured,
                settings.BootstrapOnStartup);
            return;
        }

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await EnsureSchemaAsync(connection, cancellationToken);
        await SeedDefaultsAsync(connection, cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private static async Task EnsureSchemaAsync(MySqlConnection connection, CancellationToken cancellationToken)
    {
        var statements = new[]
        {
            """
            CREATE TABLE IF NOT EXISTS tenants (
                id CHAR(36) NOT NULL PRIMARY KEY,
                name VARCHAR(200) NOT NULL,
                slug VARCHAR(120) NOT NULL,
                is_active TINYINT(1) NOT NULL DEFAULT 1,
                created_utc DATETIME(6) NOT NULL,
                UNIQUE KEY uq_tenants_slug (slug)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS companies (
                id CHAR(36) NOT NULL PRIMARY KEY,
                tenant_id CHAR(36) NOT NULL,
                name VARCHAR(200) NOT NULL,
                slug VARCHAR(120) NOT NULL,
                legacy_center_code CHAR(1) NOT NULL,
                is_active TINYINT(1) NOT NULL DEFAULT 1,
                created_utc DATETIME(6) NOT NULL,
                UNIQUE KEY uq_companies_tenant_slug (tenant_id, slug),
                UNIQUE KEY uq_companies_tenant_legacy_center (tenant_id, legacy_center_code)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS app_users (
                id CHAR(36) NOT NULL PRIMARY KEY,
                email VARCHAR(256) NOT NULL,
                display_name VARCHAR(200) NOT NULL,
                password_hash VARCHAR(512) NOT NULL,
                is_active TINYINT(1) NOT NULL DEFAULT 1,
                created_utc DATETIME(6) NOT NULL,
                UNIQUE KEY uq_app_users_email (email)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS user_tenant_memberships (
                user_id CHAR(36) NOT NULL,
                tenant_id CHAR(36) NOT NULL,
                is_default TINYINT(1) NOT NULL DEFAULT 1,
                created_utc DATETIME(6) NOT NULL,
                PRIMARY KEY (user_id, tenant_id)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS user_company_memberships (
                user_id CHAR(36) NOT NULL,
                tenant_id CHAR(36) NOT NULL,
                company_id CHAR(36) NOT NULL,
                created_utc DATETIME(6) NOT NULL,
                PRIMARY KEY (user_id, company_id)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS user_role_assignments (
                user_id CHAR(36) NOT NULL,
                tenant_id CHAR(36) NULL,
                role_name VARCHAR(100) NOT NULL,
                created_utc DATETIME(6) NOT NULL,
                UNIQUE KEY uq_user_role_assignment (user_id, tenant_id, role_name)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS audit_logs (
                id CHAR(36) NOT NULL PRIMARY KEY,
                tenant_id CHAR(36) NULL,
                company_id CHAR(36) NULL,
                user_id CHAR(36) NULL,
                action VARCHAR(100) NOT NULL,
                entity_name VARCHAR(100) NOT NULL,
                entity_id VARCHAR(120) NULL,
                details TEXT NULL,
                created_utc DATETIME(6) NOT NULL
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS plan_definitions (
                id CHAR(36) NOT NULL PRIMARY KEY,
                slug VARCHAR(50) NOT NULL,
                name VARCHAR(120) NOT NULL,
                max_users INT NOT NULL,
                monthly_price DECIMAL(10,2) NOT NULL,
                description VARCHAR(255) NOT NULL,
                is_active TINYINT(1) NOT NULL DEFAULT 1,
                created_utc DATETIME(6) NOT NULL,
                UNIQUE KEY uq_plan_definitions_slug (slug)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS lead_requests (
                id CHAR(36) NOT NULL PRIMARY KEY,
                contact_name VARCHAR(200) NOT NULL,
                company_name VARCHAR(200) NOT NULL,
                email VARCHAR(256) NOT NULL,
                phone VARCHAR(50) NULL,
                requested_users INT NOT NULL,
                message TEXT NULL,
                status VARCHAR(50) NOT NULL,
                created_utc DATETIME(6) NOT NULL
            );
            """
        };

        foreach (var statement in statements)
        {
            await using var command = connection.CreateCommand();
            command.CommandText = statement;
            await command.ExecuteNonQueryAsync(cancellationToken);
        }
    }

    private async Task SeedDefaultsAsync(MySqlConnection connection, CancellationToken cancellationToken)
    {
        await EnsurePlanAsync(connection, "starter", "Starter", 5, 149m, "Ideal para equipos que arrancan con CRM, ventas y trazabilidad básica.", cancellationToken);
        await EnsurePlanAsync(connection, "growth", "Growth", 20, 399m, "Pensado para grupos con varias empresas y operativa diaria compartida.", cancellationToken);
        await EnsurePlanAsync(connection, "scale", "Scale", 100, 990m, "Para operaciones complejas con usuarios, procesos y reporting ampliados.", cancellationToken);

        if (!_seedOptions.Value.HasPlatformAdminSeed)
        {
            return;
        }

        var userId = await EnsurePlatformAdminAsync(connection, cancellationToken);

        if (_seedOptions.Value.HasInitialCompanySeed)
        {
            await EnsureInitialTenantAndCompanyAsync(connection, userId, cancellationToken);
        }
    }

    private static async Task EnsurePlanAsync(
        MySqlConnection connection,
        string slug,
        string name,
        int maxUsers,
        decimal monthlyPrice,
        string description,
        CancellationToken cancellationToken)
    {
        await using var existsCommand = connection.CreateCommand();
        existsCommand.CommandText = "SELECT COUNT(*) FROM plan_definitions WHERE slug = @slug;";
        existsCommand.Parameters.AddWithValue("@slug", slug);
        var exists = Convert.ToInt32(await existsCommand.ExecuteScalarAsync(cancellationToken)) > 0;
        if (exists)
        {
            return;
        }

        await using var insertCommand = connection.CreateCommand();
        insertCommand.CommandText =
            """
            INSERT INTO plan_definitions (id, slug, name, max_users, monthly_price, description, is_active, created_utc)
            VALUES (@id, @slug, @name, @maxUsers, @monthlyPrice, @description, 1, @createdUtc);
            """;
        insertCommand.Parameters.AddWithValue("@id", Guid.NewGuid().ToString());
        insertCommand.Parameters.AddWithValue("@slug", slug);
        insertCommand.Parameters.AddWithValue("@name", name);
        insertCommand.Parameters.AddWithValue("@maxUsers", maxUsers);
        insertCommand.Parameters.AddWithValue("@monthlyPrice", monthlyPrice);
        insertCommand.Parameters.AddWithValue("@description", description);
        insertCommand.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
        await insertCommand.ExecuteNonQueryAsync(cancellationToken);
    }

    private async Task<Guid> EnsurePlatformAdminAsync(MySqlConnection connection, CancellationToken cancellationToken)
    {
        var seed = _seedOptions.Value;

        await using var existsCommand = connection.CreateCommand();
        existsCommand.CommandText = "SELECT id FROM app_users WHERE email = @email LIMIT 1;";
        existsCommand.Parameters.AddWithValue("@email", seed.PlatformAdminEmail.Trim().ToLowerInvariant());
        var existingId = await existsCommand.ExecuteScalarAsync(cancellationToken);

        var userId = existingId is null
            ? Guid.NewGuid()
            : Guid.Parse(Convert.ToString(existingId)!);

        if (existingId is null)
        {
            await using var insertUserCommand = connection.CreateCommand();
            insertUserCommand.CommandText =
                """
                INSERT INTO app_users (id, email, display_name, password_hash, is_active, created_utc)
                VALUES (@id, @email, @displayName, @passwordHash, 1, @createdUtc);
                """;
            insertUserCommand.Parameters.AddWithValue("@id", userId.ToString());
            insertUserCommand.Parameters.AddWithValue("@email", seed.PlatformAdminEmail.Trim().ToLowerInvariant());
            insertUserCommand.Parameters.AddWithValue("@displayName", seed.PlatformAdminDisplayName.Trim());
            insertUserCommand.Parameters.AddWithValue("@passwordHash", PasswordHasher.Hash(seed.PlatformAdminPassword));
            insertUserCommand.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
            await insertUserCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        await using var roleCommand = connection.CreateCommand();
        roleCommand.CommandText =
            """
            INSERT IGNORE INTO user_role_assignments (user_id, tenant_id, role_name, created_utc)
            VALUES (@userId, NULL, @roleName, @createdUtc);
            """;
        roleCommand.Parameters.AddWithValue("@userId", userId.ToString());
        roleCommand.Parameters.AddWithValue("@roleName", PlatformRoles.PlatformAdmin);
        roleCommand.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
        await roleCommand.ExecuteNonQueryAsync(cancellationToken);

        return userId;
    }

    private async Task EnsureInitialTenantAndCompanyAsync(MySqlConnection connection, Guid userId, CancellationToken cancellationToken)
    {
        var seed = _seedOptions.Value;
        var tenantId = await EnsureTenantAsync(
            connection,
            seed.InitialTenantName.Trim(),
            SlugGenerator.Generate(seed.InitialTenantSlug),
            cancellationToken);

        var companyId = await EnsureCompanyAsync(
            connection,
            tenantId,
            seed.InitialCompanyName.Trim(),
            SlugGenerator.Generate(seed.InitialCompanySlug),
            seed.InitialCompanyLegacyCenterCode.Trim().ToUpperInvariant(),
            cancellationToken);

        await EnsureUserTenantMembershipAsync(connection, userId, tenantId, cancellationToken);
        await EnsureUserCompanyMembershipAsync(connection, userId, tenantId, companyId, cancellationToken);
        await EnsureTenantRoleAsync(connection, userId, tenantId, PlatformRoles.TenantAdmin, cancellationToken);
    }

    private static async Task<Guid> EnsureTenantAsync(
        MySqlConnection connection,
        string name,
        string slug,
        CancellationToken cancellationToken)
    {
        await using var existsCommand = connection.CreateCommand();
        existsCommand.CommandText = "SELECT id FROM tenants WHERE slug = @slug LIMIT 1;";
        existsCommand.Parameters.AddWithValue("@slug", slug);
        var existingId = await existsCommand.ExecuteScalarAsync(cancellationToken);
        if (existingId is not null)
        {
            return Guid.Parse(Convert.ToString(existingId)!);
        }

        var tenantId = Guid.NewGuid();
        await using var insertCommand = connection.CreateCommand();
        insertCommand.CommandText =
            """
            INSERT INTO tenants (id, name, slug, is_active, created_utc)
            VALUES (@id, @name, @slug, 1, @createdUtc);
            """;
        insertCommand.Parameters.AddWithValue("@id", tenantId.ToString());
        insertCommand.Parameters.AddWithValue("@name", name);
        insertCommand.Parameters.AddWithValue("@slug", slug);
        insertCommand.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
        await insertCommand.ExecuteNonQueryAsync(cancellationToken);

        return tenantId;
    }

    private static async Task<Guid> EnsureCompanyAsync(
        MySqlConnection connection,
        Guid tenantId,
        string name,
        string slug,
        string legacyCenterCode,
        CancellationToken cancellationToken)
    {
        await using var existsCommand = connection.CreateCommand();
        existsCommand.CommandText =
            """
            SELECT id
            FROM companies
            WHERE tenant_id = @tenantId
              AND slug = @slug
            LIMIT 1;
            """;
        existsCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        existsCommand.Parameters.AddWithValue("@slug", slug);
        var existingId = await existsCommand.ExecuteScalarAsync(cancellationToken);
        if (existingId is not null)
        {
            return Guid.Parse(Convert.ToString(existingId)!);
        }

        var companyId = Guid.NewGuid();
        await using var insertCommand = connection.CreateCommand();
        insertCommand.CommandText =
            """
            INSERT INTO companies (id, tenant_id, name, slug, legacy_center_code, is_active, created_utc)
            VALUES (@id, @tenantId, @name, @slug, @legacyCenterCode, 1, @createdUtc);
            """;
        insertCommand.Parameters.AddWithValue("@id", companyId.ToString());
        insertCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        insertCommand.Parameters.AddWithValue("@name", name);
        insertCommand.Parameters.AddWithValue("@slug", slug);
        insertCommand.Parameters.AddWithValue("@legacyCenterCode", legacyCenterCode);
        insertCommand.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
        await insertCommand.ExecuteNonQueryAsync(cancellationToken);

        return companyId;
    }

    private static async Task EnsureUserTenantMembershipAsync(
        MySqlConnection connection,
        Guid userId,
        Guid tenantId,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
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

    private static async Task EnsureUserCompanyMembershipAsync(
        MySqlConnection connection,
        Guid userId,
        Guid tenantId,
        Guid companyId,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            INSERT IGNORE INTO user_company_memberships (user_id, tenant_id, company_id, created_utc)
            VALUES (@userId, @tenantId, @companyId, @createdUtc);
            """;
        command.Parameters.AddWithValue("@userId", userId.ToString());
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task EnsureTenantRoleAsync(
        MySqlConnection connection,
        Guid userId,
        Guid tenantId,
        string roleName,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            INSERT IGNORE INTO user_role_assignments (user_id, tenant_id, role_name, created_utc)
            VALUES (@userId, @tenantId, @roleName, @createdUtc);
            """;
        command.Parameters.AddWithValue("@userId", userId.ToString());
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@roleName", roleName);
        command.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }
}
