namespace ExcessInventoryManagement.Models
{
    public class DailyMetrics
    {
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public int? ProductCount { get; set; }
        public int UnitsSold {  get; set; }
        public decimal? Margin { get; set; }
        public decimal? Profit { get; set; }
        public int? RemainingInventory { get; set; }
    }
}
