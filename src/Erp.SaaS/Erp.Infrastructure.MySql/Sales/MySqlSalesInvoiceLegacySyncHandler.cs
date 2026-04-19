using Erp.Application.LegacySync;
using Erp.Application.Sales;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Support;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Sales;

public sealed class MySqlSalesInvoiceLegacySyncHandler : ILegacyModuleSyncHandler
{
    private const int SyncCommandTimeoutSeconds = 300;
    private readonly MySqlConnectionFactory _saasConnectionFactory;
    private readonly LegacyMySqlConnectionFactory _legacyConnectionFactory;

    public MySqlSalesInvoiceLegacySyncHandler(
        MySqlConnectionFactory saasConnectionFactory,
        LegacyMySqlConnectionFactory legacyConnectionFactory)
    {
        _saasConnectionFactory = saasConnectionFactory;
        _legacyConnectionFactory = legacyConnectionFactory;
    }

    public string ModuleKey => LegacySyncModuleKeys.SalesInvoices;
    public string DisplayName => "Ventas / Facturas";

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

        var headers = await LoadLegacyInvoiceHeadersAsync(legacyConnection, context.CompanyLegacyCenterCode, cancellationToken);
        var linesByInvoice = await LoadLegacyInvoiceLinesAsync(legacyConnection, context.CompanyLegacyCenterCode, cancellationToken);
        var existingInvoices = await LoadExistingInvoiceRecordsAsync(saasConnection, context.TenantId, context.CompanyId, cancellationToken);
        var shipmentsByNumber = await LoadImportedShipmentsAsync(saasConnection, context.TenantId, context.CompanyId, cancellationToken);

        var inserted = 0;
        var updated = 0;
        var skipped = 0;
        var mappings = new List<LegacySyncMappingRecord>();
        var errors = new List<LegacySyncErrorRecord>();
        var seenLegacyInvoiceNumbers = new HashSet<int>();

        await DeleteExistingMappingsAsync(saasConnection, context, cancellationToken);

        foreach (var header in headers)
        {
            if (!linesByInvoice.TryGetValue(header.InvoiceNumber, out var legacyLines) || legacyLines.Count == 0)
            {
                skipped++;
                continue;
            }

            var normalizedLines = NormalizeLegacyInvoiceLines(header, legacyLines);
            if (normalizedLines.Count == 0)
            {
                skipped++;
                continue;
            }

            if (existingInvoices.TryGetValue(header.InvoiceNumber, out var existingRecord) &&
                !string.Equals(existingRecord.Origin, SalesOrderOrigins.Legacy, StringComparison.OrdinalIgnoreCase))
            {
                skipped++;
                errors.Add(new LegacySyncErrorRecord
                {
                    Stage = "UpsertInvoice",
                    LegacyEntityKey = $"{context.CompanyLegacyCenterCode}/F/{header.InvoiceNumber}",
                    ErrorMessage = "Existe una factura SaaS con el mismo número y no se puede sobreescribir desde la sincronización legacy.",
                    Payload = $"InvoiceNumber={header.InvoiceNumber}; Origin={existingRecord.Origin}"
                });
                continue;
            }

            var invoiceRecord = existingInvoices.TryGetValue(header.InvoiceNumber, out var currentRecord)
                ? currentRecord
                : new ExistingInvoiceRecord(
                    DeterministicGuid(context.TenantId, context.CompanyId, "invoice", header.InvoiceNumber),
                    DeterministicGuid(context.TenantId, context.CompanyId, "legacy-draft", header.InvoiceNumber),
                    SalesOrderOrigins.Legacy);

            await using var transaction = await saasConnection.BeginTransactionAsync(cancellationToken);
            try
            {
                var nowUtc = DateTime.UtcNow;
                await UpsertImportedInvoiceHeaderAsync(
                    saasConnection,
                    transaction,
                    context.TenantId,
                    context.CompanyId,
                    context.CompanyLegacyCenterCode,
                    header,
                    invoiceRecord,
                    normalizedLines,
                    nowUtc,
                    cancellationToken);

                await ReplaceImportedInvoiceLinesAsync(
                    saasConnection,
                    transaction,
                    invoiceRecord.InvoiceId,
                    context.TenantId,
                    context.CompanyId,
                    normalizedLines,
                    cancellationToken);

                await ReplaceImportedInvoiceShipmentsAsync(
                    saasConnection,
                    transaction,
                    context.TenantId,
                    context.CompanyId,
                    context.CompanyLegacyCenterCode,
                    header.InvoiceNumber,
                    invoiceRecord.InvoiceId,
                    normalizedLines,
                    shipmentsByNumber,
                    nowUtc,
                    errors,
                    cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                if (existingInvoices.ContainsKey(header.InvoiceNumber))
                {
                    updated++;
                }
                else
                {
                    inserted++;
                }

                existingInvoices[header.InvoiceNumber] = new ExistingInvoiceRecord(
                    invoiceRecord.InvoiceId,
                    invoiceRecord.DraftId,
                    SalesOrderOrigins.Legacy);
                seenLegacyInvoiceNumbers.Add(header.InvoiceNumber);

                mappings.Add(new LegacySyncMappingRecord
                {
                    LegacyCenterCode = context.CompanyLegacyCenterCode,
                    LegacyDocumentType = "F",
                    LegacyDocumentNumber = header.InvoiceNumber.ToString(),
                    TargetEntityName = "SalesInvoice",
                    TargetEntityId = header.InvoiceNumber.ToString()
                });

                foreach (var line in normalizedLines)
                {
                    mappings.Add(new LegacySyncMappingRecord
                    {
                        LegacyCenterCode = context.CompanyLegacyCenterCode,
                        LegacyDocumentType = "F",
                        LegacyDocumentNumber = header.InvoiceNumber.ToString(),
                        LegacyLineNumber = line.LineNumber,
                        TargetEntityName = "SalesInvoiceLine",
                        TargetEntityId = $"{header.InvoiceNumber}:{line.LineNumber}"
                    });
                }
            }
            catch (Exception exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                skipped++;
                errors.Add(new LegacySyncErrorRecord
                {
                    Stage = "UpsertInvoice",
                    LegacyEntityKey = $"{context.CompanyLegacyCenterCode}/F/{header.InvoiceNumber}",
                    ErrorMessage = exception.Message,
                    Payload = $"InvoiceNumber={header.InvoiceNumber}; ClientCode={header.ClientCode}"
                });
            }
        }

        updated += await MarkMissingImportedInvoicesAsDeletedAsync(
            saasConnection,
            context.TenantId,
            context.CompanyId,
            seenLegacyInvoiceNumbers,
            cancellationToken);

        return new LegacySyncModuleRunResult
        {
            RecordsInserted = inserted,
            RecordsUpdated = updated,
            RecordsSkipped = skipped,
            NewCheckpointValue = $"FULL@{DateTime.UtcNow:O}",
            Summary = $"Headers={headers.Count}; Insertados={inserted}; Actualizados={updated}; Omitidos={skipped}; Errores={errors.Count}",
            Mappings = mappings,
            Errors = errors
        };
    }

    private static async Task<List<LegacySalesInvoiceHeader>> LoadLegacyInvoiceHeadersAsync(
        MySqlConnection connection,
        string centerCode,
        CancellationToken cancellationToken)
    {
        await using var command = CreateTimedCommand(connection);
        command.CommandText =
            """
            SELECT f.FRA,
                   f.CLIENT,
                   COALESCE(NULLIF(c.NOM, ''), CONCAT('Cliente ', CAST(f.CLIENT AS CHAR))) AS client_name,
                   COALESCE(c.NIF, '') AS client_tax_id,
                   CAST(f.DATA AS CHAR) AS raw_issue_date,
                   CAST(f.VENCIM AS CHAR) AS raw_due_date,
                   COALESCE(f.BASE1, 0) AS subtotal_amount,
                   COALESCE(f.IVA1, 0) AS tax_amount,
                   COALESCE(f.TOTAL, COALESCE(f.BASE1, 0) + COALESCE(f.IVA1, 0)) AS total_amount,
                   COALESCE(f.OBSERV, '') AS notes
            FROM factur f
            LEFT JOIN clients c
              ON c.CENTRO = f.CENTRO
             AND c.CODI = f.CLIENT
            WHERE f.DOCUMENT = 'F'
              AND f.CENTRO = @centerCode
            ORDER BY f.FRA;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);

        var headers = new List<LegacySalesInvoiceHeader>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var issueDate = ParseLegacyDate(reader.GetStringOrEmpty("raw_issue_date"));
            if (!issueDate.HasValue)
            {
                continue;
            }

            var dueDate = ParseLegacyDate(reader.GetStringOrEmpty("raw_due_date"));
            headers.Add(new LegacySalesInvoiceHeader(
                reader.GetInt32(reader.GetOrdinal("FRA")),
                reader.GetInt32(reader.GetOrdinal("CLIENT")),
                reader.GetStringOrEmpty("client_name"),
                reader.GetStringOrEmpty("client_tax_id"),
                issueDate.Value.Date,
                dueDate?.Date,
                decimal.Round(reader.GetDecimalOrDefault("subtotal_amount"), 2, MidpointRounding.AwayFromZero),
                decimal.Round(reader.GetDecimalOrDefault("tax_amount"), 2, MidpointRounding.AwayFromZero),
                decimal.Round(reader.GetDecimalOrDefault("total_amount"), 2, MidpointRounding.AwayFromZero),
                reader.GetStringOrEmpty("notes")));
        }

        return headers;
    }

    private static async Task<Dictionary<int, List<LegacySalesInvoiceLine>>> LoadLegacyInvoiceLinesAsync(
        MySqlConnection connection,
        string centerCode,
        CancellationToken cancellationToken)
    {
        await using var command = CreateTimedCommand(connection);
        command.CommandText =
            """
            SELECT FRA,
                   NLINEA,
                   COALESCE(NULLIF(MOSTRA, ''), NULLIF(NCCODE, ''), '') AS item_code,
                   COALESCE(DESCRI, '') AS description,
                   COALESCE(UNITATS, 0) AS quantity,
                   COALESCE(PREU, 0) AS unit_price,
                   COALESCE(NULLIF(ALBAR, 0), 0) AS shipment_number,
                   COALESCE(NULLIF(COMAN, 0), 0) AS order_number
            FROM dfactu
            WHERE DOCUMENT = 'F'
              AND CENTRO = @centerCode
            ORDER BY FRA, NLINEA;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);

        var linesByInvoice = new Dictionary<int, List<LegacySalesInvoiceLine>>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var invoiceNumber = reader.GetInt32(reader.GetOrdinal("FRA"));
            if (!linesByInvoice.TryGetValue(invoiceNumber, out var lines))
            {
                lines = [];
                linesByInvoice[invoiceNumber] = lines;
            }

            lines.Add(new LegacySalesInvoiceLine(
                reader.GetInt32(reader.GetOrdinal("NLINEA")),
                reader.GetStringOrEmpty("item_code"),
                reader.GetStringOrEmpty("description"),
                Math.Abs(reader.GetDecimalOrDefault("quantity")),
                decimal.Round(reader.GetDecimalOrDefault("unit_price"), 4, MidpointRounding.AwayFromZero),
                reader.GetInt32OrDefault("shipment_number"),
                reader.GetInt32OrDefault("order_number")));
        }

        return linesByInvoice;
    }

    private static List<ImportedSalesInvoiceLine> NormalizeLegacyInvoiceLines(
        LegacySalesInvoiceHeader header,
        IReadOnlyCollection<LegacySalesInvoiceLine> legacyLines)
    {
        var preparedLines = legacyLines
            .Select(legacyLine =>
            {
                var quantity = legacyLine.Quantity > 0 ? legacyLine.Quantity : (legacyLine.UnitPrice == 0 ? 0 : 1);
                if (quantity <= 0 && legacyLine.UnitPrice == 0)
                {
                    return null;
                }

                var description = string.IsNullOrWhiteSpace(legacyLine.Description)
                    ? (!string.IsNullOrWhiteSpace(legacyLine.ItemCode) ? legacyLine.ItemCode : $"Línea {legacyLine.LineNumber}")
                    : legacyLine.Description.Trim();
                var lineSubtotal = decimal.Round(quantity * legacyLine.UnitPrice, 2, MidpointRounding.AwayFromZero);

                return new ImportedSalesInvoiceLine(
                    legacyLine.LineNumber,
                    legacyLine.ItemCode.Trim(),
                    description,
                    quantity,
                    legacyLine.UnitPrice,
                    lineSubtotal,
                    legacyLine.ShipmentNumber,
                    legacyLine.OrderNumber);
            })
            .Where(line => line is not null)
            .Cast<ImportedSalesInvoiceLine>()
            .GroupBy(line => line.LineNumber)
            .Select(group =>
            {
                var first = group.First();
                var totalQuantity = decimal.Round(group.Sum(line => line.Quantity), 3, MidpointRounding.AwayFromZero);
                var lineSubtotal = decimal.Round(group.Sum(line => line.LineSubtotal), 2, MidpointRounding.AwayFromZero);
                var unitPrice = totalQuantity > 0
                    ? decimal.Round(lineSubtotal / totalQuantity, 4, MidpointRounding.AwayFromZero)
                    : first.UnitPrice;

                return new ImportedSalesInvoiceLine(
                    first.LineNumber,
                    first.ItemCode,
                    first.Description,
                    totalQuantity,
                    unitPrice,
                    lineSubtotal,
                    group.Select(line => line.ShipmentNumber).Where(number => number > 0).DefaultIfEmpty(0).Max(),
                    group.Select(line => line.OrderNumber).Where(number => number > 0).DefaultIfEmpty(0).Max());
            })
            .OrderBy(line => line.LineNumber)
            .ToList();

        if (preparedLines.Count == 0)
        {
            return preparedLines;
        }

        var taxableBase = preparedLines.Sum(line => line.LineSubtotal);
        var taxRate = taxableBase <= 0
            ? 0m
            : decimal.Round((header.TaxAmount / taxableBase) * 100m, 4, MidpointRounding.AwayFromZero);

        return preparedLines
            .Select(line =>
            {
                var taxAmount = taxableBase <= 0
                    ? 0m
                    : decimal.Round(line.LineSubtotal * taxRate / 100m, 2, MidpointRounding.AwayFromZero);
                var lineTotal = decimal.Round(line.LineSubtotal + taxAmount, 2, MidpointRounding.AwayFromZero);
                return line with
                {
                    TaxRate = taxRate,
                    TaxAmount = taxAmount,
                    LineTotal = lineTotal
                };
            })
            .ToList();
    }

    private static async Task<Dictionary<int, ExistingInvoiceRecord>> LoadExistingInvoiceRecordsAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        CancellationToken cancellationToken)
    {
        await using var command = CreateTimedCommand(connection);
        command.CommandText =
            """
            SELECT invoice_number, invoice_id, draft_id, COALESCE(origin, 'saas') AS origin
            FROM sales_invoices
            WHERE tenant_id = @tenantId
              AND company_id = @companyId;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());

        var items = new Dictionary<int, ExistingInvoiceRecord>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items[reader.GetInt32(reader.GetOrdinal("invoice_number"))] = new ExistingInvoiceRecord(
                reader.GetGuid("invoice_id"),
                reader.GetGuid("draft_id"),
                reader.GetStringOrEmpty("origin"));
        }

        return items;
    }

    private static async Task<Dictionary<int, ImportedShipmentLookup>> LoadImportedShipmentsAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        CancellationToken cancellationToken)
    {
        await using var command = CreateTimedCommand(connection);
        command.CommandText =
            """
            SELECT shipment_id,
                   shipment_series,
                   shipment_number,
                   order_number,
                   shipment_date,
                   warehouse
            FROM sales_order_shipments
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND COALESCE(is_deleted, 0) = 0;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());

        var items = new Dictionary<int, ImportedShipmentLookup>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var shipmentNumberOrdinal = reader.GetOrdinal("shipment_number");
            if (reader.IsDBNull(shipmentNumberOrdinal))
            {
                continue;
            }

            items[reader.GetInt32(shipmentNumberOrdinal)] = new ImportedShipmentLookup(
                reader.GetGuid("shipment_id"),
                reader.GetStringOrEmpty("shipment_series"),
                reader.GetInt32(shipmentNumberOrdinal),
                reader.GetInt32(reader.GetOrdinal("order_number")),
                reader.GetDateTime(reader.GetOrdinal("shipment_date")),
                reader.GetStringOrEmpty("warehouse"));
        }

        return items;
    }

    private static async Task UpsertImportedInvoiceHeaderAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid tenantId,
        Guid companyId,
        string legacyCenterCode,
        LegacySalesInvoiceHeader header,
        ExistingInvoiceRecord invoiceRecord,
        IReadOnlyCollection<ImportedSalesInvoiceLine> lines,
        DateTime nowUtc,
        CancellationToken cancellationToken)
    {
        var subtotalAmount = header.SubtotalAmount > 0
            ? header.SubtotalAmount
            : decimal.Round(lines.Sum(line => line.LineSubtotal), 2, MidpointRounding.AwayFromZero);
        var taxAmount = header.TaxAmount > 0
            ? header.TaxAmount
            : decimal.Round(lines.Sum(line => line.TaxAmount), 2, MidpointRounding.AwayFromZero);
        var totalAmount = header.TotalAmount > 0
            ? header.TotalAmount
            : decimal.Round(subtotalAmount + taxAmount, 2, MidpointRounding.AwayFromZero);

        await using var command = CreateTimedCommand(connection, transaction);
        command.CommandText =
            """
            INSERT INTO sales_invoices (
                invoice_id,
                invoice_series,
                invoice_number,
                draft_id,
                draft_series,
                draft_number,
                tenant_id,
                company_id,
                client_code,
                client_name,
                client_tax_id,
                issue_date,
                due_date,
                status,
                origin,
                is_deleted,
                legacy_source_system,
                legacy_center_code,
                legacy_document_type,
                legacy_document_number,
                synced_utc,
                shipment_count,
                total_quantity,
                subtotal_amount,
                tax_amount,
                total_amount,
                payment_status,
                amount_paid,
                outstanding_amount,
                last_payment_utc,
                accounting_status,
                accounting_reference,
                accounting_ready_utc,
                notes,
                issued_utc,
                created_utc,
                updated_utc)
            VALUES (
                @invoiceId,
                @invoiceSeries,
                @invoiceNumber,
                @draftId,
                @draftSeries,
                @draftNumber,
                @tenantId,
                @companyId,
                @clientCode,
                @clientName,
                @clientTaxId,
                @issueDate,
                @dueDate,
                @status,
                @origin,
                0,
                @legacySourceSystem,
                @legacyCenterCode,
                @legacyDocumentType,
                @legacyDocumentNumber,
                @syncedUtc,
                @shipmentCount,
                @totalQuantity,
                @subtotalAmount,
                @taxAmount,
                @totalAmount,
                @paymentStatus,
                @amountPaid,
                @outstandingAmount,
                @lastPaymentUtc,
                @accountingStatus,
                @accountingReference,
                @accountingReadyUtc,
                @notes,
                @issuedUtc,
                @createdUtc,
                @updatedUtc)
            ON DUPLICATE KEY UPDATE
                client_code = VALUES(client_code),
                client_name = VALUES(client_name),
                client_tax_id = VALUES(client_tax_id),
                issue_date = VALUES(issue_date),
                due_date = VALUES(due_date),
                status = VALUES(status),
                origin = VALUES(origin),
                is_deleted = VALUES(is_deleted),
                legacy_source_system = VALUES(legacy_source_system),
                legacy_center_code = VALUES(legacy_center_code),
                legacy_document_type = VALUES(legacy_document_type),
                legacy_document_number = VALUES(legacy_document_number),
                synced_utc = VALUES(synced_utc),
                shipment_count = VALUES(shipment_count),
                total_quantity = VALUES(total_quantity),
                subtotal_amount = VALUES(subtotal_amount),
                tax_amount = VALUES(tax_amount),
                total_amount = VALUES(total_amount),
                payment_status = VALUES(payment_status),
                amount_paid = VALUES(amount_paid),
                outstanding_amount = VALUES(outstanding_amount),
                last_payment_utc = VALUES(last_payment_utc),
                accounting_status = VALUES(accounting_status),
                accounting_reference = VALUES(accounting_reference),
                accounting_ready_utc = VALUES(accounting_ready_utc),
                notes = VALUES(notes),
                issued_utc = VALUES(issued_utc),
                updated_utc = VALUES(updated_utc);
            """;
        command.Parameters.AddWithValue("@invoiceId", invoiceRecord.InvoiceId.ToString());
        command.Parameters.AddWithValue("@invoiceSeries", BuildInvoiceSeries(legacyCenterCode));
        command.Parameters.AddWithValue("@invoiceNumber", header.InvoiceNumber);
        command.Parameters.AddWithValue("@draftId", invoiceRecord.DraftId.ToString());
        command.Parameters.AddWithValue("@draftSeries", BuildLegacyDraftSeries(legacyCenterCode));
        command.Parameters.AddWithValue("@draftNumber", header.InvoiceNumber);
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@clientCode", header.ClientCode);
        command.Parameters.AddWithValue("@clientName", header.ClientName);
        command.Parameters.AddWithValue("@clientTaxId", DbValue(header.ClientTaxId));
        command.Parameters.AddWithValue("@issueDate", header.IssueDate);
        command.Parameters.AddWithValue("@dueDate", header.DueDate.HasValue ? header.DueDate.Value : DBNull.Value);
        command.Parameters.AddWithValue("@status", SalesInvoiceStatuses.Issued);
        command.Parameters.AddWithValue("@origin", SalesOrderOrigins.Legacy);
        command.Parameters.AddWithValue("@legacySourceSystem", "legacy");
        command.Parameters.AddWithValue("@legacyCenterCode", legacyCenterCode);
        command.Parameters.AddWithValue("@legacyDocumentType", "F");
        command.Parameters.AddWithValue("@legacyDocumentNumber", header.InvoiceNumber.ToString());
        command.Parameters.AddWithValue("@syncedUtc", nowUtc);
        command.Parameters.AddWithValue("@shipmentCount", lines.Select(line => line.ShipmentNumber).Where(number => number > 0).Distinct().Count());
        command.Parameters.AddWithValue("@totalQuantity", decimal.Round(lines.Sum(line => line.Quantity), 3, MidpointRounding.AwayFromZero));
        command.Parameters.AddWithValue("@subtotalAmount", subtotalAmount);
        command.Parameters.AddWithValue("@taxAmount", taxAmount);
        command.Parameters.AddWithValue("@totalAmount", totalAmount);
        command.Parameters.AddWithValue("@paymentStatus", SalesInvoicePaymentStatuses.Pending);
        command.Parameters.AddWithValue("@amountPaid", 0m);
        command.Parameters.AddWithValue("@outstandingAmount", totalAmount);
        command.Parameters.AddWithValue("@lastPaymentUtc", DBNull.Value);
        command.Parameters.AddWithValue("@accountingStatus", SalesInvoiceAccountingStatuses.Ready);
        command.Parameters.AddWithValue("@accountingReference", DBNull.Value);
        command.Parameters.AddWithValue("@accountingReadyUtc", header.IssueDate);
        command.Parameters.AddWithValue("@notes", DbValue(header.Notes));
        command.Parameters.AddWithValue("@issuedUtc", header.IssueDate);
        command.Parameters.AddWithValue("@createdUtc", nowUtc);
        command.Parameters.AddWithValue("@updatedUtc", nowUtc);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task ReplaceImportedInvoiceLinesAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid invoiceId,
        Guid tenantId,
        Guid companyId,
        IReadOnlyCollection<ImportedSalesInvoiceLine> lines,
        CancellationToken cancellationToken)
    {
        await using (var deleteCommand = CreateTimedCommand(connection, transaction))
        {
            deleteCommand.CommandText = "DELETE FROM sales_invoice_lines WHERE invoice_id = @invoiceId;";
            deleteCommand.Parameters.AddWithValue("@invoiceId", invoiceId.ToString());
            await deleteCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        foreach (var line in lines)
        {
            await using var command = CreateTimedCommand(connection, transaction);
            command.CommandText =
                """
                INSERT INTO sales_invoice_lines (
                    invoice_id,
                    tenant_id,
                    company_id,
                    line_number,
                    item_code,
                    description,
                    quantity,
                    unit_of_measure,
                    unit_price,
                    line_subtotal,
                    tax_rate,
                    tax_amount,
                    line_total,
                    source_summary)
                VALUES (
                    @invoiceId,
                    @tenantId,
                    @companyId,
                    @lineNumber,
                    @itemCode,
                    @description,
                    @quantity,
                    @unitOfMeasure,
                    @unitPrice,
                    @lineSubtotal,
                    @taxRate,
                    @taxAmount,
                    @lineTotal,
                    @sourceSummary);
                """;
            command.Parameters.AddWithValue("@invoiceId", invoiceId.ToString());
            command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
            command.Parameters.AddWithValue("@companyId", companyId.ToString());
            command.Parameters.AddWithValue("@lineNumber", line.LineNumber);
            command.Parameters.AddWithValue("@itemCode", DbValue(line.ItemCode));
            command.Parameters.AddWithValue("@description", line.Description);
            command.Parameters.AddWithValue("@quantity", line.Quantity);
            command.Parameters.AddWithValue("@unitOfMeasure", DBNull.Value);
            command.Parameters.AddWithValue("@unitPrice", line.UnitPrice);
            command.Parameters.AddWithValue("@lineSubtotal", line.LineSubtotal);
            command.Parameters.AddWithValue("@taxRate", line.TaxRate);
            command.Parameters.AddWithValue("@taxAmount", line.TaxAmount);
            command.Parameters.AddWithValue("@lineTotal", line.LineTotal);
            command.Parameters.AddWithValue("@sourceSummary", DbValue(BuildLineSourceSummary(line)));
            await command.ExecuteNonQueryAsync(cancellationToken);
        }
    }

    private static async Task ReplaceImportedInvoiceShipmentsAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid tenantId,
        Guid companyId,
        string legacyCenterCode,
        int invoiceNumber,
        Guid invoiceId,
        IReadOnlyCollection<ImportedSalesInvoiceLine> lines,
        IReadOnlyDictionary<int, ImportedShipmentLookup> shipmentsByNumber,
        DateTime nowUtc,
        List<LegacySyncErrorRecord> errors,
        CancellationToken cancellationToken)
    {
        await using (var deleteCommand = CreateTimedCommand(connection, transaction))
        {
            deleteCommand.CommandText = "DELETE FROM sales_invoice_shipments WHERE invoice_id = @invoiceId;";
            deleteCommand.Parameters.AddWithValue("@invoiceId", invoiceId.ToString());
            await deleteCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        var groupedShipments = lines
            .Where(line => line.ShipmentNumber > 0)
            .GroupBy(line => line.ShipmentNumber)
            .ToArray();

        foreach (var shipmentGroup in groupedShipments)
        {
            if (!shipmentsByNumber.TryGetValue(shipmentGroup.Key, out var shipment))
            {
                errors.Add(new LegacySyncErrorRecord
                {
                    Stage = "LinkShipment",
                    LegacyEntityKey = $"{legacyCenterCode}/F/{invoiceNumber}/{shipmentGroup.Key}",
                    ErrorMessage = "No se ha encontrado el albarán sincronizado para enlazar la factura legacy.",
                    Payload = $"InvoiceNumber={invoiceNumber}; ShipmentNumber={shipmentGroup.Key}"
                });
                continue;
            }

            var estimatedAmount = decimal.Round(shipmentGroup.Sum(line => line.LineTotal), 2, MidpointRounding.AwayFromZero);
            var totalQuantity = decimal.Round(shipmentGroup.Sum(line => line.Quantity), 3, MidpointRounding.AwayFromZero);

            await using (var deleteExistingLinkCommand = CreateTimedCommand(connection, transaction))
            {
                deleteExistingLinkCommand.CommandText =
                    """
                    DELETE FROM sales_invoice_shipments
                    WHERE shipment_id = @shipmentId
                      AND invoice_id <> @invoiceId;
                    """;
                deleteExistingLinkCommand.Parameters.AddWithValue("@shipmentId", shipment.ShipmentId.ToString());
                deleteExistingLinkCommand.Parameters.AddWithValue("@invoiceId", invoiceId.ToString());
                await deleteExistingLinkCommand.ExecuteNonQueryAsync(cancellationToken);
            }

            await using (var insertCommand = CreateTimedCommand(connection, transaction))
            {
                insertCommand.CommandText =
                    """
                    INSERT INTO sales_invoice_shipments (
                        invoice_id,
                        tenant_id,
                        company_id,
                        shipment_id,
                        shipment_series,
                        shipment_number,
                        order_number,
                        shipment_date,
                        warehouse,
                        shipped_quantity,
                        estimated_amount)
                    VALUES (
                        @invoiceId,
                        @tenantId,
                        @companyId,
                        @shipmentId,
                        @shipmentSeries,
                        @shipmentNumber,
                        @orderNumber,
                        @shipmentDate,
                        @warehouse,
                        @shippedQuantity,
                        @estimatedAmount);
                    """;
                insertCommand.Parameters.AddWithValue("@invoiceId", invoiceId.ToString());
                insertCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
                insertCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
                insertCommand.Parameters.AddWithValue("@shipmentId", shipment.ShipmentId.ToString());
                insertCommand.Parameters.AddWithValue("@shipmentSeries", DbValue(shipment.ShipmentSeries));
                insertCommand.Parameters.AddWithValue("@shipmentNumber", shipment.ShipmentNumber);
                insertCommand.Parameters.AddWithValue("@orderNumber", shipment.OrderNumber);
                insertCommand.Parameters.AddWithValue("@shipmentDate", shipment.ShipmentDate);
                insertCommand.Parameters.AddWithValue("@warehouse", DbValue(shipment.Warehouse));
                insertCommand.Parameters.AddWithValue("@shippedQuantity", totalQuantity);
                insertCommand.Parameters.AddWithValue("@estimatedAmount", estimatedAmount);
                await insertCommand.ExecuteNonQueryAsync(cancellationToken);
            }

            await using (var updateShipmentCommand = CreateTimedCommand(connection, transaction))
            {
                updateShipmentCommand.CommandText =
                    """
                    UPDATE sales_order_shipments
                    SET invoice_status = 'Invoiced',
                        invoice_reference = @invoiceReference,
                        invoice_id = @invoiceId,
                        invoice_ready_utc = @invoiceReadyUtc
                    WHERE shipment_id = @shipmentId;
                    """;
                updateShipmentCommand.Parameters.AddWithValue("@invoiceReference", $"{BuildInvoiceSeries(legacyCenterCode)}/{invoiceNumber:000000}");
                updateShipmentCommand.Parameters.AddWithValue("@invoiceId", invoiceId.ToString());
                updateShipmentCommand.Parameters.AddWithValue("@invoiceReadyUtc", nowUtc);
                updateShipmentCommand.Parameters.AddWithValue("@shipmentId", shipment.ShipmentId.ToString());
                await updateShipmentCommand.ExecuteNonQueryAsync(cancellationToken);
            }
        }
    }

    private static async Task<int> MarkMissingImportedInvoicesAsDeletedAsync(
        MySqlConnection connection,
        Guid tenantId,
        Guid companyId,
        IReadOnlyCollection<int> visibleLegacyInvoiceNumbers,
        CancellationToken cancellationToken)
    {
        await using var selectCommand = CreateTimedCommand(connection);
        selectCommand.CommandText =
            """
            SELECT invoice_id, invoice_number
            FROM sales_invoices
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND COALESCE(origin, 'saas') = @origin
              AND COALESCE(is_deleted, 0) = 0;
            """;
        selectCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        selectCommand.Parameters.AddWithValue("@companyId", companyId.ToString());
        selectCommand.Parameters.AddWithValue("@origin", SalesOrderOrigins.Legacy);

        var missingInvoices = new List<(Guid InvoiceId, int InvoiceNumber)>();
        await using (var reader = await selectCommand.ExecuteReaderAsync(cancellationToken))
        {
            while (await reader.ReadAsync(cancellationToken))
            {
                var invoiceNumber = reader.GetInt32(reader.GetOrdinal("invoice_number"));
                if (!visibleLegacyInvoiceNumbers.Contains(invoiceNumber))
                {
                    missingInvoices.Add((reader.GetGuid("invoice_id"), invoiceNumber));
                }
            }
        }

        if (missingInvoices.Count == 0)
        {
            return 0;
        }

        foreach (var invoice in missingInvoices)
        {
            await using (var updateInvoiceCommand = CreateTimedCommand(connection))
            {
                updateInvoiceCommand.CommandText =
                    """
                    UPDATE sales_invoices
                    SET status = @status,
                        is_deleted = 1,
                        synced_utc = @syncedUtc,
                        updated_utc = @updatedUtc
                    WHERE invoice_id = @invoiceId;
                    """;
                updateInvoiceCommand.Parameters.AddWithValue("@status", SalesInvoiceStatuses.Cancelled);
                updateInvoiceCommand.Parameters.AddWithValue("@syncedUtc", DateTime.UtcNow);
                updateInvoiceCommand.Parameters.AddWithValue("@updatedUtc", DateTime.UtcNow);
                updateInvoiceCommand.Parameters.AddWithValue("@invoiceId", invoice.InvoiceId.ToString());
                await updateInvoiceCommand.ExecuteNonQueryAsync(cancellationToken);
            }

            await using var resetShipmentCommand = CreateTimedCommand(connection);
            resetShipmentCommand.CommandText =
                """
                UPDATE sales_order_shipments
                SET invoice_status = 'Pending',
                    invoice_reference = NULL,
                    invoice_id = NULL,
                    invoice_ready_utc = NULL
                WHERE invoice_id = @invoiceId;
                """;
            resetShipmentCommand.Parameters.AddWithValue("@invoiceId", invoice.InvoiceId.ToString());
            await resetShipmentCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        return missingInvoices.Count;
    }

    private static async Task DeleteExistingMappingsAsync(
        MySqlConnection connection,
        LegacySyncModuleContext context,
        CancellationToken cancellationToken)
    {
        await using var command = CreateTimedCommand(connection);
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

    private static string BuildInvoiceSeries(string companyLegacyCenterCode) =>
        $"FV-{(string.IsNullOrWhiteSpace(companyLegacyCenterCode) ? "GEN" : companyLegacyCenterCode.Trim().ToUpperInvariant())}";

    private static string BuildLegacyDraftSeries(string companyLegacyCenterCode) =>
        $"LG-{(string.IsNullOrWhiteSpace(companyLegacyCenterCode) ? "GEN" : companyLegacyCenterCode.Trim().ToUpperInvariant())}";

    private static string BuildLineSourceSummary(ImportedSalesInvoiceLine line)
    {
        var parts = new List<string>();
        if (line.OrderNumber > 0)
        {
            parts.Add($"Pedido {line.OrderNumber}");
        }

        if (line.ShipmentNumber > 0)
        {
            parts.Add($"Albarán {line.ShipmentNumber}");
        }

        return string.Join(" · ", parts);
    }

    private static Guid DeterministicGuid(Guid tenantId, Guid companyId, string entityName, int number)
    {
        var seed = $"{tenantId:N}:{companyId:N}:{entityName}:{number}";
        var bytes = System.Security.Cryptography.MD5.HashData(System.Text.Encoding.UTF8.GetBytes(seed));
        return new Guid(bytes);
    }

    private static object DbValue(string? value) =>
        string.IsNullOrWhiteSpace(value) ? DBNull.Value : value.Trim();

    private static DateTime? ParseLegacyDate(string rawValue)
    {
        if (string.IsNullOrWhiteSpace(rawValue))
        {
            return null;
        }

        var value = rawValue.Trim();
        if (value is "0000-00-00" or "0000-00-00 00:00:00")
        {
            return null;
        }

        if (DateTime.TryParse(value, out var parsed))
        {
            return parsed;
        }

        return null;
    }

    private static MySqlCommand CreateTimedCommand(MySqlConnection connection, MySqlTransaction? transaction = null)
    {
        var command = connection.CreateCommand();
        command.CommandTimeout = SyncCommandTimeoutSeconds;
        if (transaction is not null)
        {
            command.Transaction = transaction;
        }

        return command;
    }

    private sealed record LegacySalesInvoiceHeader(
        int InvoiceNumber,
        int ClientCode,
        string ClientName,
        string ClientTaxId,
        DateTime IssueDate,
        DateTime? DueDate,
        decimal SubtotalAmount,
        decimal TaxAmount,
        decimal TotalAmount,
        string Notes);

    private sealed record LegacySalesInvoiceLine(
        int LineNumber,
        string ItemCode,
        string Description,
        decimal Quantity,
        decimal UnitPrice,
        int ShipmentNumber,
        int OrderNumber);

    private sealed record ExistingInvoiceRecord(
        Guid InvoiceId,
        Guid DraftId,
        string Origin);

    private sealed record ImportedShipmentLookup(
        Guid ShipmentId,
        string ShipmentSeries,
        int ShipmentNumber,
        int OrderNumber,
        DateTime ShipmentDate,
        string Warehouse);

    private sealed record ImportedSalesInvoiceLine(
        int LineNumber,
        string ItemCode,
        string Description,
        decimal Quantity,
        decimal UnitPrice,
        decimal LineSubtotal,
        int ShipmentNumber,
        int OrderNumber)
    {
        public decimal TaxRate { get; init; }
        public decimal TaxAmount { get; init; }
        public decimal LineTotal { get; init; }
    }
}
