namespace GreenMind.ServiceAbstraction.Interfaces
{
    public interface ISocialAuthService
    {
        Task<(string Email, string Name)> VerifyGoogleAsync(string idToken);

        Task<(string Email, string Name)> VerifyFacebookAsync(string accessToken);
    }
}