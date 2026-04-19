namespace Erp.Application.Tenants;

public sealed class ResetUserPasswordCommand
{
    public Guid UserId { get; set; }
    public string NewPassword { get; set; } = string.Empty;
}
