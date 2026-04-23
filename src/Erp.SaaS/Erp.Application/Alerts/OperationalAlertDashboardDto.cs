namespace Erp.Application.Alerts;

public sealed class OperationalAlertDashboardDto
{
    public int TotalAlerts { get; set; }
    public int CriticalAlerts { get; set; }
    public int WarningAlerts { get; set; }
    public int InfoAlerts { get; set; }
    public int ActiveGroups { get; set; }
    public DateTime GeneratedUtc { get; set; } = DateTime.UtcNow;
    public IReadOnlyCollection<OperationalAlertGroupDto> Groups { get; set; } = [];
}
