namespace Erp.Application.Clients;

public sealed class ClienteSearchResultDto
{
    public IReadOnlyCollection<ClienteListItemDto> Items { get; set; } = [];
    public int TotalCount { get; set; }
}
