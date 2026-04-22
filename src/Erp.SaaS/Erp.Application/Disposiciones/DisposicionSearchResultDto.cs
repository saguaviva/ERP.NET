namespace Erp.Application.Disposiciones;

public sealed class DisposicionSearchResultDto
{
    public IReadOnlyCollection<DisposicionListItemDto> Items { get; set; } = [];
    public int TotalCount { get; set; }
}
