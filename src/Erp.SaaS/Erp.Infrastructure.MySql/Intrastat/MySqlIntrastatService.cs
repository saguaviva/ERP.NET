using System.Globalization;
using System.Text.RegularExpressions;
using Erp.Application.Companies;
using Erp.Application.Contexts;
using Erp.Application.Intrastat;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Support;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Intrastat;

public sealed class MySqlIntrastatService : IIntrastatQueries
{
    private const string TransportDescription = "TRANSIT AND COURIER CHARGES";
    private static readonly Regex NcCodeRegex = new(@"\b(?<code>\d{4}\s?\d{2}\s?\d{2})\b", RegexOptions.Compiled | RegexOptions.CultureInvariant);
    private static readonly HashSet<string> EuCountryCodes =
    [
        "AT", "BE", "BG", "CY", "CZ", "DE", "DK", "EE", "EL", "ES", "FI", "FR",
        "HR", "HU", "IE", "IT", "LT", "LU", "LV", "MT", "NL", "PL", "PT", "RO",
        "SE", "SI", "SK"
    ];
    private static readonly HashSet<string> LegacyIntrastatCountryCodes =
    [
        "AT", "BE", "BG", "CY", "CZ", "DE", "DK", "EE", "EL", "FI", "FR",
        "HR", "HU", "IE", "IT", "LT", "LU", "LV", "MT", "NL", "PL", "PT", "RO",
        "SE", "SI", "SK"
    ];
    private static readonly IReadOnlyList<string> LegacyMatrixNcCodes =
    [
        "6002 40 00",
        "6002 90 00",
        "6004 10 00",
        "6004 90 00",
        "6117 80 10",
        "5205 23 00"
    ];
    private static readonly IReadOnlyList<LegacyCountryDefinition> LegacyMatrixCountries =
    [
        new("AT", "Austria (1995)", false),
        new("BE", "Bélgica (1952)", false),
        new("BG", "Bulgaria (2007)", false),
        new("CY", "Chipre (2004)", false),
        new("CZ", "República Checa (2004)", false),
        new("DE", "Alemania (1952)", false),
        new("DK", "Dinamarca (1973)", false),
        new("EE", "Estonia (2004)", false),
        new("EL", "Grecia (1981)", false),
        new("ES", "España (1986)", true),
        new("FI", "Finlandia (1995)", false),
        new("FR", "Francia (1952)", false),
        new("HR", "Croacia (2013)", false),
        new("HU", "Hungría (2004)", false),
        new("IE", "Irlanda (1973)", false),
        new("IT", "Italia (1952)", false),
        new("LT", "Lituania (2004)", false),
        new("LU", "Luxemburgo (1952)", false),
        new("LV", "Letonia (2004)", false),
        new("MT", "Malta (2004)", false),
        new("NL", "Países Bajos (1952)", false),
        new("PL", "Polonia (2004)", false),
        new("PT", "Portugal (1986)", false),
        new("RO", "Rumanía (2007)", false),
        new("SE", "Suecia (1995)", false),
        new("SI", "Eslovenia (2004)", false),
        new("SK", "Eslovaquia (2004)", false)
    ];

    private static readonly IReadOnlyDictionary<string, string> CountryNames = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        ["AT"] = "Austria",
        ["BE"] = "Bélgica",
        ["BG"] = "Bulgaria",
        ["CY"] = "Chipre",
        ["CZ"] = "Chequia",
        ["DE"] = "Alemania",
        ["DK"] = "Dinamarca",
        ["EE"] = "Estonia",
        ["EL"] = "Grecia",
        ["ES"] = "España",
        ["FI"] = "Finlandia",
        ["FR"] = "Francia",
        ["HR"] = "Croacia",
        ["HU"] = "Hungría",
        ["IE"] = "Irlanda",
        ["IT"] = "Italia",
        ["LT"] = "Lituania",
        ["LU"] = "Luxemburgo",
        ["LV"] = "Letonia",
        ["MT"] = "Malta",
        ["NL"] = "Países Bajos",
        ["PL"] = "Polonia",
        ["PT"] = "Portugal",
        ["RO"] = "Rumanía",
        ["SE"] = "Suecia",
        ["SI"] = "Eslovenia",
        ["SK"] = "Eslovaquia"
    };
    private static readonly IReadOnlyDictionary<string, string> CountryAliases = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        ["AT"] = "AT",
        ["AUSTRIA"] = "AT",
        ["BE"] = "BE",
        ["BELGICA"] = "BE",
        ["BÉLGICA"] = "BE",
        ["BELGIUM"] = "BE",
        ["BG"] = "BG",
        ["BULGARIA"] = "BG",
        ["CY"] = "CY",
        ["CHIPRE"] = "CY",
        ["CYPRUS"] = "CY",
        ["CZ"] = "CZ",
        ["CHEQUIA"] = "CZ",
        ["REPUBLICA CHECA"] = "CZ",
        ["REPÚBLICA CHECA"] = "CZ",
        ["CZECH REPUBLIC"] = "CZ",
        ["DE"] = "DE",
        ["ALEMANIA"] = "DE",
        ["GERMANY"] = "DE",
        ["DK"] = "DK",
        ["DINAMARCA"] = "DK",
        ["DINAMARK"] = "DK",
        ["DENMARK"] = "DK",
        ["EE"] = "EE",
        ["ESTONIA"] = "EE",
        ["EL"] = "EL",
        ["GR"] = "EL",
        ["GRECIA"] = "EL",
        ["GREECE"] = "EL",
        ["ES"] = "ES",
        ["ESPANA"] = "ES",
        ["ESPAÑA"] = "ES",
        ["SPAIN"] = "ES",
        ["CATALUNYA"] = "ES",
        ["CATALUÑA"] = "ES",
        ["CATALONIA"] = "ES",
        ["EUSKADI"] = "ES",
        ["PAIS VASCO"] = "ES",
        ["PAÍS VASCO"] = "ES",
        ["FI"] = "FI",
        ["FINLANDIA"] = "FI",
        ["FINLAND"] = "FI",
        ["FR"] = "FR",
        ["FRANCIA"] = "FR",
        ["FRANCE"] = "FR",
        ["HR"] = "HR",
        ["CROACIA"] = "HR",
        ["CROATIA"] = "HR",
        ["HU"] = "HU",
        ["HUNGRIA"] = "HU",
        ["HUNGRÍA"] = "HU",
        ["HUNGARY"] = "HU",
        ["IE"] = "IE",
        ["IRLANDA"] = "IE",
        ["IRELAND"] = "IE",
        ["IT"] = "IT",
        ["ITALIA"] = "IT",
        ["ITALY"] = "IT",
        ["LT"] = "LT",
        ["LITUANIA"] = "LT",
        ["LITHUANIA"] = "LT",
        ["LU"] = "LU",
        ["LUXEMBURGO"] = "LU",
        ["LUXEMBOURG"] = "LU",
        ["LV"] = "LV",
        ["LETONIA"] = "LV",
        ["LATVIA"] = "LV",
        ["MT"] = "MT",
        ["MALTA"] = "MT",
        ["NL"] = "NL",
        ["PAISES BAJOS"] = "NL",
        ["PAÍSES BAJOS"] = "NL",
        ["HOLANDA"] = "NL",
        ["NETHERLANDS"] = "NL",
        ["PL"] = "PL",
        ["POLONIA"] = "PL",
        ["POLAND"] = "PL",
        ["PT"] = "PT",
        ["PORTUGAL"] = "PT",
        ["RO"] = "RO",
        ["RUMANIA"] = "RO",
        ["RUMANÍA"] = "RO",
        ["ROMANIA"] = "RO",
        ["SE"] = "SE",
        ["SUECIA"] = "SE",
        ["SWEDEN"] = "SE",
        ["SI"] = "SI",
        ["ESLOVENIA"] = "SI",
        ["SLOVENIA"] = "SI",
        ["SK"] = "SK",
        ["ESLOVAQUIA"] = "SK",
        ["SLOVAKIA"] = "SK"
    };

    private readonly MySqlConnectionFactory _connectionFactory;
    private readonly LegacyMySqlConnectionFactory _legacyConnectionFactory;
    private readonly ICompanyAccessService _companyAccessService;
    private readonly ICurrentUserContext _currentUserContext;
    private readonly ITenantContext _tenantContext;
    private readonly IActiveCompanyContext _activeCompanyContext;

    public MySqlIntrastatService(
        MySqlConnectionFactory connectionFactory,
        LegacyMySqlConnectionFactory legacyConnectionFactory,
        ICompanyAccessService companyAccessService,
        ICurrentUserContext currentUserContext,
        ITenantContext tenantContext,
        IActiveCompanyContext activeCompanyContext)
    {
        _connectionFactory = connectionFactory;
        _legacyConnectionFactory = legacyConnectionFactory;
        _companyAccessService = companyAccessService;
        _currentUserContext = currentUserContext;
        _tenantContext = tenantContext;
        _activeCompanyContext = activeCompanyContext;
    }

    public async Task<IntrastatPeriodDto?> GetLatestPeriodAsync(Guid tenantId, Guid companyId, CancellationToken cancellationToken = default)
    {
        if (_legacyConnectionFactory.IsConfigured)
        {
            return await GetLatestLegacyPeriodAsync(tenantId, companyId, cancellationToken);
        }

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
            SELECT si.issue_date,
                   COALESCE(c.PAIS, '') AS client_country,
                   COALESCE(si.client_tax_id, '') AS client_tax_id
            FROM sales_invoices si
            INNER JOIN sales_invoice_lines sil
              ON sil.invoice_id = si.invoice_id
             AND sil.tenant_id = si.tenant_id
             AND sil.company_id = si.company_id
            LEFT JOIN clients c
              ON c.CENTRO = @centerCode
             AND c.CODI = si.client_code
             AND COALESCE(c.is_deleted, 0) = 0
            WHERE si.tenant_id = @tenantId
              AND si.company_id = @companyId
              AND COALESCE(si.is_deleted, 0) = 0
              AND si.status <> 'Cancelled'
            ORDER BY si.issue_date DESC, si.invoice_number DESC, sil.line_number ASC;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@centerCode", centerCode);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var normalizedCountryCode = ResolveIntrastatCountryCode(
                reader.GetStringOrEmpty("client_country"),
                reader.GetStringOrEmpty("client_tax_id"));

            if (string.IsNullOrWhiteSpace(normalizedCountryCode) || !EuCountryCodes.Contains(normalizedCountryCode))
            {
                continue;
            }

            var issueDate = reader.GetDateTime(reader.GetOrdinal("issue_date"));
            return new IntrastatPeriodDto
            {
                Year = issueDate.Year,
                Month = issueDate.Month
            };
        }

        return null;
    }

    public async Task<IntrastatReportDto> GetReportAsync(Guid tenantId, Guid companyId, IntrastatFilter filter, CancellationToken cancellationToken = default)
    {
        if (_legacyConnectionFactory.IsConfigured)
        {
            return await GetLegacyReportAsync(tenantId, companyId, filter, cancellationToken);
        }

        if (!_connectionFactory.IsConfigured)
        {
            return new IntrastatReportDto();
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);
        var centerCode = await ResolveCompanyCenterCodeAsync(tenantId, companyId, cancellationToken);

        var safeMonth = Math.Clamp(filter.Month, 1, 12);
        var safeYear = filter.Year <= 2000 ? DateTime.Today.Year : filter.Year;
        var page = Math.Max(filter.Page, 1);
        var pageSize = Math.Clamp(filter.PageSize, 10, 500);
        var search = filter.Search?.Trim() ?? string.Empty;

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT si.invoice_series,
                   si.invoice_number,
                   si.client_code,
                   si.client_name,
                   COALESCE(si.client_tax_id, '') AS client_tax_id,
                   COALESCE(c.PAIS, '') AS client_country,
                   COALESCE(si.legacy_center_code, @centerCode) AS legacy_center_code,
                   si.issue_date,
                   COALESCE(si.origin, 'saas') AS origin,
                   sil.line_number,
                   COALESCE(sil.item_code, '') AS item_code,
                   COALESCE(sil.description, '') AS description,
                   COALESCE(sil.quantity, 0) AS quantity,
                   COALESCE(sil.line_subtotal, 0) AS line_subtotal,
                   COALESCE(sil.tax_amount, 0) AS tax_amount,
                   COALESCE(sil.line_total, 0) AS line_total
            FROM sales_invoices si
            INNER JOIN sales_invoice_lines sil
              ON sil.invoice_id = si.invoice_id
             AND sil.tenant_id = si.tenant_id
             AND sil.company_id = si.company_id
            LEFT JOIN clients c
              ON c.CENTRO = @centerCode
             AND c.CODI = si.client_code
             AND COALESCE(c.is_deleted, 0) = 0
            WHERE si.tenant_id = @tenantId
              AND si.company_id = @companyId
              AND COALESCE(si.is_deleted, 0) = 0
              AND si.status <> 'Cancelled'
              AND YEAR(si.issue_date) = @year
              AND MONTH(si.issue_date) = @month
            ORDER BY si.issue_date DESC, si.invoice_number DESC, sil.line_number ASC;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@year", safeYear);
        command.Parameters.AddWithValue("@month", safeMonth);

        var allLines = new List<IntrastatLineDto>();
        MySqlConnection? legacyConnection = null;
        Dictionary<string, string>? legacyCountryCache = null;
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var clientCountry = reader.GetStringOrEmpty("client_country");
            if (string.IsNullOrWhiteSpace(clientCountry) && _legacyConnectionFactory.IsConfigured)
            {
                legacyConnection ??= await _legacyConnectionFactory.OpenConnectionAsync(cancellationToken);
                legacyCountryCache ??= new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                clientCountry = await ResolveLegacyClientCountryAsync(
                    legacyConnection,
                    legacyCountryCache,
                    reader.GetStringOrEmpty("legacy_center_code"),
                    reader.GetInt32(reader.GetOrdinal("client_code")),
                    cancellationToken);
            }

            var normalizedCountryCode = ResolveIntrastatCountryCode(
                clientCountry,
                reader.GetStringOrEmpty("client_tax_id"));

            if (string.IsNullOrWhiteSpace(normalizedCountryCode) || !EuCountryCodes.Contains(normalizedCountryCode))
            {
                continue;
            }

            var itemCode = reader.GetStringOrEmpty("item_code");
            var description = reader.GetStringOrEmpty("description");
            var intrastatCode = NormalizeIntrastatCode(itemCode, description);

            allLines.Add(new IntrastatLineDto
            {
                InvoiceSeries = reader.GetStringOrEmpty("invoice_series"),
                InvoiceNumber = reader.GetInt32(reader.GetOrdinal("invoice_number")),
                ClientCode = reader.GetInt32(reader.GetOrdinal("client_code")),
                ClientName = reader.GetStringOrEmpty("client_name"),
                ClientTaxId = reader.GetStringOrEmpty("client_tax_id"),
                IssueDate = reader.GetDateTime(reader.GetOrdinal("issue_date")),
                Origin = reader.GetStringOrEmpty("origin"),
                LineNumber = reader.GetInt32(reader.GetOrdinal("line_number")),
                ItemCode = itemCode,
                Description = description,
                CountryCode = normalizedCountryCode,
                CountryName = CountryNames.TryGetValue(normalizedCountryCode, out var countryName) ? countryName : normalizedCountryCode,
                IntrastatCode = intrastatCode,
                Quantity = reader.GetDecimalOrDefault("quantity"),
                NetAmount = reader.GetDecimalOrDefault("line_subtotal"),
                TaxAmount = reader.GetDecimalOrDefault("tax_amount"),
                GrossAmount = reader.GetDecimalOrDefault("line_total")
            });
        }
        if (legacyConnection is not null)
        {
            await legacyConnection.DisposeAsync();
        }

        var filteredLines = allLines
            .Where(line => !filter.OnlyClassified || line.IsClassified)
            .Where(line => MatchesSearch(line, search))
            .ToList();

        var orderedLines = OrderLines(filteredLines, filter).ToList();
        var pagedLines = orderedLines
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToArray();

        var summary = filteredLines
            .GroupBy(line => new { line.CountryCode, line.CountryName, Code = line.IsClassified ? line.IntrastatCode : "SIN CLASIFICAR" })
            .Select(group => new IntrastatCountrySummaryDto
            {
                CountryCode = group.Key.CountryCode,
                CountryName = group.Key.CountryName,
                IntrastatCode = group.Key.Code,
                LinesCount = group.Count(),
                TotalQuantity = decimal.Round(group.Sum(item => item.Quantity), 3, MidpointRounding.AwayFromZero),
                TotalNetAmount = decimal.Round(group.Sum(item => item.NetAmount), 2, MidpointRounding.AwayFromZero),
                TotalGrossAmount = decimal.Round(group.Sum(item => item.GrossAmount), 2, MidpointRounding.AwayFromZero)
            })
            .OrderBy(item => item.CountryCode, StringComparer.OrdinalIgnoreCase)
            .ThenBy(item => item.IntrastatCode, StringComparer.OrdinalIgnoreCase)
            .ToArray();

        return new IntrastatReportDto
        {
            Items = pagedLines,
            Summary = summary,
            TotalCount = filteredLines.Count,
            CountriesCount = filteredLines.Select(item => item.CountryCode).Distinct(StringComparer.OrdinalIgnoreCase).Count(),
            ClassifiedLinesCount = filteredLines.Count(item => item.IsClassified),
            UnclassifiedLinesCount = filteredLines.Count(item => !item.IsClassified),
            TotalNetAmount = decimal.Round(filteredLines.Sum(item => item.NetAmount), 2, MidpointRounding.AwayFromZero),
            TotalGrossAmount = decimal.Round(filteredLines.Sum(item => item.GrossAmount), 2, MidpointRounding.AwayFromZero)
        };
    }

    private async Task<IntrastatPeriodDto?> GetLatestLegacyPeriodAsync(Guid tenantId, Guid companyId, CancellationToken cancellationToken)
    {
        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);
        var centerCode = await ResolveCompanyCenterCodeAsync(tenantId, companyId, cancellationToken);

        await using var connection = await _legacyConnectionFactory.OpenConnectionAsync(cancellationToken);
        return await TryGetLatestLegacyPeriodAsync(connection, centerCode, useCenterFilter: true, cancellationToken)
            ?? await TryGetLatestLegacyPeriodAsync(connection, centerCode, useCenterFilter: false, cancellationToken);
    }

    private async Task<IntrastatReportDto> GetLegacyReportAsync(Guid tenantId, Guid companyId, IntrastatFilter filter, CancellationToken cancellationToken)
    {
        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);
        var centerCode = await ResolveCompanyCenterCodeAsync(tenantId, companyId, cancellationToken);

        var safeMonth = Math.Clamp(filter.Month, 1, 12);
        var safeYear = filter.Year <= 2000 ? DateTime.Today.Year : filter.Year;
        var page = Math.Max(filter.Page, 1);
        var pageSize = Math.Clamp(filter.PageSize, 10, 500);
        var search = filter.Search?.Trim() ?? string.Empty;

        await using var connection = await _legacyConnectionFactory.OpenConnectionAsync(cancellationToken);
        var allLines = await LoadLegacyLinesAsync(connection, centerCode, safeYear, safeMonth, useCenterFilter: true, cancellationToken);
        var useFallbackScope = allLines.Count == 0;
        if (useFallbackScope)
        {
            allLines = await LoadLegacyLinesAsync(connection, centerCode, safeYear, safeMonth, useCenterFilter: false, cancellationToken);
        }

        var matrixRows = await LoadLegacyMatrixRowsAsync(connection, centerCode, safeYear, safeMonth, useCenterFilter: !useFallbackScope, cancellationToken);
        var hasClientsByCountry = await LoadLegacyCountryAvailabilityAsync(connection, centerCode, useCenterFilter: !useFallbackScope, cancellationToken);

        return BuildReportFromLines(allLines, filter, search, page, pageSize, BuildLegacyMatrixRows(matrixRows, hasClientsByCountry));
    }

    private IntrastatReportDto BuildReportFromLines(
        List<IntrastatLineDto> allLines,
        IntrastatFilter filter,
        string search,
        int page,
        int pageSize,
        IReadOnlyCollection<IntrastatMatrixRowDto>? matrixRows = null)
    {
        var filteredLines = allLines
            .Where(line => !filter.OnlyClassified || line.IsClassified)
            .Where(line => MatchesSearch(line, search))
            .ToList();

        var orderedLines = OrderLines(filteredLines, filter).ToList();
        var pagedLines = orderedLines
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToArray();

        var summary = filteredLines
            .GroupBy(line => new { line.CountryCode, line.CountryName, Code = line.IsClassified ? line.IntrastatCode : "SIN CLASIFICAR" })
            .Select(group => new IntrastatCountrySummaryDto
            {
                CountryCode = group.Key.CountryCode,
                CountryName = group.Key.CountryName,
                IntrastatCode = group.Key.Code,
                LinesCount = group.Count(),
                TotalQuantity = decimal.Round(group.Sum(item => item.Quantity), 3, MidpointRounding.AwayFromZero),
                TotalNetAmount = decimal.Round(group.Sum(item => item.NetAmount), 2, MidpointRounding.AwayFromZero),
                TotalGrossAmount = decimal.Round(group.Sum(item => item.GrossAmount), 2, MidpointRounding.AwayFromZero)
            })
            .OrderBy(item => item.CountryCode, StringComparer.OrdinalIgnoreCase)
            .ThenBy(item => item.IntrastatCode, StringComparer.OrdinalIgnoreCase)
            .ToArray();

        return new IntrastatReportDto
        {
            Items = pagedLines,
            Summary = summary,
            MatrixRows = matrixRows ?? BuildLegacyMatrixRows(Array.Empty<LegacyMatrixAggregateRow>(), new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase)),
            MatrixNcCodes = LegacyMatrixNcCodes,
            TotalCount = filteredLines.Count,
            CountriesCount = filteredLines.Select(item => item.CountryCode).Distinct(StringComparer.OrdinalIgnoreCase).Count(),
            ClassifiedLinesCount = filteredLines.Count(item => item.IsClassified),
            UnclassifiedLinesCount = filteredLines.Count(item => !item.IsClassified),
            TransportAmount = decimal.Round(filteredLines.Where(item => item.IsTransportCharge).Sum(item => item.NetAmount), 2, MidpointRounding.AwayFromZero),
            SalesAmount = decimal.Round(filteredLines.Where(item => !item.IsTransportCharge).Sum(item => item.NetAmount), 2, MidpointRounding.AwayFromZero),
            TotalWeightKg = decimal.Round(filteredLines.Sum(item => item.TotalWeightKg), 2, MidpointRounding.AwayFromZero),
            TotalWithTransportAmount = decimal.Round(filteredLines.Sum(item => item.NetAmount), 2, MidpointRounding.AwayFromZero),
            TotalNetAmount = decimal.Round(filteredLines.Sum(item => item.NetAmount), 2, MidpointRounding.AwayFromZero),
            TotalGrossAmount = decimal.Round(filteredLines.Sum(item => item.GrossAmount), 2, MidpointRounding.AwayFromZero)
        };
    }

    private async Task<IntrastatPeriodDto?> TryGetLatestLegacyPeriodAsync(
        MySqlConnection connection,
        string centerCode,
        bool useCenterFilter,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            $"""
            SELECT YEAR(f.DATA) AS year_value,
                   MONTH(f.DATA) AS month_value
            FROM factur f
            INNER JOIN clients c
              ON c.CODI = f.CLIENT
            WHERE f.DOCUMENT = 'F'
              {(useCenterFilter ? "AND f.CENTRO = @centerCode" : string.Empty)}
              AND LEFT(COALESCE(c.NIF, ''), 2) IN ('AT','BE','BG','CY','CZ','DE','DK','EE','EL','FI','FR','HR','HU','IE','IT','LT','LU','LV','MT','NL','PL','PT','RO','SE','SI','SK')
            ORDER BY YEAR(f.DATA) DESC, MONTH(f.DATA) DESC
            LIMIT 1;
            """;
        if (useCenterFilter)
        {
            command.Parameters.AddWithValue("@centerCode", centerCode);
        }

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        return new IntrastatPeriodDto
        {
            Year = reader.GetInt32(reader.GetOrdinal("year_value")),
            Month = reader.GetInt32(reader.GetOrdinal("month_value"))
        };
    }

    private static async Task<List<IntrastatLineDto>> LoadLegacyLinesAsync(
        MySqlConnection connection,
        string centerCode,
        int year,
        int month,
        bool useCenterFilter,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            $"""
            SELECT f.FRA AS invoice_number,
                   COALESCE(c.NOM, CONCAT('Cliente ', CAST(f.CLIENT AS CHAR))) AS client_name,
                   COALESCE(c.NIF, '') AS client_tax_id,
                   f.DATA AS issue_date,
                   COALESCE(df.NLINEA, 0) AS line_number,
                   COALESCE(df.DESCRI, '') AS description,
                   COALESCE(df.COMPOSICIO, '') AS composition,
                   COALESCE(df.TALLA, '') AS size_value,
                   COALESCE(df.UNITATS, 0) AS quantity,
                   COALESCE(df.PESO, 0) AS unit_weight,
                   COALESCE(df.IMPORT, 0) AS line_import,
                   COALESCE(df.NCCODE, '') AS nc_code,
                   COALESCE(df.MOSTRA, '') AS item_code,
                   COALESCE(c.DTOFORMA, 0) AS payment_discount_percent,
                   COALESCE(f.DTE1, 0) AS discount_amount
            FROM factur f
            INNER JOIN dfactu df
              ON df.FRA = f.FRA
             AND df.DOCUMENT = f.DOCUMENT
            INNER JOIN clients c
              ON c.CODI = f.CLIENT
            WHERE f.DOCUMENT = 'F'
              {(useCenterFilter ? "AND f.CENTRO = @centerCode" : string.Empty)}
              AND YEAR(f.DATA) = @year
              AND MONTH(f.DATA) = @month
              AND LEFT(COALESCE(c.NIF, ''), 2) IN ('AT','BE','BG','CY','CZ','DE','DK','EE','EL','FI','FR','HR','HU','IE','IT','LT','LU','LV','MT','NL','PL','PT','RO','SE','SI','SK')
            ORDER BY f.FRA, df.NLINEA;
            """;
        if (useCenterFilter)
        {
            command.Parameters.AddWithValue("@centerCode", centerCode);
        }
        command.Parameters.AddWithValue("@year", year);
        command.Parameters.AddWithValue("@month", month);

        var result = new List<IntrastatLineDto>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var description = reader.GetStringOrEmpty("description");
            var lineImport = reader.GetDecimalOrDefault("line_import");
            var discountAmount = reader.GetDecimalOrDefault("discount_amount");
            var paymentDiscountPercent = reader.GetDecimalOrDefault("payment_discount_percent");
            var isTransportCharge = string.Equals(description, TransportDescription, StringComparison.OrdinalIgnoreCase);
            var effectiveAmount = isTransportCharge
                ? decimal.Round(lineImport, 2, MidpointRounding.AwayFromZero)
                : discountAmount != 0
                    ? decimal.Round(lineImport * ((100m - paymentDiscountPercent) / 100m), 2, MidpointRounding.AwayFromZero)
                    : decimal.Round(lineImport, 2, MidpointRounding.AwayFromZero);
            var totalWeightKg = decimal.Round(
                reader.GetDecimalOrDefault("unit_weight") * reader.GetDecimalOrDefault("quantity") / 1000m,
                2,
                MidpointRounding.AwayFromZero);
            var clientTaxId = reader.GetStringOrEmpty("client_tax_id");
            var countryCode = NormalizeCountryCode(clientTaxId);

            result.Add(new IntrastatLineDto
            {
                InvoiceNumber = reader.GetInt32(reader.GetOrdinal("invoice_number")),
                ClientName = reader.GetStringOrEmpty("client_name"),
                ClientTaxId = clientTaxId,
                IssueDate = reader.GetDateTime(reader.GetOrdinal("issue_date")),
                LineNumber = reader.GetInt32(reader.GetOrdinal("line_number")),
                Description = description,
                Composition = reader.GetStringOrEmpty("composition"),
                Size = reader.GetStringOrEmpty("size_value"),
                ItemCode = reader.GetStringOrEmpty("item_code"),
                CountryCode = countryCode,
                CountryName = CountryNames.TryGetValue(countryCode, out var countryName) ? countryName : countryCode,
                IntrastatCode = NormalizeNcCodeCandidate(reader.GetStringOrEmpty("nc_code")),
                Quantity = reader.GetDecimalOrDefault("quantity"),
                UnitWeight = reader.GetDecimalOrDefault("unit_weight"),
                TotalWeightKg = totalWeightKg,
                DiscountAmount = discountAmount != 0 && !isTransportCharge
                    ? decimal.Round(lineImport - effectiveAmount, 2, MidpointRounding.AwayFromZero)
                    : 0m,
                PaymentDiscountPercent = paymentDiscountPercent,
                NetAmount = effectiveAmount,
                TaxAmount = 0m,
                GrossAmount = effectiveAmount,
                Origin = "legacy",
                IsTransportCharge = isTransportCharge
            });
        }

        return result;
    }

    private static async Task<IReadOnlyDictionary<string, bool>> LoadLegacyCountryAvailabilityAsync(
        MySqlConnection connection,
        string centerCode,
        bool useCenterFilter,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            $"""
            SELECT DISTINCT LEFT(COALESCE(clients.NIF, ''), 2) AS country_code
            FROM clients
            WHERE LEFT(COALESCE(clients.NIF, ''), 2) IN ('AT','BE','BG','CY','CZ','DE','DK','EE','EL','FI','FR','HR','HU','IE','IT','LT','LU','LV','MT','NL','PL','PT','RO','SE','SI','SK')
              {(useCenterFilter ? "AND clients.CENTRO = @centerCode" : string.Empty)};
            """;
        if (useCenterFilter)
        {
            command.Parameters.AddWithValue("@centerCode", centerCode);
        }

        var result = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var countryCode = reader.GetStringOrEmpty("country_code");
            if (!string.IsNullOrWhiteSpace(countryCode))
            {
                result[countryCode] = true;
            }
        }

        return result;
    }

    private static async Task<IReadOnlyCollection<LegacyMatrixAggregateRow>> LoadLegacyMatrixRowsAsync(
        MySqlConnection connection,
        string centerCode,
        int year,
        int month,
        bool useCenterFilter,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            $"""
            SELECT LEFT(COALESCE(c.NIF, ''), 2) AS country_code,
                   COALESCE(df.NCCODE, '') AS nc_code,
                   ROUND(SUM(df.PESO * df.UNITATS) / 1000, 2) AS total_weight_kg,
                   ROUND(SUM(df.IMPORT) - SUM(IFNULL(f.DTE1, 0)), 2) AS total_amount
            FROM factur f
            INNER JOIN dfactu df
              ON f.FRA = df.FRA
             AND f.DOCUMENT = df.DOCUMENT
            INNER JOIN clients c
              ON f.CLIENT = c.CODI
            WHERE f.DOCUMENT = 'F'
              AND COALESCE(df.DESCRI, '') <> @transportDescription
              {(useCenterFilter ? "AND f.CENTRO = @centerCode" : string.Empty)}
              AND YEAR(f.DATA) = @year
              AND MONTH(f.DATA) = @month
              AND LEFT(COALESCE(c.NIF, ''), 2) IN ('AT','BE','BG','CY','CZ','DE','DK','EE','EL','FI','FR','HR','HU','IE','IT','LT','LU','LV','MT','NL','PL','PT','RO','SE','SI','SK')
            GROUP BY LEFT(COALESCE(c.NIF, ''), 2), COALESCE(df.NCCODE, '')
            ORDER BY LEFT(COALESCE(c.NIF, ''), 2), COALESCE(df.NCCODE, '');
            """;
        command.Parameters.AddWithValue("@transportDescription", TransportDescription);
        if (useCenterFilter)
        {
            command.Parameters.AddWithValue("@centerCode", centerCode);
        }
        command.Parameters.AddWithValue("@year", year);
        command.Parameters.AddWithValue("@month", month);

        var rows = new List<LegacyMatrixAggregateRow>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var ncCode = NormalizeNcCodeCandidate(reader.GetStringOrEmpty("nc_code"));
            if (string.IsNullOrWhiteSpace(ncCode) || !LegacyMatrixNcCodes.Contains(ncCode, StringComparer.OrdinalIgnoreCase))
            {
                continue;
            }

            rows.Add(new LegacyMatrixAggregateRow(
                reader.GetStringOrEmpty("country_code"),
                ncCode,
                reader.GetDecimalOrDefault("total_weight_kg"),
                reader.GetDecimalOrDefault("total_amount")));
        }

        return rows;
    }

    private static IReadOnlyCollection<IntrastatMatrixRowDto> BuildLegacyMatrixRows(
        IReadOnlyCollection<LegacyMatrixAggregateRow> aggregates,
        IReadOnlyDictionary<string, bool> hasClientsByCountry)
    {
        return LegacyMatrixCountries
            .Select(country =>
            {
                var weightByCode = LegacyMatrixNcCodes.ToDictionary(code => code, _ => 0m, StringComparer.OrdinalIgnoreCase);
                var amountByCode = LegacyMatrixNcCodes.ToDictionary(code => code, _ => 0m, StringComparer.OrdinalIgnoreCase);

                foreach (var aggregate in aggregates.Where(item => string.Equals(item.CountryCode, country.Code, StringComparison.OrdinalIgnoreCase)))
                {
                    weightByCode[aggregate.NcCode] = aggregate.TotalWeightKg;
                    amountByCode[aggregate.NcCode] = aggregate.TotalAmount;
                }

                return new IntrastatMatrixRowDto
                {
                    CountryCode = country.Code,
                    CountryName = country.Name,
                    HasClients = hasClientsByCountry.ContainsKey(country.Code),
                    IsDomesticReference = country.IsDomesticReference,
                    WeightByNcCode = weightByCode,
                    AmountByNcCode = amountByCode,
                    TotalWeightKg = decimal.Round(weightByCode.Values.Sum(), 2, MidpointRounding.AwayFromZero),
                    TotalInvoiceAmount = decimal.Round(amountByCode.Values.Sum(), 2, MidpointRounding.AwayFromZero)
                };
            })
            .ToArray();
    }

    private static IEnumerable<IntrastatLineDto> OrderLines(IEnumerable<IntrastatLineDto> source, IntrastatFilter filter)
    {
        Func<IntrastatLineDto, object> keySelector = filter.SortColumn switch
        {
            nameof(IntrastatLineDto.InvoiceNumber) => item => item.InvoiceNumber,
            nameof(IntrastatLineDto.ClientName) => item => item.ClientName,
            nameof(IntrastatLineDto.CountryCode) => item => item.CountryCode,
            nameof(IntrastatLineDto.IntrastatCode) => item => item.IntrastatCode,
            nameof(IntrastatLineDto.Quantity) => item => item.Quantity,
            nameof(IntrastatLineDto.TotalWeightKg) => item => item.TotalWeightKg,
            nameof(IntrastatLineDto.NetAmount) => item => item.NetAmount,
            nameof(IntrastatLineDto.GrossAmount) => item => item.GrossAmount,
            nameof(IntrastatLineDto.ItemCode) => item => item.ItemCode,
            _ => item => item.IssueDate
        };

        var ordered = filter.SortDescending
            ? source.OrderByDescending(keySelector)
            : source.OrderBy(keySelector);

        return ordered
            .ThenByDescending(item => item.IssueDate)
            .ThenByDescending(item => item.InvoiceNumber)
            .ThenBy(item => item.LineNumber);
    }

    private static bool MatchesSearch(IntrastatLineDto line, string search)
    {
        if (string.IsNullOrWhiteSpace(search))
        {
            return true;
        }

        return line.DisplayInvoiceNumber.Contains(search, StringComparison.OrdinalIgnoreCase)
               || line.ClientName.Contains(search, StringComparison.OrdinalIgnoreCase)
               || line.ClientTaxId.Contains(search, StringComparison.OrdinalIgnoreCase)
               || line.CountryCode.Contains(search, StringComparison.OrdinalIgnoreCase)
               || line.CountryName.Contains(search, StringComparison.OrdinalIgnoreCase)
               || line.ItemCode.Contains(search, StringComparison.OrdinalIgnoreCase)
               || line.Description.Contains(search, StringComparison.OrdinalIgnoreCase)
               || line.Composition.Contains(search, StringComparison.OrdinalIgnoreCase)
               || line.Size.Contains(search, StringComparison.OrdinalIgnoreCase)
               || line.IntrastatCode.Contains(search, StringComparison.OrdinalIgnoreCase);
    }

    private static string ResolveIntrastatCountryCode(string country, string taxId)
    {
        var normalizedCountry = NormalizeCountry(country);
        if (!string.IsNullOrWhiteSpace(normalizedCountry))
        {
            return normalizedCountry;
        }

        return NormalizeCountryCode(taxId);
    }

    private static string NormalizeCountry(string country)
    {
        if (string.IsNullOrWhiteSpace(country))
        {
            return string.Empty;
        }

        var normalized = country.Trim().ToUpperInvariant();
        if (CountryAliases.TryGetValue(normalized, out var mapped))
        {
            return mapped;
        }

        return string.Empty;
    }

    private static string NormalizeCountryCode(string taxId)
    {
        if (string.IsNullOrWhiteSpace(taxId))
        {
            return string.Empty;
        }

        var prefix = new string(taxId.Trim().ToUpperInvariant().Take(2).ToArray());
        return prefix switch
        {
            "GR" => "EL",
            _ => prefix
        };
    }

    private static string NormalizeIntrastatCode(string itemCode, string description)
    {
        var fromItemCode = NormalizeNcCodeCandidate(itemCode);
        if (!string.IsNullOrWhiteSpace(fromItemCode))
        {
            return fromItemCode;
        }

        var match = NcCodeRegex.Match(description ?? string.Empty);
        if (match.Success)
        {
            return NormalizeNcCodeCandidate(match.Groups["code"].Value);
        }

        return string.Empty;
    }

    private static string NormalizeNcCodeCandidate(string rawValue)
    {
        if (string.IsNullOrWhiteSpace(rawValue))
        {
            return string.Empty;
        }

        var digits = new string(rawValue.Where(char.IsDigit).ToArray());
        if (digits.Length != 8)
        {
            return string.Empty;
        }

        return string.Create(CultureInfo.InvariantCulture, $"{digits[..4]} {digits.Substring(4, 2)} {digits.Substring(6, 2)}");
    }

    private static async Task<string> ResolveLegacyClientCountryAsync(
        MySqlConnection connection,
        IDictionary<string, string> cache,
        string centerCode,
        int clientCode,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(centerCode) || clientCode <= 0)
        {
            return string.Empty;
        }

        var cacheKey = $"{centerCode.Trim().ToUpperInvariant()}::{clientCode}";
        if (cache.TryGetValue(cacheKey, out var cached))
        {
            return cached;
        }

        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT COALESCE(PAIS, '') AS PAIS
            FROM clients
            WHERE CENTRO = @centerCode
              AND CODI = @clientCode
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode.Trim().ToUpperInvariant());
        command.Parameters.AddWithValue("@clientCode", clientCode);

        var value = await command.ExecuteScalarAsync(cancellationToken);
        var country = value?.ToString()?.Trim() ?? string.Empty;
        cache[cacheKey] = country;
        return country;
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

    private sealed record LegacyCountryDefinition(string Code, string Name, bool IsDomesticReference);
    private sealed record LegacyMatrixAggregateRow(string CountryCode, string NcCode, decimal TotalWeightKg, decimal TotalAmount);
}
