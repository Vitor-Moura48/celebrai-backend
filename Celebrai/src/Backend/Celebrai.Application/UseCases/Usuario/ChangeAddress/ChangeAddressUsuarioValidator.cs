using Celebrai.Communication.Requests.Usuario;
using FluentValidation;

namespace Celebrai.Application.UseCases.Usuario.ChangeAddress;
public class ChangeAddressUsuarioValidator : AbstractValidator<RequestChangeAddressUsuarioJson>
{
    public ChangeAddressUsuarioValidator()
    {
        RuleFor(x => x.Lograduro)
            .NotEmpty().WithMessage("O logradouro é obrigatório.")
            .MinimumLength(3).WithMessage("O logradouro deve ter pelo menos 3 caracteres.");

        RuleFor(x => x.Numero)
            .NotEmpty().WithMessage("O número é obrigatório.")
            .MaximumLength(10).WithMessage("O número não pode ter mais que 10 caracteres.");

        RuleFor(x => x.CEP)
            .NotEmpty().WithMessage("O CEP é obrigatório.")
            .Matches(@"^\d{5}-?\d{3}$").WithMessage("O CEP deve estar no formato 00000-000 ou 00000000.");
    }
}
