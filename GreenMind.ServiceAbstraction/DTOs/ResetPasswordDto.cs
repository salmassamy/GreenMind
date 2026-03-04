using System.ComponentModel.DataAnnotations;

namespace GreenMind.ServiceAbstraction.Authentication.DTOs
{
    public class ResetPasswordDto
    {
        [Required]
        public string Token { get; set; } = null!;

        [Required, MinLength(6)]
        public string NewPassword { get; set; } = null!;

        [Required]
        public string Role { get; set; } = null!; // User/Admin
    }
}