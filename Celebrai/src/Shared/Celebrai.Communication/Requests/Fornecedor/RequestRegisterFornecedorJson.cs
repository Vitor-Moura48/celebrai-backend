namespace Celebrai.Communication.Requests.Fornecedor;
public class RequestRegisterFornecedorJson
{
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string? Celular { get; set; }
    public string CpfUsuario { get; set; } = string.Empty;
    public DateOnly DataNascimento { get; set; }
    public string? UrlIcon { get; set; }
    public string? Lograduro { get; set; }
    public string? Numero { get; set; }
    public string? CEP { get; set; }


    public int RaioAtuacao { get; set; }
    public bool AtendimentoPresencial { get; set; }
    public string? NomeCompleto { get; set; }
    public string? RazaoSocial { get; set; }
}