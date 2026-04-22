using System.Globalization;

namespace Erp.App.Localization;

public static class AppLanguages
{
    public const string Spanish = "es";
    public const string Catalan = "ca";
    public const string English = "en";

    public static IReadOnlyList<AppLanguageOption> Supported { get; } =
    [
        new(Spanish, "Español"),
        new(Catalan, "Català"),
        new(English, "English")
    ];

    public static string Normalize(string? culture)
    {
        if (string.IsNullOrWhiteSpace(culture))
        {
            return Spanish;
        }

        var key = culture.Trim().ToLowerInvariant();
        if (key.StartsWith("ca", StringComparison.Ordinal))
        {
            return Catalan;
        }

        if (key.StartsWith("en", StringComparison.Ordinal))
        {
            return English;
        }

        return Spanish;
    }

    public static string Normalize(CultureInfo? culture) =>
        Normalize(culture?.Name);
}

public sealed record AppLanguageOption(string Key, string DisplayName);
