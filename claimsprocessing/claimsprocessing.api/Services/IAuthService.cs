namespace claimsprocessing.api.Services
{
    public interface IAuthService
    {
        Task<AuthResult> AuthenticateAsync(string email, string password);
    }
}
