using Codepulse.API.Application.DTOs.Auth;
using Microsoft.AspNetCore.Identity;

namespace Codepulse.API.Application.Features.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<IdentityResult> CreateRoleAsync(RoleNameDto roleNameDto);
        Task<List<IdentityRole<long>>> GetRolesAsync();
        Task<(bool isSuccess, object result)> RegisterUserAsync(UserRegistrationDto dto);
        Task<(bool isSuccess, object result)> LoginAsync(LoginDto loginDto);
    }
}
