namespace Erp.Application.Sales;

public class SalesRemittanceListItemDto
{
    public Guid RemittanceId { get; set; }
    public string RemittanceSeries { get; set; } = string.Empty;
    public int RemittanceNumber { get; set; }
    public DateTime RemittanceDate { get; set; }
    public DateTime? DueDate { get; set; }
    public string Status { get; set; } = SalesRemittanceStatuses.Draft;
    public string BankName { get; set; } = string.Empty;
    public int InvoiceCount { get; set; }
    public int ClientCount { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal CollectedAmount { get; set; }
    public decimal OutstandingAmount { get; set; }
    public string Notes { get; set; } = string.Empty;
    public DateTime? SentUtc { get; set; }
    public DateTime? CollectedUtc { get; set; }

    public string DisplayNumber => string.IsNullOrWhiteSpace(RemittanceSeries)
        ? RemittanceNumber.ToString()
        : $"{RemittanceSeries}/{RemittanceNumber:000000}";
}
