using Celebrai.Communication.Requests.Usuario;
using FluentValidation;

namespace Celebrai.Application.UseCases.Usuario.Update;
public class UpdateUsuarioValidator : AbstractValidator<RequestUpdateUsuarioJson>
{
    public UpdateUsuarioValidator()
    {
        RuleFor(user => user.Nome).NotEmpty().WithMessage("O nome é obrigatória.");
        RuleFor(user => user.Telefone).NotEmpty().WithMessage("O telefone é obrigatório")
            .Matches(@"^\+?[1-9]\d{1,14}$")
            .WithMessage("Telefone deve estar no formato internacional (ex: +5511999999999)."); 
    }
}
