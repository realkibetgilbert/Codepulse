using Codepulse.API.Application.Features.Img.Services;
using Codepulse.API.Application.Mappers.Img.Interfaces;
using Codepulse.API.Domain.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Codepulse.API.Test.ServicesTests
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

        [Fact]
        public async Task UploadImageAsync_InvalidFile_ThrowsArgumentException()
        {
            // Arrange
            var invalidFile = new FormFile(new MemoryStream(new byte[0]), 0, 0, "Data", "badfile.txt")
            {
                Headers = new HeaderDictionary(),
                ContentType = "text/plain"
            };

            // Act
            var act = async () => await _imageService.UploadImageAsync(invalidFile, "file", "title");

            // Assert
            await act.Should().ThrowAsync<ArgumentException>()
                      .WithMessage("*unsupported file format*");
        }

    }
}
