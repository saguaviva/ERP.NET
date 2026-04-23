namespace Erp.Application.Sales;

public static class SalesRemittanceStatuses
{
    public const string Draft = "Draft";
    public const string Sent = "Sent";
    public const string Collected = "Collected";
    public const string Cancelled = "Cancelled";

    public static IReadOnlyList<string> All { get; } = [Draft, Sent, Collected, Cancelled];
}
