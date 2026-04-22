using System;
using MySqlConnector;
using System.Threading.Tasks;
class P {
  static async Task Main() {
    var cs = "Server=localhost;Port=3306;Database=completex;User ID=completex;Password=completex314;AllowUserVariables=True;SslMode=None;";
    await using var con = new MySqlConnection(cs);
    await con.OpenAsync();
    string[] sqls = {
      "SHOW COLUMNS FROM mostres",
      "SELECT CENTRO, COUNT(*) CNT FROM mostres GROUP BY CENTRO ORDER BY CENTRO",
      "SELECT COUNT(*) FROM information_schema.columns WHERE table_schema = DATABASE() AND table_name='mostres' AND column_name IN ('TIPUS','TIPO','TIPUSMOSTRA','TIPUS_MOSTRA','COMPLEMENT')"
    };
    foreach (var sql in sqls) {
      Console.WriteLine("--SQL-- " + sql);
      await using var cmd = new MySqlCommand(sql, con);
      await using var rdr = await cmd.ExecuteReaderAsync();
      while (await rdr.ReadAsync()) {
        for (int i=0;i<rdr.FieldCount;i++) {
          Console.Write((i>0?" | ":"") + rdr.GetName(i)+":"+rdr.GetValue(i));
        }
        Console.WriteLine();
      }
      Console.WriteLine();
    }
  }
}
