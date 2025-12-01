namespace Celebrai.Communication.Requests.Usuario;
public record RequestChangeAddressUsuarioJson
{
    public string Lograduro { get; init; } = string.Empty;
    public string Numero { get; init; } = string.Empty;
    public string CEP { get; init; } = string.Empty;
}
