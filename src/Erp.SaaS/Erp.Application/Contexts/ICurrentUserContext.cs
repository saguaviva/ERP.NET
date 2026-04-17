namespace Erp.Application.Contexts;

public interface ICurrentUserContext
{
    Guid? UserId { get; }
    string Email { get; }
    string DisplayName { get; }
    bool IsAuthenticated { get; }
    bool IsPlatformAdmin { get; }
    IReadOnlyCollection<string> Roles { get; }
}
