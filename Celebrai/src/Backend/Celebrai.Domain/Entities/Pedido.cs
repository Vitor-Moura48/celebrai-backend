namespace Celebrai.Domain.Entities;

public class Pedido
{
    public int IdPedido { get; set; }
    public int IdModalidadeEntrega { get; set; }
    public Guid IdUsuario { get; set; }
    public int IdPagamento { get; set; }
    public decimal ValorTotal { get; set; }
    public decimal ValorFrete { get; set; }
    public DateTime DataPedido { get; set; } = DateTime.UtcNow;
    public char Status { get; set; } = 'p';

    public Usuario Usuario { get; set; } = default!;
    public ModalidadeEntrega ModalidadeEntrega { get; set; } = default!;
    public Pagamento Pagamento { get; set; } = default!;
}