namespace Erp.Application.Sales;

public static class SalesInvoicePaymentStatuses
{
    public const string Pending = "Pending";
    public const string PartiallyPaid = "PartiallyPaid";
    public const string Paid = "Paid";

    public static readonly IReadOnlyCollection<string> All = [Pending, PartiallyPaid, Paid];
}
