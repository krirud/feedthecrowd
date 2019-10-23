using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedTheCrowd.Models;
using FeedTheCrowd.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FeedTheCrowd.Dtos.Comments;

namespace FeedTheCrowd.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentsController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var comments = await _commentService.GetAll();
            return Ok(comments);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] NewCommentDto newCommentDto)
        {
            var createComment = await _commentService.Create(newCommentDto);

            return Ok(createComment);
        }

        [HttpGet("{id}/recipe")]
        public async Task<IActionResult> GetByRecipeId(int id)
        {
            var recipeComments = await _commentService.GetByRecipeId(id);

            return Ok(recipeComments);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            CommentDto comm = new CommentDto();
            if(id > 0)
            {
                comm = await _commentService.GetById(id);
            }
            var success = await _commentService.Delete(id);
            if (success == null)
                return BadRequest("Cannot delete comment");

            return Ok(comm);
        }

        [HttpDelete("{id}/recipe")]
        public async Task<IActionResult> DeleteByRecipeId(int id)
        {
            var success = await _commentService.DeleteByRecipeId(id);
            if (success == null)
                return BadRequest("Cannot delete comments");

            return Ok("deletion is succesful");
        }

        [HttpDelete("{id}/user")]
        public async Task<IActionResult> DeleteByUserId(int id)
        {
            var success = await _commentService.DeleteByUserId(id);
            if (success == null)
                return BadRequest("Cannot delete comments");

            return Ok("deletion is succesful");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] CommentDto newComment)
        {
            await _commentService.Update(id, newComment);
            return NoContent();
        }
    }
}