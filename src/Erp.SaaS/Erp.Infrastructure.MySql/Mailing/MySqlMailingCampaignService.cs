using Erp.Application.Auditing;
using Erp.Application.Companies;
using Erp.Application.Contexts;
using Erp.Application.Mailing;
using Erp.Domain.Common;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Support;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Mailing;

public sealed class MySqlMailingCampaignService : IMailingCampaignQueries, IMailingCampaignService
{
    private readonly MySqlConnectionFactory _connectionFactory;
    private readonly IAuditLogService _auditLogService;
    private readonly ICompanyAccessService _companyAccessService;
    private readonly ICurrentUserContext _currentUserContext;
    private readonly ITenantContext _tenantContext;

    public MySqlMailingCampaignService(
        MySqlConnectionFactory connectionFactory,
        IAuditLogService auditLogService,
        ICompanyAccessService companyAccessService,
        ICurrentUserContext currentUserContext,
        ITenantContext tenantContext)
    {
        _connectionFactory = connectionFactory;
        _auditLogService = auditLogService;
        _companyAccessService = companyAccessService;
        _currentUserContext = currentUserContext;
        _tenantContext = tenantContext;
    }

    public async Task<MailingCampaignSearchResultDto> SearchAsync(Guid tenantId, Guid companyId, MailingCampaignFilter filter, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return new MailingCampaignSearchResultDto();
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);

        var pageSize = Math.Clamp(filter.PageSize, 10, 200);
        var page = Math.Max(filter.Page, 1);
        var offset = (page - 1) * pageSize;
        var search = filter.Search?.Trim() ?? string.Empty;
        var likeSearch = $"%{search}%";
        var sourceType = MailingSourceTypes.Normalize(filter.SourceType);
        var filterBySource = !string.IsNullOrWhiteSpace(filter.SourceType);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);

        await using var countCommand = connection.CreateCommand();
        countCommand.CommandText =
            """
            SELECT COUNT(*)
            FROM mailing_campaigns
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND is_deleted = 0
              AND (@filterBySource = 0 OR source_type = @sourceType)
              AND (
                    @search = ''
                    OR title LIKE @likeSearch
                    OR body_text LIKE @likeSearch
                    OR notes LIKE @likeSearch
                  );
            """;
        countCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        countCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
        countCommand.Parameters.AddWithValue("@filterBySource", filterBySource);
        countCommand.Parameters.AddWithValue("@sourceType", sourceType);
        countCommand.Parameters.AddWithValue("@search", search);
        countCommand.Parameters.AddWithValue("@likeSearch", likeSearch);

        var totalCount = Convert.ToInt32(await countCommand.ExecuteScalarAsync(cancellationToken));
        if (totalCount == 0)
        {
            return new MailingCampaignSearchResultDto { TotalCount = 0 };
        }

        var items = new List<MailingCampaignListItemDto>();
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT campaign_id,
                   source_type,
                   title,
                   include_all_recipients,
                   recipient_count,
                   created_utc,
                   updated_utc
            FROM mailing_campaigns
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND is_deleted = 0
              AND (@filterBySource = 0 OR source_type = @sourceType)
              AND (
                    @search = ''
                    OR title LIKE @likeSearch
                    OR body_text LIKE @likeSearch
                    OR notes LIKE @likeSearch
                  )
            ORDER BY updated_utc DESC, created_utc DESC
            LIMIT @limit OFFSET @offset;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@filterBySource", filterBySource);
        command.Parameters.AddWithValue("@sourceType", sourceType);
        command.Parameters.AddWithValue("@search", search);
        command.Parameters.AddWithValue("@likeSearch", likeSearch);
        command.Parameters.AddWithValue("@limit", pageSize);
        command.Parameters.AddWithValue("@offset", offset);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new MailingCampaignListItemDto
            {
                CampaignId = reader.GetGuid("campaign_id"),
                SourceType = reader.GetStringOrEmpty("source_type"),
                Title = reader.GetStringOrEmpty("title"),
                IncludeAllRecipients = reader.GetBoolean("include_all_recipients"),
                RecipientCount = reader.GetInt32("recipient_count"),
                CreatedUtc = reader.GetDateTime("created_utc"),
                UpdatedUtc = reader.GetDateTime("updated_utc")
            });
        }

        return new MailingCampaignSearchResultDto
        {
            Items = items,
            TotalCount = totalCount
        };
    }

    public async Task<MailingCampaignDetailDto?> GetByIdAsync(Guid tenantId, Guid companyId, Guid campaignId, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return null;
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT campaign_id,
                   source_type,
                   title,
                   body_text,
                   notes,
                   include_all_recipients,
                   created_utc,
                   updated_utc
            FROM mailing_campaigns
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND campaign_id = @campaignId
              AND is_deleted = 0
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@campaignId", campaignId.ToString());

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        var result = new MailingCampaignDetailDto
        {
            CampaignId = reader.GetGuid("campaign_id"),
            SourceType = reader.GetStringOrEmpty("source_type"),
            Title = reader.GetStringOrEmpty("title"),
            BodyText = reader.GetStringOrEmpty("body_text"),
            Notes = reader.GetStringOrEmpty("notes"),
            IncludeAllRecipients = reader.GetBoolean("include_all_recipients"),
            CreatedUtc = reader.GetDateTime("created_utc"),
            UpdatedUtc = reader.GetDateTime("updated_utc")
        };

        await reader.CloseAsync();
        result.Recipients = await LoadCampaignRecipientsAsync(connection, tenantId, companyId, campaignId, cancellationToken);
        return result;
    }

    public async Task<MailingRecipientSearchResultDto> SearchRecipientsAsync(Guid tenantId, Guid companyId, MailingRecipientFilter filter, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return new MailingRecipientSearchResultDto();
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);
        var centerCode = await ResolveCompanyCenterCodeAsync(tenantId, companyId, cancellationToken);
        var sourceType = MailingSourceTypes.Normalize(filter.SourceType);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        return await SearchSourceRecipientsAsync(connection, centerCode, sourceType, filter.Search, filter.Page, filter.PageSize, cancellationToken);
    }

    public async Task<Guid> SaveAsync(SaveMailingCampaignCommand command, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return command.CampaignId ?? Guid.Empty;
        }

        await EnsureCompanyAccessAsync(command.TenantId, command.CompanyId, cancellationToken);
        EnsureTenantWriteAccess();

        var sourceType = MailingSourceTypes.Normalize(command.SourceType);
        var title = (command.Title ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new InvalidOperationException("El título del mailing es obligatorio.");
        }

        var bodyText = (command.BodyText ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(bodyText))
        {
            throw new InvalidOperationException("El texto del mailing es obligatorio.");
        }

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var centerCode = await ResolveCompanyCenterCodeAsync(command.TenantId, command.CompanyId, cancellationToken);
        var campaignId = command.CampaignId.GetValueOrDefault(Guid.NewGuid());
        var recipientSnapshots = command.IncludeAllRecipients
            ? await LoadAllSourceRecipientsAsync(connection, centerCode, sourceType, cancellationToken)
            : await LoadSelectedSourceRecipientsAsync(connection, centerCode, sourceType, command.RecipientCodes, cancellationToken);

        if (recipientSnapshots.Count == 0)
        {
            throw new InvalidOperationException("No hay destinatarios para guardar este mailing.");
        }

        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);
        var utcNow = DateTime.UtcNow;

        await using (var upsertCommand = connection.CreateCommand())
        {
            upsertCommand.Transaction = transaction;
            upsertCommand.CommandText =
                """
                INSERT INTO mailing_campaigns (
                    campaign_id,
                    tenant_id,
                    company_id,
                    source_type,
                    title,
                    body_text,
                    notes,
                    include_all_recipients,
                    recipient_count,
                    is_deleted,
                    created_utc,
                    updated_utc)
                VALUES (
                    @campaignId,
                    @tenantId,
                    @companyId,
                    @sourceType,
                    @title,
                    @bodyText,
                    @notes,
                    @includeAllRecipients,
                    @recipientCount,
                    0,
                    @createdUtc,
                    @updatedUtc)
                ON DUPLICATE KEY UPDATE
                    source_type = VALUES(source_type),
                    title = VALUES(title),
                    body_text = VALUES(body_text),
                    notes = VALUES(notes),
                    include_all_recipients = VALUES(include_all_recipients),
                    recipient_count = VALUES(recipient_count),
                    is_deleted = 0,
                    updated_utc = VALUES(updated_utc);
                """;
            upsertCommand.Parameters.AddWithValue("@campaignId", campaignId.ToString());
            upsertCommand.Parameters.AddWithValue("@tenantId", command.TenantId.ToString());
            upsertCommand.Parameters.AddWithValue("@companyId", command.CompanyId.ToString());
            upsertCommand.Parameters.AddWithValue("@sourceType", sourceType);
            upsertCommand.Parameters.AddWithValue("@title", title);
            upsertCommand.Parameters.AddWithValue("@bodyText", bodyText);
            upsertCommand.Parameters.AddWithValue("@notes", (command.Notes ?? string.Empty).Trim());
            upsertCommand.Parameters.AddWithValue("@includeAllRecipients", command.IncludeAllRecipients);
            upsertCommand.Parameters.AddWithValue("@recipientCount", recipientSnapshots.Count);
            upsertCommand.Parameters.AddWithValue("@createdUtc", utcNow);
            upsertCommand.Parameters.AddWithValue("@updatedUtc", utcNow);
            await upsertCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        await using (var deleteRecipientsCommand = connection.CreateCommand())
        {
            deleteRecipientsCommand.Transaction = transaction;
            deleteRecipientsCommand.CommandText =
                """
                DELETE FROM mailing_campaign_recipients
                WHERE tenant_id = @tenantId
                  AND company_id = @companyId
                  AND campaign_id = @campaignId;
                """;
            deleteRecipientsCommand.Parameters.AddWithValue("@tenantId", command.TenantId.ToString());
            deleteRecipientsCommand.Parameters.AddWithValue("@companyId", command.CompanyId.ToString());
            deleteRecipientsCommand.Parameters.AddWithValue("@campaignId", campaignId.ToString());
            await deleteRecipientsCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        for (var index = 0; index < recipientSnapshots.Count; index++)
        {
            var recipient = recipientSnapshots[index];
            await using var insertRecipientCommand = connection.CreateCommand();
            insertRecipientCommand.Transaction = transaction;
            insertRecipientCommand.CommandText =
                """
                INSERT INTO mailing_campaign_recipients (
                    campaign_id,
                    tenant_id,
                    company_id,
                    line_number,
                    recipient_code,
                    recipient_name,
                    recipient_tax_id,
                    address_line,
                    postal_code,
                    city,
                    province,
                    email)
                VALUES (
                    @campaignId,
                    @tenantId,
                    @companyId,
                    @lineNumber,
                    @recipientCode,
                    @recipientName,
                    @recipientTaxId,
                    @addressLine,
                    @postalCode,
                    @city,
                    @province,
                    @email);
                """;
            insertRecipientCommand.Parameters.AddWithValue("@campaignId", campaignId.ToString());
            insertRecipientCommand.Parameters.AddWithValue("@tenantId", command.TenantId.ToString());
            insertRecipientCommand.Parameters.AddWithValue("@companyId", command.CompanyId.ToString());
            insertRecipientCommand.Parameters.AddWithValue("@lineNumber", index + 1);
            insertRecipientCommand.Parameters.AddWithValue("@recipientCode", recipient.Code);
            insertRecipientCommand.Parameters.AddWithValue("@recipientName", recipient.Name);
            insertRecipientCommand.Parameters.AddWithValue("@recipientTaxId", recipient.TaxId);
            insertRecipientCommand.Parameters.AddWithValue("@addressLine", recipient.AddressLine);
            insertRecipientCommand.Parameters.AddWithValue("@postalCode", recipient.PostalCode);
            insertRecipientCommand.Parameters.AddWithValue("@city", recipient.City);
            insertRecipientCommand.Parameters.AddWithValue("@province", recipient.Province);
            insertRecipientCommand.Parameters.AddWithValue("@email", recipient.Email);
            await insertRecipientCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        await transaction.CommitAsync(cancellationToken);

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = command.TenantId,
            CompanyId = command.CompanyId,
            UserId = _currentUserContext.UserId,
            Action = command.CampaignId.HasValue ? "MailingCampaignUpdated" : "MailingCampaignCreated",
            EntityName = "MailingCampaign",
            EntityId = campaignId.ToString(),
            Details = $"Title={title}; Source={sourceType}; Recipients={recipientSnapshots.Count}; IncludeAll={command.IncludeAllRecipients}"
        }, cancellationToken);

        return campaignId;
    }

    public async Task DeleteAsync(Guid tenantId, Guid companyId, Guid campaignId, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return;
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);
        EnsureTenantWriteAccess();

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            UPDATE mailing_campaigns
            SET is_deleted = 1,
                updated_utc = @updatedUtc
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND campaign_id = @campaignId;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@campaignId", campaignId.ToString());
        command.Parameters.AddWithValue("@updatedUtc", DateTime.UtcNow);
        await command.ExecuteNonQueryAsync(cancellationToken);

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = tenantId,
            CompanyId = companyId,
            UserId = _currentUserContext.UserId,
            Action = "MailingCampaignDeleted",
            EntityName = "MailingCampaign",
            EntityId = campaignId.ToString(),
            Details = "Deleted"
        }, cancellationToken);
    }

    private async Task<string> ResolveCompanyCenterCodeAsync(Guid tenantId, Guid companyId, CancellationToken cancellationToken)
    {
        var companies = await _companyAccessService.GetAllowedCompaniesAsync(_currentUserContext.UserId!.Value, tenantId, cancellationToken);
        var company = companies.FirstOrDefault(item => item.CompanyId == companyId);
        if (company is null || string.IsNullOrWhiteSpace(company.LegacyCenterCode))
        {
            throw new InvalidOperationException("La empresa activa no tiene centro legacy configurado.");
        }

        return company.LegacyCenterCode.Trim().ToUpperInvariant();
    }

    private void EnsureTenantWriteAccess()
    {
        if (_currentUserContext.IsPlatformAdmin)
        {
            return;
        }

        if (_currentUserContext.Roles.Contains(PlatformRoles.TenantAdmin, StringComparer.OrdinalIgnoreCase))
        {
            return;
        }

        throw new InvalidOperationException("No tienes permisos para editar mailings en este tenant.");
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

        var allowedCompanies = await _companyAccessService.GetAllowedCompaniesAsync(_currentUserContext.UserId.Value, tenantId, cancellationToken);
        if (!allowedCompanies.Any(item => item.CompanyId == companyId))
        {
            throw new InvalidOperationException("No tienes acceso a la empresa solicitada.");
        }
    }

    private static async Task<IReadOnlyList<MailingRecipientDto>> LoadCampaignRecipientsAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        Guid campaignId,
        CancellationToken cancellationToken)
    {
        var items = new List<MailingRecipientDto>();
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT recipient_code,
                   recipient_name,
                   recipient_tax_id,
                   address_line,
                   postal_code,
                   city,
                   province,
                   email
            FROM mailing_campaign_recipients
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND campaign_id = @campaignId
            ORDER BY line_number;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@campaignId", campaignId.ToString());

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(MapRecipient(reader));
        }

        return items;
    }

    private static async Task<List<MailingRecipientDto>> LoadAllSourceRecipientsAsync(
        MySqlConnection connection,
        string centerCode,
        string sourceType,
        CancellationToken cancellationToken)
    {
        var result = await SearchSourceRecipientsAsync(connection, centerCode, sourceType, string.Empty, 1, 5000, cancellationToken);
        return result.Items.ToList();
    }

    private static async Task<List<MailingRecipientDto>> LoadSelectedSourceRecipientsAsync(
        MySqlConnection connection,
        string centerCode,
        string sourceType,
        IReadOnlyList<int> recipientCodes,
        CancellationToken cancellationToken)
    {
        if (recipientCodes.Count == 0)
        {
            return [];
        }

        var codeParameters = recipientCodes.Distinct().OrderBy(code => code).ToArray();
        var parameterNames = codeParameters.Select((_, index) => $"@code{index}").ToArray();
        var (tableName, emailColumn) = ResolveSourceDefinition(sourceType);

        await using var command = connection.CreateCommand();
        command.CommandText =
            $"""
            SELECT CODI,
                   COALESCE(NOM, '') AS NOM,
                   COALESCE(NIF, '') AS NIF,
                   COALESCE(DOM, '') AS DOM,
                   COALESCE(CP, '') AS CP,
                   COALESCE(POB, '') AS POB,
                   COALESCE(PROV, '') AS PROV,
                   COALESCE({emailColumn}, '') AS EMAIL
            FROM {tableName}
            WHERE CENTRO = @centerCode
              AND COALESCE(is_deleted, 0) = 0
              AND CODI IN ({string.Join(", ", parameterNames)})
            ORDER BY NOM, CODI;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        for (var index = 0; index < codeParameters.Length; index++)
        {
            command.Parameters.AddWithValue(parameterNames[index], codeParameters[index]);
        }

        var items = new List<MailingRecipientDto>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(MapRecipient(reader));
        }

        return items;
    }

    private static async Task<MailingRecipientSearchResultDto> SearchSourceRecipientsAsync(
        MySqlConnection connection,
        string centerCode,
        string sourceType,
        string? search,
        int page,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var normalizedSearch = (search ?? string.Empty).Trim();
        var likeSearch = $"%{normalizedSearch}%";
        var currentPage = Math.Max(page, 1);
        var currentPageSize = Math.Clamp(pageSize, 10, 5000);
        var offset = (currentPage - 1) * currentPageSize;
        var (tableName, emailColumn) = ResolveSourceDefinition(sourceType);

        await using var countCommand = connection.CreateCommand();
        countCommand.CommandText =
            $"""
            SELECT COUNT(*)
            FROM {tableName}
            WHERE CENTRO = @centerCode
              AND COALESCE(is_deleted, 0) = 0
              AND (
                    @search = ''
                    OR NOM LIKE @likeSearch
                    OR NIF LIKE @likeSearch
                    OR POB LIKE @likeSearch
                    OR COALESCE({emailColumn}, '') LIKE @likeSearch
                  );
            """;
        countCommand.Parameters.AddWithValue("@centerCode", centerCode);
        countCommand.Parameters.AddWithValue("@search", normalizedSearch);
        countCommand.Parameters.AddWithValue("@likeSearch", likeSearch);

        var totalCount = Convert.ToInt32(await countCommand.ExecuteScalarAsync(cancellationToken));
        if (totalCount == 0)
        {
            return new MailingRecipientSearchResultDto { TotalCount = 0 };
        }

        var items = new List<MailingRecipientDto>();
        await using var command = connection.CreateCommand();
        command.CommandText =
            $"""
            SELECT CODI,
                   COALESCE(NOM, '') AS NOM,
                   COALESCE(NIF, '') AS NIF,
                   COALESCE(DOM, '') AS DOM,
                   COALESCE(CP, '') AS CP,
                   COALESCE(POB, '') AS POB,
                   COALESCE(PROV, '') AS PROV,
                   COALESCE({emailColumn}, '') AS EMAIL
            FROM {tableName}
            WHERE CENTRO = @centerCode
              AND COALESCE(is_deleted, 0) = 0
              AND (
                    @search = ''
                    OR NOM LIKE @likeSearch
                    OR NIF LIKE @likeSearch
                    OR POB LIKE @likeSearch
                    OR COALESCE({emailColumn}, '') LIKE @likeSearch
                  )
            ORDER BY NOM, CODI
            LIMIT @limit OFFSET @offset;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@search", normalizedSearch);
        command.Parameters.AddWithValue("@likeSearch", likeSearch);
        command.Parameters.AddWithValue("@limit", currentPageSize);
        command.Parameters.AddWithValue("@offset", offset);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(MapRecipient(reader));
        }

        return new MailingRecipientSearchResultDto
        {
            Items = items,
            TotalCount = totalCount
        };
    }

    private static (string TableName, string EmailColumn) ResolveSourceDefinition(string sourceType) =>
        MailingSourceTypes.Normalize(sourceType) switch
        {
            MailingSourceTypes.Suppliers => ("prove", "EMAIL1"),
            MailingSourceTypes.Representatives => ("repres", "EMAIL1"),
            MailingSourceTypes.Carriers => ("trans", "EMAIL"),
            _ => ("clients", "EMAIL1")
        };

    private static MailingRecipientDto MapRecipient(MySqlDataReader reader) =>
        new()
        {
            Code = GetInt32ByAlias(reader, "CODI", "recipient_code"),
            Name = GetStringByAlias(reader, "NOM", "recipient_name"),
            TaxId = GetStringByAlias(reader, "NIF", "recipient_tax_id"),
            AddressLine = GetStringByAlias(reader, "DOM", "address_line"),
            PostalCode = GetStringByAlias(reader, "CP", "postal_code"),
            City = GetStringByAlias(reader, "POB", "city"),
            Province = GetStringByAlias(reader, "PROV", "province"),
            Email = GetStringByAlias(reader, "EMAIL", "email")
        };

    private static int GetInt32ByAlias(MySqlDataReader reader, params string[] aliases)
    {
        foreach (var alias in aliases)
        {
            var ordinal = TryGetOrdinal(reader, alias);
            if (ordinal is not null)
            {
                if (reader.IsDBNull(ordinal.Value))
                {
                    return 0;
                }

                return reader.GetInt32(ordinal.Value);
            }
        }

        return 0;
    }

    private static string GetStringByAlias(MySqlDataReader reader, params string[] aliases)
    {
        foreach (var alias in aliases)
        {
            var ordinal = TryGetOrdinal(reader, alias);
            if (ordinal is not null)
            {
                if (reader.IsDBNull(ordinal.Value))
                {
                    return string.Empty;
                }

                return Convert.ToString(reader.GetValue(ordinal.Value)) ?? string.Empty;
            }
        }

        return string.Empty;
    }

    private static int? TryGetOrdinal(MySqlDataReader reader, string columnName)
    {
        for (var index = 0; index < reader.FieldCount; index++)
        {
            if (string.Equals(reader.GetName(index), columnName, StringComparison.OrdinalIgnoreCase))
            {
                return index;
            }
        }

        return null;
    }
}
