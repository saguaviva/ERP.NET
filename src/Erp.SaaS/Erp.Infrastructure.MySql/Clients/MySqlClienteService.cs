using Erp.Application.Clients;
using Erp.Application.Auditing;
using Erp.Application.Companies;
using Erp.Application.Contexts;
using Erp.Application.Tenants;
using Erp.Domain.Common;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Support;
using MySqlConnector;
using System.Net.Mail;

namespace Erp.Infrastructure.MySql.Clients;

public sealed class MySqlClienteService : IClienteQueries, IClienteService
{
    private readonly MySqlConnectionFactory _connectionFactory;
    private readonly IAuditLogService _auditLogService;
    private readonly ICompanyAccessService _companyAccessService;
    private readonly ICurrentUserContext _currentUserContext;
    private readonly ITenantContext _tenantContext;
    private readonly IActiveCompanyContext _activeCompanyContext;

    public MySqlClienteService(
        MySqlConnectionFactory connectionFactory,
        IAuditLogService auditLogService,
        ICompanyAccessService companyAccessService,
        ICurrentUserContext currentUserContext,
        ITenantContext tenantContext,
        IActiveCompanyContext activeCompanyContext)
    {
        _connectionFactory = connectionFactory;
        _auditLogService = auditLogService;
        _companyAccessService = companyAccessService;
        _currentUserContext = currentUserContext;
        _tenantContext = tenantContext;
        _activeCompanyContext = activeCompanyContext;
    }

    public async Task<ClienteSearchResultDto> SearchAsync(
        Guid tenantId,
        Guid companyId,
        ClienteFilter filter,
        CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return new ClienteSearchResultDto();
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);

        var pageSize = Math.Clamp(filter.PageSize, 10, 200);
        var page = Math.Max(filter.Page, 1);
        var offset = (page - 1) * pageSize;
        var search = filter.Search?.Trim() ?? string.Empty;
        var likeSearch = $"%{search}%";
        var items = new List<ClienteListItemDto>();

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var moduleContext = await ResolveClientModuleContextAsync(connection, tenantId, companyId, cancellationToken);
        var mainCenterParameterNames = moduleContext.VisibleCenterCodes
            .Select((_, index) => $"@mainCenter{index}")
            .ToArray();
        var duplicateCenterParameterNames = moduleContext.VisibleCenterCodes
            .Select((_, index) => $"@dupCenter{index}")
            .ToArray();

        await using (var countCommand = connection.CreateCommand())
        {
            countCommand.CommandText =
                $"""
                SELECT COUNT(*)
                FROM clients c
                WHERE c.CENTRO IN ({string.Join(", ", mainCenterParameterNames)})
                  AND COALESCE(c.is_deleted, 0) = 0
                  AND (@includeBlocked = 1 OR COALESCE(c.BLOQUEADO, 0) = 0)
                  AND (
                        @search = ''
                        OR c.NOM LIKE @likeSearch
                        OR c.NIF LIKE @likeSearch
                        OR c.POB LIKE @likeSearch
                        OR c.EMAIL1 LIKE @likeSearch
                        OR c.TEL LIKE @likeSearch
                      );
                """;
            AddCenterParameters(countCommand, mainCenterParameterNames, moduleContext.VisibleCenterCodes);
            countCommand.Parameters.AddWithValue("@includeBlocked", filter.IncludeBlocked);
            countCommand.Parameters.AddWithValue("@search", search);
            countCommand.Parameters.AddWithValue("@likeSearch", likeSearch);

            var totalCount = Convert.ToInt32(await countCommand.ExecuteScalarAsync(cancellationToken));
            if (totalCount == 0)
            {
                return new ClienteSearchResultDto
                {
                    TotalCount = 0
                };
            }

            await using var command = connection.CreateCommand();
            command.CommandText =
            $"""
            SELECT
                c.CODI,
                c.CENTRO,
                c.NOM,
                c.NIF,
                c.POB,
                c.EMAIL1,
                c.TEL,
                COALESCE(c.BLOQUEADO, 0) AS BLOQUEADO
            FROM clients c
            WHERE c.CENTRO IN ({string.Join(", ", mainCenterParameterNames)})
              AND COALESCE(c.is_deleted, 0) = 0
              AND (@includeBlocked = 1 OR COALESCE(c.BLOQUEADO, 0) = 0)
              AND (
                    @search = ''
                    OR c.NOM LIKE @likeSearch
                    OR c.NIF LIKE @likeSearch
                    OR c.POB LIKE @likeSearch
                    OR c.EMAIL1 LIKE @likeSearch
                    OR c.TEL LIKE @likeSearch
                  )
            {BuildSearchOrderByClause(filter)}
            LIMIT @limit OFFSET @offset;
            """;
            AddCenterParameters(command, mainCenterParameterNames, moduleContext.VisibleCenterCodes);
            command.Parameters.AddWithValue("@includeBlocked", filter.IncludeBlocked);
            command.Parameters.AddWithValue("@search", search);
            command.Parameters.AddWithValue("@likeSearch", likeSearch);
            command.Parameters.AddWithValue("@limit", pageSize);
            command.Parameters.AddWithValue("@offset", offset);

            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                items.Add(new ClienteListItemDto
                {
                    Code = reader.GetInt32(reader.GetOrdinal("CODI")),
                    CompanyCenterCode = reader.GetStringOrEmpty("CENTRO"),
                    Name = reader.GetStringOrEmpty("NOM"),
                    TaxId = reader.GetStringOrEmpty("NIF"),
                    City = reader.GetStringOrEmpty("POB"),
                    Email = reader.GetStringOrEmpty("EMAIL1"),
                    Phone = reader.GetStringOrEmpty("TEL"),
                    IsBlocked = reader.GetBooleanValue("BLOQUEADO")
                });
            }
            await reader.DisposeAsync();

            await LoadDuplicateFlagsAsync(
                connection,
                tenantId,
                moduleContext.ScopeCompanyId,
                moduleContext.VisibleCenterCodes,
                duplicateCenterParameterNames,
                moduleContext.IsTenantShared,
                items,
                cancellationToken);

            return new ClienteSearchResultDto
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }

    public async Task<ClienteDetailDto?> GetByCodeAsync(Guid tenantId, Guid companyId, int code, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return null;
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var moduleContext = await ResolveClientModuleContextAsync(connection, tenantId, companyId, cancellationToken);
        var centerParameterNames = moduleContext.VisibleCenterCodes
            .Select((_, index) => $"@detailCenter{index}")
            .ToArray();
        await using var command = connection.CreateCommand();
        command.CommandText =
            $"""
            SELECT CODI, CENTRO, NOM, NIF, DOM, CP, POB, PROV, PAIS, CONTACTE, TEL, FAX, EMAIL1, EMAIL2, WEB, NOTES, COALESCE(BLOQUEADO, 0) AS BLOQUEADO
            FROM clients
            WHERE CODI = @code
              AND CENTRO IN ({string.Join(", ", centerParameterNames)})
              AND COALESCE(is_deleted, 0) = 0
            ORDER BY CASE WHEN CENTRO = @activeCenterCode THEN 0 ELSE 1 END, CENTRO
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@code", code);
        command.Parameters.AddWithValue("@activeCenterCode", moduleContext.ActiveCenterCode);
        AddCenterParameters(command, centerParameterNames, moduleContext.VisibleCenterCodes);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        var detail = new ClienteDetailDto
        {
            Code = reader.GetInt32(reader.GetOrdinal("CODI")),
            CompanyCenterCode = reader.GetStringOrEmpty("CENTRO"),
            Name = reader.GetStringOrEmpty("NOM"),
            TaxId = reader.GetStringOrEmpty("NIF"),
            Address = reader.GetStringOrEmpty("DOM"),
            PostalCode = reader.GetStringOrEmpty("CP"),
            City = reader.GetStringOrEmpty("POB"),
            Province = reader.GetStringOrEmpty("PROV"),
            Country = reader.GetStringOrEmpty("PAIS"),
            ContactName = reader.GetStringOrEmpty("CONTACTE"),
            Phone = reader.GetStringOrEmpty("TEL"),
            Fax = reader.GetStringOrEmpty("FAX"),
            PrimaryEmail = reader.GetStringOrEmpty("EMAIL1"),
            SecondaryEmail = reader.GetStringOrEmpty("EMAIL2"),
            Website = reader.GetStringOrEmpty("WEB"),
            Notes = reader.GetStringOrEmpty("NOTES"),
            IsBlocked = reader.GetBooleanValue("BLOQUEADO")
        };

        await reader.DisposeAsync();
        detail.SecondaryAddresses = (await GetSecondaryAddressesAsync(connection, detail.CompanyCenterCode, code, cancellationToken)).ToList();
        detail.Contacts = (await GetContactsAsync(connection, tenantId, moduleContext.ScopeCompanyId, code, cancellationToken)).ToList();
        return detail;
    }

    public async Task<IReadOnlyCollection<ClienteDuplicatePairDto>> GetDuplicateInboxAsync(
        Guid tenantId,
        Guid companyId,
        CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return [];
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);
        var items = new List<ClienteDuplicatePairDto>();

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var moduleContext = await ResolveClientModuleContextAsync(connection, tenantId, companyId, cancellationToken);
        var leftCenterParameterNames = moduleContext.VisibleCenterCodes
            .Select((_, index) => $"@leftCenter{index}")
            .ToArray();
        var rightCenterParameterNames = moduleContext.VisibleCenterCodes
            .Select((_, index) => $"@rightCenter{index}")
            .ToArray();
        await using var command = connection.CreateCommand();
        command.CommandText =
            $"""
            SELECT
                c1.CODI AS left_code,
                COALESCE(c1.NOM, '') AS left_name,
                c2.CODI AS right_code,
                COALESCE(c2.NOM, '') AS right_name,
                COALESCE(c1.NIF, '') AS left_tax_id,
                COALESCE(c2.NIF, '') AS right_tax_id,
                COALESCE(c1.EMAIL1, '') AS left_email1,
                COALESCE(c1.EMAIL2, '') AS left_email2,
                COALESCE(c2.EMAIL1, '') AS right_email1,
                COALESCE(c2.EMAIL2, '') AS right_email2,
                COALESCE(c1.TEL, '') AS left_phone,
                COALESCE(c2.TEL, '') AS right_phone,
                COALESCE(dr.status, '') AS review_status,
                dr.updated_utc AS review_updated_utc,
                COALESCE(u.display_name, '') AS review_updated_by,
                dr.preferred_client_code AS preferred_client_code,
                dr.preferred_updated_utc AS preferred_updated_utc,
                COALESCE(pu.display_name, '') AS preferred_updated_by
            FROM clients c1
            INNER JOIN clients c2
                ON c1.CODI < c2.CODI
            LEFT JOIN client_duplicate_reviews dr
                ON dr.tenant_id = @tenantId
               AND dr.company_id = @scopeCompanyId
               AND dr.left_client_code = c1.CODI
               AND dr.right_client_code = c2.CODI
            LEFT JOIN app_users u ON u.id = dr.updated_by_user_id
            LEFT JOIN app_users pu ON pu.id = dr.preferred_updated_by_user_id
            WHERE c1.CENTRO IN ({string.Join(", ", leftCenterParameterNames)})
              AND c2.CENTRO IN ({string.Join(", ", rightCenterParameterNames)})
              AND COALESCE(c1.is_deleted, 0) = 0
              AND COALESCE(c2.is_deleted, 0) = 0
              AND (
                    (UPPER(TRIM(COALESCE(c1.NIF, ''))) <> '' AND UPPER(TRIM(COALESCE(c1.NIF, ''))) = UPPER(TRIM(COALESCE(c2.NIF, ''))))
                 OR (LOWER(TRIM(COALESCE(c1.EMAIL1, ''))) <> '' AND (
                        LOWER(TRIM(COALESCE(c1.EMAIL1, ''))) = LOWER(TRIM(COALESCE(c2.EMAIL1, '')))
                     OR LOWER(TRIM(COALESCE(c1.EMAIL1, ''))) = LOWER(TRIM(COALESCE(c2.EMAIL2, '')))
                    ))
                 OR (LOWER(TRIM(COALESCE(c1.EMAIL2, ''))) <> '' AND (
                        LOWER(TRIM(COALESCE(c1.EMAIL2, ''))) = LOWER(TRIM(COALESCE(c2.EMAIL1, '')))
                     OR LOWER(TRIM(COALESCE(c1.EMAIL2, ''))) = LOWER(TRIM(COALESCE(c2.EMAIL2, '')))
                    ))
                 OR (UPPER(TRIM(COALESCE(c1.NOM, ''))) <> '' AND UPPER(TRIM(COALESCE(c1.NOM, ''))) = UPPER(TRIM(COALESCE(c2.NOM, ''))))
                 OR (TRIM(COALESCE(c1.TEL, '')) <> '' AND TRIM(COALESCE(c1.TEL, '')) = TRIM(COALESCE(c2.TEL, '')))
                  )
            ORDER BY
                CASE WHEN COALESCE(dr.status, '') = '' THEN 0 ELSE 1 END,
                CASE WHEN COALESCE(dr.status, '') = @falsePositiveStatus THEN 1 ELSE 0 END,
                c1.NOM,
                c2.NOM
            LIMIT 250;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@scopeCompanyId", moduleContext.ScopeCompanyId.ToString());
        command.Parameters.AddWithValue("@falsePositiveStatus", ClienteDuplicateReviewStatus.FalsePositive);
        AddCenterParameters(command, leftCenterParameterNames, moduleContext.VisibleCenterCodes);
        AddCenterParameters(command, rightCenterParameterNames, moduleContext.VisibleCenterCodes);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var leftTaxId = reader.GetStringOrEmpty("left_tax_id");
            var rightTaxId = reader.GetStringOrEmpty("right_tax_id");
            var leftEmail1 = reader.GetStringOrEmpty("left_email1");
            var leftEmail2 = reader.GetStringOrEmpty("left_email2");
            var rightEmail1 = reader.GetStringOrEmpty("right_email1");
            var rightEmail2 = reader.GetStringOrEmpty("right_email2");
            var leftPhone = reader.GetStringOrEmpty("left_phone");
            var rightPhone = reader.GetStringOrEmpty("right_phone");

            var reasons = BuildPairReasons(leftTaxId, rightTaxId, leftEmail1, leftEmail2, rightEmail1, rightEmail2, leftPhone, rightPhone, reader.GetStringOrEmpty("left_name"), reader.GetStringOrEmpty("right_name"));
            if (reasons.Count == 0)
            {
                continue;
            }

            items.Add(new ClienteDuplicatePairDto
            {
                LeftCode = reader.GetInt32(reader.GetOrdinal("left_code")),
                LeftName = reader.GetStringOrEmpty("left_name"),
                RightCode = reader.GetInt32(reader.GetOrdinal("right_code")),
                RightName = reader.GetStringOrEmpty("right_name"),
                SharedTaxId = GetSharedValue(leftTaxId, rightTaxId),
                SharedEmail = GetSharedEmail(leftEmail1, leftEmail2, rightEmail1, rightEmail2),
                SharedPhone = GetSharedValue(leftPhone, rightPhone),
                IsHardConflict = reasons.Any(reason =>
                    string.Equals(reason, "Mismo NIF", StringComparison.Ordinal) ||
                    reason.StartsWith("Mismo email", StringComparison.Ordinal)),
                MatchReasons = reasons,
                ReviewStatus = reader.GetStringOrEmpty("review_status"),
                ReviewUpdatedUtc = reader.IsDBNull(reader.GetOrdinal("review_updated_utc"))
                    ? null
                    : new DateTimeOffset(reader.GetDateTime(reader.GetOrdinal("review_updated_utc")), TimeSpan.Zero),
                ReviewUpdatedBy = reader.GetStringOrEmpty("review_updated_by"),
                PreferredClientCode = reader.IsDBNull(reader.GetOrdinal("preferred_client_code"))
                    ? null
                    : reader.GetInt32(reader.GetOrdinal("preferred_client_code")),
                PreferredUpdatedUtc = reader.IsDBNull(reader.GetOrdinal("preferred_updated_utc"))
                    ? null
                    : new DateTimeOffset(reader.GetDateTime(reader.GetOrdinal("preferred_updated_utc")), TimeSpan.Zero),
                PreferredUpdatedBy = reader.GetStringOrEmpty("preferred_updated_by")
            });
        }

        return items;
    }

    public async Task<IReadOnlyCollection<ClienteDuplicateDto>> FindDuplicatesAsync(
        Guid tenantId,
        Guid companyId,
        SaveClienteCommand command,
        CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return [];
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);

        var normalizedName = NormalizeName(command.Name);
        var normalizedTaxId = NormalizeTaxId(command.TaxId);
        var normalizedPrimaryEmail = NormalizeEmail(command.PrimaryEmail);
        var normalizedSecondaryEmail = NormalizeEmail(command.SecondaryEmail);
        var normalizedPhone = NormalizePhone(command.Phone);

        if (string.IsNullOrWhiteSpace(normalizedName) &&
            string.IsNullOrWhiteSpace(normalizedTaxId) &&
            string.IsNullOrWhiteSpace(normalizedPrimaryEmail) &&
            string.IsNullOrWhiteSpace(normalizedSecondaryEmail) &&
            string.IsNullOrWhiteSpace(normalizedPhone))
        {
            return [];
        }

        var items = new List<ClienteDuplicateDto>();

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var moduleContext = await ResolveClientModuleContextAsync(connection, tenantId, companyId, cancellationToken);
        var centerParameterNames = moduleContext.VisibleCenterCodes
            .Select((_, index) => $"@duplicateCenter{index}")
            .ToArray();
        await using var dbCommand = connection.CreateCommand();
        dbCommand.CommandText =
            $"""
            SELECT
                c.CODI,
                c.CENTRO,
                c.NOM,
                c.NIF,
                c.POB,
                c.EMAIL1,
                c.EMAIL2,
                c.TEL,
                COALESCE(c.BLOQUEADO, 0) AS BLOQUEADO,
                COALESCE(dr.status, '') AS review_status,
                dr.updated_utc AS review_updated_utc,
                COALESCE(u.display_name, '') AS review_updated_by
            FROM clients c
            LEFT JOIN client_duplicate_reviews dr
              ON @code IS NOT NULL
             AND dr.tenant_id = @tenantId
             AND dr.company_id = @scopeCompanyId
             AND dr.left_client_code = LEAST(c.CODI, @code)
             AND dr.right_client_code = GREATEST(c.CODI, @code)
            LEFT JOIN app_users u ON u.id = dr.updated_by_user_id
            WHERE CENTRO IN ({string.Join(", ", centerParameterNames)})
              AND COALESCE(c.is_deleted, 0) = 0
              AND (@code IS NULL OR c.CODI <> @code)
              AND (
                    (@normalizedTaxId <> '' AND UPPER(TRIM(COALESCE(c.NIF, ''))) = @normalizedTaxId)
                 OR (@normalizedPrimaryEmail <> '' AND (
                        LOWER(TRIM(COALESCE(c.EMAIL1, ''))) = @normalizedPrimaryEmail
                     OR LOWER(TRIM(COALESCE(c.EMAIL2, ''))) = @normalizedPrimaryEmail
                    ))
                 OR (@normalizedSecondaryEmail <> '' AND (
                        LOWER(TRIM(COALESCE(c.EMAIL1, ''))) = @normalizedSecondaryEmail
                     OR LOWER(TRIM(COALESCE(c.EMAIL2, ''))) = @normalizedSecondaryEmail
                    ))
                 OR (@normalizedName <> '' AND UPPER(TRIM(COALESCE(c.NOM, ''))) = @normalizedName)
                 OR (@normalizedPhone <> '' AND TRIM(COALESCE(c.TEL, '')) = @normalizedPhone)
                  )
            ORDER BY c.NOM
            LIMIT 25;
            """;
        AddCenterParameters(dbCommand, centerParameterNames, moduleContext.VisibleCenterCodes);
        dbCommand.Parameters.AddWithValue("@code", command.Code);
        dbCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        dbCommand.Parameters.AddWithValue("@scopeCompanyId", moduleContext.ScopeCompanyId.ToString());
        dbCommand.Parameters.AddWithValue("@normalizedName", normalizedName);
        dbCommand.Parameters.AddWithValue("@normalizedTaxId", normalizedTaxId);
        dbCommand.Parameters.AddWithValue("@normalizedPrimaryEmail", normalizedPrimaryEmail);
        dbCommand.Parameters.AddWithValue("@normalizedSecondaryEmail", normalizedSecondaryEmail);
        dbCommand.Parameters.AddWithValue("@normalizedPhone", normalizedPhone);

        await using var reader = await dbCommand.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var candidate = new ClienteDetailDto
            {
                Code = reader.GetInt32(reader.GetOrdinal("CODI")),
                CompanyCenterCode = reader.GetStringOrEmpty("CENTRO"),
                Name = reader.GetStringOrEmpty("NOM"),
                TaxId = reader.GetStringOrEmpty("NIF"),
                City = reader.GetStringOrEmpty("POB"),
                PrimaryEmail = reader.GetStringOrEmpty("EMAIL1"),
                SecondaryEmail = reader.GetStringOrEmpty("EMAIL2"),
                Phone = reader.GetStringOrEmpty("TEL"),
                IsBlocked = reader.GetBooleanValue("BLOQUEADO")
            };

            var reasons = BuildDuplicateReasons(command, candidate);
            if (reasons.Count == 0)
            {
                continue;
            }

            items.Add(new ClienteDuplicateDto
            {
                Code = candidate.Code ?? 0,
                Name = candidate.Name,
                TaxId = candidate.TaxId,
                City = candidate.City,
                PrimaryEmail = candidate.PrimaryEmail,
                SecondaryEmail = candidate.SecondaryEmail,
                Phone = candidate.Phone,
                IsBlocked = candidate.IsBlocked,
                MatchReasons = reasons,
                IsHardConflict = reasons.Any(reason =>
                    string.Equals(reason, "Mismo NIF", StringComparison.Ordinal) ||
                    reason.StartsWith("Mismo email", StringComparison.Ordinal)),
                ReviewStatus = reader.GetStringOrEmpty("review_status"),
                ReviewUpdatedUtc = reader.IsDBNull(reader.GetOrdinal("review_updated_utc"))
                    ? null
                    : new DateTimeOffset(reader.GetDateTime(reader.GetOrdinal("review_updated_utc")), TimeSpan.Zero),
                ReviewUpdatedBy = reader.GetStringOrEmpty("review_updated_by")
            });
        }

        return items;
    }

    public async Task SetDuplicateReviewAsync(SetClienteDuplicateReviewCommand command, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return;
        }

        await EnsureCompanyAccessAsync(command.TenantId, command.CompanyId, cancellationToken);
        EnsureTenantWriteAccess();

        if (command.ClientCode <= 0 || command.DuplicateClientCode <= 0 || command.ClientCode == command.DuplicateClientCode)
        {
            throw new InvalidOperationException("La revisión de duplicados necesita dos clientes distintos.");
        }

        if (!string.IsNullOrWhiteSpace(command.Status) &&
            !ClienteDuplicateReviewStatus.All.Contains(command.Status, StringComparer.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("El estado de revisión indicado no es válido.");
        }

        var (leftCode, rightCode) = NormalizeClientPair(command.ClientCode, command.DuplicateClientCode);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var moduleContext = await ResolveClientModuleContextAsync(connection, command.TenantId, command.CompanyId, cancellationToken);

        if (string.IsNullOrWhiteSpace(command.Status))
        {
            await using var clearCommand = connection.CreateCommand();
            clearCommand.CommandText =
                """
                UPDATE client_duplicate_reviews
                SET status = '',
                    updated_by_user_id = NULL,
                    updated_utc = @updatedUtc
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND left_client_code = @leftCode
                  AND right_client_code = @rightCode;
                """;
            clearCommand.Parameters.AddWithValue("@updatedUtc", DateTime.UtcNow);
            clearCommand.Parameters.AddWithValue("@tenantId", command.TenantId.ToString());
            clearCommand.Parameters.AddWithValue("@companyId", moduleContext.ScopeCompanyId.ToString());
            clearCommand.Parameters.AddWithValue("@leftCode", leftCode);
            clearCommand.Parameters.AddWithValue("@rightCode", rightCode);
            await clearCommand.ExecuteNonQueryAsync(cancellationToken);

            await DeleteDuplicatePairRowIfEmptyAsync(connection, command.TenantId, moduleContext.ScopeCompanyId, leftCode, rightCode, cancellationToken);

            await _auditLogService.WriteAsync(new WriteAuditLogCommand
            {
                TenantId = command.TenantId,
                CompanyId = command.CompanyId,
                Action = "ClienteDuplicateReviewCleared",
                EntityName = "ClienteDuplicatePair",
                EntityId = $"{leftCode}-{rightCode}",
                Details = "Review cleared."
            }, cancellationToken);

            return;
        }

        await using var upsertCommand = connection.CreateCommand();
        upsertCommand.CommandText =
            """
            INSERT INTO client_duplicate_reviews
                (tenant_id, company_id, left_client_code, right_client_code, status, updated_by_user_id, updated_utc)
            VALUES
                (@tenantId, @companyId, @leftCode, @rightCode, @status, @updatedByUserId, @updatedUtc)
            ON DUPLICATE KEY UPDATE
                status = VALUES(status),
                updated_by_user_id = VALUES(updated_by_user_id),
                updated_utc = VALUES(updated_utc);
            """;
        upsertCommand.Parameters.AddWithValue("@tenantId", command.TenantId.ToString());
        upsertCommand.Parameters.AddWithValue("@companyId", moduleContext.ScopeCompanyId.ToString());
        upsertCommand.Parameters.AddWithValue("@leftCode", leftCode);
        upsertCommand.Parameters.AddWithValue("@rightCode", rightCode);
        upsertCommand.Parameters.AddWithValue("@status", command.Status!.Trim().ToLowerInvariant());
        upsertCommand.Parameters.AddWithValue("@updatedByUserId", _currentUserContext.UserId?.ToString());
        upsertCommand.Parameters.AddWithValue("@updatedUtc", DateTime.UtcNow);
        await upsertCommand.ExecuteNonQueryAsync(cancellationToken);

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = command.TenantId,
            CompanyId = command.CompanyId,
            Action = "ClienteDuplicateReviewSet",
            EntityName = "ClienteDuplicatePair",
            EntityId = $"{leftCode}-{rightCode}",
            Details = $"Status={command.Status!.Trim().ToLowerInvariant()}"
        }, cancellationToken);
    }

    public async Task SetPreferredPrincipalAsync(SetClientePreferredPrincipalCommand command, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return;
        }

        await EnsureCompanyAccessAsync(command.TenantId, command.CompanyId, cancellationToken);
        EnsureTenantWriteAccess();

        if (command.ClientCode <= 0 || command.DuplicateClientCode <= 0 || command.ClientCode == command.DuplicateClientCode)
        {
            throw new InvalidOperationException("La propuesta de principal necesita dos clientes distintos.");
        }

        if (command.PreferredClientCode.HasValue &&
            command.PreferredClientCode.Value != command.ClientCode &&
            command.PreferredClientCode.Value != command.DuplicateClientCode)
        {
            throw new InvalidOperationException("El cliente principal propuesto debe pertenecer a la pareja seleccionada.");
        }

        var (leftCode, rightCode) = NormalizeClientPair(command.ClientCode, command.DuplicateClientCode);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var moduleContext = await ResolveClientModuleContextAsync(connection, command.TenantId, command.CompanyId, cancellationToken);

        if (!command.PreferredClientCode.HasValue)
        {
            await using var clearCommand = connection.CreateCommand();
            clearCommand.CommandText =
                """
                UPDATE client_duplicate_reviews
                SET preferred_client_code = NULL,
                    preferred_updated_by_user_id = NULL,
                    preferred_updated_utc = NULL
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND left_client_code = @leftCode
                  AND right_client_code = @rightCode;
                """;
            clearCommand.Parameters.AddWithValue("@tenantId", command.TenantId.ToString());
            clearCommand.Parameters.AddWithValue("@companyId", moduleContext.ScopeCompanyId.ToString());
            clearCommand.Parameters.AddWithValue("@leftCode", leftCode);
            clearCommand.Parameters.AddWithValue("@rightCode", rightCode);
            await clearCommand.ExecuteNonQueryAsync(cancellationToken);

            await DeleteDuplicatePairRowIfEmptyAsync(connection, command.TenantId, moduleContext.ScopeCompanyId, leftCode, rightCode, cancellationToken);

            await _auditLogService.WriteAsync(new WriteAuditLogCommand
            {
                TenantId = command.TenantId,
                CompanyId = command.CompanyId,
                Action = "ClienteDuplicatePrincipalCleared",
                EntityName = "ClienteDuplicatePair",
                EntityId = $"{leftCode}-{rightCode}",
                Details = "Principal proposal cleared."
            }, cancellationToken);

            return;
        }

        var now = DateTime.UtcNow;

        await using var upsertCommand = connection.CreateCommand();
        upsertCommand.CommandText =
            """
            INSERT INTO client_duplicate_reviews
                (tenant_id, company_id, left_client_code, right_client_code, status, updated_by_user_id, updated_utc, preferred_client_code, preferred_updated_by_user_id, preferred_updated_utc)
            VALUES
                (@tenantId, @companyId, @leftCode, @rightCode, '', NULL, @updatedUtc, @preferredClientCode, @preferredUpdatedByUserId, @preferredUpdatedUtc)
            ON DUPLICATE KEY UPDATE
                preferred_client_code = VALUES(preferred_client_code),
                preferred_updated_by_user_id = VALUES(preferred_updated_by_user_id),
                preferred_updated_utc = VALUES(preferred_updated_utc);
            """;
        upsertCommand.Parameters.AddWithValue("@tenantId", command.TenantId.ToString());
        upsertCommand.Parameters.AddWithValue("@companyId", moduleContext.ScopeCompanyId.ToString());
        upsertCommand.Parameters.AddWithValue("@leftCode", leftCode);
        upsertCommand.Parameters.AddWithValue("@rightCode", rightCode);
        upsertCommand.Parameters.AddWithValue("@updatedUtc", now);
        upsertCommand.Parameters.AddWithValue("@preferredClientCode", command.PreferredClientCode.Value);
        upsertCommand.Parameters.AddWithValue("@preferredUpdatedByUserId", _currentUserContext.UserId?.ToString());
        upsertCommand.Parameters.AddWithValue("@preferredUpdatedUtc", now);
        await upsertCommand.ExecuteNonQueryAsync(cancellationToken);

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = command.TenantId,
            CompanyId = command.CompanyId,
            Action = "ClienteDuplicatePrincipalProposed",
            EntityName = "ClienteDuplicatePair",
            EntityId = $"{leftCode}-{rightCode}",
            Details = $"PreferredClientCode={command.PreferredClientCode.Value}"
        }, cancellationToken);
    }

    public async Task<int> SaveAsync(SaveClienteCommand command, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return 0;
        }

        await EnsureCompanyAccessAsync(command.TenantId, command.CompanyId, cancellationToken);
        EnsureTenantWriteAccess();

        Validate(command);

        ClienteDetailDto? previous = null;

        var duplicates = await FindDuplicatesAsync(command.TenantId, command.CompanyId, command, cancellationToken);
        var hardConflicts = duplicates
            .Where(item => item.IsHardConflict &&
                           !string.Equals(item.ReviewStatus, ClienteDuplicateReviewStatus.FalsePositive, StringComparison.OrdinalIgnoreCase))
            .ToArray();
        if (hardConflicts.Length > 0)
        {
            var reasons = string.Join(" | ", hardConflicts.Select(item => $"Cliente {item.Code}: {string.Join(", ", item.MatchReasons)}"));
            throw new InvalidOperationException($"Se han encontrado duplicados incompatibles en este centro. {reasons}");
        }

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var moduleContext = await ResolveClientModuleContextAsync(connection, command.TenantId, command.CompanyId, cancellationToken);
        var capabilities = await GetClientCapabilitiesAsync(connection, cancellationToken);

        if (command.Code.HasValue)
        {
            previous = await GetByCodeAsync(command.TenantId, command.CompanyId, command.Code.Value, cancellationToken);
        }

        var targetCenterCode = previous?.CompanyCenterCode ?? moduleContext.ActiveCenterCode;
        var code = command.Code ?? await GetNextCodeAsync(connection, moduleContext.VisibleCenterCodes, cancellationToken);

        await using var dbCommand = connection.CreateCommand();
        dbCommand.CommandText = command.Code.HasValue
            ? $"""
              UPDATE clients
              SET NOM = @name,
                  NIF = @taxId,
                  DOM = @address,
                  CP = @postalCode,
                  POB = @city,
                  PROV = @province,
                  PAIS = @country,
                  CONTACTE = @contactName,
                  TEL = @phone,
                  FAX = @fax,
                  EMAIL1 = @primaryEmail,
                  EMAIL2 = @secondaryEmail,
                  WEB = @website,
                  NOTES = @notes,
                  BLOQUEADO = @isBlocked,
                  origin = 'local',
                  is_deleted = 0,
                  synced_utc = NULL{(capabilities.HasMesesCompletosColumn ? ",\n                  MESESCOMPLETOS = @mesesCompletos" : string.Empty)}
              WHERE CODI = @code
                AND CENTRO = @centerCode;
              """
            : $"""
              INSERT INTO clients
              (CODI, CENTRO, NOM, NIF, DOM, CP, POB, PROV, PAIS, CONTACTE, TEL, FAX, EMAIL1, EMAIL2, WEB, NOTES, BLOQUEADO, origin, is_deleted, synced_utc{(capabilities.HasMesesCompletosColumn ? ", MESESCOMPLETOS" : string.Empty)})
              VALUES
              (@code, @centerCode, @name, @taxId, @address, @postalCode, @city, @province, @country, @contactName, @phone, @fax, @primaryEmail, @secondaryEmail, @website, @notes, @isBlocked, 'local', 0, NULL{(capabilities.HasMesesCompletosColumn ? ", @mesesCompletos" : string.Empty)});
              """;

        dbCommand.Parameters.AddWithValue("@code", code);
        dbCommand.Parameters.AddWithValue("@centerCode", targetCenterCode);
        dbCommand.Parameters.AddWithValue("@name", command.Name.Trim());
        dbCommand.Parameters.AddWithValue("@taxId", command.TaxId.Trim());
        dbCommand.Parameters.AddWithValue("@address", command.Address.Trim());
        dbCommand.Parameters.AddWithValue("@postalCode", command.PostalCode.Trim());
        dbCommand.Parameters.AddWithValue("@city", command.City.Trim());
        dbCommand.Parameters.AddWithValue("@province", command.Province.Trim());
        dbCommand.Parameters.AddWithValue("@country", command.Country.Trim());
        dbCommand.Parameters.AddWithValue("@contactName", command.ContactName.Trim());
        dbCommand.Parameters.AddWithValue("@phone", command.Phone.Trim());
        dbCommand.Parameters.AddWithValue("@fax", command.Fax.Trim());
        dbCommand.Parameters.AddWithValue("@primaryEmail", command.PrimaryEmail.Trim());
        dbCommand.Parameters.AddWithValue("@secondaryEmail", command.SecondaryEmail.Trim());
        dbCommand.Parameters.AddWithValue("@website", command.Website.Trim());
        dbCommand.Parameters.AddWithValue("@notes", command.Notes.Trim());
        dbCommand.Parameters.AddWithValue("@isBlocked", command.IsBlocked);
        if (capabilities.HasMesesCompletosColumn)
        {
            dbCommand.Parameters.AddWithValue("@mesesCompletos", false);
        }

        await dbCommand.ExecuteNonQueryAsync(cancellationToken);
        await SyncSecondaryAddressesAsync(connection, targetCenterCode, code, command.SecondaryAddresses, cancellationToken);
        await SyncContactsAsync(connection, command.TenantId, moduleContext.ScopeCompanyId, code, command.Contacts, cancellationToken);
        await WriteAuditEntryAsync(command, targetCenterCode, code, previous, cancellationToken);
        return code;
    }

    public async Task DeleteAsync(Guid tenantId, Guid companyId, int code, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return;
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);
        EnsureTenantWriteAccess();

        var previous = await GetByCodeAsync(tenantId, companyId, code, cancellationToken)
            ?? throw new InvalidOperationException("No se ha encontrado el cliente que intentas eliminar.");

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);

        await using (var command = connection.CreateCommand())
        {
            command.CommandText =
                """
                UPDATE clients
                SET is_deleted = 1,
                    origin = 'local',
                    synced_utc = NULL
                WHERE CODI = @code
                  AND CENTRO = @centerCode;
                """;
            command.Parameters.AddWithValue("@code", code);
            command.Parameters.AddWithValue("@centerCode", previous.CompanyCenterCode);
            await command.ExecuteNonQueryAsync(cancellationToken);
        }

        await using (var addressCommand = connection.CreateCommand())
        {
            addressCommand.CommandText =
                """
                DELETE FROM adres
                WHERE CODI = @code
                  AND CENTRO = @centerCode;
                """;
            addressCommand.Parameters.AddWithValue("@code", code);
            addressCommand.Parameters.AddWithValue("@centerCode", previous.CompanyCenterCode);
            await addressCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        await using (var contactCommand = connection.CreateCommand())
        {
            contactCommand.CommandText =
                """
                DELETE FROM client_contacts
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND client_code = @code;
                """;
            contactCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            contactCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
            contactCommand.Parameters.AddWithValue("@code", code);
            await contactCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = tenantId,
            CompanyId = companyId,
            Action = "ClienteDeleted",
            EntityName = "Cliente",
            EntityId = code.ToString(),
            Details = $"Codigo={code}; Nombre={previous.Name}; Centro={previous.CompanyCenterCode}"
        }, cancellationToken);
    }

    private async Task WriteAuditEntryAsync(
        SaveClienteCommand command,
        string centerCode,
        int code,
        ClienteDetailDto? previous,
        CancellationToken cancellationToken)
    {
        var current = ToDetail(command, code, centerCode);
        var changes = DescribeChanges(previous, current);
        var isCreate = previous is null;
        var previousBlocked = previous?.IsBlocked ?? false;
        var action = isCreate
            ? "ClienteCreated"
            : previousBlocked != current.IsBlocked
                ? (current.IsBlocked ? "ClienteBlocked" : "ClienteUnblocked")
                : "ClienteUpdated";

        if (!isCreate && changes.Count == 0)
        {
            return;
        }

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = command.TenantId,
            CompanyId = command.CompanyId,
            Action = action,
            EntityName = "Cliente",
            EntityId = code.ToString(),
            Details = changes.Count == 0
                ? $"Codigo={code}; Nombre={current.Name}; Centro={centerCode}"
                : string.Join("; ", changes)
        }, cancellationToken);
    }

    private async Task<string> ResolveCompanyCenterCodeAsync(Guid tenantId, Guid companyId, CancellationToken cancellationToken)
    {
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        return await ResolveCompanyCenterCodeAsync(connection, tenantId, companyId, cancellationToken);
    }

    private static async Task<string> ResolveCompanyCenterCodeAsync(MySqlConnection connection, Guid tenantId, Guid companyId, CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT legacy_center_code
            FROM companies
            WHERE id = @companyId
              AND tenant_id = @tenantId
              AND is_active = 1
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        var scalar = await command.ExecuteScalarAsync(cancellationToken);

        var centerCode = Convert.ToString(scalar)?.Trim();
        if (string.IsNullOrWhiteSpace(centerCode))
        {
            throw new InvalidOperationException("The selected company is not active or is not linked to a legacy center.");
        }

        return centerCode;
    }

    private static async Task<string> GetClientesDataScopeAsync(MySqlConnection connection, Guid tenantId, CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT data_scope
            FROM tenant_module_settings
            WHERE tenant_id = @tenantId
              AND module_key = @moduleKey
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@moduleKey", TenantModuleKeys.CrmClients);

        var scalar = await command.ExecuteScalarAsync(cancellationToken);
        var dataScope = Convert.ToString(scalar)?.Trim();
        return TenantModuleDataScopes.All.Contains(dataScope ?? string.Empty, StringComparer.OrdinalIgnoreCase)
            ? dataScope!
            : TenantModuleDataScopes.Company;
    }

    private static async Task<IReadOnlyCollection<string>> GetTenantCenterCodesAsync(MySqlConnection connection, Guid tenantId, CancellationToken cancellationToken)
    {
        var centers = new List<string>();

        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT legacy_center_code
            FROM companies
            WHERE tenant_id = @tenantId
              AND is_active = 1
            ORDER BY name, legacy_center_code;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var centerCode = reader.GetStringOrEmpty("legacy_center_code");
            if (!string.IsNullOrWhiteSpace(centerCode))
            {
                centers.Add(centerCode);
            }
        }

        return centers
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();
    }

    private static async Task<ClientModuleContext> ResolveClientModuleContextAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        CancellationToken cancellationToken)
    {
        var activeCenterCode = await ResolveCompanyCenterCodeAsync(connection, tenantId, companyId, cancellationToken);
        var dataScope = await GetClientesDataScopeAsync(connection, tenantId, cancellationToken);
        var isTenantShared = string.Equals(dataScope, TenantModuleDataScopes.TenantShared, StringComparison.OrdinalIgnoreCase);
        var visibleCenterCodes = isTenantShared
            ? await GetTenantCenterCodesAsync(connection, tenantId, cancellationToken)
            : [activeCenterCode];

        if (visibleCenterCodes.Count == 0)
        {
            visibleCenterCodes = [activeCenterCode];
        }

        return new ClientModuleContext(
            isTenantShared,
            activeCenterCode,
            isTenantShared ? tenantId : companyId,
            visibleCenterCodes.Select(code => code.Trim()).Where(code => !string.IsNullOrWhiteSpace(code)).Distinct(StringComparer.OrdinalIgnoreCase).ToArray());
    }

    private void EnsureTenantWriteAccess()
    {
        if (!_currentUserContext.IsAuthenticated)
        {
            throw new InvalidOperationException("Debes iniciar sesión para modificar datos.");
        }

        if (_currentUserContext.IsPlatformAdmin ||
            _currentUserContext.Roles.Contains(PlatformRoles.TenantAdmin, StringComparer.OrdinalIgnoreCase))
        {
            return;
        }

        throw new InvalidOperationException("No tienes permisos de escritura en este tenant.");
    }

    private async Task EnsureCompanyAccessAsync(Guid tenantId, Guid companyId, CancellationToken cancellationToken)
    {
        if (!_currentUserContext.IsAuthenticated || !_currentUserContext.UserId.HasValue)
        {
            throw new InvalidOperationException("Debes iniciar sesión para acceder a esta empresa.");
        }

        if (!_tenantContext.TenantId.HasValue || _tenantContext.TenantId.Value != tenantId)
        {
            throw new InvalidOperationException("El tenant solicitado no coincide con tu sesión activa.");
        }

        if (!_activeCompanyContext.CompanyId.HasValue || _activeCompanyContext.CompanyId.Value != companyId)
        {
            throw new InvalidOperationException("La empresa activa no coincide con la empresa solicitada.");
        }

        var allowedCompanies = await _companyAccessService.GetAllowedCompaniesAsync(
            _currentUserContext.UserId.Value,
            tenantId,
            cancellationToken);

        if (!allowedCompanies.Any(company => company.CompanyId == companyId))
        {
            throw new InvalidOperationException("No tienes acceso a la empresa activa.");
        }
    }

    private static async Task<int> GetNextCodeAsync(MySqlConnection connection, IReadOnlyCollection<string> centerCodes, CancellationToken cancellationToken)
    {
        var parameterNames = centerCodes
            .Select((_, index) => $"@nextCenter{index}")
            .ToArray();

        await using var command = connection.CreateCommand();
        command.CommandText =
            $"""
            SELECT COALESCE(MAX(CODI), 0) + 1
            FROM clients
            WHERE CENTRO IN ({string.Join(", ", parameterNames)});
            """;
        AddCenterParameters(command, parameterNames, centerCodes);
        return Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));
    }

    private static async Task<ClientColumnCapabilities> GetClientCapabilitiesAsync(MySqlConnection connection, CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT COLUMN_NAME
            FROM INFORMATION_SCHEMA.COLUMNS
            WHERE TABLE_SCHEMA = DATABASE()
              AND TABLE_NAME = 'clients'
              AND COLUMN_NAME IN ('MESESCOMPLETOS');
            """;

        var names = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            names.Add(reader.GetStringOrEmpty("COLUMN_NAME"));
        }

        return new ClientColumnCapabilities(
            HasMesesCompletosColumn: names.Contains("MESESCOMPLETOS"));
    }

    private static string BuildSearchOrderByClause(ClienteFilter filter)
    {
        var column = filter.SortColumn switch
        {
            nameof(ClienteListItemDto.Code) => "c.CODI",
            nameof(ClienteListItemDto.Name) => "c.NOM",
            nameof(ClienteListItemDto.TaxId) => "c.NIF",
            nameof(ClienteListItemDto.City) => "c.POB",
            nameof(ClienteListItemDto.Email) => "c.EMAIL1",
            nameof(ClienteListItemDto.Phone) => "c.TEL",
            "Status" => "COALESCE(c.BLOQUEADO, 0)",
            _ => string.Empty
        };

        if (string.IsNullOrWhiteSpace(column))
        {
            return string.IsNullOrWhiteSpace(filter.Search)
                ? "ORDER BY c.CODI DESC"
                : "ORDER BY c.NOM, c.CODI DESC";
        }

        var direction = filter.SortDescending ? "DESC" : "ASC";
        return $"ORDER BY {column} {direction}, c.CODI DESC";
    }

    private static async Task LoadDuplicateFlagsAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid scopeCompanyId,
        IReadOnlyCollection<string> visibleCenterCodes,
        IReadOnlyList<string> duplicateCenterParameterNames,
        bool isTenantShared,
        List<ClienteListItemDto> items,
        CancellationToken cancellationToken)
    {
        if (items.Count == 0)
        {
            return;
        }

        var codeParameterNames = items
            .Select((_, index) => $"@pageCode{index}")
            .ToArray();
        var keyLookup = items.ToDictionary(
            item => GetClientItemKey(item.CompanyCenterCode, item.Code),
            StringComparer.OrdinalIgnoreCase);
        var duplicateCenterFilter = isTenantShared
            ? $"dup.CENTRO IN ({string.Join(", ", duplicateCenterParameterNames)}) AND COALESCE(dup.is_deleted, 0) = 0"
            : "dup.CENTRO = c.CENTRO AND COALESCE(dup.is_deleted, 0) = 0";

        await using var command = connection.CreateCommand();
        command.CommandText =
            $"""
            SELECT
                c.CENTRO,
                c.CODI,
                MAX(CASE
                    WHEN UPPER(TRIM(COALESCE(c.NIF, ''))) <> ''
                     AND UPPER(TRIM(COALESCE(dup.NIF, ''))) = UPPER(TRIM(COALESCE(c.NIF, '')))
                    THEN 1 ELSE 0 END) AS has_duplicate_tax_id,
                MAX(CASE
                    WHEN LOWER(TRIM(COALESCE(c.EMAIL1, ''))) <> ''
                     AND (
                            LOWER(TRIM(COALESCE(dup.EMAIL1, ''))) = LOWER(TRIM(COALESCE(c.EMAIL1, '')))
                         OR LOWER(TRIM(COALESCE(dup.EMAIL2, ''))) = LOWER(TRIM(COALESCE(c.EMAIL1, '')))
                         )
                    THEN 1 ELSE 0 END) AS has_duplicate_email,
                MAX(CASE
                    WHEN UPPER(TRIM(COALESCE(c.NOM, ''))) <> ''
                     AND UPPER(TRIM(COALESCE(dup.NOM, ''))) = UPPER(TRIM(COALESCE(c.NOM, '')))
                    THEN 1 ELSE 0 END) AS has_duplicate_name,
                MAX(CASE
                    WHEN TRIM(COALESCE(c.TEL, '')) <> ''
                     AND TRIM(COALESCE(dup.TEL, '')) = TRIM(COALESCE(c.TEL, ''))
                    THEN 1 ELSE 0 END) AS has_duplicate_phone
            FROM clients c
            LEFT JOIN clients dup
              ON dup.CODI <> c.CODI
             AND {duplicateCenterFilter}
             AND (
                    (UPPER(TRIM(COALESCE(c.NIF, ''))) <> '' AND UPPER(TRIM(COALESCE(dup.NIF, ''))) = UPPER(TRIM(COALESCE(c.NIF, ''))))
                 OR (LOWER(TRIM(COALESCE(c.EMAIL1, ''))) <> '' AND (
                        LOWER(TRIM(COALESCE(dup.EMAIL1, ''))) = LOWER(TRIM(COALESCE(c.EMAIL1, '')))
                     OR LOWER(TRIM(COALESCE(dup.EMAIL2, ''))) = LOWER(TRIM(COALESCE(c.EMAIL1, '')))
                    ))
                 OR (UPPER(TRIM(COALESCE(c.NOM, ''))) <> '' AND UPPER(TRIM(COALESCE(dup.NOM, ''))) = UPPER(TRIM(COALESCE(c.NOM, ''))))
                 OR (TRIM(COALESCE(c.TEL, '')) <> '' AND TRIM(COALESCE(dup.TEL, '')) = TRIM(COALESCE(c.TEL, '')))
                 )
            LEFT JOIN client_duplicate_reviews dr
              ON dr.tenant_id = @tenantId
             AND dr.company_id = @scopeCompanyId
             AND dr.left_client_code = LEAST(c.CODI, dup.CODI)
             AND dr.right_client_code = GREATEST(c.CODI, dup.CODI)
             AND dr.status = @falsePositiveStatus
            WHERE c.CODI IN ({string.Join(", ", codeParameterNames)})
              AND c.CENTRO IN ({string.Join(", ", duplicateCenterParameterNames)})
              AND COALESCE(c.is_deleted, 0) = 0
              AND (dup.CODI IS NULL OR dr.left_client_code IS NULL)
            GROUP BY c.CENTRO, c.CODI;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@scopeCompanyId", scopeCompanyId.ToString());
        command.Parameters.AddWithValue("@falsePositiveStatus", ClienteDuplicateReviewStatus.FalsePositive);
        AddCenterParameters(command, duplicateCenterParameterNames, visibleCenterCodes);

        for (var index = 0; index < items.Count; index++)
        {
            command.Parameters.AddWithValue(codeParameterNames[index], items[index].Code);
        }

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var key = GetClientItemKey(
                reader.GetStringOrEmpty("CENTRO"),
                reader.GetInt32(reader.GetOrdinal("CODI")));

            if (!keyLookup.TryGetValue(key, out var item))
            {
                continue;
            }

            item.HasDuplicateTaxId = reader.GetBooleanValue("has_duplicate_tax_id");
            item.HasDuplicateEmail = reader.GetBooleanValue("has_duplicate_email");
            item.HasDuplicateName = reader.GetBooleanValue("has_duplicate_name");
            item.HasDuplicatePhone = reader.GetBooleanValue("has_duplicate_phone");
        }
    }

    private static void AddCenterParameters(
        MySqlCommand command,
        IReadOnlyList<string> parameterNames,
        IReadOnlyCollection<string> centerCodes)
    {
        var index = 0;
        foreach (var centerCode in centerCodes)
        {
            command.Parameters.AddWithValue(parameterNames[index++], centerCode);
        }
    }

    private static void Validate(SaveClienteCommand command)
    {
        command.Name = command.Name.Trim();
        command.TaxId = NormalizeTaxId(command.TaxId);
        command.Address = command.Address.Trim();
        command.PostalCode = command.PostalCode.Trim();
        command.City = command.City.Trim();
        command.Province = command.Province.Trim();
        command.Country = command.Country.Trim();
        command.ContactName = command.ContactName.Trim();
        command.Phone = NormalizePhone(command.Phone);
        command.Fax = NormalizePhone(command.Fax);
        command.PrimaryEmail = NormalizeEmail(command.PrimaryEmail);
        command.SecondaryEmail = NormalizeEmail(command.SecondaryEmail);
        command.Website = command.Website.Trim();
        command.Notes = command.Notes.Trim();
        command.SecondaryAddresses = command.SecondaryAddresses
            .Select(NormalizeSecondaryAddress)
            .Where(address => !IsSecondaryAddressEmpty(address))
            .ToList();
        command.Contacts = command.Contacts
            .Select(NormalizeContact)
            .Where(contact => !IsContactEmpty(contact))
            .ToList();

        if (string.IsNullOrWhiteSpace(command.Name))
        {
            throw new InvalidOperationException("El nombre del cliente es obligatorio.");
        }

        if (command.Name.Length < 3)
        {
            throw new InvalidOperationException("El nombre del cliente debe tener al menos 3 caracteres.");
        }

        if (command.TaxId.Length > 12)
        {
            throw new InvalidOperationException("El NIF no puede superar 12 caracteres.");
        }

        if (!string.IsNullOrWhiteSpace(command.TaxId) && !command.TaxId.All(character => char.IsLetterOrDigit(character) || character is '-' or '/'))
        {
            throw new InvalidOperationException("El NIF contiene caracteres no permitidos.");
        }

        ValidateEmail(command.PrimaryEmail, "El email principal no es válido.");
        ValidateEmail(command.SecondaryEmail, "El email secundario no es válido.");

        if (!string.IsNullOrWhiteSpace(command.PrimaryEmail) &&
            string.Equals(command.PrimaryEmail, command.SecondaryEmail, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("El email principal y el secundario no pueden ser iguales.");
        }

        var duplicateAddresses = command.SecondaryAddresses
            .GroupBy(address => address.Address, StringComparer.OrdinalIgnoreCase)
            .Where(group => group.Count() > 1)
            .Select(group => group.Key)
            .ToArray();
        if (duplicateAddresses.Length > 0)
        {
            throw new InvalidOperationException("Las direcciones secundarias no pueden repetir la misma dirección.");
        }

        if (command.SecondaryAddresses.Any(address => string.IsNullOrWhiteSpace(address.Address)))
        {
            throw new InvalidOperationException("Cada dirección secundaria debe tener dirección.");
        }

        foreach (var contact in command.Contacts)
        {
            if (string.IsNullOrWhiteSpace(contact.Name))
            {
                throw new InvalidOperationException("Cada contacto debe tener nombre.");
            }

            ValidateEmail(contact.Email, $"El email del contacto '{contact.Name}' no es válido.");
        }

        if (command.Contacts.Count(contact => contact.IsPrimary) > 1)
        {
            throw new InvalidOperationException("Solo puede haber un contacto principal por cliente.");
        }

        var duplicateContactEmails = command.Contacts
            .Where(contact => !string.IsNullOrWhiteSpace(contact.Email))
            .GroupBy(contact => contact.Email, StringComparer.OrdinalIgnoreCase)
            .Where(group => group.Count() > 1)
            .Select(group => group.Key)
            .ToArray();
        if (duplicateContactEmails.Length > 0)
        {
            throw new InvalidOperationException("No puede haber dos contactos con el mismo email.");
        }
    }

    private static ClienteDetailDto ToDetail(SaveClienteCommand command, int code, string centerCode) =>
        new()
        {
            Code = code,
            CompanyCenterCode = centerCode,
            Name = command.Name.Trim(),
            TaxId = command.TaxId.Trim(),
            Address = command.Address.Trim(),
            PostalCode = command.PostalCode.Trim(),
            City = command.City.Trim(),
            Province = command.Province.Trim(),
            Country = command.Country.Trim(),
            ContactName = command.ContactName.Trim(),
            Phone = command.Phone.Trim(),
            Fax = command.Fax.Trim(),
            PrimaryEmail = command.PrimaryEmail.Trim(),
            SecondaryEmail = command.SecondaryEmail.Trim(),
            Website = command.Website.Trim(),
            Notes = command.Notes.Trim(),
            IsBlocked = command.IsBlocked,
            SecondaryAddresses = command.SecondaryAddresses
                .Select(NormalizeSecondaryAddress)
                .ToList(),
            Contacts = command.Contacts
                .Select(NormalizeContact)
                .ToList()
        };

    private static List<string> DescribeChanges(ClienteDetailDto? previous, ClienteDetailDto current)
    {
        var changes = new List<string>();
        if (previous is null)
        {
            changes.Add($"Codigo={current.Code}");
            changes.Add($"Nombre={current.Name}");
            changes.Add($"Centro={current.CompanyCenterCode}");
            if (!string.IsNullOrWhiteSpace(current.TaxId))
            {
                changes.Add($"NIF={current.TaxId}");
            }

            if (!string.IsNullOrWhiteSpace(current.City))
            {
                changes.Add($"Ciudad={current.City}");
            }

            changes.Add($"Bloqueado={current.IsBlocked}");
            return changes;
        }

        AppendChange(changes, "Nombre", previous.Name, current.Name);
        AppendChange(changes, "NIF", previous.TaxId, current.TaxId);
        AppendChange(changes, "Direccion", previous.Address, current.Address);
        AppendChange(changes, "CP", previous.PostalCode, current.PostalCode);
        AppendChange(changes, "Ciudad", previous.City, current.City);
        AppendChange(changes, "Provincia", previous.Province, current.Province);
        AppendChange(changes, "Pais", previous.Country, current.Country);
        AppendChange(changes, "Contacto", previous.ContactName, current.ContactName);
        AppendChange(changes, "Telefono", previous.Phone, current.Phone);
        AppendChange(changes, "Fax", previous.Fax, current.Fax);
        AppendChange(changes, "Email1", previous.PrimaryEmail, current.PrimaryEmail);
        AppendChange(changes, "Email2", previous.SecondaryEmail, current.SecondaryEmail);
        AppendChange(changes, "Web", previous.Website, current.Website);
        AppendChange(changes, "Notas", previous.Notes, current.Notes);
        AppendSecondaryAddressChanges(changes, previous.SecondaryAddresses, current.SecondaryAddresses);
        AppendContactChanges(changes, previous.Contacts, current.Contacts);

        if (previous.IsBlocked != current.IsBlocked)
        {
            changes.Add($"Bloqueado: {previous.IsBlocked} -> {current.IsBlocked}");
        }

        return changes;
    }

    private static void AppendChange(List<string> changes, string label, string previous, string current)
    {
        var previousValue = previous.Trim();
        var currentValue = current.Trim();
        if (string.Equals(previousValue, currentValue, StringComparison.Ordinal))
        {
            return;
        }

        changes.Add($"{label}: '{previousValue}' -> '{currentValue}'");
    }

    private static List<string> BuildDuplicateReasons(SaveClienteCommand command, ClienteDetailDto candidate)
    {
        var reasons = new List<string>();

        var normalizedTaxId = NormalizeTaxId(command.TaxId);
        if (!string.IsNullOrWhiteSpace(normalizedTaxId) &&
            string.Equals(normalizedTaxId, NormalizeTaxId(candidate.TaxId), StringComparison.Ordinal))
        {
            reasons.Add("Mismo NIF");
        }

        var normalizedPrimaryEmail = NormalizeEmail(command.PrimaryEmail);
        if (!string.IsNullOrWhiteSpace(normalizedPrimaryEmail) &&
            MatchesEmail(candidate, normalizedPrimaryEmail))
        {
            reasons.Add("Mismo email principal");
        }

        var normalizedSecondaryEmail = NormalizeEmail(command.SecondaryEmail);
        if (!string.IsNullOrWhiteSpace(normalizedSecondaryEmail) &&
            MatchesEmail(candidate, normalizedSecondaryEmail))
        {
            reasons.Add("Mismo email secundario");
        }

        var normalizedName = NormalizeName(command.Name);
        if (!string.IsNullOrWhiteSpace(normalizedName) &&
            string.Equals(normalizedName, NormalizeName(candidate.Name), StringComparison.Ordinal))
        {
            reasons.Add(string.Equals(command.City.Trim(), candidate.City.Trim(), StringComparison.OrdinalIgnoreCase)
                ? "Mismo nombre y ciudad"
                : "Mismo nombre");
        }

        var normalizedPhone = NormalizePhone(command.Phone);
        if (!string.IsNullOrWhiteSpace(normalizedPhone) &&
            string.Equals(normalizedPhone, NormalizePhone(candidate.Phone), StringComparison.Ordinal))
        {
            reasons.Add("Mismo teléfono");
        }

        return reasons;
    }

    private static List<string> BuildPairReasons(
        string leftTaxId,
        string rightTaxId,
        string leftEmail1,
        string leftEmail2,
        string rightEmail1,
        string rightEmail2,
        string leftPhone,
        string rightPhone,
        string leftName,
        string rightName)
    {
        var reasons = new List<string>();

        if (!string.IsNullOrWhiteSpace(leftTaxId) &&
            string.Equals(NormalizeTaxId(leftTaxId), NormalizeTaxId(rightTaxId), StringComparison.Ordinal))
        {
            reasons.Add("Mismo NIF");
        }

        var sharedEmail = GetSharedEmail(leftEmail1, leftEmail2, rightEmail1, rightEmail2);
        if (!string.IsNullOrWhiteSpace(sharedEmail))
        {
            reasons.Add("Mismo email");
        }

        if (!string.IsNullOrWhiteSpace(leftName) &&
            string.Equals(NormalizeName(leftName), NormalizeName(rightName), StringComparison.Ordinal))
        {
            reasons.Add("Mismo nombre");
        }

        if (!string.IsNullOrWhiteSpace(leftPhone) &&
            string.Equals(NormalizePhone(leftPhone), NormalizePhone(rightPhone), StringComparison.Ordinal))
        {
            reasons.Add("Mismo teléfono");
        }

        return reasons;
    }

    private static bool MatchesEmail(ClienteDetailDto candidate, string normalizedEmail) =>
        string.Equals(normalizedEmail, NormalizeEmail(candidate.PrimaryEmail), StringComparison.Ordinal) ||
        string.Equals(normalizedEmail, NormalizeEmail(candidate.SecondaryEmail), StringComparison.Ordinal);

    private static string GetSharedEmail(string leftEmail1, string leftEmail2, string rightEmail1, string rightEmail2)
    {
        var leftEmails = new[] { NormalizeEmail(leftEmail1), NormalizeEmail(leftEmail2) }
            .Where(value => !string.IsNullOrWhiteSpace(value))
            .ToArray();
        var rightEmails = new[] { NormalizeEmail(rightEmail1), NormalizeEmail(rightEmail2) }
            .Where(value => !string.IsNullOrWhiteSpace(value))
            .ToArray();

        return leftEmails.Intersect(rightEmails, StringComparer.Ordinal).FirstOrDefault() ?? string.Empty;
    }

    private static string GetSharedValue(string leftValue, string rightValue)
    {
        var normalizedLeft = leftValue.Trim();
        var normalizedRight = rightValue.Trim();
        return string.Equals(normalizedLeft, normalizedRight, StringComparison.OrdinalIgnoreCase)
            ? normalizedLeft
            : string.Empty;
    }

    private static string NormalizeName(string value) => value.Trim().ToUpperInvariant();

    private static string NormalizeTaxId(string value) => value.Trim().ToUpperInvariant();

    private static string NormalizeEmail(string value) => value.Trim().ToLowerInvariant();

    private static string NormalizePhone(string value) => value.Trim();

    private static string GetClientItemKey(string centerCode, int code) => $"{centerCode}|{code}";

    private static ClienteSecondaryAddressDto NormalizeSecondaryAddress(ClienteSecondaryAddressDto address) =>
        new()
        {
            OriginalAddress = address.OriginalAddress.Trim(),
            Name = address.Name.Trim(),
            ContactName = address.ContactName.Trim(),
            Address = address.Address.Trim(),
            PostalCode = address.PostalCode.Trim(),
            City = address.City.Trim(),
            Province = address.Province.Trim(),
            Country = address.Country.Trim(),
            Phone = NormalizePhone(address.Phone),
            Fax = NormalizePhone(address.Fax)
        };

    private static ClienteContactDto NormalizeContact(ClienteContactDto contact) =>
        new()
        {
            Id = contact.Id,
            Name = contact.Name.Trim(),
            Role = contact.Role.Trim(),
            Email = NormalizeEmail(contact.Email),
            Phone = NormalizePhone(contact.Phone),
            Notes = contact.Notes.Trim(),
            IsPrimary = contact.IsPrimary
        };

    private static bool IsSecondaryAddressEmpty(ClienteSecondaryAddressDto address) =>
        string.IsNullOrWhiteSpace(address.Name) &&
        string.IsNullOrWhiteSpace(address.ContactName) &&
        string.IsNullOrWhiteSpace(address.Address) &&
        string.IsNullOrWhiteSpace(address.PostalCode) &&
        string.IsNullOrWhiteSpace(address.City) &&
        string.IsNullOrWhiteSpace(address.Province) &&
        string.IsNullOrWhiteSpace(address.Country) &&
        string.IsNullOrWhiteSpace(address.Phone) &&
        string.IsNullOrWhiteSpace(address.Fax);

    private static bool IsContactEmpty(ClienteContactDto contact) =>
        string.IsNullOrWhiteSpace(contact.Name) &&
        string.IsNullOrWhiteSpace(contact.Role) &&
        string.IsNullOrWhiteSpace(contact.Email) &&
        string.IsNullOrWhiteSpace(contact.Phone) &&
        string.IsNullOrWhiteSpace(contact.Notes);

    private static void AppendSecondaryAddressChanges(
        List<string> changes,
        IReadOnlyCollection<ClienteSecondaryAddressDto> previous,
        IReadOnlyCollection<ClienteSecondaryAddressDto> current)
    {
        var previousLines = previous
            .Select(FormatSecondaryAddressAuditLine)
            .OrderBy(value => value, StringComparer.OrdinalIgnoreCase)
            .ToArray();
        var currentLines = current
            .Select(FormatSecondaryAddressAuditLine)
            .OrderBy(value => value, StringComparer.OrdinalIgnoreCase)
            .ToArray();

        if (previousLines.SequenceEqual(currentLines, StringComparer.OrdinalIgnoreCase))
        {
            return;
        }

        changes.Add($"Direcciones secundarias: {previous.Count} -> {current.Count}");

        foreach (var removed in previousLines.Except(currentLines, StringComparer.OrdinalIgnoreCase))
        {
            changes.Add($"Direccion secundaria eliminada: {removed}");
        }

        foreach (var added in currentLines.Except(previousLines, StringComparer.OrdinalIgnoreCase))
        {
            changes.Add($"Direccion secundaria nueva o cambiada: {added}");
        }
    }

    private static void AppendContactChanges(
        List<string> changes,
        IReadOnlyCollection<ClienteContactDto> previous,
        IReadOnlyCollection<ClienteContactDto> current)
    {
        var previousLines = previous
            .Select(FormatContactAuditLine)
            .OrderBy(value => value, StringComparer.OrdinalIgnoreCase)
            .ToArray();
        var currentLines = current
            .Select(FormatContactAuditLine)
            .OrderBy(value => value, StringComparer.OrdinalIgnoreCase)
            .ToArray();

        if (previousLines.SequenceEqual(currentLines, StringComparer.OrdinalIgnoreCase))
        {
            return;
        }

        changes.Add($"Contactos: {previous.Count} -> {current.Count}");

        foreach (var removed in previousLines.Except(currentLines, StringComparer.OrdinalIgnoreCase))
        {
            changes.Add($"Contacto eliminado: {removed}");
        }

        foreach (var added in currentLines.Except(previousLines, StringComparer.OrdinalIgnoreCase))
        {
            changes.Add($"Contacto nuevo o cambiado: {added}");
        }
    }

    private static string FormatSecondaryAddressAuditLine(ClienteSecondaryAddressDto address)
    {
        var parts = new List<string>();

        if (!string.IsNullOrWhiteSpace(address.Name))
        {
            parts.Add(address.Name);
        }

        if (!string.IsNullOrWhiteSpace(address.ContactName))
        {
            parts.Add($"Contacto={address.ContactName}");
        }

        parts.Add($"Direccion={address.Address}");

        if (!string.IsNullOrWhiteSpace(address.City))
        {
            parts.Add($"Ciudad={address.City}");
        }

        if (!string.IsNullOrWhiteSpace(address.Phone))
        {
            parts.Add($"Tel={address.Phone}");
        }

        return string.Join(", ", parts);
    }

    private static string FormatContactAuditLine(ClienteContactDto contact)
    {
        var parts = new List<string> { contact.Name };

        if (!string.IsNullOrWhiteSpace(contact.Role))
        {
            parts.Add($"Cargo={contact.Role}");
        }

        if (!string.IsNullOrWhiteSpace(contact.Email))
        {
            parts.Add($"Email={contact.Email}");
        }

        if (!string.IsNullOrWhiteSpace(contact.Phone))
        {
            parts.Add($"Tel={contact.Phone}");
        }

        if (contact.IsPrimary)
        {
            parts.Add("Principal=True");
        }

        return string.Join(", ", parts);
    }

    private static async Task<IReadOnlyCollection<ClienteSecondaryAddressDto>> GetSecondaryAddressesAsync(
        MySqlConnection connection,
        string centerCode,
        int code,
        CancellationToken cancellationToken)
    {
        var items = new List<ClienteSecondaryAddressDto>();

        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT CODI, CENTRO, NOM, CONTACTE, DOM, CP, POB, PROV, PAIS, TEL, FAX
            FROM adres
            WHERE CODI = @code
              AND CENTRO = @centerCode
            ORDER BY DOM;
            """;
        command.Parameters.AddWithValue("@code", code);
        command.Parameters.AddWithValue("@centerCode", centerCode);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new ClienteSecondaryAddressDto
            {
                OriginalAddress = reader.GetStringOrEmpty("DOM"),
                Name = reader.GetStringOrEmpty("NOM"),
                ContactName = reader.GetStringOrEmpty("CONTACTE"),
                Address = reader.GetStringOrEmpty("DOM"),
                PostalCode = reader.GetStringOrEmpty("CP"),
                City = reader.GetStringOrEmpty("POB"),
                Province = reader.GetStringOrEmpty("PROV"),
                Country = reader.GetStringOrEmpty("PAIS"),
                Phone = reader.GetStringOrEmpty("TEL"),
                Fax = reader.GetStringOrEmpty("FAX")
            });
        }

        return items;
    }

    private static async Task SyncSecondaryAddressesAsync(
        MySqlConnection connection,
        string centerCode,
        int code,
        IReadOnlyCollection<ClienteSecondaryAddressDto> addresses,
        CancellationToken cancellationToken)
    {
        var normalizedAddresses = addresses
            .Select(NormalizeSecondaryAddress)
            .Where(address => !IsSecondaryAddressEmpty(address))
            .ToArray();

        var currentAddresses = await GetSecondaryAddressesAsync(connection, centerCode, code, cancellationToken);
        var currentByOriginal = currentAddresses.ToDictionary(address => address.OriginalAddress, StringComparer.OrdinalIgnoreCase);
        var incomingOriginalKeys = normalizedAddresses
            .Where(address => !string.IsNullOrWhiteSpace(address.OriginalAddress))
            .Select(address => address.OriginalAddress)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        foreach (var existing in currentAddresses.Where(address => !incomingOriginalKeys.Contains(address.OriginalAddress)))
        {
            await using var deleteCommand = connection.CreateCommand();
            deleteCommand.CommandText =
                """
                DELETE FROM adres
                WHERE CODI = @code
                  AND CENTRO = @centerCode
                  AND DOM = @originalAddress;
                """;
            deleteCommand.Parameters.AddWithValue("@code", code);
            deleteCommand.Parameters.AddWithValue("@centerCode", centerCode);
            deleteCommand.Parameters.AddWithValue("@originalAddress", existing.OriginalAddress);
            await deleteCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        foreach (var address in normalizedAddresses)
        {
            if (string.IsNullOrWhiteSpace(address.OriginalAddress) || !currentByOriginal.ContainsKey(address.OriginalAddress))
            {
                await using var insertCommand = connection.CreateCommand();
                insertCommand.CommandText =
                    """
                    INSERT INTO adres
                        (CODI, CENTRO, NOM, CONTACTE, DOM, CP, POB, PROV, PAIS, TEL, FAX)
                    VALUES
                        (@code, @centerCode, @name, @contactName, @address, @postalCode, @city, @province, @country, @phone, @fax);
                    """;
                insertCommand.Parameters.AddWithValue("@code", code);
                insertCommand.Parameters.AddWithValue("@centerCode", centerCode);
                insertCommand.Parameters.AddWithValue("@name", address.Name);
                insertCommand.Parameters.AddWithValue("@contactName", address.ContactName);
                insertCommand.Parameters.AddWithValue("@address", address.Address);
                insertCommand.Parameters.AddWithValue("@postalCode", address.PostalCode);
                insertCommand.Parameters.AddWithValue("@city", address.City);
                insertCommand.Parameters.AddWithValue("@province", address.Province);
                insertCommand.Parameters.AddWithValue("@country", address.Country);
                insertCommand.Parameters.AddWithValue("@phone", address.Phone);
                insertCommand.Parameters.AddWithValue("@fax", address.Fax);
                await insertCommand.ExecuteNonQueryAsync(cancellationToken);
                continue;
            }

            await using var updateCommand = connection.CreateCommand();
            updateCommand.CommandText =
                """
                UPDATE adres
                SET NOM = @name,
                    CONTACTE = @contactName,
                    DOM = @address,
                    CP = @postalCode,
                    POB = @city,
                    PROV = @province,
                    PAIS = @country,
                    TEL = @phone,
                    FAX = @fax
                WHERE CODI = @code
                  AND CENTRO = @centerCode
                  AND DOM = @originalAddress;
                """;
            updateCommand.Parameters.AddWithValue("@code", code);
            updateCommand.Parameters.AddWithValue("@centerCode", centerCode);
            updateCommand.Parameters.AddWithValue("@name", address.Name);
            updateCommand.Parameters.AddWithValue("@contactName", address.ContactName);
            updateCommand.Parameters.AddWithValue("@address", address.Address);
            updateCommand.Parameters.AddWithValue("@postalCode", address.PostalCode);
            updateCommand.Parameters.AddWithValue("@city", address.City);
            updateCommand.Parameters.AddWithValue("@province", address.Province);
            updateCommand.Parameters.AddWithValue("@country", address.Country);
            updateCommand.Parameters.AddWithValue("@phone", address.Phone);
            updateCommand.Parameters.AddWithValue("@fax", address.Fax);
            updateCommand.Parameters.AddWithValue("@originalAddress", address.OriginalAddress);
            await updateCommand.ExecuteNonQueryAsync(cancellationToken);
        }
    }

    private static async Task<IReadOnlyCollection<ClienteContactDto>> GetContactsAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        int code,
        CancellationToken cancellationToken)
    {
        var items = new List<ClienteContactDto>();

        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT id, name, role_name, email, phone, notes, is_primary
            FROM client_contacts
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND client_code = @code
            ORDER BY is_primary DESC, display_order, name;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@code", code);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new ClienteContactDto
            {
                Id = reader.GetGuid("id"),
                Name = reader.GetStringOrEmpty("name"),
                Role = reader.GetStringOrEmpty("role_name"),
                Email = reader.GetStringOrEmpty("email"),
                Phone = reader.GetStringOrEmpty("phone"),
                Notes = reader.GetStringOrEmpty("notes"),
                IsPrimary = reader.GetBooleanValue("is_primary")
            });
        }

        return items;
    }

    private static async Task SyncContactsAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        int code,
        IReadOnlyCollection<ClienteContactDto> contacts,
        CancellationToken cancellationToken)
    {
        var normalizedContacts = contacts
            .Select(NormalizeContact)
            .Where(contact => !IsContactEmpty(contact))
            .ToArray();

        var currentContacts = await GetContactsAsync(connection, tenantId, companyId, code, cancellationToken);
        var currentIds = currentContacts
            .Where(contact => contact.Id.HasValue)
            .Select(contact => contact.Id!.Value)
            .ToHashSet();
        var incomingIds = normalizedContacts
            .Where(contact => contact.Id.HasValue)
            .Select(contact => contact.Id!.Value)
            .ToHashSet();

        foreach (var removedId in currentIds.Except(incomingIds))
        {
            await using var deleteCommand = connection.CreateCommand();
            deleteCommand.CommandText =
                """
                DELETE FROM client_contacts
                WHERE id = @id
                  AND tenant_id = @tenantId
                  AND company_id = @companyId
                  AND client_code = @code;
                """;
            deleteCommand.Parameters.AddWithValue("@id", removedId.ToString());
            deleteCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            deleteCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
            deleteCommand.Parameters.AddWithValue("@code", code);
            await deleteCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        for (var index = 0; index < normalizedContacts.Length; index++)
        {
            var contact = normalizedContacts[index];
            if (!contact.Id.HasValue || !currentIds.Contains(contact.Id.Value))
            {
                var id = contact.Id ?? Guid.NewGuid();
                await using var insertCommand = connection.CreateCommand();
                insertCommand.CommandText =
                    """
                    INSERT INTO client_contacts
                        (id, tenant_id, company_id, client_code, display_order, name, role_name, email, phone, notes, is_primary, created_utc, updated_utc)
                    VALUES
                        (@id, @tenantId, @companyId, @code, @displayOrder, @name, @roleName, @email, @phone, @notes, @isPrimary, @createdUtc, @updatedUtc);
                    """;
                insertCommand.Parameters.AddWithValue("@id", id.ToString());
                insertCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
                insertCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
                insertCommand.Parameters.AddWithValue("@code", code);
                insertCommand.Parameters.AddWithValue("@displayOrder", index);
                insertCommand.Parameters.AddWithValue("@name", contact.Name);
                insertCommand.Parameters.AddWithValue("@roleName", contact.Role);
                insertCommand.Parameters.AddWithValue("@email", contact.Email);
                insertCommand.Parameters.AddWithValue("@phone", contact.Phone);
                insertCommand.Parameters.AddWithValue("@notes", contact.Notes);
                insertCommand.Parameters.AddWithValue("@isPrimary", contact.IsPrimary);
                insertCommand.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
                insertCommand.Parameters.AddWithValue("@updatedUtc", DateTime.UtcNow);
                await insertCommand.ExecuteNonQueryAsync(cancellationToken);
                continue;
            }

            await using var updateCommand = connection.CreateCommand();
            updateCommand.CommandText =
                """
                UPDATE client_contacts
                SET display_order = @displayOrder,
                    name = @name,
                    role_name = @roleName,
                    email = @email,
                    phone = @phone,
                    notes = @notes,
                    is_primary = @isPrimary,
                    updated_utc = @updatedUtc
                WHERE id = @id
                  AND tenant_id = @tenantId
                  AND company_id = @companyId
                  AND client_code = @code;
                """;
            updateCommand.Parameters.AddWithValue("@id", contact.Id!.Value.ToString());
            updateCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            updateCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
            updateCommand.Parameters.AddWithValue("@code", code);
            updateCommand.Parameters.AddWithValue("@displayOrder", index);
            updateCommand.Parameters.AddWithValue("@name", contact.Name);
            updateCommand.Parameters.AddWithValue("@roleName", contact.Role);
            updateCommand.Parameters.AddWithValue("@email", contact.Email);
            updateCommand.Parameters.AddWithValue("@phone", contact.Phone);
            updateCommand.Parameters.AddWithValue("@notes", contact.Notes);
            updateCommand.Parameters.AddWithValue("@isPrimary", contact.IsPrimary);
            updateCommand.Parameters.AddWithValue("@updatedUtc", DateTime.UtcNow);
            await updateCommand.ExecuteNonQueryAsync(cancellationToken);
        }
    }

    private static async Task DeleteDuplicatePairRowIfEmptyAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        int leftCode,
        int rightCode,
        CancellationToken cancellationToken)
    {
        await using var deleteCommand = connection.CreateCommand();
        deleteCommand.CommandText =
            """
            DELETE FROM client_duplicate_reviews
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND left_client_code = @leftCode
              AND right_client_code = @rightCode
              AND COALESCE(status, '') = ''
              AND preferred_client_code IS NULL;
            """;
        deleteCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        deleteCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
        deleteCommand.Parameters.AddWithValue("@leftCode", leftCode);
        deleteCommand.Parameters.AddWithValue("@rightCode", rightCode);
        await deleteCommand.ExecuteNonQueryAsync(cancellationToken);
    }

    private static void ValidateEmail(string email, string errorMessage)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return;
        }

        try
        {
            _ = new MailAddress(email);
        }
        catch (FormatException)
        {
            throw new InvalidOperationException(errorMessage);
        }
    }

    private static (int LeftCode, int RightCode) NormalizeClientPair(int firstCode, int secondCode) =>
        firstCode < secondCode ? (firstCode, secondCode) : (secondCode, firstCode);

    private sealed record ClientModuleContext(
        bool IsTenantShared,
        string ActiveCenterCode,
        Guid ScopeCompanyId,
        IReadOnlyCollection<string> VisibleCenterCodes);

    private sealed record ClientColumnCapabilities(
        bool HasMesesCompletosColumn);
}
