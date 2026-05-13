using GreenMind.Service.Authentication.DTOs;
using GreenMind.ServiceAbstraction.Authentication.DTOs;

namespace GreenMind.ServiceAbstraction.Authentication
{
    public interface IAuthService
    {

        Task<string> RegisterUserAsync(RegisterUserDto dto);

        Task<string> LoginAsync(LoginDto dto);
        Task ResetPasswordAsync(ResetPasswordDto dto);

        Task<AuthResponseDto> ExternalLoginAsync(string name, string role);
    }
}