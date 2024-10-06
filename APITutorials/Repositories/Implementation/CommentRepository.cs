using APITutorials.Data;
using APITutorials.DTOs.Comment;
using APITutorials.DTOs.Stock;
using APITutorials.Helper;
using APITutorials.Models;
using APITutorials.Repositories.Interface;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.ValueContentAnalysis;
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

        public async Task<Comment> CreateAsync(Comment commentModel)
        {
            commentModel.Id = Guid.NewGuid();
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();

            return commentModel;
        }

        public async Task<Comment?> DeleteAsync(Guid id)
        {
            var commentModel = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (commentModel == null)
            {
                return null;
            }

            _context.Comments.Remove(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments
                .Include(a => a.AppUser).ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(Guid id)
        {
            return await _context.Comments
                .Include(a => a.AppUser).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Comment>> SearchAsync(CommentQueryObject queryObject)
        {
            var comments = _context.Comments.Include(a => a.AppUser).AsQueryable();
            if (!string.IsNullOrWhiteSpace(queryObject.Symbol))
            {
                comments = comments.Where(s => s.Stock!.Symbol == queryObject.Symbol);
            }
            if (queryObject.IsDecsending == true)
            {
                comments = comments.OrderByDescending(c => c.CreatedOn);
            }
            
            var skipNumber = (queryObject.PageNumber - 1) * queryObject.PageSize;
            var results = await comments.Skip(skipNumber).Take(queryObject.PageSize).ToListAsync();

            return results;
        }

        public async Task<Comment?> UpdateAsync(Guid id, Comment commentModel)
        {
            var existingComment = await _context.Comments.FindAsync(id);
            if (existingComment == null)
            {
                return null;
            }

            existingComment.Title = commentModel.Title;
            existingComment.Content = commentModel.Content;

            await _context.SaveChangesAsync();
            return existingComment;
        }
    }
}
