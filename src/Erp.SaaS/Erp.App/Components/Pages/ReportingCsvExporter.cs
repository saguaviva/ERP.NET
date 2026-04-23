using System.Text;
using Erp.Application.Reporting;
using Erp.App.Localization;

namespace Erp.App.Components.Pages;

internal static class ReportingCsvExporter
{
    public static byte[] BuildOperationalDocumentsCsv(
        IReadOnlyCollection<OperationalDocumentListItemDto> items,
        string language)
    {
        var builder = new StringBuilder();
        builder.AppendLine("sep=;");
        builder.AppendLine(string.Join(";",
            L(language, "Fecha", "Data", "Date"),
            L(language, "Categoría", "Categoria", "Category"),
            L(language, "Tipo", "Tipus", "Type"),
            L(language, "Documento", "Document", "Document"),
            L(language, "Tercero", "Tercer", "Counterparty"),
            L(language, "Estado", "Estat", "Status"),
            L(language, "Importe", "Import", "Amount"),
            L(language, "Ruta", "Ruta", "Route")));

        foreach (var item in items.OrderByDescending(x => x.DocumentDate).ThenByDescending(x => x.DocumentNumber))
        {
            builder.AppendLine(string.Join(";",
                Escape(item.DocumentDate.ToString("dd/MM/yyyy")),
                Escape(item.Category),
                Escape(item.TypeLabel),
                Escape(item.DocumentDisplay),
                Escape(item.PartyName),
                Escape(item.Status),
                Escape(item.Amount.ToUiString("0.00")),
                Escape(item.Route)));
        }

        return BuildExcelFriendlyCsv(builder);
    }

    public static byte[] BuildStatisticsCsv(BusinessStatisticsDto stats, string language, string? area = null)
    {
        var normalizedArea = NormalizeArea(area);
        var builder = new StringBuilder();
        builder.AppendLine("sep=;");
        builder.AppendLine(string.Join(";",
            L(language, "Bloque", "Bloc", "Block"),
            L(language, "Métrica", "Mètrica", "Metric"),
            L(language, "Valor", "Valor", "Value"),
            L(language, "Importe", "Import", "Amount"),
            L(language, "Peso %", "Pes %", "Share %"),
            L(language, "Actual", "Actual", "Actual"),
            L(language, "Gap", "Gap", "Gap")));

        if (string.IsNullOrWhiteSpace(normalizedArea) || string.Equals(normalizedArea, "sales", StringComparison.Ordinal))
        {
            AppendMetric(builder, L(language, "Ventas", "Vendes", "Sales"), L(language, "Pedidos", "Comandes", "Orders"), stats.SalesOrderCount.ToString(), stats.SalesOrderAmount);
            AppendMetric(builder, L(language, "Ventas", "Vendes", "Sales"), L(language, "Ticket medio pedido", "Ticket mitjà comanda", "Average order value"), string.Empty, stats.AverageSalesOrderAmount);
            AppendMetric(builder, L(language, "Ventas", "Vendes", "Sales"), L(language, "Albaranes", "Albarans", "Shipments"), stats.SalesShipmentCount.ToString(), stats.SalesShipmentAmount);
            AppendMetric(builder, L(language, "Ventas", "Vendes", "Sales"), L(language, "Facturas", "Factures", "Invoices"), stats.SalesInvoiceCount.ToString(), stats.SalesInvoiceAmount);
            AppendMetric(builder, L(language, "Ventas", "Vendes", "Sales"), L(language, "Facturado MTD", "Facturat MTD", "Invoiced MTD"), string.Empty, stats.SalesInvoiceAmountMonthToDate);
            AppendMetric(builder, L(language, "Ventas", "Vendes", "Sales"), L(language, "Facturado YTD", "Facturat YTD", "Invoiced YTD"), string.Empty, stats.SalesInvoiceAmountYearToDate);
            AppendMetric(builder, L(language, "Ventas", "Vendes", "Sales"), L(language, "Cobrado", "Cobrat", "Collected"), string.Empty, stats.SalesCollectedAmount);
            AppendMetric(builder, L(language, "Ventas", "Vendes", "Sales"), L(language, "Pendiente cobro", "Pendent cobrament", "Outstanding sales"), string.Empty, stats.SalesOutstandingAmount);
            AppendMetric(builder, L(language, "Ventas", "Vendes", "Sales"), L(language, "Ticket medio factura", "Ticket mitjà factura", "Average invoice value"), string.Empty, stats.AverageSalesInvoiceAmount);
            AppendPercentMetric(builder, L(language, "Ventas", "Vendes", "Sales"), L(language, "Concentración cliente principal", "Concentració client principal", "Top client concentration"), stats.SalesTopClientSharePercent);
            AppendPercentMetric(builder, L(language, "Ventas", "Vendes", "Sales"), L(language, "Concentración top 5 clientes", "Concentració top 5 clients", "Top 5 client concentration"), stats.SalesTop5ClientsSharePercent);
            AppendPercentMetric(builder, L(language, "Riesgo", "Risc", "Risk"), L(language, "Riesgo cliente principal", "Risc client principal", "Top client risk"), stats.SalesOutstandingTopClientSharePercent);
            AppendPercentMetric(builder, L(language, "Riesgo", "Risc", "Risk"), L(language, "Riesgo top 5 clientes", "Risc top 5 clients", "Top 5 client risk"), stats.SalesOutstandingTop5ClientsSharePercent);
            AppendBreakdown(builder, language, L(language, "Top clientes", "Top clients", "Top clients"), stats.TopClients);
            AppendBreakdown(builder, language, L(language, "Estados de venta", "Estats de venda", "Sales statuses"), stats.SalesStatusBreakdown);
            AppendBreakdown(builder, language, L(language, "Top artículos facturados", "Top articles facturats", "Top billed items"), stats.TopBilledItems);
            AppendBreakdown(builder, language, L(language, "Aging de cobro", "Aging de cobrament", "Receivables aging"), stats.SalesAgingBuckets);
            AppendDistribution(builder, L(language, "Mix por cliente", "Mix per client", "Customer mix"), stats.SalesCustomerMix);
            AppendDistribution(builder, L(language, "Mix por artículo", "Mix per article", "Item mix"), stats.SalesItemMix);
            AppendDistribution(builder, L(language, "Riesgo comercial por cliente", "Risc comercial per client", "Commercial risk by client"), stats.SalesOutstandingRiskByClient);
            AppendComparisons(builder, language, stats.PeriodComparisons.Where(item => item.Label is "Facturación ventas" or "Pendiente cobro").ToArray());
            AppendAreaTimeline(builder, language, normalizedArea, stats.WeeklyTimeline);
        }

        if (string.IsNullOrWhiteSpace(normalizedArea) || string.Equals(normalizedArea, "purchases", StringComparison.Ordinal))
        {
            AppendMetric(builder, L(language, "Compras", "Compres", "Purchases"), L(language, "Pedidos", "Comandes", "Orders"), stats.PurchaseOrderCount.ToString(), stats.PurchaseOrderAmount);
            AppendMetric(builder, L(language, "Compras", "Compres", "Purchases"), L(language, "Ticket medio pedido", "Ticket mitjà comanda", "Average order value"), string.Empty, stats.AveragePurchaseOrderAmount);
            AppendMetric(builder, L(language, "Compras", "Compres", "Purchases"), L(language, "Recepciones", "Recepcions", "Receipts"), stats.PurchaseReceiptCount.ToString(), stats.PurchaseReceivedQuantity);
            AppendMetric(builder, L(language, "Compras", "Compres", "Purchases"), L(language, "Facturas proveedor", "Factures proveïdor", "Supplier invoices"), stats.PurchaseInvoiceCount.ToString(), stats.PurchaseInvoiceAmount);
            AppendMetric(builder, L(language, "Compras", "Compres", "Purchases"), L(language, "Facturado MTD", "Facturat MTD", "Invoiced MTD"), string.Empty, stats.PurchaseInvoiceAmountMonthToDate);
            AppendMetric(builder, L(language, "Compras", "Compres", "Purchases"), L(language, "Facturado YTD", "Facturat YTD", "Invoiced YTD"), string.Empty, stats.PurchaseInvoiceAmountYearToDate);
            AppendMetric(builder, L(language, "Compras", "Compres", "Purchases"), L(language, "Pagado", "Pagat", "Paid"), string.Empty, stats.PurchasePaidAmount);
            AppendMetric(builder, L(language, "Compras", "Compres", "Purchases"), L(language, "Pendiente pago", "Pendent pagament", "Outstanding purchases"), string.Empty, stats.PurchaseOutstandingAmount);
            AppendMetric(builder, L(language, "Compras", "Compres", "Purchases"), L(language, "Ticket medio factura", "Ticket mitjà factura", "Average invoice value"), string.Empty, stats.AveragePurchaseInvoiceAmount);
            AppendBreakdown(builder, language, L(language, "Top proveedores", "Top proveïdors", "Top suppliers"), stats.TopSuppliers);
            AppendBreakdown(builder, language, L(language, "Estados de compra", "Estats de compra", "Purchase statuses"), stats.PurchaseStatusBreakdown);
            AppendBreakdown(builder, language, L(language, "Aging de pago", "Aging de pagament", "Payables aging"), stats.PurchaseAgingBuckets);
            AppendGaps(builder, L(language, "Recepción vs factura", "Recepció vs factura", "Receipt vs invoice"), stats.PurchaseReceiptInvoiceGaps);
            AppendComparisons(builder, language, stats.PeriodComparisons.Where(item => item.Label is "Facturación compras" or "Pendiente pago").ToArray());
            AppendAreaTimeline(builder, language, normalizedArea, stats.WeeklyTimeline);
        }

        if (string.IsNullOrWhiteSpace(normalizedArea) || string.Equals(normalizedArea, "production", StringComparison.Ordinal))
        {
            AppendMetric(builder, L(language, "Operación", "Operació", "Operations"), L(language, "Órdenes vivas", "Ordres vives", "Live work orders"), stats.LiveFinishOrders.ToString(), 0m);
            AppendMetric(builder, L(language, "Operación", "Operació", "Operations"), L(language, "Balance neto ventas-compras", "Balanç net vendes-compres", "Net sales-purchases balance"), string.Empty, stats.NetBillingBalance);
            AppendBreakdown(builder, language, L(language, "Estados de fabricación", "Estats de fabricació", "Work order statuses"), stats.FinishStatusBreakdown);
            AppendBreakdown(builder, language, L(language, "Top acabadores", "Top acabadors", "Top finishers"), stats.TopFinishers);
            AppendBreakdown(builder, language, L(language, "Carga por acabador", "Càrrega per acabador", "Load by finisher"), stats.ProductionLoadByFinisher);
            AppendBreakdown(builder, language, L(language, "Carga semanal", "Càrrega setmanal", "Weekly load"), stats.ProductionLoadByWeek);
            AppendComparisons(builder, language, stats.PeriodComparisons.Where(item => item.Label is "Órdenes vivas").ToArray());
            AppendAreaTimeline(builder, language, normalizedArea, stats.WeeklyTimeline);
        }

        if (string.IsNullOrWhiteSpace(normalizedArea) || string.Equals(normalizedArea, "warehouse", StringComparison.Ordinal))
        {
            AppendMetric(builder, L(language, "Operación", "Operació", "Operations"), L(language, "Posiciones stock", "Posicions estoc", "Stock positions"), stats.StockPositions.ToString(), 0m);
            AppendMetric(builder, L(language, "Operación", "Operació", "Operations"), L(language, "Movimientos", "Moviments", "Movements"), stats.StockMovementsInRange.ToString(), 0m);
            AppendMetric(builder, L(language, "Operación", "Operació", "Operations"), L(language, "Inventarios borrador", "Inventaris esborrany", "Draft counts"), stats.DraftCounts.ToString(), 0m);
            AppendMetric(builder, L(language, "Operación", "Operació", "Operations"), L(language, "Traspasos borrador", "Traspassos esborrany", "Draft transfers"), stats.DraftTransfers.ToString(), 0m);
            AppendPercentMetric(builder, L(language, "Resumen ejecutivo", "Resum executiu", "Executive summary"), L(language, "Ratio cobro %", "Ràtio cobrament %", "Collection rate %"), stats.SalesCollectionRate);
            AppendPercentMetric(builder, L(language, "Resumen ejecutivo", "Resum executiu", "Executive summary"), L(language, "Ratio pago %", "Ràtio pagament %", "Payment rate %"), stats.PurchasePaymentRate);
            AppendBreakdown(builder, language, L(language, "Top almacenes", "Top magatzems", "Top warehouses"), stats.TopWarehouses);
            AppendBreakdown(builder, language, L(language, "Tipos de movimiento", "Tipus de moviment", "Movement types"), stats.MovementTypeBreakdown);
            AppendBreakdown(builder, language, L(language, "Rotación por almacén", "Rotació per magatzem", "Rotation by warehouse"), stats.WarehouseRotationByWarehouse);
            AppendBreakdown(builder, language, L(language, "Antigüedad de stock", "Antiguitat d'estoc", "Stock age"), stats.WarehouseStockAgeBuckets);
            AppendBreakdown(builder, language, L(language, "Cobertura de stock", "Cobertura d'estoc", "Stock coverage"), stats.WarehouseCoverageBuckets);
            AppendComparisons(builder, language, stats.PeriodComparisons.Where(item => item.Label is "Movimientos stock").ToArray());
            AppendAreaTimeline(builder, language, normalizedArea, stats.WeeklyTimeline);
        }

        return BuildExcelFriendlyCsv(builder);
    }

    private static void AppendMetric(StringBuilder builder, string block, string metric, string value, decimal amount) =>
        builder.AppendLine(string.Join(";",
            Escape(block),
            Escape(metric),
            Escape(value),
            Escape(amount == 0m ? string.Empty : amount.ToUiString("0.00")),
            string.Empty,
            string.Empty,
            string.Empty));

    private static void AppendPercentMetric(StringBuilder builder, string block, string metric, decimal percent) =>
        builder.AppendLine(string.Join(";",
            Escape(block),
            Escape(metric),
            string.Empty,
            string.Empty,
            Escape(percent == 0m ? string.Empty : percent.ToUiString("0.00")),
            string.Empty,
            string.Empty));

    private static void AppendBreakdown(
        StringBuilder builder,
        string language,
        string block,
        IReadOnlyCollection<StatisticBreakdownItemDto> items)
    {
        foreach (var item in items)
        {
            builder.AppendLine(string.Join(";",
                Escape(block),
                Escape(TranslateAnalyticsLabel(language, item.Label)),
                Escape(item.Count.ToString()),
                Escape(item.Amount == 0m ? string.Empty : item.Amount.ToUiString("0.00")),
                string.Empty,
                string.Empty,
                string.Empty));
        }
    }

    private static void AppendDistribution(
        StringBuilder builder,
        string block,
        IReadOnlyCollection<StatisticDistributionItemDto> items)
    {
        foreach (var item in items)
        {
            builder.AppendLine(string.Join(";",
                Escape(block),
                Escape(item.Label),
                Escape(item.Count.ToString()),
                Escape(item.Amount == 0m ? string.Empty : item.Amount.ToUiString("0.00")),
                Escape(item.SharePercent == 0m ? string.Empty : item.SharePercent.ToUiString("0.00")),
                string.Empty,
                string.Empty));
        }
    }

    private static void AppendGaps(
        StringBuilder builder,
        string block,
        IReadOnlyCollection<StatisticGapItemDto> items)
    {
        foreach (var item in items)
        {
            builder.AppendLine(string.Join(";",
                Escape(block),
                Escape(item.Label),
                Escape(item.ExpectedValue == 0m ? string.Empty : item.ExpectedValue.ToUiString("0.00")),
                string.Empty,
                string.Empty,
                Escape(item.ActualValue == 0m ? string.Empty : item.ActualValue.ToUiString("0.00")),
                Escape(item.GapValue == 0m ? string.Empty : item.GapValue.ToUiString("0.00"))));
        }
    }

    private static void AppendComparisons(
        StringBuilder builder,
        string language,
        IReadOnlyCollection<StatisticComparisonItemDto> items)
    {
        foreach (var item in items)
        {
            builder.AppendLine(string.Join(";",
                Escape(L(language, "Comparativa", "Comparativa", "Comparison")),
                Escape(item.Label),
                Escape(FormatValue(item.CurrentValue, item.ValueKind)),
                Escape(FormatValue(item.PreviousValue, item.ValueKind)),
                string.Empty,
                string.Empty,
                string.Empty));
            builder.AppendLine(string.Join(";",
                Escape(L(language, "Comparativa", "Comparativa", "Comparison")),
                Escape($"{item.Label} · {L(language, "Delta", "Delta", "Delta")}"),
                Escape(item.DeltaValue.ToUiString("0.00")),
                Escape($"{item.DeltaPercentage.ToUiString("0.##")}%"),
                string.Empty,
                string.Empty,
                string.Empty));
        }
    }

    private static void AppendTimeline(
        StringBuilder builder,
        string language,
        IReadOnlyCollection<StatisticTimelinePointDto> items)
    {
        foreach (var item in items)
        {
            builder.AppendLine(string.Join(";",
                Escape(L(language, "Evolución semanal", "Evolució setmanal", "Weekly timeline")),
                Escape(item.Label),
                Escape(item.StockMovementCount.ToString()),
                Escape(item.SalesInvoiceAmount.ToUiString("0.00")),
                string.Empty,
                string.Empty,
                string.Empty));
            builder.AppendLine(string.Join(";",
                Escape(L(language, "Evolución semanal", "Evolució setmanal", "Weekly timeline")),
                Escape($"{item.Label} · {L(language, "Compras", "Compres", "Purchases")}"),
                Escape(item.PurchaseInvoiceCount.ToString()),
                Escape(item.PurchaseInvoiceAmount.ToUiString("0.00")),
                string.Empty,
                string.Empty,
                string.Empty));
        }
    }

    private static void AppendAreaTimeline(
        StringBuilder builder,
        string language,
        string? area,
        IReadOnlyCollection<StatisticTimelinePointDto> items)
    {
        var normalizedArea = NormalizeArea(area);
        foreach (var item in items)
        {
            switch (normalizedArea)
            {
                case "sales":
                    builder.AppendLine(string.Join(";",
                        Escape(L(language, "Evolución semanal ventas", "Evolució setmanal vendes", "Weekly sales timeline")),
                        Escape(item.Label),
                        Escape(item.SalesInvoiceCount.ToUiString("0")),
                        Escape(item.SalesInvoiceAmount.ToUiString("0.00")),
                        string.Empty,
                        string.Empty,
                        string.Empty));
                    break;
                case "purchases":
                    builder.AppendLine(string.Join(";",
                        Escape(L(language, "Evolución semanal compras", "Evolució setmanal compres", "Weekly purchase timeline")),
                        Escape(item.Label),
                        Escape(item.PurchaseInvoiceCount.ToUiString("0")),
                        Escape(item.PurchaseInvoiceAmount.ToUiString("0.00")),
                        string.Empty,
                        string.Empty,
                        string.Empty));
                    break;
                case "production":
                    builder.AppendLine(string.Join(";",
                        Escape(L(language, "Evolución semanal producción", "Evolució setmanal producció", "Weekly production timeline")),
                        Escape(item.Label),
                        Escape(item.FinishOrderCount.ToUiString("0")),
                        Escape(item.StockMovementCount.ToUiString("0")),
                        string.Empty,
                        string.Empty,
                        string.Empty));
                    break;
                case "warehouse":
                    builder.AppendLine(string.Join(";",
                        Escape(L(language, "Evolución semanal almacén", "Evolució setmanal magatzem", "Weekly warehouse timeline")),
                        Escape(item.Label),
                        Escape(item.StockMovementCount.ToUiString("0")),
                        Escape(item.FinishOrderCount.ToUiString("0")),
                        string.Empty,
                        string.Empty,
                        string.Empty));
                    break;
                default:
                    AppendTimeline(builder, language, [item]);
                    break;
            }
        }
    }

    private static string FormatValue(decimal value, string valueKind) =>
        valueKind switch
        {
            "currency" => value.ToUiString("0.00"),
            "count" => value.ToUiString("0"),
            _ => value.ToUiString("0.##")
        };

    private static byte[] BuildExcelFriendlyCsv(StringBuilder builder)
    {
        var payload = builder.ToString();
        var bom = Encoding.UTF8.GetPreamble();
        var data = Encoding.UTF8.GetBytes(payload);
        var result = new byte[bom.Length + data.Length];
        Buffer.BlockCopy(bom, 0, result, 0, bom.Length);
        Buffer.BlockCopy(data, 0, result, bom.Length, data.Length);
        return result;
    }

    private static string Escape(string value)
    {
        var safe = value ?? string.Empty;
        if (!safe.Contains(';') && !safe.Contains('"') && !safe.Contains('\n') && !safe.Contains('\r'))
        {
            return safe;
        }

        return $"\"{safe.Replace("\"", "\"\"")}\"";
    }

    private static string L(string language, string spanish, string catalan, string english) =>
        AppLanguages.Normalize(language) switch
        {
            AppLanguages.Catalan => catalan,
            AppLanguages.English => english,
            _ => spanish
        };

    private static string NormalizeArea(string? area) =>
        (area ?? string.Empty).Trim().ToLowerInvariant() switch
        {
            "sales" => "sales",
            "purchases" => "purchases",
            "production" => "production",
            "warehouse" => "warehouse",
            _ => string.Empty
        };

    private static string TranslateAnalyticsLabel(string language, string label) =>
        label switch
        {
            "aging_current" => L(language, "Al día", "Al dia", "Current"),
            "aging_1_30" => L(language, "1-30 días", "1-30 dies", "1-30 days"),
            "aging_31_60" => L(language, "31-60 días", "31-60 dies", "31-60 days"),
            "aging_61_90" => L(language, "61-90 días", "61-90 dies", "61-90 days"),
            "aging_90_plus" => L(language, "90+ días", "90+ dies", "90+ days"),
            "stock_age_0_30" => L(language, "0-30 días", "0-30 dies", "0-30 days"),
            "stock_age_31_90" => L(language, "31-90 días", "31-90 dies", "31-90 days"),
            "stock_age_91_180" => L(language, "91-180 días", "91-180 dies", "91-180 days"),
            "stock_age_180_plus" => L(language, "180+ días", "180+ dies", "180+ days"),
            "coverage_none" => L(language, "Sin salida", "Sense sortida", "No outbound"),
            "coverage_0_30" => L(language, "0-30 días", "0-30 dies", "0-30 days"),
            "coverage_31_90" => L(language, "31-90 días", "31-90 dies", "31-90 days"),
            "coverage_90_plus" => L(language, "90+ días", "90+ dies", "90+ days"),
            _ => label
        };
}
