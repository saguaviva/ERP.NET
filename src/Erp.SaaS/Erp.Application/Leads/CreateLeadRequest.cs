namespace Erp.Application.Leads;

public sealed class CreateLeadRequest
{
    public string ContactName { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public int RequestedUsers { get; set; }
    public string Message { get; set; } = string.Empty;
}
