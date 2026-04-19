using MySqlConnector;
var cs = "Server=ns346061.ip-5-196-80.eu;Port=3306;Database=completex;User ID=completexlectura;Password=completex314;Allow User Variables=true;Convert Zero Datetime=true;";
await using var conn = new MySqlConnection(cs);
await conn.OpenAsync();
await using var cmd = conn.CreateCommand();
cmd.CommandText = "SELECT CENTRO, COUNT(*) FROM filcol WHERE TIPUS='O' GROUP BY CENTRO ORDER BY CENTRO;";
await using var reader = await cmd.ExecuteReaderAsync();
while (await reader.ReadAsync())
{
    Console.WriteLine($"{reader.GetString(0)} | {reader.GetInt32(1)}");
}
