namespace Erp.Application.Clients;

public sealed class ClienteFilter
{
    public string Search { get; set; } = string.Empty;
    public bool IncludeBlocked { get; set; }
    public int Limit { get; set; } = 100;
}
