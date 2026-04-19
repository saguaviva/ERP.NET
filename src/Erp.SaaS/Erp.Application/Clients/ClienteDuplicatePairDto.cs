namespace Erp.Application.Clients;

public sealed class ClienteDuplicatePairDto
{
    public int LeftCode { get; init; }
    public string LeftName { get; init; } = string.Empty;
    public int RightCode { get; init; }
    public string RightName { get; init; } = string.Empty;
    public string SharedTaxId { get; init; } = string.Empty;
    public string SharedEmail { get; init; } = string.Empty;
    public string SharedPhone { get; init; } = string.Empty;
    public bool IsHardConflict { get; init; }
    public IReadOnlyCollection<string> MatchReasons { get; init; } = [];
    public string ReviewStatus { get; init; } = string.Empty;
    public DateTimeOffset? ReviewUpdatedUtc { get; init; }
    public string ReviewUpdatedBy { get; init; } = string.Empty;
    public int? PreferredClientCode { get; init; }
    public DateTimeOffset? PreferredUpdatedUtc { get; init; }
    public string PreferredUpdatedBy { get; init; } = string.Empty;
}
