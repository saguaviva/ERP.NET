namespace Erp.Application.Sales;

public sealed class RegisterSalesInvoicePaymentCommand
{
    public Guid TenantId { get; set; }
    public Guid CompanyId { get; set; }
    public int InvoiceNumber { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.Today;
    public decimal Amount { get; set; }
    public string Method { get; set; } = string.Empty;
    public string Reference { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
}
