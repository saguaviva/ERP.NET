namespace Erp.Application.DemoAccess;

public sealed class CreatePreviewInvitesFromRequestCommand
{
    public Guid RequestId { get; set; }
    public Guid? CreatedByUserId { get; set; }
}
