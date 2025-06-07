using Codepulse.API.Domain.Entities;

namespace Codepulse.API.Domain.Interfaces

{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);
        Task<List<Category>> GetAllAsync(string? query = null, string? sortBy = null, string? sortDirection = null, int? pageNumber = 1, int? pageSize = 10);
        Task<Category?> GetByIdAsync(long id);
        Task<Category?> UpdateAsync(Category category);
        Task<Category?> DeleteAsync(long id);
        Task<int> GetCountAsync();
    }
}
