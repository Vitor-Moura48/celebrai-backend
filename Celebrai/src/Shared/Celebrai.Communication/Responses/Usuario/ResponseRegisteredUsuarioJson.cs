using Celebrai.Communication.Responses.Tokens;

namespace Celebrai.Communication.Responses.Usuario;
public record ResponseRegisteredUsuarioJson
{
    public string Nome { get; set; } = string.Empty;
    public ResponseTokenFirebaseJson Tokens { get; set; } = default!;
}
