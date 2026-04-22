namespace Erp.Application.Models;

public sealed class SaveModeloCommand
{
    public Guid TenantId { get; set; }
    public Guid CompanyId { get; set; }
    public Guid? Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Series { get; set; } = string.Empty;
    public string Season { get; set; } = string.Empty;
    public int ClientCode { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ModelCode { get; set; } = string.Empty;
    public string FabricCode { get; set; } = string.Empty;
    public string FabricDescription { get; set; } = string.Empty;
    public int SupplierCode { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public string WidthText { get; set; } = string.Empty;
    public int DyeingWorkshopCode { get; set; }
    public string DyeingWorkshopName { get; set; } = string.Empty;
    public int FinishingWorkshopCode { get; set; }
    public string FinishingWorkshopName { get; set; } = string.Empty;
    public int PrintingWorkshopCode { get; set; }
    public string PrintingWorkshopName { get; set; } = string.Empty;
    public int TailoringWorkshopCode { get; set; }
    public string TailoringWorkshopName { get; set; } = string.Empty;
    public decimal Yield { get; set; }
    public string FornituraSummary { get; set; } = string.Empty;
    public string PrintingCode1 { get; set; } = string.Empty;
    public string PrintingCode2 { get; set; } = string.Empty;
    public decimal PrintingUnits1 { get; set; }
    public decimal PrintingUnits2 { get; set; }
    public decimal TailoringPrice { get; set; }
    public decimal PlatePrice { get; set; }
    public decimal ReviewPrice { get; set; }
    public decimal TransferQuantity { get; set; }
    public decimal TransferPrice { get; set; }
    public decimal FlockedQuantity { get; set; }
    public decimal FlockedPrice { get; set; }
    public decimal EmbroideredQuantity { get; set; }
    public decimal EmbroideredPrice { get; set; }
    public decimal PrintingPrice { get; set; }
    public decimal DyeingPrice { get; set; }
    public decimal FinishingPrice { get; set; }
    public decimal FornituraPrice { get; set; }
    public decimal ManipulationPrice { get; set; }
    public decimal PackagingPrice { get; set; }
    public decimal CostPrice { get; set; }
    public decimal MarginPercent { get; set; }
    public decimal SalePrice { get; set; }
    public decimal FinalSalePrice { get; set; }
    public string Notes { get; set; } = string.Empty;
    public string VatCode { get; set; } = string.Empty;
    public string Size01 { get; set; } = string.Empty;
    public string Size02 { get; set; } = string.Empty;
    public string Size03 { get; set; } = string.Empty;
    public string Size04 { get; set; } = string.Empty;
    public string Size05 { get; set; } = string.Empty;
    public string Size06 { get; set; } = string.Empty;
    public string Size07 { get; set; } = string.Empty;
    public string Size08 { get; set; } = string.Empty;
    public string Size09 { get; set; } = string.Empty;
    public string Size10 { get; set; } = string.Empty;
    public List<SaveModeloScandalloLineInput> ScandalloLines { get; set; } = [];
    public List<SaveModeloColorLineInput> ColorLines { get; set; } = [];
    public List<SaveModeloFornituraLineInput> FornituraLines { get; set; } = [];
    public List<SaveModeloStockLineInput> StockLines { get; set; } = [];
}
