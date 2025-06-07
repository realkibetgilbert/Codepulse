using Codepulse.API.Domain.Entities;
using Codepulse.API.Domain.Interfaces;
using Codepulse.API.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Codepulse.API.Infrastructure.Repositories.Implementations
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly CodepulseDbContext _context;

        public BlogPostRepository(CodepulseDbContext context)
        {
            _context = context;
        }
        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await _context.BlogPosts.AddAsync(blogPost);
            await _context.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(long id)
        {
            var existing = await _context.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) { return null; }
            _context.BlogPosts.Remove(existing);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<List<BlogPost>> GetAllAsync()
        {
            return await _context.BlogPosts.Include(c => c.Categories).OrderByDescending(c => c.Id).ToListAsync();
        }

        public async Task<BlogPost?> GetByIdAsync(long id)
        {
            return await _context.BlogPosts.Include(b => b.Categories).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<BlogPost?> GetByUrlAsync(string urlHandle)
        {
            return await _context.BlogPosts.Include(b => b.Categories).FirstOrDefaultAsync(c => c.UrlHandle == urlHandle);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingBlogPost = await _context.BlogPosts.Include(b => b.Categories)
                .FirstOrDefaultAsync(x => x.Id == blogPost.Id);

            if (existingBlogPost is null) { return null; }

            _context.Entry(existingBlogPost).CurrentValues.SetValues(blogPost);
            existingBlogPost.Categories = blogPost.Categories;

            await _context.SaveChangesAsync();
            return blogPost;
        }
    }
}
