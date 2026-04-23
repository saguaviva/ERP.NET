namespace Erp.Application.Mailing;

public sealed class MailingCampaignSearchResultDto
{
    public IReadOnlyList<MailingCampaignListItemDto> Items { get; set; } = [];
    public int TotalCount { get; set; }
}
