namespace Celebrai.Communication.Requests.Disponibilidade;

public record RequestDisponibilidadeJson
{
    public DayOfWeek DiaSemana { get; init; }
    public TimeSpan HoraInicio { get; init; }
    public TimeSpan HoraFim { get; init; }
}
