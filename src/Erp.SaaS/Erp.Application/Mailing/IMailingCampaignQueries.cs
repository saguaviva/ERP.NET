namespace Erp.Application.Mailing;

public interface IMailingCampaignQueries
{
    Task<MailingCampaignSearchResultDto> SearchAsync(Guid tenantId, Guid companyId, MailingCampaignFilter filter, CancellationToken cancellationToken = default);
    Task<MailingCampaignDetailDto?> GetByIdAsync(Guid tenantId, Guid companyId, Guid campaignId, CancellationToken cancellationToken = default);
    Task<MailingRecipientSearchResultDto> SearchRecipientsAsync(Guid tenantId, Guid companyId, MailingRecipientFilter filter, CancellationToken cancellationToken = default);
}
