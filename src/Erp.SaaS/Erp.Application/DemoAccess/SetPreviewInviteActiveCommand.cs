namespace Erp.Application.DemoAccess;

public sealed class SetPreviewInviteActiveCommand
{
    public Guid InviteId { get; set; }
    public bool IsActive { get; set; }
    public Guid? UpdatedByUserId { get; set; }
}
