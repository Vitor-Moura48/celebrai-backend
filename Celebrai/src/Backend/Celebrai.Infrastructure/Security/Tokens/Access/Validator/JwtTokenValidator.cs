using Celebrai.Domain.Enums;
using Celebrai.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Celebrai.Infrastructure.Security.Tokens.Access.Validator;
public class JwtTokenValidator : JwtTokenHandler, IAccessTokenValidator
{
    private readonly string _signingKey;

    public JwtTokenValidator(string signingKey) => _signingKey = signingKey;

    public Guid ValidateAndGetUserIdentifier(string token, UserTokenType tokenType)
    {
        var validationParameter = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            IssuerSigningKey = SecurityKey(_signingKey),
            ClockSkew = new TimeSpan(0)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var principal = tokenHandler.ValidateToken(token, validationParameter, out _);

        var purposeClaim = principal.Claims.FirstOrDefault(c => c.Type == "purpose")?.Value;
        if (purposeClaim != tokenType.ToString())
            throw new SecurityTokenException($"Token não é do tipo esperado ({tokenType}).");

        var userIdentifier = principal.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

        return Guid.Parse(userIdentifier);
    }
}
