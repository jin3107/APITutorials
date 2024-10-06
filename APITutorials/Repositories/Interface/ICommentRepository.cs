using APITutorials.DTOs.Comment;
using APITutorials.Helper;
using APITutorials.Models;

namespace APITutorials.Repositories.Interface
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(Guid id);
        Task<Comment> CreateAsync(Comment commentModel);
        Task<Comment?> UpdateAsync(Guid id, Comment commentModel);
        Task<Comment?> DeleteAsync(Guid id);
        Task<List<Comment>> SearchAsync(CommentQueryObject queryObject);
    }
}
