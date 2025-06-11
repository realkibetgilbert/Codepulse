using AutoMapper;
using Codepulse.API.Application.DTOs.ImageUpload;
using Codepulse.API.Domain.Entities;

namespace Codepulse.API.Application.Utils.MappingProfiles
{
    public class BlogImageMappingProfile:Profile
    {
        public BlogImageMappingProfile()
        {
            CreateMap<BlogImage, BlogImageToDisplay>().ReverseMap();
        }
    }
}
