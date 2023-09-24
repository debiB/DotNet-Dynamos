using System;
using BlogWebApi.Data;
using BlogWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BlogWebApi.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class CommentController: ControllerBase
	{
        private BlogWebApiDbContext _context;
        public CommentController(BlogWebApiDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int postid)
        {
            var comment = await _context.Comment.FirstOrDefaultAsync(x => x.PostId == postid);
            if (comment == null)
            {
                return BadRequest(error: "invalid id");
            }
            return Ok(comment);
        }
        [HttpGet(template: "{id:int}")]
        public async Task<IActionResult> GetWithId(int id)
        {
            var comment = await _context.Comment.FirstOrDefaultAsync(x => x.CommentId == id);
            if (comment == null)
            {
                return BadRequest(error: "invalid id");
            }
            return Ok(comment);
        }
        [HttpPost]
        public async Task<IActionResult> post(int postid, string content)
        {
            if (string.IsNullOrEmpty(content))
                return BadRequest("Input can not be empty");
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == postid);
            if(post == null)
            {
                return BadRequest("Not found");
            }
            var _comment = new Comment
            {
                PostId = postid,
                Text = content
            };
            await _context.Comment.AddAsync(_comment);
            await _context.SaveChangesAsync();
            return CreatedAtAction("Get", routeValues: _comment.CommentId, value: _comment);
        }
        [HttpPatch]
        public async Task<IActionResult> Patch(int id,  string content)
        {
            content = content.Trim();
            if (string.IsNullOrEmpty(content))
            {
                return BadRequest("input can not be empty");
            }
            var filtered = await _context.Comment.FirstOrDefaultAsync(x => x.CommentId == id);
            if (filtered == null)
            {
                return BadRequest(error: "Invalid id");
            }
    
            filtered.Text = content;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var filtered = await _context.Comment.FirstOrDefaultAsync(x => x.CommentId == id);
            if (filtered == null)
            {
                return BadRequest(error: "Invalid id");
            }
            _context.Comment.Remove(filtered);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}

