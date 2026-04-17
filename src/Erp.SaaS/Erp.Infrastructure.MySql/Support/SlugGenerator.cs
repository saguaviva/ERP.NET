using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Erp.Infrastructure.MySql.Support;

internal static partial class SlugGenerator
{
    private static readonly Regex DashRegex = BuildDashRegex();

    public static string Generate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Guid.NewGuid().ToString("N")[..12];
        }

        var normalized = value.Trim().ToLowerInvariant().Normalize(NormalizationForm.FormD);
        var builder = new StringBuilder(normalized.Length);

        foreach (var character in normalized)
        {
            var category = CharUnicodeInfo.GetUnicodeCategory(character);
            if (category == UnicodeCategory.NonSpacingMark)
            {
                continue;
            }

            builder.Append(char.IsLetterOrDigit(character) ? character : '-');
        }

        var slug = DashRegex.Replace(builder.ToString(), "-").Trim('-');
        return string.IsNullOrWhiteSpace(slug) ? Guid.NewGuid().ToString("N")[..12] : slug;
    }

    [GeneratedRegex("-{2,}")]
    private static partial Regex BuildDashRegex();
}
