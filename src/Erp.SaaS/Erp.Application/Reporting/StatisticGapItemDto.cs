namespace Erp.Application.Reporting;

public sealed class StatisticGapItemDto
{
    public string Label { get; set; } = string.Empty;
    public decimal ExpectedValue { get; set; }
    public decimal ActualValue { get; set; }
    public decimal GapValue { get; set; }
}
