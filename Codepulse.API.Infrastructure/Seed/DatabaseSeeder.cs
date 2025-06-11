using Codepulse.API.Domain.Entities;
using Codepulse.API.Infrastructure.Persistence;
using Codepulse.API.Infrastructure.Seed.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Codepulse.API.Infrastructure.Seed
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(
            CodepulseDbContext context,
            UserManager<AuthUser> userManager,
            RoleManager<IdentityRole<long>> roleManager, IOptions<SeederSettings> seederOptions)
        {
            var settings = seederOptions.Value;
            // Seed categories and blog posts (same as before)
            if (!context.Categories.Any() && !context.BlogPosts.Any())
            {
                var webDev = new Category { Name = "Web Development", UrlHandle = "web-development" };
                var cloud = new Category { Name = "Cloud", UrlHandle = "cloud" };

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
            //  Seed Roles from config
            foreach (var role in settings.DefaultRoles.Distinct())
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<long>(role));
                }
            }
            //  Seed Users from config
            foreach (var user in settings.DefaultUsers)
            {
                var existingUser = await userManager.FindByEmailAsync(user.Email);
                if (existingUser == null)
                {
                    var newUser = new AuthUser
                    {
                        UserName = user.Email,
                        Email = user.Email,
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(newUser, user.Password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(newUser, user.Role);
                    }
                }
            }

        }
    }
}