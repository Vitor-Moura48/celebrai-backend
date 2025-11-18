using Celebrai.Communication.Requests.Usuario;
using Celebrai.Communication.Responses.Tokens;
using Celebrai.Communication.Responses.Usuario;
using Celebrai.Domain.Enums;
using Celebrai.Domain.Repositories;
using Celebrai.Domain.Repositories.Usuario;
using Celebrai.Domain.Security.Cryptography;
using Celebrai.Domain.Security.Tokens;
using Celebrai.Exceptions.ExceptionsBase;

namespace Celebrai.Application.UseCases.Login.DoLogin;
public class DoLoginUseCase : IDoLoginUseCase
{
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IUsuarioReadOnlyRepository _repository;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IUnitOfWork _unitOfWork;

    public DoLoginUseCase(
        IUsuarioReadOnlyRepository repository,
        IUnitOfWork unitOfWork,
        IAccessTokenGenerator accessTokenGenerator,
        IPasswordEncripter passwordEncripter)
    {
        _accessTokenGenerator = accessTokenGenerator;
        _repository = repository;
        _unitOfWork = unitOfWork;
        _passwordEncripter = passwordEncripter;
    }
    public async Task<ResponseLoginUsuarioJson> Execute(RequestLoginJson request)
    {
        var user = await _repository.GetByEmail(request.Email);

        if (user is null || _passwordEncripter.IsValid(request.Password, user.Senha) == false)
            throw new InvalidLoginException();

        var token = _accessTokenGenerator.Generate(user.IdUsuario, UserTokenType.AccessToken, user.Role.ToString());
        return new ResponseLoginUsuarioJson
        {
            Name = user.Nome,
            Tokens = new ResponseTokensJson
            {
                AccessToken = token
            }
        };
    }
}
