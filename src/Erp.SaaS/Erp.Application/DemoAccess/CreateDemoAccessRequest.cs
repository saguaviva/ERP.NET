namespace Erp.Application.DemoAccess;

public sealed class CreateDemoAccessRequest
{
    public string ContactName { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public int RequestedUsers { get; set; }
    public string TesterEmails { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}
