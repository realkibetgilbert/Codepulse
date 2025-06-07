using System.Text;
using Codepulse.API.Application;
using Codepulse.API.Domain.Entities;
using Codepulse.API.Infrastructure;
using Codepulse.API.Infrastructure.Persistence;
using Codepulse.API.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// Add services to the container.
builder.Services.AddDbContext<CodepulseDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("CodepulseConnectionString")
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
    FileProvider= new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"Images")),
    RequestPath="/Images"

});
app.MapControllers();

app.Run();
