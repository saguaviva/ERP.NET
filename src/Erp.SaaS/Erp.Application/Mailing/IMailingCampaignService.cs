namespace Erp.Application.Mailing;

public interface IMailingCampaignService
{
    Task<Guid> SaveAsync(SaveMailingCampaignCommand command, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid tenantId, Guid companyId, Guid campaignId, CancellationToken cancellationToken = default);
}
