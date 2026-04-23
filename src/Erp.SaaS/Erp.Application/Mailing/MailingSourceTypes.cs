namespace Erp.Application.Mailing;

public static class MailingSourceTypes
{
    public const string Clients = "clients";
    public const string Suppliers = "suppliers";
    public const string Representatives = "representatives";
    public const string Carriers = "carriers";

    public static IReadOnlyList<string> OrderedTypes { get; } =
    [
        Clients,
        Suppliers,
        Representatives,
        Carriers
    ];

    public static string Normalize(string? sourceType)
    {
        var value = (sourceType ?? string.Empty).Trim().ToLowerInvariant();
        return OrderedTypes.Contains(value, StringComparer.OrdinalIgnoreCase) ? value : Clients;
    }
}
