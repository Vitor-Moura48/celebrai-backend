using Celebrai.Communication.Responses.Tokens;

namespace Celebrai.Communication.Responses.Fornecedor;
public record ResponseRegisteredFornecedorJson
{
    public string Nome { get; set; } = string.Empty;
    public ResponseTokenFirebaseJson Tokens { get; set; } = default!;
}
