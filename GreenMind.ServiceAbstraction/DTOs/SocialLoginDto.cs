using System.ComponentModel.DataAnnotations;

namespace GreenMind.ServiceAbstraction.Authentication.DTOs
{
    public class SocialLoginDto
    {
        [Required]
        public string Token { get; set; } = null!;

        [Required]
        public string Role { get; set; } = "User"; // User/Admin
    }
}