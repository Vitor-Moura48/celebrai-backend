namespace Celebrai.Domain.Entities;

public class PedidoProduto
{
    public int IdPedido { get; set; }
    public int IdProduto { get; set; }
    public int Quantidade { get; set; }
    public decimal Preco { get; set; }
    public string? Avaliacao { get; set; }
    public int? Nota { get; set; }

    public Pedido Pedido { get; set; }
    public Produto Produto { get; set; }
}