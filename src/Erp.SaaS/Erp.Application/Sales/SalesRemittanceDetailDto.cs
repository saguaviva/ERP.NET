namespace Erp.Application.Sales;

public sealed class SalesRemittanceDetailDto : SalesRemittanceListItemDto
{
    public IReadOnlyCollection<SalesRemittanceInvoiceDto> Invoices { get; set; } = [];
}
