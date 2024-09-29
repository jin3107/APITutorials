using APITutorials.Models;

namespace APITutorials.Repositories.Interface
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
    }
}
