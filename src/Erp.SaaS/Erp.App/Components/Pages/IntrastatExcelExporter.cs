using ClosedXML.Excel;
using Erp.Application.Intrastat;

namespace Erp.App.Components.Pages;

internal static class IntrastatExcelExporter
{
    public static byte[] BuildWorkbook(IntrastatReportDto report, int year, int month)
    {
        using var workbook = new XLWorkbook();
        BuildDetailSheet(workbook, report, year, month);
        BuildSummarySheet(workbook, report, year, month);

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    private static void BuildDetailSheet(XLWorkbook workbook, IntrastatReportDto report, int year, int month)
    {
        var sheet = workbook.Worksheets.Add("Factures");
        var title = $"Intrastat detalle - {new DateTime(year, month, 1):MMMM yyyy}";

        sheet.Cell("A1").Value = title;
        sheet.Range("A1:Q1").Merge();
        sheet.Cell("A1").Style.Font.Bold = true;
        sheet.Cell("A1").Style.Font.FontSize = 16;
        sheet.Cell("A1").Style.Fill.BackgroundColor = XLColor.FromHtml("#8E3E2E");
        sheet.Cell("A1").Style.Font.FontColor = XLColor.White;
        sheet.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

        sheet.Cell("A2").Value = "Kg total";
        sheet.Cell("B2").Value = report.TotalWeightKg;
        sheet.Cell("C2").Value = "Import venda";
        sheet.Cell("D2").Value = report.SalesAmount;
        sheet.Cell("E2").Value = "Transport";
        sheet.Cell("F2").Value = report.TransportAmount;
        sheet.Cell("G2").Value = "Total amb transport";
        sheet.Cell("H2").Value = report.TotalWithTransportAmount;

        var headers = new[]
        {
            "Factura", "Fecha", "Cliente", "VAT", "País", "Descripción", "Composición", "Talla",
            "Artículo", "Cantidad", "Pes unit.", "Pes total/Kgrs", "Import", "NC code",
            "Dte?", "Transport?", "Origen"
        };

        for (var index = 0; index < headers.Length; index++)
        {
            var cell = sheet.Cell(4, index + 1);
            cell.Value = headers[index];
            ApplyHeaderStyle(cell);
        }

        var rowNumber = 5;
        foreach (var item in report.Items)
        {
            sheet.Cell(rowNumber, 1).Value = item.DisplayInvoiceNumber;
            sheet.Cell(rowNumber, 2).Value = item.IssueDate;
            sheet.Cell(rowNumber, 3).Value = item.ClientName;
            sheet.Cell(rowNumber, 4).Value = item.ClientTaxId;
            sheet.Cell(rowNumber, 5).Value = item.CountryCode;
            sheet.Cell(rowNumber, 6).Value = item.Description;
            sheet.Cell(rowNumber, 7).Value = item.Composition;
            sheet.Cell(rowNumber, 8).Value = item.Size;
            sheet.Cell(rowNumber, 9).Value = item.ItemCode;
            sheet.Cell(rowNumber, 10).Value = item.Quantity;
            sheet.Cell(rowNumber, 11).Value = item.UnitWeight;
            sheet.Cell(rowNumber, 12).Value = item.TotalWeightKg;
            sheet.Cell(rowNumber, 13).Value = item.NetAmount;
            sheet.Cell(rowNumber, 14).Value = item.IntrastatCode;
            sheet.Cell(rowNumber, 15).Value = item.DiscountAmount;
            sheet.Cell(rowNumber, 16).Value = item.IsTransportCharge ? "si" : "no";
            sheet.Cell(rowNumber, 17).Value = item.Origin;

            if (item.IsTransportCharge)
            {
                sheet.Range(rowNumber, 1, rowNumber, 17).Style.Fill.BackgroundColor = XLColor.FromHtml("#FFF1C7");
            }
            else if (!item.IsClassified)
            {
                sheet.Range(rowNumber, 1, rowNumber, 17).Style.Fill.BackgroundColor = XLColor.FromHtml("#FDE7D9");
            }

            rowNumber++;
        }

        sheet.Column(2).Style.DateFormat.Format = "dd/MM/yyyy";
        for (var col = 10; col <= 13; col++)
        {
            sheet.Column(col).Style.NumberFormat.Format = "#,##0.00";
        }
        sheet.Column(15).Style.NumberFormat.Format = "#,##0.00";

        sheet.SheetView.FreezeRows(4);
        sheet.Range(4, 1, Math.Max(rowNumber - 1, 4), 17).SetAutoFilter();
        sheet.Columns().AdjustToContents();
        sheet.Column(6).Width = Math.Max(sheet.Column(6).Width, 28);
        sheet.Column(7).Width = Math.Max(sheet.Column(7).Width, 20);
        sheet.Column(3).Width = Math.Max(sheet.Column(3).Width, 22);
        sheet.Column(9).Width = Math.Max(sheet.Column(9).Width, 14);
    }

    private static void BuildSummarySheet(XLWorkbook workbook, IntrastatReportDto report, int year, int month)
    {
        var sheet = workbook.Worksheets.Add("Agrupacio");
        var title = $"Intrastat agrupació - {new DateTime(year, month, 1):MMMM yyyy}";
        var matrixNcCodes = report.MatrixNcCodes.ToArray();

        sheet.Cell("A1").Value = title;
        sheet.Range("A1:Q1").Merge();
        sheet.Cell("A1").Style.Font.Bold = true;
        sheet.Cell("A1").Style.Font.FontSize = 16;
        sheet.Cell("A1").Style.Fill.BackgroundColor = XLColor.FromHtml("#8E3E2E");
        sheet.Cell("A1").Style.Font.FontColor = XLColor.White;
        sheet.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

        sheet.Cell("D3").Value = "Codis arancelaris · PESOS/Kgrs";
        sheet.Range("D3:I3").Merge();
        sheet.Cell("K3").Value = "Codis arancelaris · FACTURES€";
        sheet.Range("K3:P3").Merge();

        ApplyGroupHeaderStyle(sheet.Range("D3:I3"));
        ApplyGroupHeaderStyle(sheet.Range("K3:P3"));

        sheet.Cell("A4").Value = "Clients";
        sheet.Cell("B4").Value = "Ordre alfabètic";
        sheet.Cell("C4").Value = "Sigles";

        for (var index = 0; index < matrixNcCodes.Length; index++)
        {
            sheet.Cell(4, 4 + index).Value = matrixNcCodes[index];
            sheet.Cell(4, 11 + index).Value = matrixNcCodes[index];
        }

        sheet.Cell("J4").Value = "TOTAL PESOS/Kgrs";
        sheet.Cell("Q4").Value = "TOTAL FACTURES€";

        sheet.Range("A4:Q4").Style.Font.Bold = true;
        sheet.Range("A4:Q4").Style.Fill.BackgroundColor = XLColor.FromHtml("#D9E2F3");
        sheet.Range("A4:Q4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

        var rowNumber = 5;
        foreach (var row in report.MatrixRows)
        {
            sheet.Cell(rowNumber, 1).Value = row.HasClients ? "si" : "no";
            sheet.Cell(rowNumber, 2).Value = row.CountryName;
            sheet.Cell(rowNumber, 3).Value = row.CountryCode;

            for (var index = 0; index < matrixNcCodes.Length; index++)
            {
                var code = matrixNcCodes[index];
                sheet.Cell(rowNumber, 4 + index).Value = row.WeightByNcCode.TryGetValue(code, out var weight) ? weight : 0m;
                sheet.Cell(rowNumber, 11 + index).Value = row.AmountByNcCode.TryGetValue(code, out var amount) ? amount : 0m;
            }

            sheet.Cell(rowNumber, 10).Value = row.TotalWeightKg;
            sheet.Cell(rowNumber, 17).Value = row.TotalInvoiceAmount;

            if (row.IsDomesticReference)
            {
                sheet.Range(rowNumber, 1, rowNumber, 17).Style.Fill.BackgroundColor = XLColor.FromHtml("#FDE7D9");
            }
            else if (rowNumber % 2 == 0)
            {
                sheet.Range(rowNumber, 1, rowNumber, 17).Style.Fill.BackgroundColor = XLColor.FromHtml("#F7F7F7");
            }

            rowNumber++;
        }

        sheet.Range(4, 1, Math.Max(rowNumber - 1, 4), 17).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        sheet.Range(4, 1, Math.Max(rowNumber - 1, 4), 17).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
        sheet.Range(5, 4, Math.Max(rowNumber - 1, 5), 17).Style.NumberFormat.Format = "#,##0.00";
        sheet.SheetView.FreezeRows(4);
        sheet.Columns().AdjustToContents();
        sheet.Column(2).Width = Math.Max(sheet.Column(2).Width, 24);
    }

    private static void ApplyHeaderStyle(IXLCell cell)
    {
        cell.Style.Font.Bold = true;
        cell.Style.Fill.BackgroundColor = XLColor.FromHtml("#D9E2F3");
        cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
    }

    private static void ApplyGroupHeaderStyle(IXLRange range)
    {
        range.Style.Font.Bold = true;
        range.Style.Fill.BackgroundColor = XLColor.FromHtml("#B4C6E7");
        range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
    }
}
