namespace Erp.Application.Mailing;

public sealed class MailingRecipientFilter
{
    public string SourceType { get; set; } = MailingSourceTypes.Clients;
    public string Search { get; set; } = string.Empty;
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 50;
}
