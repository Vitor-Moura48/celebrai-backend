namespace Celebrai.Communication.Responses.Usuario;
public record ResponseRegisteredUsuarioJson
{
    public string Nome { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}
