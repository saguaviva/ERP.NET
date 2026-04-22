namespace Erp.Application.Purchases;

public sealed class PurchaseInvoicePaymentDto
{
    public Guid PaymentId { get; set; }
    public int PaymentNumber { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public string Method { get; set; } = string.Empty;
    public string Reference { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public DateTime CreatedUtc { get; set; }
}
