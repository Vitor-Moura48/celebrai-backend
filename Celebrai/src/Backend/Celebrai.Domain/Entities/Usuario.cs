namespace Celebrai.Domain.Entities;
public class Usuario
{
    public Guid IdUsuario { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Celular { get; set; }
    public string CpfUsuario { get; set; } = string.Empty;
    public string IdExterno { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public string? UrlIcon { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime DataAtualizacao { get; set; }
    public bool Ativo { get; set; }
    public string? Lograduro { get; set; }
    public string? Numero { get; set; }
    public string? CEP { get; set; }
}
