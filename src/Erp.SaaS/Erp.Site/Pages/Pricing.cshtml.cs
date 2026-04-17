using Erp.Application.Pricing;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Erp.Site.Pages;

public sealed class PricingModel : PageModel
{
    private readonly IPlanCatalogService _planCatalogService;

    public PricingModel(IPlanCatalogService planCatalogService)
    {
        _planCatalogService = planCatalogService;
    }

    public IReadOnlyCollection<PlanCardDto> Plans { get; private set; } = [];

    public async Task OnGetAsync()
    {
        Plans = await _planCatalogService.GetPublicPlansAsync();
    }
}
