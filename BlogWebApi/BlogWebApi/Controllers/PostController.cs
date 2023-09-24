using System;
using BlogWebApi.Data;
using BlogWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BlogWebApi.Controllers
{
    [Route(template: "api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private BlogWebApiDbContext _context;
        public PostController(BlogWebApiDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var post = await _context.Posts.ToListAsync();
            return Ok(post);
        }
        [HttpGet(template: "{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == id);
            if (post == null)
            {
                return BadRequest(error: "invalid id");
            }
            return Ok(post);
        }
        [HttpPost]
        public async Task<IActionResult> post(string title, string content)
        {
            if(string.IsNullOrEmpty(title) || string.IsNullOrEmpty(content))
            {
                return BadRequest("Input empty.");
            }
            var _post = new Post
            {
                Title = title,
                Content = content
            };
            await _context.Posts.AddAsync(_post);
            await _context.SaveChangesAsync();
            return CreatedAtAction("Get", routeValues: _post.PostId, value: _post);
        }
        [HttpPatch("edit title")]
        public async Task<IActionResult> PatchTitle(int id, string title)
        {
            title = title.Trim();
            if (string.IsNullOrEmpty(title))
            {
                return BadRequest("input can not be empty");
            }
            Post? filtered = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == id);
            if (filtered is null)
            {
                return BadRequest(error: "Invalid id");
            }

            filtered.Title = title;
            await _context.SaveChangesAsync();
            return NoContent();
       }
        [HttpPatch("edit content")]
        public async Task<IActionResult> Patch(int id, string content)
        {
            content = content.Trim();
            if (string.IsNullOrEmpty(content))
            {
                return BadRequest("input can not be empty");
            }
            Post? filtered = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == id);
            if(filtered is null)
            {
                return BadRequest(error: "Invalid id");
            }
      
            filtered.Content = content;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var filtered = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == id);
            if (filtered == null)
            {
                return BadRequest(error: "Invalid id");
            }
            _context.Posts.Remove(filtered);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

