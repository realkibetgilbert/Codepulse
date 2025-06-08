using AutoMapper;
using Codepulse.API.Application.DTOs.ImageUpload;
using Codepulse.API.Application.Mappers.Img.Interfaces;
using Codepulse.API.Domain.Entities;

namespace Codepulse.API.Application.Mappers.Img.Services
{
    public class ImageMapper : IImageMapper
    {
        private readonly IMapper _mapper;

        public ImageMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public BlogImageToDisplay MapToDto(BlogImage image)
            => _mapper.Map<BlogImageToDisplay>(image);

        public List<BlogImageToDisplay> MapToDto(IEnumerable<BlogImage> images)
            => _mapper.Map<List<BlogImageToDisplay>>(images.ToList());
    }
}
