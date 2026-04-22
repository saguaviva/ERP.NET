using System;
using System.Threading.Tasks;
using MySqlConnector;

var cs = "Server=ns346061.ip-5-196-80.eu;Port=3306;Database=completex;User ID=completexlectura;Password=;SslMode=None;AllowUserVariables=True;";
await using var conn = new MySqlConnection(cs);
await conn.OpenAsync();
await using var cmd = conn.CreateCommand();
cmd.CommandText = @"
SELECT COUNT(*)
FROM factur f
INNER JOIN dfactu df ON df.FRA = f.FRA AND df.DOCUMENT = f.DOCUMENT
INNER JOIN clients c ON c.CODI = f.CLIENT
WHERE f.DOCUMENT='F'
  AND YEAR(f.DATA)=2025
  AND MONTH(f.DATA)=4
  AND LEFT(COALESCE(c.NIF,''),2) IN ('AT','BE','BG','CY','CZ','DE','DK','EE','EL','FI','FR','HR','HU','IE','IT','LT','LU','LV','MT','NL','PL','PT','RO','SE','SI','SK');";
Console.WriteLine(await cmd.ExecuteScalarAsync());
