namespace Erp.Application.Sales;

public static class SalesInvoiceAccountingStatuses
{
    public const string Pending = "Pending";
    public const string Ready = "Ready";
    public const string Exported = "Exported";

    public static readonly IReadOnlyCollection<string> All = [Pending, Ready, Exported];
}
