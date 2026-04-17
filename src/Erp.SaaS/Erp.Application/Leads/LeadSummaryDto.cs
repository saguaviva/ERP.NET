namespace Erp.Application.Leads;

public sealed class LeadSummaryDto
{
    public Guid Id { get; init; }
    public string ContactName { get; init; } = string.Empty;
    public string CompanyName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
    public int RequestedUsers { get; init; }
    public string Message { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;
    public DateTimeOffset CreatedUtc { get; init; }
}
