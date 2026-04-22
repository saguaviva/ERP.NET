namespace Erp.Application.Tejidos;

public sealed class TejidoSearchResultDto
{
    public IReadOnlyCollection<TejidoListItemDto> Items { get; set; } = [];
    public int TotalCount { get; set; }
}
