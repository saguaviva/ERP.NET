namespace Erp.Application.Transportistas;

public sealed class TransportistaDetailDto
{
    public int Code { get; set; }
    public string CompanyCenterCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string TaxId { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Province { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string SecondaryPhone { get; set; } = string.Empty;
    public string Fax { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Format { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
}
