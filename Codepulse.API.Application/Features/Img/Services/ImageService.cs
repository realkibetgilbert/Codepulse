using Codepulse.API.Application.DTOs.ImageUpload;
using Codepulse.API.Application.Features.Img.Interfaces;
using Codepulse.API.Application.Mappers.Img.Interfaces;
using Codepulse.API.Domain.Entities;
using Codepulse.API.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Codepulse.API.Application.Features.Img.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly IImageMapper _mapper;

        public ImageService(IImageRepository imageRepository, IImageMapper mapper)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        public async Task<BlogImageToDisplay> UploadImageAsync(IFormFile file, string fileName, string title)
        {
            ValidateFileUpload(file); // <- Moved here

            var blogImage = new BlogImage
            {
                FileExtension = Path.GetExtension(file.FileName).ToLower(),
                FileName = fileName,
                Title = title,
                DateCreated = DateTime.UtcNow
            };

            await _imageRepository.Upload(file, blogImage);

            return _mapper.MapToDto(blogImage);
        }
        public async Task<List<BlogImageToDisplay>> GetAllImagesAsync()
        {
            var images = await _imageRepository.GetAll();
            return _mapper.MapToDto(images);
        }
        private void ValidateFileUpload(IFormFile file)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

            var extension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
            {
                throw new ArgumentException("Unsupported file format. Allowed formats are .jpg, .jpeg, .png");
            }

            if (file.Length > 10 * 1024 * 1024) // 10 MB
            {
                throw new ArgumentException("File size cannot be greater than 10MB");
            }

        }
    }
}