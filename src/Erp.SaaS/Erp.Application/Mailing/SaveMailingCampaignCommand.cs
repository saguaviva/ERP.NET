namespace Erp.Application.Mailing;

public sealed class SaveMailingCampaignCommand
{
    public Guid TenantId { get; set; }
    public Guid CompanyId { get; set; }
    public Guid? CampaignId { get; set; }
    public string SourceType { get; set; } = MailingSourceTypes.Clients;
    public string Title { get; set; } = string.Empty;
    public string BodyText { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public bool IncludeAllRecipients { get; set; }
    public IReadOnlyList<int> RecipientCodes { get; set; } = [];
}
