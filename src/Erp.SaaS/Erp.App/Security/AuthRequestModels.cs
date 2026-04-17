namespace Erp.App.Security;

public sealed class LoginForm
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? RememberMe { get; set; }
    public string? ReturnUrl { get; set; }

    public bool RememberMeValue =>
        string.Equals(RememberMe, "true", StringComparison.OrdinalIgnoreCase) ||
        string.Equals(RememberMe, "on", StringComparison.OrdinalIgnoreCase);
}

public sealed class SwitchCompanyForm
{
    public Guid CompanyId { get; set; }
    public string? ReturnUrl { get; set; }
}
