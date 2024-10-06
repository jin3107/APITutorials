using APITutorials.DTOs.Comment;
using APITutorials.DTOs.Stock;
using APITutorials.Extensions;
using APITutorials.Helper;
using APITutorials.Mappers;
using APITutorials.Models;
using APITutorials.Repositories.Interface;
using APITutorials.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace APITutorials.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFMPService _fMPService;

        public CommentsController(ICommentRepository commentRepository, 
            IStockRepository stockRepository, UserManager<AppUser> userManager, IFMPService fMPService)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
            _userManager = userManager;
            _fMPService = fMPService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); 

            var comments = await _commentRepository.GetAllAsync();
            var commentDto = comments.Select(s => s.ToCommentDto());
            return Ok(commentDto);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{symbol:alpha}")]
        public async Task<IActionResult> Create([FromRoute] string symbol, [FromBody] CreateCommentRequestDto commentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await _stockRepository.GetBySymbolAsync(symbol);
            if (stock == null)
            {
                stock = await _fMPService.FindStockBySymbolAsync(symbol);
                if (stock == null)
                {
                    return BadRequest("Stock does not exists");
                }
                else
                {
                    await _stockRepository.CreateAsync(stock);
                }
            }

            var userName = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(userName);

            var commentModel = commentDto.ToCommentFromCreate(stock.Id);
            commentModel.AppUserId = appUser?.Id;
            await _commentRepository.CreateAsync(commentModel);

            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCommentRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepository.UpdateAsync(id, updateDto.ToCommentFromUpdate());
            if (comment == null)
            {
                return NotFound("Comment not found");
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var commentModel = await _commentRepository.DeleteAsync(id);
            if (commentModel == null)
            {
                return NotFound("Comment does not exist");
            }

            return Ok(commentModel);
        }

        [HttpPost("search")]
        public async Task<ActionResult<List<Comment>>> Search([FromBody] CommentQueryObject queryObject)
        {
            var comments = await _commentRepository.SearchAsync(queryObject);

            if (comments == null || comments.Count == 0)
            {
                return NotFound("No comments found with the specified criteria.");
            }

            return Ok(comments);
        }
    }
}
