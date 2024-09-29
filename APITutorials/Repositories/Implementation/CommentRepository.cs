using APITutorials.Data;
using APITutorials.DTOs.Comment;
using APITutorials.DTOs.Stock;
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

        public async Task<Comment?> CreateAsync(Comment commentModel)
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
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(Guid id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task<Comment?> UpdateAsync(Guid id, UpdateCommentRequestDto commentDto)
        {
            var existingComment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (existingComment == null)
            {
                return null;
            }

            existingComment.Title = commentDto.Title;
            existingComment.Content = commentDto.Content;
            existingComment.CreatedOn = commentDto.CreatedOn;

            await _context.SaveChangesAsync();
            return existingComment;
        }
    }
}
