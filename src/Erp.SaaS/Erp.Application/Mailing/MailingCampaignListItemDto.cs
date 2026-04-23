namespace Erp.Application.Mailing;

public sealed class MailingCampaignListItemDto
{
    public Guid CampaignId { get; set; }
    public string SourceType { get; set; } = MailingSourceTypes.Clients;
    public string Title { get; set; } = string.Empty;
    public bool IncludeAllRecipients { get; set; }
    public int RecipientCount { get; set; }
    public DateTime CreatedUtc { get; set; }
    public DateTime UpdatedUtc { get; set; }
}
