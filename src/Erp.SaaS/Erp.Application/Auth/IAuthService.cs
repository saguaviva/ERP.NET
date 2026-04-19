namespace Erp.Application.Auth;

public interface IAuthService
{
    Task<AuthenticatedSession?> AuthenticateAsync(LoginRequest request, CancellationToken cancellationToken = default);
    Task<AuthenticatedSession?> GetSessionAsync(Guid userId, CancellationToken cancellationToken = default);
    Task ChangeOwnPasswordAsync(Guid userId, string newPassword, CancellationToken cancellationToken = default);
}
