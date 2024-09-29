using APITutorials.Data;
using APITutorials.Models;
using APITutorials.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace APITutorials.Repositories.Implementation
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }
    }
}
