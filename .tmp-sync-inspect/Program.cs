using MySqlConnector;
var cs = "Server=ns346061.ip-5-196-80.eu;Port=3306;Database=completex;User ID=completexlectura;Password=completex314;SslMode=None;AllowUserVariables=True;ConvertZeroDateTime=True";
await using var c = new MySqlConnection(cs);
await c.OpenAsync();
await using var cmd = c.CreateCommand();
cmd.CommandText = @"
SELECT CENTRO, COUNT(*) AS duplicated_numbers
FROM (
    SELECT CENTRO, FRA
    FROM cactur
    WHERE DOCUMENT='C'
    GROUP BY CENTRO, FRA
    HAVING COUNT(*) > 1
) x
GROUP BY CENTRO
ORDER BY CENTRO;";
await using var r = await cmd.ExecuteReaderAsync();
while (await r.ReadAsync())
{
    Console.WriteLine($"{r.GetString(0)} => {r.GetInt32(1)}");
}
