namespace Celebrai.Domain.Entities;
public class Fornecedor
{
    public Guid IdFornecedor { get; set; } = Guid.NewGuid();
    public Guid IdUsuario { get; set; }
    public int? RaioAtuacao { get; set; }
    public bool AtendimentoPresencial { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    public DateTime DataAtualizacao { get; set; } = DateTime.UtcNow;
    public bool Ativo { get; set; } = true;
    public string Lograduro { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
    public string CEP { get; set; } = string.Empty;

    public Usuario Usuario { get; set; } = default!;
}
