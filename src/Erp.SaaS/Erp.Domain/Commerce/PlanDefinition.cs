namespace Erp.Domain.Commerce;

public sealed class PlanDefinition
{
    public Guid Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int MaxUsers { get; set; }
    public decimal MonthlyPrice { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsPublic { get; set; } = true;
}
