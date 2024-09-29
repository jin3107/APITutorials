using APITutorials.DTOs.Comment;
using APITutorials.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace APITutorials.Mappers
{
    public static class CommentMaper
    {
        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                StockId = commentModel.StockId,
            };
        }
    }
}
