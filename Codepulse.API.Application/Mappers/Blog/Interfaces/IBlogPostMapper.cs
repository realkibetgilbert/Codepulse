using Codepulse.API.Application.DTOs.BlogPost;
using Codepulse.API.Domain.Entities;

namespace Codepulse.API.Application.Mappers.Blog.Interfaces
{
    public interface IBlogPostMapper
    {
        BlogPost MapToDomain(CreateBlogPostRequestDto dto);
        BlogPost MapToDomain(BlogPostToUpdate dto);
        BlogPostToDisplay MapToDto(BlogPost blogPost);
        List<BlogPostToDisplay> MapToDto(IEnumerable<BlogPost> blogPosts);
    }
}
