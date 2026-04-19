namespace Erp.Application.Clients;

public sealed class SetClienteDuplicateReviewCommand
{
    public Guid TenantId { get; set; }
    public Guid CompanyId { get; set; }
    public int ClientCode { get; set; }
    public int DuplicateClientCode { get; set; }
    public string? Status { get; set; }
}
