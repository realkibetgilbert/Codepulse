using Codepulse.API.Application.Features.Auth.Interfaces;
using Codepulse.API.Application.Features.Auth.Services;
using Codepulse.API.Application.Features.Blog.Interfaces;
using Codepulse.API.Application.Features.Blog.Services;
using Codepulse.API.Application.Features.Categ.Interfaces;
using Codepulse.API.Application.Features.Categ.Services;
using Codepulse.API.Application.Features.Img.Interfaces;
using Codepulse.API.Application.Features.Img.Services;
using Codepulse.API.Application.Mappers.Blog.Interfaces;
using Codepulse.API.Application.Mappers.Blog.Services;
using Codepulse.API.Application.Mappers.Categ.Interfaces;
using Codepulse.API.Application.Mappers.Categ.Services;
using Codepulse.API.Application.Mappers.Img.Interfaces;
using Codepulse.API.Application.Mappers.Img.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Codepulse.API.Application
{
    public static class ApplicationRegistrationServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBlogPostService, BlogPostService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IBlogPostMapper, BlogPostMapper>();
            services.AddScoped<ICategoryMapper, CategoryMapper>();
            services.AddScoped<IImageMapper, ImageMapper>();
            return services;
        }
    }
}
