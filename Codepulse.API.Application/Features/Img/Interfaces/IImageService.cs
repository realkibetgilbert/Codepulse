using Codepulse.API.Application.DTOs.ImageUpload;
using Microsoft.AspNetCore.Http;

namespace Codepulse.API.Application.Features.Img.Interfaces
{
    public interface IImageService
    {
        Task<BlogImageToDisplay> UploadImageAsync(IFormFile file, string fileName, string title);
        Task<List<BlogImageToDisplay>> GetAllImagesAsync();
    }
}
