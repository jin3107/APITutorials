namespace APITutorials.Helper
{
    public class QueryObject
    {
        public string? Symbol { get; set; }
        public string? CompanyName { get; set; }
        public decimal Purchase { get; set; }
        public decimal LastDiv { get; set; }
        public string? Industry { get; set; }
        public long MarketCap { get; set; }
        public string? SortBy { get; set; }
        public bool IsDecsending { get; set; } = false;
    }
}
