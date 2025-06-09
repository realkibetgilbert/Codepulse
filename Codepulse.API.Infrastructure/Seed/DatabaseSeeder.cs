using Codepulse.API.Domain.Entities;
using Codepulse.API.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codepulse.API.Infrastructure.Seed
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(CodepulseDbContext context)
        {
            if (!context.Categories.Any() && !context.BlogPosts.Any())
            {
                // Seed categories
                var webDev = new Category { Name = "Web Development", UrlHandle = "web-development" };
                var cloud = new Category { Name = "Cloud", UrlHandle = "cloud" };

                // Seed blog posts
                var blog1 = new BlogPost
                {
                    Title = "Intro to Web Dev",
                    ShortDescription = "A beginner's guide to web development.",
                    Content = "Here's a full guide...",
                    Author = "Admin",
                    FeaturedImageUrl = "https://via.placeholder.com/300",
                    UrlHandle = "intro-web-dev",
                    PublishedDate = DateTime.UtcNow,
                    IsVisible = true,
                    Categories = new List<Category> { webDev }
                };

                var blog2 = new BlogPost
                {
                    Title = "Getting Started with Cloud",
                    ShortDescription = "Basics of cloud computing.",
                    Content = "Cloud computing is...",
                    Author = "System",
                    FeaturedImageUrl = "https://via.placeholder.com/300",
                    UrlHandle = "getting-started-cloud",
                    PublishedDate = DateTime.UtcNow,
                    IsVisible = true,
                    Categories = new List<Category> { cloud }
                };

                context.BlogPosts.AddRange(blog1, blog2);
                await context.SaveChangesAsync();
            }
        }
    }


}
