namespace Erp.Application.LegacySync;

public sealed class LegacySyncErrorRecord
{
    public string Stage { get; set; } = string.Empty;
    public string LegacyEntityKey { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;
    public string Payload { get; set; } = string.Empty;
}
