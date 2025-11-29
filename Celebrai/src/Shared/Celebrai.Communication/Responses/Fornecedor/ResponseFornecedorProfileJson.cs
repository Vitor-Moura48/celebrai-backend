using System.Text.Json.Serialization;

namespace Celebrai.Communication.Requests.Fornecedor;

public record ResponseFornecedorProfileJson
{
    public string RaioAtuacao { get; set; } = string.Empty;
    public bool AtendimentoPresencial { get; set; }
    public string Lograduro { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
    public string CEP { get; set; } = string.Empty;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? NomeCompleto { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? RazaoSocial { get; set; }
}