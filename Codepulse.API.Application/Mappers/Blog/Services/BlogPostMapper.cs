using AutoMapper;
using Codepulse.API.Application.DTOs.BlogPost;
using Codepulse.API.Application.Mappers.Blog.Interfaces;
using Codepulse.API.Domain.Entities;

namespace Codepulse.API.Application.Mappers.Blog.Services
{
    public class BlogPostMapper : IBlogPostMapper
    {
        private readonly IMapper _mapper;

        public BlogPostMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public BlogPost MapToDomain(CreateBlogPostRequestDto dto)
            => _mapper.Map<BlogPost>(dto);

        public BlogPost MapToDomain(BlogPostToUpdate dto)
            => _mapper.Map<BlogPost>(dto);

        public BlogPostToDisplay MapToDto(BlogPost blogPost)
            => _mapper.Map<BlogPostToDisplay>(blogPost);

        public List<BlogPostToDisplay> MapToDto(IEnumerable<BlogPost> blogPosts)
            => _mapper.Map<List<BlogPostToDisplay>>(blogPosts);
    }

}
