using APITutorials.DTOs.Comment;
using APITutorials.Helper;
using APITutorials.Mappers;
using APITutorials.Models;
using APITutorials.Repositories.Interface;
using APITutorials.Services.Interface;
using Microsoft.AspNetCore.Identity;

namespace APITutorials.Services.Implementation
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFMPService _fMPService;

        public CommentService(
            ICommentRepository commentRepository,
            IStockRepository stockRepository,
            UserManager<AppUser> userManager,
            IFMPService fMPService)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
            _userManager = userManager;
            _fMPService = fMPService;
        }

        public async Task<Comment> CreateAsync(string symbol, CreateCommentRequestDto commentDto, string userName)
        {
            var stock = await _stockRepository.GetBySymbolAsync(symbol);
            if (stock == null)
            {
                stock = await _fMPService.FindStockBySymbolAsync(symbol);
                if (stock == null)
                {
                    throw new ArgumentException("Stock does not exist");
                }
                else
                {
                    await _stockRepository.CreateAsync(stock);
                }
            }

            var appUser = await _userManager.FindByNameAsync(userName);
            if (appUser == null)
            {
                throw new ArgumentException("User not found");
            }

            var commentModel = commentDto.ToCommentFromCreate(stock.Id);
            commentModel.AppUserId = appUser.Id;
            return await _commentRepository.CreateAsync(commentModel);
        }

        public async Task<Comment?> DeleteAsync(Guid id)
        {
            return await _commentRepository.DeleteAsync(id);
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _commentRepository.GetAllAsync();
        }

        public async Task<Comment?> GetByIdAsync(Guid id)
        {
            return await _commentRepository.GetByIdAsync(id);
        }

        public async Task<List<Comment>> SearchAsync(CommentQueryObject queryObject)
        {
            return await _commentRepository.SearchAsync(queryObject);
        }

        public async Task<Comment?> UpdateAsync(Guid id, UpdateCommentRequestDto updateDto)
        {
            var commentModel = updateDto.ToCommentFromUpdate();
            return await _commentRepository.UpdateAsync(id, commentModel);
        }
    }
}
