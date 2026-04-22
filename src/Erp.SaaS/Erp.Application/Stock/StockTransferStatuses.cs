namespace Erp.Application.Stock;

public static class StockTransferStatuses
{
    public const string Draft = "Draft";
    public const string Completed = "Completed";
    public const string Cancelled = "Cancelled";

    public static readonly IReadOnlyCollection<string> All =
    [
        Draft,
        Completed,
        Cancelled
    ];
}
