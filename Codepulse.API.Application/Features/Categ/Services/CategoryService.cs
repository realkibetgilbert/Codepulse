using AutoMapper;
using Codepulse.API.Application.DTOs.Category;
using Codepulse.API.Application.Features.Categ.Interfaces;
using Codepulse.API.Domain.Entities;
using Codepulse.API.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Codepulse.API.Application.Features.Categ.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _repository;

        public CategoryService(IMapper mapper, ICategoryRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CategoryToDisplayDto> CreateAsync(CategoryToCreateDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            await _repository.CreateAsync(category);
            return _mapper.Map<CategoryToDisplayDto>(category);
        }
        public async Task<List<CategoryToDisplayDto>> GetAllAsync(string? query, string? sortBy, string? sortDirection, int? pageNumber, int? pageSize)
        {
            var categories = await _repository.GetAllAsync(query, sortBy, sortDirection, pageNumber, pageSize);
            return _mapper.Map<List<CategoryToDisplayDto>>(categories);
        }
        public async Task<CategoryToDisplayDto?> GetByIdAsync(long id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null) return null;

            return _mapper.Map<CategoryToDisplayDto>(category);
        }
        public async Task<CategoryToDisplayDto?> UpdateAsync(long id, CategoryToUpdateDto categoryToUpdateDto)
        {
            var categoryDomain = _mapper.Map<Category>(categoryToUpdateDto);
            categoryDomain.Id = id;

            var updatedCategory = await _repository.UpdateAsync(categoryDomain);
            if (updatedCategory == null)
            {
                return null;
            }

            return _mapper.Map<CategoryToDisplayDto>(updatedCategory);
        }
        public async Task<CategoryToDisplayDto?> DeleteAsync(long id)
        {
            var deletedCategory = await _repository.DeleteAsync(id);

            if (deletedCategory == null)
                return null;

            return _mapper.Map<CategoryToDisplayDto>(deletedCategory);
        }
        public async Task<int> CountAsync()
        {
            return await _repository.GetCountAsync();
        }

    }
}
