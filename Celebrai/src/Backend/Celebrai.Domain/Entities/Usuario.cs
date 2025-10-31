using Celebrai.Domain.Enums;

namespace Celebrai.Domain.Entities;
public class Usuario
{
    public Guid IdUsuario { get; set; } = Guid.NewGuid();
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Celular { get; set; }
    public string CpfUsuario { get; set; } = string.Empty;
    public string IdExterno { get; set; } = string.Empty;
    public DateOnly DataNascimento { get; set; }
    public string? UrlIcon { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    public DateTime DataAtualizacao { get; set; } = DateTime.UtcNow;
    public bool Ativo { get; set; } = false;
    public string? Lograduro { get; set; }
    public string? Numero { get; set; }
    public string? CEP { get; set; }
    public RoleUsuario Role { get; set; } = RoleUsuario.Cliente;
}
