using Celebrai.Communication.Responses.Produto;

namespace Celebrai.Communication.Responses.Kit;

public record ResponseKitJson
{
    public int IdKit {get; set;}
    public string Nome { get; set; } = string.Empty;
    public bool VendaIndividual { get; set; } = false;
    public decimal KitPreco { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public List<ResponseProdutoJson> Produtos {get; set;} = [];
}
