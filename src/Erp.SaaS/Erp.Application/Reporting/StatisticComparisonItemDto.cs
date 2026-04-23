namespace Erp.Application.Reporting;

public sealed class StatisticComparisonItemDto
{
    public string Label { get; set; } = string.Empty;
    public decimal CurrentValue { get; set; }
    public decimal PreviousValue { get; set; }
    public decimal DeltaValue { get; set; }
    public decimal DeltaPercentage { get; set; }
    public string ValueKind { get; set; } = "number";
}
