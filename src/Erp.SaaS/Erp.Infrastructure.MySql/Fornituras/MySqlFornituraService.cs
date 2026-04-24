using Erp.Application.Auditing;
using Erp.Application.Companies;
using Erp.Application.Contexts;
using Erp.Application.Fornituras;
using Erp.Application.LegacySync;
using Erp.Domain.Common;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Support;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Fornituras;

public sealed class MySqlFornituraService : IFornituraQueries, IFornituraService
{
    private readonly MySqlConnectionFactory _connectionFactory;
    private readonly IAuditLogService _auditLogService;
    private readonly ICompanyAccessService _companyAccessService;
    private readonly ICurrentUserContext _currentUserContext;
    private readonly ITenantContext _tenantContext;
    private readonly IActiveCompanyContext _activeCompanyContext;

    public MySqlFornituraService(
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

    public async Task<FornituraSearchResultDto> SearchAsync(Guid tenantId, Guid companyId, FornituraFilter filter, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return new FornituraSearchResultDto();
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
            FROM forni
            WHERE CENTRO = @centerCode
              AND is_deleted = 0
              AND (
                    @search = ''
                    OR CODI LIKE @likeSearch
                    OR DESCRI LIKE @likeSearch
                    OR REFPRO LIKE @likeSearch
                    OR MODEL LIKE @likeSearch
                    OR SERIE LIKE @likeSearch
                    OR TEMPORADA LIKE @likeSearch
                  );
            """;
        countCommand.Parameters.AddWithValue("@centerCode", centerCode);
        countCommand.Parameters.AddWithValue("@search", search);
        countCommand.Parameters.AddWithValue("@likeSearch", likeSearch);
        var totalCount = Convert.ToInt32(await countCommand.ExecuteScalarAsync(cancellationToken));
        if (totalCount == 0)
        {
            return new FornituraSearchResultDto { TotalCount = 0 };
        }

        var items = new List<FornituraListItemDto>();
        await using var command = connection.CreateCommand();
        command.CommandText =
            $"""
            SELECT CODI, CENTRO, DESCRI, PREU, PROVE, CLIENT, MODEL, SERIE, TEMPORADA
            FROM forni
            WHERE CENTRO = @centerCode
              AND is_deleted = 0
              AND (
                    @search = ''
                    OR CODI LIKE @likeSearch
                    OR DESCRI LIKE @likeSearch
                    OR REFPRO LIKE @likeSearch
                    OR MODEL LIKE @likeSearch
                    OR SERIE LIKE @likeSearch
                    OR TEMPORADA LIKE @likeSearch
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
            items.Add(new FornituraListItemDto
            {
                Code = reader.GetStringOrEmpty("CODI"),
                CompanyCenterCode = reader.GetStringOrEmpty("CENTRO"),
                Description = reader.GetStringOrEmpty("DESCRI"),
                UnitPrice = reader.GetDecimalOrDefault("PREU"),
                SupplierCode = reader.GetInt32OrDefault("PROVE"),
                ClientCode = reader.GetInt32OrDefault("CLIENT"),
                Model = reader.GetStringOrEmpty("MODEL"),
                Series = reader.GetStringOrEmpty("SERIE"),
                Season = reader.GetStringOrEmpty("TEMPORADA")
            });
        }

        return new FornituraSearchResultDto
        {
            Items = items,
            TotalCount = totalCount
        };
    }

    public async Task<FornituraDetailDto?> GetByCodeAsync(Guid tenantId, Guid companyId, string code, CancellationToken cancellationToken = default)
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
            SELECT CODI, CENTRO, DESCRI, PREU, PROVE, REFPRO, CLIENT, MODEL, SERIE, TEMPORADA
            FROM forni
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

        var detail = new FornituraDetailDto
        {
            Code = reader.GetStringOrEmpty("CODI"),
            CompanyCenterCode = reader.GetStringOrEmpty("CENTRO"),
            Description = reader.GetStringOrEmpty("DESCRI"),
            UnitPrice = reader.GetDecimalOrDefault("PREU"),
            SupplierCode = reader.GetInt32OrDefault("PROVE"),
            SupplierReference = reader.GetStringOrEmpty("REFPRO"),
            ClientCode = reader.GetInt32OrDefault("CLIENT"),
            Model = reader.GetStringOrEmpty("MODEL"),
            Series = reader.GetStringOrEmpty("SERIE"),
            Season = reader.GetStringOrEmpty("TEMPORADA")
        };

        await reader.CloseAsync();
        detail.Variants = await LoadVariantsAsync(connection, centerCode, detail.Code, cancellationToken);
        return detail;
    }

    public async Task<string> SaveAsync(SaveFornituraCommand command, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return string.Empty;
        }

        await EnsureCompanyAccessAsync(command.TenantId, command.CompanyId, cancellationToken);
        EnsureTenantWriteAccess();
        Validate(command);
        var centerCode = await ResolveCompanyCenterCodeAsync(command.TenantId, command.CompanyId, cancellationToken);
        FornituraDetailDto? previous = null;
        if (command.IsNew)
        {
            if (!string.IsNullOrWhiteSpace(command.Code))
            {
                var duplicate = await GetByCodeAsync(command.TenantId, command.CompanyId, command.Code, cancellationToken);
                if (duplicate is not null)
                {
                    throw new InvalidOperationException("Ya existe una fornitura con este código.");
                }
            }
        }
        else
        {
            previous = await GetByCodeAsync(command.TenantId, command.CompanyId, command.Code!, cancellationToken);
            if (previous is null)
            {
                throw new InvalidOperationException("No se ha encontrado la fornitura que intentas modificar.");
            }
        }

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var code = string.IsNullOrWhiteSpace(command.Code)
            ? await GetNextCodeAsync(connection, centerCode, cancellationToken)
            : command.Code.Trim().ToUpperInvariant();

        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        if (!command.IsNew)
        {
            await using var updateCommand = connection.CreateCommand();
            updateCommand.Transaction = transaction;
            updateCommand.CommandText =
                """
                UPDATE forni
                SET DESCRI = @description,
                    PREU = @unitPrice,
                    PROVE = @supplierCode,
                    REFPRO = @supplierReference,
                    CLIENT = @clientCode,
                    MODEL = @model,
                    SERIE = @series,
                    TEMPORADA = @season,
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
                throw new InvalidOperationException("No se ha podido actualizar la fornitura.");
            }

            await ReplaceVariantsAsync(connection, transaction, centerCode, code, command.Variants, cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            await AuditAsync(command.TenantId, command.CompanyId, code, previous!, command, cancellationToken);
            return code;
        }

        await using var insertCommand = connection.CreateCommand();
        insertCommand.Transaction = transaction;
        insertCommand.CommandText =
            """
            INSERT INTO forni (CODI, DESCRI, PREU, PROVE, REFPRO, CLIENT, MODEL, SERIE, TEMPORADA, CENTRO, origin, is_deleted, synced_utc)
            VALUES (@code, @description, @unitPrice, @supplierCode, @supplierReference, @clientCode, @model, @series, @season, @centerCode, 'local', 0, NULL);
            """;
        FillSaveParameters(insertCommand, centerCode, code, command);
        await insertCommand.ExecuteNonQueryAsync(cancellationToken);

        await ReplaceVariantsAsync(connection, transaction, centerCode, code, command.Variants, cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = command.TenantId,
            CompanyId = command.CompanyId,
            UserId = _currentUserContext.UserId,
            Action = "FornituraCreated",
            EntityName = "Fornitura",
            EntityId = code,
            Details = $"Fornitura {code} creada: {command.Description}; variantes={command.Variants.Count}"
        }, cancellationToken);

        return code;
    }

    public async Task DeleteAsync(Guid tenantId, Guid companyId, string code, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return;
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);
        EnsureTenantWriteAccess();
        var centerCode = await ResolveCompanyCenterCodeAsync(tenantId, companyId, cancellationToken);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        await using (var detailDelete = connection.CreateCommand())
        {
            detailDelete.Transaction = transaction;
            detailDelete.CommandText =
                """
                DELETE FROM forni_detail
                WHERE CENTRO = @centerCode
                  AND FORNI_CODI = @code;
                """;
            detailDelete.Parameters.AddWithValue("@centerCode", centerCode);
            detailDelete.Parameters.AddWithValue("@code", code);
            await detailDelete.ExecuteNonQueryAsync(cancellationToken);
        }

        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            UPDATE forni
            SET origin = 'local',
                is_deleted = 1,
                synced_utc = NULL
            WHERE CENTRO = @centerCode
              AND CODI = @code;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@code", code);
        var affected = await command.ExecuteNonQueryAsync(cancellationToken);
        if (affected == 0)
        {
            throw new InvalidOperationException("No se ha encontrado la fornitura a eliminar.");
        }

        await transaction.CommitAsync(cancellationToken);

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = tenantId,
            CompanyId = companyId,
            UserId = _currentUserContext.UserId,
            Action = "FornituraDeleted",
            EntityName = "Fornitura",
            EntityId = code,
            Details = $"Fornitura {code} eliminada en local."
        }, cancellationToken);
    }

    private static void FillSaveParameters(MySqlCommand command, string centerCode, string code, SaveFornituraCommand model)
    {
        command.Parameters.AddWithValue("@code", code);
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@description", model.Description);
        command.Parameters.AddWithValue("@unitPrice", model.UnitPrice);
        command.Parameters.AddWithValue("@supplierCode", model.SupplierCode);
        command.Parameters.AddWithValue("@supplierReference", DbValue(model.SupplierReference));
        command.Parameters.AddWithValue("@clientCode", model.ClientCode);
        command.Parameters.AddWithValue("@model", DbValue(model.Model));
        command.Parameters.AddWithValue("@series", DbValue(model.Series));
        command.Parameters.AddWithValue("@season", DbValue(model.Season));
    }

    private static async Task<List<FornituraVariantDto>> LoadVariantsAsync(
        MySqlConnection connection,
        string centerCode,
        string code,
        CancellationToken cancellationToken)
    {
        var variants = new List<FornituraVariantDto>();
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT LINE_NUMBER, PROVE, OBSERV, COLOR, MEDIDA, PREU, ACTUAL, MINIM, PREUCOST
            FROM forni_detail
            WHERE CENTRO = @centerCode
              AND FORNI_CODI = @code
            ORDER BY LINE_NUMBER;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@code", code);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            variants.Add(new FornituraVariantDto
            {
                LineNumber = reader.GetInt32OrDefault("LINE_NUMBER"),
                SupplierCode = reader.GetInt32OrDefault("PROVE"),
                SupplierItemCode = reader.GetStringOrEmpty("OBSERV"),
                Color = reader.GetStringOrEmpty("COLOR"),
                Measure = reader.GetStringOrEmpty("MEDIDA"),
                UnitPrice = reader.GetDecimalOrDefault("PREU"),
                CurrentStock = reader.GetDecimalOrDefault("ACTUAL"),
                MinimumStock = reader.GetDecimalOrDefault("MINIM"),
                CostPrice = reader.GetDecimalOrDefault("PREUCOST")
            });
        }

        return variants;
    }

    private static async Task ReplaceVariantsAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        string centerCode,
        string code,
        IReadOnlyList<SaveFornituraVariantInput> variants,
        CancellationToken cancellationToken)
    {
        await using (var deleteCommand = connection.CreateCommand())
        {
            deleteCommand.Transaction = transaction;
            deleteCommand.CommandText =
                """
                DELETE FROM forni_detail
                WHERE CENTRO = @centerCode
                  AND FORNI_CODI = @code;
                """;
            deleteCommand.Parameters.AddWithValue("@centerCode", centerCode);
            deleteCommand.Parameters.AddWithValue("@code", code);
            await deleteCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        if (variants.Count == 0)
        {
            return;
        }

        await using var insertCommand = connection.CreateCommand();
        insertCommand.Transaction = transaction;
        insertCommand.CommandText =
            """
            INSERT INTO forni_detail (
                CENTRO,
                FORNI_CODI,
                LINE_NUMBER,
                PROVE,
                OBSERV,
                COLOR,
                MEDIDA,
                PREU,
                ACTUAL,
                MINIM,
                PREUCOST)
            VALUES (
                @centerCode,
                @code,
                @lineNumber,
                @supplierCode,
                @supplierItemCode,
                @color,
                @measure,
                @unitPrice,
                @currentStock,
                @minimumStock,
                @costPrice);
            """;

        insertCommand.Parameters.Add("@centerCode", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@code", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@lineNumber", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@supplierCode", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@supplierItemCode", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@color", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@measure", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@unitPrice", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@currentStock", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@minimumStock", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@costPrice", MySqlDbType.Decimal);

        for (var index = 0; index < variants.Count; index++)
        {
            var variant = variants[index];
            insertCommand.Parameters["@centerCode"].Value = centerCode;
            insertCommand.Parameters["@code"].Value = code;
            insertCommand.Parameters["@lineNumber"].Value = index + 1;
            insertCommand.Parameters["@supplierCode"].Value = variant.SupplierCode;
            insertCommand.Parameters["@supplierItemCode"].Value = DbValue(variant.SupplierItemCode);
            insertCommand.Parameters["@color"].Value = DbValue(variant.Color);
            insertCommand.Parameters["@measure"].Value = DbValue(variant.Measure);
            insertCommand.Parameters["@unitPrice"].Value = variant.UnitPrice;
            insertCommand.Parameters["@currentStock"].Value = variant.CurrentStock;
            insertCommand.Parameters["@minimumStock"].Value = variant.MinimumStock;
            insertCommand.Parameters["@costPrice"].Value = variant.CostPrice;
            await insertCommand.ExecuteNonQueryAsync(cancellationToken);
        }
    }

    private async Task AuditAsync(Guid tenantId, Guid companyId, string code, FornituraDetailDto previous, SaveFornituraCommand current, CancellationToken cancellationToken)
    {
        var changes = new List<string>();
        Compare(changes, "Descripción", previous.Description, current.Description);
        Compare(changes, "Precio", previous.UnitPrice.ToString("0.####"), current.UnitPrice.ToString("0.####"));
        Compare(changes, "Proveedor", previous.SupplierCode.ToString(), current.SupplierCode.ToString());
        Compare(changes, "Referencia proveedor", previous.SupplierReference, current.SupplierReference);
        Compare(changes, "Cliente", previous.ClientCode.ToString(), current.ClientCode.ToString());
        Compare(changes, "Modelo", previous.Model, current.Model);
        Compare(changes, "Serie", previous.Series, current.Series);
        Compare(changes, "Temporada", previous.Season, current.Season);
        if (previous.Variants.Count != current.Variants.Count)
        {
            changes.Add($"Variantes: {previous.Variants.Count} -> {current.Variants.Count}");
        }

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = tenantId,
            CompanyId = companyId,
            UserId = _currentUserContext.UserId,
            Action = "FornituraUpdated",
            EntityName = "Fornitura",
            EntityId = code,
            Details = changes.Count == 0
                ? $"Fornitura {code} actualizada sin cambios detectados."
                : $"Fornitura {code} actualizada: {string.Join("; ", changes)}"
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

        throw new InvalidOperationException("No tienes permisos para editar fornituras en este tenant.");
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

    private static async Task<string> GetNextCodeAsync(MySqlConnection connection, string centerCode, CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT COALESCE(MAX(CAST(CODI AS UNSIGNED)), 0) + 1
            FROM forni
            WHERE CENTRO = @centerCode
              AND CODI REGEXP '^[0-9]+$';
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        return Convert.ToString(await command.ExecuteScalarAsync(cancellationToken)) ?? "1";
    }

    private static void Validate(SaveFornituraCommand command)
    {
        command.Code = command.Code?.Trim().ToUpperInvariant();
        command.Description = command.Description.Trim();
        command.SupplierReference = command.SupplierReference.Trim();
        command.Model = command.Model.Trim();
        command.Series = command.Series.Trim();
        command.Season = command.Season.Trim();

        if (string.IsNullOrWhiteSpace(command.Description) || command.Description.Length < 3)
        {
            throw new InvalidOperationException("La descripción de la fornitura es obligatoria y debe tener al menos 3 caracteres.");
        }

        if (!string.IsNullOrWhiteSpace(command.Code) && command.Code.Length > 30)
        {
            throw new InvalidOperationException("El código de la fornitura no puede superar 30 caracteres.");
        }

        if (command.UnitPrice < 0)
        {
            throw new InvalidOperationException("El precio de la fornitura no puede ser negativo.");
        }

        if (command.SupplierCode < 0 || command.ClientCode < 0)
        {
            throw new InvalidOperationException("Proveedor y cliente no pueden ser negativos.");
        }

        var duplicateKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        for (var index = 0; index < command.Variants.Count; index++)
        {
            var variant = command.Variants[index];
            variant.SupplierItemCode = variant.SupplierItemCode.Trim();
            variant.Color = variant.Color.Trim();
            variant.Measure = variant.Measure.Trim();

            if (variant.UnitPrice < 0 || variant.CurrentStock < 0 || variant.MinimumStock < 0 || variant.CostPrice < 0)
            {
                throw new InvalidOperationException($"La línea {index + 1} de detalle no admite importes o stock negativos.");
            }

            var duplicateKey = $"{variant.SupplierCode}|{variant.SupplierItemCode}|{variant.Color}|{variant.Measure}";
            if (!duplicateKeys.Add(duplicateKey))
            {
                throw new InvalidOperationException($"La línea {index + 1} repite una combinación de detalle ya existente en la misma fornitura.");
            }
        }
    }

    private static object DbValue(string value) =>
        string.IsNullOrWhiteSpace(value) ? DBNull.Value : value;

    private static string BuildSearchOrderByClause(FornituraFilter filter)
    {
        var column = filter.SortColumn switch
        {
            nameof(FornituraListItemDto.Code) => "CODI",
            nameof(FornituraListItemDto.Description) => "DESCRI",
            nameof(FornituraListItemDto.UnitPrice) => "PREU",
            nameof(FornituraListItemDto.SupplierCode) => "PROVE",
            nameof(FornituraListItemDto.ClientCode) => "CLIENT",
            nameof(FornituraListItemDto.Model) => "MODEL",
            nameof(FornituraListItemDto.Series) => "SERIE",
            nameof(FornituraListItemDto.Season) => "TEMPORADA",
            _ => string.Empty
        };

        if (string.IsNullOrWhiteSpace(column))
        {
            return string.IsNullOrWhiteSpace(filter.Search)
                ? "ORDER BY CODI"
                : "ORDER BY DESCRI, CODI";
        }

        var direction = filter.SortDescending ? "DESC" : "ASC";
        return $"ORDER BY {column} {direction}, CODI";
    }
}
