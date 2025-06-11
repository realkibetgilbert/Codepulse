using Microsoft.AspNetCore.Http;

namespace Codepulse.API.Application.DTOs.ImageUpload
{
    public class UploadImageRequest
    {
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string Title { get; set; }
    }
}
