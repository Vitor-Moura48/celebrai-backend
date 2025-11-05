using Celebrai.Domain.Enums;

namespace Celebrai.Domain.Security.Tokens;
public interface IAccessTokenValidator
{
    public Guid ValidateAndGetUserIdentifier(string token, UserTokenType tokenType);
}
