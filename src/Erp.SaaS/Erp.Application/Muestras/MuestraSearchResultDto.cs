namespace Erp.Application.Muestras;

public sealed class MuestraSearchResultDto
{
    public IReadOnlyCollection<MuestraListItemDto> Items { get; set; } = [];
    public int TotalCount { get; set; }
}
