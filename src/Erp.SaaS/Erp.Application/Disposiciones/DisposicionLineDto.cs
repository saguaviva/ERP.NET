namespace Erp.Application.Disposiciones;

public sealed class DisposicionLineDto
{
    public int LineNumber { get; set; }
    public string Description { get; set; } = string.Empty;
    public int WeaverCode { get; set; }
    public string WeaverName { get; set; } = string.Empty;
    public string DeliveryNoteNumber { get; set; } = string.Empty;
    public string FabricCode { get; set; } = string.Empty;
    public string CompositionText { get; set; } = string.Empty;
    public string PiecesText { get; set; } = string.Empty;
    public decimal TotalPieces { get; set; }
    public decimal TotalKilograms { get; set; }
    public string FinishText { get; set; } = string.Empty;
    public string WidthText { get; set; } = string.Empty;
    public decimal GramWeight { get; set; }
    public decimal Yield { get; set; }
    public bool IsServed { get; set; }
    public bool IsDisposed { get; set; }
}
