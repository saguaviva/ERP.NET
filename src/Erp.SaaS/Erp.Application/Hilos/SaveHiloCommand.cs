namespace Erp.Application.Hilos;

public sealed class SaveHiloCommand
{
    public Guid TenantId { get; set; }
    public Guid CompanyId { get; set; }
    public bool IsNew { get; set; }
    public string? Code { get; set; }
    public string Description { get; set; } = string.Empty;
    public int SupplierCode { get; set; }
    public decimal CostPrice { get; set; }
    public decimal UnitPrice { get; set; }
    public string VatCode { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public List<SaveHiloColorInput> Colors { get; set; } = [];
}
