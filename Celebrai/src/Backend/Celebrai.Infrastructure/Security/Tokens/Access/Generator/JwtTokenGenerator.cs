using Celebrai.Domain.Enums;
using Celebrai.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Celebrai.Infrastructure.Security.Tokens.Access.Generator;
public class JwtTokenGenerator : JwtTokenHandler, IAccessTokenGenerator
{
    private readonly uint _expirationTimeMinutes;
    private readonly string _signingKey;

    public JwtTokenGenerator(uint expirationTimeMinutes, string signingKey)
    {
        _expirationTimeMinutes = expirationTimeMinutes;
        _signingKey = signingKey;
    }

    public string Generate(Guid userIdentifier, UserTokenType tokenType, string? roleClaim = null, uint? customExpirationMinutes = null)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Sid, userIdentifier.ToString()),
            new Claim("purpose", tokenType.ToString())
        };

        if (roleClaim != null)
            claims.AddRange(new Claim(ClaimTypes.Role, roleClaim));

        var expires = customExpirationMinutes ?? _expirationTimeMinutes;

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(expires),
            SigningCredentials = new SigningCredentials(SecurityKey(_signingKey), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
