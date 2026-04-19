using System.Globalization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;

namespace Erp.App.Security;

public sealed class PreviewAccessCookieProtector(IDataProtectionProvider dataProtectionProvider, IOptionsMonitor<PreviewAccessOptions> optionsMonitor)
{
    private readonly IDataProtector _protector = dataProtectionProvider.CreateProtector("Erp.App.PreviewAccess");

    public string Protect(string email)
    {
        var options = optionsMonitor.CurrentValue;
        var expiresAtUtc = DateTimeOffset.UtcNow.AddDays(Math.Max(1, options.CookieDays));
        var payload = string.Join('\n', email.Trim(), expiresAtUtc.ToUnixTimeSeconds().ToString(CultureInfo.InvariantCulture));
        return _protector.Protect(payload);
    }

    public bool TryValidate(string protectedValue, out string? email)
    {
        email = null;

        if (string.IsNullOrWhiteSpace(protectedValue))
        {
            return false;
        }

        try
        {
            var payload = _protector.Unprotect(protectedValue);
            var parts = payload.Split('\n', 2, StringSplitOptions.None);
            if (parts.Length != 2 ||
                !long.TryParse(parts[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out var expiresAtSeconds))
            {
                return false;
            }

            if (DateTimeOffset.UtcNow > DateTimeOffset.FromUnixTimeSeconds(expiresAtSeconds))
            {
                return false;
            }

            email = parts[0];
            return true;
        }
        catch
        {
            return false;
        }
    }
}
