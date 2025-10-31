namespace Celebrai.Domain.Entities;

public class ModalidadeEntregaFornecedor
{
    public int IdModalidadeEntrega { get; set; }
    public Guid IdFornecedor { get; set; }
    public decimal ValorBase { get; set; }
    public decimal PrecoKm { get; set; }

    public ModalidadeEntrega ModalidadeEntrega { get; set; } = default!;
    public Fornecedor Fornecedor { get; set; } = default!;
}