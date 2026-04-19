namespace Erp.Application.Sales;

public static class SalesInvoiceDraftStatuses
{
    public const string Draft = "Draft";
    public const string Issued = "Issued";

    public static readonly IReadOnlyCollection<string> All = [Draft, Issued];
}
