namespace Erp.Application.Muestras;

public sealed class SaveMuestraBreakdownLineInput
{
    public int LineNumber { get; set; }
    public string YarnCode { get; set; } = string.Empty;
    public int ProviderCode { get; set; }
    public string ProviderName { get; set; } = string.Empty;
    public string MaterialColor { get; set; } = string.Empty;
    public decimal YarnMetric { get; set; }
    public decimal Ends { get; set; }
    public decimal Passes { get; set; }
    public int Graduation { get; set; }
    public decimal Consumption { get; set; }
    public decimal Price { get; set; }
}
