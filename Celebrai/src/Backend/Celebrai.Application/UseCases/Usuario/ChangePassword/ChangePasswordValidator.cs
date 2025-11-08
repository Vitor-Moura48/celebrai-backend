using Celebrai.Application.SharedValidators;
using Celebrai.Communication.Requests.Usuario;
using FluentValidation;

namespace Celebrai.Application.UseCases.Usuario.ChangePassword;
public class ChangePasswordValidator : AbstractValidator<RequestChangePasswordJson>
{
    public ChangePasswordValidator()
    {
        RuleFor(user => user.NewPassword).Password();
    }
}
