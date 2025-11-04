using Celebrai.Communication.Responses.Usuario;
using Celebrai.Domain.Enums;
using Celebrai.Domain.Repositories;
using Celebrai.Domain.Repositories.Usuario;
using Celebrai.Domain.Security.Tokens;
using Celebrai.Exceptions.ExceptionsBase;

namespace Celebrai.Application.UseCases.Usuario.ConfirmEmail;
public class ConfirmEmailUsuarioUseCase : IConfirmEmailUsuarioUseCase
{
    private readonly IUsuarioUpdateOnlyRepository _userUpdateOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccessTokenValidator _tokenValidator;

    public ConfirmEmailUsuarioUseCase(
        IUsuarioUpdateOnlyRepository userUpdateeOnlyRepository,
        IUnitOfWork unitOfWork,
        IAccessTokenValidator tokenValidator)
    {
        _userUpdateOnlyRepository = userUpdateeOnlyRepository;
        _unitOfWork = unitOfWork;
        _tokenValidator = tokenValidator;
    }
    public async Task<ResponseConfirmEmailUsuariojson> Execute(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new ErrorValidationException(["Token de confirmação é obrigatório."]);

        var userId = _tokenValidator.ValidateAndGetUserIdentifier(token, UserTokenType.AccountVerification);

        var user = await _userUpdateOnlyRepository.GetById(userId);
        if (user == null)
            throw new NotFoundException("Usuário não encontrado.");

        if (user.Ativo)
            throw new ErrorValidationException(["E-mail já confirmado anteriormente."]);

        user.Ativo = true;

        _userUpdateOnlyRepository.Update(user);
        await _unitOfWork.Commit();

        return new ResponseConfirmEmailUsuariojson
        {
            ConfirmMessage = "E-mail confirmado com sucesso! Agora você pode fazer login."
        };
    }
}
