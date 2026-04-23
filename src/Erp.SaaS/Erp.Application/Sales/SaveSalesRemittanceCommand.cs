namespace Erp.Application.Sales;

public sealed class SaveSalesRemittanceCommand
{
    public int? RemittanceNumber { get; set; }
    public string RemittanceSeries { get; set; } = string.Empty;
    public DateTime RemittanceDate { get; set; }
    public DateTime? DueDate { get; set; }
    public string Status { get; set; } = SalesRemittanceStatuses.Draft;
    public string BankName { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public IReadOnlyCollection<SalesRemittanceInvoiceInputDto> Invoices { get; set; } = [];
}

public sealed class SalesRemittanceInvoiceInputDto
{
    public int InvoiceNumber { get; set; }
}
