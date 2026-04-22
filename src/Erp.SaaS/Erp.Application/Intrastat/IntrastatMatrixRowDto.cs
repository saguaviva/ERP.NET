namespace Erp.Application.Intrastat;

public sealed class IntrastatMatrixRowDto
{
    public string CountryCode { get; set; } = string.Empty;
    public string CountryName { get; set; } = string.Empty;
    public bool HasClients { get; set; }
    public bool IsDomesticReference { get; set; }
    public Dictionary<string, decimal> WeightByNcCode { get; set; } = new(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, decimal> AmountByNcCode { get; set; } = new(StringComparer.OrdinalIgnoreCase);
    public decimal TotalWeightKg { get; set; }
    public decimal TotalInvoiceAmount { get; set; }
}
