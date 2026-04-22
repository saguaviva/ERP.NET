namespace Erp.Application.Models;

public sealed class ModeloSearchResultDto
{
    public IReadOnlyCollection<ModeloListItemDto> Items { get; set; } = [];
    public int TotalCount { get; set; }
}
