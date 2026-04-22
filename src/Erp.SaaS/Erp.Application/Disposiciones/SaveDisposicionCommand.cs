namespace Erp.Application.Disposiciones;

public sealed class SaveDisposicionCommand
{
    public Guid TenantId { get; set; }
    public Guid CompanyId { get; set; }
    public int? Code { get; set; }
    public string Year { get; set; } = string.Empty;
    public int Number { get; set; }
    public DateTime? Date { get; set; }
    public DateTime? ReceptionDate { get; set; }
    public int FinisherCode { get; set; }
    public int ClientCode { get; set; }
    public string ClientReferenceCode { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string ClientColor { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string OrderReference { get; set; } = string.Empty;
    public bool IsReceived { get; set; }
    public bool IsCancelled { get; set; }
    public List<SaveDisposicionLineInput> Lines { get; set; } = [];
}
