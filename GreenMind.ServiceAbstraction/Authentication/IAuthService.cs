using GreenMind.Service.Authentication.DTOs;

namespace GreenMind.ServiceAbstraction.Authentication
{
    public interface IAuthService
    {
        Task<string> RegisterUserAsync(RegisterUserDto dto);
        
        Task<string> LoginAsync(LoginDto dto);
    }
}