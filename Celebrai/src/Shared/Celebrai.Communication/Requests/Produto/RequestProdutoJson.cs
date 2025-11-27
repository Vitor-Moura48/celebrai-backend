using Celebrai.Communication.Enums;

namespace Celebrai.Communication.Requests.Produto;
public class RequestProdutoJson
{
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public SubCategoriaEnum SubCategoria { get; set; }
    public decimal PrecoUnitario { get; set; }
}
