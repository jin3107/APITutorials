namespace APITutorials.DTOs.Comment
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public Guid? StockId { get; set; }
    }
}
