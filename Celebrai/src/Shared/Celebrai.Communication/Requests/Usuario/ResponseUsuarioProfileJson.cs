namespace Celebrai.Communication.Requests.Usuario;
public record ResponseUsuarioProfileJson
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Celular { get; set; } = string.Empty;
    public string CpfUsuario { get; set; } = string.Empty;
    public DateOnly DataNascimento { get; set; }
    public string Lograduro { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
    public string CEP { get; set; } = string.Empty;
}
