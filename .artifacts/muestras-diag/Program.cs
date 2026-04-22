using MySqlConnector;

var cs = "Server=localhost;Port=3306;Database=completex;User ID=completex;Password=completex314;Allow User Variables=true;";
await using var conn = new MySqlConnection(cs);
await conn.OpenAsync();

var statements = new[]
{
    "ALTER TABLE mostres ADD COLUMN NOMCLIENT VARCHAR(255) NULL AFTER CLIENT;",
    "ALTER TABLE mostres ADD COLUMN NOMMAQUI VARCHAR(255) NULL AFTER MAQUINA;",
    "ALTER TABLE mostres ADD COLUMN origin VARCHAR(20) NOT NULL DEFAULT 'legacy' AFTER CENTRO;",
    "ALTER TABLE mostres ADD COLUMN is_deleted TINYINT(1) NOT NULL DEFAULT 0 AFTER origin;",
    "ALTER TABLE mostres ADD COLUMN synced_utc DATETIME(6) NULL AFTER is_deleted;"
};

foreach (var statement in statements)
{
    await using var cmd = conn.CreateCommand();
    cmd.CommandText = statement;
    try
    {
        await cmd.ExecuteNonQueryAsync();
        Console.WriteLine($"OK: {statement.Split('\n')[0]}");
    }
    catch (MySqlException exception) when (exception.Number is 1060)
    {
        Console.WriteLine($"SKIP: {exception.Message}");
    }
}

Console.WriteLine();
foreach (var table in new[] { "mostres", "mostres_detail" })
{
    Console.WriteLine($"[{table}]");
    await using var cmd = conn.CreateCommand();
    cmd.CommandText = $"SHOW COLUMNS FROM {table};";
    await using var reader = await cmd.ExecuteReaderAsync();
    while (await reader.ReadAsync())
    {
        Console.WriteLine($"{reader.GetValue(0)}|{reader.GetValue(1)}|{reader.GetValue(2)}|{reader.GetValue(4)}");
    }

    Console.WriteLine();
}
