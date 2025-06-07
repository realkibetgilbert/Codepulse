using System.ComponentModel.DataAnnotations;

namespace Codepulse.API.Application.DTOs.Auth
{
    public class RoleNameDto
    {
        [Required]
        public string Name { get; set; }
    }
}
