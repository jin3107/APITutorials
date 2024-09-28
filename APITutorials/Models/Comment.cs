namespace APITutorials.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? StockId { get; set; }
        public Stock? Stock { get; set; }
    }
}
