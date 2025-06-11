using AutoMapper;
using Codepulse.API.Application.DTOs.BlogPost;
using Codepulse.API.Domain.Entities;

namespace Codepulse.API.Application.Utils.MappingProfiles
{
    public class BlogPostMappingProfile:Profile
    {
        public BlogPostMappingProfile()
        {
            CreateMap<CreateBlogPostRequestDto, BlogPost>().ForMember(dest => dest.Categories, opt => opt.Ignore());
            CreateMap<BlogPostToUpdate, BlogPost>().ForMember(dest => dest.Categories, opt => opt.Ignore());
            CreateMap<BlogPost, BlogPostToDisplay>().ReverseMap();
        }
    }
}
