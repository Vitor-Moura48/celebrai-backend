namespace Celebrai.Domain.Entities;
public class Fornecedor
{
    public Guid IdFornecedor { get; set; }
    public Guid IdUsuario { get; set; }
    public int? RaioAtuacao { get; set; }
    public bool AtendimentoPresencial { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime DataAtualizacao { get; set; }
    public bool Ativo { get; set; }
    public string Lograduro { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
    public string CEP { get; set; } = string.Empty;

    public Usuario Usuario { get; set; } = default!;
}
