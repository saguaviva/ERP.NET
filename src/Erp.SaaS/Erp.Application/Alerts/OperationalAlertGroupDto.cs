namespace Erp.Application.Alerts;

public sealed class OperationalAlertGroupDto
{
    public string Key { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public int TotalCount { get; set; }
    public string DefaultRoute { get; set; } = string.Empty;
    public IReadOnlyCollection<OperationalAlertListItemDto> Items { get; set; } = [];
}
