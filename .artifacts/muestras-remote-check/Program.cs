using System;
using MySqlConnector;
using System.Threading.Tasks;
class P {
  static async Task Main() {
    var cs = "Server=ns346061.ip-5-196-80.eu;Port=3306;Database=completex;User ID=completexlectura;Password=completex314;SslMode=None;AllowUserVariables=True;ConvertZeroDateTime=True;";
    await using var con = new MySqlConnection(cs);
    await con.OpenAsync();
    string[] sqls = {
      "SELECT CENTRO, COUNT(*) CNT FROM MOSTRES GROUP BY CENTRO ORDER BY CENTRO",
      "SELECT CENTRO, CODI, CLIENT, DESCRI FROM MOSTRES ORDER BY CENTRO, CODI LIMIT 20",
      "SELECT CENTRO, COUNT(*) CNT FROM TALLA GROUP BY CENTRO ORDER BY CENTRO",
      "SELECT CENTRO, COUNT(*) CNT FROM MAQ GROUP BY CENTRO ORDER BY CENTRO",
      "SELECT CENTRO, COUNT(*) CNT FROM COLOR GROUP BY CENTRO ORDER BY CENTRO"
    };
    foreach (var sql in sqls) {
      Console.WriteLine("--SQL-- " + sql);
      await using var cmd = new MySqlCommand(sql, con);
      await using var rdr = await cmd.ExecuteReaderAsync();
      while (await rdr.ReadAsync()) {
        for (int i=0;i<rdr.FieldCount;i++) Console.Write((i>0?" | ":"") + rdr.GetName(i)+":"+rdr.GetValue(i));
        Console.WriteLine();
      }
      Console.WriteLine();
    }
  }
}
