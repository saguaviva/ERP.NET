using MySqlConnector;

var cs = "Server=ns346061.ip-5-196-80.eu;Port=3306;Database=completex;User ID=completexlectura;Password=completex314;SslMode=None;AllowUserVariables=True;ConvertZeroDateTime=True";
await using var conn = new MySqlConnection(cs);
await conn.OpenAsync();
var tables = new[]{"teixits","fil","forni","mostres","filcol"};
foreach (var table in tables)
{
    Console.WriteLine($"-- {table} --");
    await using var cmd = conn.CreateCommand();
    cmd.CommandText = @"SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE
FROM information_schema.columns
WHERE table_schema = DATABASE() AND table_name = @t
ORDER BY ordinal_position;";
    cmd.Parameters.AddWithValue("@t", table);
    await using var reader = await cmd.ExecuteReaderAsync();
    while (await reader.ReadAsync())
    {
        Console.WriteLine($"{reader.GetString(0)} | {reader.GetString(1)} | {reader.GetString(2)}");
    }
    await reader.CloseAsync();

    await using var countCmd = conn.CreateCommand();
    countCmd.CommandText = $"SELECT COUNT(*) FROM {table};";
    var count = Convert.ToInt32(await countCmd.ExecuteScalarAsync());
    Console.WriteLine($"COUNT={count}");
}
