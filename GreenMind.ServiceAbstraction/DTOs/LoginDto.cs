using System.ComponentModel.DataAnnotations;

namespace GreenMind.ServiceAbstraction.Authentication.DTOs
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; } = null!;   // Email OR UserName

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = null!;

        [Required]
        public string Role { get; set; } = null!;    // User أو Admin
    }
}