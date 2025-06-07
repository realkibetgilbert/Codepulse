using Codepulse.API.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Codepulse.API.Infrastructure.Persistence
{
    public class CodepulseDbContext : IdentityDbContext<AuthUser, IdentityRole<long>, long>
    {
        public CodepulseDbContext(DbContextOptions<CodepulseDbContext> options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }  
        public DbSet<Category> Categories { get; set; }  
        public DbSet<BlogImage> BlogImages { get; set; }  
    }
}
