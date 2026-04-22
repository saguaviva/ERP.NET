namespace Erp.Application.Suppliers;

public interface IProveedorService
{
    Task<int> SaveAsync(SaveProveedorCommand command, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid tenantId, Guid companyId, int code, CancellationToken cancellationToken = default);
}
