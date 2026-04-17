using System.Data;

namespace Erp.Infrastructure.MySql.Support;

internal static class DataRecordExtensions
{
    public static string GetStringOrEmpty(this IDataRecord record, string columnName)
    {
        var ordinal = record.GetOrdinal(columnName);
        return record.IsDBNull(ordinal) ? string.Empty : record.GetString(ordinal);
    }

    public static Guid GetGuid(this IDataRecord record, string columnName)
    {
        var value = record.GetString(record.GetOrdinal(columnName));
        return Guid.Parse(value);
    }

    public static Guid? GetNullableGuid(this IDataRecord record, string columnName)
    {
        var ordinal = record.GetOrdinal(columnName);
        return record.IsDBNull(ordinal) ? null : Guid.Parse(record.GetString(ordinal));
    }

    public static bool GetBoolean(this IDataRecord record, string columnName)
    {
        var ordinal = record.GetOrdinal(columnName);
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
            _ => Convert.ToBoolean(value)
        };
    }
}
