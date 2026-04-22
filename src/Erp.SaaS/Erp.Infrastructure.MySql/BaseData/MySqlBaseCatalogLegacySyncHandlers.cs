using Erp.Application.BaseData;
using Erp.Application.LegacySync;
using Erp.Infrastructure.MySql.Database;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.BaseData;

internal abstract class MySqlBaseCatalogLegacySyncHandlerBase : ILegacyModuleSyncHandler
{
    private readonly MySqlConnectionFactory _saasConnectionFactory;
    private readonly LegacyMySqlConnectionFactory _legacyConnectionFactory;

    protected MySqlBaseCatalogLegacySyncHandlerBase(
        MySqlConnectionFactory saasConnectionFactory,
        LegacyMySqlConnectionFactory legacyConnectionFactory)
    {
        _saasConnectionFactory = saasConnectionFactory;
        _legacyConnectionFactory = legacyConnectionFactory;
    }

    public abstract string ModuleKey { get; }
    public abstract string DisplayName { get; }
    protected abstract string CatalogKey { get; }
    protected abstract string LegacyTableName { get; }
    protected abstract string LegacyDocumentType { get; }
    protected abstract IReadOnlyList<string> LegacyColumns { get; }
    protected virtual string OrderByClause => "`CODI`";

    public async Task<LegacySyncModuleRunResult> RunAsync(
        LegacySyncModuleContext context,
        CancellationToken cancellationToken = default)
    {
        if (!_saasConnectionFactory.IsConfigured || !_legacyConnectionFactory.IsConfigured)
        {
            return new LegacySyncModuleRunResult
            {
                NewCheckpointValue = context.CheckpointValue
            };
        }

        await using var saasConnection = await _saasConnectionFactory.OpenConnectionAsync(cancellationToken);
        await using var legacyConnection = await _legacyConnectionFactory.OpenConnectionAsync(cancellationToken);

        var availableLegacyColumns = await LoadOrderedColumnsAsync(legacyConnection, LegacyTableName, cancellationToken);
        var missingColumns = LegacyColumns
            .Where(column => !availableLegacyColumns.Contains(column, StringComparer.OrdinalIgnoreCase))
            .ToArray();
        if (missingColumns.Length > 0)
        {
            throw new InvalidOperationException(
                $"No se han encontrado las columnas mínimas necesarias para sincronizar {DisplayName}: {string.Join(", ", missingColumns)}.");
        }

        await using var transaction = await saasConnection.BeginTransactionAsync(cancellationToken);
        var errors = new List<LegacySyncErrorRecord>();
        var mappings = new List<LegacySyncMappingRecord>();

        try
        {
            await DeleteExistingMappingsAsync(saasConnection, transaction, context, cancellationToken);
            await DeleteTargetRowsAsync(saasConnection, transaction, context, cancellationToken);

            var importedRows = 0;
            var skippedRows = 0;
            var ordinals = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            var syncedUtc = DateTime.UtcNow;
            var columnList = string.Join(", ", LegacyColumns.Select(column => $"`{column}`"));

            await using var readCommand = legacyConnection.CreateCommand();
            readCommand.CommandText =
                $"""
                SELECT {columnList}
                FROM `{LegacyTableName}`
                WHERE `CENTRO` = @centerCode
                ORDER BY {OrderByClause};
                """;
            readCommand.Parameters.AddWithValue("@centerCode", context.CompanyLegacyCenterCode);

            await using var insertCommand = saasConnection.CreateCommand();
            insertCommand.Transaction = transaction;
            insertCommand.CommandText =
                """
                INSERT INTO base_catalog_items (
                    tenant_id,
                    company_id,
                    catalog_key,
                    code,
                    name,
                    description,
                    reference_value,
                    secondary_reference_value,
                    numeric_value,
                    secondary_numeric_value,
                    notes,
                    is_active,
                    origin,
                    is_deleted,
                    synced_utc,
                    created_utc,
                    updated_utc)
                VALUES (
                    @tenantId,
                    @companyId,
                    @catalogKey,
                    @code,
                    @name,
                    @description,
                    @referenceValue,
                    @secondaryReferenceValue,
                    @numericValue,
                    @secondaryNumericValue,
                    @notes,
                    1,
                    'legacy',
                    0,
                    @syncedUtc,
                    @syncedUtc,
                    @syncedUtc);
                """;
            insertCommand.Parameters.AddWithValue("@tenantId", context.TenantId.ToString());
            insertCommand.Parameters.AddWithValue("@companyId", context.CompanyId.ToString());
            insertCommand.Parameters.AddWithValue("@catalogKey", CatalogKey);
            insertCommand.Parameters.Add(new MySqlParameter("@code", DBNull.Value));
            insertCommand.Parameters.Add(new MySqlParameter("@name", DBNull.Value));
            insertCommand.Parameters.Add(new MySqlParameter("@description", DBNull.Value));
            insertCommand.Parameters.Add(new MySqlParameter("@referenceValue", DBNull.Value));
            insertCommand.Parameters.Add(new MySqlParameter("@secondaryReferenceValue", DBNull.Value));
            insertCommand.Parameters.Add(new MySqlParameter("@numericValue", DBNull.Value));
            insertCommand.Parameters.Add(new MySqlParameter("@secondaryNumericValue", DBNull.Value));
            insertCommand.Parameters.Add(new MySqlParameter("@notes", DBNull.Value));
            insertCommand.Parameters.AddWithValue("@syncedUtc", syncedUtc);

            await using var reader = await readCommand.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                var entityNumber = GetEntityNumber(reader, ordinals);

                try
                {
                    var item = MapRow(reader, ordinals);
                    if (string.IsNullOrWhiteSpace(item.Code))
                    {
                        skippedRows++;
                        errors.Add(new LegacySyncErrorRecord
                        {
                            Stage = LegacyTableName,
                            LegacyEntityKey = $"{context.CompanyLegacyCenterCode}/{entityNumber}",
                            ErrorMessage = "El registro legacy no tiene código válido.",
                            Payload = string.Empty
                        });
                        continue;
                    }

                    insertCommand.Parameters["@code"].Value = item.Code;
                    insertCommand.Parameters["@name"].Value = item.Name;
                    insertCommand.Parameters["@description"].Value = DbValue(item.Description);
                    insertCommand.Parameters["@referenceValue"].Value = DbValue(item.Reference);
                    insertCommand.Parameters["@secondaryReferenceValue"].Value = DbValue(item.SecondaryReference);
                    insertCommand.Parameters["@numericValue"].Value = item.NumericValue.HasValue ? item.NumericValue.Value : DBNull.Value;
                    insertCommand.Parameters["@secondaryNumericValue"].Value = item.SecondaryNumericValue.HasValue ? item.SecondaryNumericValue.Value : DBNull.Value;
                    insertCommand.Parameters["@notes"].Value = DbValue(item.Notes);

                    await insertCommand.ExecuteNonQueryAsync(cancellationToken);
                    importedRows++;

                    mappings.Add(new LegacySyncMappingRecord
                    {
                        LegacySourceSystem = "legacy",
                        LegacyCenterCode = context.CompanyLegacyCenterCode,
                        LegacyDocumentType = LegacyDocumentType,
                        LegacyDocumentNumber = item.Code,
                        TargetEntityName = CatalogKey,
                        TargetEntityId = item.Code
                    });
                }
                catch (MySqlException exception) when (exception.Number == 1062)
                {
                    skippedRows++;
                    errors.Add(new LegacySyncErrorRecord
                    {
                        Stage = LegacyTableName,
                        LegacyEntityKey = $"{context.CompanyLegacyCenterCode}/{entityNumber}",
                        ErrorMessage = "Código duplicado en el catálogo legacy.",
                        Payload = string.Empty
                    });
                }
                catch (Exception exception)
                {
                    skippedRows++;
                    errors.Add(new LegacySyncErrorRecord
                    {
                        Stage = LegacyTableName,
                        LegacyEntityKey = $"{context.CompanyLegacyCenterCode}/{entityNumber}",
                        ErrorMessage = exception.Message,
                        Payload = string.Empty
                    });
                }
            }

            await transaction.CommitAsync(cancellationToken);

            return new LegacySyncModuleRunResult
            {
                RecordsInserted = importedRows,
                RecordsUpdated = 0,
                RecordsSkipped = skippedRows,
                NewCheckpointValue = DateTime.UtcNow.ToString("O"),
                Summary = $"{DisplayName} replicados={importedRows}; errores={errors.Count}",
                Mappings = mappings,
                Errors = errors
            };
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    protected static string? GetNullableString(MySqlDataReader reader, IReadOnlyDictionary<string, int> ordinals, string columnName)
    {
        if (!ordinals.TryGetValue(columnName, out var ordinal))
        {
            ordinal = reader.GetOrdinal(columnName);
            ((Dictionary<string, int>)ordinals)[columnName] = ordinal;
        }

        return reader.IsDBNull(ordinal)
            ? null
            : (Convert.ToString(reader.GetValue(ordinal)) ?? string.Empty).Trim();
    }

    protected static decimal? GetNullableDecimal(MySqlDataReader reader, IReadOnlyDictionary<string, int> ordinals, string columnName)
    {
        if (!ordinals.TryGetValue(columnName, out var ordinal))
        {
            ordinal = reader.GetOrdinal(columnName);
            ((Dictionary<string, int>)ordinals)[columnName] = ordinal;
        }

        if (reader.IsDBNull(ordinal))
        {
            return null;
        }

        return Convert.ToDecimal(reader.GetValue(ordinal));
    }

    protected abstract BaseCatalogLegacyImportRow MapRow(MySqlDataReader reader, IReadOnlyDictionary<string, int> ordinals);

    private static async Task DeleteExistingMappingsAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        LegacySyncModuleContext context,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            DELETE FROM legacy_sync_mappings
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND module_key = @moduleKey;
            """;
        command.Parameters.AddWithValue("@tenantId", context.TenantId.ToString());
        command.Parameters.AddWithValue("@companyId", context.CompanyId.ToString());
        command.Parameters.AddWithValue("@moduleKey", context.ModuleKey);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private async Task DeleteTargetRowsAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        LegacySyncModuleContext context,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            DELETE FROM base_catalog_items
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND catalog_key = @catalogKey
              AND origin = 'legacy';
            """;
        command.Parameters.AddWithValue("@tenantId", context.TenantId.ToString());
        command.Parameters.AddWithValue("@companyId", context.CompanyId.ToString());
        command.Parameters.AddWithValue("@catalogKey", CatalogKey);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task<IReadOnlyList<string>> LoadOrderedColumnsAsync(
        MySqlConnection connection,
        string tableName,
        CancellationToken cancellationToken)
    {
        var columns = new List<string>();

        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT COLUMN_NAME
            FROM INFORMATION_SCHEMA.COLUMNS
            WHERE TABLE_SCHEMA = DATABASE()
              AND TABLE_NAME = @tableName
            ORDER BY ORDINAL_POSITION;
            """;
        command.Parameters.AddWithValue("@tableName", tableName);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            columns.Add(reader.GetString(reader.GetOrdinal("COLUMN_NAME")));
        }

        return columns;
    }

    private static object DbValue(string? value)
        => string.IsNullOrWhiteSpace(value) ? DBNull.Value : value.Trim();

    private string GetEntityNumber(MySqlDataReader reader, IReadOnlyDictionary<string, int> ordinals)
        => GetLegacyEntityNumber(reader, ordinals) ?? "0";

    protected virtual string? GetLegacyEntityNumber(MySqlDataReader reader, IReadOnlyDictionary<string, int> ordinals)
        => GetNullableString(reader, ordinals, "CODI");
}

internal sealed record BaseCatalogLegacyImportRow(
    string Code,
    string Name,
    string? Description = null,
    string? Reference = null,
    string? SecondaryReference = null,
    decimal? NumericValue = null,
    decimal? SecondaryNumericValue = null,
    string? Notes = null);

internal sealed class MySqlBanksCashboxesLegacySyncHandler : MySqlBaseCatalogLegacySyncHandlerBase
{
    public MySqlBanksCashboxesLegacySyncHandler(
        MySqlConnectionFactory saasConnectionFactory,
        LegacyMySqlConnectionFactory legacyConnectionFactory)
        : base(saasConnectionFactory, legacyConnectionFactory)
    {
    }

    public override string ModuleKey => LegacySyncModuleKeys.BaseBanksCashboxes;
    public override string DisplayName => "Base de datos / Bancs · Caixes";
    protected override string CatalogKey => BaseCatalogKeys.BanksCashboxes;
    protected override string LegacyTableName => "bancs";
    protected override string LegacyDocumentType => "BANK";
    protected override IReadOnlyList<string> LegacyColumns => ["CODI", "DESCRIPCIO", "NUMCUENTA", "IBAN", "SWIFT", "CENTRO"];

    protected override BaseCatalogLegacyImportRow MapRow(MySqlDataReader reader, IReadOnlyDictionary<string, int> ordinals)
        => new(
            Code: GetNullableString(reader, ordinals, "CODI") ?? string.Empty,
            Name: GetNullableString(reader, ordinals, "DESCRIPCIO") ?? string.Empty,
            Description: GetNullableString(reader, ordinals, "NUMCUENTA"),
            Reference: GetNullableString(reader, ordinals, "IBAN"),
            SecondaryReference: GetNullableString(reader, ordinals, "SWIFT"));
}

internal sealed class MySqlPaymentMethodsLegacySyncHandler : MySqlBaseCatalogLegacySyncHandlerBase
{
    public MySqlPaymentMethodsLegacySyncHandler(
        MySqlConnectionFactory saasConnectionFactory,
        LegacyMySqlConnectionFactory legacyConnectionFactory)
        : base(saasConnectionFactory, legacyConnectionFactory)
    {
    }

    public override string ModuleKey => LegacySyncModuleKeys.BasePaymentMethods;
    public override string DisplayName => "Base de datos / Formes de pagament";
    protected override string CatalogKey => BaseCatalogKeys.PaymentMethods;
    protected override string LegacyTableName => "forpag";
    protected override string LegacyDocumentType => "PAYMENT_METHOD";
    protected override IReadOnlyList<string> LegacyColumns => ["CODI", "DESCRIPCIO", "Nro", "V_1", "Dies", "CENTRO"];

    protected override BaseCatalogLegacyImportRow MapRow(MySqlDataReader reader, IReadOnlyDictionary<string, int> ordinals)
        => new(
            Code: GetNullableString(reader, ordinals, "CODI") ?? string.Empty,
            Name: GetNullableString(reader, ordinals, "DESCRIPCIO") ?? string.Empty,
            Description: GetNullableDecimal(reader, ordinals, "Nro")?.ToString("0"),
            NumericValue: GetNullableDecimal(reader, ordinals, "Dies"),
            SecondaryNumericValue: GetNullableDecimal(reader, ordinals, "V_1"));
}

internal sealed class MySqlOperationsLegacySyncHandler : MySqlBaseCatalogLegacySyncHandlerBase
{
    public MySqlOperationsLegacySyncHandler(
        MySqlConnectionFactory saasConnectionFactory,
        LegacyMySqlConnectionFactory legacyConnectionFactory)
        : base(saasConnectionFactory, legacyConnectionFactory)
    {
    }

    public override string ModuleKey => LegacySyncModuleKeys.BaseOperations;
    public override string DisplayName => "Base de datos / Operacions";
    protected override string CatalogKey => BaseCatalogKeys.Operations;
    protected override string LegacyTableName => "treballs";
    protected override string LegacyDocumentType => "OPERATION";
    protected override IReadOnlyList<string> LegacyColumns => ["CODI", "DESCRI", "CENTRO"];

    protected override BaseCatalogLegacyImportRow MapRow(MySqlDataReader reader, IReadOnlyDictionary<string, int> ordinals)
        => new(
            Code: GetNullableString(reader, ordinals, "CODI") ?? string.Empty,
            Name: GetNullableString(reader, ordinals, "DESCRI") ?? string.Empty);
}

internal sealed class MySqlMachinesLegacySyncHandler : MySqlBaseCatalogLegacySyncHandlerBase
{
    public MySqlMachinesLegacySyncHandler(
        MySqlConnectionFactory saasConnectionFactory,
        LegacyMySqlConnectionFactory legacyConnectionFactory)
        : base(saasConnectionFactory, legacyConnectionFactory)
    {
    }

    public override string ModuleKey => LegacySyncModuleKeys.BaseMachines;
    public override string DisplayName => "Base de datos / Màquines";
    protected override string CatalogKey => BaseCatalogKeys.Machines;
    protected override string LegacyTableName => "maqui";
    protected override string LegacyDocumentType => "MACHINE";
    protected override IReadOnlyList<string> LegacyColumns => ["CODI", "DESCRI", "PREU", "CENTRO"];

    protected override BaseCatalogLegacyImportRow MapRow(MySqlDataReader reader, IReadOnlyDictionary<string, int> ordinals)
        => new(
            Code: GetNullableString(reader, ordinals, "CODI") ?? string.Empty,
            Name: GetNullableString(reader, ordinals, "DESCRI") ?? string.Empty,
            NumericValue: GetNullableDecimal(reader, ordinals, "PREU"));
}

internal sealed class MySqlSeasonsLegacySyncHandler : MySqlBaseCatalogLegacySyncHandlerBase
{
    public MySqlSeasonsLegacySyncHandler(
        MySqlConnectionFactory saasConnectionFactory,
        LegacyMySqlConnectionFactory legacyConnectionFactory)
        : base(saasConnectionFactory, legacyConnectionFactory)
    {
    }

    public override string ModuleKey => LegacySyncModuleKeys.BaseSeasons;
    public override string DisplayName => "Base de datos / Temporades";
    protected override string CatalogKey => BaseCatalogKeys.Seasons;
    protected override string LegacyTableName => "TEMPORADAS";
    protected override string LegacyDocumentType => "SEASON";
    protected override IReadOnlyList<string> LegacyColumns => ["CODI", "DESCRI", "CENTRO"];

    protected override BaseCatalogLegacyImportRow MapRow(MySqlDataReader reader, IReadOnlyDictionary<string, int> ordinals)
        => new(
            Code: GetNullableString(reader, ordinals, "CODI") ?? string.Empty,
            Name: GetNullableString(reader, ordinals, "DESCRI") ?? string.Empty);
}

internal sealed class MySqlVatTypesLegacySyncHandler : MySqlBaseCatalogLegacySyncHandlerBase
{
    public MySqlVatTypesLegacySyncHandler(
        MySqlConnectionFactory saasConnectionFactory,
        LegacyMySqlConnectionFactory legacyConnectionFactory)
        : base(saasConnectionFactory, legacyConnectionFactory)
    {
    }

    public override string ModuleKey => LegacySyncModuleKeys.BaseVatTypes;
    public override string DisplayName => "Base de datos / Tipus d'IVA";
    protected override string CatalogKey => BaseCatalogKeys.VatTypes;
    protected override string LegacyTableName => "IVA";
    protected override string LegacyDocumentType => "VAT";
    protected override IReadOnlyList<string> LegacyColumns => ["CODI", "DESCRIPCIO", "IVA", "RE", "CENTRO"];

    protected override BaseCatalogLegacyImportRow MapRow(MySqlDataReader reader, IReadOnlyDictionary<string, int> ordinals)
        => new(
            Code: GetNullableString(reader, ordinals, "CODI") ?? string.Empty,
            Name: GetNullableString(reader, ordinals, "DESCRIPCIO") ?? string.Empty,
            NumericValue: GetNullableDecimal(reader, ordinals, "IVA"),
            SecondaryNumericValue: GetNullableDecimal(reader, ordinals, "RE"));
}

internal sealed class MySqlIncotermsLegacySyncHandler : MySqlBaseCatalogLegacySyncHandlerBase
{
    public MySqlIncotermsLegacySyncHandler(
        MySqlConnectionFactory saasConnectionFactory,
        LegacyMySqlConnectionFactory legacyConnectionFactory)
        : base(saasConnectionFactory, legacyConnectionFactory)
    {
    }

    public override string ModuleKey => LegacySyncModuleKeys.BaseIncoterms;
    public override string DisplayName => "Base de datos / Incoterms";
    protected override string CatalogKey => BaseCatalogKeys.Incoterms;
    protected override string LegacyTableName => "INCOTERM";
    protected override string LegacyDocumentType => "INCOTERM";
    protected override IReadOnlyList<string> LegacyColumns => ["NOMBRE", "DESCRI", "CENTRO"];
    protected override string OrderByClause => "`NOMBRE`";

    protected override BaseCatalogLegacyImportRow MapRow(MySqlDataReader reader, IReadOnlyDictionary<string, int> ordinals)
        => new(
            Code: GetNullableString(reader, ordinals, "NOMBRE") ?? string.Empty,
            Name: GetNullableString(reader, ordinals, "DESCRI") ?? string.Empty);

    protected override string? GetLegacyEntityNumber(MySqlDataReader reader, IReadOnlyDictionary<string, int> ordinals)
        => GetNullableString(reader, ordinals, "NOMBRE");
}
