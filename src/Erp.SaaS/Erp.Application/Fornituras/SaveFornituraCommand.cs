namespace Erp.Application.Fornituras;

public sealed class SaveFornituraCommand
{
    public Guid TenantId { get; set; }
    public Guid CompanyId { get; set; }
    public string? Code { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int SupplierCode { get; set; }
    public string SupplierReference { get; set; } = string.Empty;
    public int ClientCode { get; set; }
    public string Model { get; set; } = string.Empty;
    public string Series { get; set; } = string.Empty;
    public string Season { get; set; } = string.Empty;
    public List<SaveFornituraVariantInput> Variants { get; set; } = [];
}
