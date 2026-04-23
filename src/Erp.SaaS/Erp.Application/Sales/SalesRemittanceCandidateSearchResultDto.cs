namespace Erp.Application.Sales;

public sealed class SalesRemittanceCandidateSearchResultDto
{
    public IReadOnlyCollection<SalesRemittanceCandidateInvoiceDto> Items { get; set; } = [];
    public int TotalCount { get; set; }
}
