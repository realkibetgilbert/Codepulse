using Codepulse.API.Application.Features.Img.Services;
using Codepulse.API.Application.Mappers.Img.Interfaces;
using Codepulse.API.Domain.Interfaces;
using Codepulse.API.Test.Services.Image.TestData;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Codepulse.API.Test.Services.Image
{
    public class ImageServiceTests
    {
        private readonly Mock<IImageRepository> _imageRepoMock;
        private readonly Mock<IImageMapper> _imageMapperMock;
        private readonly ImageService _imageService;

        public ImageServiceTests()
        {
            _imageRepoMock = new Mock<IImageRepository>();
            _imageMapperMock = new Mock<IImageMapper>();
            _imageService = new ImageService(_imageRepoMock.Object, _imageMapperMock.Object);
        }

        [Theory]
        [MemberData(nameof(InvalidFileTestData.InvalidFiles), MemberType = typeof(InvalidFileTestData))]
        public async Task UploadImageAsync_InvalidFile_ThrowsArgumentException(IFormFile invalidFile)
        {
            // Act
            var act = async () => await _imageService.UploadImageAsync(invalidFile, "testfile", "title");

            // Assert
            await act.Should().ThrowAsync<ArgumentException>()
                     .WithMessage("*unsupported file format*");
        }

    }
}
