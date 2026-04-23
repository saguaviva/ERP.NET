namespace Erp.Application.Reporting;

public sealed class StatisticDistributionItemDto
{
    public string Label { get; set; } = string.Empty;
    public int Count { get; set; }
    public decimal Amount { get; set; }
    public decimal SharePercent { get; set; }
}
