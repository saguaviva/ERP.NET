using System.Net.Mail;
using Erp.Application.Auditing;
using Erp.Application.Companies;
using Erp.Application.Contexts;
using Erp.Application.Talleres;
using Erp.Domain.Common;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Support;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Talleres;

public sealed class MySqlTallerService : ITallerQueries, ITallerService
{
    private readonly MySqlConnectionFactory _connectionFactory;
    private readonly IAuditLogService _auditLogService;
    private readonly ICompanyAccessService _companyAccessService;
    private readonly ICurrentUserContext _currentUserContext;
    private readonly ITenantContext _tenantContext;
    private readonly IActiveCompanyContext _activeCompanyContext;

    public MySqlTallerService(
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

    public async Task<TallerSearchResultDto> SearchAsync(Guid tenantId, Guid companyId, TallerFilter filter, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return new TallerSearchResultDto();
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);
        var centerCode = await ResolveCompanyCenterCodeAsync(tenantId, companyId, cancellationToken);
        var pageSize = Math.Clamp(filter.PageSize, 10, 200);
        var page = Math.Max(filter.Page, 1);
        var offset = (page - 1) * pageSize;
        var search = filter.Search?.Trim() ?? string.Empty;
        var likeSearch = $"%{search}%";

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var countCommand = connection.CreateCommand();
        countCommand.CommandText =
            """
            SELECT COUNT(*)
            FROM tallers
            WHERE CENTRO = @centerCode
              AND is_deleted = 0
              AND (
                    @search = ''
                    OR NOM LIKE @likeSearch
                    OR NIF LIKE @likeSearch
                    OR POB LIKE @likeSearch
                    OR EMAIL1 LIKE @likeSearch
                    OR EMAIL2 LIKE @likeSearch
                    OR TEL LIKE @likeSearch
                    OR TEL2 LIKE @likeSearch
                  );
            """;
        countCommand.Parameters.AddWithValue("@centerCode", centerCode);
        countCommand.Parameters.AddWithValue("@search", search);
        countCommand.Parameters.AddWithValue("@likeSearch", likeSearch);
        var totalCount = Convert.ToInt32(await countCommand.ExecuteScalarAsync(cancellationToken));
        if (totalCount == 0)
        {
            return new TallerSearchResultDto { TotalCount = 0 };
        }

        var items = new List<TallerListItemDto>();
        await using var command = connection.CreateCommand();
        command.CommandText =
            $"""
            SELECT CODI, CENTRO, NOM, NIF, POB, EMAIL1, TEL, TEL2
            FROM tallers
            WHERE CENTRO = @centerCode
              AND is_deleted = 0
              AND (
                    @search = ''
                    OR NOM LIKE @likeSearch
                    OR NIF LIKE @likeSearch
                    OR POB LIKE @likeSearch
                    OR EMAIL1 LIKE @likeSearch
                    OR EMAIL2 LIKE @likeSearch
                    OR TEL LIKE @likeSearch
                    OR TEL2 LIKE @likeSearch
                  )
            {BuildSearchOrderByClause(filter)}
            LIMIT @limit OFFSET @offset;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@search", search);
        command.Parameters.AddWithValue("@likeSearch", likeSearch);
        command.Parameters.AddWithValue("@limit", pageSize);
        command.Parameters.AddWithValue("@offset", offset);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new TallerListItemDto
            {
                Code = reader.GetInt32(reader.GetOrdinal("CODI")),
                CompanyCenterCode = reader.GetStringOrEmpty("CENTRO"),
                Name = reader.GetStringOrEmpty("NOM"),
                TaxId = reader.GetStringOrEmpty("NIF"),
                City = reader.GetStringOrEmpty("POB"),
                PrimaryEmail = reader.GetStringOrEmpty("EMAIL1"),
                Phone = reader.GetStringOrEmpty("TEL"),
                SecondaryPhone = reader.GetStringOrEmpty("TEL2")
            });
        }

        return new TallerSearchResultDto
        {
            Items = items,
            TotalCount = totalCount
        };
    }

    public async Task<TallerDetailDto?> GetByCodeAsync(Guid tenantId, Guid companyId, int code, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return null;
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);
        var centerCode = await ResolveCompanyCenterCodeAsync(tenantId, companyId, cancellationToken);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT CODI, CENTRO, NOM, NIF, DOM, CP, POB, PROV, TEL, TEL2, FAX, EMAIL1, EMAIL2, WEB, PAIS, FORMAT, OBSERV
            FROM tallers
            WHERE CODI = @code
              AND CENTRO = @centerCode
              AND is_deleted = 0
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@code", code);
        command.Parameters.AddWithValue("@centerCode", centerCode);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        return new TallerDetailDto
        {
            Code = reader.GetInt32(reader.GetOrdinal("CODI")),
            CompanyCenterCode = reader.GetStringOrEmpty("CENTRO"),
            Name = reader.GetStringOrEmpty("NOM"),
            TaxId = reader.GetStringOrEmpty("NIF"),
            Address = reader.GetStringOrEmpty("DOM"),
            PostalCode = reader.GetStringOrEmpty("CP"),
            City = reader.GetStringOrEmpty("POB"),
            Province = reader.GetStringOrEmpty("PROV"),
            Phone = reader.GetStringOrEmpty("TEL"),
            SecondaryPhone = reader.GetStringOrEmpty("TEL2"),
            Fax = reader.GetStringOrEmpty("FAX"),
            PrimaryEmail = reader.GetStringOrEmpty("EMAIL1"),
            SecondaryEmail = reader.GetStringOrEmpty("EMAIL2"),
            Website = reader.GetStringOrEmpty("WEB"),
            Country = reader.GetStringOrEmpty("PAIS"),
            Format = reader.GetStringOrEmpty("FORMAT"),
            Notes = reader.GetStringOrEmpty("OBSERV")
        };
    }

    public async Task<int> SaveAsync(SaveTallerCommand command, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return 0;
        }

        await EnsureCompanyAccessAsync(command.TenantId, command.CompanyId, cancellationToken);
        EnsureTenantWriteAccess();
        Validate(command);
        var centerCode = await ResolveCompanyCenterCodeAsync(command.TenantId, command.CompanyId, cancellationToken);
        TallerDetailDto? previous = null;
        if (command.Code.HasValue)
        {
            previous = await GetByCodeAsync(command.TenantId, command.CompanyId, command.Code.Value, cancellationToken);
            if (previous is null)
            {
                throw new InvalidOperationException("No se ha encontrado el taller que intentas modificar.");
            }
        }

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var code = command.Code ?? await GetNextCodeAsync(connection, centerCode, cancellationToken);

        if (command.Code.HasValue)
        {
            await using var updateCommand = connection.CreateCommand();
            updateCommand.CommandText =
                """
                UPDATE tallers
                SET NOM = @name,
                    NIF = @taxId,
                    DOM = @address,
                    CP = @postalCode,
                    POB = @city,
                    PROV = @province,
                    TEL = @phone,
                    TEL2 = @secondaryPhone,
                    FAX = @fax,
                    EMAIL1 = @primaryEmail,
                    EMAIL2 = @secondaryEmail,
                    WEB = @website,
                    PAIS = @country,
                    FORMAT = @format,
                    OBSERV = @notes,
                    origin = 'local',
                    is_deleted = 0,
                    synced_utc = NULL
                WHERE CODI = @code
                  AND CENTRO = @centerCode;
                """;
            FillSaveParameters(updateCommand, centerCode, code, command);
            var affected = await updateCommand.ExecuteNonQueryAsync(cancellationToken);
            if (affected == 0)
            {
                throw new InvalidOperationException("No se ha podido actualizar el taller.");
            }

            await AuditAsync(command.TenantId, command.CompanyId, code, previous!, command, cancellationToken);
            return code;
        }

        await using var insertCommand = connection.CreateCommand();
        insertCommand.CommandText =
            """
            INSERT INTO tallers (CODI, CENTRO, NOM, DOM, POB, CP, PROV, TEL, TEL2, FAX, NIF, OBSERV, FORMAT, WEB, EMAIL1, EMAIL2, PAIS, origin, is_deleted, synced_utc)
            VALUES (@code, @centerCode, @name, @address, @city, @postalCode, @province, @phone, @secondaryPhone, @fax, @taxId, @notes, @format, @website, @primaryEmail, @secondaryEmail, @country, 'local', 0, NULL);
            """;
        FillSaveParameters(insertCommand, centerCode, code, command);
        await insertCommand.ExecuteNonQueryAsync(cancellationToken);

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = command.TenantId,
            CompanyId = command.CompanyId,
            UserId = _currentUserContext.UserId,
            Action = "TallerCreated",
            EntityName = "Taller",
            EntityId = code.ToString(),
            Details = $"Taller {code} creado: {command.Name}"
        }, cancellationToken);

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
        var centerCode = await ResolveCompanyCenterCodeAsync(tenantId, companyId, cancellationToken);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            UPDATE tallers
            SET origin = 'local',
                is_deleted = 1,
                synced_utc = NULL
            WHERE CODI = @code
              AND CENTRO = @centerCode;
            """;
        command.Parameters.AddWithValue("@code", code);
        command.Parameters.AddWithValue("@centerCode", centerCode);
        var affected = await command.ExecuteNonQueryAsync(cancellationToken);
        if (affected == 0)
        {
            throw new InvalidOperationException("No se ha encontrado el taller a eliminar.");
        }

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = tenantId,
            CompanyId = companyId,
            UserId = _currentUserContext.UserId,
            Action = "TallerDeleted",
            EntityName = "Taller",
            EntityId = code.ToString(),
            Details = $"Taller {code} eliminado en local."
        }, cancellationToken);
    }

    private static void FillSaveParameters(MySqlCommand command, string centerCode, int code, SaveTallerCommand model)
    {
        command.Parameters.AddWithValue("@code", code);
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@name", model.Name);
        command.Parameters.AddWithValue("@taxId", DbValue(model.TaxId));
        command.Parameters.AddWithValue("@address", DbValue(model.Address));
        command.Parameters.AddWithValue("@postalCode", DbValue(model.PostalCode));
        command.Parameters.AddWithValue("@city", DbValue(model.City));
        command.Parameters.AddWithValue("@province", DbValue(model.Province));
        command.Parameters.AddWithValue("@phone", DbValue(model.Phone));
        command.Parameters.AddWithValue("@secondaryPhone", DbValue(model.SecondaryPhone));
        command.Parameters.AddWithValue("@fax", DbValue(model.Fax));
        command.Parameters.AddWithValue("@primaryEmail", DbValue(model.PrimaryEmail));
        command.Parameters.AddWithValue("@secondaryEmail", DbValue(model.SecondaryEmail));
        command.Parameters.AddWithValue("@website", DbValue(model.Website));
        command.Parameters.AddWithValue("@country", DbValue(model.Country));
        command.Parameters.AddWithValue("@format", DbValue(model.Format));
        command.Parameters.AddWithValue("@notes", DbValue(model.Notes));
    }

    private async Task AuditAsync(Guid tenantId, Guid companyId, int code, TallerDetailDto previous, SaveTallerCommand current, CancellationToken cancellationToken)
    {
        var changes = new List<string>();
        Compare(changes, "Nombre", previous.Name, current.Name);
        Compare(changes, "NIF", previous.TaxId, current.TaxId);
        Compare(changes, "Dirección", previous.Address, current.Address);
        Compare(changes, "Código postal", previous.PostalCode, current.PostalCode);
        Compare(changes, "Ciudad", previous.City, current.City);
        Compare(changes, "Provincia", previous.Province, current.Province);
        Compare(changes, "Teléfono", previous.Phone, current.Phone);
        Compare(changes, "Teléfono 2", previous.SecondaryPhone, current.SecondaryPhone);
        Compare(changes, "Fax", previous.Fax, current.Fax);
        Compare(changes, "Email 1", previous.PrimaryEmail, current.PrimaryEmail);
        Compare(changes, "Email 2", previous.SecondaryEmail, current.SecondaryEmail);
        Compare(changes, "Web", previous.Website, current.Website);
        Compare(changes, "País", previous.Country, current.Country);
        Compare(changes, "Formato", previous.Format, current.Format);
        Compare(changes, "Notas", previous.Notes, current.Notes);

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = tenantId,
            CompanyId = companyId,
            UserId = _currentUserContext.UserId,
            Action = "TallerUpdated",
            EntityName = "Taller",
            EntityId = code.ToString(),
            Details = changes.Count == 0
                ? $"Taller {code} actualizado sin cambios detectados."
                : $"Taller {code} actualizado: {string.Join("; ", changes)}"
        }, cancellationToken);
    }

    private static void Compare(ICollection<string> changes, string label, string previous, string current)
    {
        var before = previous.Trim();
        var after = current.Trim();
        if (string.Equals(before, after, StringComparison.Ordinal))
        {
            return;
        }

        changes.Add($"{label}: '{before}' -> '{after}'");
    }

    private async Task<string> ResolveCompanyCenterCodeAsync(Guid tenantId, Guid companyId, CancellationToken cancellationToken)
    {
        var allowedCompanies = await _companyAccessService.GetAllowedCompaniesAsync(_currentUserContext.UserId!.Value, tenantId, cancellationToken);
        var company = allowedCompanies.FirstOrDefault(item => item.CompanyId == companyId);
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

        throw new InvalidOperationException("No tienes permisos para editar talleres en este tenant.");
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

        var allowedCompanies = await _companyAccessService.GetAllowedCompaniesAsync(_currentUserContext.UserId.Value, tenantId, cancellationToken);
        if (!allowedCompanies.Any(company => company.CompanyId == companyId))
        {
            throw new InvalidOperationException("No tienes acceso a la empresa activa.");
        }
    }

    private static async Task<int> GetNextCodeAsync(MySqlConnection connection, string centerCode, CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT COALESCE(MAX(CODI), 0) + 1
            FROM tallers
            WHERE CENTRO = @centerCode;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        return Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));
    }

    private static void Validate(SaveTallerCommand command)
    {
        command.Name = command.Name.Trim();
        command.TaxId = command.TaxId.Trim().ToUpperInvariant();
        command.Address = command.Address.Trim();
        command.PostalCode = command.PostalCode.Trim();
        command.City = command.City.Trim();
        command.Province = command.Province.Trim();
        command.Phone = command.Phone.Trim();
        command.SecondaryPhone = command.SecondaryPhone.Trim();
        command.Fax = command.Fax.Trim();
        command.PrimaryEmail = command.PrimaryEmail.Trim();
        command.SecondaryEmail = command.SecondaryEmail.Trim();
        command.Website = command.Website.Trim();
        command.Country = command.Country.Trim();
        command.Format = command.Format.Trim();
        command.Notes = command.Notes.Trim();

        if (string.IsNullOrWhiteSpace(command.Name) || command.Name.Length < 3)
        {
            throw new InvalidOperationException("El nombre del taller es obligatorio y debe tener al menos 3 caracteres.");
        }

        if (command.TaxId.Length > 20)
        {
            throw new InvalidOperationException("El NIF del taller no puede superar 20 caracteres.");
        }

        if (!string.IsNullOrWhiteSpace(command.TaxId) &&
            command.TaxId.Any(character => !(char.IsLetterOrDigit(character) || character is '-' or '/' or '.')))
        {
            throw new InvalidOperationException("El NIF del taller contiene caracteres no permitidos.");
        }

        ValidateEmail(command.PrimaryEmail, "email principal");
        ValidateEmail(command.SecondaryEmail, "email secundario");
    }

    private static void ValidateEmail(string email, string label)
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
            throw new InvalidOperationException($"El {label} del taller no es válido.");
        }
    }

    private static object DbValue(string value) =>
        string.IsNullOrWhiteSpace(value) ? DBNull.Value : value;

    private static string BuildSearchOrderByClause(TallerFilter filter)
    {
        var column = filter.SortColumn switch
        {
            nameof(TallerListItemDto.Code) => "CODI",
            nameof(TallerListItemDto.Name) => "NOM",
            nameof(TallerListItemDto.TaxId) => "NIF",
            nameof(TallerListItemDto.City) => "POB",
            nameof(TallerListItemDto.PrimaryEmail) => "EMAIL1",
            nameof(TallerListItemDto.Phone) => "TEL",
            nameof(TallerListItemDto.SecondaryPhone) => "TEL2",
            _ => string.Empty
        };

        if (string.IsNullOrWhiteSpace(column))
        {
            return string.IsNullOrWhiteSpace(filter.Search)
                ? "ORDER BY CODI DESC"
                : "ORDER BY NOM, CODI DESC";
        }

        var direction = filter.SortDescending ? "DESC" : "ASC";
        return $"ORDER BY {column} {direction}, CODI DESC";
    }
}
