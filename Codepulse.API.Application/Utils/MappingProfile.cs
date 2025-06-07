using AutoMapper;
using Codepulse.API.Application.DTOs.BlogPost;
using Codepulse.API.Application.DTOs.Category;
using Codepulse.API.Application.DTOs.ImageUpload;
using Codepulse.API.Domain.Entities;

namespace Codepulse.API.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryToCreateDto, Category>().ReverseMap();
            CreateMap<Category, CategoryToDisplayDto>().ReverseMap();
            CreateMap<CategoryToUpdateDto, Category>().ReverseMap();
            CreateMap<CreateBlogPostRequestDto, BlogPost>().ForMember(dest => dest.Categories, opt => opt.Ignore());
            CreateMap<BlogPostToUpdate, BlogPost>().ForMember(dest => dest.Categories, opt => opt.Ignore());
            CreateMap<BlogPost, BlogPostToDisplay>().ReverseMap();
            CreateMap<BlogImage, BlogImageToDisplay>().ReverseMap();
        }
    }
}
