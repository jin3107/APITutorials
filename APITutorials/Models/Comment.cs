using System.ComponentModel.DataAnnotations.Schema;

namespace APITutorials.Models
{
    [Table("Comments")]
    public class Comment
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public Guid? StockId { get; set; }
        public Stock? Stock { get; set; }
    }
}
