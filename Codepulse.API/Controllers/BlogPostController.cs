using Codepulse.API.Application.DTOs.BlogPost;
using Codepulse.API.Application.Features.Blog.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Codepulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostService _blogPostService;
        public BlogPostController(IBlogPostService blogPostService)
        {
            _blogPostService = blogPostService;
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateBlogPostRequestDto createBlogPostRequestDto)
        {
            var createdPost = await _blogPostService.CreateAsync(createBlogPostRequestDto);
            return Ok(createdPost);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var blogPostToDisplays = await _blogPostService.GetAllAsync();
            return Ok(blogPostToDisplays);
        }

        [HttpGet]
        [Route("{id:long}")]
        public async Task<IActionResult> GetById([FromRoute] long id)
        {
            var blogPost = await _blogPostService.GetByIdAsync(id);

            if (blogPost == null)
                return NotFound();

            return Ok(blogPost);
        }

        [HttpGet]
        [Route("{urlHandle}")]
        public async Task<IActionResult> GetByUrlHandle([FromRoute] string urlHandle)
        {
            var blogPost = await _blogPostService.GetByUrlHandleAsync(urlHandle);

            if (blogPost == null)
                return NotFound();

            return Ok(blogPost);
        }

        [HttpPut]
        [Route("{id:long}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] BlogPostToUpdate blogPostToUpdate)
        {
            var updatedBlogPost = await _blogPostService.UpdateAsync(id, blogPostToUpdate);

            if (updatedBlogPost == null)
                return NotFound();

            return Ok(updatedBlogPost);
        }

        [HttpDelete]
        [Route("{id:long}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            var blogPost = await _blogPostService.DeleteAsync(id);

            if (blogPost == null)
                return NotFound();

            return Ok(blogPost);
        }
    }
}
