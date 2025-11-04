using Celebrai.Communication.Responses.Tokens;

namespace Celebrai.Communication.Responses.Usuario;
public record ResponseLoginUsuarioJson
{
    public string Name { get; init; } = string.Empty;
    public ResponseTokensJson Tokens { get; init; } = default!;
}
