using Celebrai.Domain.Enums;

namespace Celebrai.Domain.Security.Tokens;
public interface IAccessTokenGenerator
{
    public string Generate(
        Guid userIdentifier,
        UserTokenType tokenType,
        string? roleClaim = null,
        uint? customExpirationMinutes = null
        );
}
