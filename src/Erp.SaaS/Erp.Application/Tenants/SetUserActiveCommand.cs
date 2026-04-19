namespace Erp.Application.Tenants;

public sealed class SetUserActiveCommand
{
    public Guid UserId { get; set; }
    public bool IsActive { get; set; }
}
