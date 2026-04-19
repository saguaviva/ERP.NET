using Erp.Application.Tenants;
using Erp.Application.Contexts;
using Erp.Domain.Common;
using Erp.Application.Auditing;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Support;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Tenants;

public sealed class MySqlTenantAdminService : ITenantAdminService
{
    private readonly MySqlConnectionFactory _connectionFactory;
    private readonly IAuditLogService _auditLogService;
    private readonly ICurrentUserContext _currentUserContext;
    private readonly ITenantContext _tenantContext;
    private readonly IActiveCompanyContext _activeCompanyContext;

    public MySqlTenantAdminService(
        MySqlConnectionFactory connectionFactory,
        IAuditLogService auditLogService,
        ICurrentUserContext currentUserContext,
        ITenantContext tenantContext,
        IActiveCompanyContext activeCompanyContext)
    {
        _connectionFactory = connectionFactory;
        _auditLogService = auditLogService;
        _currentUserContext = currentUserContext;
        _tenantContext = tenantContext;
        _activeCompanyContext = activeCompanyContext;
    }

    public async Task<PlatformSetupStatusDto> GetSetupStatusAsync(CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return new PlatformSetupStatusDto
            {
                IsDatabaseConfigured = false
            };
        }

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);

        var userCount = await CountAsync(connection, "SELECT COUNT(*) FROM app_users;", cancellationToken);
        var tenantCount = await CountAsync(connection, "SELECT COUNT(*) FROM tenants;", cancellationToken);
        var companyCount = await CountAsync(connection, "SELECT COUNT(*) FROM companies;", cancellationToken);

        await using var platformAdminCommand = connection.CreateCommand();
        platformAdminCommand.CommandText =
            """
            SELECT COUNT(*)
            FROM user_role_assignments
            WHERE tenant_id IS NULL
              AND role_name = @roleName;
            """;
        platformAdminCommand.Parameters.AddWithValue("@roleName", PlatformRoles.PlatformAdmin);
        var platformAdminCount = Convert.ToInt32(await platformAdminCommand.ExecuteScalarAsync(cancellationToken));

        var hasUsers = userCount > 0;
        var hasTenants = tenantCount > 0;
        var hasCompanies = companyCount > 0;
        var hasPlatformAdmin = platformAdminCount > 0;

        return new PlatformSetupStatusDto
        {
            IsDatabaseConfigured = true,
            HasUsers = hasUsers,
            HasTenants = hasTenants,
            HasCompanies = hasCompanies,
            HasPlatformAdmin = hasPlatformAdmin,
            CanRunInitialSetup = !hasUsers && !hasTenants && !hasCompanies
        };
    }

    public async Task<IReadOnlyCollection<TenantSummaryDto>> GetTenantsAsync(CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return [];
        }

        var effectiveTenantId = EnsureTenantReadAccess();
        var items = new List<TenantSummaryDto>();

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT id, name, slug, is_active
            FROM tenants
            WHERE @tenantId IS NULL OR id = @tenantId
            ORDER BY name;
            """;
        command.Parameters.AddWithValue("@tenantId", effectiveTenantId?.ToString());

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new TenantSummaryDto
            {
                Id = reader.GetGuid("id"),
                Name = reader.GetStringOrEmpty("name"),
                Slug = reader.GetStringOrEmpty("slug"),
                IsActive = reader.GetBooleanValue("is_active")
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

        var effectiveTenantId = EnsureTenantReadAccess(tenantId);
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
        command.Parameters.AddWithValue("@tenantId", effectiveTenantId?.ToString());

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
                IsActive = reader.GetBooleanValue("is_active")
            });
        }

        return items;
    }

    public async Task<IReadOnlyCollection<LegacyCenterOptionDto>> GetLegacyCentersAsync(Guid? tenantId = null, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return [];
        }

        var effectiveTenantId = EnsureTenantReadAccess(tenantId);
        var items = new List<LegacyCenterOptionDto>();

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT
                CAST(f.CODI AS CHAR) AS code,
                COALESCE(CAST(f.DESCRI AS CHAR), '') AS description,
                CASE WHEN c.id IS NULL THEN 0 ELSE 1 END AS is_assigned,
                COALESCE(c.name, '') AS assigned_company_name
            FROM filiales f
            LEFT JOIN companies c
              ON @tenantId IS NOT NULL
             AND c.tenant_id = @tenantId
             AND c.legacy_center_code = f.CODI
            ORDER BY f.DESCRI, f.CODI;
            """;
        command.Parameters.AddWithValue("@tenantId", effectiveTenantId?.ToString());

        try
        {
            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                items.Add(new LegacyCenterOptionDto
                {
                    Code = reader.GetStringOrEmpty("code"),
                    Description = reader.GetStringOrEmpty("description"),
                    IsAssigned = reader.GetBooleanValue("is_assigned"),
                    AssignedCompanyName = reader.GetStringOrEmpty("assigned_company_name")
                });
            }
        }
        catch (MySqlException)
        {
            return [];
        }

        return items;
    }

    public async Task<IReadOnlyCollection<TenantModuleSettingDto>> GetModuleSettingsAsync(Guid? tenantId = null, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return [];
        }

        var effectiveTenantId = EnsureTenantReadAccess(tenantId);
        if (!effectiveTenantId.HasValue)
        {
            return [];
        }

        var items = new List<TenantModuleSettingDto>();
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT tenant_id, module_key, data_scope
            FROM tenant_module_settings
            WHERE tenant_id = @tenantId
            ORDER BY module_key;
            """;
        command.Parameters.AddWithValue("@tenantId", effectiveTenantId.Value.ToString());

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new TenantModuleSettingDto
            {
                TenantId = reader.GetGuid("tenant_id"),
                ModuleKey = reader.GetStringOrEmpty("module_key"),
                DataScope = reader.GetStringOrEmpty("data_scope")
            });
        }

        return items;
    }

    public async Task<CompanyLegacyCenterImpactDto?> GetCompanyLegacyCenterImpactAsync(
        Guid companyId,
        Guid tenantId,
        CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return null;
        }

        EnsureTenantScopedWriteAccess(tenantId);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var company = await GetCompanyByIdAsync(connection, companyId, cancellationToken);
        if (company is null || company.TenantId != tenantId)
        {
            return null;
        }

        return await GetCompanyLegacyCenterImpactCoreAsync(connection, company, cancellationToken);
    }

    public async Task<IReadOnlyCollection<UserSummaryDto>> GetUsersAsync(Guid? tenantId = null, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return [];
        }

        var effectiveTenantId = EnsureTenantReadAccess(tenantId);
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
        command.Parameters.AddWithValue("@tenantId", effectiveTenantId?.ToString());

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            users.Add(new UserSummaryDto
            {
                Id = reader.GetGuid("id"),
                TenantId = reader.GetNullableGuid("tenant_id"),
                Email = reader.GetStringOrEmpty("email"),
                DisplayName = reader.GetStringOrEmpty("display_name"),
                IsActive = reader.GetBooleanValue("is_active")
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

    public async Task InitializePlatformAsync(InitializePlatformCommand command, CancellationToken cancellationToken = default)
    {
        var tenantName = command.TenantName.Trim();
        var adminDisplayName = command.AdminDisplayName.Trim();
        var adminEmail = command.AdminEmail.Trim().ToLowerInvariant();
        var adminPassword = command.AdminPassword.Trim();
        var tenantSlug = SlugGenerator.Generate(string.IsNullOrWhiteSpace(command.TenantSlug) ? tenantName : command.TenantSlug);
        var initialCompanies = NormalizeInitialCompanies(
            command.InitialCompanies,
            command.CompanyName,
            command.CompanySlug,
            command.LegacyCenterCode);

        if (string.IsNullOrWhiteSpace(tenantName))
        {
            throw new InvalidOperationException("El nombre del tenant es obligatorio.");
        }

        if (string.IsNullOrWhiteSpace(adminDisplayName))
        {
            throw new InvalidOperationException("El nombre del administrador es obligatorio.");
        }

        if (string.IsNullOrWhiteSpace(adminEmail))
        {
            throw new InvalidOperationException("El email del administrador es obligatorio.");
        }

        if (string.IsNullOrWhiteSpace(adminPassword) || adminPassword.Length < 8)
        {
            throw new InvalidOperationException("La contraseña del administrador debe tener al menos 8 caracteres.");
        }

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var setupStatus = await GetSetupStatusAsync(cancellationToken);
        if (!setupStatus.CanRunInitialSetup)
        {
            throw new InvalidOperationException("La instalación inicial ya está creada. Usa la pantalla de plataforma para seguir administrando.");
        }

        var tenantId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var companies = initialCompanies
            .Select(company => new CompanySummaryDto
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                Name = company.Name,
                Slug = company.Slug,
                LegacyCenterCode = company.LegacyCenterCode,
                IsActive = true
            })
            .ToArray();

        await EnsureTenantSlugAvailableAsync(connection, tenantSlug, cancellationToken);
        foreach (var company in companies)
        {
            await EnsureCompanySlugAvailableAsync(connection, tenantId, company.Slug, cancellationToken);
            await EnsureLegacyCenterAvailableAsync(connection, tenantId, company.LegacyCenterCode, cancellationToken);
        }

        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        try
        {
            await using (var tenantCommand = connection.CreateCommand())
            {
                tenantCommand.Transaction = transaction;
                tenantCommand.CommandText =
                    """
                    INSERT INTO tenants (id, name, slug, is_active, created_utc)
                    VALUES (@id, @name, @slug, 1, @createdUtc);
                    """;
                tenantCommand.Parameters.AddWithValue("@id", tenantId.ToString());
                tenantCommand.Parameters.AddWithValue("@name", tenantName);
                tenantCommand.Parameters.AddWithValue("@slug", tenantSlug);
                tenantCommand.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
                await tenantCommand.ExecuteNonQueryAsync(cancellationToken);
            }

            foreach (var company in companies)
            {
                await InsertCompanyAsync(connection, transaction, company, cancellationToken);
            }

            await using (var userCommand = connection.CreateCommand())
            {
                userCommand.Transaction = transaction;
                userCommand.CommandText =
                    """
                    INSERT INTO app_users (id, email, display_name, password_hash, require_password_change, is_active, created_utc)
                    VALUES (@id, @email, @displayName, @passwordHash, 0, 1, @createdUtc);
                    """;
                userCommand.Parameters.AddWithValue("@id", userId.ToString());
                userCommand.Parameters.AddWithValue("@email", adminEmail);
                userCommand.Parameters.AddWithValue("@displayName", adminDisplayName);
                userCommand.Parameters.AddWithValue("@passwordHash", PasswordHasher.Hash(adminPassword));
                userCommand.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
                await userCommand.ExecuteNonQueryAsync(cancellationToken);
            }

            await EnsureTenantMembershipAsync(connection, transaction, userId, tenantId, cancellationToken);
            await ReplaceUserCompaniesAsync(connection, transaction, userId, tenantId, companies.Select(company => company.Id).ToArray(), cancellationToken);
            await ReplaceUserRolesAsync(connection, transaction, userId, tenantId, [PlatformRoles.TenantAdmin], cancellationToken);
            await AddPlatformAdminRoleAsync(connection, transaction, userId, cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = tenantId,
            CompanyId = companies.First().Id,
            UserId = userId,
            Action = "PlatformInitialized",
            EntityName = "Setup",
            EntityId = tenantId.ToString(),
            Details = $"Tenant={tenantName}; Companies={FormatCompanyAuditSummary(companies)}; Admin={adminEmail}"
        }, cancellationToken);
    }

    public async Task<TenantSummaryDto> CreateTenantAsync(CreateTenantCommand command, CancellationToken cancellationToken = default)
    {
        EnsurePlatformWriteAccess();

        if (string.IsNullOrWhiteSpace(command.Name))
        {
            throw new InvalidOperationException("El nombre del tenant es obligatorio.");
        }

        var tenant = new TenantSummaryDto
        {
            Id = Guid.NewGuid(),
            Name = command.Name.Trim(),
            Slug = SlugGenerator.Generate(string.IsNullOrWhiteSpace(command.Slug) ? command.Name : command.Slug),
            IsActive = true
        };
        var initialCompanies = NormalizeInitialCompanies(command.InitialCompanies);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);

        await EnsureTenantSlugAvailableAsync(connection, tenant.Slug, cancellationToken);
        foreach (var company in initialCompanies)
        {
            await EnsureCompanySlugAvailableAsync(connection, tenant.Id, company.Slug, cancellationToken);
            await EnsureLegacyCenterAvailableAsync(connection, tenant.Id, company.LegacyCenterCode, cancellationToken);
        }

        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        try
        {
            await using var insertCommand = connection.CreateCommand();
            insertCommand.Transaction = transaction;
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

            foreach (var company in initialCompanies.Select(company => new CompanySummaryDto
                         {
                             Id = Guid.NewGuid(),
                             TenantId = tenant.Id,
                             Name = company.Name,
                             Slug = company.Slug,
                             LegacyCenterCode = company.LegacyCenterCode,
                             IsActive = true
                         }))
            {
                await InsertCompanyAsync(connection, transaction, company, cancellationToken);
            }

            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = tenant.Id,
            Action = "TenantCreated",
            EntityName = "Tenant",
            EntityId = tenant.Id.ToString(),
            Details = initialCompanies.Count == 0
                ? $"Nombre={tenant.Name}; Slug={tenant.Slug}"
                : $"Nombre={tenant.Name}; Slug={tenant.Slug}; Companies={string.Join(", ", initialCompanies.Select(company => $"{company.Name}({company.LegacyCenterCode})"))}"
        }, cancellationToken);

        return tenant;
    }

    public async Task<CompanySummaryDto> CreateCompanyAsync(CreateCompanyCommand command, CancellationToken cancellationToken = default)
    {
        EnsureTenantScopedWriteAccess(command.TenantId);

        if (command.TenantId == Guid.Empty)
        {
            throw new InvalidOperationException("Debes seleccionar un tenant.");
        }

        if (string.IsNullOrWhiteSpace(command.Name))
        {
            throw new InvalidOperationException("El nombre de la company es obligatorio.");
        }

        var legacyCenterCode = command.LegacyCenterCode.Trim().ToUpperInvariant();
        if (string.IsNullOrWhiteSpace(legacyCenterCode))
        {
            throw new InvalidOperationException("El centro legacy es obligatorio.");
        }

        if (legacyCenterCode.Length != 1)
        {
            throw new InvalidOperationException("El centro legacy debe tener exactamente 1 caracter.");
        }

        var company = new CompanySummaryDto
        {
            Id = Guid.NewGuid(),
            TenantId = command.TenantId,
            Name = command.Name.Trim(),
            Slug = SlugGenerator.Generate(string.IsNullOrWhiteSpace(command.Slug) ? command.Name : command.Slug),
            LegacyCenterCode = legacyCenterCode,
            IsActive = true
        };

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);

        await EnsureCompanySlugAvailableAsync(connection, company.TenantId, company.Slug, cancellationToken);
        await EnsureLegacyCenterAvailableAsync(connection, company.TenantId, company.LegacyCenterCode, cancellationToken);

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

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = company.TenantId,
            CompanyId = company.Id,
            Action = "CompanyCreated",
            EntityName = "Company",
            EntityId = company.Id.ToString(),
            Details = $"Nombre={company.Name}; Slug={company.Slug}; CentroLegacy={company.LegacyCenterCode}"
        }, cancellationToken);

        return company;
    }

    public async Task SetModuleDataScopeAsync(SetTenantModuleDataScopeCommand command, CancellationToken cancellationToken = default)
    {
        EnsureTenantScopedWriteAccess(command.TenantId);

        var moduleKey = command.ModuleKey.Trim();
        var dataScope = string.IsNullOrWhiteSpace(command.DataScope)
            ? TenantModuleDataScopes.Company
            : command.DataScope.Trim();

        if (!TenantModuleKeys.All.Contains(moduleKey, StringComparer.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("El módulo indicado no es válido.");
        }

        if (!TenantModuleDataScopes.All.Contains(dataScope, StringComparer.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("El alcance de datos indicado no es válido.");
        }

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var dbCommand = connection.CreateCommand();
        dbCommand.CommandText =
            """
            INSERT INTO tenant_module_settings (tenant_id, module_key, data_scope, updated_by_user_id, updated_utc)
            VALUES (@tenantId, @moduleKey, @dataScope, @updatedByUserId, @updatedUtc)
            ON DUPLICATE KEY UPDATE
                data_scope = VALUES(data_scope),
                updated_by_user_id = VALUES(updated_by_user_id),
                updated_utc = VALUES(updated_utc);
            """;
        dbCommand.Parameters.AddWithValue("@tenantId", command.TenantId.ToString());
        dbCommand.Parameters.AddWithValue("@moduleKey", moduleKey);
        dbCommand.Parameters.AddWithValue("@dataScope", dataScope);
        dbCommand.Parameters.AddWithValue("@updatedByUserId", _currentUserContext.UserId?.ToString());
        dbCommand.Parameters.AddWithValue("@updatedUtc", DateTime.UtcNow);
        await dbCommand.ExecuteNonQueryAsync(cancellationToken);

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = command.TenantId,
            CompanyId = _activeCompanyContext.CompanyId,
            UserId = _currentUserContext.UserId,
            Action = "TenantModuleDataScopeUpdated",
            EntityName = "TenantModule",
            EntityId = moduleKey,
            Details = $"DataScope={dataScope}"
        }, cancellationToken);
    }

    public async Task<CompanySummaryDto> UpdateCompanyLegacyCenterAsync(
        UpdateCompanyLegacyCenterCommand command,
        CancellationToken cancellationToken = default)
    {
        if (command.CompanyId == Guid.Empty)
        {
            throw new InvalidOperationException("Debes seleccionar una company.");
        }

        if (command.TenantId == Guid.Empty)
        {
            throw new InvalidOperationException("Debes indicar el tenant de la company.");
        }

        EnsureTenantScopedWriteAccess(command.TenantId);

        var legacyCenterCode = command.LegacyCenterCode.Trim().ToUpperInvariant();
        if (string.IsNullOrWhiteSpace(legacyCenterCode))
        {
            throw new InvalidOperationException("El centro legacy es obligatorio.");
        }

        if (legacyCenterCode.Length != 1)
        {
            throw new InvalidOperationException("El centro legacy debe tener exactamente 1 caracter.");
        }

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var company = await GetCompanyByIdAsync(connection, command.CompanyId, cancellationToken);
        if (company is null || company.TenantId != command.TenantId)
        {
            throw new InvalidOperationException("No se ha encontrado la company indicada.");
        }

        if (string.Equals(company.LegacyCenterCode, legacyCenterCode, StringComparison.OrdinalIgnoreCase))
        {
            return company;
        }

        var impact = await GetCompanyLegacyCenterImpactCoreAsync(connection, company, cancellationToken);
        if (impact.HasUsage && !command.ForceChange)
        {
            throw new InvalidOperationException("Esta company ya tiene usuarios o datos operativos asociados. Revisa el impacto y confirma el cambio antes de guardar.");
        }

        await EnsureLegacyCenterAvailableAsync(connection, command.TenantId, legacyCenterCode, cancellationToken);

        await using var updateCommand = connection.CreateCommand();
        updateCommand.CommandText =
            """
            UPDATE companies
            SET legacy_center_code = @legacyCenterCode
            WHERE id = @companyId
              AND tenant_id = @tenantId;
            """;
        updateCommand.Parameters.AddWithValue("@legacyCenterCode", legacyCenterCode);
        updateCommand.Parameters.AddWithValue("@companyId", command.CompanyId.ToString());
        updateCommand.Parameters.AddWithValue("@tenantId", command.TenantId.ToString());
        var affectedRows = await updateCommand.ExecuteNonQueryAsync(cancellationToken);

        if (affectedRows == 0)
        {
            throw new InvalidOperationException("No se ha podido actualizar el centro legacy de la company.");
        }

        var updatedCompany = new CompanySummaryDto
        {
            Id = company.Id,
            TenantId = company.TenantId,
            Name = company.Name,
            Slug = company.Slug,
            LegacyCenterCode = legacyCenterCode,
            IsActive = company.IsActive
        };

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = updatedCompany.TenantId,
            CompanyId = updatedCompany.Id,
            Action = "CompanyLegacyCenterUpdated",
            EntityName = "Company",
            EntityId = updatedCompany.Id.ToString(),
            Details = $"Company={updatedCompany.Name}; CentroLegacy: '{company.LegacyCenterCode}' -> '{updatedCompany.LegacyCenterCode}'"
        }, cancellationToken);

        return updatedCompany;
    }

    public async Task<UserSummaryDto> CreateUserAsync(CreateUserCommand command, CancellationToken cancellationToken = default)
    {
        EnsureTenantScopedWriteAccess(command.TenantId);

        if (command.TenantId == Guid.Empty)
        {
            throw new InvalidOperationException("Debes seleccionar un tenant para el usuario.");
        }

        if (string.IsNullOrWhiteSpace(command.DisplayName))
        {
            throw new InvalidOperationException("El nombre del usuario es obligatorio.");
        }

        if (string.IsNullOrWhiteSpace(command.Email))
        {
            throw new InvalidOperationException("El email del usuario es obligatorio.");
        }

        if (string.IsNullOrWhiteSpace(command.Password))
        {
            throw new InvalidOperationException("La contraseña del usuario es obligatoria.");
        }

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
                    INSERT INTO app_users (id, email, display_name, password_hash, require_password_change, is_active, created_utc)
                    VALUES (@id, @email, @displayName, @passwordHash, 1, 1, @createdUtc);
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

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = command.TenantId,
            Action = "UserCreated",
            EntityName = "User",
            EntityId = userId.ToString(),
            Details = $"Email={command.Email.Trim().ToLowerInvariant()}; Roles={string.Join(",", command.Roles)}; Companies={string.Join(",", command.CompanyIds)}"
        }, cancellationToken);

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
        EnsureTenantScopedWriteAccess(command.TenantId);
        EnsureSelfLockoutProtection(command.UserId, command.TenantId, companyIds: command.CompanyIds);

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

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = command.TenantId,
            Action = "UserCompaniesAssigned",
            EntityName = "User",
            EntityId = command.UserId.ToString(),
            Details = $"Companies={string.Join(",", command.CompanyIds)}"
        }, cancellationToken);
    }

    public async Task AssignUserRolesAsync(AssignUserRolesCommand command, CancellationToken cancellationToken = default)
    {
        EnsureTenantScopedWriteAccess(command.TenantId);
        EnsureSelfLockoutProtection(command.UserId, command.TenantId, roles: command.Roles);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await EnsureNotRemovingLastActiveTenantAdminAsync(
            connection,
            command.TenantId,
            command.UserId,
            removingTenantAdminRole: !command.Roles.Contains(PlatformRoles.TenantAdmin, StringComparer.OrdinalIgnoreCase),
            deactivatingUser: false,
            cancellationToken);
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

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = command.TenantId,
            Action = "UserRolesAssigned",
            EntityName = "User",
            EntityId = command.UserId.ToString(),
            Details = $"Roles={string.Join(",", command.Roles)}"
        }, cancellationToken);
    }

    public async Task SetUserActiveAsync(SetUserActiveCommand command, CancellationToken cancellationToken = default)
    {
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var targetTenantId = await GetUserTenantIdAsync(connection, command.UserId, cancellationToken);
        if (!targetTenantId.HasValue)
        {
            throw new InvalidOperationException("No se ha encontrado el tenant del usuario indicado.");
        }

        EnsureTenantScopedWriteAccess(targetTenantId.Value);
        EnsureSelfLockoutProtection(command.UserId, targetTenantId.Value, isActive: command.IsActive);
        await EnsureNotRemovingLastActiveTenantAdminAsync(
            connection,
            targetTenantId.Value,
            command.UserId,
            removingTenantAdminRole: false,
            deactivatingUser: !command.IsActive,
            cancellationToken);

        if (!command.IsActive && await IsPlatformAdminAsync(connection, command.UserId, cancellationToken))
        {
            throw new InvalidOperationException("No se puede desactivar un usuario PlatformAdmin desde esta pantalla.");
        }

        await using var updateCommand = connection.CreateCommand();
        updateCommand.CommandText =
            """
            UPDATE app_users
            SET is_active = @isActive
            WHERE id = @userId;
            """;
        updateCommand.Parameters.AddWithValue("@userId", command.UserId.ToString());
        updateCommand.Parameters.AddWithValue("@isActive", command.IsActive);
        var affectedRows = await updateCommand.ExecuteNonQueryAsync(cancellationToken);

        if (affectedRows == 0)
        {
            throw new InvalidOperationException("No se ha encontrado el usuario indicado.");
        }

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            Action = command.IsActive ? "UserActivated" : "UserDeactivated",
            EntityName = "User",
            EntityId = command.UserId.ToString(),
            Details = $"IsActive={command.IsActive}"
        }, cancellationToken);
    }

    public async Task ResetUserPasswordAsync(ResetUserPasswordCommand command, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(command.NewPassword))
        {
            throw new InvalidOperationException("La nueva contraseña es obligatoria.");
        }

        var password = command.NewPassword.Trim();
        if (password.Length < 8)
        {
            throw new InvalidOperationException("La nueva contraseña debe tener al menos 8 caracteres.");
        }

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var targetTenantId = await GetUserTenantIdAsync(connection, command.UserId, cancellationToken);
        if (!targetTenantId.HasValue)
        {
            throw new InvalidOperationException("No se ha encontrado el tenant del usuario indicado.");
        }

        EnsureTenantScopedWriteAccess(targetTenantId.Value);
        await using var updateCommand = connection.CreateCommand();
        updateCommand.CommandText =
            """
            UPDATE app_users
            SET password_hash = @passwordHash,
                require_password_change = 1
            WHERE id = @userId;
            """;
        updateCommand.Parameters.AddWithValue("@userId", command.UserId.ToString());
        updateCommand.Parameters.AddWithValue("@passwordHash", PasswordHasher.Hash(password));
        var affectedRows = await updateCommand.ExecuteNonQueryAsync(cancellationToken);

        if (affectedRows == 0)
        {
            throw new InvalidOperationException("No se ha encontrado el usuario indicado.");
        }

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            Action = "UserPasswordReset",
            EntityName = "User",
            EntityId = command.UserId.ToString(),
            Details = "Password reset by platform admin."
        }, cancellationToken);
    }

    private void EnsurePlatformWriteAccess()
    {
        if (!_currentUserContext.IsAuthenticated)
        {
            throw new InvalidOperationException("Debes iniciar sesión para realizar esta acción.");
        }

        if (_currentUserContext.IsPlatformAdmin)
        {
            return;
        }

        throw new InvalidOperationException("Solo un administrador de plataforma puede realizar esta acción.");
    }

    private Guid? EnsureTenantReadAccess(Guid? requestedTenantId = null)
    {
        if (!_currentUserContext.IsAuthenticated)
        {
            throw new InvalidOperationException("Debes iniciar sesión para acceder a esta información.");
        }

        if (_currentUserContext.IsPlatformAdmin)
        {
            return requestedTenantId;
        }

        if (!_currentUserContext.Roles.Contains(PlatformRoles.TenantAdmin, StringComparer.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("No tienes permisos para consultar administración de tenants.");
        }

        if (!_tenantContext.TenantId.HasValue)
        {
            throw new InvalidOperationException("Tu sesión no tiene un tenant activo.");
        }

        if (requestedTenantId.HasValue && requestedTenantId.Value != _tenantContext.TenantId.Value)
        {
            throw new InvalidOperationException("No puedes consultar información de otro tenant.");
        }

        return _tenantContext.TenantId.Value;
    }

    private void EnsureTenantScopedWriteAccess(Guid tenantId)
    {
        if (!_currentUserContext.IsAuthenticated)
        {
            throw new InvalidOperationException("Debes iniciar sesión para realizar esta acción.");
        }

        if (_currentUserContext.IsPlatformAdmin)
        {
            return;
        }

        if (!_currentUserContext.Roles.Contains(PlatformRoles.TenantAdmin, StringComparer.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("No tienes permisos para administrar este tenant.");
        }

        if (!_tenantContext.TenantId.HasValue)
        {
            throw new InvalidOperationException("Tu sesión no tiene un tenant activo.");
        }

        if (_tenantContext.TenantId.Value != tenantId)
        {
            throw new InvalidOperationException("No puedes administrar otro tenant distinto al activo.");
        }
    }

    private void EnsureSelfLockoutProtection(
        Guid targetUserId,
        Guid tenantId,
        IReadOnlyCollection<Guid>? companyIds = null,
        IReadOnlyCollection<string>? roles = null,
        bool? isActive = null)
    {
        if (_currentUserContext.IsPlatformAdmin ||
            !_currentUserContext.UserId.HasValue ||
            _currentUserContext.UserId.Value != targetUserId)
        {
            return;
        }

        if (isActive.HasValue && !isActive.Value)
        {
            throw new InvalidOperationException("No puedes desactivarte a ti mismo desde esta pantalla.");
        }

        if (roles is not null &&
            !roles.Contains(PlatformRoles.TenantAdmin, StringComparer.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("No puedes quitarte el rol TenantAdmin desde esta pantalla.");
        }

        if (companyIds is null)
        {
            return;
        }

        var normalizedCompanyIds = companyIds
            .Distinct()
            .ToArray();

        if (normalizedCompanyIds.Length == 0)
        {
            throw new InvalidOperationException("Debes conservar al menos una company asignada.");
        }

        if (_activeCompanyContext.CompanyId.HasValue &&
            !normalizedCompanyIds.Contains(_activeCompanyContext.CompanyId.Value))
        {
            throw new InvalidOperationException("No puedes quitarte el acceso a la company activa desde esta pantalla.");
        }
    }

    private static async Task EnsureNotRemovingLastActiveTenantAdminAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid userId,
        bool removingTenantAdminRole,
        bool deactivatingUser,
        CancellationToken cancellationToken)
    {
        if (!removingTenantAdminRole && !deactivatingUser)
        {
            return;
        }

        if (!await IsUserActiveTenantAdminAsync(connection, tenantId, userId, cancellationToken))
        {
            return;
        }

        var activeTenantAdminCount = await CountActiveTenantAdminsAsync(connection, tenantId, cancellationToken);
        if (activeTenantAdminCount <= 1)
        {
            throw new InvalidOperationException("No puedes dejar el tenant sin ningún TenantAdmin activo.");
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

    private static async Task InsertCompanyAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        CompanySummaryDto company,
        CancellationToken cancellationToken)
    {
        await using var companyCommand = connection.CreateCommand();
        companyCommand.Transaction = transaction;
        companyCommand.CommandText =
            """
            INSERT INTO companies (id, tenant_id, name, slug, legacy_center_code, is_active, created_utc)
            VALUES (@id, @tenantId, @name, @slug, @legacyCenterCode, 1, @createdUtc);
            """;
        companyCommand.Parameters.AddWithValue("@id", company.Id.ToString());
        companyCommand.Parameters.AddWithValue("@tenantId", company.TenantId.ToString());
        companyCommand.Parameters.AddWithValue("@name", company.Name);
        companyCommand.Parameters.AddWithValue("@slug", company.Slug);
        companyCommand.Parameters.AddWithValue("@legacyCenterCode", company.LegacyCenterCode);
        companyCommand.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
        await companyCommand.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task<CompanySummaryDto?> GetCompanyByIdAsync(
        MySqlConnection connection,
        Guid companyId,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT id, tenant_id, name, slug, legacy_center_code, is_active
            FROM companies
            WHERE id = @companyId
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@companyId", companyId.ToString());

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        return new CompanySummaryDto
        {
            Id = reader.GetGuid("id"),
            TenantId = reader.GetGuid("tenant_id"),
            Name = reader.GetStringOrEmpty("name"),
            Slug = reader.GetStringOrEmpty("slug"),
            LegacyCenterCode = reader.GetStringOrEmpty("legacy_center_code"),
            IsActive = reader.GetBooleanValue("is_active")
        };
    }

    private static async Task<CompanyLegacyCenterImpactDto> GetCompanyLegacyCenterImpactCoreAsync(
        MySqlConnection connection,
        CompanySummaryDto company,
        CancellationToken cancellationToken)
    {
        var assignedUsersCount = await CountBySqlAsync(
            connection,
            """
            SELECT COUNT(*)
            FROM user_company_memberships
            WHERE company_id = @companyId;
            """,
            [("@companyId", company.Id.ToString())],
            cancellationToken);

        var clientCount = await CountLegacyCenterRowsAsync(connection, "clients", company.LegacyCenterCode, cancellationToken);
        var supplierCount = await CountLegacyCenterRowsAsync(connection, "prove", company.LegacyCenterCode, cancellationToken);
        var purchaseDocumentCount = await CountLegacyCenterRowsAsync(connection, "cactur", company.LegacyCenterCode, cancellationToken);
        var salesDocumentCount = await CountLegacyCenterRowsAsync(connection, "factur", company.LegacyCenterCode, cancellationToken);
        var auditEventCount = await CountBySqlAsync(
            connection,
            """
            SELECT COUNT(*)
            FROM audit_logs
            WHERE company_id = @companyId;
            """,
            [("@companyId", company.Id.ToString())],
            cancellationToken);

        return new CompanyLegacyCenterImpactDto
        {
            CompanyId = company.Id,
            TenantId = company.TenantId,
            CompanyName = company.Name,
            CurrentLegacyCenterCode = company.LegacyCenterCode,
            AssignedUsersCount = assignedUsersCount,
            ClientCount = clientCount,
            SupplierCount = supplierCount,
            PurchaseDocumentCount = purchaseDocumentCount,
            SalesDocumentCount = salesDocumentCount,
            AuditEventCount = auditEventCount
        };
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

    private static async Task AddPlatformAdminRoleAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid userId,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            INSERT INTO user_role_assignments (user_id, tenant_id, role_name, created_utc)
            VALUES (@userId, NULL, @roleName, @createdUtc);
            """;
        command.Parameters.AddWithValue("@userId", userId.ToString());
        command.Parameters.AddWithValue("@roleName", PlatformRoles.PlatformAdmin);
        command.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
        await command.ExecuteNonQueryAsync(cancellationToken);
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

    private static async Task<bool> IsPlatformAdminAsync(
        MySqlConnection connection,
        Guid userId,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT COUNT(*)
            FROM user_role_assignments
            WHERE user_id = @userId
              AND tenant_id IS NULL
              AND role_name = @roleName;
            """;
        command.Parameters.AddWithValue("@userId", userId.ToString());
        command.Parameters.AddWithValue("@roleName", PlatformRoles.PlatformAdmin);
        return Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken)) > 0;
    }

    private static async Task<bool> IsUserActiveTenantAdminAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid userId,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT COUNT(*)
            FROM user_role_assignments ura
            INNER JOIN app_users u ON u.id = ura.user_id
            WHERE ura.tenant_id = @tenantId
              AND ura.user_id = @userId
              AND ura.role_name = @roleName
              AND u.is_active = 1;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@userId", userId.ToString());
        command.Parameters.AddWithValue("@roleName", PlatformRoles.TenantAdmin);
        return Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken)) > 0;
    }

    private static async Task<int> CountActiveTenantAdminsAsync(
        MySqlConnection connection,
        Guid tenantId,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT COUNT(DISTINCT ura.user_id)
            FROM user_role_assignments ura
            INNER JOIN app_users u ON u.id = ura.user_id
            WHERE ura.tenant_id = @tenantId
              AND ura.role_name = @roleName
              AND u.is_active = 1;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@roleName", PlatformRoles.TenantAdmin);
        return Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));
    }

    private static async Task<Guid?> GetUserTenantIdAsync(
        MySqlConnection connection,
        Guid userId,
        CancellationToken cancellationToken)
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

    private static async Task EnsureTenantSlugAvailableAsync(
        MySqlConnection connection,
        string slug,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(*) FROM tenants WHERE slug = @slug;";
        command.Parameters.AddWithValue("@slug", slug);
        var exists = Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken)) > 0;
        if (exists)
        {
            throw new InvalidOperationException("Ya existe un tenant con ese slug.");
        }
    }

    private static async Task EnsureCompanySlugAvailableAsync(
        MySqlConnection connection,
        Guid tenantId,
        string slug,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT COUNT(*)
            FROM companies
            WHERE tenant_id = @tenantId
              AND slug = @slug;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@slug", slug);
        var exists = Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken)) > 0;
        if (exists)
        {
            throw new InvalidOperationException("Ya existe una company con ese slug dentro del tenant.");
        }
    }

    private static async Task EnsureLegacyCenterAvailableAsync(
        MySqlConnection connection,
        Guid tenantId,
        string legacyCenterCode,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT COUNT(*)
            FROM companies
            WHERE tenant_id = @tenantId
              AND legacy_center_code = @legacyCenterCode;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@legacyCenterCode", legacyCenterCode);
        var exists = Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken)) > 0;
        if (exists)
        {
            throw new InvalidOperationException("Ya existe una company en este tenant con ese centro legacy. Cada company debe usar un centro distinto.");
        }
    }

    private static async Task<int> CountLegacyCenterRowsAsync(
        MySqlConnection connection,
        string tableName,
        string legacyCenterCode,
        CancellationToken cancellationToken)
    {
        try
        {
            return await CountBySqlAsync(
                connection,
                $"""
                SELECT COUNT(*)
                FROM {tableName}
                WHERE CENTRO = @centerCode;
                """,
                [("@centerCode", legacyCenterCode)],
                cancellationToken);
        }
        catch (MySqlException)
        {
            return 0;
        }
    }

    private static async Task<int> CountBySqlAsync(
        MySqlConnection connection,
        string sql,
        IReadOnlyCollection<(string Name, object? Value)> parameters,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        foreach (var parameter in parameters)
        {
            command.Parameters.AddWithValue(parameter.Name, parameter.Value);
        }

        return Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));
    }

    private static IReadOnlyCollection<NormalizedInitialCompany> NormalizeInitialCompanies(
        IReadOnlyCollection<InitialCompanyInput>? initialCompanies,
        string fallbackCompanyName = "",
        string fallbackCompanySlug = "",
        string fallbackLegacyCenterCode = "")
    {
        var rawCompanies = (initialCompanies ?? [])
            .Where(company => company is not null)
            .Select(company => new InitialCompanyInput
            {
                Name = company.Name,
                Slug = company.Slug,
                LegacyCenterCode = company.LegacyCenterCode
            })
            .Where(company =>
                !string.IsNullOrWhiteSpace(company.Name) ||
                !string.IsNullOrWhiteSpace(company.Slug) ||
                !string.IsNullOrWhiteSpace(company.LegacyCenterCode))
            .ToList();

        if (rawCompanies.Count == 0 &&
            (!string.IsNullOrWhiteSpace(fallbackCompanyName) ||
             !string.IsNullOrWhiteSpace(fallbackCompanySlug) ||
             !string.IsNullOrWhiteSpace(fallbackLegacyCenterCode)))
        {
            rawCompanies.Add(new InitialCompanyInput
            {
                Name = fallbackCompanyName,
                Slug = fallbackCompanySlug,
                LegacyCenterCode = fallbackLegacyCenterCode
            });
        }

        if (rawCompanies.Count == 0)
        {
            return [];
        }

        var normalizedCompanies = new List<NormalizedInitialCompany>(rawCompanies.Count);

        for (var index = 0; index < rawCompanies.Count; index++)
        {
            var company = rawCompanies[index];
            var name = company.Name.Trim();
            var slug = SlugGenerator.Generate(string.IsNullOrWhiteSpace(company.Slug) ? name : company.Slug);
            var legacyCenterCode = company.LegacyCenterCode.Trim().ToUpperInvariant();

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new InvalidOperationException($"La company inicial {index + 1} necesita nombre.");
            }

            if (string.IsNullOrWhiteSpace(legacyCenterCode) || legacyCenterCode.Length != 1)
            {
                throw new InvalidOperationException($"La company inicial {index + 1} necesita un centro legacy de 1 caracter.");
            }

            normalizedCompanies.Add(new NormalizedInitialCompany(name, slug, legacyCenterCode));
        }

        if (normalizedCompanies.Count != normalizedCompanies
                .Select(company => company.Slug)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .Count())
        {
            throw new InvalidOperationException("Las companies iniciales no pueden repetir slug dentro del mismo tenant.");
        }

        if (normalizedCompanies.Count != normalizedCompanies
                .Select(company => company.LegacyCenterCode)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .Count())
        {
            throw new InvalidOperationException("Las companies iniciales no pueden repetir centro legacy dentro del mismo tenant.");
        }

        return normalizedCompanies;
    }

    private static string FormatCompanyAuditSummary(IEnumerable<CompanySummaryDto> companies)
    {
        return string.Join(", ", companies.Select(company => $"{company.Name}({company.LegacyCenterCode})"));
    }

    private static async Task<int> CountAsync(
        MySqlConnection connection,
        string sql,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        return Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));
    }

    private sealed record NormalizedInitialCompany(string Name, string Slug, string LegacyCenterCode);
}
