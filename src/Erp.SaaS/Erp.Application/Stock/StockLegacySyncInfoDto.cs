namespace Erp.Application.Stock;

public sealed class StockLegacySyncInfoDto
{
    public bool IsActive { get; set; }
    public string LastStatus { get; set; } = string.Empty;
    public DateTime? LastCompletedUtc { get; set; }
    public int LastInserted { get; set; }
    public int LastUpdated { get; set; }
    public int LastSkipped { get; set; }
    public int LastErrors { get; set; }
}
