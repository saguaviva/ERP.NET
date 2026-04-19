namespace Erp.Application.Transportistas;

public sealed class TransportistaListItemDto
{
    public int Code { get; init; }
    public string CompanyCenterCode { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string TaxId { get; init; } = string.Empty;
    public string City { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
    public string SecondaryPhone { get; init; } = string.Empty;
}
