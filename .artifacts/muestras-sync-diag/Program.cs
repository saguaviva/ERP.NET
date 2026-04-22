using MySqlConnector;

var cs = "Server=localhost;Port=3306;Database=completex;User ID=completex;Password=completex314;AllowUserVariables=true;";

await using var cn = new MySqlConnection(cs);
await cn.OpenAsync();

Console.WriteLine("JOBS");
await using (var cmd = cn.CreateCommand())
{
    cmd.CommandText =
        """
        SELECT COLUMN_NAME
        FROM information_schema.columns
        WHERE table_schema = DATABASE()
          AND table_name = 'legacy_sync_jobs'
        ORDER BY ordinal_position;
        """;
    await using var rd = await cmd.ExecuteReaderAsync();
    while (await rd.ReadAsync())
    {
        Console.WriteLine($"COL|{rd.GetString(0)}");
    }
}

Console.WriteLine("LATEST_JOBS");
await using (var cmd = cn.CreateCommand())
{
    cmd.CommandText =
        """
        SELECT *
        FROM legacy_sync_jobs
        WHERE module_key = 'article-muestras'
        ORDER BY started_utc DESC
        LIMIT 4;
        """;
    await using var rd = await cmd.ExecuteReaderAsync();
    while (await rd.ReadAsync())
    {
        var parts = new List<string>();
        for (var i = 0; i < rd.FieldCount; i++)
        {
            parts.Add($"{rd.GetName(i)}={(rd.IsDBNull(i) ? "NULL" : Convert.ToString(rd.GetValue(i)))}");
        }

        Console.WriteLine($"JOB|{string.Join("|", parts)}");
    }
}

Console.WriteLine("ERRORS");
await using (var cmd = cn.CreateCommand())
{
    cmd.CommandText =
        """
        SELECT module_key, stage, legacy_entity_key, error_message, payload, created_utc
        FROM legacy_sync_errors
        WHERE module_key = 'article-muestras'
        ORDER BY created_utc DESC
        LIMIT 10;
        """;
    await using var rd = await cmd.ExecuteReaderAsync();
    while (await rd.ReadAsync())
    {
        Console.WriteLine(
            $"ERR|{rd.GetString(0)}|{rd.GetString(1)}|{(rd.IsDBNull(2) ? "" : rd.GetString(2))}|{rd.GetString(3)}|{(rd.IsDBNull(4) ? "" : rd.GetString(4))}|{rd.GetDateTime(5):O}");
    }
}

Console.WriteLine("REMOTE_MAQ_COLUMNS");
var remoteCs = "Server=ns346061.ip-5-196-80.eu;Port=3306;Database=completex;User ID=completexlectura;Password=completex314;AllowUserVariables=true;";
await using var remote = new MySqlConnection(remoteCs);
await remote.OpenAsync();
await using (var cmd = remote.CreateCommand())
{
    cmd.CommandText =
        """
        SELECT COLUMN_NAME
        FROM information_schema.columns
        WHERE table_schema = DATABASE()
          AND table_name = 'MAQ'
        ORDER BY ordinal_position;
        """;
    await using var rd = await cmd.ExecuteReaderAsync();
    while (await rd.ReadAsync())
    {
        Console.WriteLine($"RMAQ|{rd.GetString(0)}");
    }
}

Console.WriteLine("REMOTE_MAQUI_COLUMNS");
await using (var cmd = remote.CreateCommand())
{
    cmd.CommandText =
        """
        SELECT COLUMN_NAME
        FROM information_schema.columns
        WHERE table_schema = DATABASE()
          AND table_name = 'MAQUI'
        ORDER BY ordinal_position;
        """;
    await using var rd = await cmd.ExecuteReaderAsync();
    while (await rd.ReadAsync())
    {
        Console.WriteLine($"RMAQUI|{rd.GetString(0)}");
    }
}
