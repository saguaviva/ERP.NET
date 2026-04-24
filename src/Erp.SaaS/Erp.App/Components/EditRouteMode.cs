using Microsoft.AspNetCore.Components;

namespace Erp.App.Components;

public static class EditRouteMode
{
    public static bool IsCreateRoute(NavigationManager navigationManager, params string[] createPaths)
    {
        var relativePath = navigationManager.ToBaseRelativePath(navigationManager.Uri);
        var cleanPath = relativePath.Split('?', '#')[0].Trim('/').ToLowerInvariant();

        return createPaths.Any(candidate =>
            string.Equals(cleanPath, candidate.Trim('/').ToLowerInvariant(), StringComparison.Ordinal));
    }
}
