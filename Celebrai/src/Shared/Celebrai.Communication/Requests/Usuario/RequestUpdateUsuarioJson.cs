namespace Celebrai.Communication.Requests.Usuario;
public record RequestUpdateUsuarioJson
{
    public string Nome { get; init; } = string.Empty;
    public string Telefone { get; init; } = string.Empty;
}
