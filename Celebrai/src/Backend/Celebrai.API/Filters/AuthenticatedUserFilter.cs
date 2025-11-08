using Celebrai.Communication.Responses;
using Celebrai.Domain.Enums;
using Celebrai.Domain.Repositories.Usuario;
using Celebrai.Domain.Security.Tokens;
using Celebrai.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Celebrai.API.Filters;

public class AuthenticatedUserFilter : IAsyncAuthorizationFilter
{
    private readonly IAccessTokenValidator _accessTokenValidator;
    private readonly IUsuarioReadOnlyRepository _repository;
    private readonly RoleUsuario? _roleEsperado;

    public AuthenticatedUserFilter(
        IAccessTokenValidator accessTokenValidator,
        IUsuarioReadOnlyRepository repository,
        RoleUsuario? roleEsperado = null)
    {
        _accessTokenValidator = accessTokenValidator;
        _repository = repository;
        _roleEsperado = roleEsperado;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = TokenOnRequest(context);

            var userId = _accessTokenValidator.ValidateAndGetUserIdentifier(
                token,
                UserTokenType.AccessToken);

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var roleClaim = jwtToken.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.Role || c.Type == "role")?.Value;

            if (_roleEsperado.HasValue && roleClaim != _roleEsperado.Value.ToString())
                throw new UnauthorizedException($"Usuário precisa ser {_roleEsperado.Value} para acessar este recurso.");

            var exists = await _repository.ExistActiveUserWithIdentifier(userId);
            if (exists == false)
                throw new UnauthorizedException("Usuário inativo ou inexistente.");
        }
        catch (SecurityTokenExpiredException)
        {
            context.Result = new UnauthorizedObjectResult(new ResponseErrorJson("Token expirado"));
        }
        catch (CelebraiException ex)
        {
            context.HttpContext.Response.StatusCode = (int)ex.GetStatusCode();
            context.Result = new ObjectResult(new ResponseErrorJson(ex.GetErrorMessages()));
        }
        catch
        {
            context.Result = new UnauthorizedObjectResult(new ResponseErrorJson("Acesso não autorizado."));
        }
    }

    private static string TokenOnRequest(AuthorizationFilterContext context)
    {
        var authentication = context.HttpContext.Request.Headers.Authorization.ToString();
        if (string.IsNullOrWhiteSpace(authentication))
            throw new UnauthorizedException("Token não informado.");

        return authentication["Bearer ".Length..].Trim();
    }
}
