using System.ComponentModel.DataAnnotations;

namespace GreenMind.ServiceAbstraction.Authentication.DTOs
{
    public class ForgotPasswordRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Role { get; set; } = null!; // User/Admin
    }
}