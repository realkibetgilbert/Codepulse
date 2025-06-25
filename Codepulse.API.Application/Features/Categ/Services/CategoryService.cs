using AutoMapper;
using Codepulse.API.Application.DTOs.Category;
using Codepulse.API.Application.Features.Categ.Interfaces;
using Codepulse.API.Application.Mappers.Categ.Interfaces;
using Codepulse.API.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Codepulse.API.Infrastructure.Catching;

namespace Codepulse.API.Application.Features.Categ.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IDistributedCache _cache;
        private readonly ICategoryRepository _repository;
        private readonly ICategoryMapper _categoryMapper;

        public CategoryService(IDistributedCache cache, ICategoryRepository repository, ICategoryMapper categoryMapper)
        {
            _cache = cache;
            _repository = repository;
            _categoryMapper = categoryMapper;
        }

        public async Task<CategoryToDisplayDto> CreateAsync(CategoryToCreateDto dto)
        {
            var category = _categoryMapper.ToDomain(dto);
            await _repository.CreateAsync(category);
            return _categoryMapper.ToDisplay(category);
        }
        public async Task<List<CategoryToDisplayDto>> GetAllAsync(string? query, string? sortBy, string? sortDirection, int? pageNumber, int? pageSize)
        {
            string cacheKey = $"categories:all:{query}:{sortBy}:{sortDirection}:{pageNumber}:{pageSize}";
            var cached = await _cache.GetOrSetAsync<List<CategoryToDisplayDto>>(
                cacheKey,
                async () =>
                {
                    var categories = await _repository.GetAllAsync(query, sortBy, sortDirection, pageNumber, pageSize);
                    return _categoryMapper.ToDisplayList(categories);
                }
            );
            return cached ?? new List<CategoryToDisplayDto>();
        }
        public async Task<CategoryToDisplayDto?> GetByIdAsync(long id)
        {
            string cacheKey = $"category:{id}";
            var cached = await _cache.GetOrSetAsync<CategoryToDisplayDto>(
                cacheKey,
                async () =>
                {
                    var category = await _repository.GetByIdAsync(id);
                    if (category == null) return null;
                    return _categoryMapper.ToDisplay(category);
                }
            );
            return cached;
        }
        public async Task<CategoryToDisplayDto?> UpdateAsync(long id, CategoryToUpdateDto categoryToUpdateDto)
        {
            var categoryDomain = _categoryMapper.ToDomain(categoryToUpdateDto, id);
            categoryDomain.Id = id;

            var updatedCategory = await _repository.UpdateAsync(categoryDomain);
            if (updatedCategory == null)
            {
                return null;
            }

            return _categoryMapper.ToDisplay(updatedCategory);
        }
        public async Task<CategoryToDisplayDto?> DeleteAsync(long id)
        {
            var deletedCategory = await _repository.DeleteAsync(id);

            if (deletedCategory == null)
                return null;

            return _categoryMapper.ToDisplay(deletedCategory);
        }
        public async Task<int> CountAsync()
        {
            return await _repository.GetCountAsync();
        }

    }
}
