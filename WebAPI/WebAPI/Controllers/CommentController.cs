using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;
using WebAPI.Interface;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IComment _comment;
        private readonly IStock _stock;

        public CommentController(IMapper mapper, IComment comment, IStock stock)
        {
            _mapper = mapper;
            _comment = comment;
            _stock = stock;
        }

        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            var comments = _mapper.Map<List<CommentDto>>(await _comment.GetCommentsAsync());

            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!comments.Any()) return NotFound();

            return Ok(comments);
        }
        [HttpGet("{commentId}")]
        public async Task<IActionResult> GetById([FromRoute] int commentId)
        {
            if (!await _comment.CommentExistAsync(commentId)) return NotFound();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var comment = _mapper.Map<CommentDto>(await _comment.GetCommentAsync(commentId));
            if (comment == null) return NotFound();

            return Ok(comment);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateComment([FromBody] CommentDto commentCreate)
        {
            if (commentCreate == null) return BadRequest();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var commentMap = _mapper.Map<Comment>(commentCreate);

            if (!await _comment.CreateCommentAsync(commentMap))
            {
                ModelState.AddModelError("", "Something went wrong in creating the comment ");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Created!");

        }

        [HttpPatch("{commentId:int}/UpdatePartial")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> PatchComment(int commentId, [FromBody] JsonPatchDocument<CommentDto> commentUpdate)
        {


            if (!await _comment.CommentExistAsync(commentId))
            {
                return NotFound();
            }

            var comment = await _comment.GetCommentAsync(commentId);
            if (comment == null)
            {
                return NotFound();
            }

            var commentDto = _mapper.Map<CommentDto>(comment);
            commentUpdate.ApplyTo(commentDto, ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            _mapper.Map(commentDto, comment);
            if (!await _comment.UpdateCommentAsync(comment))
            {
                ModelState.AddModelError("", "Something went wrong while updating the comment");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfulyy Updated");
        }


        [HttpDelete("{commentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            if (!await _comment.CommentExistAsync(commentId)) return NotFound();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var commentDelete = await _comment.GetCommentAsync(commentId);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!await _comment.DeleteCommentAsync(commentDelete))
            {
                ModelState.AddModelError("", "Something went wrong in deleting the comment");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Deleted");
        }
    }
}
