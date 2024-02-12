using BlogApi.Models;
using BlogApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {

        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }
        

        [Authorize]
        [HttpGet]
        public async Task<List<Post>> Get() => await _postService.GetAllAsync();

        [Authorize]
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Post>> Get(string id)
        {
            var post = await _postService.GetAsync(id);

            if (post is null)
            {
                return NotFound();
            }

            return post;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(Post newPost)
        {
            await _postService.CreateAsync(newPost);
            return CreatedAtAction(nameof(Get), new { id = newPost.Id }, newPost);
        }

        [Authorize]
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Post updatedPost)
        {
            var post = await _postService.GetAsync(id);

            if (post is null)
            {
                return NotFound();
            }

            await _postService.UpdateAsync(id, updatedPost);

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var post = await _postService.GetAsync(id);

            if (post is null)
            {
                return NotFound();
            }

            await _postService.DeleteAsync(id);

            return NoContent();
        }

        [Authorize]
        [HttpGet("test")]
        public ActionResult<string> Test()
        {
            return "API is working!";
        }
    }
}
