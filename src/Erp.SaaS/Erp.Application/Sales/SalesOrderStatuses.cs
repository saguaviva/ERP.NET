namespace Erp.Application.Sales;

public static class SalesOrderStatuses
{
    public const string Draft = "Draft";
    public const string Confirmed = "Confirmed";
    public const string PartiallyShipped = "PartiallyShipped";
    public const string Shipped = "Shipped";
    public const string Cancelled = "Cancelled";

    public static IReadOnlyCollection<string> All { get; } =
    [
        Draft,
        Confirmed,
        PartiallyShipped,
        Shipped,
        Cancelled
    ];

    public static bool IsClosed(string status) =>
        string.Equals(status, Shipped, StringComparison.OrdinalIgnoreCase) ||
        string.Equals(status, Cancelled, StringComparison.OrdinalIgnoreCase);
}
