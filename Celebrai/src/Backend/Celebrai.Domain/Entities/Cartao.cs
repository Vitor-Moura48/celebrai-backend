namespace Celebrai.Domain.Entities;

public class Cartao
{
    public int IdCartao { get; set; }
    public int IdPagamento { get; set; }
    public char TipoCartao { get; set; }
    public string AutorizacaoTid { get; set; } = string.Empty;
    public int Parcelas { get; set; }
    public string NumeroCartao { get; set; } = string.Empty;
    public DateOnly DataValidade { get; set; }
    public string Titular { get; set; } = string.Empty;
    public string Bandeira { get; set; } = string.Empty;

    public Pagamento Pagamento { get; set; } = default!;
}