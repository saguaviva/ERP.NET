namespace Erp.Application.Suppliers;

public interface IProveedorQueries
{
    Task<ProveedorSearchResultDto> SearchAsync(Guid tenantId, Guid companyId, ProveedorFilter filter, CancellationToken cancellationToken = default);
    Task<ProveedorDetailDto?> GetByCodeAsync(Guid tenantId, Guid companyId, int code, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<ProveedorCatalogOptionDto>> GetPaymentMethodsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<ProveedorCatalogOptionDto>> GetBanksAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<ProveedorCatalogOptionDto>> GetVatRatesAsync(Guid tenantId, Guid companyId, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<ProveedorCatalogOptionDto>> GetIncotermsAsync(CancellationToken cancellationToken = default);
}
