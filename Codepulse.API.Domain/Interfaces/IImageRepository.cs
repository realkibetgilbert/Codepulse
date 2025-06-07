using Codepulse.API.Domain.Entities;
using Microsoft.AspNetCore.Http;
namespace Codepulse.API.Domain.Interfaces
{
    public interface IImageRepository
    {
        Task<BlogImage> Upload(IFormFile formFile, BlogImage blogImage);
        Task<List<BlogImage>> GetAll();
    }
}
