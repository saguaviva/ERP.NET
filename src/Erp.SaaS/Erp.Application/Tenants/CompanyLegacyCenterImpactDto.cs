namespace Erp.Application.Tenants;

public sealed class CompanyLegacyCenterImpactDto
{
    public Guid CompanyId { get; init; }
    public Guid TenantId { get; init; }
    public string CompanyName { get; init; } = string.Empty;
    public string CurrentLegacyCenterCode { get; init; } = string.Empty;
    public int AssignedUsersCount { get; init; }
    public int ClientCount { get; init; }
    public int SupplierCount { get; init; }
    public int PurchaseDocumentCount { get; init; }
    public int SalesDocumentCount { get; init; }
    public int AuditEventCount { get; init; }

    public bool HasUsage =>
        AssignedUsersCount > 0 ||
        ClientCount > 0 ||
        SupplierCount > 0 ||
        PurchaseDocumentCount > 0 ||
        SalesDocumentCount > 0 ||
        AuditEventCount > 0;
}
