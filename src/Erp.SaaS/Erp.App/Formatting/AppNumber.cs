using System.Globalization;

namespace Erp.App.Formatting;

public static class AppNumber
{
    public static CultureInfo CreateDisplayCulture(CultureInfo baseCulture)
    {
        var culture = (CultureInfo)baseCulture.Clone();
        culture.NumberFormat.NumberGroupSeparator = ".";
        culture.NumberFormat.NumberDecimalSeparator = ",";
        culture.NumberFormat.CurrencyGroupSeparator = ".";
        culture.NumberFormat.CurrencyDecimalSeparator = ",";
        culture.NumberFormat.PercentGroupSeparator = ".";
        culture.NumberFormat.PercentDecimalSeparator = ",";
        return culture;
    }

    public static CultureInfo CurrentCulture => CreateDisplayCulture(CultureInfo.CurrentCulture);

    public static string Format<T>(T value, string format)
        where T : IFormattable
    {
        return value.ToString(NormalizeFormat(format), CurrentCulture);
    }

    public static string Currency<T>(T value, int decimals = 2)
        where T : IFormattable
    {
        return $"{Format(value, $"N{decimals}")} €";
    }

    private static string NormalizeFormat(string format)
    {
        if (string.IsNullOrWhiteSpace(format))
        {
            return format;
        }

        if (format.StartsWith("0", StringComparison.Ordinal) &&
            !format.Contains(',', StringComparison.Ordinal) &&
            !format.Contains(';', StringComparison.Ordinal))
        {
            return $"#,##{format}";
        }

        return format;
    }
}

public static class AppNumberExtensions
{
    public static string ToUiString<T>(this T value, string format)
        where T : IFormattable
    {
        return AppNumber.Format(value, format);
    }

    public static string ToUiString<T>(this T? value, string format, string emptyValue = "")
        where T : struct, IFormattable
    {
        return value.HasValue ? AppNumber.Format(value.Value, format) : emptyValue;
    }
}
