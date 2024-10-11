using APITutorials.DTOs.Comment;
using APITutorials.Helper;
using APITutorials.Models;

namespace APITutorials.Services.Interface
{
    public interface ICommentService
    {
        Task<Comment> CreateAsync(string symbol, CreateCommentRequestDto commentDto, string userName);
        Task<Comment?> DeleteAsync(Guid id);
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(Guid id);
        Task<List<Comment>> SearchAsync(CommentQueryObject queryObject);
        Task<Comment?> UpdateAsync(Guid id, UpdateCommentRequestDto updateDto);
    }
}
