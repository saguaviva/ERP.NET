namespace Erp.Application.LegacySync;

public interface ILegacyModuleSyncHandler
{
    string ModuleKey { get; }
    string DisplayName { get; }
    Task<LegacySyncModuleRunResult> RunAsync(LegacySyncModuleContext context, CancellationToken cancellationToken = default);
}
