namespace Erp.Application.DemoAccess;

public sealed class DemoAccessRequestSummaryDto
{
    public Guid Id { get; set; }
    public string ContactName { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public int RequestedUsers { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedUtc { get; set; }
    public DateTime? ReviewedUtc { get; set; }
    public Guid? ReviewedByUserId { get; set; }
    public string[] RequestedTesterEmails { get; set; } = [];
}
