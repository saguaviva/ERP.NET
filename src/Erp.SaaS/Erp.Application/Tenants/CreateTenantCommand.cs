namespace Erp.Application.Tenants;

public sealed class CreateTenantCommand
{
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
}
