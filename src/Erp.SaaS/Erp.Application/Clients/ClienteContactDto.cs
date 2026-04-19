namespace Erp.Application.Clients;

public sealed class ClienteContactDto
{
    public Guid? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public bool IsPrimary { get; set; }
}
