using Codepulse.API.Domain.Entities;
using Codepulse.API.Domain.Interfaces;
using Codepulse.API.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Codepulse.API.Infrastructure.Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CodepulseDbContext _context;

        public CategoryRepository(CodepulseDbContext context)
        {
            _context = context;
        }
        public async Task<Category> CreateAsync(Category category)
        {
            await _context.Categories.AddAsync(category);

            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<Category?> DeleteAsync(long id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (category == null) return null;
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<List<Category>> GetAllAsync(string? query = null, string? sortBy = null, string? sortDirection = null, int? pageNumber = 1, int? pageSize = 10)
        {
            //query database not actually retrieve 

            var categories = _context.Categories.AsQueryable();

            //filtering 
            if (string.IsNullOrWhiteSpace(query) == false)
            {
                categories = categories.Where(x => x.Name.Contains(query));
            }

            //sorting  arranging data in a particular order.......on name column...sort on..a column..
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {

                if (String.Equals(sortBy, "Name", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;

                    categories = isAsc ? categories.OrderBy(x => x.Name) : categories.OrderByDescending(x => x.Name);
                }

            }
            //apply the formula for pagination....
            //page 1 page size 5 skip 0 take 5
            //page 2 page size 5 skip 5 take 5
            //page 3 page size 5 skip 10 take 5

            var skipResults = (pageNumber - 1) * pageSize;

            categories = categories.Skip(skipResults ?? 0).Take(pageSize ?? 10);


            return await categories.ToListAsync();




        }
        public async Task<Category?> GetByIdAsync(long id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (category == null) return null;
            return category;
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Categories.CountAsync();
        }

        public async Task<Category?> UpdateAsync(Category category)
        {
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(r => r.Id == category.Id);

            if (category == null)
            {
                return null;
            }
            if (existingCategory != null)
            {
                existingCategory.Name = category.Name;
                existingCategory.UrlHandle = category.UrlHandle;

            }
            await _context.SaveChangesAsync();
            return existingCategory;
        }

    }
}
