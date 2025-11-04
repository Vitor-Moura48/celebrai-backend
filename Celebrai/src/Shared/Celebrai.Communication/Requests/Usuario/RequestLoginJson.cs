namespace Celebrai.Communication.Requests.Usuario;
public record RequestLoginJson
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}
