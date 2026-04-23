namespace Erp.Application.Reporting;

public sealed class OperationalDocumentListItemDto
{
    public string Category { get; set; } = string.Empty;
    public string TypeKey { get; set; } = string.Empty;
    public string TypeLabel { get; set; } = string.Empty;
    public int DocumentNumber { get; set; }
    public string DocumentDisplay { get; set; } = string.Empty;
    public DateTime DocumentDate { get; set; }
    public string PartyName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Route { get; set; } = string.Empty;
}
