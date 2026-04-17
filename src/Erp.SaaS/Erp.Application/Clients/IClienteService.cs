namespace Erp.Application.Clients;

public interface IClienteService
{
    Task<int> SaveAsync(SaveClienteCommand command, CancellationToken cancellationToken = default);
}
