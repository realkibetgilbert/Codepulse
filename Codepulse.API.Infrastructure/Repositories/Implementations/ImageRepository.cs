using Codepulse.API.Domain.Entities;
using Codepulse.API.Domain.Interfaces;
using Codepulse.API.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Codepulse.API.Infrastructure.Repositories.Implementations
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly CodepulseDbContext _context;

        public ImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, CodepulseDbContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<List<BlogImage>> GetAll()
        {
            return await _context.BlogImages.ToListAsync();
        }

        public async Task<BlogImage> Upload(IFormFile formFile, BlogImage blogImage)
        {
            //UPLOAD IMAGE TO API FOLDER......

            var localPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", $"{blogImage.FileName}{blogImage.FileExtension}");

            using var stream = new FileStream(localPath, FileMode.Create);

            await formFile.CopyToAsync(stream);

            //update the database......


            // https://codepulse/images/filaname.jpeg
            var httpRequest = _httpContextAccessor.HttpContext.Request;
            var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{blogImage.FileName}{blogImage.FileExtension}";
            blogImage.Url = urlPath;
            await _context.BlogImages.AddAsync(blogImage);
            await _context.SaveChangesAsync();
            return blogImage;

        }
    }
}
