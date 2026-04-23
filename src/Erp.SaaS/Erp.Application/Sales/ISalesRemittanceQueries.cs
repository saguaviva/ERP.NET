namespace Erp.Application.Sales;

public interface ISalesRemittanceQueries
{
    Task<SalesRemittanceSearchResultDto> SearchAsync(Guid tenantId, Guid companyId, SalesRemittanceFilter filter, CancellationToken cancellationToken = default);
    Task<SalesRemittanceDetailDto?> GetByNumberAsync(Guid tenantId, Guid companyId, int remittanceNumber, CancellationToken cancellationToken = default);
    Task<SalesRemittanceCandidateSearchResultDto> SearchCandidateInvoicesAsync(Guid tenantId, Guid companyId, SalesRemittanceCandidateFilter filter, CancellationToken cancellationToken = default);
    Task<SalesRemittanceCandidateInvoiceDto?> GetCandidateInvoiceByNumberAsync(Guid tenantId, Guid companyId, int invoiceNumber, CancellationToken cancellationToken = default);
}
