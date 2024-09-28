using System.ComponentModel.DataAnnotations.Schema;

namespace APITutorials.DTOs.Stock
{
    public class CreateStockRequestDto
    {
        public string? Symbol { get; set; }
        public string? CompanyName { get; set; }
        public decimal Purchase { get; set; }
        public decimal LastDiv { get; set; }
        public string? Industry { get; set; }
        public long MarketCap { get; set; }
    }
}
