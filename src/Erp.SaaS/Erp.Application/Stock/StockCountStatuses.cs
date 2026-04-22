namespace Erp.Application.Stock;

public static class StockCountStatuses
{
    public const string Draft = "Draft";
    public const string Completed = "Completed";
    public const string Cancelled = "Cancelled";

    public static IReadOnlyList<string> All { get; } =
    [
        Draft,
        Completed,
        Cancelled
    ];
}
