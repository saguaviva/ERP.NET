namespace Erp.Application.Disposiciones;

public sealed class DisposicionListItemDto
{
    public int Code { get; set; }
    public string CompanyCenterCode { get; set; } = string.Empty;
    public string Year { get; set; } = string.Empty;
    public int Number { get; set; }
    public DateTime? Date { get; set; }
    public DateTime? ReceptionDate { get; set; }
    public int ClientCode { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public int FinisherCode { get; set; }
    public string FinisherName { get; set; } = string.Empty;
    public string ClientReferenceCode { get; set; } = string.Empty;
    public string ClientColor { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string OrderReference { get; set; } = string.Empty;
    public decimal TotalPieces { get; set; }
    public decimal TotalKilograms { get; set; }
    public int LinesCount { get; set; }
    public bool IsReceived { get; set; }
    public bool IsCancelled { get; set; }
    public string Origin { get; set; } = string.Empty;
}
