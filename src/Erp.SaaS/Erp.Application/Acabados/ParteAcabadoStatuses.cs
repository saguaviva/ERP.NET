namespace Erp.Application.Acabados;

public static class ParteAcabadoStatuses
{
    public const string Pending = "Pending";
    public const string InProgress = "InProgress";
    public const string Finished = "Finished";
    public const string Cancelled = "Cancelled";

    public static IReadOnlyList<string> All { get; } =
    [
        Pending,
        InProgress,
        Finished,
        Cancelled
    ];
}
