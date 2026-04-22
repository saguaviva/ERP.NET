namespace Erp.Application.Tejidos;

public sealed class TejidoDetailDto
{
    public string Code { get; set; } = string.Empty;
    public string CompanyCenterCode { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string CompositionText { get; set; } = string.Empty;
    public int MachineCode { get; set; }
    public decimal MaterialCost { get; set; }
    public string Notes { get; set; } = string.Empty;
    public string VatCode { get; set; } = string.Empty;
    public int WeaverCode { get; set; }
    public decimal WeavingCost { get; set; }
    public int PrinterCode { get; set; }
    public decimal PrintingCost { get; set; }
    public int FinisherCode { get; set; }
    public string FinishSummary { get; set; } = string.Empty;
    public decimal FinishingCost { get; set; }
    public decimal RawCost { get; set; }
    public string WidthText { get; set; } = string.Empty;
    public decimal Yield { get; set; }
    public decimal Margin { get; set; }
    public decimal GramWeight { get; set; }
    public decimal PricePerMeter { get; set; }
    public decimal PricePerKilogram { get; set; }
    public decimal RawStockMeters { get; set; }
    public decimal AvailableStockMeters { get; set; }
    public decimal RawStockKilograms { get; set; }
    public decimal AvailableStockKilograms { get; set; }
    public decimal SamplePrice { get; set; }
    public bool IsTubular { get; set; }
    public decimal Width2 { get; set; }
    public List<TejidoColorDetailDto> Colors { get; set; } = [];
    public List<TejidoCompositionDetailDto> Composition { get; set; } = [];
    public List<TejidoFinishDetailDto> Finishes { get; set; } = [];
}
