namespace Erp.Application.Suppliers;

public sealed class ProveedorSearchResultDto
{
    public IReadOnlyCollection<ProveedorListItemDto> Items { get; set; } = [];
    public int TotalCount { get; set; }
}
