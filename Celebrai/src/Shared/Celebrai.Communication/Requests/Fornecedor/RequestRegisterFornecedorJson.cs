namespace Celebrai.Communication.Requests.Fornecedor;
public class RequestRegisterFornecedorJson
{
    public int RaioAtuacao { get; set; }
    public bool AtendimentoPresencial { get; set; }

    public string Lograduro { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
    public string CEP { get; set; } = string.Empty;

    public string TipoFornecedor { get; set; } = string.Empty;

    public string? NomeCompleto { get; set; }
    public string? RazaoSocial { get; set; }

    public string? CPF { get; set; }
    public string? CNPJ { get; set; }
}