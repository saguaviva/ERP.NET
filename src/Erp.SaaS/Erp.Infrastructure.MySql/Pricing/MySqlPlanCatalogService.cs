using Erp.Application.Pricing;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Support;

namespace Erp.Infrastructure.MySql.Pricing;

public sealed class MySqlPlanCatalogService : IPlanCatalogService
{
    private readonly MySqlConnectionFactory _connectionFactory;

    public MySqlPlanCatalogService(MySqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IReadOnlyCollection<PlanCardDto>> GetPublicPlansAsync(CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return [];
        }

        var plans = new List<PlanCardDto>();

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT slug, name, max_users, monthly_price, description
            FROM plan_definitions
            WHERE is_active = 1
            ORDER BY max_users;
            """;

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            plans.Add(new PlanCardDto
            {
                Slug = reader.GetStringOrEmpty("slug"),
                Name = reader.GetStringOrEmpty("name"),
                MaxUsers = reader.GetInt32(reader.GetOrdinal("max_users")),
                MonthlyPrice = reader.GetDecimal(reader.GetOrdinal("monthly_price")),
                Description = reader.GetStringOrEmpty("description")
            });
        }

        return plans;
    }
}
