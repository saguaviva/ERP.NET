namespace Erp.Application.Clients;

public sealed class ClienteDuplicateDto
{
    public int Code { get; init; }
    public string CompanyCenterCode { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string TaxId { get; init; } = string.Empty;
    public string City { get; init; } = string.Empty;
    public string PrimaryEmail { get; init; } = string.Empty;
    public string SecondaryEmail { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
    public bool IsBlocked { get; init; }
    public bool IsHardConflict { get; init; }
    public IReadOnlyCollection<string> MatchReasons { get; init; } = [];
    public string ReviewStatus { get; init; } = string.Empty;
    public DateTimeOffset? ReviewUpdatedUtc { get; init; }
    public string ReviewUpdatedBy { get; init; } = string.Empty;
}
