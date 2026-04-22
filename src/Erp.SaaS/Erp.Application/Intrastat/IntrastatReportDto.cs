namespace Erp.Application.Intrastat;

public sealed class IntrastatReportDto
{
    public IReadOnlyCollection<IntrastatLineDto> Items { get; set; } = [];
    public IReadOnlyCollection<IntrastatCountrySummaryDto> Summary { get; set; } = [];
    public IReadOnlyCollection<IntrastatMatrixRowDto> MatrixRows { get; set; } = [];
    public IReadOnlyCollection<string> MatrixNcCodes { get; set; } = [];
    public int TotalCount { get; set; }
    public int CountriesCount { get; set; }
    public int ClassifiedLinesCount { get; set; }
    public int UnclassifiedLinesCount { get; set; }
    public decimal TransportAmount { get; set; }
    public decimal SalesAmount { get; set; }
    public decimal TotalWeightKg { get; set; }
    public decimal TotalWithTransportAmount { get; set; }
    public decimal TotalNetAmount { get; set; }
    public decimal TotalGrossAmount { get; set; }
}
