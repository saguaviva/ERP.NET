namespace Erp.Application.Tejidos;

public sealed class TejidoFilter
{
    public string Search { get; set; } = string.Empty;
    public int? WeaverCode { get; set; }
    public int? FinisherCode { get; set; }
    public string TubularMode { get; set; } = string.Empty;
    public bool OnlyWithAvailableStock { get; set; }
    public decimal? GramWeightMin { get; set; }
    public decimal? GramWeightMax { get; set; }
    public decimal? WidthMin { get; set; }
    public decimal? WidthMax { get; set; }
    public decimal? PricePerMeterMin { get; set; }
    public decimal? PricePerMeterMax { get; set; }
    public decimal? PricePerKilogramMin { get; set; }
    public decimal? PricePerKilogramMax { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 25;
    public string SortColumn { get; set; } = nameof(TejidoListItemDto.Code);
    public bool SortDescending { get; set; }
}
