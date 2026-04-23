namespace Erp.Application.Reporting;

public sealed class OperationalDocumentSearchResultDto
{
    public IReadOnlyCollection<OperationalDocumentListItemDto> Items { get; set; } = [];
    public int TotalCount { get; set; }
}
