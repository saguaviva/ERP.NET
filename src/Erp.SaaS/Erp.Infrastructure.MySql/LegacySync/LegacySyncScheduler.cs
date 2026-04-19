using Erp.Application.LegacySync;
using Erp.Infrastructure.MySql.Configuration;
using Erp.Infrastructure.MySql.Database;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.LegacySync;

public sealed class LegacySyncScheduler : BackgroundService
{
    private readonly MySqlConnectionFactory _connectionFactory;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IOptionsMonitor<LegacySyncOptions> _optionsMonitor;
    private readonly ILogger<LegacySyncScheduler> _logger;
    private DateOnly? _lastRunDate;

    public LegacySyncScheduler(
        MySqlConnectionFactory connectionFactory,
        IServiceScopeFactory scopeFactory,
        IOptionsMonitor<LegacySyncOptions> optionsMonitor,
        ILogger<LegacySyncScheduler> logger)
    {
        _connectionFactory = connectionFactory;
        _scopeFactory = scopeFactory;
        _optionsMonitor = optionsMonitor;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromMinutes(15));
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await TryRunAsync(stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error ejecutando el planificador nocturno de sincronización legacy.");
            }

            await timer.WaitForNextTickAsync(stoppingToken);
        }
    }

    private async Task TryRunAsync(CancellationToken cancellationToken)
    {
        var options = _optionsMonitor.CurrentValue;
        if (!_connectionFactory.IsConfigured || !options.NightlyEnabled)
        {
            return;
        }

        var localNow = DateTime.Now;
        var scheduledTimeToday = new DateTime(localNow.Year, localNow.Month, localNow.Day, options.NightlyHourLocal, options.NightlyMinuteLocal, 0, DateTimeKind.Local);
        if (localNow < scheduledTimeToday)
        {
            return;
        }

        var runDate = DateOnly.FromDateTime(localNow);
        if (_lastRunDate.HasValue && _lastRunDate.Value == runDate)
        {
            return;
        }

        var tenantIds = await LoadActiveTenantIdsAsync(cancellationToken);
        if (tenantIds.Count == 0)
        {
            _lastRunDate = runDate;
            return;
        }

        using var scope = _scopeFactory.CreateScope();
        var syncService = scope.ServiceProvider.GetRequiredService<ILegacySyncService>();
        var moduleKeys = options.ModuleKeys
            .Where(moduleKey => !string.IsNullOrWhiteSpace(moduleKey))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();

        foreach (var tenantId in tenantIds)
        {
            foreach (var moduleKey in moduleKeys)
            {
                var result = await syncService.RunAsync(new RunLegacySyncCommand
                {
                    TenantId = tenantId,
                    ModuleKey = moduleKey,
                    TriggeredByScheduler = true
                }, cancellationToken);

                _logger.LogInformation(
                    "Sync nocturno ejecutado. Tenant={TenantId}, Modulo={ModuleKey}, Companies={Companies}, Insertados={Inserted}, Actualizados={Updated}, Omitidos={Skipped}, Errores={Errors}",
                    tenantId,
                    moduleKey,
                    result.CompaniesProcessed,
                    result.RecordsInserted,
                    result.RecordsUpdated,
                    result.RecordsSkipped,
                    result.ErrorsCount);
            }
        }

        _lastRunDate = runDate;
    }

    private async Task<IReadOnlyCollection<Guid>> LoadActiveTenantIdsAsync(CancellationToken cancellationToken)
    {
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT DISTINCT t.id
            FROM tenants t
            INNER JOIN companies c
              ON c.tenant_id = t.id
            WHERE t.is_active = 1
              AND c.is_active = 1;
            """;

        var items = new List<Guid>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(reader.GetGuid("id"));
        }

        return items;
    }
}
