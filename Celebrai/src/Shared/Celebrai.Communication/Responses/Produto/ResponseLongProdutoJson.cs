namespace Celebrai.Communication.Responses.Produto;

public class ResponseLongProdutoJson
{
    public string Nome { get; set; } = string.Empty;
    public string NomeFornecedor { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string SubCategoria { get; set; } = string.Empty;
    public decimal PrecoUnitario { get; set; }
    public int QuantidadeAluguelPorDia { get; init; } = 1;
    public string ImagemUrl { get; set; } = string.Empty;
}
