namespace Erp.Application.Suppliers;

public interface IProveedorService
{
    Task<int> SaveAsync(SaveProveedorCommand command, CancellationToken cancellationToken = default);
}
