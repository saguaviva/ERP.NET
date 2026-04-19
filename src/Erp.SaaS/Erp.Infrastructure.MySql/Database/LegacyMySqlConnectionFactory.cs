using Erp.Infrastructure.MySql.Configuration;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Database;

public sealed class LegacyMySqlConnectionFactory
{
    private readonly IOptions<LegacySourceDatabaseOptions> _options;

    public LegacyMySqlConnectionFactory(IOptions<LegacySourceDatabaseOptions> options)
    {
        _options = options;
    }

    public bool IsConfigured => _options.Value.IsConfigured;

    public async Task<MySqlConnection> OpenConnectionAsync(CancellationToken cancellationToken = default)
    {
        var settings = _options.Value;
        if (!settings.IsConfigured)
        {
            throw new InvalidOperationException("LegacySourceDatabase is not configured.");
        }

        var connection = new MySqlConnection(settings.BuildConnectionString());
        await connection.OpenAsync(cancellationToken);

        // Force every session against the production legacy source into read-only mode.
        await using var command = connection.CreateCommand();
        command.CommandText = "SET SESSION TRANSACTION READ ONLY;";
        await command.ExecuteNonQueryAsync(cancellationToken);

        return connection;
    }
}
