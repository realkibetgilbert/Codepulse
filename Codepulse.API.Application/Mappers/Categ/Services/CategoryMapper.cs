using AutoMapper;
using Codepulse.API.Application.DTOs.Category;
using Codepulse.API.Application.Mappers.Categ.Interfaces;
using Codepulse.API.Domain.Entities;

namespace Codepulse.API.Application.Mappers.Categ.Services
{

    public class CategoryMapper : ICategoryMapper
    {
        private readonly IMapper _mapper;

        public CategoryMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Category ToDomain(CategoryToCreateDto dto)
        {
            return _mapper.Map<Category>(dto);
        }

        public Category ToDomain(CategoryToUpdateDto dto, long id)
        {
            var category = _mapper.Map<Category>(dto);
            category.Id = id;
            return category;
        }

        public CategoryToDisplayDto ToDisplay(Category category)
        {
            return _mapper.Map<CategoryToDisplayDto>(category);
        }

        public List<CategoryToDisplayDto> ToDisplayList(IEnumerable<Category> categories)
        {
            return _mapper.Map<List<CategoryToDisplayDto>>(categories);
        }

    }

}