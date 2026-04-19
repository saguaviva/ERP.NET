using System.Globalization;
using System.Net.Mail;
using Erp.Application.Auditing;
using Erp.Application.Companies;
using Erp.Application.Contexts;
using Erp.Application.LegacySync;
using Erp.Application.Representatives;
using Erp.Domain.Common;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Support;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Representatives;

public sealed class MySqlRepresentativeService : IRepresentativeQueries, IRepresentativeService
{
    private readonly MySqlConnectionFactory _connectionFactory;
    private readonly IAuditLogService _auditLogService;
    private readonly ICompanyAccessService _companyAccessService;
    private readonly ICurrentUserContext _currentUserContext;
    private readonly ITenantContext _tenantContext;
    private readonly IActiveCompanyContext _activeCompanyContext;

    public MySqlRepresentativeService(
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

    public async Task<RepresentativeSearchResultDto> SearchAsync(
        Guid tenantId,
        Guid companyId,
        RepresentativeFilter filter,
        CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return new RepresentativeSearchResultDto();
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
            FROM repres
            WHERE CENTRO = @centerCode
              AND is_deleted = 0
              AND (
                    @search = ''
                    OR NOM LIKE @likeSearch
                    OR NIF LIKE @likeSearch
                    OR POB LIKE @likeSearch
                    OR EMAIL1 LIKE @likeSearch
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
            return new RepresentativeSearchResultDto
            {
                TotalCount = 0
            };
        }

        var items = new List<RepresentativeListItemDto>();
        await using var command = connection.CreateCommand();
        command.CommandText =
            $"""
            SELECT CODI, CENTRO, NOM, COMIS, NIF, POB, EMAIL1, TEL
            FROM repres
            WHERE CENTRO = @centerCode
              AND is_deleted = 0
              AND (
                    @search = ''
                    OR NOM LIKE @likeSearch
                    OR NIF LIKE @likeSearch
                    OR POB LIKE @likeSearch
                    OR EMAIL1 LIKE @likeSearch
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
            items.Add(new RepresentativeListItemDto
            {
                Code = reader.GetInt32(reader.GetOrdinal("CODI")),
                CompanyCenterCode = reader.GetStringOrEmpty("CENTRO"),
                Name = reader.GetStringOrEmpty("NOM"),
                CommissionPercent = ReadNullableDecimal(reader, "COMIS") ?? 0m,
                TaxId = reader.GetStringOrEmpty("NIF"),
                City = reader.GetStringOrEmpty("POB"),
                Email = reader.GetStringOrEmpty("EMAIL1"),
                Phone = reader.GetStringOrEmpty("TEL")
            });
        }

        return new RepresentativeSearchResultDto
        {
            Items = items,
            TotalCount = totalCount
        };
    }

    public async Task<RepresentativeDetailDto?> GetByCodeAsync(
        Guid tenantId,
        Guid companyId,
        int code,
        CancellationToken cancellationToken = default)
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
            SELECT CODI, CENTRO, NOM, NIF, DOM, CP, POB, PROV, PAIS, TEL, TEL2, FAX, EMAIL1, EMAIL2, WEB, COMIS, OBSERV
            FROM repres
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

        return new RepresentativeDetailDto
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
            Phone = reader.GetStringOrEmpty("TEL"),
            SecondaryPhone = reader.GetStringOrEmpty("TEL2"),
            Fax = reader.GetStringOrEmpty("FAX"),
            PrimaryEmail = reader.GetStringOrEmpty("EMAIL1"),
            SecondaryEmail = reader.GetStringOrEmpty("EMAIL2"),
            Website = reader.GetStringOrEmpty("WEB"),
            CommissionPercent = ReadNullableDecimal(reader, "COMIS") ?? 0m,
            Notes = reader.GetStringOrEmpty("OBSERV")
        };
    }

    public async Task<int> SaveAsync(SaveRepresentativeCommand command, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return 0;
        }

        await EnsureCompanyAccessAsync(command.TenantId, command.CompanyId, cancellationToken);
        EnsureTenantWriteAccess();

        Validate(command);

        var centerCode = await ResolveCompanyCenterCodeAsync(command.TenantId, command.CompanyId, cancellationToken);
        RepresentativeDetailDto? previous = null;
        if (command.Code.HasValue)
        {
            previous = await GetByCodeAsync(command.TenantId, command.CompanyId, command.Code.Value, cancellationToken);
            if (previous is null)
            {
                throw new InvalidOperationException("No se ha encontrado el representante que intentas modificar.");
            }
        }

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var code = command.Code ?? await GetNextCodeAsync(connection, centerCode, cancellationToken);

        if (command.Code.HasValue)
        {
            await using var updateCommand = connection.CreateCommand();
            updateCommand.CommandText =
                """
                UPDATE repres
                SET NOM = @name,
                    NIF = @taxId,
                    DOM = @address,
                    CP = @postalCode,
                    POB = @city,
                    PROV = @province,
                    PAIS = @country,
                    TEL = @phone,
                    TEL2 = @secondaryPhone,
                    FAX = @fax,
                    EMAIL1 = @primaryEmail,
                    EMAIL2 = @secondaryEmail,
                    WEB = @website,
                    COMIS = @commissionPercent,
                    OBSERV = @notes,
                    origin = 'local',
                    is_deleted = 0,
                    synced_utc = NULL
                WHERE CODI = @code
                  AND CENTRO = @centerCode;
                """;
            FillSaveParameters(updateCommand, centerCode, code, command);

            var affectedRows = await updateCommand.ExecuteNonQueryAsync(cancellationToken);
            if (affectedRows == 0)
            {
                throw new InvalidOperationException("No se ha podido actualizar el representante.");
            }

            await AuditAsync(command.TenantId, command.CompanyId, code, previous, command, cancellationToken);
            return code;
        }

        await using var insertCommand = connection.CreateCommand();
        insertCommand.CommandText =
            """
            INSERT INTO repres (
                CODI, CENTRO, NOM, NIF, DOM, CP, POB, PROV, PAIS, TEL, TEL2, FAX, EMAIL1, EMAIL2, WEB, COMIS, OBSERV, origin, is_deleted, synced_utc
            )
            VALUES (
                @code, @centerCode, @name, @taxId, @address, @postalCode, @city, @province, @country, @phone, @secondaryPhone, @fax, @primaryEmail, @secondaryEmail, @website, @commissionPercent, @notes, 'local', 0, NULL
            );
            """;
        FillSaveParameters(insertCommand, centerCode, code, command);
        await insertCommand.ExecuteNonQueryAsync(cancellationToken);

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = command.TenantId,
            CompanyId = command.CompanyId,
            UserId = _currentUserContext.UserId,
            Action = "RepresentanteCreated",
            EntityName = "Representante",
            EntityId = code.ToString(CultureInfo.InvariantCulture),
            Details = $"Representante {code} creado: {command.Name}"
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
            UPDATE repres
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
            throw new InvalidOperationException("No se ha encontrado el representante a eliminar.");
        }

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = tenantId,
            CompanyId = companyId,
            UserId = _currentUserContext.UserId,
            Action = "RepresentanteDeleted",
            EntityName = "Representante",
            EntityId = code.ToString(CultureInfo.InvariantCulture),
            Details = $"Representante {code} eliminado en local."
        }, cancellationToken);
    }

    private static void FillSaveParameters(MySqlCommand command, string centerCode, int code, SaveRepresentativeCommand model)
    {
        command.Parameters.AddWithValue("@code", code);
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@name", model.Name);
        command.Parameters.AddWithValue("@taxId", DbValue(model.TaxId));
        command.Parameters.AddWithValue("@address", DbValue(model.Address));
        command.Parameters.AddWithValue("@postalCode", DbValue(model.PostalCode));
        command.Parameters.AddWithValue("@city", DbValue(model.City));
        command.Parameters.AddWithValue("@province", DbValue(model.Province));
        command.Parameters.AddWithValue("@country", DbValue(model.Country));
        command.Parameters.AddWithValue("@phone", DbValue(model.Phone));
        command.Parameters.AddWithValue("@secondaryPhone", DbValue(model.SecondaryPhone));
        command.Parameters.AddWithValue("@fax", DbValue(model.Fax));
        command.Parameters.AddWithValue("@primaryEmail", DbValue(model.PrimaryEmail));
        command.Parameters.AddWithValue("@secondaryEmail", DbValue(model.SecondaryEmail));
        command.Parameters.AddWithValue("@website", DbValue(model.Website));
        command.Parameters.AddWithValue("@commissionPercent", model.CommissionPercent);
        command.Parameters.AddWithValue("@notes", DbValue(model.Notes));
    }

    private async Task AuditAsync(
        Guid tenantId,
        Guid companyId,
        int code,
        RepresentativeDetailDto? previous,
        SaveRepresentativeCommand current,
        CancellationToken cancellationToken)
    {
        if (previous is null)
        {
            return;
        }

        var changes = new List<string>();
        AddChange(changes, "Nombre", previous.Name, current.Name);
        AddChange(changes, "NIF", previous.TaxId, current.TaxId);
        AddChange(changes, "Dirección", previous.Address, current.Address);
        AddChange(changes, "CP", previous.PostalCode, current.PostalCode);
        AddChange(changes, "Ciudad", previous.City, current.City);
        AddChange(changes, "Provincia", previous.Province, current.Province);
        AddChange(changes, "País", previous.Country, current.Country);
        AddChange(changes, "Teléfono", previous.Phone, current.Phone);
        AddChange(changes, "Teléfono 2", previous.SecondaryPhone, current.SecondaryPhone);
        AddChange(changes, "Fax", previous.Fax, current.Fax);
        AddChange(changes, "Email 1", previous.PrimaryEmail, current.PrimaryEmail);
        AddChange(changes, "Email 2", previous.SecondaryEmail, current.SecondaryEmail);
        AddChange(changes, "Web", previous.Website, current.Website);
        AddChange(changes, "Comisión", previous.CommissionPercent.ToString("0.##", CultureInfo.InvariantCulture), current.CommissionPercent.ToString("0.##", CultureInfo.InvariantCulture));
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
            Action = "RepresentanteUpdated",
            EntityName = "Representante",
            EntityId = code.ToString(CultureInfo.InvariantCulture),
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

    private static async Task<int> GetNextCodeAsync(MySqlConnection connection, string centerCode, CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT COALESCE(MAX(CODI), 0) + 1
            FROM repres
            WHERE CENTRO = @centerCode;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        return Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));
    }

    private static void Validate(SaveRepresentativeCommand command)
    {
        command.Name = command.Name.Trim();
        command.TaxId = command.TaxId.Trim().ToUpperInvariant();
        command.Address = command.Address.Trim();
        command.PostalCode = command.PostalCode.Trim();
        command.City = command.City.Trim();
        command.Province = command.Province.Trim();
        command.Country = command.Country.Trim();
        command.Phone = command.Phone.Trim();
        command.SecondaryPhone = command.SecondaryPhone.Trim();
        command.Fax = command.Fax.Trim();
        command.PrimaryEmail = command.PrimaryEmail.Trim();
        command.SecondaryEmail = command.SecondaryEmail.Trim();
        command.Website = command.Website.Trim();
        command.Notes = command.Notes.Trim();

        if (string.IsNullOrWhiteSpace(command.Name) || command.Name.Length < 3)
        {
            throw new InvalidOperationException("El nombre del representante es obligatorio y debe tener al menos 3 caracteres.");
        }

        if (command.TaxId.Length > 12)
        {
            throw new InvalidOperationException("El NIF del representante no puede superar 12 caracteres.");
        }

        if (!string.IsNullOrWhiteSpace(command.TaxId) &&
            command.TaxId.Any(character => !(char.IsLetterOrDigit(character) || character is '-' or '/' or '.')))
        {
            throw new InvalidOperationException("El NIF del representante contiene caracteres no permitidos.");
        }

        ValidateEmail(command.PrimaryEmail, "El email principal del representante no es válido.");
        ValidateEmail(command.SecondaryEmail, "El email secundario del representante no es válido.");

        if (!string.IsNullOrWhiteSpace(command.PrimaryEmail) &&
            string.Equals(command.PrimaryEmail, command.SecondaryEmail, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("El email principal y el secundario no pueden ser iguales.");
        }

        if (command.CommissionPercent < 0 || command.CommissionPercent > 100)
        {
            throw new InvalidOperationException("La comisión debe estar entre 0 y 100.");
        }
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

    private static decimal? ReadNullableDecimal(MySqlDataReader reader, string columnName)
    {
        var ordinal = reader.GetOrdinal(columnName);
        if (reader.IsDBNull(ordinal))
        {
            return null;
        }

        var value = reader.GetValue(ordinal);
        return value switch
        {
            decimal decimalValue => decimalValue,
            float floatValue => (decimal)floatValue,
            double doubleValue => (decimal)doubleValue,
            int intValue => intValue,
            long longValue => longValue,
            string stringValue when string.IsNullOrWhiteSpace(stringValue) => null,
            string stringValue when decimal.TryParse(stringValue, NumberStyles.Float, CultureInfo.InvariantCulture, out var parsedInvariantDecimal) => parsedInvariantDecimal,
            string stringValue when decimal.TryParse(stringValue, NumberStyles.Float, CultureInfo.CurrentCulture, out var parsedCurrentDecimal) => parsedCurrentDecimal,
            _ => Convert.ToDecimal(value, CultureInfo.InvariantCulture)
        };
    }

    private static object DbValue(string value) =>
        string.IsNullOrWhiteSpace(value) ? DBNull.Value : value;

    private static string BuildSearchOrderByClause(RepresentativeFilter filter)
    {
        var column = filter.SortColumn switch
        {
            nameof(RepresentativeListItemDto.Code) => "CODI",
            nameof(RepresentativeListItemDto.Name) => "NOM",
            nameof(RepresentativeListItemDto.CommissionPercent) => "COMIS",
            nameof(RepresentativeListItemDto.TaxId) => "NIF",
            nameof(RepresentativeListItemDto.City) => "POB",
            nameof(RepresentativeListItemDto.Email) => "EMAIL1",
            nameof(RepresentativeListItemDto.Phone) => "TEL",
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
