using Codepulse.API.Application.DTOs.Auth;
using Codepulse.API.Application.Features.Auth.Interfaces;
using Codepulse.API.Domain.Entities;
using Codepulse.API.Domain.Interfaces;
using Codepulse.API.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Codepulse.API.Application.Features.Auth.Services
{

    public class AuthService : IAuthService
    {
        private readonly UserManager<AuthUser> _userManager;
        private readonly RoleManager<IdentityRole<long>> _roleManager;
        private readonly CodepulseDbContext _context;
        private readonly ITokenRepository _tokenRepository;

        public AuthService(
            UserManager<AuthUser> userManager,
            RoleManager<IdentityRole<long>> roleManager,
            CodepulseDbContext context,
            ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _tokenRepository = tokenRepository;
        }

        public async Task<IdentityResult> CreateRoleAsync(RoleNameDto roleNameDto)
        {
            var role = new IdentityRole<long> { Name = roleNameDto.Name };
            return await _roleManager.CreateAsync(role);
        }
        public async Task<List<IdentityRole<long>>> GetRolesAsync()
        {
            return await _roleManager.Roles.ToListAsync();
        }
        public async Task<(bool isSuccess, object result)> RegisterUserAsync(UserRegistrationDto dto)
        {
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                return (false, new
                {
                    title = "Fail",
                    message = "User with the above name exists"
                });
            }

            var user = new AuthUser
            {
                UserName = dto.Email,
                Email = dto.Email
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                return (false, result.Errors);
            }

            if (dto.Roles != null && dto.Roles.Length > 0)
            {
                foreach (var roleName in dto.Roles)
                {
                    var existingRole = await _roleManager.FindByNameAsync(roleName);
                    if (existingRole != null)
                    {
                        var addToRoleResult = await _userManager.AddToRoleAsync(user, roleName);
                        if (!addToRoleResult.Succeeded)
                        {
                            await _userManager.DeleteAsync(user);
                            return (false, addToRoleResult.Errors);
                        }
                    }
                    else
                    {
                        return (false, new
                        {
                            title = "Role Not Found",
                            message = $"Role '{roleName}' does not exist"
                        });
                    }
                }
            }

            return (true, new
            {
                title = "Success",
                description = "User registered successfully with roles"
            });
        }

        public async Task<(bool isSuccess, object result)> LoginAsync(LoginDto loginDto)
        {
            var loginUser = await _context.Users.FirstOrDefaultAsync(e => e.Email == loginDto.UserName);

            if (loginUser == null)
            {
                return (false, new
                {
                    title = "User Not Found",
                    message = "No user found with the provided email"
                });
            }

            bool isPasswordValid = await _userManager.CheckPasswordAsync(loginUser, loginDto.Password);

            if (!isPasswordValid)
            {
                return (false, new
                {
                    title = "Invalid Credentials",
                    message = "The submitted login credentials are invalid"
                });
            }

            var roles = await _userManager.GetRolesAsync(loginUser);

            var jwtToken = _tokenRepository.CreateJwtToken(loginUser, roles.ToList());

            var loginResponse = new LoginResponseDto
            {
                Email = loginDto.UserName,
                Token = jwtToken,
                Roles = roles.ToList()
            };

            return (true, loginResponse);
        }

    }

}


