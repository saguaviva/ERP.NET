using Erp.Application.Auditing;
using Erp.Application.Companies;
using Erp.Application.Contexts;
using Erp.Application.Hilos;
using Erp.Domain.Common;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Support;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Hilos;

public sealed class MySqlHiloService : IHiloQueries, IHiloService
{
    private readonly MySqlConnectionFactory _connectionFactory;
    private readonly IAuditLogService _auditLogService;
    private readonly ICompanyAccessService _companyAccessService;
    private readonly ICurrentUserContext _currentUserContext;
    private readonly ITenantContext _tenantContext;
    private readonly IActiveCompanyContext _activeCompanyContext;

    public MySqlHiloService(
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

    public async Task<HiloSearchResultDto> SearchAsync(Guid tenantId, Guid companyId, HiloFilter filter, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return new HiloSearchResultDto();
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
            FROM fil
            WHERE CENTRO = @centerCode
              AND is_deleted = 0
              AND (
                    @search = ''
                    OR CODI LIKE @likeSearch
                    OR DESCRI LIKE @likeSearch
                    OR OBSERV LIKE @likeSearch
                    OR IVA LIKE @likeSearch
                    OR CAST(PROVE AS CHAR) LIKE @likeSearch
                  );
            """;
        countCommand.Parameters.AddWithValue("@centerCode", centerCode);
        countCommand.Parameters.AddWithValue("@search", search);
        countCommand.Parameters.AddWithValue("@likeSearch", likeSearch);
        var totalCount = Convert.ToInt32(await countCommand.ExecuteScalarAsync(cancellationToken));
        if (totalCount == 0)
        {
            return new HiloSearchResultDto { TotalCount = 0 };
        }

        var items = new List<HiloListItemDto>();
        await using var command = connection.CreateCommand();
        command.CommandText =
            $"""
            SELECT CODI, CENTRO, DESCRI, PROVE, COST, PREU, IVA, OBSERV
            FROM fil
            WHERE CENTRO = @centerCode
              AND is_deleted = 0
              AND (
                    @search = ''
                    OR CODI LIKE @likeSearch
                    OR DESCRI LIKE @likeSearch
                    OR OBSERV LIKE @likeSearch
                    OR IVA LIKE @likeSearch
                    OR CAST(PROVE AS CHAR) LIKE @likeSearch
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
            items.Add(new HiloListItemDto
            {
                Code = reader.GetStringOrEmpty("CODI"),
                CompanyCenterCode = reader.GetStringOrEmpty("CENTRO"),
                Description = reader.GetStringOrEmpty("DESCRI"),
                SupplierCode = reader.GetInt32OrDefault("PROVE"),
                CostPrice = reader.GetDecimalOrDefault("COST"),
                UnitPrice = reader.GetDecimalOrDefault("PREU"),
                VatCode = reader.GetStringOrEmpty("IVA"),
                Notes = reader.GetStringOrEmpty("OBSERV")
            });
        }

        return new HiloSearchResultDto
        {
            Items = items,
            TotalCount = totalCount
        };
    }

    public async Task<HiloDetailDto?> GetByCodeAsync(Guid tenantId, Guid companyId, string code, CancellationToken cancellationToken = default)
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
            SELECT CODI, CENTRO, DESCRI, PROVE, COST, PREU, IVA, OBSERV
            FROM fil
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

        var detail = new HiloDetailDto
        {
            Code = reader.GetStringOrEmpty("CODI"),
            CompanyCenterCode = reader.GetStringOrEmpty("CENTRO"),
            Description = reader.GetStringOrEmpty("DESCRI"),
            SupplierCode = reader.GetInt32OrDefault("PROVE"),
            CostPrice = reader.GetDecimalOrDefault("COST"),
            UnitPrice = reader.GetDecimalOrDefault("PREU"),
            VatCode = reader.GetStringOrEmpty("IVA"),
            Notes = reader.GetStringOrEmpty("OBSERV")
        };

        await reader.CloseAsync();
        detail.Colors = await LoadColorsAsync(connection, centerCode, detail.Code, cancellationToken);
        return detail;
    }

    public async Task<string> SaveAsync(SaveHiloCommand command, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return string.Empty;
        }

        await EnsureCompanyAccessAsync(command.TenantId, command.CompanyId, cancellationToken);
        EnsureTenantWriteAccess();
        Validate(command);
        var centerCode = await ResolveCompanyCenterCodeAsync(command.TenantId, command.CompanyId, cancellationToken);
        HiloDetailDto? previous = null;
        if (command.IsNew)
        {
            var duplicate = await GetByCodeAsync(command.TenantId, command.CompanyId, command.Code!, cancellationToken);
            if (duplicate is not null)
            {
                throw new InvalidOperationException("Ya existe un hilo con este código.");
            }
        }
        else
        {
            previous = await GetByCodeAsync(command.TenantId, command.CompanyId, command.Code!, cancellationToken);
            if (previous is null)
            {
                throw new InvalidOperationException("No se ha encontrado el hilo que intentas modificar.");
            }
        }

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var code = command.Code!.Trim().ToUpperInvariant();
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        if (!command.IsNew && previous is not null)
        {
            await using var updateCommand = connection.CreateCommand();
            updateCommand.Transaction = transaction;
            updateCommand.CommandText =
                """
                UPDATE fil
                SET DESCRI = @description,
                    PROVE = @supplierCode,
                    COST = @costPrice,
                    PREU = @unitPrice,
                    IVA = @vatCode,
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
                throw new InvalidOperationException("No se ha podido actualizar el hilo.");
            }

            await ReplaceColorsAsync(connection, transaction, centerCode, code, command.Colors, cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            await AuditAsync(command.TenantId, command.CompanyId, code, previous, command, cancellationToken);
            return code;
        }

        await using var insertCommand = connection.CreateCommand();
        insertCommand.Transaction = transaction;
        insertCommand.CommandText =
            """
            INSERT INTO fil (CODI, DESCRI, PROVE, COST, PREU, IVA, OBSERV, CENTRO, origin, is_deleted, synced_utc)
            VALUES (@code, @description, @supplierCode, @costPrice, @unitPrice, @vatCode, @notes, @centerCode, 'local', 0, NULL);
            """;
        FillSaveParameters(insertCommand, centerCode, code, command);
        await insertCommand.ExecuteNonQueryAsync(cancellationToken);

        await ReplaceColorsAsync(connection, transaction, centerCode, code, command.Colors, cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = command.TenantId,
            CompanyId = command.CompanyId,
            UserId = _currentUserContext.UserId,
            Action = "HiloCreated",
            EntityName = "Hilo",
            EntityId = code,
            Details = $"Hilo {code} creado: {command.Description}; detalle={command.Colors.Count}"
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
                DELETE FROM fil_detail
                WHERE CENTRO = @centerCode
                  AND FIL_CODI = @code;
                """;
            detailDelete.Parameters.AddWithValue("@centerCode", centerCode);
            detailDelete.Parameters.AddWithValue("@code", code);
            await detailDelete.ExecuteNonQueryAsync(cancellationToken);
        }

        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            UPDATE fil
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
            throw new InvalidOperationException("No se ha encontrado el hilo a eliminar.");
        }

        await transaction.CommitAsync(cancellationToken);

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = tenantId,
            CompanyId = companyId,
            UserId = _currentUserContext.UserId,
            Action = "HiloDeleted",
            EntityName = "Hilo",
            EntityId = code,
            Details = $"Hilo {code} eliminado en local."
        }, cancellationToken);
    }

    private static void FillSaveParameters(MySqlCommand command, string centerCode, string code, SaveHiloCommand model)
    {
        command.Parameters.AddWithValue("@code", code);
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@description", model.Description);
        command.Parameters.AddWithValue("@supplierCode", model.SupplierCode);
        command.Parameters.AddWithValue("@costPrice", model.CostPrice);
        command.Parameters.AddWithValue("@unitPrice", model.UnitPrice);
        command.Parameters.AddWithValue("@vatCode", DbValue(model.VatCode));
        command.Parameters.AddWithValue("@notes", DbValue(model.Notes));
    }

    private static async Task<List<HiloColorDetailDto>> LoadColorsAsync(
        MySqlConnection connection,
        string centerCode,
        string code,
        CancellationToken cancellationToken)
    {
        var items = new List<HiloColorDetailDto>();
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT LINE_NUMBER, PROVE, COLOR, ACTUAL, MINIM, PREU, PREUCOST, TINTAR, METRES, KG, OBSERV
            FROM fil_detail
            WHERE CENTRO = @centerCode
              AND FIL_CODI = @code
            ORDER BY LINE_NUMBER;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@code", code);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new HiloColorDetailDto
            {
                LineNumber = reader.GetInt32OrDefault("LINE_NUMBER"),
                SupplierCode = reader.GetInt32OrDefault("PROVE"),
                Color = reader.GetStringOrEmpty("COLOR"),
                CurrentStock = reader.GetDecimalOrDefault("ACTUAL"),
                MinimumStock = reader.GetDecimalOrDefault("MINIM"),
                UnitPrice = reader.GetDecimalOrDefault("PREU"),
                CostPrice = reader.GetDecimalOrDefault("PREUCOST"),
                DyeingPrice = reader.GetDecimalOrDefault("TINTAR"),
                Meters = reader.GetDecimalOrDefault("METRES"),
                Kilograms = reader.GetDecimalOrDefault("KG"),
                Notes = reader.GetStringOrEmpty("OBSERV")
            });
        }

        return items;
    }

    private static async Task ReplaceColorsAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        string centerCode,
        string code,
        IReadOnlyList<SaveHiloColorInput> colors,
        CancellationToken cancellationToken)
    {
        await using (var deleteCommand = connection.CreateCommand())
        {
            deleteCommand.Transaction = transaction;
            deleteCommand.CommandText =
                """
                DELETE FROM fil_detail
                WHERE CENTRO = @centerCode
                  AND FIL_CODI = @code;
                """;
            deleteCommand.Parameters.AddWithValue("@centerCode", centerCode);
            deleteCommand.Parameters.AddWithValue("@code", code);
            await deleteCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        if (colors.Count == 0)
        {
            return;
        }

        await using var insertCommand = connection.CreateCommand();
        insertCommand.Transaction = transaction;
        insertCommand.CommandText =
            """
            INSERT INTO fil_detail (
                CENTRO,
                FIL_CODI,
                LINE_NUMBER,
                PROVE,
                COLOR,
                ACTUAL,
                MINIM,
                PREU,
                PREUCOST,
                TINTAR,
                METRES,
                KG,
                OBSERV)
            VALUES (
                @centerCode,
                @code,
                @lineNumber,
                @supplierCode,
                @color,
                @currentStock,
                @minimumStock,
                @unitPrice,
                @costPrice,
                @dyeingPrice,
                @meters,
                @kilograms,
                @notes);
            """;
        insertCommand.Parameters.Add("@centerCode", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@code", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@lineNumber", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@supplierCode", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@color", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@currentStock", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@minimumStock", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@unitPrice", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@costPrice", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@dyeingPrice", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@meters", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@kilograms", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@notes", MySqlDbType.VarChar);

        for (var index = 0; index < colors.Count; index++)
        {
            var color = colors[index];
            insertCommand.Parameters["@centerCode"].Value = centerCode;
            insertCommand.Parameters["@code"].Value = code;
            insertCommand.Parameters["@lineNumber"].Value = index + 1;
            insertCommand.Parameters["@supplierCode"].Value = color.SupplierCode;
            insertCommand.Parameters["@color"].Value = DbValue(color.Color);
            insertCommand.Parameters["@currentStock"].Value = color.CurrentStock;
            insertCommand.Parameters["@minimumStock"].Value = color.MinimumStock;
            insertCommand.Parameters["@unitPrice"].Value = color.UnitPrice;
            insertCommand.Parameters["@costPrice"].Value = color.CostPrice;
            insertCommand.Parameters["@dyeingPrice"].Value = color.DyeingPrice;
            insertCommand.Parameters["@meters"].Value = color.Meters;
            insertCommand.Parameters["@kilograms"].Value = color.Kilograms;
            insertCommand.Parameters["@notes"].Value = DbValue(color.Notes);
            await insertCommand.ExecuteNonQueryAsync(cancellationToken);
        }
    }

    private async Task AuditAsync(Guid tenantId, Guid companyId, string code, HiloDetailDto previous, SaveHiloCommand current, CancellationToken cancellationToken)
    {
        var changes = new List<string>();
        Compare(changes, "Descripción", previous.Description, current.Description);
        Compare(changes, "Proveedor", previous.SupplierCode.ToString(), current.SupplierCode.ToString());
        Compare(changes, "Coste", previous.CostPrice.ToString("0.####"), current.CostPrice.ToString("0.####"));
        Compare(changes, "Precio", previous.UnitPrice.ToString("0.####"), current.UnitPrice.ToString("0.####"));
        Compare(changes, "IVA", previous.VatCode, current.VatCode);
        Compare(changes, "Notas", previous.Notes, current.Notes);
        if (previous.Colors.Count != current.Colors.Count)
        {
            changes.Add($"Detalle colores: {previous.Colors.Count} -> {current.Colors.Count}");
        }

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = tenantId,
            CompanyId = companyId,
            UserId = _currentUserContext.UserId,
            Action = "HiloUpdated",
            EntityName = "Hilo",
            EntityId = code,
            Details = changes.Count == 0
                ? $"Hilo {code} actualizado sin cambios detectados."
                : $"Hilo {code} actualizado: {string.Join("; ", changes)}"
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

        throw new InvalidOperationException("No tienes permisos para editar hilos en este tenant.");
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

    private static void Validate(SaveHiloCommand command)
    {
        command.Code = command.Code?.Trim().ToUpperInvariant();
        command.Description = command.Description.Trim();
        command.VatCode = command.VatCode.Trim().ToUpperInvariant();
        command.Notes = command.Notes.Trim();

        if (string.IsNullOrWhiteSpace(command.Code))
        {
            throw new InvalidOperationException("Debes indicar un código para el hilo.");
        }

        if (command.Code.Length > 30)
        {
            throw new InvalidOperationException("El código del hilo no puede superar 30 caracteres.");
        }

        if (string.IsNullOrWhiteSpace(command.Description) || command.Description.Length < 2)
        {
            throw new InvalidOperationException("La descripción del hilo es obligatoria y debe tener al menos 2 caracteres.");
        }

        if (command.SupplierCode < 0 || command.CostPrice < 0 || command.UnitPrice < 0)
        {
            throw new InvalidOperationException("Proveedor, coste y precio no pueden ser negativos.");
        }

        var duplicateKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        for (var index = 0; index < command.Colors.Count; index++)
        {
            var color = command.Colors[index];
            color.Color = color.Color.Trim();
            color.Notes = color.Notes.Trim();

            if (color.SupplierCode < 0 ||
                color.CurrentStock < 0 ||
                color.MinimumStock < 0 ||
                color.UnitPrice < 0 ||
                color.CostPrice < 0 ||
                color.DyeingPrice < 0 ||
                color.Meters < 0 ||
                color.Kilograms < 0)
            {
                throw new InvalidOperationException($"La línea {index + 1} del detalle no admite valores negativos.");
            }

            var duplicateKey = $"{color.SupplierCode}|{color.Color}";
            if (!duplicateKeys.Add(duplicateKey))
            {
                throw new InvalidOperationException($"La línea {index + 1} repite color/proveedor dentro del mismo hilo.");
            }
        }
    }

    private static object DbValue(string value) =>
        string.IsNullOrWhiteSpace(value) ? DBNull.Value : value;

    private static string BuildSearchOrderByClause(HiloFilter filter)
    {
        var column = filter.SortColumn switch
        {
            nameof(HiloListItemDto.Code) => "CODI",
            nameof(HiloListItemDto.Description) => "DESCRI",
            nameof(HiloListItemDto.SupplierCode) => "PROVE",
            nameof(HiloListItemDto.CostPrice) => "COST",
            nameof(HiloListItemDto.UnitPrice) => "PREU",
            nameof(HiloListItemDto.VatCode) => "IVA",
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
