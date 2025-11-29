namespace Celebrai.Communication.Responses.Disponibilidade;

public record ResponseDisponibilidadeJson
{
    public int DiaSemana { get; init; }
    public TimeSpan HoraInicio { get; init; }
    public TimeSpan HoraFim { get; init; }
}
