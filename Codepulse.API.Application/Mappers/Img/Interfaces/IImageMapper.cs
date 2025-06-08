using Codepulse.API.Application.DTOs.ImageUpload;
using Codepulse.API.Domain.Entities;

namespace Codepulse.API.Application.Mappers.Img.Interfaces
{
    public interface IImageMapper
    {
        BlogImageToDisplay MapToDto(BlogImage image);
        List<BlogImageToDisplay> MapToDto(IEnumerable<BlogImage> images);
    }
}
