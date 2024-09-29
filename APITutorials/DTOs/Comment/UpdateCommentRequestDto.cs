using System.ComponentModel.DataAnnotations;

namespace APITutorials.DTOs.Comment
{
    public class UpdateCommentRequestDto
    {
        [Required]
        [MaxLength(250, ErrorMessage = "Title cannot be over 250 characters")]
        public string? Title { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage = "Content cannot be over 250 characters")]
        public string? Content { get; set; }
    }
}
