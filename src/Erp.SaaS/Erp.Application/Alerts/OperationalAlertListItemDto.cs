namespace Erp.Application.Alerts;

public sealed class OperationalAlertListItemDto
{
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string Detail { get; set; } = string.Empty;
    public DateTime? ReferenceDate { get; set; }
    public int AgeDays { get; set; }
    public decimal? MetricValue { get; set; }
    public string MetricKind { get; set; } = string.Empty;
    public string Route { get; set; } = string.Empty;
}
