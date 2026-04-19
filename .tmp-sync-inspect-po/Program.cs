using MySqlConnector;
var cs = "Server=ns346061.ip-5-196-80.eu;Port=3306;Database=completex;User ID=completexlectura;Password=completex314;SslMode=None;AllowUserVariables=True;ConvertZeroDateTime=True";
await using var c = new MySqlConnection(cs);
await c.OpenAsync();
await using var cmd = c.CreateCommand();
cmd.CommandText = "SELECT CENTRO, COUNT(DISTINCT CONCAT(TIPUS,'/',FRA)) AS receipts, COUNT(DISTINCT CASE WHEN COALESCE(COMAN,0) > 0 THEN CONCAT(TIPUS,'/',FRA) END) AS receipts_with_order, COUNT(*) AS total_lines, SUM(CASE WHEN COALESCE(COMAN,0) > 0 THEN 1 ELSE 0 END) AS lines_with_order FROM dcactu WHERE DOCUMENT='A' GROUP BY CENTRO ORDER BY CENTRO;";
await using var r = await cmd.ExecuteReaderAsync();
while (await r.ReadAsync())
{
    Console.WriteLine($"{Convert.ToString(r.GetValue(0))} => receipts={Convert.ToString(r.GetValue(1))} receipts_with_order={Convert.ToString(r.GetValue(2))} total_lines={Convert.ToString(r.GetValue(3))} lines_with_order={Convert.ToString(r.GetValue(4))}");
}
