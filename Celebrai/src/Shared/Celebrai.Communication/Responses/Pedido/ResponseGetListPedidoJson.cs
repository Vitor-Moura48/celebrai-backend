namespace Celebrai.Communication.Responses.Pedido;

public record ResponseListPedidoJson
{
    public List<PedidoItemJson> Pedidos { get; set; } = new();
}

public record PedidoItemJson
{
    public int IdPedido { get; set; }
    public int IdModalidadeEntrega { get; set; }
    public int IdPagamento { get; set; }
    public decimal ValorTotal { get; set; }
    public decimal ValorFrete { get; set; }
    public DateTime DataPedido { get; set; }
    public char Status { get; set; }
}