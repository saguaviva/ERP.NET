namespace Erp.Application.Auth;

public interface IAuthService
{
    Task<AuthenticatedSession?> AuthenticateAsync(LoginRequest request, CancellationToken cancellationToken = default);
}
