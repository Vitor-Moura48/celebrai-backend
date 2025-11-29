using Celebrai.Application.SharedValidators;
using Celebrai.Communication.Requests.Disponibilidade;
using FluentValidation;

namespace Celebrai.Application.UseCases.Disponibilidade;

public class RegisterDisponibilidadeValidator : AbstractValidator<RequestRegistedDisponibilidadeJson>
{
    public RegisterDisponibilidadeValidator()
    {
        RuleForEach(x => x.Horarios).SetValidator(new HorarioValidator());
    }
}
