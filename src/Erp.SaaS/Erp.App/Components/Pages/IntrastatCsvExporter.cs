using System.Text;
using Erp.Application.Intrastat;
using Erp.App.Localization;

namespace Erp.App.Components.Pages;

internal static class IntrastatCsvExporter
{
    public static byte[] BuildDetailCsv(IntrastatReportDto report, string language)
    {
        var builder = new StringBuilder();
        builder.AppendLine("sep=;");
        builder.AppendLine(string.Join(";",
            L(language, "Factura", "Factura", "Invoice"),
            L(language, "Fecha", "Data", "Date"),
            L(language, "Cliente", "Client", "Client"),
            "VAT",
            L(language, "País", "País", "Country"),
            L(language, "Descripción", "Descripció", "Description"),
            L(language, "Composición", "Composició", "Composition"),
            L(language, "Talla", "Talla", "Size"),
            L(language, "Artículo", "Article", "Item"),
            L(language, "Cantidad", "Quantitat", "Quantity"),
            L(language, "Peso unit.", "Pes unit.", "Unit weight"),
            L(language, "Peso total/Kg", "Pes total/Kg", "Total weight/Kg"),
            L(language, "Importe", "Import", "Amount"),
            L(language, "Código NC", "Codi NC", "CN code"),
            L(language, "Dte.?", "Dte.?", "Discount?"),
            L(language, "Línea transporte?", "Línia transport?", "Transport line?"),
            L(language, "Origen", "Origen", "Source")));

        foreach (var item in report.Items)
        {
            builder.AppendLine(string.Join(";",
                Escape(item.DisplayInvoiceNumber),
                Escape(item.IssueDate.ToString("dd/MM/yyyy")),
                Escape(item.ClientName),
                Escape(item.ClientTaxId),
                Escape(item.CountryCode),
                Escape(item.Description),
                Escape(item.Composition),
                Escape(item.Size),
                Escape(item.ItemCode),
                Escape(item.Quantity.ToUiString("0.###")),
                Escape(item.UnitWeight.ToUiString("0.###")),
                Escape(item.TotalWeightKg.ToUiString("0.00")),
                Escape(item.NetAmount.ToUiString("0.00")),
                Escape(item.IntrastatCode),
                Escape(item.DiscountAmount.ToUiString("0.00")),
                Escape(item.IsTransportCharge ? L(language, "si", "sí", "yes") : L(language, "no", "no", "no")),
                Escape(item.Origin)));
        }

        return BuildExcelFriendlyCsv(builder);
    }

    public static byte[] BuildSummaryCsv(IntrastatReportDto report, string language)
    {
        var builder = new StringBuilder();
        builder.AppendLine("sep=;");
        var headers = new List<string>
        {
            L(language, "Con clientes", "Amb clients", "Has clients"),
            L(language, "Orden alfabético", "Ordre alfabètic", "Alphabetical order"),
            L(language, "Siglas", "Sigles", "Country code")
        };

        headers.AddRange(report.MatrixNcCodes.Select(code => $"{L(language, "Kg", "Kg", "Kg")} {code}"));
        headers.Add(L(language, "TOTAL PESOS/Kgrs", "TOTAL PESOS/Kgrs", "TOTAL WEIGHTS/Kgs"));
        headers.AddRange(report.MatrixNcCodes.Select(code => $"{L(language, "Importe", "Import", "Amount")} {code}"));
        headers.Add(L(language, "TOTAL FACTURAS€", "TOTAL FACTURES€", "TOTAL INVOICES€"));
        builder.AppendLine(string.Join(";", headers));

        foreach (var item in report.MatrixRows)
        {
            var values = new List<string>
            {
                Escape(item.HasClients ? L(language, "si", "sí", "yes") : L(language, "no", "no", "no")),
                Escape(item.CountryName),
                Escape(item.CountryCode)
            };

            values.AddRange(report.MatrixNcCodes.Select(code =>
                Escape((item.WeightByNcCode.TryGetValue(code, out var value) ? value : 0m).ToUiString("0.00"))));
            values.Add(Escape(item.TotalWeightKg.ToUiString("0.00")));
            values.AddRange(report.MatrixNcCodes.Select(code =>
                Escape((item.AmountByNcCode.TryGetValue(code, out var value) ? value : 0m).ToUiString("0.00"))));
            values.Add(Escape(item.TotalInvoiceAmount.ToUiString("0.00")));

            builder.AppendLine(string.Join(";", values));
        }

        return BuildExcelFriendlyCsv(builder);
    }

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
}
