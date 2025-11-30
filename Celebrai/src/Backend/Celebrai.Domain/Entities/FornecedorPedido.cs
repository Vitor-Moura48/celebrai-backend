namespace Celebrai.Domain.Entities;

public class FornecedorPedido
{
    public Guid IdFornecedor { get; set; }
    public int IdPedido { get; set; }

    public Fornecedor Fornecedor { get; set; } = default!;
    public Pedido Pedido { get; set; } = default!;
}