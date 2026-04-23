using MySqlConnector;

namespace Erp.Infrastructure.MySql.Database;

public static class ClientNameRepair
{
    private const string PlaceholderPredicate = """
        (
            COALESCE(TRIM({0}), '') = ''
            OR {0} REGEXP '^(Cliente|Client) [0-9]+$'
            OR TRIM({0}) = CAST({1} AS CHAR)
        )
        """;

    public static async Task<IReadOnlyCollection<ClientNameRepairResult>> RunAsync(
        MySqlConnection connection,
        CancellationToken cancellationToken)
    {
        var results = new List<ClientNameRepairResult>
        {
            await RepairSalesOrdersAsync(connection, cancellationToken),
            await RepairSalesInvoiceDraftsAsync(connection, cancellationToken),
            await RepairSalesInvoicesAsync(connection, cancellationToken),
            await RepairSalesRemittanceInvoicesAsync(connection, cancellationToken),
            await RepairModelsAsync(connection, cancellationToken),
            await RepairSamplesAsync(connection, "mostres", cancellationToken),
            await RepairSamplesAsync(connection, "mostres_detail", cancellationToken),
            await RepairSamplesAsync(connection, "mostres_breakdown", cancellationToken)
        };

        return results;
    }

    private static Task<ClientNameRepairResult> RepairSalesOrdersAsync(MySqlConnection connection, CancellationToken cancellationToken)
    {
        var placeholderPredicate = string.Format(PlaceholderPredicate, "so.client_name", "so.client_code");
        return ExecuteAsync(
            connection,
            "sales_orders",
            $"""
            UPDATE sales_orders so
            INNER JOIN (
                SELECT CODI,
                       MAX(NULLIF(NOM, '')) AS client_name,
                       MAX(NULLIF(NIF, '')) AS client_tax_id
                FROM clients
                GROUP BY CODI
            ) client_map
              ON client_map.CODI = so.client_code
            SET so.client_name = client_map.client_name,
                so.client_tax_id = CASE
                    WHEN COALESCE(so.client_tax_id, '') = '' THEN COALESCE(client_map.client_tax_id, '')
                    ELSE so.client_tax_id
                END
            WHERE COALESCE(so.is_deleted, 0) = 0
              AND COALESCE(NULLIF(client_map.client_name, ''), '') <> ''
              AND {placeholderPredicate};
            """,
            cancellationToken);
    }

    private static Task<ClientNameRepairResult> RepairSalesInvoiceDraftsAsync(MySqlConnection connection, CancellationToken cancellationToken)
    {
        var placeholderPredicate = string.Format(PlaceholderPredicate, "sid.client_name", "sid.client_code");
        return ExecuteAsync(
            connection,
            "sales_invoice_drafts",
            $"""
            UPDATE sales_invoice_drafts sid
            INNER JOIN (
                SELECT CODI,
                       MAX(NULLIF(NOM, '')) AS client_name,
                       MAX(NULLIF(NIF, '')) AS client_tax_id
                FROM clients
                GROUP BY CODI
            ) client_map
              ON client_map.CODI = sid.client_code
            SET sid.client_name = client_map.client_name,
                sid.client_tax_id = CASE
                    WHEN COALESCE(sid.client_tax_id, '') = '' THEN COALESCE(client_map.client_tax_id, '')
                    ELSE sid.client_tax_id
                END
            WHERE COALESCE(NULLIF(client_map.client_name, ''), '') <> ''
              AND {placeholderPredicate};
            """,
            cancellationToken);
    }

    private static Task<ClientNameRepairResult> RepairSalesInvoicesAsync(MySqlConnection connection, CancellationToken cancellationToken)
    {
        var placeholderPredicate = string.Format(PlaceholderPredicate, "si.client_name", "si.client_code");
        return ExecuteAsync(
            connection,
            "sales_invoices",
            $"""
            UPDATE sales_invoices si
            INNER JOIN (
                SELECT CODI,
                       MAX(NULLIF(NOM, '')) AS client_name,
                       MAX(NULLIF(NIF, '')) AS client_tax_id
                FROM clients
                GROUP BY CODI
            ) client_map
              ON client_map.CODI = si.client_code
            SET si.client_name = client_map.client_name,
                si.client_tax_id = CASE
                    WHEN COALESCE(si.client_tax_id, '') = '' THEN COALESCE(client_map.client_tax_id, '')
                    ELSE si.client_tax_id
                END
            WHERE COALESCE(si.is_deleted, 0) = 0
              AND COALESCE(NULLIF(client_map.client_name, ''), '') <> ''
              AND {placeholderPredicate};
            """,
            cancellationToken);
    }

    private static Task<ClientNameRepairResult> RepairSalesRemittanceInvoicesAsync(MySqlConnection connection, CancellationToken cancellationToken)
    {
        var placeholderPredicate = string.Format(PlaceholderPredicate, "sri.client_name", "sri.client_code");
        return ExecuteAsync(
            connection,
            "sales_remittance_invoices",
            $"""
            UPDATE sales_remittance_invoices sri
            INNER JOIN (
                SELECT CODI,
                       MAX(NULLIF(NOM, '')) AS client_name
                FROM clients
                GROUP BY CODI
            ) client_map
              ON client_map.CODI = sri.client_code
            SET sri.client_name = client_map.client_name
            WHERE COALESCE(NULLIF(client_map.client_name, ''), '') <> ''
              AND {placeholderPredicate};
            """,
            cancellationToken);
    }

    private static Task<ClientNameRepairResult> RepairModelsAsync(MySqlConnection connection, CancellationToken cancellationToken)
    {
        var placeholderPredicate = string.Format(PlaceholderPredicate, "am.NOMCLIENT", "am.CLIENT");
        return ExecuteAsync(
            connection,
            "article_models",
            $"""
            UPDATE article_models am
            INNER JOIN (
                SELECT CODI,
                       MAX(NULLIF(NOM, '')) AS client_name
                FROM clients
                GROUP BY CODI
            ) client_map
              ON client_map.CODI = am.CLIENT
            SET am.NOMCLIENT = client_map.client_name
            WHERE COALESCE(am.is_deleted, 0) = 0
              AND COALESCE(am.CLIENT, 0) > 0
              AND COALESCE(NULLIF(client_map.client_name, ''), '') <> ''
              AND {placeholderPredicate};
            """,
            cancellationToken);
    }

    private static Task<ClientNameRepairResult> RepairSamplesAsync(
        MySqlConnection connection,
        string tableName,
        CancellationToken cancellationToken)
    {
        var alias = "m";
        var placeholderPredicate = string.Format(PlaceholderPredicate, $"{alias}.NOMCLIENT", $"{alias}.CLIENT");
        return ExecuteAsync(
            connection,
            tableName,
            $"""
            UPDATE {tableName} {alias}
            INNER JOIN (
                SELECT CODI,
                       MAX(NULLIF(NOM, '')) AS client_name
                FROM clients
                GROUP BY CODI
            ) client_map
              ON client_map.CODI = {alias}.CLIENT
            SET {alias}.NOMCLIENT = client_map.client_name
            WHERE COALESCE({alias}.CLIENT, 0) > 0
              AND COALESCE(NULLIF(client_map.client_name, ''), '') <> ''
              AND {placeholderPredicate};
            """,
            cancellationToken);
    }

    private static async Task<ClientNameRepairResult> ExecuteAsync(
        MySqlConnection connection,
        string target,
        string commandText,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText = commandText;
        var rows = await command.ExecuteNonQueryAsync(cancellationToken);
        return new ClientNameRepairResult(target, rows);
    }
}

public sealed record ClientNameRepairResult(string Target, int RowsAffected);
