using System.Net.Mail;
using System.Globalization;
using Erp.Application.Auditing;
using Erp.Application.Companies;
using Erp.Application.Contexts;
using Erp.Application.LegacySync;
using Erp.Application.Suppliers;
using Erp.Domain.Common;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Support;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Suppliers;

public sealed class MySqlProveedorService : IProveedorQueries, IProveedorService
{
    private readonly MySqlConnectionFactory _connectionFactory;
    private readonly IAuditLogService _auditLogService;
    private readonly ICompanyAccessService _companyAccessService;
    private readonly ICurrentUserContext _currentUserContext;
    private readonly ITenantContext _tenantContext;
    private readonly IActiveCompanyContext _activeCompanyContext;

    public MySqlProveedorService(
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

    public async Task<ProveedorSearchResultDto> SearchAsync(
        Guid tenantId,
        Guid companyId,
        ProveedorFilter filter,
        CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return new ProveedorSearchResultDto();
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);

        var centerCode = await ResolveCompanyCenterCodeAsync(tenantId, companyId, cancellationToken);
        var pageSize = Math.Clamp(filter.PageSize, 10, 200);
        var page = Math.Max(filter.Page, 1);
        var offset = (page - 1) * pageSize;
        var search = filter.Search?.Trim() ?? string.Empty;
        var likeSearch = $"%{search}%";
        var items = new List<ProveedorListItemDto>();

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using (var countCommand = connection.CreateCommand())
        {
            countCommand.CommandText =
                """
                SELECT COUNT(*)
                FROM prove
                WHERE CENTRO = @centerCode
                  AND (
                        @search = ''
                        OR NOM LIKE @likeSearch
                        OR NIF LIKE @likeSearch
                        OR CONTACTE LIKE @likeSearch
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
                return new ProveedorSearchResultDto
                {
                    TotalCount = 0
                };
            }

            await using var command = connection.CreateCommand();
            command.CommandText =
                $"""
                SELECT CODI, CENTRO, NOM, NIF, CONTACTE, POB, EMAIL1, TEL
                FROM prove
                WHERE CENTRO = @centerCode
                  AND (
                        @search = ''
                        OR NOM LIKE @likeSearch
                        OR NIF LIKE @likeSearch
                        OR CONTACTE LIKE @likeSearch
                        OR POB LIKE @likeSearch
                        OR EMAIL1 LIKE @likeSearch
                        OR TEL LIKE @likeSearch
                        OR TEL2 LIKE @likeSearch
                      )
                {BuildSupplierSearchOrderByClause(filter)}
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
                items.Add(new ProveedorListItemDto
                {
                    Code = reader.GetInt32(reader.GetOrdinal("CODI")),
                    CompanyCenterCode = reader.GetStringOrEmpty("CENTRO"),
                    Name = reader.GetStringOrEmpty("NOM"),
                    TaxId = reader.GetStringOrEmpty("NIF"),
                    ContactName = reader.GetStringOrEmpty("CONTACTE"),
                    City = reader.GetStringOrEmpty("POB"),
                    Email = reader.GetStringOrEmpty("EMAIL1"),
                    Phone = reader.GetStringOrEmpty("TEL")
                });
            }

            return new ProveedorSearchResultDto
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }

    public async Task<IReadOnlyCollection<ProveedorCatalogOptionDto>> GetPaymentMethodsAsync(CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return [];
        }

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT CODI, DESCRIPCIO, NRO, DIES, V_1
            FROM forpag
            ORDER BY DESCRIPCIO, CODI;
            """;

        var items = new List<ProveedorCatalogOptionDto>();

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new ProveedorCatalogOptionDto
            {
                Code = reader.GetStringOrEmpty("CODI"),
                Name = reader.GetStringOrEmpty("DESCRIPCIO"),
                NumberOfPayments = ReadNullableInt32(reader, "NRO"),
                PaymentDays = ReadNullableInt32(reader, "DIES"),
                FirstPaymentDays = ReadNullableInt32(reader, "V_1")
            });
        }

        return items;
    }

    public async Task<IReadOnlyCollection<ProveedorCatalogOptionDto>> GetBanksAsync(CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return [];
        }

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT CODI, DESCRIPCIO
            FROM bancs
            ORDER BY DESCRIPCIO, CODI;
            """;

        return await ReadCatalogOptionsAsync(command, cancellationToken);
    }

    public async Task<IReadOnlyCollection<ProveedorCatalogOptionDto>> GetVatRatesAsync(
        Guid tenantId,
        Guid companyId,
        CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return [];
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);

        var centerCode = await ResolveCompanyCenterCodeAsync(tenantId, companyId, cancellationToken);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT CODI, DESCRIPCIO, IVA, RE
            FROM IVA
            WHERE CENTRO = @centerCode
            ORDER BY DESCRIPCIO, CODI;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);

        var items = new List<ProveedorCatalogOptionDto>();

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new ProveedorCatalogOptionDto
            {
                Code = reader.GetStringOrEmpty("CODI"),
                Name = reader.GetStringOrEmpty("DESCRIPCIO"),
                TaxPercent = ReadNullableDecimal(reader, "IVA"),
                SurchargePercent = ReadNullableDecimal(reader, "RE")
            });
        }

        return items;
    }

    public async Task<IReadOnlyCollection<ProveedorCatalogOptionDto>> GetIncotermsAsync(CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return [];
        }

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT NOMBRE, DESCRI
            FROM incoterm
            ORDER BY DESCRI, NOMBRE;
            """;

        var items = new List<ProveedorCatalogOptionDto>();

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new ProveedorCatalogOptionDto
            {
                Code = reader.GetStringOrEmpty("NOMBRE"),
                Name = reader.GetStringOrEmpty("DESCRI")
            });
        }

        return items;
    }

    public async Task<ProveedorDetailDto?> GetByCodeAsync(Guid tenantId, Guid companyId, int code, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return null;
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);

        var centerCode = await ResolveCompanyCenterCodeAsync(tenantId, companyId, cancellationToken);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var capabilities = await GetSupplierCapabilitiesAsync(connection, cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT p.CODI,
                   p.CENTRO,
                   p.NOM,
                   p.NIF,
                   p.DOM,
                   p.CP,
                   p.POB,
                   p.PROV,
                   p.PAIS,
                   p.CONTACTE,
                   p.TEL,
                   p.TEL2,
                   p.FAX,
                   p.EMAIL1,
                   p.EMAIL2,
                   {BLOQUEADO_SELECT}
                   p.FORMA,
                   fp.DESCRIPCIO AS NOMFORMA,
                   fp.NRO AS FORMA_NRO,
                   fp.DIES AS FORMA_DIES,
                   fp.V_1 AS FORMA_V1,
                   p.BANC,
                   b.DESCRIPCIO AS NOMBANC,
                   p.COFI,
                   p.OFI,
                   p.DC,
                   p.CTA,
                   p.DIA1,
                   p.DIA2,
                   p.DIA3,
                   p.IVA,
                   i.DESCRIPCIO AS NOMIVA,
                   i.IVA AS P_IVA,
                   i.RE AS P_RE,
                   p.SUBCTA,
                   p.TRASPAS,
                   {IBAN_SELECT}
                   {SWIFT_SELECT}
                   {INCOTERM_SELECT}
                   p.WEB,
                   p.NOTES
            FROM prove p
            LEFT JOIN forpag fp ON fp.CODI = p.FORMA
            LEFT JOIN bancs b ON b.CODI = p.BANC
            LEFT JOIN IVA i ON i.CODI = p.IVA AND i.CENTRO = p.CENTRO
            {INCOTERM_JOIN}
            WHERE p.CODI = @code
              AND p.CENTRO = @centerCode
            LIMIT 1;
            """
            .Replace("{BLOQUEADO_SELECT}", capabilities.HasBlockedColumn ? "p.BLOQUEADO," : "CAST(0 AS SIGNED) AS BLOQUEADO,")
            .Replace("{IBAN_SELECT}", capabilities.HasIbanColumn ? "p.IBAN," : "CAST('' AS CHAR) AS IBAN,")
            .Replace("{SWIFT_SELECT}", capabilities.HasSwiftColumn ? "p.SWIFT," : "CAST('' AS CHAR) AS SWIFT,")
            .Replace("{INCOTERM_SELECT}", capabilities.HasIncotermColumn ? "p.INCOTERM, inc.DESCRI AS NOMINCOTERM," : "CAST('' AS CHAR) AS INCOTERM, CAST('' AS CHAR) AS NOMINCOTERM,")
            .Replace("{INCOTERM_JOIN}", capabilities.HasIncotermColumn ? "LEFT JOIN incoterm inc ON inc.NOMBRE = p.INCOTERM" : string.Empty);
        command.Parameters.AddWithValue("@code", code);
        command.Parameters.AddWithValue("@centerCode", centerCode);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        return new ProveedorDetailDto
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
            SecondaryPhone = reader.GetStringOrEmpty("TEL2"),
            Fax = reader.GetStringOrEmpty("FAX"),
            IsBlocked = reader.GetBooleanValue("BLOQUEADO"),
            PrimaryEmail = reader.GetStringOrEmpty("EMAIL1"),
            SecondaryEmail = reader.GetStringOrEmpty("EMAIL2"),
            PaymentMethodCode = reader.GetStringOrEmpty("FORMA"),
            PaymentMethodName = reader.GetStringOrEmpty("NOMFORMA"),
            PaymentMethodNumberOfPayments = ReadNullableInt32(reader, "FORMA_NRO"),
            PaymentMethodDays = ReadNullableInt32(reader, "FORMA_DIES"),
            PaymentMethodFirstPaymentDays = ReadNullableInt32(reader, "FORMA_V1"),
            BankCode = reader.GetStringOrEmpty("BANC"),
            BankName = reader.GetStringOrEmpty("NOMBANC"),
            BankEntityCode = reader.GetStringOrEmpty("COFI"),
            BankOfficeCode = reader.GetStringOrEmpty("OFI"),
            BankControlDigit = reader.GetStringOrEmpty("DC"),
            BankAccountNumber = reader.GetStringOrEmpty("CTA"),
            PaymentDay1 = ReadNullableInt32(reader, "DIA1"),
            PaymentDay2 = ReadNullableInt32(reader, "DIA2"),
            PaymentDay3 = ReadNullableInt32(reader, "DIA3"),
            TaxCode = reader.GetStringOrEmpty("IVA"),
            TaxName = reader.GetStringOrEmpty("NOMIVA"),
            TaxPercent = ReadNullableDecimal(reader, "P_IVA"),
            SurchargePercent = ReadNullableDecimal(reader, "P_RE"),
            SubAccount = FormatSubAccount(ReadNullableDouble(reader, "SUBCTA")),
            TransferToAccounting = reader.GetBooleanValue("TRASPAS"),
            Iban = reader.GetStringOrEmpty("IBAN"),
            Swift = reader.GetStringOrEmpty("SWIFT"),
            IncotermCode = reader.GetStringOrEmpty("INCOTERM"),
            IncotermName = reader.GetStringOrEmpty("NOMINCOTERM"),
            Website = reader.GetStringOrEmpty("WEB"),
            Notes = reader.GetStringOrEmpty("NOTES")
        };
    }

    public async Task<int> SaveAsync(SaveProveedorCommand command, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return 0;
        }

        await EnsureCompanyAccessAsync(command.TenantId, command.CompanyId, cancellationToken);
        EnsureTenantWriteAccess();

        Validate(command);

        var centerCode = await ResolveCompanyCenterCodeAsync(command.TenantId, command.CompanyId, cancellationToken);
        ProveedorDetailDto? previous = null;

        if (command.Code.HasValue)
        {
            previous = await GetByCodeAsync(command.TenantId, command.CompanyId, command.Code.Value, cancellationToken);
            if (previous is null)
            {
                throw new InvalidOperationException("No se ha encontrado el proveedor que intentas modificar.");
            }
        }

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await EnsureSuppliersWriteAllowedAsync(connection, command.TenantId, command.CompanyId, cancellationToken);
        var capabilities = await GetSupplierCapabilitiesAsync(connection, cancellationToken);
        var code = command.Code ?? await GetNextCodeAsync(connection, centerCode, cancellationToken);

        if (command.Code.HasValue)
        {
            await using var updateCommand = connection.CreateCommand();
            updateCommand.CommandText =
                """
                UPDATE prove
                SET NOM = @name,
                    NIF = @taxId,
                    DOM = @address,
                    CP = @postalCode,
                    POB = @city,
                    PROV = @province,
                    PAIS = @country,
                    CONTACTE = @contactName,
                    TEL = @phone,
                    TEL2 = @secondaryPhone,
                    FAX = @fax,
                    EMAIL1 = @primaryEmail,
                    EMAIL2 = @secondaryEmail,
                    {BLOQUEADO_ASSIGNMENT}
                    FORMA = @paymentMethodCode,
                    BANC = @bankCode,
                    COFI = @bankEntityCode,
                    OFI = @bankOfficeCode,
                    DC = @bankControlDigit,
                    CTA = @bankAccountNumber,
                    DIA1 = @paymentDay1,
                    DIA2 = @paymentDay2,
                    DIA3 = @paymentDay3,
                    IVA = @taxCode,
                    SUBCTA = @subAccount,
                    TRASPAS = @transferToAccounting,
                    {IBAN_ASSIGNMENT}
                    {SWIFT_ASSIGNMENT}
                    {INCOTERM_ASSIGNMENT}
                    WEB = @website,
                    NOTES = @notes
                WHERE CODI = @code
                  AND CENTRO = @centerCode;
                """
                .Replace("{BLOQUEADO_ASSIGNMENT}", capabilities.HasBlockedColumn ? "BLOQUEADO = @isBlocked," : string.Empty)
                .Replace("{IBAN_ASSIGNMENT}", capabilities.HasIbanColumn ? "IBAN = @iban," : string.Empty)
                .Replace("{SWIFT_ASSIGNMENT}", capabilities.HasSwiftColumn ? "SWIFT = @swift," : string.Empty)
                .Replace("{INCOTERM_ASSIGNMENT}", capabilities.HasIncotermColumn ? "INCOTERM = @incotermCode," : string.Empty);
            FillSaveParameters(updateCommand, centerCode, code, command);

            var affectedRows = await updateCommand.ExecuteNonQueryAsync(cancellationToken);
            if (affectedRows == 0)
            {
                throw new InvalidOperationException("No se ha podido actualizar el proveedor.");
            }
        }
        else
        {
            await using var insertCommand = connection.CreateCommand();
            var insertColumns = new List<string>
            {
                "CODI", "CENTRO", "NOM", "NIF", "DOM", "CP", "POB", "PROV", "PAIS", "CONTACTE",
                "TEL", "TEL2", "FAX", "EMAIL1", "EMAIL2", "FORMA", "BANC", "COFI", "OFI", "DC", "CTA",
                "DIA1", "DIA2", "DIA3", "IVA", "SUBCTA", "TRASPAS", "WEB", "NOTES"
            };
            var insertValues = new List<string>
            {
                "@code", "@centerCode", "@name", "@taxId", "@address", "@postalCode", "@city", "@province", "@country", "@contactName",
                "@phone", "@secondaryPhone", "@fax", "@primaryEmail", "@secondaryEmail", "@paymentMethodCode", "@bankCode", "@bankEntityCode", "@bankOfficeCode", "@bankControlDigit", "@bankAccountNumber",
                "@paymentDay1", "@paymentDay2", "@paymentDay3", "@taxCode", "@subAccount", "@transferToAccounting", "@website", "@notes"
            };

            if (capabilities.HasBlockedColumn)
            {
                insertColumns.Add("BLOQUEADO");
                insertValues.Add("@isBlocked");
            }

            if (capabilities.HasIbanColumn)
            {
                insertColumns.Add("IBAN");
                insertValues.Add("@iban");
            }

            if (capabilities.HasSwiftColumn)
            {
                insertColumns.Add("SWIFT");
                insertValues.Add("@swift");
            }

            if (capabilities.HasIncotermColumn)
            {
                insertColumns.Add("INCOTERM");
                insertValues.Add("@incotermCode");
            }

            insertCommand.CommandText = $"""
                INSERT INTO prove
                    ({string.Join(", ", insertColumns)})
                VALUES
                    ({string.Join(", ", insertValues)});
                """;
            FillSaveParameters(insertCommand, centerCode, code, command);
            await insertCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        var current = await GetByCodeAsync(command.TenantId, command.CompanyId, code, cancellationToken)
            ?? throw new InvalidOperationException("No se ha podido recargar el proveedor guardado.");

        await WriteAuditAsync(command, code, centerCode, previous, current, cancellationToken);
        return code;
    }

    private static void FillSaveParameters(MySqlCommand command, string centerCode, int code, SaveProveedorCommand source)
    {
        command.Parameters.AddWithValue("@code", code);
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@name", source.Name);
        command.Parameters.AddWithValue("@taxId", source.TaxId);
        command.Parameters.AddWithValue("@address", source.Address);
        command.Parameters.AddWithValue("@postalCode", source.PostalCode);
        command.Parameters.AddWithValue("@city", source.City);
        command.Parameters.AddWithValue("@province", source.Province);
        command.Parameters.AddWithValue("@country", source.Country);
        command.Parameters.AddWithValue("@contactName", source.ContactName);
        command.Parameters.AddWithValue("@phone", source.Phone);
        command.Parameters.AddWithValue("@secondaryPhone", source.SecondaryPhone);
        command.Parameters.AddWithValue("@fax", source.Fax);
        command.Parameters.AddWithValue("@isBlocked", source.IsBlocked);
        command.Parameters.AddWithValue("@primaryEmail", source.PrimaryEmail);
        command.Parameters.AddWithValue("@secondaryEmail", source.SecondaryEmail);
        command.Parameters.AddWithValue("@paymentMethodCode", DbValue(source.PaymentMethodCode));
        command.Parameters.AddWithValue("@bankCode", DbValue(source.BankCode));
        command.Parameters.AddWithValue("@bankEntityCode", DbValue(source.BankEntityCode));
        command.Parameters.AddWithValue("@bankOfficeCode", DbValue(source.BankOfficeCode));
        command.Parameters.AddWithValue("@bankControlDigit", DbValue(source.BankControlDigit));
        command.Parameters.AddWithValue("@bankAccountNumber", DbValue(source.BankAccountNumber));
        command.Parameters.AddWithValue("@paymentDay1", DbValue(source.PaymentDay1));
        command.Parameters.AddWithValue("@paymentDay2", DbValue(source.PaymentDay2));
        command.Parameters.AddWithValue("@paymentDay3", DbValue(source.PaymentDay3));
        command.Parameters.AddWithValue("@taxCode", DbValue(source.TaxCode));
        command.Parameters.AddWithValue("@subAccount", DbValue(ParseSubAccount(source.SubAccount)));
        command.Parameters.AddWithValue("@transferToAccounting", source.TransferToAccounting);
        command.Parameters.AddWithValue("@iban", DbValue(source.Iban));
        command.Parameters.AddWithValue("@swift", DbValue(source.Swift));
        command.Parameters.AddWithValue("@incotermCode", DbValue(source.IncotermCode));
        command.Parameters.AddWithValue("@website", source.Website);
        command.Parameters.AddWithValue("@notes", source.Notes);
    }

    private async Task WriteAuditAsync(
        SaveProveedorCommand command,
        int code,
        string centerCode,
        ProveedorDetailDto? previous,
        ProveedorDetailDto current,
        CancellationToken cancellationToken)
    {
        var isCreate = previous is null;
        var action = isCreate ? "ProveedorCreated" : "ProveedorUpdated";
        var changes = new List<string>();

        if (isCreate)
        {
            changes.Add($"Codigo={code}");
            changes.Add($"Nombre={current.Name}");
            changes.Add($"Centro={centerCode}");
            if (current.IsBlocked)
            {
                changes.Add("Estado=Bloqueado");
            }
        }
        else
        {
            AppendChange(changes, "Nombre", previous!.Name, current.Name);
            AppendChange(changes, "NIF", previous.TaxId, current.TaxId);
            AppendChange(changes, "Contacto", previous.ContactName, current.ContactName);
            AppendChange(changes, "Telefono", previous.Phone, current.Phone);
            AppendChange(changes, "Telefono2", previous.SecondaryPhone, current.SecondaryPhone);
            AppendChange(changes, "Fax", previous.Fax, current.Fax);
            AppendChange(changes, "Estado", previous.IsBlocked ? "Bloqueado" : "Activo", current.IsBlocked ? "Bloqueado" : "Activo");
            AppendChange(changes, "Email1", previous.PrimaryEmail, current.PrimaryEmail);
            AppendChange(changes, "Email2", previous.SecondaryEmail, current.SecondaryEmail);
            AppendChange(changes, "FormaPago", previous.PaymentMethodName, current.PaymentMethodName);
            AppendChange(changes, "Banco", previous.BankName, current.BankName);
            AppendChange(changes, "Entidad", previous.BankEntityCode, current.BankEntityCode);
            AppendChange(changes, "Oficina", previous.BankOfficeCode, current.BankOfficeCode);
            AppendChange(changes, "DC", previous.BankControlDigit, current.BankControlDigit);
            AppendChange(changes, "Cuenta", previous.BankAccountNumber, current.BankAccountNumber);
            AppendChange(changes, "DiaPago1", FormatDay(previous.PaymentDay1), FormatDay(current.PaymentDay1));
            AppendChange(changes, "DiaPago2", FormatDay(previous.PaymentDay2), FormatDay(current.PaymentDay2));
            AppendChange(changes, "DiaPago3", FormatDay(previous.PaymentDay3), FormatDay(current.PaymentDay3));
            AppendChange(changes, "IVA", previous.TaxName, current.TaxName);
            AppendChange(changes, "Subcuenta", previous.SubAccount, current.SubAccount);
            AppendChange(changes, "TraspasoContable", previous.TransferToAccounting ? "Si" : "No", current.TransferToAccounting ? "Si" : "No");
            AppendChange(changes, "IBAN", previous.Iban, current.Iban);
            AppendChange(changes, "SWIFT", previous.Swift, current.Swift);
            AppendChange(changes, "Incoterm", previous.IncotermName, current.IncotermName);
            AppendChange(changes, "Direccion", previous.Address, current.Address);
            AppendChange(changes, "CP", previous.PostalCode, current.PostalCode);
            AppendChange(changes, "Ciudad", previous.City, current.City);
            AppendChange(changes, "Provincia", previous.Province, current.Province);
            AppendChange(changes, "Pais", previous.Country, current.Country);
            AppendChange(changes, "Web", previous.Website, current.Website);
            AppendChange(changes, "Notas", previous.Notes, current.Notes);
        }

        if (!isCreate && changes.Count == 0)
        {
            return;
        }

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = command.TenantId,
            CompanyId = command.CompanyId,
            Action = action,
            EntityName = "Proveedor",
            EntityId = code.ToString(),
            Details = changes.Count == 0
                ? $"Codigo={code}; Nombre={current.Name}; Centro={centerCode}"
                : string.Join("; ", changes)
        }, cancellationToken);
    }

    private static void AppendChange(List<string> changes, string label, string previous, string current)
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

    private static async Task<bool> IsLegacySyncActiveForCompanyAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT COUNT(*)
            FROM legacy_sync_checkpoints
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND module_key = @moduleKey
              AND last_status IN ('Completed', 'CompletedWithErrors');
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@moduleKey", LegacySyncModuleKeys.CrmSuppliers);
        return Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken)) > 0;
    }

    private static async Task EnsureSuppliersWriteAllowedAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        CancellationToken cancellationToken)
    {
        if (await IsLegacySyncActiveForCompanyAsync(connection, tenantId, companyId, cancellationToken))
        {
            throw new InvalidOperationException("Compras / Proveedores está en convivencia con legacy para esta empresa. Mientras el módulo esté sincronizado, la web queda en solo lectura.");
        }
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
            FROM prove
            WHERE CENTRO = @centerCode;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        return Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));
    }

    private static void Validate(SaveProveedorCommand command)
    {
        command.Name = command.Name.Trim();
        command.TaxId = command.TaxId.Trim().ToUpperInvariant();
        command.Address = command.Address.Trim();
        command.PostalCode = command.PostalCode.Trim();
        command.City = command.City.Trim();
        command.Province = command.Province.Trim();
        command.Country = command.Country.Trim();
        command.ContactName = command.ContactName.Trim();
        command.Phone = command.Phone.Trim();
        command.SecondaryPhone = command.SecondaryPhone.Trim();
        command.Fax = command.Fax.Trim();
        command.PrimaryEmail = command.PrimaryEmail.Trim();
        command.SecondaryEmail = command.SecondaryEmail.Trim();
        command.PaymentMethodCode = command.PaymentMethodCode.Trim();
        command.BankCode = command.BankCode.Trim();
        command.BankEntityCode = NormalizeDigits(command.BankEntityCode, 4);
        command.BankOfficeCode = NormalizeDigits(command.BankOfficeCode, 4);
        command.BankControlDigit = NormalizeDigits(command.BankControlDigit, 2);
        command.BankAccountNumber = NormalizeDigits(command.BankAccountNumber, 10);
        command.PaymentDay1 = NormalizePaymentDay(command.PaymentDay1);
        command.PaymentDay2 = NormalizePaymentDay(command.PaymentDay2);
        command.PaymentDay3 = NormalizePaymentDay(command.PaymentDay3);
        command.TaxCode = command.TaxCode.Trim();
        command.SubAccount = NormalizeSubAccount(command.SubAccount);
        command.Iban = NormalizeIban(command.Iban);
        command.Swift = NormalizeSwift(command.Swift);
        command.IncotermCode = command.IncotermCode.Trim().ToUpperInvariant();
        command.Website = command.Website.Trim();
        command.Notes = command.Notes.Trim();

        if (string.IsNullOrWhiteSpace(command.Name) || command.Name.Length < 3)
        {
            throw new InvalidOperationException("El nombre del proveedor es obligatorio y debe tener al menos 3 caracteres.");
        }

        if (command.TaxId.Length > 12)
        {
            throw new InvalidOperationException("El NIF del proveedor no puede superar 12 caracteres.");
        }

        if (!string.IsNullOrWhiteSpace(command.TaxId) &&
            command.TaxId.Any(character => !(char.IsLetterOrDigit(character) || character is '-' or '/' or '.')))
        {
            throw new InvalidOperationException("El NIF del proveedor contiene caracteres no permitidos.");
        }

        ValidateEmail(command.PrimaryEmail, "El email principal del proveedor no es válido.");
        ValidateEmail(command.SecondaryEmail, "El email secundario del proveedor no es válido.");

        if (!string.IsNullOrWhiteSpace(command.PrimaryEmail) &&
            string.Equals(command.PrimaryEmail, command.SecondaryEmail, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("El email principal y el secundario no pueden ser iguales.");
        }

        var paymentDays = new[] { command.PaymentDay1, command.PaymentDay2, command.PaymentDay3 }
            .Where(day => day.HasValue)
            .Select(day => day!.Value)
            .ToArray();

        if (paymentDays.Length != paymentDays.Distinct().Count())
        {
            throw new InvalidOperationException("Los días de pago no pueden repetirse.");
        }

        ValidateAccountBlock(command.BankEntityCode, 4, "La entidad bancaria debe tener 4 dígitos.");
        ValidateAccountBlock(command.BankOfficeCode, 4, "La oficina bancaria debe tener 4 dígitos.");
        ValidateAccountBlock(command.BankControlDigit, 2, "El dígito de control debe tener 2 dígitos.");
        ValidateAccountBlock(command.BankAccountNumber, 10, "La cuenta bancaria debe tener 10 dígitos.");
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

    private static async Task<IReadOnlyCollection<ProveedorCatalogOptionDto>> ReadCatalogOptionsAsync(
        MySqlCommand command,
        CancellationToken cancellationToken)
    {
        var items = new List<ProveedorCatalogOptionDto>();

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new ProveedorCatalogOptionDto
            {
                Code = reader.GetStringOrEmpty("CODI"),
                Name = reader.GetStringOrEmpty("DESCRIPCIO")
            });
        }

        return items;
    }

    private static async Task<SupplierColumnCapabilities> GetSupplierCapabilitiesAsync(MySqlConnection connection, CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT COLUMN_NAME
            FROM INFORMATION_SCHEMA.COLUMNS
            WHERE TABLE_SCHEMA = DATABASE()
              AND TABLE_NAME = 'prove'
              AND COLUMN_NAME IN ('BLOQUEADO', 'IBAN', 'SWIFT', 'INCOTERM');
            """;

        var names = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            names.Add(reader.GetStringOrEmpty("COLUMN_NAME"));
        }

        return new SupplierColumnCapabilities(
            HasBlockedColumn: names.Contains("BLOQUEADO"),
            HasIbanColumn: names.Contains("IBAN"),
            HasSwiftColumn: names.Contains("SWIFT"),
            HasIncotermColumn: names.Contains("INCOTERM"));
    }

    private static int? ReadNullableInt32(MySqlDataReader reader, string columnName)
    {
        var ordinal = reader.GetOrdinal(columnName);
        if (reader.IsDBNull(ordinal))
        {
            return null;
        }

        var value = reader.GetValue(ordinal);
        return value switch
        {
            sbyte signedByte => signedByte,
            byte unsignedByte => unsignedByte,
            short shortValue => shortValue,
            int intValue => intValue,
            long longValue => checked((int)longValue),
            decimal decimalValue => decimal.ToInt32(decimalValue),
            string stringValue when string.IsNullOrWhiteSpace(stringValue) => null,
            string stringValue when int.TryParse(stringValue, out var parsed) => parsed,
            _ => Convert.ToInt32(value)
        };
    }

    private static double? ReadNullableDouble(MySqlDataReader reader, string columnName)
    {
        var ordinal = reader.GetOrdinal(columnName);
        if (reader.IsDBNull(ordinal))
        {
            return null;
        }

        var value = reader.GetValue(ordinal);
        return value switch
        {
            float floatValue => floatValue,
            double doubleValue => doubleValue,
            decimal decimalValue => decimal.ToDouble(decimalValue),
            int intValue => intValue,
            long longValue => longValue,
            string stringValue when string.IsNullOrWhiteSpace(stringValue) => null,
            string stringValue when double.TryParse(stringValue, NumberStyles.Float, CultureInfo.InvariantCulture, out var parsedInvariantDouble) => parsedInvariantDouble,
            string stringValue when double.TryParse(stringValue, NumberStyles.Float, CultureInfo.CurrentCulture, out var parsedCurrentDouble) => parsedCurrentDouble,
            _ => Convert.ToDouble(value, CultureInfo.InvariantCulture)
        };
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

    private static object DbValue(string value)
    {
        return string.IsNullOrWhiteSpace(value) ? DBNull.Value : value;
    }

    private static object DbValue(int? value)
    {
        return value.HasValue ? value.Value : DBNull.Value;
    }

    private static object DbValue(double? value)
    {
        return value.HasValue ? value.Value : DBNull.Value;
    }

    private static string NormalizeDigits(string value, int maxLength)
    {
        var trimmed = value.Trim();
        if (string.IsNullOrWhiteSpace(trimmed))
        {
            return string.Empty;
        }

        if (trimmed.Any(character => !char.IsDigit(character)))
        {
            throw new InvalidOperationException("Los datos bancarios solo pueden contener dígitos.");
        }

        if (trimmed.Length > maxLength)
        {
            throw new InvalidOperationException($"Los datos bancarios no pueden superar {maxLength} dígitos.");
        }

        return trimmed;
    }

    private static string NormalizeSubAccount(string value)
    {
        var trimmed = value.Trim();
        if (string.IsNullOrWhiteSpace(trimmed))
        {
            return string.Empty;
        }

        if (!double.TryParse(trimmed, NumberStyles.Float, CultureInfo.InvariantCulture, out _) &&
            !double.TryParse(trimmed, NumberStyles.Float, CultureInfo.CurrentCulture, out _))
        {
            throw new InvalidOperationException("La subcuenta contable no es válida.");
        }

        return trimmed;
    }

    private static string NormalizeIban(string value)
    {
        var normalized = value.Replace(" ", string.Empty, StringComparison.Ordinal).Trim().ToUpperInvariant();
        if (string.IsNullOrWhiteSpace(normalized))
        {
            return string.Empty;
        }

        if (normalized.Any(character => !char.IsLetterOrDigit(character)))
        {
            throw new InvalidOperationException("El IBAN solo puede contener letras y dígitos.");
        }

        if (normalized.Length < 15 || normalized.Length > 34)
        {
            throw new InvalidOperationException("El IBAN debe tener entre 15 y 34 caracteres.");
        }

        return normalized;
    }

    private static string NormalizeSwift(string value)
    {
        var normalized = value.Replace(" ", string.Empty, StringComparison.Ordinal).Trim().ToUpperInvariant();
        if (string.IsNullOrWhiteSpace(normalized))
        {
            return string.Empty;
        }

        if (normalized.Any(character => !char.IsLetterOrDigit(character)))
        {
            throw new InvalidOperationException("El SWIFT solo puede contener letras y dígitos.");
        }

        if (normalized.Length is not (8 or 11))
        {
            throw new InvalidOperationException("El SWIFT debe tener 8 u 11 caracteres.");
        }

        return normalized;
    }

    private static int? NormalizePaymentDay(int? day)
    {
        if (!day.HasValue || day.Value == 0)
        {
            return null;
        }

        if (day.Value < 1 || day.Value > 31)
        {
            throw new InvalidOperationException("Los días de pago deben estar entre 1 y 31.");
        }

        return day.Value;
    }

    private static double? ParseSubAccount(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var invariantValue))
        {
            return invariantValue;
        }

        if (double.TryParse(value, NumberStyles.Float, CultureInfo.CurrentCulture, out var currentValue))
        {
            return currentValue;
        }

        throw new InvalidOperationException("La subcuenta contable no es válida.");
    }

    private static void ValidateAccountBlock(string value, int expectedLength, string errorMessage)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        if (value.Length != expectedLength)
        {
            throw new InvalidOperationException(errorMessage);
        }
    }

    private static string FormatDay(int? day)
    {
        return day?.ToString() ?? string.Empty;
    }

    private static string FormatSubAccount(double? value)
    {
        if (!value.HasValue)
        {
            return string.Empty;
        }

        return value.Value % 1 == 0
            ? value.Value.ToString("0", CultureInfo.InvariantCulture)
            : value.Value.ToString(CultureInfo.InvariantCulture);
    }

    private static string BuildSupplierSearchOrderByClause(ProveedorFilter filter)
    {
        var column = filter.SortColumn switch
        {
            nameof(ProveedorListItemDto.Code) => "CODI",
            nameof(ProveedorListItemDto.Name) => "NOM",
            nameof(ProveedorListItemDto.TaxId) => "NIF",
            nameof(ProveedorListItemDto.ContactName) => "CONTACTE",
            nameof(ProveedorListItemDto.City) => "POB",
            nameof(ProveedorListItemDto.Email) => "EMAIL1",
            nameof(ProveedorListItemDto.Phone) => "TEL",
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

    private sealed record SupplierColumnCapabilities(
        bool HasBlockedColumn,
        bool HasIbanColumn,
        bool HasSwiftColumn,
        bool HasIncotermColumn);
}
