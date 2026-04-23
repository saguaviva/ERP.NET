namespace Erp.App.Localization;

public static class AppDocumentLanguage
{
    public const string CookieName = "erp-document-language";

    private static readonly string[] DocumentRouteMarkers =
    [
        "/imprimir",
        "/pdf",
        "/export/",
        "/ficha",
        "/fitxa",
        "/etiquetas",
        "/etiquetes",
        "/labels",
        "/valoracion",
        "/valoracio",
        "/valuation",
        "/reposicion",
        "/reposicio",
        "/replenishment",
        "/desglose",
        "/breakdown",
        "/worksheet",
        "/imprimir-recuento"
    ];

    public static string GetPreferredLanguage(HttpRequest? request, string? fallbackLanguage = null)
    {
        var fallback = AppLanguages.Normalize(fallbackLanguage);
        if (request?.Cookies.TryGetValue(CookieName, out var cookieValue) == true)
        {
            return AppLanguages.Normalize(cookieValue);
        }

        return fallback;
    }

    public static bool UsesDocumentLanguage(PathString path)
    {
        return UsesDocumentLanguage(path.Value);
    }

    public static bool UsesDocumentLanguage(string? pathOrUrl)
    {
        var value = pathOrUrl;
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        if (Uri.TryCreate(value, UriKind.Absolute, out var absoluteUri))
        {
            value = absoluteUri.PathAndQuery;
        }

        foreach (var marker in DocumentRouteMarkers)
        {
            if (value.Contains(marker, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }
}
