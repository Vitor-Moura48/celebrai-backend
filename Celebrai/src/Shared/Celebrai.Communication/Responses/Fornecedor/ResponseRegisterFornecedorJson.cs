using Celebrai.Communication.Responses.Tokens;

namespace Celebrai.Communication.Responses.Fornecedor;
public record ResponseRegisteredFornecedorJson
{
    public string Nome { get; set; } = string.Empty;
    public ResponseTokensJson Tokens { get; set; } = default!;
    public string Message { get; set; } = string.Empty;
}
