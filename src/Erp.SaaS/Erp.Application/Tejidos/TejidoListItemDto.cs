namespace Erp.Application.Tejidos;

public sealed class TejidoListItemDto
{
    public string Code { get; set; } = string.Empty;
    public string CompanyCenterCode { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int MachineCode { get; set; }
    public int WeaverCode { get; set; }
    public int FinisherCode { get; set; }
    public string WidthText { get; set; } = string.Empty;
    public decimal GramWeight { get; set; }
    public decimal PricePerMeter { get; set; }
    public decimal PricePerKilogram { get; set; }
    public decimal AvailableStockMeters { get; set; }
    public bool IsTubular { get; set; }
    public string Origin { get; set; } = string.Empty;
}
