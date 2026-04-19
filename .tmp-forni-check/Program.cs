using MySqlConnector;
var remote = "Server=ns346061.ip-5-196-80.eu;Port=3306;Database=completex;User ID=completexlectura;Password=completex314;Allow User Variables=true;Convert Zero Datetime=true;";
await using var conn = new MySqlConnection(remote);
await conn.OpenAsync();
await using var cmd = conn.CreateCommand();
cmd.CommandText = "SELECT TIPUS, COUNT(*) FROM filcol GROUP BY TIPUS ORDER BY TIPUS;";
await using var reader = await cmd.ExecuteReaderAsync();
while (await reader.ReadAsync())
{
    Console.WriteLine($"{reader.GetString(0)} | {reader.GetInt32(1)}");
}
