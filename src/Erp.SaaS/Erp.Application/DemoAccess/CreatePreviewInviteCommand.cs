namespace Erp.Application.DemoAccess;

public sealed class CreatePreviewInviteCommand
{
    public string Email { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public Guid? SourceRequestId { get; set; }
    public Guid? CreatedByUserId { get; set; }
}
