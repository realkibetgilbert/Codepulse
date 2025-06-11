using AutoMapper;
using Codepulse.API.Application.DTOs.Category;
using Codepulse.API.Domain.Entities;

namespace Codepulse.API.Application.Utils.MappingProfiles
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<CategoryToCreateDto, Category>().ReverseMap();
            CreateMap<Category, CategoryToDisplayDto>().ReverseMap();
            CreateMap<CategoryToUpdateDto, Category>().ReverseMap();
        }
    }
}
