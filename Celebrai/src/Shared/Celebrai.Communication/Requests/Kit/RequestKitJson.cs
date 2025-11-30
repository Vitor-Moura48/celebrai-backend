namespace Celebrai.Communication.Requests.Kit;

public class RequestKitJson
{
    public string Nome { get; set; } = string.Empty;
    public bool VendaIndividual { get; set; } = false;
    public decimal KitPreco { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public int QuantidadeAluguelPorDia { get; set; } = 1;
    public List<int> ProdutosIds { get; set; } = [];
}
