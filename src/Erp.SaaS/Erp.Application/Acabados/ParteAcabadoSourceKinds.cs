namespace Erp.Application.Acabados;

public static class ParteAcabadoSourceKinds
{
    public const string Sample = "sample";
    public const string Complement = "complement";

    public static string Normalize(string? value, bool hasLinkedSource = false)
    {
        var normalized = (value ?? string.Empty).Trim().ToLowerInvariant();
        return normalized switch
        {
            Complement => Complement,
            Sample => Sample,
            _ when hasLinkedSource => Sample,
            _ => string.Empty
        };
    }

    public static string GetRouteBase(string? value) =>
        Normalize(value, hasLinkedSource: true) == Complement
            ? "/articulos/complementos"
            : "/articulos/muestras";

    public static string GetLabel(string? value) =>
        Normalize(value, hasLinkedSource: true) == Complement
            ? "Complemento"
            : "Muestra";
}
