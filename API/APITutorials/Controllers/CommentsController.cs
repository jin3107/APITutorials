using APITutorials.DTOs.Comment;
using APITutorials.Extensions;
using APITutorials.Helper;
using APITutorials.Mappers;
using APITutorials.Models;
using APITutorials.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace APITutorials.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comments = await _commentService.GetAllAsync();
            var commentDto = comments.Select(s => s.ToCommentDto());
            return Ok(commentDto);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentService.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{symbol:alpha}")]
        [Authorize]
        public async Task<IActionResult> Create([FromRoute] string symbol, [FromBody] CreateCommentRequestDto commentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userName = User.GetUserName();
            try
            {
                var createdComment = await _commentService.CreateAsync(symbol, commentDto, userName);
                return CreatedAtAction(nameof(GetById), new { id = createdComment.Id }, createdComment.ToCommentDto());
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCommentRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedComment = await _commentService.UpdateAsync(id, updateDto);
            if (updatedComment == null)
            {
                return NotFound("Comment not found");
            }

            return Ok(updatedComment.ToCommentDto());
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var deletedComment = await _commentService.DeleteAsync(id);
            if (deletedComment == null)
            {
                return NotFound("Comment does not exist");
            }

            return NoContent();
        }

        [HttpPost("search")]
        [Authorize]
        public async Task<ActionResult<List<Comment>>> Search([FromBody] CommentQueryObject queryObject)
        {
            var comments = await _commentService.SearchAsync(queryObject);

            if (comments == null || comments.Count == 0)
            {
                return NotFound("No comments found with the specified criteria.");
            }

            return Ok(comments);
        }
    }
}
