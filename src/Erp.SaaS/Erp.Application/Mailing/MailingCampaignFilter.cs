namespace Erp.Application.Mailing;

public sealed class MailingCampaignFilter
{
    public string Search { get; set; } = string.Empty;
    public string SourceType { get; set; } = string.Empty;
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 25;
}
