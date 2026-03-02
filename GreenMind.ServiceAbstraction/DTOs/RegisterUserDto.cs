
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GreenMind.Service.Authentication.DTOs
{
    public class RegisterUserDto
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
       
        [Required]
        [MinLength(6)]
        
        public string Password { get; set; } = null!;
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
      
        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = null!;
    }
}