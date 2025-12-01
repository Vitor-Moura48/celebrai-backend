namespace Celebrai.Communication.Requests.PedidoKit;
public class RequestRegisterPedidoKitJson
{
    public int IdPedido { get; set; }
    public int IdKit { get; set; }
    public int Quantidade { get; set; }
    public decimal Preco { get; set; }
    public string? Avaliacao { get; set; }
    public int? Nota { get; set; }
}

