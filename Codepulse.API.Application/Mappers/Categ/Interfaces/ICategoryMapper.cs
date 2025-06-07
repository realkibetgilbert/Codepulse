using Codepulse.API.Application.DTOs.Category;
using Codepulse.API.Domain.Entities;

namespace Codepulse.API.Application.Mappers.Categ.Interfaces
{
    public interface ICategoryMapper
    {
        Category ToDomain(CategoryToCreateDto dto);
        Category ToDomain(CategoryToUpdateDto dto, long id);
        CategoryToDisplayDto ToDisplay(Category category);
        List<CategoryToDisplayDto> ToDisplayList(IEnumerable<Category> categories);
    }
}
