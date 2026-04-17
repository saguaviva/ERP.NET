namespace Erp.Application.Pricing;

public sealed class PlanCardDto
{
    public string Slug { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public int MaxUsers { get; init; }
    public decimal MonthlyPrice { get; init; }
    public string Description { get; init; } = string.Empty;
}
