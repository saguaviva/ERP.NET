using Erp.Application.Clients;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Support;

namespace Erp.Infrastructure.MySql.Clients;

public sealed class MySqlClienteService : IClienteQueries, IClienteService
{
    private readonly MySqlConnectionFactory _connectionFactory;

    public MySqlClienteService(MySqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IReadOnlyCollection<ClienteListItemDto>> SearchAsync(
        Guid tenantId,
        Guid companyId,
        ClienteFilter filter,
        CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return [];
        }

        var centerCode = await ResolveCompanyCenterCodeAsync(tenantId, companyId, cancellationToken);
        var limit = Math.Clamp(filter.Limit, 1, 250);
        var search = filter.Search?.Trim() ?? string.Empty;
        var likeSearch = $"%{search}%";
        var items = new List<ClienteListItemDto>();

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT CODI, CENTRO, NOM, NIF, POB, EMAIL1, TEL, COALESCE(BLOQUEADO, 0) AS BLOQUEADO
            FROM clients
            WHERE CENTRO = @centerCode
              AND (@includeBlocked = 1 OR COALESCE(BLOQUEADO, 0) = 0)
              AND (
                    @search = ''
                    OR NOM LIKE @likeSearch
                    OR NIF LIKE @likeSearch
                    OR POB LIKE @likeSearch
                    OR EMAIL1 LIKE @likeSearch
                    OR TEL LIKE @likeSearch
                  )
            ORDER BY NOM
            LIMIT @limit;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@includeBlocked", filter.IncludeBlocked);
        command.Parameters.AddWithValue("@search", search);
        command.Parameters.AddWithValue("@likeSearch", likeSearch);
        command.Parameters.AddWithValue("@limit", limit);

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
                IsBlocked = reader.GetBoolean("BLOQUEADO")
            });
        }

        return items;
    }

    public async Task<ClienteDetailDto?> GetByCodeAsync(Guid tenantId, Guid companyId, int code, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return null;
        }

        var centerCode = await ResolveCompanyCenterCodeAsync(tenantId, companyId, cancellationToken);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT CODI, CENTRO, NOM, NIF, DOM, CP, POB, PROV, PAIS, CONTACTE, TEL, FAX, EMAIL1, EMAIL2, WEB, NOTES, COALESCE(BLOQUEADO, 0) AS BLOQUEADO
            FROM clients
            WHERE CODI = @code AND CENTRO = @centerCode
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@code", code);
        command.Parameters.AddWithValue("@centerCode", centerCode);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        return new ClienteDetailDto
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
            IsBlocked = reader.GetBoolean("BLOQUEADO")
        };
    }

    public async Task<int> SaveAsync(SaveClienteCommand command, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return 0;
        }

        if (string.IsNullOrWhiteSpace(command.Name))
        {
            throw new InvalidOperationException("Client name is required.");
        }

        var centerCode = await ResolveCompanyCenterCodeAsync(command.TenantId, command.CompanyId, cancellationToken);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var code = command.Code ?? await GetNextCodeAsync(connection, centerCode, cancellationToken);

        await using var dbCommand = connection.CreateCommand();
        dbCommand.CommandText = command.Code.HasValue
            ? """
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
                  BLOQUEADO = @isBlocked
              WHERE CODI = @code
                AND CENTRO = @centerCode;
              """
            : """
              INSERT INTO clients
              (CODI, CENTRO, NOM, NIF, DOM, CP, POB, PROV, PAIS, CONTACTE, TEL, FAX, EMAIL1, EMAIL2, WEB, NOTES, BLOQUEADO)
              VALUES
              (@code, @centerCode, @name, @taxId, @address, @postalCode, @city, @province, @country, @contactName, @phone, @fax, @primaryEmail, @secondaryEmail, @website, @notes, @isBlocked);
              """;

        dbCommand.Parameters.AddWithValue("@code", code);
        dbCommand.Parameters.AddWithValue("@centerCode", centerCode);
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

        await dbCommand.ExecuteNonQueryAsync(cancellationToken);
        return code;
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

    private static async Task<int> GetNextCodeAsync(MySqlConnector.MySqlConnection connection, string centerCode, CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT COALESCE(MAX(CODI), 0) + 1
            FROM clients
            WHERE CENTRO = @centerCode;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        return Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));
    }
}
