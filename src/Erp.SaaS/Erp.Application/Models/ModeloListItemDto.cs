namespace Erp.Application.Models;

public sealed class ModeloListItemDto
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Series { get; set; } = string.Empty;
    public string Season { get; set; } = string.Empty;
    public int ClientCode { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string FabricCode { get; set; } = string.Empty;
    public string FabricDescription { get; set; } = string.Empty;
    public decimal CostPrice { get; set; }
    public decimal SalePrice { get; set; }
    public decimal FinalSalePrice { get; set; }
    public string Origin { get; set; } = string.Empty;
    public int ColorsCount { get; set; }
    public int ScandalloLinesCount { get; set; }
    public int ForniturasCount { get; set; }
}
