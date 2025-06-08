using Codepulse.API.Application.DTOs.Auth;
using Codepulse.API.Application.Features.Auth.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Codepulse.API.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        [Route("Role")]
        public async Task<IActionResult> CreateRole([FromBody] RoleNameDto roleNameDto)
        {

            var result = await _authService.CreateRoleAsync(roleNameDto);

            if (result.Succeeded)
            {
                return Ok(new { roleNameDto.Name });
            }

            return BadRequest(result.Errors);
        }

        [HttpGet]
        [Route("Role")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _authService.GetRolesAsync();
            return Ok(roles);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterUser(UserRegistrationDto expressWayPosUserDto)
        {
            var (isSuccess, result) = await _authService.RegisterUserAsync(expressWayPosUserDto);

            if (isSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var (isSuccess, result) = await _authService.LoginAsync(loginDto);

            if (isSuccess)
            {
               
                var jsonRes = JsonConvert.SerializeObject(result);

                return Content(jsonRes, "application/json");
            }

            return BadRequest(result);
        }
    }
}
