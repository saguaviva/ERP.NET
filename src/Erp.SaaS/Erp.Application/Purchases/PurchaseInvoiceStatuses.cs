namespace Erp.Application.Purchases;

public static class PurchaseInvoiceStatuses
{
    public const string Draft = "Draft";
    public const string Registered = "Registered";
    public const string Paid = "Paid";
    public const string Cancelled = "Cancelled";

    public static readonly IReadOnlyCollection<string> All =
    [
        Draft,
        Registered,
        Paid,
        Cancelled
    ];
}
