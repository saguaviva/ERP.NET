namespace Erp.Application.Contexts;

public interface ITenantContext
{
    Guid? TenantId { get; }
}
