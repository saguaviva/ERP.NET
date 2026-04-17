namespace Erp.Application.Pricing;

public interface IPlanCatalogService
{
    Task<IReadOnlyCollection<PlanCardDto>> GetPublicPlansAsync(CancellationToken cancellationToken = default);
}
