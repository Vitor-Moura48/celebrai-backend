using Celebrai.Domain.Entities;
using Celebrai.Domain.Security.Tokens;
using Celebrai.Domain.Services.LoggedUser;
using Celebrai.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Celebrai.Infrastructure.Services.LoggedUser;
public class LoggedUser : ILoggedUser
{
    private readonly CelebraiDbContext _context;
    private readonly ITokenProvider _tokenProvider;

    public LoggedUser(CelebraiDbContext context, ITokenProvider tokenProvider)
    {
        _context = context;
        _tokenProvider = tokenProvider;
    }
    public async Task<Usuario> User()
    {
        var token = _tokenProvider.Value();

        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var identifier = jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

        var userIdentifier = Guid.Parse(identifier);

        return await _context
            .Usuario
            .AsNoTracking()
            .FirstAsync(user => user.Ativo && user.IdUsuario == userIdentifier);
    }
}
