using Codepulse.API.Application;
using Codepulse.API.Domain.Entities;
using Codepulse.API.Extensions;
using Codepulse.API.Infrastructure;
using Codepulse.API.Infrastructure.Persistence;
using Codepulse.API.Infrastructure.Seed;
using Codepulse.API.Middleware;
using Codepulse.API.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureSerilog();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// Add services to the container.
builder.Services.AddDbContext<CodepulseDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("CodePulseConnectionString")
));

// Application layer services
builder.Services.AddApplicationServices();

// Infrastructure layer services (e.g., repositories)
builder.Services.AddInfrastructureServices();
builder.Services.AddAutoMapper(typeof(MappingProfile));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Codepulse API",
        Description = "This API Provides Endpoints For  Codepuls Operations",
    });

    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
     {
        new OpenApiSecurityScheme
        {
            Reference=new OpenApiReference
            {
                Type=ReferenceType.SecurityScheme,
                Id=JwtBearerDefaults.AuthenticationScheme,
            },
            Scheme="Oauth2",
            Name=JwtBearerDefaults.AuthenticationScheme,
            In=ParameterLocation.Header,
        },
        new List<string>()
    }
    });
});
builder.Services.AddIdentityCore<AuthUser>()
    .AddRoles<IdentityRole<long>>()
    .AddTokenProvider<DataProtectorTokenProvider<AuthUser>>("Codepulse")
    .AddEntityFrameworkStores<CodepulseDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 4;
    options.Password.RequiredUniqueChars = 1;
}
);
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;

    options.ApiVersionReader = new UrlSegmentApiVersionReader(); // enables /v{version}/
});

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = 429;
    options.OnRejected = async (context, token) =>
    {
        context.HttpContext.Response.StatusCode = 429;
        await context.HttpContext.Response.WriteAsync("Too many requests. Please try again later.", token);
    };
    options.AddPolicy("PerUser", httpContext =>
    {
        var userId = httpContext.User?.Identity?.IsAuthenticated == true
            ? httpContext.User.Identity.Name
            : httpContext.Connection.RemoteIpAddress?.ToString() ?? "anonymous";

        return RateLimitPartition.GetTokenBucketLimiter(userId, _ => new TokenBucketRateLimiterOptions
        {
            TokenLimit = 2,                    
            TokensPerPeriod = 2,                
            ReplenishmentPeriod = TimeSpan.FromSeconds(10),
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
            QueueLimit = 0
        });
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });

var app = builder.Build();
app.UseRateLimiter();
// Migrate on startup
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CodepulseDbContext>();
    await context.Database.MigrateAsync(); 

    await DatabaseSeeder.SeedAsync(context);
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyMethod();
    options.AllowAnyOrigin();
}
    );
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
    RequestPath = "/Images"

});
app.MapControllers().RequireRateLimiting("PerUser");
app.UseMiddleware<CustomExceptionHandlerMiddleware>();
app.Run();
