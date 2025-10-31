using Celebrai.Domain.Services.AuthService;
using Celebrai.Exceptions.ExceptionsBase;
using FirebaseAdmin.Auth;

namespace Celebrai.Infrastructure.Services.AuthService;
public class FirebaseAuthService : IAuthService
{
    public async Task<string> CreateAuthAccount(string email, string password)
    {
        try
        {
            var userArgs = new UserRecordArgs
            {
                Email = email,
                Password = password,
                EmailVerified = false
            };

            var userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(userArgs);
            return userRecord.Uid;
        }
        catch (FirebaseAuthException ex)
        {
            if (ex.AuthErrorCode == AuthErrorCode.EmailAlreadyExists)
            {
                throw new ErrorValidationException(["O e-mail já está registrado na plataforma."]);
            }

            throw;
        }
    }

    public async Task<string> CreateCustomToken(string uid, string role)
    {
        var claims = new Dictionary<string, object>
        {
            { "role", role } 
        };

        await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(uid, claims);

        string customToken = await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(uid, claims);

        return customToken;
    }

    public async Task<string> GenerateEmailVerificationLink(string email)
        => await FirebaseAuth.DefaultInstance.GenerateEmailVerificationLinkAsync(email);
}
