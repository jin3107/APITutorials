namespace APITutorials.Helper
{
    public class CommentQueryObject
    {
        public string? Symbol { get; set; }
        public bool IsDecsending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
