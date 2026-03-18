using GreenMind.Service.Authentication.DTOs;
using GreenMind.ServiceAbstraction.Authentication;
using GreenMind.ServiceAbstraction.Authentication.DTOs;
using GreenMind.ServiceAbstraction.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GreenMindAI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            var result = await _authService.RegisterUserAsync(dto);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            return Ok(result);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequestDto dto)
        {
            var result = await _authService.ForgotPasswordAsync(dto);
            return Ok(new { message = result });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            var result = await _authService.ResetPasswordAsync(dto);
            return Ok(new { message = result });
        }

        [HttpPost("google")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginDto dto)
        {
            var result = await _authService.GoogleLoginAsync(dto.Token, dto.Role);
            return Ok(result);
        }

        [HttpPost("facebook")]
        public async Task<IActionResult> FacebookLogin(FacebookLoginDto dto)
        {
            var result = await _authService.FacebookLoginAsync(dto.Token, dto.Role);
            return Ok(result);
        }
    }
}
