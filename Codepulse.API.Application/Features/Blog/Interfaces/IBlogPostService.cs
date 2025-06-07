using Codepulse.API.Application.DTOs.BlogPost;

namespace Codepulse.API.Application.Features.Blog.Interfaces
{
    public interface IBlogPostService
    {
        Task<BlogPostToDisplay> CreateAsync(CreateBlogPostRequestDto createBlogPostRequestDto);
        Task<IEnumerable<BlogPostToDisplay>> GetAllAsync();
        Task<BlogPostToDisplay?> GetByIdAsync(long id);
        Task<BlogPostToDisplay?> GetByUrlHandleAsync(string urlHandle);
        Task<BlogPostToDisplay?> UpdateAsync(long id, BlogPostToUpdate blogPostToUpdate);
        Task<BlogPostToDisplay?> DeleteAsync(long id);


    }
}
