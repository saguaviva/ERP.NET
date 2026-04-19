using Erp.Application.LegacySync;

namespace Erp.Infrastructure.MySql.Configuration;

public sealed class LegacySyncOptions
{
    public const string SectionName = "LegacySync";

    public bool NightlyEnabled { get; set; }
    public int NightlyHourLocal { get; set; } = 2;
    public int NightlyMinuteLocal { get; set; }
    public string[] ModuleKeys { get; set; } = [LegacySyncModuleKeys.SalesOrders];
}
