namespace Erp.Application.Clients;

public sealed class SetClientePreferredPrincipalCommand
{
    public Guid TenantId { get; set; }
    public Guid CompanyId { get; set; }
    public int ClientCode { get; set; }
    public int DuplicateClientCode { get; set; }
    public int? PreferredClientCode { get; set; }
}
