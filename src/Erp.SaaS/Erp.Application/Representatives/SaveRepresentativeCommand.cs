namespace Erp.Application.Representatives;

public sealed class SaveRepresentativeCommand
{
    public Guid TenantId { get; set; }
    public Guid CompanyId { get; set; }
    public int? Code { get; set; }
    public string Name { get; set; } = string.Empty;
    public string TaxId { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Province { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string SecondaryPhone { get; set; } = string.Empty;
    public string Fax { get; set; } = string.Empty;
    public string PrimaryEmail { get; set; } = string.Empty;
    public string SecondaryEmail { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    public decimal CommissionPercent { get; set; }
    public string Notes { get; set; } = string.Empty;
}
