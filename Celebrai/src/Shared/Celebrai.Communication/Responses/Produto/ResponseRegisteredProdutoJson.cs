using Celebrai.Communication.Enums;
using Celebrai.Communication.Responses.SubCategoria;

namespace Celebrai.Communication.Responses.Produto;
public record ResponseRegisteredProdutoJson
{
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string ImagemUrl { get; init; } = string.Empty;
    public string SubCategoria { get; init; } = string.Empty;
    public int QuantidadeAluguelPorDia { get; init; } = 1;
}
