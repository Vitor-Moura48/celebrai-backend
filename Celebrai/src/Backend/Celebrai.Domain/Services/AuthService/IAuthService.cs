namespace Celebrai.Domain.Services.AuthService;
public interface IAuthService
{
    public Task<string> CreateAuthAccount(string email, string password);
    public Task<string> GenerateEmailVerificationLink(string email);
    public Task<string> CreateCustomToken(string uid, string role);
}
