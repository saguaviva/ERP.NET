using System.Text;
using Erp.Application.Intrastat;

namespace Erp.App.Components.Pages;

internal static class IntrastatCsvExporter
{
    public static byte[] BuildDetailCsv(IntrastatReportDto report)
    {
        var builder = new StringBuilder();
        builder.AppendLine("sep=;");
        builder.AppendLine("Factura;Fecha;Cliente;VAT;País;Descripción;Composición;Talla;Artículo;Cantidad;Pes unit.;Pes total/Kgrs;Import;NC Code;Dte?;Transport?;Origen");

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
                Escape(item.Quantity.ToString("0.###")),
                Escape(item.UnitWeight.ToString("0.###")),
                Escape(item.TotalWeightKg.ToString("0.00")),
                Escape(item.NetAmount.ToString("0.00")),
                Escape(item.IntrastatCode),
                Escape(item.DiscountAmount.ToString("0.00")),
                Escape(item.IsTransportCharge ? "si" : "no"),
                Escape(item.Origin)));
        }

        return BuildExcelFriendlyCsv(builder);
    }

    public static byte[] BuildSummaryCsv(IntrastatReportDto report)
    {
        var builder = new StringBuilder();
        builder.AppendLine("sep=;");
        var headers = new List<string>
        {
            "Clients",
            "Ordre alfabètic",
            "Sigles"
        };

        headers.AddRange(report.MatrixNcCodes.Select(code => $"Kg {code}"));
        headers.Add("TOTAL PESOS/Kgrs");
        headers.AddRange(report.MatrixNcCodes.Select(code => $"Factura {code}"));
        headers.Add("TOTAL FACTURES€");
        builder.AppendLine(string.Join(";", headers));

        foreach (var item in report.MatrixRows)
        {
            var values = new List<string>
            {
                Escape(item.HasClients ? "si" : "no"),
                Escape(item.CountryName),
                Escape(item.CountryCode)
            };

            values.AddRange(report.MatrixNcCodes.Select(code =>
                Escape((item.WeightByNcCode.TryGetValue(code, out var value) ? value : 0m).ToString("0.00"))));
            values.Add(Escape(item.TotalWeightKg.ToString("0.00")));
            values.AddRange(report.MatrixNcCodes.Select(code =>
                Escape((item.AmountByNcCode.TryGetValue(code, out var value) ? value : 0m).ToString("0.00"))));
            values.Add(Escape(item.TotalInvoiceAmount.ToString("0.00")));

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
}
