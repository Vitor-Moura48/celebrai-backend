namespace Celebrai.Communication.Requests.PedidoProduto;
public class RequestRegisterPedidoProdutoJson
{
    public int IdPedido { get; set; }
    public int IdProduto { get; set; }
    public int Quantidade { get; set; }
    public decimal Preco { get; set; }
    public string? Avaliacao { get; set; }
    public int? Nota { get; set; }
}


