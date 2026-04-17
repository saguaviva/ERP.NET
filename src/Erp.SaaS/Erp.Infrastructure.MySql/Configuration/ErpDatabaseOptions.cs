using MySqlConnector;

namespace Erp.Infrastructure.MySql.Configuration;

public sealed class ErpDatabaseOptions
{
    public const string SectionName = "ErpDatabase";

    public string Host { get; set; } = "localhost";
    public int Port { get; set; } = 3306;
    public string Database { get; set; } = string.Empty;
    public string Username { get; set; } = "root";
    public string Password { get; set; } = string.Empty;
    public bool BootstrapOnStartup { get; set; }

    public bool IsConfigured => !string.IsNullOrWhiteSpace(Database);

    public string BuildConnectionString()
    {
        var builder = new MySqlConnectionStringBuilder
        {
            Server = Host,
            Port = (uint)Math.Max(0, Port),
            Database = Database,
            UserID = Username,
            Password = Password,
            AllowUserVariables = true,
            ConvertZeroDateTime = true
        };

        return builder.ConnectionString;
    }
}
