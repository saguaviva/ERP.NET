namespace Erp.Application.Tenants;

public sealed class LegacyCenterOptionDto
{
    public string Code { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public bool IsAssigned { get; init; }
    public string AssignedCompanyName { get; init; } = string.Empty;
}
