namespace Erp.Application.Numbering;

public interface IDocumentNumberingQueries
{
    Task<DocumentNumberingSetupDto> GetSetupAsync(Guid tenantId, Guid companyId, CancellationToken cancellationToken = default);
}
