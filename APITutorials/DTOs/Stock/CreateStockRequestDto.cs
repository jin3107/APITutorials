using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APITutorials.DTOs.Stock
{
    public class CreateStockRequestDto
    {
        [Required]
        [MaxLength(250, ErrorMessage = "Synbol cannot be over 250 characters")]
        public string? Symbol { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage = "Campany Name cannot be over 250 characters")]
        public string? CompanyName { get; set; }

        [Required]
        [Range(1, 9999999999999)]
        public decimal Purchase { get; set; }

        [Required]
        [Range(0.001, 999)]
        public decimal LastDiv { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage = "Industry cannot be over 250 characters")]
        public string? Industry { get; set; }

        [Range(1, 9999999999999)]
        public long MarketCap { get; set; }
    }
}
