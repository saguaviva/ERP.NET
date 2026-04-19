namespace Erp.Application.Clients;

public static class ClienteDuplicateReviewStatus
{
    public const string Reviewed = "reviewed";
    public const string FalsePositive = "false_positive";

    public static readonly string[] All =
    [
        Reviewed,
        FalsePositive
    ];
}
