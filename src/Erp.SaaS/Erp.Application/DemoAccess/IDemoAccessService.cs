namespace Erp.Application.DemoAccess;

public interface IDemoAccessService
{
    Task CaptureRequestAsync(CreateDemoAccessRequest request, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<DemoAccessRequestSummaryDto>> GetRecentRequestsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<PreviewInviteSummaryDto>> GetRecentInvitesAsync(CancellationToken cancellationToken = default);
    Task<bool> IsEmailAllowedAsync(string email, CancellationToken cancellationToken = default);
    Task CreateInviteAsync(CreatePreviewInviteCommand command, CancellationToken cancellationToken = default);
    Task CreateInvitesFromRequestAsync(CreatePreviewInvitesFromRequestCommand command, CancellationToken cancellationToken = default);
    Task SetInviteActiveAsync(SetPreviewInviteActiveCommand command, CancellationToken cancellationToken = default);
}
