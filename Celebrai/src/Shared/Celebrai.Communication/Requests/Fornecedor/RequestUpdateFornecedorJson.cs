namespace Celebrai.Communication.Requests.Fornecedor;

public class RequestUpdateFornecedorJson
{
    public int? RaioAtuacao { get; set; }
    public bool AtendimentoPresencial { get; set; }
    public string Lograduro { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
    public string CEP { get; set; } = string.Empty;
}
