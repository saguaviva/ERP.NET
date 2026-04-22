namespace Erp.Application.Intrastat;

public sealed class IntrastatCountrySummaryDto
{
    public string CountryCode { get; set; } = string.Empty;
    public string CountryName { get; set; } = string.Empty;
    public string IntrastatCode { get; set; } = string.Empty;
    public int LinesCount { get; set; }
    public decimal TotalQuantity { get; set; }
    public decimal TotalNetAmount { get; set; }
    public decimal TotalGrossAmount { get; set; }
}
