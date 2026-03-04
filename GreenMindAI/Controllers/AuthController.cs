using GreenMind.Service.Authentication.DTOs;
using GreenMind.Service.Authentication.Services;
using GreenMind.ServiceAbstraction.Authentication;
using GreenMind.ServiceAbstraction.Authentication.DTOs;
using GreenMind.ServiceAbstraction.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace GreenMindAI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        private readonly ISocialAuthService _social;

        public AuthController(IAuthService auth, ISocialAuthService social)
        {
            _auth = auth;
            _social = social;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            try
            {
                var res = await _auth.RegisterUserAsync(dto);
                return Ok(res);
            }
            catch (AuthHttpException ex)
            {
                return StatusCode(ex.StatusCode, new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            try
            {
                var res = await _auth.LoginAsync(dto);
                return Ok(res);
            }
            catch (AuthHttpException ex)
            {
                return StatusCode(ex.StatusCode, new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDto dto)
        {
            try
            {
                var token = await _auth.ForgotPasswordAsync(dto);

                return Ok(new
                {
                    message = "Reset token generated",
                    token = token
                });
            }
            catch (AuthHttpException ex)
            {
                return StatusCode(ex.StatusCode, new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            try
            {
                await _auth.ResetPasswordAsync(dto);

                return Ok(new
                {
                    message = "Password updated successfully"
                });
            }
            catch (AuthHttpException ex)
            {
                return StatusCode(ex.StatusCode, new { message = ex.Message });
            }
        }

        [HttpPost("google")]
        public async Task<IActionResult> Google([FromBody] SocialLoginDto dto)
        {
            try
            {
                // مهم: من غير deconstruction عشان مايحصلش infer error
                var info = await _social.VerifyGoogleAsync(dto.Token);
                var res = await _auth.ExternalLoginAsync(info.Email, info.Name, dto.Role);
                return Ok(res);
            }
            catch (AuthHttpException ex)
            {
                return StatusCode(ex.StatusCode, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("facebook")]
        public async Task<IActionResult> Facebook([FromBody] SocialLoginDto dto)
        {
            try
            {
                var info = await _social.VerifyFacebookAsync(dto.Token);
                var res = await _auth.ExternalLoginAsync(info.Email, info.Name, dto.Role);
                return Ok(res);
            }
            catch (AuthHttpException ex)
            {
                return StatusCode(ex.StatusCode, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}