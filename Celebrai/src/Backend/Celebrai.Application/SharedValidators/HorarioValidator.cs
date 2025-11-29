using Celebrai.Communication.Requests.Disponibilidade;
using FluentValidation;

namespace Celebrai.Application.SharedValidators;

public class HorarioValidator : AbstractValidator<RequestDisponibilidadeJson>
{
    public HorarioValidator()
    {
        RuleFor(x => x.DiaSemana)
            .ValidDayOfWeek();

        RuleFor(x => x.HoraInicio)
            .MustBe30MinuteInterval();

        RuleFor(x => x.HoraFim)
            .MustBe30MinuteInterval()
            .GreaterThan(x => x.HoraInicio)
            .WithMessage("O horário final deve ser posterior ao horário inicial.");
    }
}
