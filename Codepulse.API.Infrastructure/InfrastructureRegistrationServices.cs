using Codepulse.API.Domain.Interfaces;
using Codepulse.API.Infrastructure.Repositories.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Codepulse.API.Infrastructure
{
    public static class InfrastructureRegistrationServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBlogPostRepository, BlogPostRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();


            return services;
        }
    }
}