namespace Erp.App.Security;

public sealed class PreviewAccessOptions
{
    public const string SectionName = "PreviewAccess";

    public bool Enabled { get; set; }
    public bool RequireApprovedEmail { get; set; }
    public string SharedPassword { get; set; } = string.Empty;
    public string[] AllowedEmails { get; set; } = [];
    public string CookieName { get; set; } = "erp_preview_access";
    public int CookieDays { get; set; } = 14;

    public bool RequiresEmail => RequireApprovedEmail || AllowedEmails.Length > 0;
    public bool RequiresPassword => !string.IsNullOrWhiteSpace(SharedPassword);

    public IReadOnlySet<string> GetNormalizedAllowedEmails() =>
        AllowedEmails
            .Where(email => !string.IsNullOrWhiteSpace(email))
            .Select(email => email.Trim())
            .ToHashSet(StringComparer.OrdinalIgnoreCase);
}
