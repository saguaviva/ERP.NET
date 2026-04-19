namespace Erp.Application.Suppliers;

public sealed class ProveedorCatalogOptionDto
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int? NumberOfPayments { get; set; }
    public int? PaymentDays { get; set; }
    public int? FirstPaymentDays { get; set; }
    public decimal? TaxPercent { get; set; }
    public decimal? SurchargePercent { get; set; }
}
