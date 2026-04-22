namespace Erp.Application.BaseData;

public static class BaseCatalogKeys
{
    public const string BanksCashboxes = "bancos-cajas";
    public const string PaymentMethods = "formas-pago";
    public const string Operations = "operacions";
    public const string Machines = "maquinas";
    public const string Seasons = "temporadas";
    public const string VatTypes = "tipos-iva";
    public const string Incoterms = "incoterms";

    public static readonly IReadOnlyCollection<string> All =
    [
        BanksCashboxes,
        PaymentMethods,
        Operations,
        Machines,
        Seasons,
        VatTypes,
        Incoterms
    ];
}
