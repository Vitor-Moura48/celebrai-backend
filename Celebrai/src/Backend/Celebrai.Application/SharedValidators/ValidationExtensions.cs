using FluentValidation;

namespace Celebrai.Application.SharedValidators;
public static class ValidationExtensions
{
    public static IRuleBuilderOptions<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("A senha não pode ser vazia")
            .MinimumLength(8).WithMessage("A senha não pode ter menos de 8 caracteres")
            .Matches("[A-Z]").WithMessage("A senha deve conter pelo menos uma letra maiúscula")
            .Matches("[a-z]").WithMessage("A senha deve conter pelo menos uma letra minúscula")
            .Matches("[0-9]").WithMessage("A senha deve conter números")
            .Matches("[^a-zA-Z0-9]").WithMessage("A senha deve conter caracteres especiais");
    }
}
