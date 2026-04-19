using System.Net.Mail;
using Erp.Application.Auditing;
using Erp.Application.Companies;
using Erp.Application.Contexts;
using Erp.Application.LegacySync;
using Erp.Application.Transportistas;
using Erp.Domain.Common;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Support;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Transportistas;

public sealed class MySqlTransportistaService : ITransportistaQueries, ITransportistaService
{
    private readonly MySqlConnectionFactory _connectionFactory;
    private readonly IAuditLogService _auditLogService;
    private readonly ICompanyAccessService _companyAccessService;
    private readonly ICurrentUserContext _currentUserContext;
    private readonly ITenantContext _tenantContext;
    private readonly IActiveCompanyContext _activeCompanyContext;

    public MySqlTransportistaService(
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

    public async Task<TransportistaSearchResultDto> SearchAsync(Guid tenantId, Guid companyId, TransportistaFilter filter, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return new TransportistaSearchResultDto();
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
            FROM trans
            WHERE CENTRO = @centerCode
              AND is_deleted = 0
              AND (
                    @search = ''
                    OR NOM LIKE @likeSearch
                    OR NIF LIKE @likeSearch
                    OR POB LIKE @likeSearch
                    OR EMAIL LIKE @likeSearch
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
            return new TransportistaSearchResultDto { TotalCount = 0 };
        }

        var items = new List<TransportistaListItemDto>();
        await using var command = connection.CreateCommand();
        command.CommandText =
            $"""
            SELECT CODI, CENTRO, NOM, NIF, POB, EMAIL, TEL, TEL2
            FROM trans
            WHERE CENTRO = @centerCode
              AND is_deleted = 0
              AND (
                    @search = ''
                    OR NOM LIKE @likeSearch
                    OR NIF LIKE @likeSearch
                    OR POB LIKE @likeSearch
                    OR EMAIL LIKE @likeSearch
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
            items.Add(new TransportistaListItemDto
            {
                Code = reader.GetInt32(reader.GetOrdinal("CODI")),
                CompanyCenterCode = reader.GetStringOrEmpty("CENTRO"),
                Name = reader.GetStringOrEmpty("NOM"),
                TaxId = reader.GetStringOrEmpty("NIF"),
                City = reader.GetStringOrEmpty("POB"),
                Email = reader.GetStringOrEmpty("EMAIL"),
                Phone = reader.GetStringOrEmpty("TEL"),
                SecondaryPhone = reader.GetStringOrEmpty("TEL2")
            });
        }

        return new TransportistaSearchResultDto
        {
            Items = items,
            TotalCount = totalCount
        };
    }

    public async Task<TransportistaDetailDto?> GetByCodeAsync(Guid tenantId, Guid companyId, int code, CancellationToken cancellationToken = default)
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
            SELECT CODI, CENTRO, NOM, NIF, DOM, CP, POB, PROV, TEL, TEL2, FAX, EMAIL, WEB, PAIS, FORMAT, OBSERV
            FROM trans
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

        return new TransportistaDetailDto
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
            Email = reader.GetStringOrEmpty("EMAIL"),
            Website = reader.GetStringOrEmpty("WEB"),
            Country = reader.GetStringOrEmpty("PAIS"),
            Format = reader.GetStringOrEmpty("FORMAT"),
            Notes = reader.GetStringOrEmpty("OBSERV")
        };
    }

    public async Task<int> SaveAsync(SaveTransportistaCommand command, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return 0;
        }

        await EnsureCompanyAccessAsync(command.TenantId, command.CompanyId, cancellationToken);
        EnsureTenantWriteAccess();
        Validate(command);
        var centerCode = await ResolveCompanyCenterCodeAsync(command.TenantId, command.CompanyId, cancellationToken);
        TransportistaDetailDto? previous = null;
        if (command.Code.HasValue)
        {
            previous = await GetByCodeAsync(command.TenantId, command.CompanyId, command.Code.Value, cancellationToken);
            if (previous is null)
            {
                throw new InvalidOperationException("No se ha encontrado el transportista que intentas modificar.");
            }
        }

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var code = command.Code ?? await GetNextCodeAsync(connection, centerCode, cancellationToken);

        if (command.Code.HasValue)
        {
            await using var updateCommand = connection.CreateCommand();
            updateCommand.CommandText =
                """
                UPDATE trans
                SET NOM = @name,
                    NIF = @taxId,
                    DOM = @address,
                    CP = @postalCode,
                    POB = @city,
                    PROV = @province,
                    TEL = @phone,
                    TEL2 = @secondaryPhone,
                    FAX = @fax,
                    EMAIL = @email,
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
                throw new InvalidOperationException("No se ha podido actualizar el transportista.");
            }

            await AuditAsync(command.TenantId, command.CompanyId, code, previous!, command, cancellationToken);
            return code;
        }

        await using var insertCommand = connection.CreateCommand();
        insertCommand.CommandText =
            """
            INSERT INTO trans (CODI, TEL2, WEB, NOM, DOM, POB, CP, PROV, TEL, FAX, NIF, OBSERV, FORMAT, EMAIL, CENTRO, PAIS, origin, is_deleted, synced_utc)
            VALUES (@code, @secondaryPhone, @website, @name, @address, @city, @postalCode, @province, @phone, @fax, @taxId, @notes, @format, @email, @centerCode, @country, 'local', 0, NULL);
            """;
        FillSaveParameters(insertCommand, centerCode, code, command);
        await insertCommand.ExecuteNonQueryAsync(cancellationToken);

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = command.TenantId,
            CompanyId = command.CompanyId,
            UserId = _currentUserContext.UserId,
            Action = "TransportistaCreated",
            EntityName = "Transportista",
            EntityId = code.ToString(),
            Details = $"Transportista {code} creado: {command.Name}"
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
            UPDATE trans
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
            throw new InvalidOperationException("No se ha encontrado el transportista a eliminar.");
        }

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = tenantId,
            CompanyId = companyId,
            UserId = _currentUserContext.UserId,
            Action = "TransportistaDeleted",
            EntityName = "Transportista",
            EntityId = code.ToString(),
            Details = $"Transportista {code} eliminado en local."
        }, cancellationToken);
    }

    private static void FillSaveParameters(MySqlCommand command, string centerCode, int code, SaveTransportistaCommand model)
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
        command.Parameters.AddWithValue("@email", DbValue(model.Email));
        command.Parameters.AddWithValue("@website", DbValue(model.Website));
        command.Parameters.AddWithValue("@country", DbValue(model.Country));
        command.Parameters.AddWithValue("@format", DbValue(model.Format));
        command.Parameters.AddWithValue("@notes", DbValue(model.Notes));
    }

    private async Task AuditAsync(Guid tenantId, Guid companyId, int code, TransportistaDetailDto previous, SaveTransportistaCommand current, CancellationToken cancellationToken)
    {
        var changes = new List<string>();
        AddChange(changes, "Nombre", previous.Name, current.Name);
        AddChange(changes, "NIF", previous.TaxId, current.TaxId);
        AddChange(changes, "Dirección", previous.Address, current.Address);
        AddChange(changes, "CP", previous.PostalCode, current.PostalCode);
        AddChange(changes, "Ciudad", previous.City, current.City);
        AddChange(changes, "Provincia", previous.Province, current.Province);
        AddChange(changes, "Teléfono", previous.Phone, current.Phone);
        AddChange(changes, "Teléfono 2", previous.SecondaryPhone, current.SecondaryPhone);
        AddChange(changes, "Fax", previous.Fax, current.Fax);
        AddChange(changes, "Email", previous.Email, current.Email);
        AddChange(changes, "Web", previous.Website, current.Website);
        AddChange(changes, "País", previous.Country, current.Country);
        AddChange(changes, "Formato", previous.Format, current.Format);
        AddChange(changes, "Notas", previous.Notes, current.Notes);

        if (changes.Count == 0)
        {
            return;
        }

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = tenantId,
            CompanyId = companyId,
            UserId = _currentUserContext.UserId,
            Action = "TransportistaUpdated",
            EntityName = "Transportista",
            EntityId = code.ToString(),
            Details = string.Join(" | ", changes)
        }, cancellationToken);
    }

    private static void AddChange(ICollection<string> changes, string label, string previous, string current)
    {
        if (string.Equals(previous?.Trim(), current?.Trim(), StringComparison.Ordinal))
        {
            return;
        }

        changes.Add($"{label}: '{previous}' -> '{current}'");
    }

    private async Task<string> ResolveCompanyCenterCodeAsync(Guid tenantId, Guid companyId, CancellationToken cancellationToken)
    {
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
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

    private void EnsureTenantWriteAccess()
    {
        if (!_currentUserContext.IsAuthenticated)
        {
            throw new InvalidOperationException("Debes iniciar sesión para modificar datos.");
        }

        if (_currentUserContext.IsPlatformAdmin || _currentUserContext.Roles.Contains(PlatformRoles.TenantAdmin, StringComparer.OrdinalIgnoreCase))
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
            FROM trans
            WHERE CENTRO = @centerCode;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        return Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));
    }

    private static void Validate(SaveTransportistaCommand command)
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
        command.Email = command.Email.Trim();
        command.Website = command.Website.Trim();
        command.Country = command.Country.Trim();
        command.Format = command.Format.Trim();
        command.Notes = command.Notes.Trim();

        if (string.IsNullOrWhiteSpace(command.Name) || command.Name.Length < 3)
        {
            throw new InvalidOperationException("El nombre del transportista es obligatorio y debe tener al menos 3 caracteres.");
        }

        if (command.TaxId.Length > 12)
        {
            throw new InvalidOperationException("El NIF del transportista no puede superar 12 caracteres.");
        }

        if (!string.IsNullOrWhiteSpace(command.TaxId) &&
            command.TaxId.Any(character => !(char.IsLetterOrDigit(character) || character is '-' or '/' or '.')))
        {
            throw new InvalidOperationException("El NIF del transportista contiene caracteres no permitidos.");
        }

        if (!string.IsNullOrWhiteSpace(command.Email))
        {
            try
            {
                _ = new MailAddress(command.Email);
            }
            catch (FormatException)
            {
                throw new InvalidOperationException("El email del transportista no es válido.");
            }
        }
    }

    private static object DbValue(string value) =>
        string.IsNullOrWhiteSpace(value) ? DBNull.Value : value;

    private static string BuildSearchOrderByClause(TransportistaFilter filter)
    {
        var column = filter.SortColumn switch
        {
            nameof(TransportistaListItemDto.Code) => "CODI",
            nameof(TransportistaListItemDto.Name) => "NOM",
            nameof(TransportistaListItemDto.TaxId) => "NIF",
            nameof(TransportistaListItemDto.City) => "POB",
            nameof(TransportistaListItemDto.Email) => "EMAIL",
            nameof(TransportistaListItemDto.Phone) => "TEL",
            nameof(TransportistaListItemDto.SecondaryPhone) => "TEL2",
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
