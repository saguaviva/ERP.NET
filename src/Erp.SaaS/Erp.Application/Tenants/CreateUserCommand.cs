namespace Erp.Application.Tenants;

public sealed class CreateUserCommand
{
    public Guid TenantId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public IReadOnlyCollection<Guid> CompanyIds { get; set; } = [];
    public IReadOnlyCollection<string> Roles { get; set; } = [];
}
