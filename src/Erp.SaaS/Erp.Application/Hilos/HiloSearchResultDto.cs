namespace Erp.Application.Hilos;

public sealed class HiloSearchResultDto
{
    public IReadOnlyCollection<HiloListItemDto> Items { get; init; } = [];
    public int TotalCount { get; init; }
}
