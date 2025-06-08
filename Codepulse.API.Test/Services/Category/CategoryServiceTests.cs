using AutoMapper;
using Codepulse.API.Application.Features.Categ.Services;
using Codepulse.API.Application.Mappers.Categ.Interfaces;
using Codepulse.API.Domain.Interfaces;
using Moq;

namespace Codepulse.API.Test.Services.Category
{
    public class CategoryServiceTests
    {
        private readonly Mock<ICategoryRepository> _repositoryMock;
        private readonly Mock<ICategoryMapper> _mapperMock;
        private readonly CategoryService _service;

        public CategoryServiceTests()
        {
            _repositoryMock = new Mock<ICategoryRepository>();
            _mapperMock = new Mock<ICategoryMapper>();
            _service = new CategoryService(_repositoryMock.Object, _mapperMock.Object);
        }
    }
}
