

using Codepulse.API.Domain.Entities;

namespace Codepulse.API.Domain.Interfaces
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> CreateAsync(BlogPost blogPost);
        Task<List<BlogPost>> GetAllAsync();
        Task<BlogPost?> GetByIdAsync(long id);
        Task<BlogPost?> GetByUrlAsync(string urlHandle);
        Task<BlogPost?> UpdateAsync(BlogPost category);
        Task<BlogPost?> DeleteAsync(long id);
    }
}
