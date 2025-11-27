namespace Celebrai.Domain.Entities;

public class PedidoKit
{
    public int IdPedido { get; set; }
    public int IdKit { get; set; }
    public int Quantidade { get; set; }
    public decimal Preco { get; set; }
    public string? Avaliacao { get; set; }
    public int? Nota { get; set; }
    public DateOnly Data { get; set; }

    public Pedido Pedido { get; set; } = default!;
    public Kit Kit { get; set; } = default!;

}

