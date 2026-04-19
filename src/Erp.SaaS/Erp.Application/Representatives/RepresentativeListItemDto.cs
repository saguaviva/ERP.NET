namespace Erp.Application.Representatives;

public sealed class RepresentativeListItemDto
{
    public int Code { get; init; }
    public string CompanyCenterCode { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public decimal CommissionPercent { get; init; }
    public string TaxId { get; init; } = string.Empty;
    public string City { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
}
