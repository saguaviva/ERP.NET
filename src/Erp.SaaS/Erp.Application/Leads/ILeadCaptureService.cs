namespace Erp.Application.Leads;

public interface ILeadCaptureService
{
    Task CaptureAsync(CreateLeadRequest request, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<LeadSummaryDto>> GetRecentAsync(CancellationToken cancellationToken = default);
}
