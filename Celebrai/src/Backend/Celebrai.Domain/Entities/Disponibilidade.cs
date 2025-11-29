namespace Celebrai.Domain.Entities;

public class Disponibilidade
{
    public int IdDisponibilidade { get; set; }
    public DayOfWeek DiaSemana { get; set; }
    public TimeSpan HoraInicio { get; set; }
    public TimeSpan HoraFim { get; set; }
    public Guid IdFornecedor { get; set; }

    public Fornecedor Fornecedor { get; set; } = default!;
}