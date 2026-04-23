namespace Erp.Application.Mailing;

public sealed class MailingCampaignDetailDto
{
    public Guid CampaignId { get; set; }
    public string SourceType { get; set; } = MailingSourceTypes.Clients;
    public string Title { get; set; } = string.Empty;
    public string BodyText { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public bool IncludeAllRecipients { get; set; }
    public DateTime CreatedUtc { get; set; }
    public DateTime UpdatedUtc { get; set; }
    public IReadOnlyList<MailingRecipientDto> Recipients { get; set; } = [];
}
