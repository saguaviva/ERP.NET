namespace Erp.Application.Acabados;

public static class ParteAcabadoSourceKinds
{
    public const string Sample = "sample";
    public const string Complement = "complement";
    public const string Model = "model";

    public static string Normalize(string? value, bool hasLinkedSource = false)
    {
        var normalized = (value ?? string.Empty).Trim().ToLowerInvariant();
        return normalized switch
        {
            Complement => Complement,
            Model => Model,
            Sample => Sample,
            _ when hasLinkedSource => Sample,
            _ => string.Empty
        };
    }

    public static string GetRouteBase(string? value) =>
        Normalize(value, hasLinkedSource: true) switch
        {
            Complement => "/articulos/complementos",
            Model => "/articulos/models",
            _ => "/articulos/muestras"
        };

    public static string GetLabel(string? value) =>
        Normalize(value, hasLinkedSource: true) switch
        {
            Complement => "Complemento",
            Model => "Model",
            _ => "Muestra"
        };
}
