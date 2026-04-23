namespace Erp.Application.Numbering;

public interface IDocumentNumberingService
{
    Task SaveSetupAsync(SaveDocumentNumberingSetupCommand command, CancellationToken cancellationToken = default);
}
