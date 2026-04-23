using Erp.Domain.Common;
using Erp.Infrastructure.MySql.Configuration;
using Erp.Infrastructure.MySql.Support;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Database;

public sealed class SchemaBootstrapper : IHostedService
{
    private readonly MySqlConnectionFactory _connectionFactory;
    private readonly IOptions<SaasDatabaseOptions> _databaseOptions;
    private readonly IOptions<BootstrapSeedOptions> _seedOptions;
    private readonly ILogger<SchemaBootstrapper> _logger;

    public SchemaBootstrapper(
        MySqlConnectionFactory connectionFactory,
        IOptions<SaasDatabaseOptions> databaseOptions,
        IOptions<BootstrapSeedOptions> seedOptions,
        ILogger<SchemaBootstrapper> logger)
    {
        _connectionFactory = connectionFactory;
        _databaseOptions = databaseOptions;
        _seedOptions = seedOptions;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var settings = _databaseOptions.Value;
        if (!settings.IsConfigured || !settings.BootstrapOnStartup)
        {
            _logger.LogInformation("MySQL bootstrap skipped. Configured: {Configured}, BootstrapOnStartup: {BootstrapOnStartup}",
                settings.IsConfigured,
                settings.BootstrapOnStartup);
            return;
        }

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await EnsureSchemaAsync(connection, cancellationToken);
        var clientNameRepairResults = await ClientNameRepair.RunAsync(connection, cancellationToken);
        var repairedRows = clientNameRepairResults.Sum(item => item.RowsAffected);
        if (repairedRows > 0)
        {
            var summary = string.Join(", ", clientNameRepairResults.Where(item => item.RowsAffected > 0).Select(item => $"{item.Target}={item.RowsAffected}"));
            _logger.LogInformation("Client name repair applied during bootstrap: {Summary}", summary);
        }
        await SeedDefaultsAsync(connection, cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private static async Task EnsureSchemaAsync(MySqlConnection connection, CancellationToken cancellationToken)
    {
        var statements = new[]
        {
            """
            CREATE TABLE IF NOT EXISTS tenants (
                id CHAR(36) NOT NULL PRIMARY KEY,
                name VARCHAR(200) NOT NULL,
                slug VARCHAR(120) NOT NULL,
                is_active TINYINT(1) NOT NULL DEFAULT 1,
                created_utc DATETIME(6) NOT NULL,
                UNIQUE KEY uq_tenants_slug (slug)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS companies (
                id CHAR(36) NOT NULL PRIMARY KEY,
                tenant_id CHAR(36) NOT NULL,
                name VARCHAR(200) NOT NULL,
                slug VARCHAR(120) NOT NULL,
                legacy_center_code CHAR(1) NOT NULL,
                is_active TINYINT(1) NOT NULL DEFAULT 1,
                created_utc DATETIME(6) NOT NULL,
                UNIQUE KEY uq_companies_tenant_slug (tenant_id, slug),
                UNIQUE KEY uq_companies_tenant_legacy_center (tenant_id, legacy_center_code)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS tenant_module_settings (
                tenant_id CHAR(36) NOT NULL,
                module_key VARCHAR(120) NOT NULL,
                data_scope VARCHAR(30) NOT NULL,
                updated_by_user_id CHAR(36) NULL,
                updated_utc DATETIME(6) NOT NULL,
                PRIMARY KEY (tenant_id, module_key)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS app_users (
                id CHAR(36) NOT NULL PRIMARY KEY,
                email VARCHAR(256) NOT NULL,
                display_name VARCHAR(200) NOT NULL,
                password_hash VARCHAR(512) NOT NULL,
                require_password_change TINYINT(1) NOT NULL DEFAULT 0,
                is_active TINYINT(1) NOT NULL DEFAULT 1,
                created_utc DATETIME(6) NOT NULL,
                UNIQUE KEY uq_app_users_email (email)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS user_tenant_memberships (
                user_id CHAR(36) NOT NULL,
                tenant_id CHAR(36) NOT NULL,
                is_default TINYINT(1) NOT NULL DEFAULT 1,
                created_utc DATETIME(6) NOT NULL,
                PRIMARY KEY (user_id, tenant_id)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS user_company_memberships (
                user_id CHAR(36) NOT NULL,
                tenant_id CHAR(36) NOT NULL,
                company_id CHAR(36) NOT NULL,
                created_utc DATETIME(6) NOT NULL,
                PRIMARY KEY (user_id, company_id)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS user_role_assignments (
                user_id CHAR(36) NOT NULL,
                tenant_id CHAR(36) NULL,
                role_name VARCHAR(100) NOT NULL,
                created_utc DATETIME(6) NOT NULL,
                UNIQUE KEY uq_user_role_assignment (user_id, tenant_id, role_name)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS audit_logs (
                id CHAR(36) NOT NULL PRIMARY KEY,
                tenant_id CHAR(36) NULL,
                company_id CHAR(36) NULL,
                user_id CHAR(36) NULL,
                action VARCHAR(100) NOT NULL,
                entity_name VARCHAR(100) NOT NULL,
                entity_id VARCHAR(120) NULL,
                details TEXT NULL,
                created_utc DATETIME(6) NOT NULL
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS plan_definitions (
                id CHAR(36) NOT NULL PRIMARY KEY,
                slug VARCHAR(50) NOT NULL,
                name VARCHAR(120) NOT NULL,
                max_users INT NOT NULL,
                monthly_price DECIMAL(10,2) NOT NULL,
                description VARCHAR(255) NOT NULL,
                is_active TINYINT(1) NOT NULL DEFAULT 1,
                created_utc DATETIME(6) NOT NULL,
                UNIQUE KEY uq_plan_definitions_slug (slug)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS lead_requests (
                id CHAR(36) NOT NULL PRIMARY KEY,
                contact_name VARCHAR(200) NOT NULL,
                company_name VARCHAR(200) NOT NULL,
                email VARCHAR(256) NOT NULL,
                phone VARCHAR(50) NULL,
                requested_users INT NOT NULL,
                message TEXT NULL,
                status VARCHAR(50) NOT NULL,
                created_utc DATETIME(6) NOT NULL
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS base_catalog_items (
                tenant_id CHAR(36) NOT NULL,
                company_id CHAR(36) NOT NULL,
                catalog_key VARCHAR(40) NOT NULL,
                code VARCHAR(40) NOT NULL,
                name VARCHAR(200) NOT NULL,
                description VARCHAR(255) NULL,
                reference_value VARCHAR(255) NULL,
                secondary_reference_value VARCHAR(255) NULL,
                numeric_value DECIMAL(18,4) NULL,
                secondary_numeric_value DECIMAL(18,4) NULL,
                notes TEXT NULL,
                is_active TINYINT(1) NOT NULL DEFAULT 1,
                origin VARCHAR(20) NOT NULL DEFAULT 'local',
                is_deleted TINYINT(1) NOT NULL DEFAULT 0,
                synced_utc DATETIME(6) NULL,
                created_utc DATETIME(6) NOT NULL,
                updated_utc DATETIME(6) NOT NULL,
                PRIMARY KEY (tenant_id, company_id, catalog_key, code),
                KEY ix_base_catalog_items_lookup (tenant_id, company_id, catalog_key, is_deleted, is_active, name)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS document_numbering_sequences (
                tenant_id CHAR(36) NOT NULL,
                company_id CHAR(36) NOT NULL,
                sequence_key VARCHAR(60) NOT NULL,
                series VARCHAR(30) NULL,
                next_number INT NOT NULL DEFAULT 1,
                last_number INT NOT NULL DEFAULT 0,
                is_active TINYINT(1) NOT NULL DEFAULT 1,
                notes TEXT NULL,
                created_utc DATETIME(6) NOT NULL,
                updated_utc DATETIME(6) NOT NULL,
                PRIMARY KEY (tenant_id, company_id, sequence_key)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS document_numbering_disposition_settings (
                tenant_id CHAR(36) NOT NULL,
                company_id CHAR(36) NOT NULL,
                disposition_year VARCHAR(12) NOT NULL,
                next_number INT NOT NULL DEFAULT 1,
                created_utc DATETIME(6) NOT NULL,
                updated_utc DATETIME(6) NOT NULL,
                PRIMARY KEY (tenant_id, company_id)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS mailing_campaigns (
                campaign_id CHAR(36) NOT NULL PRIMARY KEY,
                tenant_id CHAR(36) NOT NULL,
                company_id CHAR(36) NOT NULL,
                source_type VARCHAR(40) NOT NULL,
                title VARCHAR(200) NOT NULL,
                body_text LONGTEXT NOT NULL,
                notes TEXT NULL,
                include_all_recipients TINYINT(1) NOT NULL DEFAULT 0,
                recipient_count INT NOT NULL DEFAULT 0,
                is_deleted TINYINT(1) NOT NULL DEFAULT 0,
                created_utc DATETIME(6) NOT NULL,
                updated_utc DATETIME(6) NOT NULL,
                KEY ix_mailing_campaigns_lookup (tenant_id, company_id, is_deleted, updated_utc),
                KEY ix_mailing_campaigns_source (tenant_id, company_id, source_type, is_deleted)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS mailing_campaign_recipients (
                campaign_id CHAR(36) NOT NULL,
                tenant_id CHAR(36) NOT NULL,
                company_id CHAR(36) NOT NULL,
                line_number INT NOT NULL,
                recipient_code INT NOT NULL,
                recipient_name VARCHAR(200) NOT NULL,
                recipient_tax_id VARCHAR(64) NULL,
                address_line VARCHAR(255) NULL,
                postal_code VARCHAR(32) NULL,
                city VARCHAR(120) NULL,
                province VARCHAR(120) NULL,
                email VARCHAR(255) NULL,
                PRIMARY KEY (campaign_id, line_number),
                KEY ix_mailing_campaign_recipients_lookup (tenant_id, company_id, campaign_id)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS demo_access_requests (
                id CHAR(36) NOT NULL PRIMARY KEY,
                contact_name VARCHAR(200) NOT NULL,
                company_name VARCHAR(200) NOT NULL,
                email VARCHAR(256) NOT NULL,
                phone VARCHAR(50) NULL,
                requested_users INT NOT NULL,
                tester_emails TEXT NULL,
                message TEXT NULL,
                status VARCHAR(50) NOT NULL,
                created_utc DATETIME(6) NOT NULL,
                reviewed_utc DATETIME(6) NULL,
                reviewed_by_user_id CHAR(36) NULL
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS preview_access_invites (
                id CHAR(36) NOT NULL PRIMARY KEY,
                email VARCHAR(256) NOT NULL,
                display_name VARCHAR(200) NULL,
                company_name VARCHAR(200) NULL,
                notes TEXT NULL,
                source_request_id CHAR(36) NULL,
                created_by_user_id CHAR(36) NULL,
                is_active TINYINT(1) NOT NULL DEFAULT 1,
                created_utc DATETIME(6) NOT NULL,
                updated_utc DATETIME(6) NOT NULL,
                UNIQUE KEY uq_preview_access_invites_email (email)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS legacy_sync_jobs (
                id CHAR(36) NOT NULL PRIMARY KEY,
                tenant_id CHAR(36) NOT NULL,
                company_id CHAR(36) NOT NULL,
                module_key VARCHAR(120) NOT NULL,
                module_display_name VARCHAR(120) NOT NULL,
                status VARCHAR(30) NOT NULL,
                triggered_by_user_id CHAR(36) NULL,
                triggered_by_scheduler TINYINT(1) NOT NULL DEFAULT 0,
                checkpoint_before TEXT NULL,
                checkpoint_after TEXT NULL,
                records_inserted INT NOT NULL DEFAULT 0,
                records_updated INT NOT NULL DEFAULT 0,
                records_skipped INT NOT NULL DEFAULT 0,
                errors_count INT NOT NULL DEFAULT 0,
                summary TEXT NULL,
                started_utc DATETIME(6) NOT NULL,
                finished_utc DATETIME(6) NULL,
                KEY ix_legacy_sync_jobs_lookup (tenant_id, module_key, started_utc),
                KEY ix_legacy_sync_jobs_company (tenant_id, company_id, module_key, started_utc)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS legacy_sync_checkpoints (
                tenant_id CHAR(36) NOT NULL,
                company_id CHAR(36) NOT NULL,
                module_key VARCHAR(120) NOT NULL,
                checkpoint_value TEXT NULL,
                last_successful_job_id CHAR(36) NULL,
                last_started_utc DATETIME(6) NULL,
                last_completed_utc DATETIME(6) NULL,
                last_status VARCHAR(30) NOT NULL,
                last_inserted INT NOT NULL DEFAULT 0,
                last_updated INT NOT NULL DEFAULT 0,
                last_skipped INT NOT NULL DEFAULT 0,
                last_errors INT NOT NULL DEFAULT 0,
                PRIMARY KEY (tenant_id, company_id, module_key)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS legacy_sync_errors (
                id CHAR(36) NOT NULL PRIMARY KEY,
                job_id CHAR(36) NOT NULL,
                tenant_id CHAR(36) NOT NULL,
                company_id CHAR(36) NOT NULL,
                module_key VARCHAR(120) NOT NULL,
                stage VARCHAR(50) NOT NULL,
                legacy_entity_key VARCHAR(255) NOT NULL,
                error_message TEXT NOT NULL,
                payload LONGTEXT NULL,
                created_utc DATETIME(6) NOT NULL,
                KEY ix_legacy_sync_errors_job (job_id),
                KEY ix_legacy_sync_errors_lookup (tenant_id, company_id, module_key, created_utc)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS legacy_sync_mappings (
                tenant_id CHAR(36) NOT NULL,
                company_id CHAR(36) NOT NULL,
                module_key VARCHAR(120) NOT NULL,
                legacy_source_system VARCHAR(40) NOT NULL,
                legacy_center_code CHAR(1) NOT NULL,
                legacy_document_type VARCHAR(40) NOT NULL,
                legacy_document_number VARCHAR(80) NOT NULL,
                legacy_line_number INT NOT NULL DEFAULT 0,
                target_entity_name VARCHAR(80) NOT NULL,
                target_entity_id VARCHAR(120) NOT NULL,
                job_id CHAR(36) NULL,
                synced_utc DATETIME(6) NOT NULL,
                UNIQUE KEY uq_legacy_sync_mapping (
                    tenant_id,
                    company_id,
                    module_key,
                    legacy_source_system,
                    legacy_center_code,
                    legacy_document_type,
                    legacy_document_number,
                    legacy_line_number,
                    target_entity_name
                )
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS client_duplicate_reviews (
                tenant_id CHAR(36) NOT NULL,
                company_id CHAR(36) NOT NULL,
                left_client_code INT NOT NULL,
                right_client_code INT NOT NULL,
                status VARCHAR(30) NOT NULL,
                updated_by_user_id CHAR(36) NULL,
                updated_utc DATETIME(6) NOT NULL,
                PRIMARY KEY (tenant_id, company_id, left_client_code, right_client_code)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS client_contacts (
                id CHAR(36) NOT NULL PRIMARY KEY,
                tenant_id CHAR(36) NOT NULL,
                company_id CHAR(36) NOT NULL,
                client_code INT NOT NULL,
                display_order INT NOT NULL DEFAULT 0,
                name VARCHAR(200) NOT NULL,
                role_name VARCHAR(120) NULL,
                email VARCHAR(255) NULL,
                phone VARCHAR(80) NULL,
                notes TEXT NULL,
                is_primary TINYINT(1) NOT NULL DEFAULT 0,
                created_utc DATETIME(6) NOT NULL,
                updated_utc DATETIME(6) NOT NULL,
                KEY ix_client_contacts_lookup (tenant_id, company_id, client_code, display_order)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS purchase_orders (
                tenant_id CHAR(36) NOT NULL,
                company_id CHAR(36) NOT NULL,
                order_number INT NOT NULL,
                supplier_code INT NOT NULL,
                supplier_name VARCHAR(200) NOT NULL,
                supplier_tax_id VARCHAR(50) NULL,
                document_date DATETIME(6) NOT NULL,
                expected_date DATETIME(6) NULL,
                status VARCHAR(40) NOT NULL,
                origin VARCHAR(20) NOT NULL DEFAULT 'saas',
                is_deleted TINYINT(1) NOT NULL DEFAULT 0,
                legacy_source_system VARCHAR(40) NULL,
                legacy_center_code CHAR(1) NULL,
                legacy_document_type VARCHAR(40) NULL,
                legacy_document_number VARCHAR(80) NULL,
                synced_utc DATETIME(6) NULL,
                notes TEXT NULL,
                created_utc DATETIME(6) NOT NULL,
                updated_utc DATETIME(6) NOT NULL,
                PRIMARY KEY (tenant_id, company_id, order_number),
                KEY ix_purchase_orders_supplier (tenant_id, company_id, supplier_code),
                KEY ix_purchase_orders_status (tenant_id, company_id, status)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS purchase_order_lines (
                tenant_id CHAR(36) NOT NULL,
                company_id CHAR(36) NOT NULL,
                order_number INT NOT NULL,
                line_number INT NOT NULL,
                item_code VARCHAR(120) NULL,
                description VARCHAR(255) NOT NULL,
                quantity DECIMAL(18,3) NOT NULL,
                received_quantity DECIMAL(18,3) NOT NULL DEFAULT 0,
                unit_of_measure VARCHAR(30) NULL,
                unit_price DECIMAL(18,4) NOT NULL,
                expected_date DATETIME(6) NULL,
                last_received_utc DATETIME(6) NULL,
                legacy_source_system VARCHAR(40) NULL,
                legacy_center_code CHAR(1) NULL,
                legacy_document_type VARCHAR(40) NULL,
                legacy_document_number VARCHAR(80) NULL,
                legacy_line_number INT NULL,
                synced_utc DATETIME(6) NULL,
                notes TEXT NULL,
                PRIMARY KEY (tenant_id, company_id, order_number, line_number),
                KEY ix_purchase_order_lines_lookup (tenant_id, company_id, order_number)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS purchase_order_receipts (
                receipt_id CHAR(36) NOT NULL PRIMARY KEY,
                receipt_series VARCHAR(20) NULL,
                receipt_number INT NULL,
                tenant_id CHAR(36) NOT NULL,
                company_id CHAR(36) NOT NULL,
                order_number INT NOT NULL,
                receipt_date DATETIME(6) NOT NULL,
                warehouse VARCHAR(120) NULL,
                origin VARCHAR(20) NOT NULL DEFAULT 'saas',
                is_deleted TINYINT(1) NOT NULL DEFAULT 0,
                legacy_source_system VARCHAR(40) NULL,
                legacy_center_code CHAR(1) NULL,
                legacy_document_type VARCHAR(40) NULL,
                legacy_document_number VARCHAR(80) NULL,
                synced_utc DATETIME(6) NULL,
                carrier VARCHAR(160) NULL,
                supplier_reference VARCHAR(120) NULL,
                vehicle_plate VARCHAR(40) NULL,
                package_count INT NULL,
                gross_weight_kg DECIMAL(18,3) NULL,
                notes TEXT NULL,
                created_utc DATETIME(6) NOT NULL,
                KEY ix_purchase_order_receipts_number (tenant_id, company_id, receipt_number),
                KEY ix_purchase_order_receipts_lookup (tenant_id, company_id, order_number, receipt_date)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS purchase_order_receipt_lines (
                receipt_id CHAR(36) NOT NULL,
                tenant_id CHAR(36) NOT NULL,
                company_id CHAR(36) NOT NULL,
                order_number INT NOT NULL,
                line_number INT NOT NULL,
                description VARCHAR(255) NOT NULL,
                received_quantity DECIMAL(18,3) NOT NULL,
                legacy_source_system VARCHAR(40) NULL,
                legacy_center_code CHAR(1) NULL,
                legacy_document_type VARCHAR(40) NULL,
                legacy_document_number VARCHAR(80) NULL,
                legacy_line_number INT NULL,
                synced_utc DATETIME(6) NULL,
                PRIMARY KEY (receipt_id, line_number),
                KEY ix_purchase_order_receipt_lines_lookup (tenant_id, company_id, order_number, line_number)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS purchase_invoices (
                invoice_id CHAR(36) NOT NULL PRIMARY KEY,
                invoice_series VARCHAR(20) NULL,
                invoice_number INT NOT NULL,
                tenant_id CHAR(36) NOT NULL,
                company_id CHAR(36) NOT NULL,
                supplier_code INT NOT NULL,
                supplier_name VARCHAR(200) NOT NULL,
                supplier_tax_id VARCHAR(50) NULL,
                supplier_document_number VARCHAR(120) NULL,
                document_date DATETIME(6) NOT NULL,
                due_date DATETIME(6) NULL,
                status VARCHAR(30) NOT NULL,
                total_net_amount DECIMAL(18,2) NOT NULL DEFAULT 0,
                total_tax_amount DECIMAL(18,2) NOT NULL DEFAULT 0,
                total_amount DECIMAL(18,2) NOT NULL DEFAULT 0,
                origin VARCHAR(20) NOT NULL DEFAULT 'local',
                is_deleted TINYINT(1) NOT NULL DEFAULT 0,
                legacy_source_system VARCHAR(40) NULL,
                legacy_center_code CHAR(1) NULL,
                legacy_document_type VARCHAR(40) NULL,
                legacy_document_number VARCHAR(80) NULL,
                synced_utc DATETIME(6) NULL,
                notes TEXT NULL,
                created_utc DATETIME(6) NOT NULL,
                updated_utc DATETIME(6) NOT NULL,
                UNIQUE KEY uq_purchase_invoices_number (tenant_id, company_id, invoice_number),
                KEY ix_purchase_invoices_supplier (tenant_id, company_id, supplier_code),
                KEY ix_purchase_invoices_status (tenant_id, company_id, status)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS purchase_invoice_lines (
                invoice_id CHAR(36) NOT NULL,
                line_number INT NOT NULL,
                item_code VARCHAR(120) NULL,
                description VARCHAR(255) NOT NULL,
                quantity DECIMAL(18,3) NOT NULL,
                unit_of_measure VARCHAR(30) NULL,
                unit_price DECIMAL(18,4) NOT NULL,
                tax_rate DECIMAL(9,4) NOT NULL DEFAULT 21,
                source_order_number INT NULL,
                source_receipt_number INT NULL,
                PRIMARY KEY (invoice_id, line_number),
                KEY ix_purchase_invoice_lines_receipt (source_receipt_number),
                KEY ix_purchase_invoice_lines_order (source_order_number)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS purchase_invoice_receipts (
                invoice_id CHAR(36) NOT NULL,
                receipt_id CHAR(36) NULL,
                receipt_series VARCHAR(20) NULL,
                receipt_number INT NOT NULL,
                order_number INT NOT NULL DEFAULT 0,
                receipt_date DATETIME(6) NOT NULL,
                total_received_quantity DECIMAL(18,3) NOT NULL DEFAULT 0,
                PRIMARY KEY (invoice_id, receipt_number),
                KEY ix_purchase_invoice_receipts_receipt (receipt_number),
                KEY ix_purchase_invoice_receipts_order (order_number)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS purchase_invoice_payments (
                payment_id CHAR(36) NOT NULL PRIMARY KEY,
                invoice_id CHAR(36) NOT NULL,
                tenant_id CHAR(36) NOT NULL,
                company_id CHAR(36) NOT NULL,
                payment_number INT NOT NULL,
                payment_date DATETIME(6) NOT NULL,
                amount DECIMAL(18,2) NOT NULL,
                method VARCHAR(80) NULL,
                reference VARCHAR(120) NULL,
                notes TEXT NULL,
                created_utc DATETIME(6) NOT NULL,
                UNIQUE KEY uq_purchase_invoice_payments_number (invoice_id, payment_number),
                KEY ix_purchase_invoice_payments_lookup (tenant_id, company_id, invoice_id, payment_date)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS inventory_movements (
                movement_id CHAR(36) NOT NULL PRIMARY KEY,
                tenant_id CHAR(36) NOT NULL,
                company_id CHAR(36) NOT NULL,
                movement_type VARCHAR(40) NOT NULL,
                movement_date DATETIME(6) NOT NULL,
                warehouse VARCHAR(120) NULL,
                item_code VARCHAR(120) NULL,
                item_description VARCHAR(255) NOT NULL,
                color VARCHAR(120) NULL,
                quantity DECIMAL(18,3) NOT NULL,
                unit_of_measure VARCHAR(30) NULL,
                supplier_code INT NULL,
                supplier_name VARCHAR(200) NULL,
                supplier_reference VARCHAR(120) NULL,
                vehicle_plate VARCHAR(40) NULL,
                source_document_type VARCHAR(40) NOT NULL,
                source_document_id CHAR(36) NULL,
                source_document_number INT NULL,
                source_line_number INT NULL,
                notes TEXT NULL,
                created_utc DATETIME(6) NOT NULL,
                UNIQUE KEY uq_inventory_movements_source_line (source_document_type, source_document_id, source_line_number),
                KEY ix_inventory_movements_lookup (tenant_id, company_id, movement_date),
                KEY ix_inventory_movements_item (tenant_id, company_id, item_code),
                KEY ix_inventory_movements_warehouse (tenant_id, company_id, warehouse)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS stock_transfers (
                transfer_id CHAR(36) NOT NULL PRIMARY KEY,
                tenant_id CHAR(36) NOT NULL,
                company_id CHAR(36) NOT NULL,
                transfer_number INT NOT NULL,
                transfer_date DATETIME(6) NOT NULL,
                status VARCHAR(30) NOT NULL,
                from_warehouse VARCHAR(120) NOT NULL,
                to_warehouse VARCHAR(120) NOT NULL,
                line_count INT NOT NULL DEFAULT 0,
                total_quantity DECIMAL(18,3) NOT NULL DEFAULT 0,
                notes TEXT NULL,
                origin VARCHAR(20) NOT NULL DEFAULT 'local',
                is_deleted TINYINT(1) NOT NULL DEFAULT 0,
                created_utc DATETIME(6) NOT NULL,
                updated_utc DATETIME(6) NOT NULL,
                UNIQUE KEY uq_stock_transfers_number (tenant_id, company_id, transfer_number),
                KEY ix_stock_transfers_status (tenant_id, company_id, status),
                KEY ix_stock_transfers_lookup (tenant_id, company_id, transfer_date),
                KEY ix_stock_transfers_warehouse (tenant_id, company_id, from_warehouse, to_warehouse)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS stock_transfer_lines (
                transfer_id CHAR(36) NOT NULL,
                line_number INT NOT NULL,
                item_code VARCHAR(120) NULL,
                item_description VARCHAR(255) NOT NULL,
                color VARCHAR(120) NULL,
                quantity DECIMAL(18,3) NOT NULL DEFAULT 0,
                unit_of_measure VARCHAR(30) NULL,
                notes TEXT NULL,
                PRIMARY KEY (transfer_id, line_number),
                KEY ix_stock_transfer_lines_item (item_code),
                KEY ix_stock_transfer_lines_color (color)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS stock_counts (
                count_id CHAR(36) NOT NULL PRIMARY KEY,
                tenant_id CHAR(36) NOT NULL,
                company_id CHAR(36) NOT NULL,
                count_number INT NOT NULL,
                count_date DATETIME(6) NOT NULL,
                status VARCHAR(30) NOT NULL,
                warehouse VARCHAR(120) NOT NULL,
                is_blind_count TINYINT(1) NOT NULL DEFAULT 0,
                is_blind_count_revealed TINYINT(1) NOT NULL DEFAULT 0,
                line_count INT NOT NULL DEFAULT 0,
                difference_line_count INT NOT NULL DEFAULT 0,
                expected_total_quantity DECIMAL(18,3) NOT NULL DEFAULT 0,
                counted_total_quantity DECIMAL(18,3) NOT NULL DEFAULT 0,
                difference_total_quantity DECIMAL(18,3) NOT NULL DEFAULT 0,
                notes TEXT NULL,
                origin VARCHAR(20) NOT NULL DEFAULT 'local',
                is_deleted TINYINT(1) NOT NULL DEFAULT 0,
                created_utc DATETIME(6) NOT NULL,
                updated_utc DATETIME(6) NOT NULL,
                UNIQUE KEY uq_stock_counts_number (tenant_id, company_id, count_number),
                KEY ix_stock_counts_status (tenant_id, company_id, status),
                KEY ix_stock_counts_lookup (tenant_id, company_id, count_date),
                KEY ix_stock_counts_warehouse (tenant_id, company_id, warehouse)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS stock_count_lines (
                count_id CHAR(36) NOT NULL,
                line_number INT NOT NULL,
                item_code VARCHAR(120) NULL,
                item_description VARCHAR(255) NOT NULL,
                color VARCHAR(120) NULL,
                expected_quantity DECIMAL(18,3) NOT NULL DEFAULT 0,
                counted_quantity DECIMAL(18,3) NOT NULL DEFAULT 0,
                difference_quantity DECIMAL(18,3) NOT NULL DEFAULT 0,
                is_difference_validated TINYINT(1) NOT NULL DEFAULT 0,
                unit_of_measure VARCHAR(30) NULL,
                notes TEXT NULL,
                PRIMARY KEY (count_id, line_number),
                KEY ix_stock_count_lines_item (item_code),
                KEY ix_stock_count_lines_color (color)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS finish_work_orders (
                order_id CHAR(36) NOT NULL PRIMARY KEY,
                tenant_id CHAR(36) NOT NULL,
                company_id CHAR(36) NOT NULL,
                center_code CHAR(1) NOT NULL,
                order_number INT NOT NULL,
                work_date DATETIME(6) NOT NULL,
                status VARCHAR(30) NOT NULL,
                client_code INT NOT NULL DEFAULT 0,
                client_name VARCHAR(255) NULL,
                finisher_code INT NOT NULL DEFAULT 0,
                finisher_name VARCHAR(255) NULL,
                machine_code INT NOT NULL DEFAULT 0,
                machine_name VARCHAR(255) NULL,
                operation_code INT NOT NULL DEFAULT 0,
                operation_name VARCHAR(255) NULL,
                disposition_code INT NULL,
                disposition_label VARCHAR(120) NULL,
                source_sample_kind VARCHAR(30) NULL,
                source_sample_code VARCHAR(120) NULL,
                source_sample_line_number INT NULL,
                source_record_id CHAR(36) NULL,
                primary_fabric_code VARCHAR(120) NULL,
                primary_fabric_description VARCHAR(255) NULL,
                primary_color VARCHAR(120) NULL,
                total_kilograms DECIMAL(18,3) NOT NULL DEFAULT 0,
                total_pieces DECIMAL(18,3) NOT NULL DEFAULT 0,
                notes TEXT NULL,
                origin VARCHAR(20) NOT NULL DEFAULT 'local',
                is_deleted TINYINT(1) NOT NULL DEFAULT 0,
                synced_utc DATETIME(6) NULL,
                created_utc DATETIME(6) NOT NULL,
                updated_utc DATETIME(6) NOT NULL,
                UNIQUE KEY uq_finish_work_orders_number (tenant_id, company_id, order_number),
                KEY ix_finish_work_orders_status (tenant_id, company_id, status),
                KEY ix_finish_work_orders_lookup (tenant_id, company_id, center_code, work_date),
                KEY ix_finish_work_orders_machine (tenant_id, company_id, machine_code),
                KEY ix_finish_work_orders_operation (tenant_id, company_id, operation_code)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS finish_work_order_lines (
                order_id CHAR(36) NOT NULL,
                line_number INT NOT NULL,
                fabric_code VARCHAR(120) NULL,
                fabric_description VARCHAR(255) NULL,
                color VARCHAR(120) NULL,
                total_kilograms DECIMAL(18,3) NOT NULL DEFAULT 0,
                total_pieces DECIMAL(18,3) NOT NULL DEFAULT 0,
                status VARCHAR(30) NOT NULL,
                notes TEXT NULL,
                PRIMARY KEY (order_id, line_number),
                KEY ix_finish_work_order_lines_fabric (fabric_code),
                KEY ix_finish_work_order_lines_color (color)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS repres (
                CODI INT NOT NULL,
                TEL2 VARCHAR(80) NULL,
                CENTRO CHAR(1) NOT NULL,
                NOM VARCHAR(200) NOT NULL,
                DOM VARCHAR(255) NULL,
                POB VARCHAR(120) NULL,
                CP VARCHAR(20) NULL,
                PROV VARCHAR(120) NULL,
                TEL VARCHAR(80) NULL,
                FAX VARCHAR(80) NULL,
                NIF VARCHAR(50) NULL,
                OBSERV TEXT NULL,
                FORMAT VARCHAR(120) NULL,
                COMIS DECIMAL(10,2) NULL,
                PAIS VARCHAR(120) NULL,
                WEB VARCHAR(255) NULL,
                EMAIL1 VARCHAR(255) NULL,
                EMAIL2 VARCHAR(255) NULL,
                origin VARCHAR(20) NOT NULL DEFAULT 'legacy',
                is_deleted TINYINT(1) NOT NULL DEFAULT 0,
                synced_utc DATETIME(6) NULL,
                PRIMARY KEY (CENTRO, CODI),
                KEY ix_repres_name (CENTRO, NOM),
                KEY ix_repres_city (CENTRO, POB)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS trans (
                CODI INT NOT NULL,
                TEL2 VARCHAR(80) NULL,
                WEB VARCHAR(255) NULL,
                NOM VARCHAR(200) NOT NULL,
                DOM VARCHAR(255) NULL,
                POB VARCHAR(120) NULL,
                CP VARCHAR(20) NULL,
                PROV VARCHAR(120) NULL,
                TEL VARCHAR(80) NULL,
                FAX VARCHAR(80) NULL,
                NIF VARCHAR(50) NULL,
                OBSERV TEXT NULL,
                FORMAT VARCHAR(120) NULL,
                EMAIL VARCHAR(255) NULL,
                CENTRO CHAR(1) NOT NULL,
                origin VARCHAR(20) NOT NULL DEFAULT 'legacy',
                is_deleted TINYINT(1) NOT NULL DEFAULT 0,
                synced_utc DATETIME(6) NULL,
                PRIMARY KEY (CENTRO, CODI),
                KEY ix_trans_name (CENTRO, NOM),
                KEY ix_trans_city (CENTRO, POB)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS tallers (
                CODI INT NOT NULL,
                CENTRO CHAR(1) NOT NULL,
                NOM VARCHAR(200) NOT NULL,
                DOM VARCHAR(255) NULL,
                POB VARCHAR(120) NULL,
                CP VARCHAR(20) NULL,
                PROV VARCHAR(120) NULL,
                TEL VARCHAR(80) NULL,
                TEL2 VARCHAR(80) NULL,
                FAX VARCHAR(80) NULL,
                NIF VARCHAR(50) NULL,
                OBSERV TEXT NULL,
                FORMAT VARCHAR(120) NULL,
                WEB VARCHAR(255) NULL,
                EMAIL1 VARCHAR(255) NULL,
                EMAIL2 VARCHAR(255) NULL,
                PAIS VARCHAR(120) NULL,
                origin VARCHAR(20) NOT NULL DEFAULT 'legacy',
                is_deleted TINYINT(1) NOT NULL DEFAULT 0,
                synced_utc DATETIME(6) NULL,
                PRIMARY KEY (CENTRO, CODI),
                KEY ix_tallers_name (CENTRO, NOM),
                KEY ix_tallers_city (CENTRO, POB)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS forni (
                CODI VARCHAR(30) NOT NULL,
                DESCRI VARCHAR(255) NOT NULL,
                PREU DECIMAL(12,4) NOT NULL DEFAULT 0,
                PROVE INT NULL,
                REFPRO VARCHAR(120) NULL,
                CLIENT INT NULL,
                MODEL VARCHAR(120) NULL,
                SERIE VARCHAR(120) NULL,
                TEMPORADA VARCHAR(120) NULL,
                CENTRO CHAR(1) NOT NULL,
                origin VARCHAR(20) NOT NULL DEFAULT 'legacy',
                is_deleted TINYINT(1) NOT NULL DEFAULT 0,
                synced_utc DATETIME(6) NULL,
                PRIMARY KEY (CENTRO, CODI),
                KEY ix_forni_descri (CENTRO, DESCRI),
                KEY ix_forni_model (CENTRO, MODEL),
                KEY ix_forni_temporada (CENTRO, TEMPORADA)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS forni_detail (
                CENTRO CHAR(1) NOT NULL,
                FORNI_CODI VARCHAR(30) NOT NULL,
                LINE_NUMBER INT NOT NULL,
                PROVE INT NOT NULL DEFAULT 0,
                OBSERV VARCHAR(120) NULL,
                COLOR VARCHAR(120) NULL,
                MEDIDA VARCHAR(120) NULL,
                PREU DECIMAL(12,4) NOT NULL DEFAULT 0,
                ACTUAL DECIMAL(18,3) NOT NULL DEFAULT 0,
                MINIM DECIMAL(18,3) NOT NULL DEFAULT 0,
                PREUCOST DECIMAL(12,4) NOT NULL DEFAULT 0,
                PRIMARY KEY (CENTRO, FORNI_CODI, LINE_NUMBER),
                KEY ix_forni_detail_lookup (CENTRO, FORNI_CODI),
                KEY ix_forni_detail_provider_item (CENTRO, OBSERV),
                KEY ix_forni_detail_color_measure (CENTRO, COLOR, MEDIDA)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS dispos (
                CODI INT NOT NULL,
                CODICLIENT VARCHAR(50) NULL,
                CENTRO CHAR(1) NOT NULL,
                ANY VARCHAR(10) NOT NULL,
                IDDISPOS INT NOT NULL DEFAULT 0,
                FECHA DATETIME NULL,
                DRECEPCION DATETIME NULL,
                ACABADOR INT NOT NULL DEFAULT 0,
                ANULADA TINYINT(1) NOT NULL DEFAULT 0,
                CLIENT INT NOT NULL DEFAULT 0,
                OBSERV TEXT NULL,
                COLORCLIENTE VARCHAR(120) NULL,
                TOTALPIEZAS DECIMAL(18,3) NOT NULL DEFAULT 0,
                TOTALKG DECIMAL(18,3) NOT NULL DEFAULT 0,
                COLOR VARCHAR(120) NULL,
                RECIBIDO TINYINT(1) NOT NULL DEFAULT 0,
                COMANDA VARCHAR(120) NULL,
                origin VARCHAR(20) NOT NULL DEFAULT 'legacy',
                is_deleted TINYINT(1) NOT NULL DEFAULT 0,
                synced_utc DATETIME(6) NULL,
                PRIMARY KEY (CENTRO, CODI),
                KEY ix_dispos_display (CENTRO, ANY, IDDISPOS),
                KEY ix_dispos_client (CENTRO, CLIENT),
                KEY ix_dispos_finisher (CENTRO, ACABADOR),
                KEY ix_dispos_date (CENTRO, FECHA)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS ddispos (
                CENTRO CHAR(1) NOT NULL,
                DESCRIPCIO VARCHAR(255) NULL,
                LINEA INT NOT NULL,
                DISPOS INT NOT NULL,
                TEJEDOR INT NOT NULL DEFAULT 0,
                NALBARAN VARCHAR(80) NULL,
                TEJIDO VARCHAR(50) NULL,
                COMPOS VARCHAR(255) NULL,
                NPIEZAS VARCHAR(255) NULL,
                TOTALPIEZAS DECIMAL(18,3) NOT NULL DEFAULT 0,
                TOTALKG DECIMAL(18,3) NOT NULL DEFAULT 0,
                ACABADO VARCHAR(255) NULL,
                ANCHO VARCHAR(120) NULL,
                GRAMAJE DECIMAL(12,3) NOT NULL DEFAULT 0,
                RENDIMIENTO DECIMAL(12,4) NOT NULL DEFAULT 0,
                SERVIDO TINYINT(1) NOT NULL DEFAULT 0,
                DISPUESTO TINYINT(1) NOT NULL DEFAULT 0,
                PRIMARY KEY (CENTRO, DISPOS, LINEA),
                KEY ix_ddispos_lookup (CENTRO, DISPOS),
                KEY ix_ddispos_weaver (CENTRO, TEJEDOR),
                KEY ix_ddispos_fabric (CENTRO, TEJIDO)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS fil (
                CODI VARCHAR(30) NOT NULL,
                DESCRI VARCHAR(255) NOT NULL,
                PROVE INT NULL,
                COST DECIMAL(12,4) NOT NULL DEFAULT 0,
                PREU DECIMAL(12,4) NOT NULL DEFAULT 0,
                IVA VARCHAR(20) NULL,
                OBSERV TEXT NULL,
                CENTRO CHAR(1) NOT NULL,
                origin VARCHAR(20) NOT NULL DEFAULT 'legacy',
                is_deleted TINYINT(1) NOT NULL DEFAULT 0,
                synced_utc DATETIME(6) NULL,
                PRIMARY KEY (CENTRO, CODI),
                KEY ix_fil_descri (CENTRO, DESCRI),
                KEY ix_fil_prove (CENTRO, PROVE),
                KEY ix_fil_iva (CENTRO, IVA)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS fil_detail (
                CENTRO CHAR(1) NOT NULL,
                FIL_CODI VARCHAR(30) NOT NULL,
                LINE_NUMBER INT NOT NULL,
                PROVE INT NOT NULL DEFAULT 0,
                COLOR VARCHAR(120) NULL,
                ACTUAL DECIMAL(18,3) NOT NULL DEFAULT 0,
                MINIM DECIMAL(18,3) NOT NULL DEFAULT 0,
                PREU DECIMAL(12,4) NOT NULL DEFAULT 0,
                PREUCOST DECIMAL(12,4) NOT NULL DEFAULT 0,
                TINTAR DECIMAL(12,4) NOT NULL DEFAULT 0,
                METRES DECIMAL(18,3) NOT NULL DEFAULT 0,
                KG DECIMAL(18,3) NOT NULL DEFAULT 0,
                OBSERV VARCHAR(255) NULL,
                PRIMARY KEY (CENTRO, FIL_CODI, LINE_NUMBER),
                KEY ix_fil_detail_lookup (CENTRO, FIL_CODI),
                KEY ix_fil_detail_color (CENTRO, COLOR),
                KEY ix_fil_detail_provider (CENTRO, PROVE)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS mostres (
                CODI VARCHAR(40) NOT NULL,
                CENTRO CHAR(1) NOT NULL,
                DESCRI VARCHAR(255) NOT NULL,
                CLIENT INT NOT NULL DEFAULT 0,
                NOMCLIENT VARCHAR(255) NULL,
                REFE VARCHAR(120) NULL,
                TEMP VARCHAR(80) NULL,
                MAQUINA INT NOT NULL DEFAULT 0,
                NOMMAQUI VARCHAR(255) NULL,
                MARGE DECIMAL(12,4) NOT NULL DEFAULT 0,
                IVA VARCHAR(20) NULL,
                OBSERV MEDIUMTEXT NULL,
                COMPO VARCHAR(255) NULL,
                PREU DECIMAL(12,4) NOT NULL DEFAULT 0,
                origin VARCHAR(20) NOT NULL DEFAULT 'legacy',
                is_deleted TINYINT(1) NOT NULL DEFAULT 0,
                synced_utc DATETIME(6) NULL,
                PRIMARY KEY (CENTRO, CODI),
                KEY ix_mostres_descri (CENTRO, DESCRI),
                KEY ix_mostres_client (CENTRO, CLIENT),
                KEY ix_mostres_machine (CENTRO, MAQUINA),
                KEY ix_mostres_refe (CENTRO, REFE)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS mostres_detail (
                CENTRO CHAR(1) NOT NULL,
                MOSTRA_CODI VARCHAR(40) NOT NULL,
                LINE_NUMBER INT NOT NULL,
                TALLA VARCHAR(80) NULL,
                TALLAH VARCHAR(80) NULL,
                TALLAL VARCHAR(80) NULL,
                DESCRI VARCHAR(255) NULL,
                COST DECIMAL(12,4) NOT NULL DEFAULT 0,
                VENDA DECIMAL(12,4) NOT NULL DEFAULT 0,
                COLOR VARCHAR(120) NULL,
                CLIENT INT NOT NULL DEFAULT 0,
                NOMCLIENT VARCHAR(255) NULL,
                NCCODE VARCHAR(120) NULL,
                PRIMARY KEY (CENTRO, MOSTRA_CODI, LINE_NUMBER),
                KEY ix_mostres_detail_lookup (CENTRO, MOSTRA_CODI),
                KEY ix_mostres_detail_color (CENTRO, COLOR),
                KEY ix_mostres_detail_client (CENTRO, CLIENT)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS mostres_breakdown (
                CENTRO CHAR(1) NOT NULL,
                MOSTRA_CODI VARCHAR(40) NOT NULL,
                SAMPLE_LINE_NUMBER INT NOT NULL,
                DATA DATE NULL,
                CLIENT INT NOT NULL DEFAULT 0,
                NOMCLIENT VARCHAR(255) NULL,
                MAQUINA INT NOT NULL DEFAULT 0,
                NOMMAQUI VARCHAR(255) NULL,
                OPERACIO INT NOT NULL DEFAULT 0,
                NOMOPER VARCHAR(255) NULL,
                AGULLES DECIMAL(12,4) NOT NULL DEFAULT 0,
                VELOSITAT DECIMAL(12,4) NOT NULL DEFAULT 0,
                DISCO VARCHAR(120) NULL,
                TEMPS DECIMAL(12,4) NOT NULL DEFAULT 0,
                MACHINE_RATE DECIMAL(12,4) NOT NULL DEFAULT 0,
                MACHINE_IMPORT DECIMAL(12,4) NOT NULL DEFAULT 0,
                CORTES VARCHAR(120) NULL,
                NOTES MEDIUMTEXT NULL,
                origin VARCHAR(20) NOT NULL DEFAULT 'legacy',
                is_deleted TINYINT(1) NOT NULL DEFAULT 0,
                synced_utc DATETIME(6) NULL,
                PRIMARY KEY (CENTRO, MOSTRA_CODI, SAMPLE_LINE_NUMBER),
                KEY ix_mostres_breakdown_machine (CENTRO, MAQUINA),
                KEY ix_mostres_breakdown_client (CENTRO, CLIENT),
                KEY ix_mostres_breakdown_operation (CENTRO, OPERACIO)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS mostres_breakdown_lines (
                CENTRO CHAR(1) NOT NULL,
                MOSTRA_CODI VARCHAR(40) NOT NULL,
                SAMPLE_LINE_NUMBER INT NOT NULL,
                LINE_NUMBER INT NOT NULL,
                TEIXIT VARCHAR(80) NULL,
                PROVE INT NOT NULL DEFAULT 0,
                NOMPROVE VARCHAR(255) NULL,
                COLOR VARCHAR(120) NULL,
                FIL DECIMAL(12,4) NOT NULL DEFAULT 0,
                CAPS DECIMAL(12,4) NOT NULL DEFAULT 0,
                PASSADES DECIMAL(12,4) NOT NULL DEFAULT 0,
                GRADUACION INT NOT NULL DEFAULT 0,
                CONSUM DECIMAL(12,4) NOT NULL DEFAULT 0,
                PREU DECIMAL(12,4) NOT NULL DEFAULT 0,
                IMPORT DECIMAL(12,4) NOT NULL DEFAULT 0,
                PRIMARY KEY (CENTRO, MOSTRA_CODI, SAMPLE_LINE_NUMBER, LINE_NUMBER),
                KEY ix_mostres_breakdown_lines_teixit (CENTRO, TEIXIT),
                KEY ix_mostres_breakdown_lines_prove (CENTRO, PROVE)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS article_models (
                record_id CHAR(36) NOT NULL PRIMARY KEY,
                tenant_id CHAR(36) NOT NULL,
                company_id CHAR(36) NOT NULL,
                CENTRO CHAR(1) NOT NULL,
                CODI VARCHAR(40) NOT NULL,
                SERIE VARCHAR(20) NOT NULL,
                CLIENT INT NOT NULL DEFAULT 0,
                NOMCLIENT VARCHAR(255) NULL,
                TEMPORADA VARCHAR(80) NOT NULL,
                DESCRI VARCHAR(255) NOT NULL,
                CODIMODEL VARCHAR(120) NULL,
                TEIXIT VARCHAR(40) NULL,
                DESCRITEIXIT VARCHAR(255) NULL,
                PROVE INT NOT NULL DEFAULT 0,
                NOMPROVE VARCHAR(255) NULL,
                AMPLE VARCHAR(120) NULL,
                TINT INT NOT NULL DEFAULT 0,
                NOMTINT VARCHAR(255) NULL,
                ACA INT NOT NULL DEFAULT 0,
                NOMACA VARCHAR(255) NULL,
                ESTAM INT NOT NULL DEFAULT 0,
                NOMESTAM VARCHAR(255) NULL,
                CONFEC INT NOT NULL DEFAULT 0,
                NOMCONFEC VARCHAR(255) NULL,
                RENDIM DECIMAL(12,4) NOT NULL DEFAULT 0,
                FORNITURA VARCHAR(255) NULL,
                CESTAM VARCHAR(120) NULL,
                CESTAM2 VARCHAR(120) NULL,
                NESTAM DECIMAL(12,4) NOT NULL DEFAULT 0,
                NESTAM2 DECIMAL(12,4) NOT NULL DEFAULT 0,
                NCONFEC DECIMAL(12,4) NOT NULL DEFAULT 0,
                NPLANXA DECIMAL(12,4) NOT NULL DEFAULT 0,
                NREPAS DECIMAL(12,4) NOT NULL DEFAULT 0,
                QTRANS DECIMAL(12,4) NOT NULL DEFAULT 0,
                NTRANS DECIMAL(12,4) NOT NULL DEFAULT 0,
                QFLOCAT DECIMAL(12,4) NOT NULL DEFAULT 0,
                NFLOCAT DECIMAL(12,4) NOT NULL DEFAULT 0,
                QBRODAT DECIMAL(12,4) NOT NULL DEFAULT 0,
                NBRODAT DECIMAL(12,4) NOT NULL DEFAULT 0,
                NESTAMP DECIMAL(12,4) NOT NULL DEFAULT 0,
                NTINTP DECIMAL(12,4) NOT NULL DEFAULT 0,
                NACAP DECIMAL(12,4) NOT NULL DEFAULT 0,
                NFORNITURA DECIMAL(12,4) NOT NULL DEFAULT 0,
                MANIPULACION DECIMAL(12,4) NOT NULL DEFAULT 0,
                COST DECIMAL(12,4) NOT NULL DEFAULT 0,
                MARGE DECIMAL(12,4) NOT NULL DEFAULT 0,
                VENDA DECIMAL(12,4) NOT NULL DEFAULT 0,
                VENDAFINAL DECIMAL(12,4) NOT NULL DEFAULT 0,
                OBSERV MEDIUMTEXT NULL,
                IVA VARCHAR(20) NULL,
                TALLA01 VARCHAR(40) NULL,
                TALLA02 VARCHAR(40) NULL,
                TALLA03 VARCHAR(40) NULL,
                TALLA04 VARCHAR(40) NULL,
                TALLA05 VARCHAR(40) NULL,
                TALLA06 VARCHAR(40) NULL,
                TALLA07 VARCHAR(40) NULL,
                TALLA08 VARCHAR(40) NULL,
                TALLA09 VARCHAR(40) NULL,
                TALLA10 VARCHAR(40) NULL,
                NPACK DECIMAL(12,4) NOT NULL DEFAULT 0,
                origin VARCHAR(20) NOT NULL DEFAULT 'local',
                is_deleted TINYINT(1) NOT NULL DEFAULT 0,
                synced_utc DATETIME(6) NULL,
                created_utc DATETIME(6) NOT NULL,
                updated_utc DATETIME(6) NOT NULL,
                UNIQUE KEY uq_article_models_identity (tenant_id, company_id, CENTRO, CODI, SERIE, CLIENT, TEMPORADA),
                KEY ix_article_models_lookup (tenant_id, company_id, CENTRO, TEMPORADA, SERIE, CLIENT, CODI),
                KEY ix_article_models_description (tenant_id, company_id, DESCRI),
                KEY ix_article_models_fabric (tenant_id, company_id, TEIXIT)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS article_model_scandallo (
                model_id CHAR(36) NOT NULL,
                line_number INT NOT NULL,
                TITULO VARCHAR(120) NULL,
                TEIXIT VARCHAR(40) NULL,
                CONSUM DECIMAL(12,4) NOT NULL DEFAULT 0,
                PREU DECIMAL(12,4) NOT NULL DEFAULT 0,
                COST DECIMAL(12,4) NOT NULL DEFAULT 0,
                PRIMARY KEY (model_id, line_number),
                KEY ix_article_model_scandallo_fabric (TEIXIT)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS article_model_colors (
                model_id CHAR(36) NOT NULL,
                line_number INT NOT NULL,
                MODCOL VARCHAR(40) NULL,
                TITULO VARCHAR(120) NULL,
                COLTITULO VARCHAR(120) NULL,
                PRIMARY KEY (model_id, line_number),
                KEY ix_article_model_colors_lookup (model_id, MODCOL),
                KEY ix_article_model_colors_title (TITULO)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS article_model_fornituras (
                model_id CHAR(36) NOT NULL,
                line_number INT NOT NULL,
                FORNI VARCHAR(40) NULL,
                MEDIDA VARCHAR(120) NULL,
                UNITATS DECIMAL(12,4) NOT NULL DEFAULT 0,
                PREU DECIMAL(12,4) NOT NULL DEFAULT 0,
                IMPORT DECIMAL(12,4) NOT NULL DEFAULT 0,
                PRIMARY KEY (model_id, line_number),
                KEY ix_article_model_fornituras_lookup (model_id, FORNI)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS article_model_stock (
                model_id CHAR(36) NOT NULL,
                line_number INT NOT NULL,
                COLOR VARCHAR(120) NULL,
                TALLA VARCHAR(40) NULL,
                TALLA01 DECIMAL(12,4) NOT NULL DEFAULT 0,
                TALLA02 DECIMAL(12,4) NOT NULL DEFAULT 0,
                TALLA03 DECIMAL(12,4) NOT NULL DEFAULT 0,
                TALLA04 DECIMAL(12,4) NOT NULL DEFAULT 0,
                TALLA05 DECIMAL(12,4) NOT NULL DEFAULT 0,
                TALLA06 DECIMAL(12,4) NOT NULL DEFAULT 0,
                TALLA07 DECIMAL(12,4) NOT NULL DEFAULT 0,
                TALLA08 DECIMAL(12,4) NOT NULL DEFAULT 0,
                TALLA09 DECIMAL(12,4) NOT NULL DEFAULT 0,
                TALLA10 DECIMAL(12,4) NOT NULL DEFAULT 0,
                PRIMARY KEY (model_id, line_number),
                KEY ix_article_model_stock_lookup (model_id, COLOR),
                KEY ix_article_model_stock_size (TALLA)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS teixits (
                CODI VARCHAR(10) NOT NULL,
                CENTRO CHAR(1) NOT NULL,
                DESCRI VARCHAR(255) NOT NULL,
                NRO VARCHAR(120) NULL,
                MAQUI INT NULL,
                MATERIA DECIMAL(12,4) NOT NULL DEFAULT 0,
                OBSERV MEDIUMTEXT NULL,
                IVA VARCHAR(20) NULL,
                TEIXIDOR INT NULL,
                PTEIXIR DECIMAL(12,4) NOT NULL DEFAULT 0,
                ESTAMPADOR INT NULL,
                PESTAM DECIMAL(12,4) NOT NULL DEFAULT 0,
                ACABADOR INT NULL,
                ACABAT VARCHAR(255) NULL,
                PACA DECIMAL(12,4) NOT NULL DEFAULT 0,
                CRU DECIMAL(12,4) NOT NULL DEFAULT 0,
                AMPLE VARCHAR(40) NULL,
                RENDIMENT DECIMAL(12,4) NOT NULL DEFAULT 0,
                MARGE DECIMAL(12,4) NOT NULL DEFAULT 0,
                GRAMA DECIMAL(12,4) NOT NULL DEFAULT 0,
                PREUM DECIMAL(12,4) NOT NULL DEFAULT 0,
                PREUK DECIMAL(12,4) NOT NULL DEFAULT 0,
                STCRUM DECIMAL(18,3) NOT NULL DEFAULT 0,
                STDISPM DECIMAL(18,3) NOT NULL DEFAULT 0,
                STCRUK DECIMAL(18,3) NOT NULL DEFAULT 0,
                STDISPK DECIMAL(18,3) NOT NULL DEFAULT 0,
                PREUPERMODEL DECIMAL(12,4) NOT NULL DEFAULT 0,
                TUBULAR TINYINT(1) NOT NULL DEFAULT 0,
                AMPLE2 DECIMAL(12,4) NOT NULL DEFAULT 0,
                origin VARCHAR(20) NOT NULL DEFAULT 'legacy',
                is_deleted TINYINT(1) NOT NULL DEFAULT 0,
                synced_utc DATETIME(6) NULL,
                PRIMARY KEY (CENTRO, CODI),
                KEY ix_teixits_descri (CENTRO, DESCRI),
                KEY ix_teixits_weaver (CENTRO, TEIXIDOR),
                KEY ix_teixits_finisher (CENTRO, ACABADOR)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS teixits_color_detail (
                CENTRO CHAR(1) NOT NULL,
                TEIXIT_CODI VARCHAR(10) NOT NULL,
                LINE_NUMBER INT NOT NULL,
                PROVE INT NOT NULL DEFAULT 0,
                COLOR VARCHAR(120) NULL,
                ACTUAL DECIMAL(18,3) NOT NULL DEFAULT 0,
                MINIM DECIMAL(18,3) NOT NULL DEFAULT 0,
                TINTAR DECIMAL(12,4) NOT NULL DEFAULT 0,
                PREU DECIMAL(12,4) NOT NULL DEFAULT 0,
                METRES DECIMAL(18,3) NOT NULL DEFAULT 0,
                KG DECIMAL(18,3) NOT NULL DEFAULT 0,
                OBSERV VARCHAR(255) NULL,
                PRIMARY KEY (CENTRO, TEIXIT_CODI, LINE_NUMBER),
                KEY ix_teixits_color_lookup (CENTRO, TEIXIT_CODI),
                KEY ix_teixits_color_color (CENTRO, COLOR)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS teixits_composition_detail (
                CENTRO CHAR(1) NOT NULL,
                TEIXIT_CODI VARCHAR(10) NOT NULL,
                LINE_NUMBER INT NOT NULL,
                COMP VARCHAR(30) NULL,
                PER INT NOT NULL DEFAULT 0,
                PROVE INT NOT NULL DEFAULT 0,
                PREU DECIMAL(12,4) NOT NULL DEFAULT 0,
                IMPORTE DECIMAL(12,4) NOT NULL DEFAULT 0,
                PRIMARY KEY (CENTRO, TEIXIT_CODI, LINE_NUMBER),
                KEY ix_teixits_comp_lookup (CENTRO, TEIXIT_CODI),
                KEY ix_teixits_comp_component (CENTRO, COMP)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS teixits_finish_detail (
                CENTRO CHAR(1) NOT NULL,
                TEIXIT_CODI VARCHAR(10) NOT NULL,
                LINE_NUMBER INT NOT NULL,
                ACABAT VARCHAR(50) NULL,
                PROVE INT NOT NULL DEFAULT 0,
                ORDEN INT NOT NULL DEFAULT 0,
                PREUM DECIMAL(12,4) NOT NULL DEFAULT 0,
                PREUK DECIMAL(12,4) NOT NULL DEFAULT 0,
                PRIMARY KEY (CENTRO, TEIXIT_CODI, LINE_NUMBER),
                KEY ix_teixits_finish_lookup (CENTRO, TEIXIT_CODI),
                KEY ix_teixits_finish_code (CENTRO, ACABAT)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS stock_items (
                tenant_id CHAR(36) NOT NULL,
                company_id CHAR(36) NOT NULL,
                item_key VARCHAR(160) NOT NULL,
                item_type VARCHAR(40) NOT NULL,
                item_code VARCHAR(120) NOT NULL,
                description VARCHAR(255) NOT NULL,
                unit_of_measure VARCHAR(30) NULL,
                origin VARCHAR(20) NOT NULL DEFAULT 'saas',
                is_deleted TINYINT(1) NOT NULL DEFAULT 0,
                legacy_source_system VARCHAR(40) NULL,
                legacy_center_code CHAR(1) NULL,
                legacy_document_type VARCHAR(40) NULL,
                legacy_document_number VARCHAR(80) NULL,
                synced_utc DATETIME(6) NULL,
                created_utc DATETIME(6) NOT NULL,
                updated_utc DATETIME(6) NOT NULL,
                PRIMARY KEY (tenant_id, company_id, item_key),
                KEY ix_stock_items_code (tenant_id, company_id, item_code),
                KEY ix_stock_items_type (tenant_id, company_id, item_type)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS legacy_stock_balances (
                tenant_id CHAR(36) NOT NULL,
                company_id CHAR(36) NOT NULL,
                warehouse VARCHAR(120) NOT NULL,
                item_key VARCHAR(160) NOT NULL,
                item_code VARCHAR(120) NOT NULL,
                item_description VARCHAR(255) NOT NULL,
                unit_of_measure VARCHAR(30) NULL,
                current_stock DECIMAL(18,3) NOT NULL,
                movement_count INT NOT NULL DEFAULT 0,
                last_movement_date DATETIME(6) NULL,
                legacy_source_system VARCHAR(40) NOT NULL,
                legacy_center_code CHAR(1) NOT NULL,
                legacy_document_type VARCHAR(40) NOT NULL,
                legacy_document_number VARCHAR(80) NOT NULL,
                synced_utc DATETIME(6) NOT NULL,
                PRIMARY KEY (tenant_id, company_id, warehouse, item_key),
                KEY ix_legacy_stock_balances_code (tenant_id, company_id, item_code),
                KEY ix_legacy_stock_balances_desc (tenant_id, company_id, item_description),
                KEY ix_legacy_stock_balances_warehouse (tenant_id, company_id, warehouse)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS sales_orders (
                tenant_id CHAR(36) NOT NULL,
                company_id CHAR(36) NOT NULL,
                order_number INT NOT NULL,
                client_code INT NOT NULL,
                client_name VARCHAR(200) NOT NULL,
                client_tax_id VARCHAR(50) NULL,
                document_date DATETIME(6) NOT NULL,
                requested_date DATETIME(6) NULL,
                status VARCHAR(40) NOT NULL,
                origin VARCHAR(20) NOT NULL DEFAULT 'saas',
                is_deleted TINYINT(1) NOT NULL DEFAULT 0,
                legacy_source_system VARCHAR(40) NULL,
                legacy_center_code CHAR(1) NULL,
                legacy_document_type VARCHAR(40) NULL,
                legacy_document_number VARCHAR(80) NULL,
                synced_utc DATETIME(6) NULL,
                notes TEXT NULL,
                created_utc DATETIME(6) NOT NULL,
                updated_utc DATETIME(6) NOT NULL,
                PRIMARY KEY (tenant_id, company_id, order_number),
                KEY ix_sales_orders_client (tenant_id, company_id, client_code),
                KEY ix_sales_orders_status (tenant_id, company_id, status)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS sales_order_lines (
                tenant_id CHAR(36) NOT NULL,
                company_id CHAR(36) NOT NULL,
                order_number INT NOT NULL,
                line_number INT NOT NULL,
                item_code VARCHAR(120) NULL,
                description VARCHAR(255) NOT NULL,
                quantity DECIMAL(18,3) NOT NULL,
                shipped_quantity DECIMAL(18,3) NOT NULL DEFAULT 0,
                unit_of_measure VARCHAR(30) NULL,
                unit_price DECIMAL(18,4) NOT NULL,
                requested_date DATETIME(6) NULL,
                legacy_source_system VARCHAR(40) NULL,
                legacy_center_code CHAR(1) NULL,
                legacy_document_type VARCHAR(40) NULL,
                legacy_document_number VARCHAR(80) NULL,
                legacy_line_number INT NULL,
                synced_utc DATETIME(6) NULL,
                last_shipped_utc DATETIME(6) NULL,
                notes TEXT NULL,
                PRIMARY KEY (tenant_id, company_id, order_number, line_number),
                KEY ix_sales_order_lines_lookup (tenant_id, company_id, order_number)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS sales_order_shipments (
                shipment_id CHAR(36) NOT NULL PRIMARY KEY,
                shipment_series VARCHAR(20) NULL,
                shipment_number INT NULL,
                tenant_id CHAR(36) NOT NULL,
                company_id CHAR(36) NOT NULL,
                order_number INT NOT NULL,
                shipment_date DATETIME(6) NOT NULL,
                warehouse VARCHAR(120) NULL,
                origin VARCHAR(20) NOT NULL DEFAULT 'saas',
                is_deleted TINYINT(1) NOT NULL DEFAULT 0,
                legacy_source_system VARCHAR(40) NULL,
                legacy_center_code CHAR(1) NULL,
                legacy_document_type VARCHAR(40) NULL,
                legacy_document_number VARCHAR(80) NULL,
                synced_utc DATETIME(6) NULL,
                invoice_status VARCHAR(30) NOT NULL DEFAULT 'Pending',
                invoice_reference VARCHAR(80) NULL,
                invoice_draft_id CHAR(36) NULL,
                invoice_id CHAR(36) NULL,
                invoice_ready_utc DATETIME(6) NULL,
                notes TEXT NULL,
                created_utc DATETIME(6) NOT NULL,
                KEY ix_sales_order_shipments_number (tenant_id, company_id, shipment_number),
                KEY ix_sales_order_shipments_lookup (tenant_id, company_id, order_number, shipment_date)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS sales_order_shipment_lines (
                shipment_id CHAR(36) NOT NULL,
                tenant_id CHAR(36) NOT NULL,
                company_id CHAR(36) NOT NULL,
                order_number INT NOT NULL,
                line_number INT NOT NULL,
                description VARCHAR(255) NOT NULL,
                shipped_quantity DECIMAL(18,3) NOT NULL,
                PRIMARY KEY (shipment_id, line_number),
                KEY ix_sales_order_shipment_lines_lookup (tenant_id, company_id, order_number, line_number)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS sales_invoice_drafts (
                draft_id CHAR(36) NOT NULL PRIMARY KEY,
                draft_series VARCHAR(20) NULL,
                draft_number INT NOT NULL,
                tenant_id CHAR(36) NOT NULL,
                company_id CHAR(36) NOT NULL,
                client_code INT NOT NULL,
                client_name VARCHAR(200) NOT NULL,
                client_tax_id VARCHAR(50) NULL,
                issue_date DATETIME(6) NOT NULL,
                due_date DATETIME(6) NULL,
                status VARCHAR(30) NOT NULL,
                invoice_id CHAR(36) NULL,
                issued_utc DATETIME(6) NULL,
                shipment_count INT NOT NULL,
                total_quantity DECIMAL(18,3) NOT NULL,
                total_amount DECIMAL(18,2) NOT NULL,
                notes TEXT NULL,
                created_utc DATETIME(6) NOT NULL,
                updated_utc DATETIME(6) NOT NULL,
                UNIQUE KEY uq_sales_invoice_drafts_number (tenant_id, company_id, draft_number),
                KEY ix_sales_invoice_drafts_client (tenant_id, company_id, client_code),
                KEY ix_sales_invoice_drafts_issue_date (tenant_id, company_id, issue_date)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS sales_invoice_draft_shipments (
                draft_id CHAR(36) NOT NULL,
                tenant_id CHAR(36) NOT NULL,
                company_id CHAR(36) NOT NULL,
                shipment_id CHAR(36) NOT NULL,
                shipment_series VARCHAR(20) NULL,
                shipment_number INT NOT NULL,
                order_number INT NOT NULL,
                shipment_date DATETIME(6) NOT NULL,
                warehouse VARCHAR(120) NULL,
                shipped_quantity DECIMAL(18,3) NOT NULL,
                estimated_amount DECIMAL(18,2) NOT NULL,
                PRIMARY KEY (draft_id, shipment_id),
                UNIQUE KEY uq_sales_invoice_draft_shipments_shipment (shipment_id),
                KEY ix_sales_invoice_draft_shipments_lookup (tenant_id, company_id, draft_id, shipment_number)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS sales_invoice_draft_lines (
                draft_id CHAR(36) NOT NULL,
                tenant_id CHAR(36) NOT NULL,
                company_id CHAR(36) NOT NULL,
                line_number INT NOT NULL,
                item_code VARCHAR(120) NULL,
                description VARCHAR(255) NOT NULL,
                quantity DECIMAL(18,3) NOT NULL,
                unit_of_measure VARCHAR(30) NULL,
                unit_price DECIMAL(18,4) NOT NULL,
                line_total DECIMAL(18,2) NOT NULL,
                source_summary VARCHAR(255) NULL,
                PRIMARY KEY (draft_id, line_number),
                KEY ix_sales_invoice_draft_lines_lookup (tenant_id, company_id, draft_id)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS sales_invoices (
                invoice_id CHAR(36) NOT NULL PRIMARY KEY,
                invoice_series VARCHAR(20) NULL,
                invoice_number INT NOT NULL,
                draft_id CHAR(36) NOT NULL,
                draft_series VARCHAR(20) NULL,
                draft_number INT NOT NULL,
                tenant_id CHAR(36) NOT NULL,
                company_id CHAR(36) NOT NULL,
                client_code INT NOT NULL,
                client_name VARCHAR(200) NOT NULL,
                client_tax_id VARCHAR(50) NULL,
                issue_date DATETIME(6) NOT NULL,
                due_date DATETIME(6) NULL,
                status VARCHAR(30) NOT NULL,
                origin VARCHAR(20) NOT NULL DEFAULT 'saas',
                is_deleted TINYINT(1) NOT NULL DEFAULT 0,
                legacy_source_system VARCHAR(40) NULL,
                legacy_center_code CHAR(1) NULL,
                legacy_document_type VARCHAR(40) NULL,
                legacy_document_number VARCHAR(80) NULL,
                synced_utc DATETIME(6) NULL,
                shipment_count INT NOT NULL,
                total_quantity DECIMAL(18,3) NOT NULL,
                subtotal_amount DECIMAL(18,2) NOT NULL,
                tax_amount DECIMAL(18,2) NOT NULL,
                total_amount DECIMAL(18,2) NOT NULL,
                payment_status VARCHAR(30) NOT NULL DEFAULT 'Pending',
                amount_paid DECIMAL(18,2) NOT NULL DEFAULT 0,
                outstanding_amount DECIMAL(18,2) NOT NULL DEFAULT 0,
                last_payment_utc DATETIME(6) NULL,
                accounting_status VARCHAR(30) NOT NULL DEFAULT 'Pending',
                accounting_reference VARCHAR(80) NULL,
                accounting_ready_utc DATETIME(6) NULL,
                notes TEXT NULL,
                issued_utc DATETIME(6) NOT NULL,
                created_utc DATETIME(6) NOT NULL,
                updated_utc DATETIME(6) NOT NULL,
                UNIQUE KEY uq_sales_invoices_number (tenant_id, company_id, invoice_number),
                UNIQUE KEY uq_sales_invoices_draft (draft_id),
                KEY ix_sales_invoices_client (tenant_id, company_id, client_code),
                KEY ix_sales_invoices_issue_date (tenant_id, company_id, issue_date)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS sales_invoice_shipments (
                invoice_id CHAR(36) NOT NULL,
                tenant_id CHAR(36) NOT NULL,
                company_id CHAR(36) NOT NULL,
                shipment_id CHAR(36) NOT NULL,
                shipment_series VARCHAR(20) NULL,
                shipment_number INT NOT NULL,
                order_number INT NOT NULL,
                shipment_date DATETIME(6) NOT NULL,
                warehouse VARCHAR(120) NULL,
                shipped_quantity DECIMAL(18,3) NOT NULL,
                estimated_amount DECIMAL(18,2) NOT NULL,
                PRIMARY KEY (invoice_id, shipment_id),
                UNIQUE KEY uq_sales_invoice_shipments_shipment (shipment_id),
                KEY ix_sales_invoice_shipments_lookup (tenant_id, company_id, invoice_id, shipment_number)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS sales_invoice_lines (
                invoice_id CHAR(36) NOT NULL,
                tenant_id CHAR(36) NOT NULL,
                company_id CHAR(36) NOT NULL,
                line_number INT NOT NULL,
                item_code VARCHAR(120) NULL,
                description VARCHAR(255) NOT NULL,
                quantity DECIMAL(18,3) NOT NULL,
                unit_of_measure VARCHAR(30) NULL,
                unit_price DECIMAL(18,4) NOT NULL,
                line_subtotal DECIMAL(18,2) NOT NULL,
                tax_rate DECIMAL(9,4) NOT NULL DEFAULT 0,
                tax_amount DECIMAL(18,2) NOT NULL DEFAULT 0,
                line_total DECIMAL(18,2) NOT NULL,
                source_summary VARCHAR(255) NULL,
                PRIMARY KEY (invoice_id, line_number),
                KEY ix_sales_invoice_lines_lookup (tenant_id, company_id, invoice_id)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS sales_invoice_payments (
                payment_id CHAR(36) NOT NULL PRIMARY KEY,
                invoice_id CHAR(36) NOT NULL,
                tenant_id CHAR(36) NOT NULL,
                company_id CHAR(36) NOT NULL,
                payment_number INT NOT NULL,
                payment_date DATETIME(6) NOT NULL,
                amount DECIMAL(18,2) NOT NULL,
                method VARCHAR(80) NULL,
                reference VARCHAR(120) NULL,
                notes TEXT NULL,
                created_utc DATETIME(6) NOT NULL,
                UNIQUE KEY uq_sales_invoice_payments_number (invoice_id, payment_number),
                KEY ix_sales_invoice_payments_lookup (tenant_id, company_id, invoice_id, payment_date)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS sales_remittances (
                remittance_id CHAR(36) NOT NULL PRIMARY KEY,
                remittance_series VARCHAR(20) NULL,
                remittance_number INT NOT NULL,
                tenant_id CHAR(36) NOT NULL,
                company_id CHAR(36) NOT NULL,
                remittance_date DATETIME(6) NOT NULL,
                due_date DATETIME(6) NULL,
                status VARCHAR(30) NOT NULL DEFAULT 'Draft',
                bank_name VARCHAR(160) NULL,
                invoice_count INT NOT NULL DEFAULT 0,
                client_count INT NOT NULL DEFAULT 0,
                total_amount DECIMAL(18,2) NOT NULL DEFAULT 0,
                collected_amount DECIMAL(18,2) NOT NULL DEFAULT 0,
                outstanding_amount DECIMAL(18,2) NOT NULL DEFAULT 0,
                notes TEXT NULL,
                sent_utc DATETIME(6) NULL,
                collected_utc DATETIME(6) NULL,
                origin VARCHAR(20) NOT NULL DEFAULT 'saas',
                is_deleted TINYINT(1) NOT NULL DEFAULT 0,
                created_utc DATETIME(6) NOT NULL,
                updated_utc DATETIME(6) NOT NULL,
                UNIQUE KEY uq_sales_remittances_number (tenant_id, company_id, remittance_number),
                KEY ix_sales_remittances_date (tenant_id, company_id, remittance_date),
                KEY ix_sales_remittances_status (tenant_id, company_id, status)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS sales_remittance_invoices (
                remittance_id CHAR(36) NOT NULL,
                tenant_id CHAR(36) NOT NULL,
                company_id CHAR(36) NOT NULL,
                line_number INT NOT NULL,
                invoice_id CHAR(36) NOT NULL,
                invoice_series VARCHAR(20) NULL,
                invoice_number INT NOT NULL,
                client_code INT NOT NULL,
                client_name VARCHAR(200) NOT NULL,
                issue_date DATETIME(6) NOT NULL,
                due_date DATETIME(6) NULL,
                total_amount DECIMAL(18,2) NOT NULL,
                amount_paid DECIMAL(18,2) NOT NULL DEFAULT 0,
                outstanding_amount DECIMAL(18,2) NOT NULL DEFAULT 0,
                payment_status VARCHAR(30) NOT NULL DEFAULT 'Pending',
                notes TEXT NULL,
                PRIMARY KEY (remittance_id, line_number),
                UNIQUE KEY uq_sales_remittance_invoice (remittance_id, invoice_id),
                KEY ix_sales_remittance_invoices_invoice (tenant_id, company_id, invoice_id),
                KEY ix_sales_remittance_invoices_lookup (tenant_id, company_id, remittance_id, invoice_number)
            );
            """
        };

        foreach (var statement in statements)
        {
            await using var command = connection.CreateCommand();
            command.CommandText = statement;
            await command.ExecuteNonQueryAsync(cancellationToken);
        }

        await EnsureAppUsersRequirePasswordChangeColumnAsync(connection, cancellationToken);
        await EnsureClientDuplicateReviewProposalColumnsAsync(connection, cancellationToken);
        await EnsureLegacySyncSchemaAsync(connection, cancellationToken);
        await EnsurePurchaseOrderSyncColumnsAsync(connection, cancellationToken);
        await EnsurePurchaseOrderReceiptColumnsAsync(connection, cancellationToken);
        await EnsurePurchaseReceiptSyncColumnsAsync(connection, cancellationToken);
        await EnsurePurchaseInvoiceColumnsAsync(connection, cancellationToken);
        await EnsureReportingDateColumnsAsync(connection, cancellationToken);
        await EnsureFinishWorkOrderMachineColumnsAsync(connection, cancellationToken);
        await EnsureFinishWorkOrderOperationColumnsAsync(connection, cancellationToken);
        await EnsureFinishWorkOrderSampleColumnsAsync(connection, cancellationToken);
        await EnsureFinishWorkOrderSourceRecordIdColumnAsync(connection, cancellationToken);
        await EnsureMuestraBreakdownOperationColumnsAsync(connection, cancellationToken);
        await EnsureColumnAsync(
            connection,
            "stock_counts",
            "is_blind_count",
            "ALTER TABLE stock_counts ADD COLUMN is_blind_count TINYINT(1) NOT NULL DEFAULT 0 AFTER warehouse;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "stock_counts",
            "is_blind_count_revealed",
            "ALTER TABLE stock_counts ADD COLUMN is_blind_count_revealed TINYINT(1) NOT NULL DEFAULT 0 AFTER is_blind_count;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "stock_count_lines",
            "is_difference_validated",
            "ALTER TABLE stock_count_lines ADD COLUMN is_difference_validated TINYINT(1) NOT NULL DEFAULT 0 AFTER difference_quantity;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "base_catalog_items",
            "secondary_reference_value",
            "ALTER TABLE base_catalog_items ADD COLUMN secondary_reference_value VARCHAR(255) NULL AFTER reference_value;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "base_catalog_items",
            "secondary_numeric_value",
            "ALTER TABLE base_catalog_items ADD COLUMN secondary_numeric_value DECIMAL(18,4) NULL AFTER numeric_value;",
            cancellationToken);
        await EnsureSalesOrderShipmentColumnsAsync(connection, cancellationToken);
        await EnsureSalesInvoiceColumnsAsync(connection, cancellationToken);
        await EnsureDispositionColumnsAsync(connection, cancellationToken);
    }

    private async Task SeedDefaultsAsync(MySqlConnection connection, CancellationToken cancellationToken)
    {
        await EnsurePlanAsync(connection, "starter", "Starter", 5, 149m, "Ideal para equipos que arrancan con CRM, ventas y trazabilidad básica.", cancellationToken);
        await EnsurePlanAsync(connection, "growth", "Growth", 20, 399m, "Pensado para grupos con varias empresas y operativa diaria compartida.", cancellationToken);
        await EnsurePlanAsync(connection, "scale", "Scale", 100, 990m, "Para operaciones complejas con usuarios, procesos y reporting ampliados.", cancellationToken);

        if (!_seedOptions.Value.HasPlatformAdminSeed)
        {
            return;
        }

        var userId = await EnsurePlatformAdminAsync(connection, cancellationToken);

        if (_seedOptions.Value.HasInitialCompanySeed)
        {
            await EnsureInitialTenantAndCompanyAsync(connection, userId, cancellationToken);
        }
    }

    private static async Task EnsurePlanAsync(
        MySqlConnection connection,
        string slug,
        string name,
        int maxUsers,
        decimal monthlyPrice,
        string description,
        CancellationToken cancellationToken)
    {
        await using var existsCommand = connection.CreateCommand();
        existsCommand.CommandText = "SELECT COUNT(*) FROM plan_definitions WHERE slug = @slug;";
        existsCommand.Parameters.AddWithValue("@slug", slug);
        var exists = Convert.ToInt32(await existsCommand.ExecuteScalarAsync(cancellationToken)) > 0;
        if (exists)
        {
            return;
        }

        await using var insertCommand = connection.CreateCommand();
        insertCommand.CommandText =
            """
            INSERT INTO plan_definitions (id, slug, name, max_users, monthly_price, description, is_active, created_utc)
            VALUES (@id, @slug, @name, @maxUsers, @monthlyPrice, @description, 1, @createdUtc);
            """;
        insertCommand.Parameters.AddWithValue("@id", Guid.NewGuid().ToString());
        insertCommand.Parameters.AddWithValue("@slug", slug);
        insertCommand.Parameters.AddWithValue("@name", name);
        insertCommand.Parameters.AddWithValue("@maxUsers", maxUsers);
        insertCommand.Parameters.AddWithValue("@monthlyPrice", monthlyPrice);
        insertCommand.Parameters.AddWithValue("@description", description);
        insertCommand.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
        await insertCommand.ExecuteNonQueryAsync(cancellationToken);
    }

    private async Task<Guid> EnsurePlatformAdminAsync(MySqlConnection connection, CancellationToken cancellationToken)
    {
        var seed = _seedOptions.Value;

        await using var existsCommand = connection.CreateCommand();
        existsCommand.CommandText = "SELECT id FROM app_users WHERE email = @email LIMIT 1;";
        existsCommand.Parameters.AddWithValue("@email", seed.PlatformAdminEmail.Trim().ToLowerInvariant());
        var existingId = await existsCommand.ExecuteScalarAsync(cancellationToken);

        var userId = existingId is null
            ? Guid.NewGuid()
            : Guid.Parse(Convert.ToString(existingId)!);

        if (existingId is null)
        {
            await using var insertUserCommand = connection.CreateCommand();
            insertUserCommand.CommandText =
                """
                INSERT INTO app_users (id, email, display_name, password_hash, require_password_change, is_active, created_utc)
                VALUES (@id, @email, @displayName, @passwordHash, 1, 1, @createdUtc);
                """;
            insertUserCommand.Parameters.AddWithValue("@id", userId.ToString());
            insertUserCommand.Parameters.AddWithValue("@email", seed.PlatformAdminEmail.Trim().ToLowerInvariant());
            insertUserCommand.Parameters.AddWithValue("@displayName", seed.PlatformAdminDisplayName.Trim());
            insertUserCommand.Parameters.AddWithValue("@passwordHash", PasswordHasher.Hash(seed.PlatformAdminPassword));
            insertUserCommand.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
            await insertUserCommand.ExecuteNonQueryAsync(cancellationToken);
        }
        else
        {
            await MarkSeedPasswordForChangeAsync(connection, userId, seed.PlatformAdminPassword, cancellationToken);
        }

        await using var roleCommand = connection.CreateCommand();
        roleCommand.CommandText =
            """
            INSERT IGNORE INTO user_role_assignments (user_id, tenant_id, role_name, created_utc)
            VALUES (@userId, NULL, @roleName, @createdUtc);
            """;
        roleCommand.Parameters.AddWithValue("@userId", userId.ToString());
        roleCommand.Parameters.AddWithValue("@roleName", PlatformRoles.PlatformAdmin);
        roleCommand.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
        await roleCommand.ExecuteNonQueryAsync(cancellationToken);

        return userId;
    }

    private async Task EnsureInitialTenantAndCompanyAsync(MySqlConnection connection, Guid userId, CancellationToken cancellationToken)
    {
        var seed = _seedOptions.Value;
        var tenantId = await EnsureTenantAsync(
            connection,
            seed.InitialTenantName.Trim(),
            SlugGenerator.Generate(seed.InitialTenantSlug),
            cancellationToken);

        var companyId = await EnsureCompanyAsync(
            connection,
            tenantId,
            seed.InitialCompanyName.Trim(),
            SlugGenerator.Generate(seed.InitialCompanySlug),
            seed.InitialCompanyLegacyCenterCode.Trim().ToUpperInvariant(),
            cancellationToken);

        await EnsureUserTenantMembershipAsync(connection, userId, tenantId, cancellationToken);
        await EnsureUserCompanyMembershipAsync(connection, userId, tenantId, companyId, cancellationToken);
        await EnsureTenantRoleAsync(connection, userId, tenantId, PlatformRoles.TenantAdmin, cancellationToken);
    }

    private static async Task EnsureSalesInvoiceColumnsAsync(MySqlConnection connection, CancellationToken cancellationToken)
    {
        await EnsureColumnAsync(
            connection,
            "sales_invoices",
            "origin",
            "ALTER TABLE sales_invoices ADD COLUMN origin VARCHAR(20) NOT NULL DEFAULT 'saas' AFTER status;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "sales_invoices",
            "is_deleted",
            "ALTER TABLE sales_invoices ADD COLUMN is_deleted TINYINT(1) NOT NULL DEFAULT 0 AFTER origin;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "sales_invoices",
            "legacy_source_system",
            "ALTER TABLE sales_invoices ADD COLUMN legacy_source_system VARCHAR(40) NULL AFTER is_deleted;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "sales_invoices",
            "legacy_center_code",
            "ALTER TABLE sales_invoices ADD COLUMN legacy_center_code CHAR(1) NULL AFTER legacy_source_system;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "sales_invoices",
            "legacy_document_type",
            "ALTER TABLE sales_invoices ADD COLUMN legacy_document_type VARCHAR(40) NULL AFTER legacy_center_code;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "sales_invoices",
            "legacy_document_number",
            "ALTER TABLE sales_invoices ADD COLUMN legacy_document_number VARCHAR(80) NULL AFTER legacy_document_type;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "sales_invoices",
            "synced_utc",
            "ALTER TABLE sales_invoices ADD COLUMN synced_utc DATETIME(6) NULL AFTER legacy_document_number;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "sales_invoices",
            "payment_status",
            "ALTER TABLE sales_invoices ADD COLUMN payment_status VARCHAR(30) NOT NULL DEFAULT 'Pending' AFTER total_amount;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "sales_invoices",
            "amount_paid",
            "ALTER TABLE sales_invoices ADD COLUMN amount_paid DECIMAL(18,2) NOT NULL DEFAULT 0 AFTER payment_status;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "sales_invoices",
            "outstanding_amount",
            "ALTER TABLE sales_invoices ADD COLUMN outstanding_amount DECIMAL(18,2) NOT NULL DEFAULT 0 AFTER amount_paid;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "sales_invoices",
            "last_payment_utc",
            "ALTER TABLE sales_invoices ADD COLUMN last_payment_utc DATETIME(6) NULL AFTER outstanding_amount;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "sales_invoices",
            "accounting_status",
            "ALTER TABLE sales_invoices ADD COLUMN accounting_status VARCHAR(30) NOT NULL DEFAULT 'Pending' AFTER last_payment_utc;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "sales_invoices",
            "accounting_reference",
            "ALTER TABLE sales_invoices ADD COLUMN accounting_reference VARCHAR(80) NULL AFTER accounting_status;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "sales_invoices",
            "accounting_ready_utc",
            "ALTER TABLE sales_invoices ADD COLUMN accounting_ready_utc DATETIME(6) NULL AFTER accounting_reference;",
            cancellationToken);

        await using var backfillCommand = connection.CreateCommand();
        backfillCommand.CommandText =
            """
            UPDATE sales_invoices
            SET payment_status = CASE
                    WHEN COALESCE(amount_paid, 0) <= 0 THEN 'Pending'
                    WHEN COALESCE(amount_paid, 0) >= total_amount THEN 'Paid'
                    ELSE 'PartiallyPaid'
                END,
                outstanding_amount = GREATEST(total_amount - COALESCE(amount_paid, 0), 0),
                accounting_status = CASE
                    WHEN accounting_status IS NULL OR accounting_status = '' OR accounting_status = 'Pending' THEN 'Ready'
                    ELSE accounting_status
                END,
                accounting_ready_utc = COALESCE(accounting_ready_utc, issued_utc)
            WHERE 1 = 1;
            """;
        await backfillCommand.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task EnsureDispositionColumnsAsync(MySqlConnection connection, CancellationToken cancellationToken)
    {
        await EnsureColumnAsync(
            connection,
            "dispos",
            "origin",
            "ALTER TABLE dispos ADD COLUMN origin VARCHAR(20) NOT NULL DEFAULT 'legacy' AFTER COMANDA;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "dispos",
            "is_deleted",
            "ALTER TABLE dispos ADD COLUMN is_deleted TINYINT(1) NOT NULL DEFAULT 0 AFTER origin;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "dispos",
            "synced_utc",
            "ALTER TABLE dispos ADD COLUMN synced_utc DATETIME(6) NULL AFTER is_deleted;",
            cancellationToken);
    }

    private static async Task<Guid> EnsureTenantAsync(
        MySqlConnection connection,
        string name,
        string slug,
        CancellationToken cancellationToken)
    {
        await using var existsCommand = connection.CreateCommand();
        existsCommand.CommandText = "SELECT id FROM tenants WHERE slug = @slug LIMIT 1;";
        existsCommand.Parameters.AddWithValue("@slug", slug);
        var existingId = await existsCommand.ExecuteScalarAsync(cancellationToken);
        if (existingId is not null)
        {
            return Guid.Parse(Convert.ToString(existingId)!);
        }

        var tenantId = Guid.NewGuid();
        await using var insertCommand = connection.CreateCommand();
        insertCommand.CommandText =
            """
            INSERT INTO tenants (id, name, slug, is_active, created_utc)
            VALUES (@id, @name, @slug, 1, @createdUtc);
            """;
        insertCommand.Parameters.AddWithValue("@id", tenantId.ToString());
        insertCommand.Parameters.AddWithValue("@name", name);
        insertCommand.Parameters.AddWithValue("@slug", slug);
        insertCommand.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
        await insertCommand.ExecuteNonQueryAsync(cancellationToken);

        return tenantId;
    }

    private static async Task<Guid> EnsureCompanyAsync(
        MySqlConnection connection,
        Guid tenantId,
        string name,
        string slug,
        string legacyCenterCode,
        CancellationToken cancellationToken)
    {
        await using var existsCommand = connection.CreateCommand();
        existsCommand.CommandText =
            """
            SELECT id
            FROM companies
            WHERE tenant_id = @tenantId
              AND slug = @slug
            LIMIT 1;
            """;
        existsCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        existsCommand.Parameters.AddWithValue("@slug", slug);
        var existingId = await existsCommand.ExecuteScalarAsync(cancellationToken);
        if (existingId is not null)
        {
            return Guid.Parse(Convert.ToString(existingId)!);
        }

        var companyId = Guid.NewGuid();
        await using var insertCommand = connection.CreateCommand();
        insertCommand.CommandText =
            """
            INSERT INTO companies (id, tenant_id, name, slug, legacy_center_code, is_active, created_utc)
            VALUES (@id, @tenantId, @name, @slug, @legacyCenterCode, 1, @createdUtc);
            """;
        insertCommand.Parameters.AddWithValue("@id", companyId.ToString());
        insertCommand.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        insertCommand.Parameters.AddWithValue("@name", name);
        insertCommand.Parameters.AddWithValue("@slug", slug);
        insertCommand.Parameters.AddWithValue("@legacyCenterCode", legacyCenterCode);
        insertCommand.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
        await insertCommand.ExecuteNonQueryAsync(cancellationToken);

        return companyId;
    }

    private static async Task EnsureUserTenantMembershipAsync(
        MySqlConnection connection,
        Guid userId,
        Guid tenantId,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            INSERT IGNORE INTO user_tenant_memberships (user_id, tenant_id, is_default, created_utc)
            VALUES (@userId, @tenantId, 1, @createdUtc);
            """;
        command.Parameters.AddWithValue("@userId", userId.ToString());
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task EnsureUserCompanyMembershipAsync(
        MySqlConnection connection,
        Guid userId,
        Guid tenantId,
        Guid companyId,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            INSERT IGNORE INTO user_company_memberships (user_id, tenant_id, company_id, created_utc)
            VALUES (@userId, @tenantId, @companyId, @createdUtc);
            """;
        command.Parameters.AddWithValue("@userId", userId.ToString());
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task EnsureTenantRoleAsync(
        MySqlConnection connection,
        Guid userId,
        Guid tenantId,
        string roleName,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            INSERT IGNORE INTO user_role_assignments (user_id, tenant_id, role_name, created_utc)
            VALUES (@userId, @tenantId, @roleName, @createdUtc);
            """;
        command.Parameters.AddWithValue("@userId", userId.ToString());
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@roleName", roleName);
        command.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task MarkSeedPasswordForChangeAsync(
        MySqlConnection connection,
        Guid userId,
        string seedPassword,
        CancellationToken cancellationToken)
    {
        await using var selectCommand = connection.CreateCommand();
        selectCommand.CommandText =
            """
            SELECT password_hash, require_password_change
            FROM app_users
            WHERE id = @userId
            LIMIT 1;
            """;
        selectCommand.Parameters.AddWithValue("@userId", userId.ToString());

        await using var reader = await selectCommand.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return;
        }

        var passwordHash = reader.GetString("password_hash");
        var requirePasswordChange = reader.GetBooleanValue("require_password_change");
        await reader.DisposeAsync();

        if (requirePasswordChange || !PasswordHasher.Verify(seedPassword, passwordHash))
        {
            return;
        }

        await using var updateCommand = connection.CreateCommand();
        updateCommand.CommandText =
            """
            UPDATE app_users
            SET require_password_change = 1
            WHERE id = @userId;
            """;
        updateCommand.Parameters.AddWithValue("@userId", userId.ToString());
        await updateCommand.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task EnsureAppUsersRequirePasswordChangeColumnAsync(
        MySqlConnection connection,
        CancellationToken cancellationToken)
    {
        await EnsureColumnAsync(
            connection,
            "app_users",
            "require_password_change",
            """
            ALTER TABLE app_users
            ADD COLUMN require_password_change TINYINT(1) NOT NULL DEFAULT 0
            AFTER password_hash;
            """,
            cancellationToken);
    }

    private static async Task EnsureClientDuplicateReviewProposalColumnsAsync(
        MySqlConnection connection,
        CancellationToken cancellationToken)
    {
        await EnsureColumnAsync(
            connection,
            "client_duplicate_reviews",
            "preferred_client_code",
            "ALTER TABLE client_duplicate_reviews ADD COLUMN preferred_client_code INT NULL AFTER updated_utc;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "client_duplicate_reviews",
            "preferred_updated_by_user_id",
            "ALTER TABLE client_duplicate_reviews ADD COLUMN preferred_updated_by_user_id CHAR(36) NULL AFTER preferred_client_code;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "client_duplicate_reviews",
            "preferred_updated_utc",
            "ALTER TABLE client_duplicate_reviews ADD COLUMN preferred_updated_utc DATETIME(6) NULL AFTER preferred_updated_by_user_id;",
            cancellationToken);
    }

    private static async Task EnsureLegacySyncSchemaAsync(
        MySqlConnection connection,
        CancellationToken cancellationToken)
    {
        await EnsureColumnAsync(
            connection,
            "sales_orders",
            "origin",
            "ALTER TABLE sales_orders ADD COLUMN origin VARCHAR(20) NOT NULL DEFAULT 'saas' AFTER status;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "inventory_movements",
            "color",
            "ALTER TABLE inventory_movements ADD COLUMN color VARCHAR(120) NULL AFTER item_description;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "sales_orders",
            "is_deleted",
            "ALTER TABLE sales_orders ADD COLUMN is_deleted TINYINT(1) NOT NULL DEFAULT 0 AFTER origin;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "sales_orders",
            "legacy_source_system",
            "ALTER TABLE sales_orders ADD COLUMN legacy_source_system VARCHAR(40) NULL AFTER is_deleted;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "sales_orders",
            "legacy_center_code",
            "ALTER TABLE sales_orders ADD COLUMN legacy_center_code CHAR(1) NULL AFTER legacy_source_system;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "sales_orders",
            "legacy_document_type",
            "ALTER TABLE sales_orders ADD COLUMN legacy_document_type VARCHAR(40) NULL AFTER legacy_center_code;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "sales_orders",
            "legacy_document_number",
            "ALTER TABLE sales_orders ADD COLUMN legacy_document_number VARCHAR(80) NULL AFTER legacy_document_type;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "sales_orders",
            "synced_utc",
            "ALTER TABLE sales_orders ADD COLUMN synced_utc DATETIME(6) NULL AFTER legacy_document_number;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "sales_order_lines",
            "legacy_source_system",
            "ALTER TABLE sales_order_lines ADD COLUMN legacy_source_system VARCHAR(40) NULL AFTER requested_date;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "sales_order_lines",
            "legacy_center_code",
            "ALTER TABLE sales_order_lines ADD COLUMN legacy_center_code CHAR(1) NULL AFTER legacy_source_system;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "sales_order_lines",
            "legacy_document_type",
            "ALTER TABLE sales_order_lines ADD COLUMN legacy_document_type VARCHAR(40) NULL AFTER legacy_center_code;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "sales_order_lines",
            "legacy_document_number",
            "ALTER TABLE sales_order_lines ADD COLUMN legacy_document_number VARCHAR(80) NULL AFTER legacy_document_type;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "sales_order_lines",
            "legacy_line_number",
            "ALTER TABLE sales_order_lines ADD COLUMN legacy_line_number INT NULL AFTER legacy_document_number;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "sales_order_lines",
            "synced_utc",
            "ALTER TABLE sales_order_lines ADD COLUMN synced_utc DATETIME(6) NULL AFTER legacy_line_number;",
            cancellationToken);
    }

    private static async Task EnsurePurchaseOrderReceiptColumnsAsync(
        MySqlConnection connection,
        CancellationToken cancellationToken)
    {
        await EnsureColumnAsync(
            connection,
            "purchase_order_lines",
            "received_quantity",
            "ALTER TABLE purchase_order_lines ADD COLUMN received_quantity DECIMAL(18,3) NOT NULL DEFAULT 0 AFTER quantity;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "purchase_order_lines",
            "last_received_utc",
            "ALTER TABLE purchase_order_lines ADD COLUMN last_received_utc DATETIME(6) NULL AFTER expected_date;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "purchase_order_receipts",
            "receipt_series",
            "ALTER TABLE purchase_order_receipts ADD COLUMN receipt_series VARCHAR(20) NULL AFTER receipt_id;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "purchase_order_receipts",
            "receipt_number",
            "ALTER TABLE purchase_order_receipts ADD COLUMN receipt_number INT NULL AFTER receipt_series;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "purchase_order_receipts",
            "warehouse",
            "ALTER TABLE purchase_order_receipts ADD COLUMN warehouse VARCHAR(120) NULL AFTER receipt_date;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "purchase_order_receipts",
            "carrier",
            "ALTER TABLE purchase_order_receipts ADD COLUMN carrier VARCHAR(160) NULL AFTER warehouse;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "purchase_order_receipts",
            "supplier_reference",
            "ALTER TABLE purchase_order_receipts ADD COLUMN supplier_reference VARCHAR(120) NULL AFTER carrier;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "purchase_order_receipts",
            "vehicle_plate",
            "ALTER TABLE purchase_order_receipts ADD COLUMN vehicle_plate VARCHAR(40) NULL AFTER supplier_reference;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "purchase_order_receipts",
            "package_count",
            "ALTER TABLE purchase_order_receipts ADD COLUMN package_count INT NULL AFTER vehicle_plate;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "purchase_order_receipts",
            "gross_weight_kg",
            "ALTER TABLE purchase_order_receipts ADD COLUMN gross_weight_kg DECIMAL(18,3) NULL AFTER package_count;",
            cancellationToken);
    }

    private static async Task EnsurePurchaseReceiptSyncColumnsAsync(
        MySqlConnection connection,
        CancellationToken cancellationToken)
    {
        await EnsureColumnAsync(
            connection,
            "purchase_order_receipts",
            "origin",
            "ALTER TABLE purchase_order_receipts ADD COLUMN origin VARCHAR(20) NOT NULL DEFAULT 'saas' AFTER warehouse;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "purchase_order_receipts",
            "is_deleted",
            "ALTER TABLE purchase_order_receipts ADD COLUMN is_deleted TINYINT(1) NOT NULL DEFAULT 0 AFTER origin;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "purchase_order_receipts",
            "legacy_source_system",
            "ALTER TABLE purchase_order_receipts ADD COLUMN legacy_source_system VARCHAR(40) NULL AFTER is_deleted;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "purchase_order_receipts",
            "legacy_center_code",
            "ALTER TABLE purchase_order_receipts ADD COLUMN legacy_center_code CHAR(1) NULL AFTER legacy_source_system;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "purchase_order_receipts",
            "legacy_document_type",
            "ALTER TABLE purchase_order_receipts ADD COLUMN legacy_document_type VARCHAR(40) NULL AFTER legacy_center_code;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "purchase_order_receipts",
            "legacy_document_number",
            "ALTER TABLE purchase_order_receipts ADD COLUMN legacy_document_number VARCHAR(80) NULL AFTER legacy_document_type;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "purchase_order_receipts",
            "synced_utc",
            "ALTER TABLE purchase_order_receipts ADD COLUMN synced_utc DATETIME(6) NULL AFTER legacy_document_number;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "purchase_order_receipt_lines",
            "legacy_source_system",
            "ALTER TABLE purchase_order_receipt_lines ADD COLUMN legacy_source_system VARCHAR(40) NULL AFTER received_quantity;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "purchase_order_receipt_lines",
            "legacy_center_code",
            "ALTER TABLE purchase_order_receipt_lines ADD COLUMN legacy_center_code CHAR(1) NULL AFTER legacy_source_system;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "purchase_order_receipt_lines",
            "legacy_document_type",
            "ALTER TABLE purchase_order_receipt_lines ADD COLUMN legacy_document_type VARCHAR(40) NULL AFTER legacy_center_code;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "purchase_order_receipt_lines",
            "legacy_document_number",
            "ALTER TABLE purchase_order_receipt_lines ADD COLUMN legacy_document_number VARCHAR(80) NULL AFTER legacy_document_type;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "purchase_order_receipt_lines",
            "legacy_line_number",
            "ALTER TABLE purchase_order_receipt_lines ADD COLUMN legacy_line_number INT NULL AFTER legacy_document_number;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "purchase_order_receipt_lines",
            "synced_utc",
            "ALTER TABLE purchase_order_receipt_lines ADD COLUMN synced_utc DATETIME(6) NULL AFTER legacy_line_number;",
            cancellationToken);
    }

    private static async Task EnsurePurchaseOrderSyncColumnsAsync(
        MySqlConnection connection,
        CancellationToken cancellationToken)
    {
        await EnsureColumnAsync(
            connection,
            "purchase_orders",
            "origin",
            "ALTER TABLE purchase_orders ADD COLUMN origin VARCHAR(20) NOT NULL DEFAULT 'saas' AFTER status;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "purchase_orders",
            "is_deleted",
            "ALTER TABLE purchase_orders ADD COLUMN is_deleted TINYINT(1) NOT NULL DEFAULT 0 AFTER origin;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "purchase_orders",
            "legacy_source_system",
            "ALTER TABLE purchase_orders ADD COLUMN legacy_source_system VARCHAR(40) NULL AFTER is_deleted;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "purchase_orders",
            "legacy_center_code",
            "ALTER TABLE purchase_orders ADD COLUMN legacy_center_code CHAR(1) NULL AFTER legacy_source_system;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "purchase_orders",
            "legacy_document_type",
            "ALTER TABLE purchase_orders ADD COLUMN legacy_document_type VARCHAR(40) NULL AFTER legacy_center_code;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "purchase_orders",
            "legacy_document_number",
            "ALTER TABLE purchase_orders ADD COLUMN legacy_document_number VARCHAR(80) NULL AFTER legacy_document_type;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "purchase_orders",
            "synced_utc",
            "ALTER TABLE purchase_orders ADD COLUMN synced_utc DATETIME(6) NULL AFTER legacy_document_number;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "purchase_order_lines",
            "legacy_source_system",
            "ALTER TABLE purchase_order_lines ADD COLUMN legacy_source_system VARCHAR(40) NULL AFTER last_received_utc;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "purchase_order_lines",
            "legacy_center_code",
            "ALTER TABLE purchase_order_lines ADD COLUMN legacy_center_code CHAR(1) NULL AFTER legacy_source_system;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "purchase_order_lines",
            "legacy_document_type",
            "ALTER TABLE purchase_order_lines ADD COLUMN legacy_document_type VARCHAR(40) NULL AFTER legacy_center_code;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "purchase_order_lines",
            "legacy_document_number",
            "ALTER TABLE purchase_order_lines ADD COLUMN legacy_document_number VARCHAR(80) NULL AFTER legacy_document_type;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "purchase_order_lines",
            "legacy_line_number",
            "ALTER TABLE purchase_order_lines ADD COLUMN legacy_line_number INT NULL AFTER legacy_document_number;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "purchase_order_lines",
            "synced_utc",
            "ALTER TABLE purchase_order_lines ADD COLUMN synced_utc DATETIME(6) NULL AFTER legacy_line_number;",
            cancellationToken);
    }

    private static async Task EnsureSalesOrderShipmentColumnsAsync(
        MySqlConnection connection,
        CancellationToken cancellationToken)
    {
        await EnsureColumnAsync(
            connection,
            "sales_order_shipments",
            "origin",
            "ALTER TABLE sales_order_shipments ADD COLUMN origin VARCHAR(20) NOT NULL DEFAULT 'saas' AFTER warehouse;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "sales_order_shipments",
            "is_deleted",
            "ALTER TABLE sales_order_shipments ADD COLUMN is_deleted TINYINT(1) NOT NULL DEFAULT 0 AFTER origin;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "sales_order_shipments",
            "legacy_source_system",
            "ALTER TABLE sales_order_shipments ADD COLUMN legacy_source_system VARCHAR(40) NULL AFTER is_deleted;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "sales_order_shipments",
            "legacy_center_code",
            "ALTER TABLE sales_order_shipments ADD COLUMN legacy_center_code CHAR(1) NULL AFTER legacy_source_system;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "sales_order_shipments",
            "legacy_document_type",
            "ALTER TABLE sales_order_shipments ADD COLUMN legacy_document_type VARCHAR(40) NULL AFTER legacy_center_code;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "sales_order_shipments",
            "legacy_document_number",
            "ALTER TABLE sales_order_shipments ADD COLUMN legacy_document_number VARCHAR(80) NULL AFTER legacy_document_type;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "sales_order_shipments",
            "synced_utc",
            "ALTER TABLE sales_order_shipments ADD COLUMN synced_utc DATETIME(6) NULL AFTER legacy_document_number;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "sales_order_shipments",
            "invoice_status",
            "ALTER TABLE sales_order_shipments ADD COLUMN invoice_status VARCHAR(30) NOT NULL DEFAULT 'Pending' AFTER synced_utc;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "sales_order_shipments",
            "invoice_reference",
            "ALTER TABLE sales_order_shipments ADD COLUMN invoice_reference VARCHAR(80) NULL AFTER invoice_status;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "sales_order_shipments",
            "invoice_draft_id",
            "ALTER TABLE sales_order_shipments ADD COLUMN invoice_draft_id CHAR(36) NULL AFTER invoice_reference;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "sales_order_shipments",
            "invoice_id",
            "ALTER TABLE sales_order_shipments ADD COLUMN invoice_id CHAR(36) NULL AFTER invoice_draft_id;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "sales_order_shipments",
            "invoice_ready_utc",
            "ALTER TABLE sales_order_shipments ADD COLUMN invoice_ready_utc DATETIME(6) NULL AFTER invoice_id;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "repres",
            "origin",
            "ALTER TABLE repres ADD COLUMN origin VARCHAR(20) NOT NULL DEFAULT 'legacy' AFTER EMAIL2;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "repres",
            "is_deleted",
            "ALTER TABLE repres ADD COLUMN is_deleted TINYINT(1) NOT NULL DEFAULT 0 AFTER origin;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "repres",
            "synced_utc",
            "ALTER TABLE repres ADD COLUMN synced_utc DATETIME(6) NULL AFTER is_deleted;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "fil",
            "origin",
            "ALTER TABLE fil ADD COLUMN origin VARCHAR(20) NOT NULL DEFAULT 'legacy' AFTER CENTRO;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "fil",
            "is_deleted",
            "ALTER TABLE fil ADD COLUMN is_deleted TINYINT(1) NOT NULL DEFAULT 0 AFTER origin;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "fil",
            "synced_utc",
            "ALTER TABLE fil ADD COLUMN synced_utc DATETIME(6) NULL AFTER is_deleted;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "teixits",
            "AMPLE2",
            "ALTER TABLE teixits ADD COLUMN AMPLE2 DECIMAL(12,4) NOT NULL DEFAULT 0 AFTER TUBULAR;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "teixits",
            "origin",
            "ALTER TABLE teixits ADD COLUMN origin VARCHAR(20) NOT NULL DEFAULT 'legacy' AFTER AMPLE2;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "teixits",
            "is_deleted",
            "ALTER TABLE teixits ADD COLUMN is_deleted TINYINT(1) NOT NULL DEFAULT 0 AFTER origin;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "teixits",
            "synced_utc",
            "ALTER TABLE teixits ADD COLUMN synced_utc DATETIME(6) NULL AFTER is_deleted;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "mostres",
            "NOMCLIENT",
            "ALTER TABLE mostres ADD COLUMN NOMCLIENT VARCHAR(255) NULL AFTER CLIENT;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "mostres",
            "NOMMAQUI",
            "ALTER TABLE mostres ADD COLUMN NOMMAQUI VARCHAR(255) NULL AFTER MAQUINA;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "mostres",
            "origin",
            "ALTER TABLE mostres ADD COLUMN origin VARCHAR(20) NOT NULL DEFAULT 'legacy' AFTER CENTRO;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "mostres",
            "is_deleted",
            "ALTER TABLE mostres ADD COLUMN is_deleted TINYINT(1) NOT NULL DEFAULT 0 AFTER origin;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "mostres",
            "synced_utc",
            "ALTER TABLE mostres ADD COLUMN synced_utc DATETIME(6) NULL AFTER is_deleted;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "trans",
            "origin",
            "ALTER TABLE trans ADD COLUMN origin VARCHAR(20) NOT NULL DEFAULT 'legacy' AFTER CENTRO;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "trans",
            "is_deleted",
            "ALTER TABLE trans ADD COLUMN is_deleted TINYINT(1) NOT NULL DEFAULT 0 AFTER origin;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "trans",
            "synced_utc",
            "ALTER TABLE trans ADD COLUMN synced_utc DATETIME(6) NULL AFTER is_deleted;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "tallers",
            "origin",
            "ALTER TABLE tallers ADD COLUMN origin VARCHAR(20) NOT NULL DEFAULT 'legacy' AFTER PAIS;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "tallers",
            "is_deleted",
            "ALTER TABLE tallers ADD COLUMN is_deleted TINYINT(1) NOT NULL DEFAULT 0 AFTER origin;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "tallers",
            "synced_utc",
            "ALTER TABLE tallers ADD COLUMN synced_utc DATETIME(6) NULL AFTER is_deleted;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "forni",
            "origin",
            "ALTER TABLE forni ADD COLUMN origin VARCHAR(20) NOT NULL DEFAULT 'legacy' AFTER CENTRO;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "forni",
            "is_deleted",
            "ALTER TABLE forni ADD COLUMN is_deleted TINYINT(1) NOT NULL DEFAULT 0 AFTER origin;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "forni",
            "synced_utc",
            "ALTER TABLE forni ADD COLUMN synced_utc DATETIME(6) NULL AFTER is_deleted;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "clients",
            "origin",
            "ALTER TABLE clients ADD COLUMN origin VARCHAR(20) NOT NULL DEFAULT 'legacy' AFTER BLOQUEADO;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "clients",
            "is_deleted",
            "ALTER TABLE clients ADD COLUMN is_deleted TINYINT(1) NOT NULL DEFAULT 0 AFTER origin;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "clients",
            "synced_utc",
            "ALTER TABLE clients ADD COLUMN synced_utc DATETIME(6) NULL AFTER is_deleted;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "prove",
            "origin",
            "ALTER TABLE prove ADD COLUMN origin VARCHAR(20) NOT NULL DEFAULT 'legacy' AFTER NOTES;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "prove",
            "is_deleted",
            "ALTER TABLE prove ADD COLUMN is_deleted TINYINT(1) NOT NULL DEFAULT 0 AFTER origin;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "prove",
            "synced_utc",
            "ALTER TABLE prove ADD COLUMN synced_utc DATETIME(6) NULL AFTER is_deleted;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "sales_invoice_drafts",
            "invoice_id",
            "ALTER TABLE sales_invoice_drafts ADD COLUMN invoice_id CHAR(36) NULL AFTER status;",
            cancellationToken);

        await EnsureColumnAsync(
            connection,
            "sales_invoice_drafts",
            "issued_utc",
            "ALTER TABLE sales_invoice_drafts ADD COLUMN issued_utc DATETIME(6) NULL AFTER invoice_id;",
            cancellationToken);
    }

    private static async Task EnsurePurchaseInvoiceColumnsAsync(MySqlConnection connection, CancellationToken cancellationToken)
    {
        await EnsureColumnAsync(
            connection,
            "purchase_invoices",
            "amount_paid",
            "ALTER TABLE purchase_invoices ADD COLUMN amount_paid DECIMAL(18,2) NOT NULL DEFAULT 0 AFTER total_amount;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "purchase_invoices",
            "outstanding_amount",
            "ALTER TABLE purchase_invoices ADD COLUMN outstanding_amount DECIMAL(18,2) NOT NULL DEFAULT 0 AFTER amount_paid;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "purchase_invoices",
            "last_payment_utc",
            "ALTER TABLE purchase_invoices ADD COLUMN last_payment_utc DATETIME(6) NULL AFTER outstanding_amount;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "purchase_invoice_lines",
            "source_order_line_number",
            "ALTER TABLE purchase_invoice_lines ADD COLUMN source_order_line_number INT NULL AFTER source_order_number;",
            cancellationToken);

        await using var normalizeCommand = connection.CreateCommand();
        normalizeCommand.CommandText =
            """
            UPDATE purchase_invoices
            SET outstanding_amount = CASE
                    WHEN status = 'Paid' THEN 0
                    WHEN COALESCE(amount_paid, 0) <= 0 THEN total_amount
                    ELSE GREATEST(total_amount - amount_paid, 0)
                END
            WHERE outstanding_amount = 0
              AND total_amount > 0;
            """;
        await normalizeCommand.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task EnsureReportingDateColumnsAsync(MySqlConnection connection, CancellationToken cancellationToken)
    {
        await EnsureColumnAsync(
            connection,
            "sales_orders",
            "document_date",
            "ALTER TABLE sales_orders ADD COLUMN document_date DATETIME(6) NULL AFTER client_tax_id;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "purchase_orders",
            "document_date",
            "ALTER TABLE purchase_orders ADD COLUMN document_date DATETIME(6) NULL AFTER supplier_tax_id;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "purchase_invoices",
            "document_date",
            "ALTER TABLE purchase_invoices ADD COLUMN document_date DATETIME(6) NULL AFTER supplier_document_number;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "finish_work_orders",
            "work_date",
            "ALTER TABLE finish_work_orders ADD COLUMN work_date DATETIME(6) NULL AFTER order_number;",
            cancellationToken);

        await using var normalizeSalesOrders = connection.CreateCommand();
        normalizeSalesOrders.CommandText =
            """
            UPDATE sales_orders
            SET document_date = COALESCE(document_date, requested_date, created_utc, updated_utc, synced_utc, CURRENT_TIMESTAMP(6))
            WHERE document_date IS NULL;
            """;
        await normalizeSalesOrders.ExecuteNonQueryAsync(cancellationToken);

        await using var normalizePurchaseOrders = connection.CreateCommand();
        normalizePurchaseOrders.CommandText =
            """
            UPDATE purchase_orders
            SET document_date = COALESCE(document_date, expected_date, created_utc, updated_utc, synced_utc, CURRENT_TIMESTAMP(6))
            WHERE document_date IS NULL;
            """;
        await normalizePurchaseOrders.ExecuteNonQueryAsync(cancellationToken);

        await using var normalizePurchaseInvoices = connection.CreateCommand();
        normalizePurchaseInvoices.CommandText =
            """
            UPDATE purchase_invoices
            SET document_date = COALESCE(document_date, due_date, created_utc, updated_utc, synced_utc, CURRENT_TIMESTAMP(6))
            WHERE document_date IS NULL;
            """;
        await normalizePurchaseInvoices.ExecuteNonQueryAsync(cancellationToken);

        await using var normalizeFinishOrders = connection.CreateCommand();
        normalizeFinishOrders.CommandText =
            """
            UPDATE finish_work_orders
            SET work_date = COALESCE(work_date, created_utc, updated_utc, synced_utc, CURRENT_TIMESTAMP(6))
            WHERE work_date IS NULL;
            """;
        await normalizeFinishOrders.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task EnsureFinishWorkOrderMachineColumnsAsync(MySqlConnection connection, CancellationToken cancellationToken)
    {
        await EnsureColumnAsync(
            connection,
            "finish_work_orders",
            "machine_code",
            "ALTER TABLE finish_work_orders ADD COLUMN machine_code INT NOT NULL DEFAULT 0 AFTER finisher_name;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "finish_work_orders",
            "machine_name",
            "ALTER TABLE finish_work_orders ADD COLUMN machine_name VARCHAR(255) NULL AFTER machine_code;",
            cancellationToken);
    }

    private static async Task EnsureFinishWorkOrderSampleColumnsAsync(MySqlConnection connection, CancellationToken cancellationToken)
    {
        await EnsureColumnAsync(
            connection,
            "finish_work_orders",
            "source_sample_kind",
            "ALTER TABLE finish_work_orders ADD COLUMN source_sample_kind VARCHAR(30) NULL AFTER disposition_label;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "finish_work_orders",
            "source_sample_code",
            "ALTER TABLE finish_work_orders ADD COLUMN source_sample_code VARCHAR(120) NULL AFTER source_sample_kind;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "finish_work_orders",
            "source_sample_line_number",
            "ALTER TABLE finish_work_orders ADD COLUMN source_sample_line_number INT NULL AFTER source_sample_code;",
            cancellationToken);
    }

    private static async Task EnsureFinishWorkOrderSourceRecordIdColumnAsync(MySqlConnection connection, CancellationToken cancellationToken)
    {
        await EnsureColumnAsync(
            connection,
            "finish_work_orders",
            "source_record_id",
            "ALTER TABLE finish_work_orders ADD COLUMN source_record_id CHAR(36) NULL AFTER source_sample_line_number;",
            cancellationToken);
    }

    private static async Task EnsureFinishWorkOrderOperationColumnsAsync(MySqlConnection connection, CancellationToken cancellationToken)
    {
        await EnsureColumnAsync(
            connection,
            "finish_work_orders",
            "operation_code",
            "ALTER TABLE finish_work_orders ADD COLUMN operation_code INT NOT NULL DEFAULT 0 AFTER machine_name;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "finish_work_orders",
            "operation_name",
            "ALTER TABLE finish_work_orders ADD COLUMN operation_name VARCHAR(255) NULL AFTER operation_code;",
            cancellationToken);
    }

    private static async Task EnsureMuestraBreakdownOperationColumnsAsync(MySqlConnection connection, CancellationToken cancellationToken)
    {
        await EnsureColumnAsync(
            connection,
            "mostres_breakdown",
            "OPERACIO",
            "ALTER TABLE mostres_breakdown ADD COLUMN OPERACIO INT NOT NULL DEFAULT 0 AFTER NOMMAQUI;",
            cancellationToken);
        await EnsureColumnAsync(
            connection,
            "mostres_breakdown",
            "NOMOPER",
            "ALTER TABLE mostres_breakdown ADD COLUMN NOMOPER VARCHAR(255) NULL AFTER OPERACIO;",
            cancellationToken);
    }

    private static async Task EnsureColumnAsync(
        MySqlConnection connection,
        string tableName,
        string columnName,
        string alterSql,
        CancellationToken cancellationToken)
    {
        await using var existsCommand = connection.CreateCommand();
        existsCommand.CommandText =
            """
            SELECT COUNT(*)
            FROM information_schema.columns
            WHERE table_schema = DATABASE()
              AND table_name = @tableName
              AND column_name = @columnName;
            """;
        existsCommand.Parameters.AddWithValue("@tableName", tableName);
        existsCommand.Parameters.AddWithValue("@columnName", columnName);

        var exists = Convert.ToInt32(await existsCommand.ExecuteScalarAsync(cancellationToken)) > 0;
        if (exists)
        {
            return;
        }

        await using var alterCommand = connection.CreateCommand();
        alterCommand.CommandText = alterSql;
        await alterCommand.ExecuteNonQueryAsync(cancellationToken);
    }
}
