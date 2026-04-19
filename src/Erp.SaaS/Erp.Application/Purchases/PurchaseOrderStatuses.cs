namespace Erp.Application.Purchases;

public static class PurchaseOrderStatuses
{
    public const string Draft = "Draft";
    public const string Sent = "Sent";
    public const string PartiallyReceived = "PartiallyReceived";
    public const string Received = "Received";
    public const string Cancelled = "Cancelled";

    public static IReadOnlyCollection<string> All { get; } =
    [
        Draft,
        Sent,
        PartiallyReceived,
        Received,
        Cancelled
    ];

    public static bool IsClosed(string status) =>
        string.Equals(status, Received, StringComparison.OrdinalIgnoreCase) ||
        string.Equals(status, Cancelled, StringComparison.OrdinalIgnoreCase);
}
