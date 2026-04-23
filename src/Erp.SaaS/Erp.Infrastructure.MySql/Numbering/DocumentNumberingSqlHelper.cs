using Erp.Application.Numbering;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Numbering;

internal static class DocumentNumberingSqlHelper
{
    public static async Task<int> ReserveNextNumberAsync(
        MySqlConnection connection,
        MySqlTransaction? transaction,
        Guid tenantId,
        Guid companyId,
        string sequenceKey,
        CancellationToken cancellationToken = default)
    {
        var existing = await LoadSequenceAsync(connection, transaction, tenantId, companyId, sequenceKey, cancellationToken);
        var nextNumber = existing is not null && existing.IsActive && existing.NextNumber > 0
            ? existing.NextNumber
            : await GetSuggestedNextNumberAsync(connection, transaction, tenantId, companyId, sequenceKey, cancellationToken);

        await UpsertSequenceAsync(
            connection,
            transaction,
            tenantId,
            companyId,
            sequenceKey,
            existing?.Series ?? string.Empty,
            nextNumber + 1,
            nextNumber,
            existing?.IsActive ?? true,
            existing?.Notes ?? string.Empty,
            cancellationToken);

        return nextNumber;
    }

    public static async Task<int> ReserveNextDispositionNumberAsync(
        MySqlConnection connection,
        MySqlTransaction? transaction,
        Guid tenantId,
        Guid companyId,
        string centerCode,
        string year,
        CancellationToken cancellationToken = default)
    {
        var normalizedYear = NormalizeDispositionYear(year);
        var existing = await LoadDispositionSetupAsync(connection, transaction, tenantId, companyId, cancellationToken);
        var nextNumber = existing is not null &&
                         string.Equals(existing.Year, normalizedYear, StringComparison.OrdinalIgnoreCase) &&
                         existing.NextNumber > 0
            ? existing.NextNumber
            : await GetSuggestedDispositionNextNumberAsync(connection, transaction, centerCode, normalizedYear, cancellationToken);

        await UpsertDispositionSetupAsync(connection, transaction, tenantId, companyId, normalizedYear, nextNumber + 1, cancellationToken);
        return nextNumber;
    }

    public static async Task<int> GetSuggestedNextNumberAsync(
        MySqlConnection connection,
        MySqlTransaction? transaction,
        Guid tenantId,
        Guid companyId,
        string sequenceKey,
        CancellationToken cancellationToken = default)
    {
        var query = sequenceKey switch
        {
            DocumentNumberingKeys.PurchaseOrder => "SELECT COALESCE(MAX(order_number), 0) + 1 FROM purchase_orders WHERE tenant_id = @tenantId AND company_id = @companyId;",
            DocumentNumberingKeys.PurchaseReceipt => "SELECT COALESCE(MAX(receipt_number), 0) + 1 FROM purchase_order_receipts WHERE tenant_id = @tenantId AND company_id = @companyId;",
            DocumentNumberingKeys.PurchaseInvoice => "SELECT COALESCE(MAX(invoice_number), 0) + 1 FROM purchase_invoices WHERE tenant_id = @tenantId AND company_id = @companyId;",
            DocumentNumberingKeys.SalesOrder => "SELECT COALESCE(MAX(order_number), 0) + 1 FROM sales_orders WHERE tenant_id = @tenantId AND company_id = @companyId;",
            DocumentNumberingKeys.SalesShipment => "SELECT COALESCE(MAX(shipment_number), 0) + 1 FROM sales_order_shipments WHERE tenant_id = @tenantId AND company_id = @companyId;",
            DocumentNumberingKeys.SalesInvoiceDraft => "SELECT COALESCE(MAX(draft_number), 0) + 1 FROM sales_invoice_drafts WHERE tenant_id = @tenantId AND company_id = @companyId;",
            DocumentNumberingKeys.SalesInvoice => "SELECT COALESCE(MAX(invoice_number), 0) + 1 FROM sales_invoices WHERE tenant_id = @tenantId AND company_id = @companyId;",
            DocumentNumberingKeys.SalesRemittance => "SELECT COALESCE(MAX(remittance_number), 0) + 1 FROM sales_remittances WHERE tenant_id = @tenantId AND company_id = @companyId;",
            DocumentNumberingKeys.StockTransfer => "SELECT COALESCE(MAX(transfer_number), 0) + 1 FROM stock_transfers WHERE tenant_id = @tenantId AND company_id = @companyId;",
            DocumentNumberingKeys.StockCount => "SELECT COALESCE(MAX(count_number), 0) + 1 FROM stock_counts WHERE tenant_id = @tenantId AND company_id = @companyId;",
            DocumentNumberingKeys.FinishWorkOrder => "SELECT COALESCE(MAX(order_number), 0) + 1 FROM finish_work_orders WHERE tenant_id = @tenantId AND company_id = @companyId;",
            _ => "SELECT 1;"
        };

        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = query;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());

        var suggested = Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));
        return Math.Max(1, suggested);
    }

    public static async Task<int> GetSuggestedDispositionNextNumberAsync(
        MySqlConnection connection,
        MySqlTransaction? transaction,
        string centerCode,
        string year,
        CancellationToken cancellationToken = default)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            SELECT COALESCE(MAX(IDDISPOS), 0) + 1
            FROM dispos
            WHERE CENTRO = @centerCode
              AND ANY = @year;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@year", NormalizeDispositionYear(year));

        var suggested = Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));
        return Math.Max(1, suggested);
    }

    public static async Task<IReadOnlyDictionary<string, SequenceState>> LoadSequencesAsync(
        MySqlConnection connection,
        MySqlTransaction? transaction,
        Guid tenantId,
        Guid companyId,
        CancellationToken cancellationToken = default)
    {
        var items = new Dictionary<string, SequenceState>(StringComparer.OrdinalIgnoreCase);

        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            SELECT sequence_key,
                   series,
                   next_number,
                   last_number,
                   is_active,
                   notes
            FROM document_numbering_sequences
            WHERE tenant_id = @tenantId
              AND company_id = @companyId;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var key = reader.GetString("sequence_key");
            items[key] = new SequenceState(
                key,
                reader.IsDBNull(reader.GetOrdinal("series")) ? string.Empty : reader.GetString("series"),
                reader.GetInt32("next_number"),
                reader.GetInt32("last_number"),
                !reader.IsDBNull(reader.GetOrdinal("is_active")) && reader.GetBoolean("is_active"),
                reader.IsDBNull(reader.GetOrdinal("notes")) ? string.Empty : reader.GetString("notes"));
        }

        return items;
    }

    public static async Task<DispositionSetupState?> LoadDispositionSetupAsync(
        MySqlConnection connection,
        MySqlTransaction? transaction,
        Guid tenantId,
        Guid companyId,
        CancellationToken cancellationToken = default)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            SELECT disposition_year,
                   next_number
            FROM document_numbering_disposition_settings
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        return new DispositionSetupState(
            reader.IsDBNull(reader.GetOrdinal("disposition_year")) ? string.Empty : reader.GetString("disposition_year"),
            reader.GetInt32("next_number"));
    }

    public static async Task UpsertSequenceAsync(
        MySqlConnection connection,
        MySqlTransaction? transaction,
        Guid tenantId,
        Guid companyId,
        string sequenceKey,
        string series,
        int nextNumber,
        int lastNumber,
        bool isActive,
        string notes,
        CancellationToken cancellationToken = default)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            INSERT INTO document_numbering_sequences (
                tenant_id,
                company_id,
                sequence_key,
                series,
                next_number,
                last_number,
                is_active,
                notes,
                created_utc,
                updated_utc)
            VALUES (
                @tenantId,
                @companyId,
                @sequenceKey,
                @series,
                @nextNumber,
                @lastNumber,
                @isActive,
                @notes,
                @createdUtc,
                @updatedUtc)
            ON DUPLICATE KEY UPDATE
                series = VALUES(series),
                next_number = VALUES(next_number),
                last_number = VALUES(last_number),
                is_active = VALUES(is_active),
                notes = VALUES(notes),
                updated_utc = VALUES(updated_utc);
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@sequenceKey", sequenceKey);
        command.Parameters.AddWithValue("@series", series.Trim());
        command.Parameters.AddWithValue("@nextNumber", Math.Max(1, nextNumber));
        command.Parameters.AddWithValue("@lastNumber", Math.Max(0, lastNumber));
        command.Parameters.AddWithValue("@isActive", isActive);
        command.Parameters.AddWithValue("@notes", notes.Trim());
        command.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
        command.Parameters.AddWithValue("@updatedUtc", DateTime.UtcNow);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public static async Task UpsertDispositionSetupAsync(
        MySqlConnection connection,
        MySqlTransaction? transaction,
        Guid tenantId,
        Guid companyId,
        string year,
        int nextNumber,
        CancellationToken cancellationToken = default)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            INSERT INTO document_numbering_disposition_settings (
                tenant_id,
                company_id,
                disposition_year,
                next_number,
                created_utc,
                updated_utc)
            VALUES (
                @tenantId,
                @companyId,
                @year,
                @nextNumber,
                @createdUtc,
                @updatedUtc)
            ON DUPLICATE KEY UPDATE
                disposition_year = VALUES(disposition_year),
                next_number = VALUES(next_number),
                updated_utc = VALUES(updated_utc);
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@year", NormalizeDispositionYear(year));
        command.Parameters.AddWithValue("@nextNumber", Math.Max(1, nextNumber));
        command.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
        command.Parameters.AddWithValue("@updatedUtc", DateTime.UtcNow);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public static string NormalizeDispositionYear(string? year)
    {
        var trimmed = (year ?? string.Empty).Trim();
        return string.IsNullOrWhiteSpace(trimmed) ? DateTime.Today.Year.ToString() : trimmed;
    }

    private static async Task<SequenceState?> LoadSequenceAsync(
        MySqlConnection connection,
        MySqlTransaction? transaction,
        Guid tenantId,
        Guid companyId,
        string sequenceKey,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            SELECT sequence_key,
                   series,
                   next_number,
                   last_number,
                   is_active,
                   notes
            FROM document_numbering_sequences
            WHERE tenant_id = @tenantId
              AND company_id = @companyId
              AND sequence_key = @sequenceKey
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@sequenceKey", sequenceKey);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        return new SequenceState(
            reader.GetString("sequence_key"),
            reader.IsDBNull(reader.GetOrdinal("series")) ? string.Empty : reader.GetString("series"),
            reader.GetInt32("next_number"),
            reader.GetInt32("last_number"),
            !reader.IsDBNull(reader.GetOrdinal("is_active")) && reader.GetBoolean("is_active"),
            reader.IsDBNull(reader.GetOrdinal("notes")) ? string.Empty : reader.GetString("notes"));
    }

    internal sealed record SequenceState(
        string SequenceKey,
        string Series,
        int NextNumber,
        int LastNumber,
        bool IsActive,
        string Notes);

    internal sealed record DispositionSetupState(
        string Year,
        int NextNumber);
}
