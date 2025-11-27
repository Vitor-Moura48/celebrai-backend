namespace Celebrai.Communication.Responses.Produto;

public record ResponseShortProdutoJson
{
    public string Nome { get; set; } = string.Empty;
    public string SubCategoria { get; set; } = string.Empty;
    public decimal PrecoUnitario { get; set; }
    public string ImagemUrl { get; set; } = string.Empty;
}
