namespace Erp.Domain.Commerce;

public sealed class LeadRequest
{
    public Guid Id { get; set; }
    public string ContactName { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public int RequestedUsers { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Status { get; set; } = "New";
    public DateTimeOffset CreatedUtc { get; set; }
}
