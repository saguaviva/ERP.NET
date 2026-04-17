using Erp.Application.Companies;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Support;

namespace Erp.Infrastructure.MySql.Companies;

public sealed class MySqlCompanyAccessService : ICompanyAccessService
{
    private readonly MySqlConnectionFactory _connectionFactory;

    public MySqlCompanyAccessService(MySqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IReadOnlyCollection<AllowedCompanyDto>> GetAllowedCompaniesAsync(Guid userId, Guid tenantId, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return [];
        }

        var results = new List<AllowedCompanyDto>();

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT c.id, c.tenant_id, c.name, c.slug, c.legacy_center_code
            FROM user_company_memberships ucm
            INNER JOIN companies c ON c.id = ucm.company_id AND c.tenant_id = ucm.tenant_id
            WHERE ucm.user_id = @userId
              AND ucm.tenant_id = @tenantId
              AND c.is_active = 1
            ORDER BY c.name;
            """;
        command.Parameters.AddWithValue("@userId", userId.ToString());
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            results.Add(new AllowedCompanyDto
            {
                CompanyId = reader.GetGuid("id"),
                TenantId = reader.GetGuid("tenant_id"),
                Name = reader.GetStringOrEmpty("name"),
                Slug = reader.GetStringOrEmpty("slug"),
                LegacyCenterCode = reader.GetStringOrEmpty("legacy_center_code")
            });
        }

        return results;
    }
}
