using Erp.Infrastructure.MySql.Configuration;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Database;

public sealed class MySqlConnectionFactory
{
    private readonly IOptions<ErpDatabaseOptions> _options;

    public MySqlConnectionFactory(IOptions<ErpDatabaseOptions> options)
    {
        _options = options;
    }

    public bool IsConfigured => _options.Value.IsConfigured;

    public async Task<MySqlConnection> OpenConnectionAsync(CancellationToken cancellationToken = default)
    {
        var settings = _options.Value;
        if (!settings.IsConfigured)
        {
            throw new InvalidOperationException("ErpDatabase is not configured.");
        }

        var connection = new MySqlConnection(settings.BuildConnectionString());
        await connection.OpenAsync(cancellationToken);
        return connection;
    }
}
