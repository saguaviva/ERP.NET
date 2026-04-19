namespace Erp.Application.Sales;

public static class SalesInvoiceStatuses
{
    public const string Issued = "Issued";
    public const string Cancelled = "Cancelled";

    public static readonly IReadOnlyCollection<string> All = [Issued, Cancelled];
}
