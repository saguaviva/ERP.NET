namespace Erp.Application.Muestras;

public sealed class SaveMuestraCommand
{
    public Guid TenantId { get; set; }
    public Guid CompanyId { get; set; }
    public bool IsNew { get; set; }
    public string? Code { get; set; }
    public string Description { get; set; } = string.Empty;
    public int ClientCode { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public string Reference { get; set; } = string.Empty;
    public string Season { get; set; } = string.Empty;
    public int MachineCode { get; set; }
    public string MachineName { get; set; } = string.Empty;
    public decimal MarginPercent { get; set; }
    public string VatCode { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string Composition { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public List<SaveMuestraLineInput> Lines { get; set; } = [];
    public List<SaveMuestraBreakdownInput> Breakdowns { get; set; } = [];
}
