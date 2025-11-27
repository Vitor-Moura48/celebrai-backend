namespace Celebrai.Communication.Requests.Produto;

public record RequestFilterProdutoJson
{
    public string Nome { get; init; } = string.Empty;
    public string Categoria { get; init; } = string.Empty;
}
