using Codepulse.API.Application.DTOs.Category;

namespace Codepulse.API.Application.Features.Categ.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryToDisplayDto> CreateAsync(CategoryToCreateDto dto);
        Task<List<CategoryToDisplayDto>> GetAllAsync(string? query, string? sortBy, string? sortDirection, int? pageNumber, int? pageSize);
        Task<CategoryToDisplayDto?> GetByIdAsync(long id);
        Task<CategoryToDisplayDto?> UpdateAsync(long id, CategoryToUpdateDto categoryToUpdateDto);
        Task<CategoryToDisplayDto?> DeleteAsync(long id);
        Task<int> CountAsync();

    }
}
