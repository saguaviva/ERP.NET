namespace Erp.Application.Acabados;

public sealed class ParteAcabadoSearchResultDto
{
    public IReadOnlyCollection<ParteAcabadoListItemDto> Items { get; set; } = [];
    public int TotalCount { get; set; }
}
