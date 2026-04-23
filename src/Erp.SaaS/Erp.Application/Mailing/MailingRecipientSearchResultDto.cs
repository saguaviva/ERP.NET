namespace Erp.Application.Mailing;

public sealed class MailingRecipientSearchResultDto
{
    public IReadOnlyList<MailingRecipientDto> Items { get; set; } = [];
    public int TotalCount { get; set; }
}
