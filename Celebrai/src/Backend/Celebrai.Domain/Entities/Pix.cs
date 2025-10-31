namespace Celebrai.Domain.Entities;

public class Pix
{
    public int IdPix { get; set; }
    public int IdPagamento { get; set; }
    public string Txid { get; set; } = string.Empty;
    public string InstituicaoFinanceira { get; set; } = string.Empty;
    public string ChavePix { get; set; } = string.Empty;

    public Pagamento Pagamento { get; set; } = default!;
}