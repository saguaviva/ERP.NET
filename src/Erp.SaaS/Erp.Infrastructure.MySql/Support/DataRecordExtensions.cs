using System.Data;

namespace Erp.Infrastructure.MySql.Support;

internal static class DataRecordExtensions
{
    public static string GetStringOrEmpty(this IDataRecord record, string columnName)
    {
        var ordinal = record.GetOrdinal(columnName);
        if (record.IsDBNull(ordinal))
        {
            return string.Empty;
        }

        var value = record.GetValue(ordinal);
        return value switch
        {
            string stringValue => stringValue,
            Guid guidValue => guidValue.ToString(),
            byte[] bytes when bytes.Length == 16 => new Guid(bytes).ToString(),
            _ => Convert.ToString(value) ?? string.Empty
        };
    }

    public static Guid GetGuid(this IDataRecord record, string columnName)
    {
        var ordinal = record.GetOrdinal(columnName);
        return ReadGuid(record.GetValue(ordinal), columnName);
    }

    public static Guid? GetNullableGuid(this IDataRecord record, string columnName)
    {
        var ordinal = record.GetOrdinal(columnName);
        return record.IsDBNull(ordinal) ? null : ReadGuid(record.GetValue(ordinal), columnName);
    }

    public static bool GetBooleanValue(this IDataRecord record, string columnName)
    {
        var ordinal = record.GetOrdinal(columnName);
        return GetBooleanValue(record, ordinal);
    }

    public static bool GetBooleanValue(this IDataRecord record, int ordinal)
    {
        if (record.IsDBNull(ordinal))
        {
            return false;
        }

        var value = record.GetValue(ordinal);
        return value switch
        {
            bool booleanValue => booleanValue,
            sbyte signedByte => signedByte != 0,
            byte unsignedByte => unsignedByte != 0,
            short shortValue => shortValue != 0,
            int intValue => intValue != 0,
            long longValue => longValue != 0,
            byte[] bytes => bytes.Any(static item => item != 0),
            string stringValue when string.IsNullOrWhiteSpace(stringValue) => false,
            string stringValue when string.Equals(stringValue, "0", StringComparison.OrdinalIgnoreCase) => false,
            string stringValue when string.Equals(stringValue, "1", StringComparison.OrdinalIgnoreCase) => true,
            string stringValue when string.Equals(stringValue, "false", StringComparison.OrdinalIgnoreCase) => false,
            string stringValue when string.Equals(stringValue, "true", StringComparison.OrdinalIgnoreCase) => true,
            _ => Convert.ToBoolean(value)
        };
    }

    public static decimal GetDecimalOrDefault(this IDataRecord record, string columnName, decimal defaultValue = 0m)
    {
        var ordinal = record.GetOrdinal(columnName);
        if (record.IsDBNull(ordinal))
        {
            return defaultValue;
        }

        var value = record.GetValue(ordinal);
        return value switch
        {
            decimal decimalValue => decimalValue,
            double doubleValue => Convert.ToDecimal(doubleValue),
            float floatValue => Convert.ToDecimal(floatValue),
            int intValue => intValue,
            long longValue => longValue,
            short shortValue => shortValue,
            byte byteValue => byteValue,
            string stringValue when string.IsNullOrWhiteSpace(stringValue) => defaultValue,
            string stringValue when decimal.TryParse(stringValue, out var parsedDecimal) => parsedDecimal,
            _ => Convert.ToDecimal(value)
        };
    }

    public static int GetInt32OrDefault(this IDataRecord record, string columnName, int defaultValue = 0)
    {
        var ordinal = record.GetOrdinal(columnName);
        if (record.IsDBNull(ordinal))
        {
            return defaultValue;
        }

        var value = record.GetValue(ordinal);
        return value switch
        {
            int intValue => intValue,
            long longValue => Convert.ToInt32(longValue),
            short shortValue => shortValue,
            byte byteValue => byteValue,
            decimal decimalValue => Convert.ToInt32(decimalValue),
            string stringValue when string.IsNullOrWhiteSpace(stringValue) => defaultValue,
            string stringValue when int.TryParse(stringValue, out var parsedInt) => parsedInt,
            _ => Convert.ToInt32(value)
        };
    }

    private static Guid ReadGuid(object value, string columnName)
    {
        return value switch
        {
            Guid guidValue => guidValue,
            string stringValue when Guid.TryParse(stringValue, out var parsedGuid) => parsedGuid,
            byte[] bytes when bytes.Length == 16 => new Guid(bytes),
            _ => throw new InvalidCastException($"The value in column '{columnName}' cannot be converted to Guid.")
        };
    }
}
