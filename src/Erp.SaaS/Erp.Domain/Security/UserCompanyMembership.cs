namespace Erp.Domain.Security;

public sealed class UserCompanyMembership
{
    public Guid UserId { get; set; }
    public Guid CompanyId { get; set; }
    public DateTimeOffset CreatedUtc { get; set; }
}
