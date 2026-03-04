using GreenMind.Service.Authentication.DTOs;
using GreenMind.ServiceAbstraction.Authentication.DTOs;

namespace GreenMind.ServiceAbstraction.Authentication
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterUserAsync(RegisterUserDto dto);
        Task<AuthResponseDto> LoginAsync(LoginDto dto);

        Task<string?> ForgotPasswordAsync(ForgotPasswordRequestDto dto); // يرجّع token
        Task ResetPasswordAsync(ResetPasswordDto dto);

        Task<AuthResponseDto> ExternalLoginAsync(string email, string name, string role);
    }
}
