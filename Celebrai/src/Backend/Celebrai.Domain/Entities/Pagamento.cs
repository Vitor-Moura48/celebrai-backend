namespace Celebrai.Domain.Entities;

public class Pagamento
{
    public int IdPagamento { get; set; }
    public decimal Valor { get; set; }
    public char Status { get; set; }
    public DateTime DataHora { get; set; }
    public char Metodo { get; set; }
}